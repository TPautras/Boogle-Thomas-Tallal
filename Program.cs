using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Boogle_Thomas_Pautras
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Entrez le nombre de joueurs : ");
            int nbJoueurs = int.Parse(Console.ReadLine());
            var joueurs = new List<Joueur>();
            for (int i = 1; i <= nbJoueurs; i++)
            {
                Console.Write($"Nom du joueur {i} : ");
                string nom = Console.ReadLine();
                joueurs.Add(new Joueur(nom));
            }

            Console.WriteLine("Entrez le nombre de tours : ");
            int nbTours = int.Parse(Console.ReadLine());

            var dictionnaire = new Dictionnaire(
                "FR"
            );

            var plateau = new Plateau(4);

            var jeu = new Jeu(joueurs, plateau, dictionnaire, nbTours);

            Jeu.AfficherBanniere();
            jeu.AfficherMenu();

            string choix = Console.ReadLine();
            while (choix != "3")
            {
                switch (choix)
                {
                    case "1":
                        jeu.LancerPartie();
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

            Console.WriteLine("Merci d'avoir joué à Boogle !");
        }
    }
}
