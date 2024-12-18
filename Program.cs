using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Boogle_Thomas_Pautras
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Joueur> joueurs = Jeu.selectJoueurs();

            int nbTours = Jeu.selectTours();

            string lang = Jeu.selectLang();

            var dictionnaire = new Dictionnaire(lang);

            var plateau = new Plateau(4);

            var jeu = new Jeu(joueurs, plateau, dictionnaire, nbTours);
            Console.Clear();

            jeu.LancerPartie(jeu);

            Console.WriteLine("Merci d'avoir joué à Boogle !");
        }
    }
}
