using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boogle_Thomas_Pautras
{
    internal class Jeu
    {
        #region Initialisation
        private List<Joueur> Joueurs;
        private Plateau PlateauActuel;
        private Dictionnaire DictionnaireActuel;
        private bool gameIsActive = true;

        public Jeu(List<Joueur> joueurs, Plateau plateau, Dictionnaire dictionnaire)
        {
            this.Joueurs = joueurs;
            this.PlateauActuel = plateau;
            this.DictionnaireActuel = dictionnaire;
        }
        #endregion

        #region Affichages

        public void AfficherMenu()
        {
            Console.WriteLine("Bienvenue dans le jeu de Boogle !");
            Console.WriteLine("============================");
            Console.WriteLine("1. Commencer une nouvelle partie");
            Console.WriteLine("2. Voir les r�gles du jeu");
            Console.WriteLine("3. Quitter");
            Console.WriteLine("============================");
            Console.Write("Choisissez une option : ");
        }

        public void AfficherRegles()
        {
            Console.WriteLine("R�gles du jeu :");
            Console.WriteLine("1. Chaque joueur a une minute pour trouver des mots.");
            Console.WriteLine("2. Les mots doivent �tre form�s avec des lettres adjacentes.");
            Console.WriteLine("3. Les mots doivent avoir au moins deux lettres.");
            Console.WriteLine("4. Les mots trouv�s doivent appartenir au dictionnaire.");
            Console.WriteLine("Bonne chance et amusez-vous bien !\n");
        }

        public void AfficherTour(Joueur joueur)
        {
            Console.WriteLine($"C'est le tour de {joueur.Nom} !");
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
                Console.WriteLine($"{joueur.Nom} : {joueur.Score} points");
            }
        }

        #endregion

        public void LancerPartie()
        {
            Console.WriteLine("La partie commence ! Bonne chance � tous les joueurs.\n");
            while(this.gameIsActive)
            {
                foreach (var joueur in Joueurs)
                {
                    AfficherTour(joueur);
                }
            }
            AfficherScores();
        }
    }
}
