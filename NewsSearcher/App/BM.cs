using NewsSearcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsSearcher.App
{
    public class BM : StringMatcher
    {
        public BM(StringMatcher strPair) : base(strPair)
        {

        }

        public BM(string keywords, RSSItem info, string[] contents) : base(keywords, info, contents)
        {

        }

        public override int Match(string text)
        {
            int[] last = BuildLast(Keywords);
            int n = text.Length;
            int m = Keywords.Length;
            int i = m - 1;
            if (i > n - 1)
                return -1; // no match if pattern is
                           // longer than text
            int j = m - 1;

            do
            {
                if (Keywords[j] == text[i])
                    if (j == 0)
                        return i; // match
                    else
                    { // looking-glass technique
                        i--;
                        j--;
                    }
                else
                { // character jump technique
                    int lo = last[text[i]];//last occ
                    i = i + m - Math.Min(j, 1 + lo);
                    j = m - 1;
                }
            } while (i <= n - 1);
            return -1;// no match  
        } // end of bmMatch()


        public int[] BuildLast(String pattern)
        {
            /* Return array storing index of last    occurrence of each ASCII char in pattern. */
            {
                int[] last = new int[128];// ASCII char set    
                for (int i = 0; i < 128; i++)
                    last[i] = -1; // initialize array    

                for (int i = 0; i < pattern.Length; i++)
                    last[pattern[i]] = i;

                return last;
            } // end of buildLast()
        }

        
    }
}