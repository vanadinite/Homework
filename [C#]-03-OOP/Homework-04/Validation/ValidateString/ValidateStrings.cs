﻿
namespace Validation.ValidateString
{
    using System;
    using System.Linq;
    using IOHelpers;

    public class ValidateStrings
    {
        public static void ValidateName(string value)
        {
            #region validate
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Name is null or empty");
            }
            else if (!char.IsUpper(value[0]))
            {
                throw new ArgumentException("Names must begin with a capital letter");
            }

            foreach (var chr in value)
            {
                if (!char.IsLetter(chr))
                {
                    throw new ArgumentException("Names can only contain valid letters");
                }
            }
            #endregion
        }

        /// <summary>
        /// Read a file of forbidden words and check the text 
        /// for them.
        /// Check for empty string.
        /// </summary>
        /// <param name="value"></param>
        public static void ValidateComment(string value)
        {
            var words = value
                .Trim()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            if (!(0<words.Count&&words.Count <= 200))
            {
                throw new ArgumentException("Comment must be between 0 and 200 words");
            }

            var forbidden = ReadWriteFile.ReadForbiddenWordsFromFile("forbidden.txt");

            foreach (var word in forbidden)
            {
                CheckWordLength(word);
                CheckForLettersAndDigits(word);

                var check = words.Where(current => current == word).Count();
                if (check > 0)
                {
                    throw new ArgumentException("Using inappropriate language");
                }
            }
        }
        
        public static void ValidateClassID(string value)
        {
            CheckWordLength(value);
            CheckForLettersAndDigits(value);
        }

        #region Private Methods
        private static void CheckWordLength(string word)
        {
            if (word.Length > 30)
            {
                throw new ArgumentException("Words must be less than 30 symbols long");
            }
        }

        private static void CheckForLettersAndDigits(string word)
        {
            foreach (var ltr in word)
            {
                if (!char.IsLetter(ltr) || !char.IsDigit(ltr) || ltr != ' ')
                {
                    throw new ArgumentException("Only letters and digits are allowed");
                }
            }
        }
        #endregion
    }
}
