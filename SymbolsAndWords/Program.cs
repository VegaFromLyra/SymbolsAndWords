using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

        // returns 0 if they are equal
        // returns positive if s1 > s2
        // retturns negative if s1 < s2
        static int CompareByLength(string s1, string s2)
        {
            return s1.Length.CompareTo(s2.Length);
        }

        // returns 0 if they are equal
        // returns positive if s2 > s1
        // retturns negative if s2 < s1
        static int CompareByLengthDescending(string s1, string s2)
        {
            return s1.Length.CompareTo(s2.Length) > 0 ? -1 : 1;
        }
    }
}
