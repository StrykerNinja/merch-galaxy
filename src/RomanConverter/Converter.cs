namespace RomanConverter
{
    using System;
    using System.Collections.Generic;

    public class Converter
    {
        private static readonly Dictionary<char, int> _numerals = new Dictionary<char, int>() { { 'I', 1 }, { 'V', 5 }, { 'X', 10 }, { 'L', 50 }, { 'C', 100 }, { 'D', 500 }, { 'M', 1000 } };
        private static readonly HashSet<char> _invalidMultipliersAndSubtractors = new HashSet<char>() { 'V', 'L', 'D' };
        private static readonly Dictionary<char, HashSet<char>> _validSubtractors = new Dictionary<char, HashSet<char>>() { { 'I', new HashSet<char> { 'V', 'X' } }, { 'X', new HashSet<char> { 'L', 'C' } }, { 'C', new HashSet<char> { 'D', 'M' } } };

        public int GetIntFromRoman(string roman)
        {
            if (string.IsNullOrWhiteSpace(roman))
            {
                throw new ArgumentException();
            }
            roman = roman.ToUpperInvariant();
            var result = 0;
            for (int i = 0; i < roman.Length; i++)
            {
                var current = roman[i];
                if (!_numerals.ContainsKey(current))
                {
                    throw new InvalidRomanNumeralCharacterException();
                }
                var next = ((i + 1) >= roman.Length) ? 'n' : roman[i + 1];
                if (current == next && _invalidMultipliersAndSubtractors.Contains(current))
                {
                    throw new InvalidRomanNumeralMultiplierException();
                }
                if (next != 'n' && _numerals[next] > _numerals[current] && _invalidMultipliersAndSubtractors.Contains(current))
                {
                    throw new InvalidRomanNumeralSubtractorException();
                }
                var prev = (i == 0) ? 'n' : roman[i - 1];
                if (prev != 'n' && next != 'n' && prev == current && _numerals[next] > _numerals[current])
                {
                    throw new TooManySmallRomanSubtractorsInARowException();
                }
                var afterNext = ((i + 2) >= roman.Length) ? 'n' : roman[i + 2];
                if (current == prev && current == next && current == afterNext)
                {
                    throw new TooManyRomanNumeralsInARowException();
                }
                if (next != 'n' && _numerals[next] > _numerals[current])
                {
                    if (_validSubtractors.ContainsKey(current) && _validSubtractors[current].Contains(next))
                    {
                        result -= _numerals[current];
                    }
                    else
                    {
                        throw new InvalidRomanNumeralSubtractorException();
                    }
                }
                else
                {
                    result += _numerals[current];
                }
            }
            return result;
        }
    }
}
