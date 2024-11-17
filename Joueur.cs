using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Boogle_Thomas_Pautras
{
    internal class Joueur
    {
        private string name;
        private int score;
        private string[] words;

        public Joueur(string name) {
            this.name = name;
        }

        public string Name { get { return name; } }
        public int Score { 
            get { return score; } 
            set { score = value; }
        }
        public string[] Words { get {  return words; } }


        /// <summary>
        /// Teste si le mot passé appartient déjà aux mots
        /// trouvés par le joueur pendant la partie
        /// </summary>
        /// <returns name="res">Bool</returns>
        public bool Contain(string mot)
        {
            bool res = false;
            foreach(string word in this.words)
            {
                if(word.Equals(mot)) res = true;
            }
            return res;
        }

        /// <summary>
        /// ajoute le mot dans la liste des mots déjà trouvés par
        /// le joueur au cours de la partie en modifiant le nombre d’occurrences si nécessaire
        /// </summary>
        /// <param name="mot"></param>
        public void Add_Mot(string mot)
        {
            string[] tmp = new string[this.words.Length + 1];
            for(int i= 0; i < this.words.Length; i++)
            {
                tmp[i] = this.words[i];
            }
            tmp[words.Length] = mot;
            this.words = tmp;
        }

        /// <summary>
        /// Retourne une chaîne de caractères qui décrit un joueur.
        /// </summary>
        /// <returns name="res">Chaîne de caractère décrivant un joueur</returns>
        public string toString()
        {
            string res = "";
            res += this.name + " a " + this.score + " points.\n";
            res += "Il a trouvé les mots suivants : \n";
            foreach(string word in this.words)
            {
                res += word + "\n";
            }

            return res;
        }
    }
}
