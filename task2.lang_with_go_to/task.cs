using System;
using System.IO;

namespace task2.lang_with_go_to
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"..\text.txt";
            StreamReader sr = new StreamReader(path);
            string text = sr.ReadToEnd();
            sr.Close();

            int i = 0;
            string currWord = null;
            string[] allWords = new string[1000000];
            string[,] wordsOnPages = new string[50000, 50000];
            int amountWords = 0;
            int amountRows = 0;
            int amountPages = 0;
            int wordsOnPageCounter = 0;

        #region TextParsing
        loop:
            if ((text[i] >= 65) && (text[i] <= 90) || (text[i] >= 97) && (text[i] <= 122) || text[i] == 45 || text[i] == 234 || text[i] == 225 || text[i] == 224)
            {
                if ((text[i] >= 65) && (text[i] <= 90))
                {
                    currWord += (char)(text[i] + 32);
                }
                else
                {
                    currWord += text[i];
                }
            }
            else
            {
                if (text[i] == '\n')
                {
                    amountRows++;
                }
                if (amountRows > 45)
                {
                    amountPages++;
                    wordsOnPageCounter = 0;
                    amountRows = 0;
                }
                if (currWord != "" && currWord != null && currWord != "-" && currWord != "no" && currWord != "from" && currWord != "the" && currWord != "by" && currWord != "and" && currWord != "i" && currWord != "in" && currWord != "or" && currWord != "any" && currWord != "for" && currWord != "to" && currWord != "\"" && currWord != "a" && currWord != "on" && currWord != "of" && currWord != "at" && currWord != "is" && currWord != "\n" && currWord != "\r" && currWord != "\r\n" && currWord != "\n\r")
                {

                    allWords[amountWords] = currWord;
                    amountWords++;
                    wordsOnPages[amountPages, wordsOnPageCounter] = currWord;
                    wordsOnPageCounter++;
                }
                currWord = "";
            }
            i++;
            if (i < text.Length)
            {
                goto loop;
            }
            else
            {
                if (currWord != "" && currWord != null && currWord != "-" && currWord != "no" && currWord != "from" && currWord != "the" && currWord != "by" && currWord != "and" && currWord != "i" && currWord != "in" && currWord != "or" && currWord != "any" && currWord != "for" && currWord != "to" && currWord != "\"" && currWord != "a" && currWord != "on" && currWord != "of" && currWord != "at" && currWord != "is" && currWord != "\n" && currWord != "\r" && currWord != "\r\n" && currWord != "\n\r")
                {
                    allWords[amountWords] = currWord;
                    amountWords++;
                }
            }
            #endregion

            string[] uniqueWord = new string[100000];
            int[] wordCounter = new int[100000];

            #region Sorting
            i = 0;
            int posIns = 0;
            bool needIns = true;
            int j = 0;
            int repeats = 0;
        counterLoop:
            posIns = 0;
            j = 0;
            needIns = true;
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
            int k = 0;
            string[] uniqueWordsLess100 = new string[100000];
            int LastInsert = 0;
        less_100:
            if (k < length && uniqueWord[k] != null)
            {
                if (wordCounter[k] <= 100)
                {
                    uniqueWordsLess100[LastInsert] = uniqueWord[k];
                    LastInsert++;
                }
                k++;
                goto less_100;
            }
            int write = 0;
            int sort = 0;
            bool wordSwap = false;
            int counter = 0;
            int currWordLenth = 0;
            int nextWordLenth = 0;
        bubbleSort_OuterLoop:
            if (write < uniqueWordsLess100.Length && uniqueWordsLess100[write] != null)
            {
                sort = 0;
            bubbleSort_InnerLoop:
                if (sort < uniqueWordsLess100.Length - write - 1 && uniqueWordsLess100[sort + 1] != null)
                {
                    currWordLenth = uniqueWordsLess100[sort].Length;
                    nextWordLenth = uniqueWordsLess100[sort + 1].Length;

                    int compare_lenth = currWordLenth > nextWordLenth ? nextWordLenth : currWordLenth;

                    wordSwap = false;
                    counter = 0;
                alphabet_condition:

                    if (uniqueWordsLess100[sort][counter] > uniqueWordsLess100[sort + 1][counter])
                    {
                        wordSwap = true;
                        goto alphabet_conditionEnd;
                    }
                    if (uniqueWordsLess100[sort][counter] < uniqueWordsLess100[sort + 1][counter])
                    {
                        goto alphabet_conditionEnd;
                    }
                    counter++;
                    if (counter < compare_lenth)
                    {
                        goto alphabet_condition;
                    }
                alphabet_conditionEnd:
                    if (wordSwap)
                    {
                        string temp = uniqueWordsLess100[sort];
                        uniqueWordsLess100[sort] = uniqueWordsLess100[sort + 1];
                        uniqueWordsLess100[sort + 1] = temp;
                    }
                    sort++;
                    goto bubbleSort_InnerLoop;
                }
                write++;
                goto bubbleSort_OuterLoop;
            }
            k = 0;
        #endregion


        show:
            if (k < uniqueWordsLess100.Length && uniqueWordsLess100[k] != null)
            {
                Console.Write($"{uniqueWordsLess100[k]} - ");
                int fd = 0;
                int sd = 0;
                int[] wordOnPage = new int[100];
                int pageInsert = 0;

            page_condition:
                if (fd < 10000 && wordsOnPages[fd, 0] != null)
                {
                    sd = 0;
                pageWord_condition:
                    if (sd < 10000 && wordsOnPages[fd, sd] != null)
                    {
                        if (wordsOnPages[fd, sd] == uniqueWordsLess100[k])
                        {
                            wordOnPage[pageInsert] = fd + 1;
                            pageInsert++;
                            fd++;
                            goto page_condition;
                        }
                        sd++;
                        goto pageWord_condition;
                    }

                    fd++;
                    goto page_condition;
                }
                int count = 0;
            numeration:
                if (count < 100 && wordOnPage[count] != 0)
                {
                    if (count != 99 && wordOnPage[count + 1] != 0)
                    {
                        Console.Write($"{wordOnPage[count]}, ");
                    }
                    else
                    {
                        Console.Write($"{wordOnPage[count]} ");
                    }
                    count++;
                    goto numeration;
                }
                Console.WriteLine();
                k++;
                goto show;
            }
        }
    }
}



