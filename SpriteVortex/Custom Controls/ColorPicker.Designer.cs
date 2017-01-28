namespace SpriteVortex
{
    partial class ColorPicker
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.colorPanel = new System.Windows.Forms.Panel();
            this.ColorDialog = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // colorPanel
            // 
            this.colorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorPanel.Location = new System.Drawing.Point(0, 0);
            this.colorPanel.Name = "colorPanel";
            this.colorPanel.Size = new System.Drawing.Size(128, 38);
            this.colorPanel.TabIndex = 0;
            this.colorPanel.Click += new System.EventHandler(this.colorPanel_Click);
            // 
            // ColorDialog
            // 
            this.ColorDialog.Color = System.Drawing.Color.LightSkyBlue;
            // 
            // ColorPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.colorPanel);
            this.Name = "ColorPicker";
            this.Size = new System.Drawing.Size(128, 38);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel colorPanel;
        private System.Windows.Forms.ColorDialog ColorDialog;
    }
}
