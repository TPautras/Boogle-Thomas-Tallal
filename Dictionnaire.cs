using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics.Eventing.Reader;

namespace Boogle_Thomas_Pautras
{
    public class Dictionnaire
    {
        #region Attributs, constructeurs et propriétés
        private List<string> dict;
        private int length = 0;
        private string lang = "FR";
        private Dictionary<int, List<string>> dictionarySorted = new Dictionary<int, List<string>>();

        /// <summary>
        /// Constructeur d'un dictionnaire, se crée en fontion
        /// de la langue passée en entrée qui va chercher un ficher 
        /// dans assets contenant une liste de mots.
        /// Puis automatiquement trier le dictionnaire et rassembler
        /// par taille les mots.
        /// </summary>
        /// <param name="lang">Langue du dictionnaire</param>
        public Dictionnaire(string lang)
        {
            this.lang = lang;
            this.dict = new List<string>();
            string path = "../../../Boogle-Thomas-Tallal/assets/MotsPossibles" + this.lang + ".txt";
            var lines = File.ReadLines(path);
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
            this.length = dict.Count;
            dict.Sort(); //Fonction par défaut sur les Listes dans c#, complexité par défaut de 
                         //O(nlogn) mais est de O(n²) dans le pire des cas
            foreach (string mot in dict)
            {
                int longueur = mot.Length;
                if (!dictionarySorted.ContainsKey(longueur))
                {
                    dictionarySorted[longueur] = new List<string>();
                }
                dictionarySorted[longueur].Add(mot);
            }
            foreach (var key in dictionarySorted.Keys)
            {
                dictionarySorted[key].Sort(); //Applique de nouveau la fonction sur chacune des sous listes
            }
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
        #endregion

        #region méthodes toString et RechDicho
        /// <summary>
        /// Fonction retournant une string contenant
        /// les caractéristiques du dictionnaire et
        /// les mots qui le composent
        /// </summary>
        /// <returns name="res" type="string"></returns>
        public string toString()
        {
            string res = "Ce dictionnaire " + this.lang + " contient " + this.length + " mots : "; 

            foreach (string str in this.dict)
            {
                res += str + "\n";
            }
            return res;
        }

        /// <summary>
        /// Fonction de recherche dichotomique dans le dictionnaire
        /// retourne un booléen représentant la présence ou non du
        /// mot. 
        /// </summary>
        /// <param name="start">borne inférieure de la recherche</param>
        /// <param name="end">borne supérieure de la recherche</param>
        /// <param name="wordToFind">Mot dont on doit déterminer la présence</param>
        /// <returns type="bool">true si présent dans le dictionnaire, faux sinon</returns>
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
        #endregion
    }
}
