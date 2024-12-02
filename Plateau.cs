using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boogle_Thomas_Pautras
{
    internal class Plateau
    {
        private int n;
        private De[,] des;
        private Random r = new Random();

        public Plateau(int n) //test
        {
            this.n = n;
            this.des = new De[n,n];
            for(int i=0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    this.des[i,j] = new De();
                }
            }   
        }

        public bool Test_Plateaua(string mot)
        {
            if (Dictionaire.RechDichoRecursif(mot) == false|| mot.Length<=2)
            {
                return false;
            }
            while()

        }
        public Dictionary CreeDico()
        {
            string cheminFichier = "Lettre.txt";
            Dictionary<char, (int, int)> dictionnaire = new Dictionary<char, (int, int)>();
            foreach (var ligne in File.ReadLines(cheminFichier))
            {
                var parties = ligne.Split(';');
                if (parties.Length == 3)
                {
                    char lettre = parties[0][0];
                    int valeur1 = int.Parse(parties[1]);
                    int valeur2 = int.Parse(parties[2]);

                    dictionnaire[lettre] = (valeur1, valeur2);
                }
            }
            return dictionnaire;

        }
    public string toString()
        {
            string message = "Le plateau est composé des dés suivants :\n";

            for (int i = 0; i < 16; i++)
            {
                message += des[i].toString() + "\n";
            }

            return message;
        }
    }
}
