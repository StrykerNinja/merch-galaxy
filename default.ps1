$script:project_config = "Release"
properties {
  Framework '4.6.1'
  $solution_name = "MerchantGalaxy"

  if(-not $version)
  {
    $version = "0.0.0.1"
  }
  $date = Get-Date  
  $ReleaseNumber =  $version

  Write-Host "**********************************************************************"
  Write-Host "Release Number: $ReleaseNumber"
  Write-Host "**********************************************************************"

  $base_dir = resolve-path .
  $build_dir = "$base_dir\build"
  $source_dir = "$base_dir\src"
  $test_dir = "$build_dir\test"
  $result_dir = "$build_dir\results"
  $package_dir = "$build_dir\packages"
  $slnFullPath = (Join-Path "$source_dir" "$solution_name.sln")

  $test_projects_patterns = @("*Tests")
  $test_assembly_patterns = @("*Tests.dll")

  $nuget_exe = "$base_dir\tools\nuget\nuget.exe"
}

#These are aliases for other build tasks. They typically are named after the camelcase letters (rd = Rebuild Databases)
#aliases should be all lowercase, conventionally
#please list all aliases in the help task
task default -depends InitialPrivateBuild
task rat -depends RunAllTests
task ? -depends help

task help {
   Write-Help-Header
   Write-Help-Section-Header "Comprehensive Building"
   Write-Help-For-Alias "(default)" "Intended for first build or when you want a fresh, clean local copy"
   Write-Help-Section-Header "Running Tests"
   Write-Help-For-Alias "rat" "Run all tests"
   Write-Help-Footer
   exit 0
}

#These are the actual build tasks. They should be Pascal case by convention
task InitialPrivateBuild -depends Clean, Compile, RunAllTests
task SetDebugBuild {
  $script:project_config = "Debug"
}
task CopyAssembliesForTest -Depends Compile {
  $test_projects_patterns | %{ get_projects_to_copy $_ }
}
task RunAllTests -Depends SetDebugBuild, CopyAssembliesForTest {
  $test_projects_patterns | %{ get_projects_to_test $_ }
}
task Compile -depends Clean { 
  exec { & $nuget_exe restore $slnFullPath }
  exec { msbuild.exe /t:build /v:q /p:Configuration=$project_config /p:Platform="Any CPU" /nologo $slnFullPath }
}
task Clean {
  delete_directory $build_dir
  create_directory $test_dir 
  create_directory $result_dir

  exec { msbuild /t:clean /v:q /p:Configuration=$project_config /p:Platform="Any CPU" $slnFullPath }
}
# -------------------------------------------------------------------------------------------------------------
# generalized functions for Help Section
# --------------------------------------------------------------------------------------------------------------
function Write-Help-Header($description) {
  Write-Host ""
  Write-Host "********************************" -foregroundcolor DarkGreen -nonewline;
  Write-Host " HELP " -foregroundcolor Green  -nonewline; 
  Write-Host "********************************"  -foregroundcolor DarkGreen
  Write-Host ""
  Write-Host "This build script has the following common build " -nonewline;
  Write-Host "task " -foregroundcolor Green -nonewline;
  Write-Host "aliases set up:"
}
function Write-Help-Footer($description) {
  Write-Host ""
  Write-Host " For a complete list of build tasks, view default.ps1."
  Write-Host ""
  Write-Host "**********************************************************************" -foregroundcolor DarkGreen
}
function Write-Help-Section-Header($description) {
  Write-Host ""
  Write-Host " $description" -foregroundcolor DarkGreen
}
function Write-Help-For-Alias($alias,$description) {
  Write-Host "  > " -nonewline;
  Write-Host "$alias" -foregroundcolor Green -nonewline; 
  Write-Host " = " -nonewline; 
  Write-Host "$description"
}
# -------------------------------------------------------------------------------------------------------------
# generalized functions 
# --------------------------------------------------------------------------------------------------------------
function get_projects_to_copy([string]$pattern) {
  $items = Get-ChildItem -Path $source_dir\$pattern
  if($items -ne $null -and $items.Count -gt 0)
  {
    $items | %{ copy_all_assemblies_for_test $_.Name }
  }
}
function get_projects_to_test([string]$pattern) {
  $items = Get-ChildItem -Path $source_dir\$pattern
  if($items -ne $null -and $items.Count -gt 0)
  {
    $items | %{ run_fixie_tests_on_project $_.Name }
  }
}
function run_fixie_tests_on_project([string]$project) {
  $test_assembly_patterns | %{ run_fixie_tests $project $_ }
}
function run_fixie_tests([string]$project, [string]$pattern) {
  $items = Get-ChildItem -Path $test_dir\$project $pattern
  if($items -ne $null -and $items.Count -gt 0)
  {
    $items | %{ run_fixie $project $_.Name }
  }
}
function global:zip_directory($directory,$file) {
  write-host "Zipping folder: " $test_assembly
  delete_file $file
  cd $directory
  & "$base_dir\tools\7zip\7za.exe" a -mx=9 -r $file
  cd $base_dir
}
function global:delete_file($file) {
  if($file) { remove-item $file -force -ErrorAction SilentlyContinue | out-null }
}
function global:delete_directory($directory_name) {
  rd $directory_name -recurse -force  -ErrorAction SilentlyContinue | out-null
}
function global:create_directory($directory_name) {
  mkdir $directory_name  -ErrorAction SilentlyContinue  | out-null
}
function global:run_fixie ($project, $test_assembly) {
  $assembly_to_test = $test_dir + "\" + $project + "\" + $test_assembly
  $results_output = $result_dir + "\" + $test_assembly + ".xml"
  write-host "Running Fixie Tests in: $test_assembly"
  exec { & tools\fixie\Fixie.Console.exe $assembly_to_test --NUnitXml $results_output --TeamCity off }
}
function global:Copy_and_flatten ($source,$include,$dest) {
  Get-ChildItem $source -include $include -r | cp -dest $dest
}
function global:copy_all_assemblies_for_test($projectDir){
  $bin_dir_match_pattern = "$source_dir\$projectDir\bin\$project_config"
  create_directory $test_dir\$projectDir
  Copy_and_flatten $bin_dir_match_pattern @("*.exe","*.dll","*.config","*.pdb","*.sql","*.xlsx","*.csv") $test_dir\$projectDir
}
function global:copy_files($source,$destination,$exclude=@()){    
  create_directory $destination
  Get-ChildItem $source -Recurse -Exclude $exclude | Copy-Item -Destination {Join-Path $destination $_.FullName.Substring($source.length)} 
}
function global:create-commonAssemblyInfo($version,$applicationName,$filename) {
$date = Get-Date
$currentYear = $date.Year
"using System.Reflection;
using System.Runtime.CompilerServices;

// Version information for an assembly consists of the following four values:
//
//      Year                    (Expressed as YYYY)
//      Major Release           (i.e. New Project / Namespace added to Solution or New File / Class added to Project)
//      Minor Release           (i.e. Fixes or Feature changes)
//      Build Date & Revsion    (Expressed as MMDD)
//
[assembly: AssemblyCompany(""Senderra RX"")]
[assembly: AssemblyCopyright(""Copyright (c) Senderra RX 2014 - $currentYear"")]
[assembly: AssemblyProduct(""$project_name System"")]
[assembly: AssemblyTrademark("""")]
[assembly: AssemblyCulture("""")]
[assembly: AssemblyVersion(""$version"")]
[assembly: AssemblyFileVersion(""$version"")]" | out-file $filename -encoding "utf8"
}

function script:poke-xml($filePath, $xpath, $value, $namespaces = @{}) {
  [xml] $fileXml = Get-Content $filePath

  if($namespaces -ne $null -and $namespaces.Count -gt 0) {
    $ns = New-Object Xml.XmlNamespaceManager $fileXml.NameTable
    $namespaces.GetEnumerator() | %{ $ns.AddNamespace($_.Key,$_.Value) }
    $node = $fileXml.SelectSingleNode($xpath,$ns)
  } else {
    $node = $fileXml.SelectSingleNode($xpath)
  }

  Assert ($node -ne $null) "could not find node @ $xpath"

  if($node.NodeType -eq "Element") {
    $node.InnerText = $value
  } else {
    $node.Value = $value
  }

  $fileXml.Save($filePath) 
}