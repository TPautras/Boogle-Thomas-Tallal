using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boogle_Thomas_Pautras
{
    public class Dictionnaire
    {
        private List<string> dict;
        private int length;
        private string lang = "Fançais";

        public Dictionnaire(List<string> dict, int length, string lang)
        {
            this.dict = dict;
            this.length = length;
            this.lang = lang;
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

            foreach (string str in this.Dict)
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
