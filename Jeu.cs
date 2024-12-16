using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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
            Console.WriteLine("         Bienvenue dans le jeu de Boogle !          ");
            Console.WriteLine("======================================================\n");
        }

        #endregion

        public void LancerPartie()
        {
            Console.WriteLine("La partie commence ! Bonne chance à tous les joueurs.");
            for (int tour = 1; tour <= nbTours; tour++)
            {
                Console.WriteLine($"Début du tour {tour}/{nbTours}.");
                foreach (var joueur in Joueurs)
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

                        if (mot.Length >= 2 && DictionnaireActuel.RechDichoRecursif(0, DictionnaireActuel.Dict.Count - 1, mot) && !joueur.Contain(mot))
                        {
                            joueur.Add_Mot(mot);
                            joueur.Score += mot.Length;
                            Console.WriteLine($"Mot accepté : {mot} (+{mot.Length} points)");
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

            Console.WriteLine("La partie est terminée. Voici les scores finaux :");
            foreach (var joueur in Joueurs)
            {
                Console.WriteLine($"{joueur.Name} : {joueur.Score} points");
            }
        }
    }
}
