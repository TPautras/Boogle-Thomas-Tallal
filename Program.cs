using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boogle_Thomas_Pautras
{
    class Program
    {
        static void Main(string[] args)
        {
            Jeu jeu = new Jeu(new List<Joueur>(), new Plateau(), new Dictionnaire());
            jeu.AfficherMenu();
        }

    }
}
