using System;
using System.IO;
using System.Text.RegularExpressions;

namespace TextManipulationWithFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read the content of the file
            string filePath = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\net6.0\\", "TestFile.txt");
            string[] lines = File.ReadAllLines(filePath);
            string text = File.ReadAllText(filePath);

            // Count the number of lines and display it
            int lineCount = File.ReadLines(filePath).Count();
            Console.WriteLine($"Number of lines: {lineCount}");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();

            // Replace 'and' with '&' in the whole text and display it
            text = text.Replace("and", "&");
            Console.WriteLine("Replaced text:");
            Console.WriteLine(text);
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();

            // Find words that start with 'p', 'a', 'O' and display them
            Console.WriteLine("Words starting with 'p', 'a', or 'O':");
            FindWordsStartingWith(text, 'p');
            FindWordsStartingWith(text, 'a');
            FindWordsStartingWith(text, 'O');
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();

            // Split the text by comma(,) and display each text by index
            string[] splitText = text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("Split text:");
            for (int i = 0; i < splitText.Length; i++)
            {
                Console.WriteLine($"[{i}] {splitText[i]}");
            }
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();

            // Count the number of occurrences of the word 'the'
            int wordCount = CountWordOccurrences(text, "the");
            Console.WriteLine($"Number of occurrences of 'the': {wordCount}");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();
        }

        static void FindWordsStartingWith(string text, char startingChar)
        {
            string[] words = text.Split(new[] { ' ', '\n', '\r', '\t', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                if (word.StartsWith(startingChar.ToString()))
                {
                    Console.WriteLine(word);
                }
            }
        }

        static int CountWordOccurrences(string text, string word)
        {
            //int count = 0;
            //int index = -1;

            //while ((index = text.IndexOf(word, index + 1, StringComparison.OrdinalIgnoreCase)) != -1)
            //{
            //    // Check if the match is a whole word
            //    bool isWholeWord = IsWholeWord(text, word, index);

            //    if (isWholeWord)
            //        count++;
            //}

            //return count;

            //Using Regex
            string pattern = @"\b" + Regex.Escape(word) + @"\b";
            MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);
            return matches.Count;
        }

        static bool IsWholeWord(string text, string word, int index)
        {
            // Check if the previous character is a word character
            if (index > 0 && char.IsLetterOrDigit(text[index - 1]))
                return false;

            int wordEndIndex = index + word.Length;

            // Check if the next character is a word character
            if (wordEndIndex < text.Length && char.IsLetterOrDigit(text[wordEndIndex]))
                return false;

            return true;
        }
    }
}
