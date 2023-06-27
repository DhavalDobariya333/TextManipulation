using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace TextManipulationNoFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read the content of the file
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string parentDir = Directory.GetParent(path).FullName;
            string binpath =  Directory.GetParent(parentDir).FullName;
            string filePath =  Directory.GetParent(binpath).FullName +"\\TestFile.txt";
            string[] lines = File.ReadAllLines(filePath);
            string text = string.Join(Environment.NewLine, lines);

            // Count the number of lines and display it
            int lineCount = lines.Length;

            Console.WriteLine("1.) Number of lines: " + lineCount);
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
            if (text == null)
                return null;

            if (oldWord == null)
                throw new ArgumentNullException(nameof(oldWord));

            char[] result = new char[text.Length];
            int resultIndex = 0;
            int startIndex = 0;

            while (true)
            {
                int matchIndex = IndexOfWord(text, oldWord, startIndex);

                if (matchIndex == -1)
                {
                    for (int i = startIndex; i < text.Length; i++)
                    {
                        result[resultIndex++] = text[i];
                    }
                    break;
                }

                for (int i = startIndex; i < matchIndex; i++)
                {
                    result[resultIndex++] = text[i];
                }

                for (int i = 0; i < newWord.Length; i++)
                {
                    result[resultIndex++] = newWord[i];
                }

                startIndex = matchIndex + oldWord.Length;
            }

            return new string(result, 0, resultIndex);
        }

        static void FindWordsStartingWith(string text, char startingChar)
        {
            string currentWord = "";
            bool isInWord = false;

            foreach (char c in text)
            {
                if (c == ' ' || c == '\t' || c == '\n' || c == '\r' || c == ',' || c == '.')
                {
                    if (isInWord && currentWord.Length > 0 && currentWord[0] == startingChar)
                        Console.WriteLine(currentWord);

                    currentWord = "";
                    isInWord = false;
                }
                else
                {
                    currentWord += c;
                    isInWord = true;
                }
            }
        }

        static int CountWord(string text, string word)
        {
            int count = 0;
            int currentIndex = 0;
            int foundIndex = FindSubstringIndex(text, word);

            while (foundIndex != -1)
            {
                count++;
                currentIndex = foundIndex + word.Length;
                foundIndex = FindSubstringIndex(text, word, currentIndex);
            }

            return count;
        }
        static int FindSubstringIndex(string input, string word, int startIndex = 0)
        {
            int inputLength = input.Length;
            int wordLength = word.Length;

            for (int i = startIndex; i <= inputLength - wordLength; i++)
            {
                int j;
                for (j = 0; j < wordLength; j++)
                {
                    if (input[i + j] != word[j])
                        break;
                }
                if (j == wordLength)
                {
                    // Check if the characters before and after the word are non-alphanumeric
                    if ((i == 0 || !char.IsLetterOrDigit(input[i - 1])) && (i + wordLength == inputLength || !char.IsLetterOrDigit(input[i + wordLength])))
                        return i;
                }
            }

            return -1;
        }

        static string[] SplitByComma(string text, char comma = ',')
        {
            // Count the number of commas in the input string
            int count = 0;
            foreach (char c in text)
            {
                if (c == comma)
                {
                    count++;
                }
            }

            // Create an array to hold the substrings
            string[] result = new string[count + 1];

            // Split the string by iterating over each character
            int startIndex = 0;
            int arrayIndex = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == comma)
                {
                    // Extract the substring between startIndex and i
                    int substringLength = i - startIndex;
                    char[] substringChars = new char[substringLength];
                    for (int j = 0; j < substringLength; j++)
                    {
                        substringChars[j] = text[startIndex + j];
                    }
                    result[arrayIndex] = new string(substringChars);

                    startIndex = i + 1;
                    arrayIndex++;
                }
            }

            // Add the remaining substring after the last comma
            int lastSubstringLength = text.Length - startIndex;
            char[] lastSubstringChars = new char[lastSubstringLength];
            for (int j = 0; j < lastSubstringLength; j++)
            {
                lastSubstringChars[j] = text[startIndex + j];
            }
            result[arrayIndex] = new string(lastSubstringChars);

            return result;
        }

        static int IndexOfWord(string input, string word, int startIndex)
        {
            int length = input.Length;
            int wordLength = word.Length;
            int matchIndex = -1;
            bool foundWord = false;

            for (int i = startIndex; i < length; i++)
            {
                if (input[i] == word[0])
                {
                    int j;

                    for (j = 1; j < wordLength && i + j < length; j++)
                    {
                        if (input[i + j] != word[j])
                            break;
                    }

                    if (j == wordLength)
                    {
                        matchIndex = i;
                        foundWord = true;
                        break;
                    }
                }
            }

            return foundWord ? matchIndex : -1;
        }
    }
}
