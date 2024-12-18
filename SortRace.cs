using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Boogle_Thomas_Pautras
{
    public class SortRace
    {
        private static List<string> dict;

        static SortRace()
        {
            dict = new List<string>();
            var path = "../../../Boogle-Thomas-Tallal/assets/MotsPossiblesFR.txt";
            IEnumerable<string> lines = null;
            try
            {
                lines = File.ReadLines(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            foreach (var line in lines)
            {
                var words = line.Split(' ');
                if (words.Length > 0)
                {
                    for (int i = 0; i < words.Length; i++)
                    {
                        dict.Add(words[i]);
                    }
                }
            }
            dict.Sort();
        }

        public static bool RechDichoRecursif(int start, int end, string wordToFind)
        {
            int a = (start + end) / 2;
            string find = dict[a];
            if (find != wordToFind)
            {
                if (end != start)
                {
                    if (start < end)
                    {
                        if (wordToFind.CompareTo(find) == -1)
                        {
                            return RechDichoRecursif(start, a - 1, wordToFind);
                        }
                        else
                        {
                            return RechDichoRecursif(a + 1, end, wordToFind);
                        }
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
            else
            {
                return true;
            }
        }

        public static bool Lineaire(string target)
        {
            for (int i = 0; i < dict.Count; i++)
            {
                if (dict[i] == target)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool DichoSimple(string target)
        {
            int left = 0;
            int right = dict.Count - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                int comparison = target.CompareTo(dict[mid]);
                if (comparison == 0)
                {
                    return true;
                }
                else if (comparison < 0)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            return false;
        }


        private static void PrintProgressBar(int current, int total, int width)
        {
            double ratio = (double)current / total;
            int progress = (int)(ratio * width);
            Console.Write("[");
            for (int i = 0; i < width; i++)
            {
                if (i < progress) Console.Write("#");
                else Console.Write(" ");
            }
            Console.Write("] " + (ratio * 100).ToString("F2") + "%");
        }

        public static void Race(int iterations = 50)
        {
            var rand = new Random();
            var stopwatch = new Stopwatch();
            double linearTotal = 0;
            double binaryIterTotal = 0;
            double binaryRecTotal = 0;
            Console.Clear();
            Console.CursorVisible = false;
            int barWidth = 30;

            for (int i = 1; i <= iterations; i++)
            {
                var target = dict[rand.Next(dict.Count)];

                stopwatch.Restart();
                Lineaire(target);
                stopwatch.Stop();
                linearTotal += stopwatch.Elapsed.TotalMilliseconds;

                stopwatch.Restart();
                DichoSimple(target);
                stopwatch.Stop();
                binaryIterTotal += stopwatch.Elapsed.TotalMilliseconds;

                stopwatch.Restart();
                RechDichoRecursif(0, dict.Count - 1, target);
                stopwatch.Stop();
                binaryRecTotal += stopwatch.Elapsed.TotalMilliseconds;


                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Comparaison des algorithmes de recherche:");
                Console.WriteLine("Lineaire :        " + (linearTotal / i).ToString("F4") + " ms");
                Console.WriteLine("Dichotomique simple :  " + (binaryIterTotal / i).ToString("F4") + " ms");
                Console.WriteLine("Dichotomique récursive :   " + (binaryRecTotal / i).ToString("F4") + " ms");
                Console.WriteLine();
                Console.Write("Progression:   ");
                PrintProgressBar(i, iterations, barWidth);
                Console.WriteLine();
            }

            Console.CursorVisible = true;
        }
    }
}
