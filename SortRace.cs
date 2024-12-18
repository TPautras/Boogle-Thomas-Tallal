using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Boogle_Thomas_Pautras
{
    public class SortRace
    {
        public static void Race()
        {
            List<string> dict = new List<string>();
            string path = "../../../Boogle-Thomas-Tallal/assets/MotsPossiblesFR.txt";
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
            Console.WriteLine("Racing");
        }
    }
}
