using System;
using System.IO;
using System.Linq;

namespace task1.lang_with_go_to
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"..\text.txt";
            StreamReader sr = new StreamReader(path);
            string text = sr.ReadToEnd();
            sr.Close();

            Console.WriteLine($"Input: {text}");

            int N = 300;
            int i = 0;

            string[] prohibitedValues = { "", "-", "no", "from", "the", "by", "and", "i", "in", "or", "any", "for", "to", "a", "\"", "of", "on", "at", "is", "\r", "\n", "\r\n", "\n\r"};

            string[] allWords = new string[1000000];
            string currWord = null;
            int amountWords = 0;

        #region TextParsing
        loop:
            if ((text[i] >= 65) && (text[i] <= 90) || (text[i] >= 97) && (text[i] <= 122) || text[i] == 45 || text[i] == 46 || text[i] == 44 || text[i] == 33 || text[i] == 63)
            {
                if ((text[i] >= 65) && (text[i] <= 90)) // caseChange
                {
                    currWord += (char)(text[i] + 32);
                }
                else
                {
                    if (text[i] != 46 && text[i] != 44 && text[i] != 33 && text[i] != 63) // . ! ? ,
                        currWord += text[i];
                }
            }
            else
            {
                int p = 0;
                bool isAllowed = true;

            prohibitedValues_Loop:
                if (currWord == prohibitedValues[p])
                {
                    isAllowed = false;
                    currWord = null;
                }
                else
                {
                    if(p+1 < prohibitedValues.Length)
                    {
                        p++;
                        goto prohibitedValues_Loop;
                    }
                }

                if (isAllowed)
                {
                    allWords[amountWords] = currWord;
                    amountWords++;
                    currWord = null;
                }
            }
            i++;
            if (i < text.Length)
            {
                goto loop;
            }
            else
            {
                int p = 0;
                bool isAllowed = true;

            prohibitedValues_Loop:
                if (currWord == prohibitedValues[p])
                {
                    isAllowed = false;
                    currWord = null;
                }
                else
                {
                    if (p + 1 < prohibitedValues.Length)
                    {
                        p++;
                        goto prohibitedValues_Loop;
                    }
                }

                if (isAllowed)
                {
                    allWords[amountWords] = currWord;
                    amountWords++;
                    currWord = null;
                }
            }
            #endregion

            string[] uniqueWord = new string[1000000];
            int[] wordCounter = new int[1000000];

        #region WordSorting
            i = 0;
            int posIns = 0;
            bool needIns = true;
            int j = 0;
            int repeats = 0;
        counterLoop:
            posIns = 0;
            needIns = true;
            j = 0;

        loop2:
            if (j < uniqueWord.Length && uniqueWord[j] != null)
            {
                if (uniqueWord[j] == allWords[i])
                {
                    posIns = j;
                    needIns = false;
                    goto loop2_End;

                }
                j++;
                goto loop2;
            }
        loop2_End:
            if (needIns)
            {
                uniqueWord[i - repeats] = allWords[i];
                wordCounter[i - repeats] = 1;
            }
            else
            {
                wordCounter[posIns] += 1;
                repeats++;
            }
            i++;
            if (i < allWords.Length && allWords[i] != null)
            {

                goto counterLoop;
            }
            int length = wordCounter.Length;
            j = 0;
            int inner_i = 0;


        bubbleSort_OuterLoop:
            if (j < length && wordCounter[j] != 0)
            {
                inner_i = 0;
            bubbleSort_InnerLoop:
                if (inner_i < length - j - 1 && wordCounter[inner_i] != 0)
                {
                    if (wordCounter[inner_i] < wordCounter[inner_i + 1])
                    {
                        int temp = wordCounter[inner_i];
                        wordCounter[inner_i] = wordCounter[inner_i + 1];
                        wordCounter[inner_i + 1] = temp;
                        string temp2 = uniqueWord[inner_i];
                        uniqueWord[inner_i] = uniqueWord[inner_i + 1];
                        uniqueWord[inner_i + 1] = temp2;
                    }
                    inner_i++;
                    goto bubbleSort_InnerLoop;
                }

                j++;
                goto bubbleSort_OuterLoop;
            }
            int z = 0;
        #endregion

        show:
            if (z < length && uniqueWord[z] != null && z < N)
            {
                Console.WriteLine($"{uniqueWord[z]}, {wordCounter[z]}");
                z++;
                goto show;
            }
        }
    }
}
