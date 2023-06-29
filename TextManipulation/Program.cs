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
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);  // This line retrieves the directory path; like this ...\bin\Debug\net6.0  but we don't want to go on bin folder
            string parentDir = Directory.GetParent(path).FullName;                          // so to get parent directory use FullName to get full path   ...\bin\Debug 
            string binpath =  Directory.GetParent(parentDir).FullName;                      // gives ...\bin\
            string filePath =  Directory.GetParent(binpath).FullName +"\\TestFile.txt";     // Here we get parent directory now we have to combine TestFile.txt name
            string[] lines = File.ReadAllLines(filePath);                                   // This line reads all the lines from the filePath and store in array in lines[]
            string text = string.Join(Environment.NewLine, lines);                          // Now its join arrays of line in to one line and store in to text

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
                Console.WriteLine($"[{i}] {splitText[i]}");             // This is string interpolation syntax i set as number and splitText[i] set text from array
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
            // Check if the input text is null and return null if it is
            if (text == null)
                return null;

            // Check if the oldWord is null and throw an exception if it is
            if (oldWord == null)
                throw new ArgumentNullException(nameof(oldWord));

            // Create a character array to hold the result of the replacement
            char[] result = new char[text.Length];
            int resultIndex = 0;             // Index to keep track of the current position in the result array
            int startIndex = 0;              // Starting index for searching the oldWord in the text

            while (true)
            {
                // Find the index of the next occurrence of the oldWord starting from the startIndex
                int matchIndex = IndexOfWord(text, oldWord, startIndex);

                // If no more occurrences are found, copy the remaining text to the result array and exit the loop
                if (matchIndex == -1)
                {
                    for (int i = startIndex; i < text.Length; i++)
                    {
                        result[resultIndex++] = text[i];
                    }
                    break;
                }

                // Copy the text from the startIndex to the matchIndex to the result array
                for (int i = startIndex; i < matchIndex; i++)
                {
                    result[resultIndex++] = text[i];
                }

                // Copy the characters of the newWord to the result array
                for (int i = 0; i < newWord.Length; i++)
                {
                    result[resultIndex++] = newWord[i];
                }
                // Update the startIndex to the position after the matched oldWord
                startIndex = matchIndex + oldWord.Length;
            }

            // Create a new string from the result character array, starting from index 0 and up to the resultIndex
            return new string(result, 0, resultIndex);
        }

        // Finds words in the text that start with a specified character and displays them
        static void FindWordsStartingWith(string text, char startingChar)
        {
            string currentWord = ""; // Variable to store the current word being processed
            bool isInWord = false;  // Flag to indicate if the current character is part of a word

            // Iterate over each character in the text
            foreach (char c in text)
            {
                // Check if the character is a delimiter (space, tab, newline, comma, period)
                if (c == ' ' || c == '\t' || c == '\n' || c == '\r' || c == ',' || c == '.')
                {
                    // Check if we were previously in a word and the currentWord starts with the specified startingChar     like person 
                    if (isInWord && currentWord.Length > 0 && currentWord[0] == startingChar)
                        Console.WriteLine(currentWord);  // Display the currentWord

                    currentWord = ""; // Reset the currentWord
                    isInWord = false; // Set the isInWord flag to false
                }
                else
                {
                    currentWord += c;  // Append the current character to the currentWord  ;  like perso  to person
                    isInWord = true; // Set the isInWord flag to true to indicate we are in a word
                }
            }
        }

        static int CountWord(string text, string word)
        {
            int count = 0;
            int currentIndex = 0;       // Indicating the starting position for searching the word

            // Find the index of the first occurrence of the word in the text
            int foundIndex = FindSubstringIndex(text, word);

            while (foundIndex != -1)
            {
                // Increment the count for each occurrence found
                count++;
                // Update the currentIndex to the position after the matched word
                currentIndex = foundIndex + word.Length;
                // Find the index of the next occurrence of the word starting from the currentIndex 
                foundIndex = FindSubstringIndex(text, word, currentIndex);
            }

            // Return the total count of occurrences
            return count;
        }
        static int FindSubstringIndex(string input, string word, int startIndex = 0)
        {
            int inputLength = input.Length;
            int wordLength = word.Length;

            for (int i = startIndex; i <= inputLength - wordLength; i++)
            {
                int j;

                // Compare each character in the substring (word) with the corresponding character in the input string
                for (j = 0; j < wordLength; j++)
                {
                    // If a character mismatch is found, break the inner loop and continue searching
                    if (input[i + j] != word[j])
                        break;
                }
                // If the inner loop completes without a character mismatch, it means the substring (word) is found
                if (j == wordLength)
                {
                    // Check if the characters before and after the word are non-alphanumeric
                    // This is done to ensure that the word is not part of a larger word
                    if ((i == 0 || !char.IsLetterOrDigit(input[i - 1])) && (i + wordLength == inputLength || !char.IsLetterOrDigit(input[i + wordLength])))
                // Return the index of the first character of the found 
                return i;
                }
            }
            // If the substring is not found, return -1
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
            int arrayIndex = 0;     // Index to keep track of the current position in the result array

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
                    // Create a new string from the extracted substring and store it in the result array
                    result[arrayIndex] = new string(substringChars);

                    // Update the startIndex to the position after the comma for extracting the next substring
                    startIndex = i + 1;

                    // Move to the next position in the result array
                    arrayIndex++;
                }
            }

            // Add the remaining substring after the last comma (or the whole text if no comma is found)
            int lastSubstringLength = text.Length - startIndex;
            char[] lastSubstringChars = new char[lastSubstringLength];
            for (int j = 0; j < lastSubstringLength; j++)
            {
                lastSubstringChars[j] = text[startIndex + j];
            }

            // Create a new string from the remaining substring and store it in the result array
            result[arrayIndex] = new string(lastSubstringChars);

            // Return the array of substrings
            return result;
        }

        static int IndexOfWord(string input, string word, int startIndex)
        {
            // Get the length of the input string and the word
            int length = input.Length;
            int wordLength = word.Length;
            int matchIndex = -1;   // Index to store the position of the first character of the matched word
            bool foundWord = false;

            for (int i = startIndex; i < length; i++)
            {
                // Check if the current character in the input string matches the first character of the word
                if (input[i] == word[0])
                {
                    int j;

                    // Compare each character in the word with the character in the input string
                    for (j = 1; j < wordLength && i + j < length; j++)
                    {
                        // If a character mismatch is found, break the inner loop and continue searching
                        if (input[i + j] != word[j])
                            break;
                    }

                    // If the inner loop completes without a character mismatch, it means the word is found
                    if (j == wordLength)
                    {
                        matchIndex = i; // Set the matchIndex to the current position in the input string
                        foundWord = true; // Set the foundWord flag to true
                        break; // Exit the loop since the word is found
                    }
                }
            }

            // If the word is found, return the index of the first character of the word
            // Otherwise, return -1 to indicate that the word is not found
            return foundWord ? matchIndex : -1;
        }
    }
}
