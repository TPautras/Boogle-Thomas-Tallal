using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boogle_Thomas_Pautras
{
    internal class De
    {
        private char[] letters = new char[6];
        private char finalLetter;

        public De(char[] letters)
        {
            this.letters = letters;
        }

        public char finalLetter
        {
            get { return finalLetter; }
        }

        public char[] Letters
        {
            get { return letters; }
            set { letters = value; }
        }

        public void Lance(Random r)
        {
            finalLetter = letters[r.Next(6)];
        }

        public string toString()
        {
            string message = "Les différentes faces de ce dé sont :";

            foreach (char letter in letters)
            {
                message += Convert.ToString(x) + " ; ";
            }

            return message + "\nLa face supérieure de ce dé est : " + Convert.ToString(finalLetter);
        }
    }
}
