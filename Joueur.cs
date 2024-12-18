using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Boogle_Thomas_Pautras
{
    public class Joueur
    {
        #region Attributs, propriétés et Constructeur
        private string name;
        private int score;
        private string[] words = new string[0];
        private bool isAi = false;
        private int difficulte = 0;

        /// <summary>
        /// Constructeur du Joueur, celui-ci
        /// n'a besoin que d'un nom pour être créé
        /// tous les autres attributs seront initialisés
        /// plus tard dans l'exécution du programme
        /// </summary>
        /// <param name="name"> nom du joueur</param>
        public Joueur(string name) {
            this.name = name;
        }

        /// <summary>
        /// Constructeur du Joueur pour l'IA, celui-ci
        /// a besoind'un nom, d'un bool qui sera true pour être créé
        /// et une difficulté,tous les autres
        /// attributs seront initialisés
        /// plus tard dans l'exécution du programme
        /// </summary>
        /// <param name="name"> nom du joueur</param>
        public Joueur(string name, bool isAi, int difficulte)
        {
            this.name = name;
            this.isAi = isAi;
            this.difficulte = difficulte;
        }

        /// <summary>
        /// Propriétés pour les attributs de la
        /// classe
        /// </summary>
        public string Name { get { return name; } }
        public bool IsAi { get { return isAi; } }
        public int Difficulte { get { return difficulte; } }
        public int Score { 
            get { return score; } 
            set { score = value; }
        }
        public string[] Words { get {  return words; } }
        #endregion

        #region Méthodes de manipulation de la liste de mots
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
        #endregion

        #region toString()
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
        #endregion
    }
}
