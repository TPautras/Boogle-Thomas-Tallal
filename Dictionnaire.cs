using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Boogle_Thomas_Pautras
{
    public class Dictionnaire
    {
        private List<string> dict;
        private int length = 0;
        private string lang = "FR";

        public Dictionnaire(string lang)
        {
            this.lang = lang;
            this.dict = new List<string>();
            if (this.lang == "FR")
            {
                var lines = File.ReadLines("../../assets/MotsPossibles"+this.lang+".txt");
                foreach(var line in  lines)
                {
                    var words = line.Split(' ');
                    if(words.Length > 0)
                    {
                        for(int i = 0; i < words.Length; i++)
                        {
                            dict.Add(words[i]);
                        }
                    }
                }
                
            };
            this.length = dict.Count;
        }

        public string Lang
        {
            get { return lang; }
        }

        public int Length
        {
            get { return length; }
        }

        public List<string> Dict
        {
            get { return dict; }
        }

        public string toString()
        {
            string res = "Ce dictionnaire " + this.lang + " contient " + this.length + " mots : "; 

            foreach (string str in this.dict)
            {
                res += str + "\n";
            }
            return res;
        }

        public bool RechDichoRecursif(int start, int end, string wordToFind)
        {
            int a = (start + end) / 2;


            string find = this.dict[a];

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
    }
}
