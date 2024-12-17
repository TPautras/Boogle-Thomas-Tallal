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
                "EN"
            );

            var plateau = new Plateau(4);

            var jeu = new Jeu(joueurs, plateau, dictionnaire, nbTours);
            Console.Clear();

            Jeu.AfficherBanniere();
            jeu.AfficherMenu();

            jeu.LancerPartie(jeu);

            Console.WriteLine("Merci d'avoir joué à Boogle !");
        }
    }
}
