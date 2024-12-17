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
        #region Attributs et constructeur
        private int n;
        private De[,] des;
        private Random r = new Random();
        private Dictionary<char, (int, int)> DicoLettre;
        private char[,] plateauActuel; //Ne stocke que les char et non des instances de De

        /// <summary>
        /// Constructeur non naturel du plateau,
        /// qui prend un entier n représentant la taille.
        /// Celui ci vaut ici toujours 4 mais on pourrait
        /// faire varier la taille du plateau si nécessaire
        /// </summary>
        /// <param name="n">Taille du plateau</param>
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
                    this.des[i, j].Lance(this.r); //Permet "d'initialiser" la face visible du dé de ce tableau
                    this.plateauActuel[i, j] = this.des[i, j].FinalLetter; //Update l'autre tableau ne contenant que les char et non les instances de Dé
                }
            }
        }
        #endregion

        #region Méthodes de vérification
        /// <summary>
        /// Méthode de vérification si le mot 
        /// est bien présent sur le plateau.
        /// Est couplée à existence du mot
        /// Appelle existence dès qu'elle trouve une lettre 
        /// correspondant à la première lettre.
        /// C'est la fonction qui est appelée à l'extérieure
        /// puisqu'elle retourne un booléen représentant la présence
        /// ou non du mot sur le plateau
        /// </summary>
        /// <param name="mot"></param>
        /// <returns type=bool></returns>
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

        /// <summary>
        /// En partant d'une "case" donnée, cherche
        /// semi récursivement autour afin de savoir
        /// si il est possible de faire le mot avec des 
        /// cases adjacentes
        /// </summary>
        /// <param name="i">Coordonnée en abscisses</param>
        /// <param name="j">Coordonnée en ordonnée</param>
        /// <param name="cpt">Compteur de lettres trouvées sur le plateau</param>
        /// <param name="mot">Mot à trouver</param>
        /// <param name="casestestees">Coordonnées des cases déjà testées</param>
        /// <returns type=bool>Booleen représentant la possibilité de faire le mot avec les cases du tableau</returns>
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

        /// <summary>
        /// Création d'un dictionnaire à
        /// partir du fichier Lettres.txt
        /// Celui-ci contient le poids de chaque lettre,
        /// ainsi que le nombre de points qui lui est associé
        /// </summary>
        /// <returns type=Dictionary<char, (int, int)>>
        /// Retourne un dictionnaire avec un char en 
        /// entrée, à savoir la lettre.
        /// Et en valeur un doubleton contenant en première valeur
        /// le nombre de point et en deuxième le poids de la lettre
        /// </returns>
        public Dictionary<char, (int, int)> CréationDicoLettres()
        {
            Dictionary<char, (int, int)> dictionnaire = new Dictionary<char, (int, int)>();
            foreach (var ligne in File.ReadLines("../../../Boogle-Thomas-Tallal/assets/Lettres.txt"))
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
        #endregion

        #region Calcul de points, Choix lettres, toString
        /// <summary>
        /// Méthode calculant le nombre de points
        /// que vaut un mot passé en entrée à l'aide 
        /// du dictionnaire recensant le nombre
        /// de points définis par lettre
        /// </summary>
        /// <param name="mot">Mot dont on veut vérifier la valeur en points</param>
        /// <returns></returns>
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
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// Fonction générant des tableaux de char
        /// en prenant en compte le poids des lettres
        /// tels que définis par Lettres.txt.
        /// La fonction calcule le poids total puis
        /// à l'aide d'un nombre aléatoire, choisit un lettre
        /// Ce processus est itérée un nombre de fois passé en 
        /// paramètre
        /// </summary>
        /// <param name="nombreLettres">Nombre d'itérations de la fonction</param>
        /// <returns type=char[]>Tableau de caractères choisis aléatoirement en fonction des poids définis dans Lettres.txt</returns>
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

        /// <summary>
        /// Fonction retournant une string contenant
        /// les informations définissant le plateau.
        /// A savoir les différents caractères qui 
        /// sont les faces visibles des différents
        /// dés. 
        /// On utilise override pour montrer que l'on
        /// comprend à quoi il sert, en l'ocurrence passer
        /// par dessus la méthode ToString de base pour à
        /// la place du type, retourner une chaine descriptive
        /// </summary>
        /// <returns type=string>String descriptive du plateau</returns>
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
        #endregion
    }
}
