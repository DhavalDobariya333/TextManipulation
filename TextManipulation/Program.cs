using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace TextManipulationNoFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read the content of the file
            string filePath = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\net6.0\\", "TestFile.txt");
            string[] lines = File.ReadAllLines(filePath);
            string text = string.Join(Environment.NewLine, lines);

            // Count the number of lines and display it
            int lineCount = lines.Length;

            int nonEmptyLineCount = 0;

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    nonEmptyLineCount++;
                }
            }

            Console.WriteLine("1.) Number of lines: " + lineCount +"  & Number of non - empty lines: " + nonEmptyLineCount);
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();

            // Replace 'and' with '&' in the whole text and display it
            string replaceText;
            replaceText = ReplaceWord(text, "and", "&");
            Console.WriteLine("2.) Replaced text: and to & ");
            Console.WriteLine(replaceText);
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();

            // Find words that start with 'p', 'a', 'O' and display them
            Console.WriteLine("3.) Words starting with 'p', 'a', or 'O':");
            FindWordsStartingWith(text, 'p');
            FindWordsStartingWith(text, 'a');
            FindWordsStartingWith(text, 'O');
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();

            // Split the text by comma(,) and display each text by index
            string[] splitText = SplitByComma(text);
            Console.WriteLine("4.) Split text:");
            for (int i = 0; i < splitText.Length; i++)
            {
                Console.WriteLine($"[{i}] {splitText[i]}");
            }
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();

            // Count the number of occurrences of the word 'the'
            int wordCount = CountWord(text, "the");
            Console.WriteLine($"5.) Number of occurrences of 'the': {wordCount}");
            Console.WriteLine("-------------------------------------");


        }

        static string ReplaceWord(string text, string oldWord, string newWord)
        {
            int startIndex = 0;
            while (startIndex < text.Length)
            {
                int wordIndex = text.IndexOf(oldWord, startIndex, StringComparison.OrdinalIgnoreCase);
                if (wordIndex == -1)
                    break;

                text = text.Substring(0, wordIndex) + newWord + text.Substring(wordIndex + oldWord.Length);
                startIndex = wordIndex + newWord.Length;
            }

            return text;
        }

        static void FindWordsStartingWith(string text, char startingChar)
        {
            string[] words = text.Split(new[] { ' ', '\n', '\r', '\t' , ',', '.'});
            foreach (string word in words)
            {
                if (word.StartsWith(startingChar.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(word);
                }
            }
        }

        static string[] SplitByComma(string text)
        {
            return text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        static int CountWord(string text, string word)
        {
            int count = 0;
            int startIndex = 0;
            while (startIndex < text.Length)
            {
                int wordIndex = text.IndexOf(word, startIndex, StringComparison.OrdinalIgnoreCase);
                if (wordIndex == -1)
                    break;

                count++;
                startIndex = wordIndex + word.Length;
            }

            return count;
        }
    }
}
