namespace SpriteVortex
{
    partial class InputControl2
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
            this.InputControlPanel = new System.Windows.Forms.Panel();
            this.InputControlLabel = new System.Windows.Forms.Label();
            this.InputControlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputControlPanel
            // 
            this.InputControlPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.InputControlPanel.Controls.Add(this.InputControlLabel);
            this.InputControlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InputControlPanel.Location = new System.Drawing.Point(0, 0);
            this.InputControlPanel.Name = "InputControlPanel";
            this.InputControlPanel.Size = new System.Drawing.Size(141, 28);
            this.InputControlPanel.TabIndex = 0;
            // 
            // InputControlLabel
            // 
            this.InputControlLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InputControlLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputControlLabel.ForeColor = System.Drawing.Color.White;
            this.InputControlLabel.Location = new System.Drawing.Point(0, 0);
            this.InputControlLabel.Name = "InputControlLabel";
            this.InputControlLabel.Size = new System.Drawing.Size(141, 28);
            this.InputControlLabel.TabIndex = 0;
            this.InputControlLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.InputControlLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.InputControlLabel_MouseClick);
            // 
            // InputControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.InputControlPanel);
            this.Name = "InputControl2";
            this.Size = new System.Drawing.Size(141, 28);
            this.InputControlPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel InputControlPanel;
        private System.Windows.Forms.Label InputControlLabel;
    }
}
