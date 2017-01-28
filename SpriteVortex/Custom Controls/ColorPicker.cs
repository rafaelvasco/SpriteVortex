using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SpriteVortex
{
    public partial class ColorPicker : UserControl
    {
        public class ColorChangedEventArgs : EventArgs
        {
            public Color NewColor { get; private set; }

            public ColorChangedEventArgs(Color newColor)
            {
                NewColor = newColor;
            }
            
        }

        public delegate void ColorChangedHandler(ColorChangedEventArgs args);

        public event ColorChangedHandler ColorChanged;

        public Color SelectedColor
        {
            get { return _selectedColor; } 
            set
            {
                if (ColorChanged != null && !_selectedColor.Equals(value))
                {
                    ColorChanged(new ColorChangedEventArgs(value));

                }

                _selectedColor = value;

                colorPanel.BackColor = value;
                
                
            }
        }


        public ColorPicker()
        {
            InitializeComponent();
        }

       

        private void colorPanel_Click(object sender, EventArgs e)
        {
            if (ColorDialog.ShowDialog() == DialogResult.OK)
            {
                colorPanel.BackColor = ColorDialog.Color;



                if (ColorChanged != null && !SelectedColor.Equals(ColorDialog.Color))
                {

                    ColorChanged(new ColorChangedEventArgs(ColorDialog.Color));
                }

                SelectedColor = ColorDialog.Color;
            }
        }

        private Color _selectedColor;
    }
}
