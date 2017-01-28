#region License

/**
     * Copyright (C) 2010 Rafael Vasco (rafaelvasco87@gmail.com)
     * 
     *
     * This program is free software; you can redistribute it and/or
     * modify it under the terms of the GNU General Public License
     * as published by the Free Software Foundation; either version 2
     * of the License, or (at your option) any later version.

     * This program is distributed in the hope that it will be useful,
     * but WITHOUT ANY WARRANTY; without even the implied warranty of
     * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
     * GNU General Public License for more details.

     * You should have received a copy of the GNU General Public License
     * along with this program; if not, write to the Free Software
     * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
     */

#endregion

using System.Windows.Forms;
using Vortex.Input;
using MouseButtons = System.Windows.Forms.MouseButtons;

namespace SpriteVortex.Helpers
{
    public static class InputConfigurationHelper
    {
        public static void LoadCurrentControlConfigIntoInputControl(ControlConfig config, InputControl2 inputControl)
        {
            if (config.Key != null)
            {
                inputControl.ControlLabel = string.Format("{0} + {1}",
                                                          config.Key,
                                                          config.MouseButton);
            }
            else
            {
                inputControl.ControlLabel = string.Format("{0}", config.MouseButton);
            }

            inputControl.AssignedKey = ConvertKey(config.Key);
            inputControl.AssignedButton = ConvertButton(config.MouseButton);
        }

        public static Key? ConvertKey(Keys key)
        {
            Key? convertedKey = null;
            switch (key)
            {
                case Keys.ControlKey:
                    convertedKey = Key.LeftControl;
                    break;
                case Keys.ShiftKey:
                    convertedKey = Key.LeftShift;
                    break;
            }


            return convertedKey;
        }

        public static Keys ConvertKey(Key? key)
        {
            if (key == null)
            {
                return Keys.None;
            }

            Key pKey = (Key) key;

            Keys convertedKey = Keys.None;
            switch (pKey)
            {
                case Key.LeftControl:
                    convertedKey = Keys.ControlKey;
                    break;
                case Key.LeftShift:
                    convertedKey = Keys.ShiftKey;
                    break;
            }

            return convertedKey;
        }

        public static MouseButton ConvertButton(MouseButtons button)
        {
            MouseButton convertedButton = MouseButton.None;
            switch (button)
            {
                case MouseButtons.Left:
                    convertedButton = MouseButton.Left;
                    break;
                case MouseButtons.Right:
                    convertedButton = MouseButton.Right;
                    break;
                case MouseButtons.Middle:
                    convertedButton = MouseButton.Middle;
                    break;
            }
            return convertedButton;
        }

        public static MouseButtons ConvertButton(MouseButton button)
        {
            MouseButtons convertedButton = MouseButtons.None;
            switch (button)
            {
                case MouseButton.Left:
                    convertedButton = MouseButtons.Left;
                    break;
                case MouseButton.Right:
                    convertedButton = MouseButtons.Right;
                    break;
                case MouseButton.Middle:
                    convertedButton = MouseButtons.Middle;
                    break;
            }
            return convertedButton;
        }


        //private Keys ConvertKey(Key key)
        //{
        //    if (key.Equals(Key.LeftControl))
        //    {
        //        return Keys.ControlKey;
        //    }
        //    if (key.Equals(Key.LeftShift))
        //    {
        //        return Keys.ShiftKey;
        //    }
        //    return Keys.None;
        //}
    }
}