using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Boogle_Thomas_Pautras
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Joueur> joueurs = Jeu.selectJoueurs(); //Crée la liste des utilisateurs et détermine si ce sont des IA

            int nbTours = Jeu.selectTours(); //Détermine le nombre de tours voulus par l'utilisateur

            string lang = Jeu.selectLang(); //Détermine la langue de la partie

            var dictionnaire = new Dictionnaire(lang); //Initialise le dictionnaire en fonction de la langue

            var plateau = new Plateau(4); //Inistialise le plateau avec une valeur par défaut de 4

            var jeu = new Jeu(joueurs, plateau, dictionnaire, nbTours); //Initialise le Jeu avec les valeurs déterminées juste avant
            Console.Clear();

            jeu.LancerBoogle(jeu); // Lance la partie de BOOGLE !!

            Console.WriteLine("Merci d'avoir joué à Boogle !");
        }
    }
}
