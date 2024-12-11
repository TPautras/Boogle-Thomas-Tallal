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
    // Ajout d'une liste pour suivre les rectangles occupés
    private readonly List<Rectangle> rectanglesOccupes;

    public NuageMots(Dictionary<string, int> mots, int width = 800, int height = 600)
    {
        this.mots = mots;
        this.width = width;
        this.height = height;
        this.random = new Random();
        this.rectanglesOccupes = new List<Rectangle>();
    }



    /// <summary>
    /// Génère une image de nuage de mots et la sauvegarde au format PNG.
    /// Les mots sont placés selon leur fréquence d'apparition, avec une taille 
    /// de police proportionnelle et des couleurs aléatoires. Les mots les plus 
    /// fréquents sont traités en premier pour optimiser leur placement
    /// </summary>
    /// <param name="cheminSortie">String - Le chemin complet où sauvegarder l'image générée</param>
    public void GenererNuage(string cheminSortie)
    {
        using (Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
        using (Graphics g = Graphics.FromImage(bitmap))
        {
            g.Clear(Color.White);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int maxOccurrences = mots.Values.Max();
            int minOccurrences = mots.Values.Min();

            rectanglesOccupes.Clear();

            foreach (var paire in mots.OrderByDescending(x => x.Value))
            {

                float taille = CalculerTaillePolice(paire.Value, minOccurrences, maxOccurrences);

                Color couleur = ObtenirCouleurAleatoire();

                using (Font font = new Font("Arial", taille, FontStyle.Bold))
                using (Brush brush = new SolidBrush(couleur))
                {
                    Point position = TrouverPosition(g, paire.Key, font);

                    g.DrawString(paire.Key, font, brush, position);

                    SizeF tailleMot = g.MeasureString(paire.Key, font);
                    rectanglesOccupes.Add(new Rectangle(
                        position.X,
                        position.Y,
                        (int)tailleMot.Width,
                        (int)tailleMot.Height
                    ));
                }
            }

            try
            {
                bitmap.Save(cheminSortie, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
                throw new Exception($"Erreur lors de la sauvegarde de l'image : {cheminSortie}");
            }
        }
    }



    /// <summary>
    /// Calcule la taille de police proportionnelle basée sur le nombre d'occurrences
    /// d'un mot, mise à l'échelle entre une valeur minimale et maximale
    /// </summary>
    /// <param name="occurrences">Nombre d'occurrences du mot</param>
    /// <param name="min">Nombre minimum d'occurrences dans l'ensemble des mots</param>
    /// <param name="max">Nombre maximum d'occurrences dans l'ensemble des mots</param>
    /// <returns name="res">Float représentant la taille de police calculée entre 10 et 48</returns>
    private float CalculerTaillePolice(int occurrences, int min, int max)
    {
        // Échelle de taille de police entre 10 et 48
        const float minSize = 10f;
        const float maxSize = 48f;
        if (max == min) return maxSize;
        return minSize + (maxSize - minSize) * (occurrences - min) / (max - min);
    }



    /// <summary>
    /// Retourne une couleur aléatoire à partir d'une palette prédéfinie
    /// de cinq couleurs harmonieuses (bleu, vert, violet, orange, turquoise)
    /// </summary>
    /// <returns name="res">Color - Une couleur aléatoire de la palette</returns>
    private Color ObtenirCouleurAleatoire()
    {
        Color[] palette = {
            Color.FromArgb(255, 41, 128, 185),  // Bleu
            Color.FromArgb(255, 39, 174, 96),   // Vert
            Color.FromArgb(255, 142, 68, 173),  // Violet
            Color.FromArgb(255, 211, 84, 0),    // Orange
            Color.FromArgb(255, 22, 160, 133)   // Turquoise
        };
        return palette[random.Next(palette.Length)];
    }


    /// <summary>
    /// Cherche une position libre dans l'espace disponible pour placer un mot
    /// en évitant les chevauchements avec les mots déjà placés.
    /// Si aucune position n'est trouvée après 100 tentatives, retourne la position (0,0)
    /// </summary>
    /// <param name="g">Graphics - Le contexte graphique pour mesurer la taille du texte</param>
    /// <param name="mot">String - Le mot à positionner</param>
    /// <param name="font">Font - La police utilisée pour le mot</param>
    /// <returns name="res">Point - Les coordonnées (x,y) où placer le mot</returns>
    private Point TrouverPosition(Graphics g, string mot, Font font)
    {
        SizeF tailleMot = g.MeasureString(mot, font);

        for (int i = 0; i < 100; i++)
        {
            int x = random.Next(0, width - (int)tailleMot.Width);
            int y = random.Next(0, height - (int)tailleMot.Height);

            Rectangle nouveauRect = new Rectangle(x, y, (int)tailleMot.Width, (int)tailleMot.Height);

            if (EstPositionLibre(nouveauRect))
            {
                return new Point(x, y);
            }
        }

        return new Point(0, 0);
    }


    /// <summary>
    /// Vérifie si la position proposée pour un nouveau mot ne chevauche pas 
    /// les rectangles occupés par les mots déjà placés dans le nuage
    /// </summary>
    /// <param name="nouveauRect">Rectangle - Le rectangle correspondant à la zone du nouveau mot</param>
    /// <returns name="res">Bool - True si la position est libre, False si un chevauchement est détecté</returns>
    private bool EstPositionLibre(Rectangle nouveauRect)
    {
        // Vérifier les collisions avec les rectangles existants
        foreach (Rectangle rect in rectanglesOccupes)
        {
            if (rect.IntersectsWith(nouveauRect))
            {
                return false;
            }
        }
        return true;
    }
}