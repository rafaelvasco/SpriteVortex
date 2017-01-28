namespace SpriteVortex
{
    partial class AboutSplash
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.splashPictureBox = new System.Windows.Forms.PictureBox();
            this.versionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splashPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splashPictureBox
            // 
            this.splashPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.splashPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splashPictureBox.Image = global::SpriteVortex.Properties.Resources.spritevortexSplash;
            this.splashPictureBox.Location = new System.Drawing.Point(0, 0);
            this.splashPictureBox.Name = "splashPictureBox";
            this.splashPictureBox.Size = new System.Drawing.Size(541, 297);
            this.splashPictureBox.TabIndex = 0;
            this.splashPictureBox.TabStop = false;
            this.splashPictureBox.Click += new System.EventHandler(this.splashPictureBox_Click);
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.Location = new System.Drawing.Point(472, 9);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(38, 13);
            this.versionLabel.TabIndex = 1;
            this.versionLabel.Text = "label1";
            // 
            // AboutSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(541, 297);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.splashPictureBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AboutSplash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AboutSplash";
            ((System.ComponentModel.ISupportInitialize)(this.splashPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private System.Windows.Forms.PictureBox splashPictureBox;
        private System.Windows.Forms.Label versionLabel;
    }
}

