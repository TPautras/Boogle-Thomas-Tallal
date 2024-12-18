using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;

namespace Boogle_Thomas_Pautras
{
    public class Jeu
    {
        #region Initialisation
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
        public static List<Joueur> selectJoueurs()
        {
            Console.WriteLine("Entrez le nombre de joueurs : ");
            int nbJoueurs = int.Parse(Console.ReadLine());
            var joueurs = new List<Joueur>();
            for (int i = 1; i <= nbJoueurs; i++)
            {
                string[] options = { "Oui", "Non" };
                int choixIA = MenuSelect("Voulez vous que le joueur soit une IA ?", options);
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

        public static int selectTours()
        {
            Console.WriteLine("Entrez le nombre de tours : ");
            int nbTours = int.Parse(Console.ReadLine());
            return nbTours;
        }
        public static string selectLang() 
        {
            Console.WriteLine("Entrez la langue que vous voulez : (FR ou EN)");
            string[] opts = { "Français", "Englais" };
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

        public void AfficherMenu()
        {
            Console.WriteLine("Bienvenue dans le jeu de Boogle !");
            Console.WriteLine("============================");
            Console.WriteLine("1. Commencer une nouvelle partie");
            Console.WriteLine("2. Voir les règles du jeu");
            Console.WriteLine("3. Quitter");
            Console.WriteLine("============================");
            Console.Write("Choisissez une option : ");
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

        public static void AfficherBanniere()
        {
            Console.WriteLine("======================================================");
            Console.WriteLine("888888b.                              888             ");
            Console.WriteLine("888  \"88b                             888              ");
            Console.WriteLine("888  .88P                             888              ");
            Console.WriteLine("8888888K.   .d88b.   .d88b.   .d88b.  888  .d88b.     ");
            Console.WriteLine("888  \"Y88b d88\"\"88b d88\"\"88b d88P\"88b 888 d8P Y8b    ");
            Console.WriteLine("888    888 888  888 888  888 888  888 888 88888888   ");
            Console.WriteLine("888   d88P Y88..88P Y88..88P Y88b 888 888 Y8b.       ");
            Console.WriteLine("8888888P\"   \"Y88P\"   \"Y88P\"   \"Y88888 888  \"Y8888   ");
            Console.WriteLine("                                  888               ");
            Console.WriteLine("                             Y8b d88P               ");
            Console.WriteLine("                              \"Y88P\"                ");
            Console.WriteLine("======================================================");
            Console.WriteLine("         Bienvenue dans le jeu du Boogle !          ");
            Console.WriteLine("======================================================\n");
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
        public void LancerPartie(Jeu jeu)
        {
            string choix = Console.ReadLine();
            while (choix != "3")
            {
                switch (choix)
                {
                    case "1":
                        jeu.LancerTour();
                        break;
                    case "2":
                        jeu.AfficherRegles();
                        break;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                        break;
                }

                jeu.AfficherMenu();
                choix = Console.ReadLine();
            }
        }
        public void LancerTour()
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

                        List<string> motsIA = PlateauActuel.AIList(joueur.Difficulte, DictionnaireActuel.DictionarySorted);

                        string mots = "";
                        foreach (string mot in motsIA)
                        {
                            joueur.Add_Mot(mot);
                            int point = PlateauActuel.calculerPoints(mot) + mot.Length;
                            joueur.Score += point;

                            mots += mot + " ";
                        }
                        Console.WriteLine("L'IA a trouvé les mots suivants : "+mots);                        
                        Console.WriteLine("Pour un total de : " + joueur.Score);
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
        }
        #endregion
    }
}
