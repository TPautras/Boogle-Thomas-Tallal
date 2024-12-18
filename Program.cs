using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Boogle_Thomas_Pautras
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] opts = { "Faire la course", "Jouer au BOOGLE !!" , "Quitter"};
            int playerChoice = Jeu.MenuSelect(Jeu.AfficherBanniere(), opts);
            while (playerChoice != 2)
            {
                switch (playerChoice)
                {
                    case 0:
                        SortRace.Race();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer ...");
                        Console.ReadKey();
                        break;
                    case 1:
                        Console.Clear();
                        Console.WriteLine(Jeu.AfficherBanniere());

                        List<Joueur> joueurs = Jeu.selectJoueurs();

                        int nbTours = Jeu.selectTours();

                        string lang = Jeu.selectLang();

                        var dictionnaire = new Dictionnaire(lang);

                        var plateau = new Plateau(4);

                        var jeu = new Jeu(joueurs, plateau, dictionnaire, nbTours);
                        Console.Clear();

                        jeu.LancerBoogle(jeu);

                        Console.WriteLine("Merci d'avoir joué à Boogle !");
                        break;
                    default: break;
                }
                playerChoice = Jeu.MenuSelect(Jeu.AfficherBanniere(), opts);
            }
        }
    }
}
