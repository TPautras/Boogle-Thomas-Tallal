using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;

public class NuageMots
{
    private readonly int width;
    private readonly int height;
    private readonly Random random;
    private readonly Dictionary<string, int> mots;

    public NuageMots(Dictionary<string, int> mots, int width = 800, int height = 600)
    {
        this.mots = mots;
        this.width = width;
        this.height = height;
        this.random = new Random();
    }

    public void GenererNuage(string cheminSortie)
    {
        using (Bitmap bitmap = new Bitmap(width, height))
        using (Graphics g = Graphics.FromImage(bitmap))
        {
            // Fond blanc
            g.Clear(Color.White);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Calculer la taille max et min des mots
            int maxOccurrences = mots.Values.Max();
            int minOccurrences = mots.Values.Min();

            foreach (var paire in mots.OrderByDescending(x => x.Value))
            {
                // Calculer la taille de la police proportionnelle au nombre d'occurrences
                float taille = CalculerTaillePolice(paire.Value, minOccurrences, maxOccurrences);
                
                // Choisir une couleur aléatoire
                Color couleur = ObtenirCouleurAleatoire();
                
                using (Font font = new Font("Arial", taille, FontStyle.Bold))
                using (Brush brush = new SolidBrush(couleur))
                {
                    // Trouver une position pour le mot
                    Point position = TrouverPosition(g, paire.Key, font);
                    
                    // Dessiner le mot
                    g.DrawString(paire.Key, font, brush, position);
                }
            }

            // Sauvegarder l'image
            bitmap.Save(cheminSortie, System.Drawing.Imaging.ImageFormat.Png);
        }
    }

    private float CalculerTaillePolice(int occurrences, int min, int max)
    {
        // Échelle de taille de police entre 10 et 48
        const float minSize = 10f;
        const float maxSize = 48f;
        
        if (max == min) return maxSize;
        return minSize + (maxSize - minSize) * (occurrences - min) / (max - min);
    }

    private Color ObtenirCouleurAleatoire()
    {
        // Palette de couleurs prédéfinies
        Color[] palette = {
            Color.FromArgb(41, 128, 185),  // Bleu
            Color.FromArgb(39, 174, 96),   // Vert
            Color.FromArgb(142, 68, 173),  // Violet
            Color.FromArgb(211, 84, 0),    // Orange
            Color.FromArgb(22, 160, 133)   // Turquoise
        };
        
        return palette[random.Next(palette.Length)];
    }

    private Point TrouverPosition(Graphics g, string mot, Font font)
    {
        // Mesurer la taille du texte
        SizeF tailleMot = g.MeasureString(mot, font);
        
        // Essayer plusieurs positions aléatoires
        for (int i = 0; i < 100; i++)
        {
            int x = random.Next(0, width - (int)tailleMot.Width);
            int y = random.Next(0, height - (int)tailleMot.Height);
            
            // Vérifier si la position est libre (à implémenter)
            if (EstPositionLibre(new Rectangle(x, y, (int)tailleMot.Width, (int)tailleMot.Height)))
            {
                return new Point(x, y);
            }
        }
        
        // Position par défaut si aucune position libre n'est trouvée
        return new Point(0, 0);
    }

    private bool EstPositionLibre(Rectangle nouveauRect)
    {
        // À implémenter: vérifier si la position est déjà occupée
        // Pour une version simple, on peut retourner true
        return true;
    }
}
