using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boogle_Thomas_Pautras
{
    public class Plateau
    {
        private int n;
        private De[,] des;
        private Random r = new Random();
        private Dictionary<char, (int, int)> DicoLettre;

        public Plateau(int n)
        {
            this.n = n;
            this.des = new De[n, n];

            // Initialisation du dictionnaire de lettres
            this.DicoLettre = CréationDicoLettres();

            // Création des dés
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    char[] lettres = ChoixLettres(6);
                    this.des[i, j] = new De(lettres);
                }
            }
        }

        //public bool Test_Plateaua(string mot) /// TODO : fix
        //{
        //    if (Dictionaire.RechDichoRecursif(mot) == false|| mot.Length<=2)
        //    {
        //        return false;
        //    }
        //    return true;

        //}
        public Dictionary<char, (int, int)> CréationDicoLettres()
        {
            Dictionary<char, (int, int)> dictionnaire = new Dictionary<char, (int, int)>();
            foreach (var ligne in File.ReadLines("../../Lettres.txt"))
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

        public char[] ChoixLettres(int nombreLettres)
        {
            var lettresChoisies = new char[nombreLettres];
            var optionsAvecCumul = new List<(string Option, double Cumul)>();

            for (int i = 0; i < nombreLettres; i++)
            {
                double cumulativeSom = 0;
                optionsAvecCumul.Clear();

                // On utilise le dictionnaire DicoLettre pour générer des lettres pondérées.
                foreach (var kvp in this.DicoLettre)
                {
                    // On suppose que kvp.Value.Item2 représente le poids de la lettre.
                    cumulativeSom += kvp.Value.Item2;
                    optionsAvecCumul.Add((kvp.Key.ToString(), cumulativeSom));
                }

                double randomValue = r.NextDouble() * cumulativeSom;

                // Recherche de la lettre correspondante dans la distribution cumulative
                foreach (var (Option, Cumul) in optionsAvecCumul)
                {
                    if (randomValue <= Cumul)
                    {
                        lettresChoisies[i] = Option[0];
                        break;
                    }
                }
            }

            return lettresChoisies;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Le plateau est composé des dés suivants :");

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sb.Append(this.des[i, j].ToString()).Append(" ");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
