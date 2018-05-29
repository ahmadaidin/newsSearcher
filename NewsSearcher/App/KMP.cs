using NewsSearcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsSearcher.App
{
    public class KMP : StringMatcher
    {
        public KMP(StringMatcher strPair) : base(strPair)
        {

        }

        public KMP(string keywords, RSSItem info, string[] contents) : base(keywords, info, contents)
        {

        }


        public override int Match(string text)
        {
            int n = text.Length;
            int m = Keywords.Length;

            int[] fail = ComputeFail(Keywords);

            int i = 0;
            int j = 0;

            while (i<n)
            {
                if (Keywords[j] == text[i])
                {
                    if (j == m - 1)
                        return i - m + 1;
                    i++;
                    j++;
                }
                else if (j > 0)
                    j = fail[j - 1];
                else
                    i++;
            }

            return -1;
        }

        public int[] ComputeFail(string Keywords)
        {
            int[] fail = new int[Keywords.Length];
            fail[0] = 0;

            int m = Keywords.Length;
            int j = 0;
            int i = 1;

            while (i < m)
            {
                if (Keywords[j] == Keywords[i])
                {
                    fail[i] = j + 1;
                    i++;
                    j++;
                }
                else if (j > 0)
                    j = fail[j - 1];
                else
                {
                    fail[j] = 0;
                    i++;
                }
            }
            return fail;
        }

    }
}