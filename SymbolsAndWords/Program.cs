using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Given a list of chemical symbols which can be made 
// of 1 or 2 characters, find the longest word in the 
// dictionary that can be 
// constructed using the given list of symbols.

// For simplicity, in this implementation,
// I have used a small list of words instead of loading up 
// all words in the dictionary
namespace SymbolsAndWords
{
    class Program
    {
        private static HashSet<string> symbolSet;

        static void Main(string[] args)
        {

            List<string> words = new List<string>();

            words.Add("a");
            words.Add("ab");
            words.Add("abcdefg");
            words.Add("ac");
            words.Add("abc");
            words.Add("abcdef");
            words.Add("abapq");
            words.Add("abefecdabc");
            words.Add("abah");
            words.Add("aaaaa");
            words.Add("pqrstuvvwxhh");

            List<string> symbols = new List<string>();

            symbols.Add("a");
            symbols.Add("ab");
            symbols.Add("bc");
            symbols.Add("cd");
            symbols.Add("de");
            symbols.Add("e");
            symbols.Add("ef");
            symbols.Add("fg");

            string result = FindLargestWordMadeUpOfSymbols(symbols, words);

            if (!string.IsNullOrEmpty(result))
            {
                Console.WriteLine("Result is {0}", result);
            }
            else
            {
                Console.WriteLine("None of the words are made up of only symbols");
            }
        }

        static bool IsWordMadeOfSymbols(string word)
        {
            int i = 0; 

            while(i < word.Length)
            {
               string twoLetteredPrefix = null;

               string oneLetteredPrefix = null;

               if ((i + 1) < word.Length)
               {
                   twoLetteredPrefix = word.Substring(i, 2);
               }
               
               oneLetteredPrefix = word[i].ToString();
                 
               if (symbolSet.Contains(twoLetteredPrefix))
               {
                    i += 2; 
               }
               else if (symbolSet.Contains(oneLetteredPrefix)) 
               {
                   i++;
               }
               else
               {
                   if ((i - 1) >= 0)
                   {
                       // since the current character didnt match, just check if current + previous works
                       // for example 'afg' when 'af' and 'fg' are symbols but g alone is not and 'i' currently points to 'g'
                       twoLetteredPrefix = word.Substring(i - 1, 2);

                       if (symbolSet.Contains(twoLetteredPrefix))
                       {
                           i++;
                       }
                       else
                       {
                           return false;
                       }
                   }
                   else
                   {
                       return false;
                   }

               }
           }

           return true;
        }

        static string FindLargestWordMadeUpOfSymbols(List<string> symbols, List<string> words)
        {
            words.Sort(CompareByLengthDescending);

            symbolSet = CreateHashSet(symbols);

            foreach (string word in words)
            {
                if (IsWordMadeOfSymbols(word))
                {
                    return word;
                }
            }

            return null;
        }

        private static HashSet<string> CreateHashSet(List<string> symbols)
        {
            HashSet<string> hashSet = new HashSet<string>(symbols);

            return hashSet;
        }

        // Places s1 before s2 if length of s1 is greater than s2
        static int CompareByLengthDescending(string s1, string s2)
        {
            return s1.Length > s2.Length ? -1 : 1;
        }
    }
}
