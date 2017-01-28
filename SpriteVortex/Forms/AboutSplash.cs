using System.Drawing;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using SpriteVortex.Helpers;

namespace SpriteVortex
{
    public partial class AboutSplash : KryptonForm
    {

        public AboutSplash()
        {
            InitializeComponent();

            versionLabel.Parent = splashPictureBox;
            versionLabel.BackColor = Color.Transparent;
            versionLabel.BringToFront();
            versionLabel.ForeColor = Color.White;
            versionLabel.AutoSize = true;
            versionLabel.Text = Application.Version;
            

        }
            
            

        


        private const int Dropshadow = 0x20000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= Dropshadow;
                return cp;
            }
        }

        private void splashPictureBox_Click(object sender, System.EventArgs e)
        {
            Close();
        }

    }
}