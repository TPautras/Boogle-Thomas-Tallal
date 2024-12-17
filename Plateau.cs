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
        private char[,] plateauActuel;

        public Plateau(int n)
        {
            this.n = n;
            this.des = new De[n, n];

            // Initialisation du dictionnaire de lettres
            this.DicoLettre = CréationDicoLettres();
            this.plateauActuel = new char[n,n];

            // Création des dés
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    char[] lettres = ChoixLettres(6);
                    this.des[i, j] = new De(lettres);
                    this.des[i, j].Lance(this.r);
                    this.plateauActuel[i, j] = this.des[i, j].FinalLetter;
                }
            }
        }

        public bool surPlateau(string mot)
        {
            if (mot.Length != 0 && mot != null)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (plateauActuel[i, j] == mot[0])
                        {
                            bool[,] casestestees = new bool[n, n];
                            if (ExistenceDuMot(i, j, 0, mot, casestestees))
                            {
                                return true;
                            }
                        }
                    }
                }
            }


            return false;

        }

        private bool ExistenceDuMot(int i, int j, int cpt, string mot, bool[,] casestestees)
        {
            if (cpt == mot.Length)
            {
                return true;
            }

            if (i < 0 || i >= n || j < 0 || j >= n || casestestees[i, j] || plateauActuel[i, j] != mot[cpt])
            {
                return false;
            }

            casestestees[i, j] = true;

            int[] coordonneeslongueur = { -1, 0, 1, 1, 1, 0, -1, -1 };
            int[] coordonneeslargeur = { -1, -1, -1, 0, 1, 1, 1, 0 };

            for (int k = 0; k < coordonneeslargeur.Length; k++)
            {
                if ((i + coordonneeslongueur[k]) > 0 || (i + coordonneeslongueur[k]) < n || (j + coordonneeslargeur[k]) > 0 || (j + coordonneeslargeur[k]) < n)
                {

                    if (ExistenceDuMot(i + coordonneeslongueur[k], j + coordonneeslargeur[k], cpt + 1, mot, casestestees))
                    {
                        return true;
                    }
                }
            }
            casestestees[i, j] = false;
            return false;
        }
        public Dictionary<char, (int, int)> CréationDicoLettres()
        {
            Dictionary<char, (int, int)> dictionnaire = new Dictionary<char, (int, int)>();
            foreach (var ligne in File.ReadLines("../../assets/Lettres.txt"))
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

        public int calculerPoints(string mot)
        {
            int res = 0;
            foreach(char c in mot)
            {
                foreach(var kvp in this.DicoLettre)
                {
                    if(kvp.Key == c)
                    {
                        res += kvp.Value.Item1;
                        Console.WriteLine(kvp.Value.Item2);
                    }
                }
            }
            Console.WriteLine(res);
            return res;
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

                // Recherche de la lettre correspondante
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
            string res = "";
            res += "Le plateau est composé des dés suivants :\n";

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    res += this.plateauActuel[i, j] + " ";
                }
                res += "\n";
            }

            return res;
        }
    }
}
