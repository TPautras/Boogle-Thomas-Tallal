using System;
using System.Drawing;
using System.Windows.Forms;

public class NuageForm : Form
{
    public NuageForm(string imagePath)
    {
        this.Text = "Nuage de Mots";
        PictureBox pictureBox = new PictureBox
        {
            Image = Image.FromFile(imagePath),
            SizeMode = PictureBoxSizeMode.AutoSize
        };
        this.Controls.Add(pictureBox);
        this.AutoSize = true;
        this.StartPosition = FormStartPosition.CenterScreen;
    }
}
