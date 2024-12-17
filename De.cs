using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boogle_Thomas_Pautras
{
    public class De
    {
        #region Attributs constructeur et propriétés
        private char[] letters = new char[6];
        private char finalLetter;

        /// <summary>
        /// Constructeur non natural du Dé, prend en argument
        /// un tableau de char représentant les dés
        /// l'un de ces char sera choisi pour être la face
        /// "visible" du dé
        /// </summary>
        /// <param name="letters">Lettres des faces du dé</param>
        public De(char[] letters)
        {
            this.letters = letters;
        }

        /// <summary>
        /// Propriétés du dé
        /// </summary>
        public char FinalLetter
        {
            get { return finalLetter; }
        }

        public char[] Letters
        {
            get { return letters; }
            set { letters = value; }
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Permet de déterminer la face visible 
        /// du dé. Utilise un random passé en paramètre
        /// afin de reproduire plusieurs fois le lancer
        /// d'un même dé au besoin.
        /// De plus cela évite de faire "dormir" le programme
        /// comme le nombre aléatoire est généré à l'extérieur de la fonction
        /// </summary>
        /// <param name="r">Nombre aléatoire</param>
        public void Lance(Random r)
        {
            finalLetter = letters[r.Next(6)];
        }

        /// <summary>
        /// Retourne une chaîne contenant les différentes
        /// faces du dé, ainsi que celle qui est "visible"
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            string message = "Les différentes faces de ce dé sont :";

            foreach (char letter in letters)
            {
                message += Convert.ToString(letter) + " ; ";
            }

            return message + "\nLa face supérieure de ce dé est : " + Convert.ToString(finalLetter);
        }
        #endregion
    }
}
