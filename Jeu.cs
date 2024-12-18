using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;
using System.Threading;

namespace Boogle_Thomas_Pautras
{
    public class Jeu
    {
        #region Initialisation
        /// <summary>
        /// Attributs et constructeurs de la classe
        /// Jeu, comprend toutes les autres
        /// classes sauf De et NuageDeMots.
        /// </summary>
        private List<Joueur> Joueurs;
        private Plateau PlateauActuel;
        private Dictionnaire DictionnaireActuel;
        private bool gameIsActive = true;
        private int nbTours;

        public Jeu(List<Joueur> joueurs, Plateau plateau, Dictionnaire dictionnaire, int nbTours)
        {
            this.Joueurs = joueurs;
            this.PlateauActuel = plateau;
            this.DictionnaireActuel = dictionnaire;
            this.nbTours = nbTours;
        }
        #endregion

        #region Méthodes initialisation
        /// <summary>
        /// Méthode permettant de sélectionner
        /// le nombre de joueur voulu par
        /// l'utilisateur. Puis retourne une
        /// liste contenant toutes les instances 
        /// nécessaires.
        /// Permet également de choisir si les joueurs sont des 
        /// IA ou des "vrais" joueurs
        /// </summary>
        /// <returns type= List<Joueur>>Liste des utilisateurs</returns>
        public static List<Joueur> selectJoueurs()
        {
            Console.WriteLine("Entrez le nombre de joueurs : ");
            int nbJoueurs = int.Parse(Console.ReadLine());
            var joueurs = new List<Joueur>();
            for (int i = 1; i <= nbJoueurs; i++)
            {
                string[] options = { "Oui", "Non" };
                int choixIA = MenuSelect("Voulez vous que le joueur soit une IA ? (Appuyez sur les flèches puis sur entrée pour valider)", options);
                Console.Write($"Nom du joueur {i} : ");
                string nom = Console.ReadLine();
                
                if (choixIA == 0)
                {
                    string[] optsDifficulte = { "Facile", "Medium","Difficile", "IMPOSSIBLE!!!" };
                    int choixDiff = MenuSelect("Quelle difficulté voulez vous ?", optsDifficulte);
                    joueurs.Add(new Joueur(nom,true, choixDiff));
                }
                else
                {
                    joueurs.Add(new Joueur(nom));

                }
            }
            return joueurs;
        }

        /// <summary>
        /// Méthode permettant
        /// simplement à l'utilisateur 
        /// d'entrer le nombre de tours qu'il 
        /// désire jouer.
        /// </summary>
        /// <returns type=int>Le nombre de tours entré</returns>
        public static int selectTours()
        {
            int nbTours = 0;
            while(nbTours <= 0 || nbTours == null)
            {
                Console.WriteLine("Entrez le nombre de tours : ");
                nbTours = int.Parse(Console.ReadLine());
            }
            return nbTours;
        }

        /// <summary>
        /// Méthode permettant
        /// simplement à l'utilisateur 
        /// de sélectionner la langue
        /// avec laquelle il 
        /// désire jouer.
        /// </summary>
        /// <returns type=string>Les deux premières lettres de la langue</returns>
        public static string selectLang() 
        {
            Console.WriteLine("Entrez la langue que vous voulez : (FR ou EN)");
            string[] opts = { "Français", "Anglais" };
            string lang = "";
            switch(MenuSelect("Entrez la langue que vous voulez :", opts))
            {
                case 0 : lang = "FR"; break;
                case 1: lang = "EN"; break;
                default: lang = "FR"; break;
            }
            return lang;
        }
        #endregion

        #region Affichages

        public string AfficherMenu()
        {
            string res = "\n" +
                         "Choisissez une option :  \n";
            return res;
                         
        }

        public void AfficherRegles()
        {
            Console.WriteLine("Règles du jeu :");
            Console.WriteLine("1. Chaque joueur a une minute pour trouver des mots.");
            Console.WriteLine("2. Les mots doivent être formés avec des lettres adjacentes.");
            Console.WriteLine("3. Les mots doivent avoir au moins deux lettres.");
            Console.WriteLine("4. Les mots trouvés doivent appartenir au dictionnaire.");
            Console.WriteLine("Bonne chance et amusez-vous bien !\n");
        }

        public void AfficherTour(Joueur joueur)
        {
            Console.WriteLine($"C'est le tour de {joueur.Name} !");
            Console.WriteLine("============================");
            Console.WriteLine("Voici le plateau :");
            Console.WriteLine(PlateauActuel.ToString());
            Console.WriteLine("============================");
        }

        public void AfficherScores()
        {
            Console.WriteLine("Scores finaux :");
            foreach (var joueur in Joueurs)
            {
                Console.WriteLine($"{joueur.Name} : {joueur.Score} points");
            }
        }

        public string AfficherBanniere()
        {
            string res = "======================================================\n" +
                         "888888b.                              888             \n" +
                         "888  \"88b                             888              \n" +
                         "888  .88P                             888              \n" +
                         "8888888K.   .d88b.   .d88b.   .d88b.  888  .d88b.     \n" +
                         "888  \"Y88b d88\"\"88b d88\"\"88b d88P\"88b 888 d8P Y8b    \n" +
                         "888    888 888  888 888  888 888  888 888 88888888  \n" +
                         "888   d88P Y88..88P Y88..88P Y88b 888 888 Y8b.       \n" +
                         "8888888P\"   \"Y88P\"   \"Y88P\"   \"Y88888 888  \"Y8888   \n" +
                         "                                  888               \n" +
                         "                             Y8b d88P               \n" +
                         "                              \"Y88P\"                \n" +
                         "======================================================\n" +
                         "         Bienvenue dans le jeu du Boogle !          \n" +
                         "======================================================\n";

            return res;
        }

        public static int MenuSelect(string prompt, string[] options)
        {
            ConsoleKey keyPressed;
            int res = 0;
            do
            {
                Clear();
                DisplayOptions(prompt, options, res);
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                if(keyPressed == ConsoleKey.UpArrow)
                {
                    res--;
                    if(res == -1)
                    {
                        res = options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    res++;
                    if (res == options.Length)
                    {
                        res = 0;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);
            return res;
        }

        public static void DisplayOptions(string prompt, string[] options, int selectedOption)
        {
            Console.WriteLine(prompt);
            for(int i = 0; i < options.Length; i++)
            {
                if(i == selectedOption)
                {
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                    Console.WriteLine($">> {options[i]}");
                }
                else
                {
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine($"   {options[i]}");
                }
            }
            ResetColor();
        }

        #endregion

        #region Gestion de la partie
        /// <summary>
        /// Méthode gérant le Boogle dans son intégralité.
        /// Affiche les différents menus,
        /// gère la victoire la défaite et la 
        /// sortie du jeu.
        /// </summary>
        /// <param name="jeu">Instance de la partie en cours pour pouvoir utiliser des méthodes d'instance</param>
        public void LancerBoogle(Jeu jeu)
        {
            string completeMenu = this.AfficherBanniere() + this.AfficherMenu();
            string[] menuOptions = { "Lancer une partie", "Afficher les règles", "Quitter" };
            int choix = MenuSelect(completeMenu,menuOptions);
            while (choix != 2)
            {
                switch (choix)
                {
                    case 0:
                        jeu.LancerPartie();
                        break;
                    case 1:
                        jeu.AfficherRegles();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer ...");
                        Console.ReadKey();
                        break;
                    case 2: break;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                        break;
                }

                choix = MenuSelect(completeMenu, menuOptions);
            }
        }

        /// <summary>
        /// Méthode gérant une partie de jeu
        /// avec les différents tours et 
        /// les différentes entrée des 
        /// utilisateurs ou des IA
        /// </summary>
        public void LancerPartie()
        {
            Console.WriteLine("La partie commence ! Bonne chance à tous les joueurs.");
            for (int tour = 1; tour <= nbTours; tour++)
            {
                Console.WriteLine($"Début du tour {tour}/{nbTours}.");
                foreach (var joueur in Joueurs)
                {
                    if (joueur.IsAi)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"C'est le tour de {joueur.Name} !");
                        Console.WriteLine();

                        Console.WriteLine(this.PlateauActuel.ToString());

                        List<string> motsIA = PlateauActuel.AIList(joueur.Difficulte, DictionnaireActuel.DictionarySorted, joueur);

                        string mots = "";
                        foreach (string mot in motsIA)
                        {
                            joueur.Add_Mot(mot);
                            int point = PlateauActuel.calculerPoints(mot) + mot.Length;
                            joueur.Score += point;
                            Console.WriteLine($"Mot accepté : {mot} (+{point} points)");
                            Thread.Sleep(1500);

                            mots += mot + " ";
                        }
                        Console.WriteLine("L'IA a trouvé les mots suivants : "+mots);                        
                        Console.WriteLine("Pour un total de : " + joueur.Score);
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer ...");
                        Console.ReadKey();

                    }
                    else
                    {
                        Console.WriteLine($"C'est le tour de {joueur.Name} !");
                        Stopwatch chrono = Stopwatch.StartNew();

                        while (chrono.Elapsed.TotalMinutes < 3)
                        {
                            TimeSpan restant = TimeSpan.FromMinutes(3) - chrono.Elapsed;
                            Console.WriteLine(this.PlateauActuel.ToString());
                            Console.WriteLine();
                            Console.WriteLine($"Temps restant : {restant.Minutes:D2}:{restant.Seconds:D2}");
                            Console.WriteLine("Entrez un mot ou appuyez sur Entrée pour terminer : ");
                            string mot = Console.ReadLine();
                            mot = mot.ToUpper();

                            if (string.IsNullOrEmpty(mot)) break;

                            if (mot.Length >= 2 && DictionnaireActuel.RechDichoRecursif(0, DictionnaireActuel.Dict.Count - 1, mot) && !joueur.Contain(mot) && PlateauActuel.surPlateau(mot))
                            {
                                joueur.Add_Mot(mot);
                                int point = PlateauActuel.calculerPoints(mot) + mot.Length;
                                joueur.Score += point;
                                Console.WriteLine($"Mot accepté : {mot} (+{point} points)");
                            }
                            else
                            {
                                Console.WriteLine("Mot invalide ou déjà trouvé.");
                            }
                        }

                        chrono.Stop();
                        Console.WriteLine($"Temps écoulé pour {joueur.Name}.");
                    }
                }
            }

            Console.Clear();
            Console.WriteLine("La partie est terminée. Voici les scores finaux :");
            Dictionary<string, int> wordsNuage = new Dictionary<string, int>();
            NuageMots nuage = new NuageMots(wordsNuage);
            foreach (var joueur in Joueurs)
            {
                Console.WriteLine($"{joueur.Name} : {joueur.Score} points");
                foreach (var word in joueur.Words)
                {
                    if (!wordsNuage.ContainsKey(word))
                    {
                        wordsNuage[word] = 1;
                    }
                    else
                    {
                        wordsNuage[word]++;
                    }
                }
            }

            nuage.GenererNuage();
            Console.WriteLine();
            Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer ...");
            Console.ReadKey();
        }
        #endregion
    }
}
