using System;
using System.IO;

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
                if (word.StartsWith(startingChar.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(word);
                }
            }
        }

        static int CountWordOccurrences(string text, string word)
        {
            int count = 0;
            int index = text.IndexOf(word, StringComparison.OrdinalIgnoreCase);
            while (index != -1)
            {
                count++;
                index = text.IndexOf(word, index + 1, StringComparison.OrdinalIgnoreCase);
            }

            return count;
        }
    }
}
