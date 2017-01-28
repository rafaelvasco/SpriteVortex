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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SpriteVortex
{
    public partial class InputControl2 : UserControl
    {
        private static Dictionary<int, InputControl2> inputControlInternalList = new Dictionary<int, InputControl2>();

        public delegate void ControlsChangedHandler(ControlsChangedEventArgs args);

        public class ControlsChangedEventArgs : EventArgs
        {
            public Keys NewControlKey { get; private set; }
            public MouseButtons NewControlMouseButton { get; private set; }

            public ControlsChangedEventArgs(Keys key, MouseButtons button)
            {
                NewControlKey = key;
                NewControlMouseButton = button;
            }
        }

        /// <summary>
        /// Ocurrs when the new controls are set
        /// </summary>
        public event ControlsChangedHandler ControlsChanged;

        public Keys AssignedKey
        {
            get { return controlKey; }
            set { controlKey = value; }
        }

        public MouseButtons AssignedButton
        {
            get { return controlButton; }
            set { controlButton = value; }
        }


        public string ControlLabel
        {
            get { return InputControlLabel.Text; }
            set { InputControlLabel.Text = value; }
        }

        public InputControl2()
        {
            InitializeComponent();

            id = inputControlInternalList.Count;

            InputControlLabel.KeyUp += InputControlLabel_KeyUp;
            InputControlLabel.KeyDown += InputControlLabel_KeyDown;

            inputControlInternalList.Add(id, this);
        }

        public static void ResetInternalList()
        {
            inputControlInternalList.Clear();
        }

        private string ProcessKeyCode(Keys keys)
        {
            return keys.Equals(Keys.ControlKey) ? "LeftControl" : (keys.Equals(Keys.ShiftKey) ? "LeftShift" : "");
        }


        private void InputControlLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.ControlKey) || e.KeyCode.Equals(Keys.ShiftKey))
            {
                if (!keyDown)
                {
                    InputControlLabel.Text = string.Format("{0} + ", ProcessKeyCode(e.KeyCode));
                    tempKey = e.KeyCode;
                    keyDown = true;
                }
            }
        }


        private bool ValidateControls(Keys key, MouseButtons button)
        {
            bool ok = true;

            foreach (var inputControl2 in inputControlInternalList)
            {
                if (inputControl2.Key != this.id)
                {
                    if (inputControl2.Value.AssignedKey.Equals(key))
                    {
                        if (inputControl2.Value.AssignedButton.Equals(button))
                        {
                            ok = false;
                        }
                    }
                }
            }


            return ok;
        }


        private void InputControlLabel_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                Restore();
                InputControlPanel.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
                UnFocus();
            }
            else
            {
                if (e.KeyCode.Equals(Keys.ControlKey) || e.KeyCode.Equals(Keys.ShiftKey))
                {
                    keyDown = false;

                    if (addedMouseButton)
                    {
                        if (ValidateControls(tempKey, tempButton))
                        {
                            controlKey = tempKey;
                            controlButton = tempButton;
                            OnControlsChanged(new ControlsChangedEventArgs(controlKey, controlButton));
                        }
                        else
                        {
                            ResetLabelToLastValue();
                        }

                        tempKey = Keys.None;
                        tempButton = MouseButtons.None;
                        addedMouseButton = false;
                    }
                    else
                    {
                        Restore();
                    }

                    InputControlPanel.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
                    UnFocus();
                }
            }
        }


        public void ResetLabelToLastValue()
        {
            InputControlLabel.Text = labelTemp;
        }

        private void Restore()
        {
            if (controlButton != MouseButtons.None && controlKey != Keys.None)
            {
                InputControlLabel.Text = string.Format("{0} + {1}", ProcessKeyCode(controlKey), controlButton);
            }
            else
            {
                InputControlLabel.Text = string.Format("{0}", controlButton);
            }
        }


        private void OnControlsChanged(ControlsChangedEventArgs args)
        {
            if (ControlsChanged != null)
            {
                ControlsChanged(args);
            }
        }


        private new void Focus()
        {
            InputControlLabel.Focus();
            focused = true;
        }

        private void UnFocus()
        {
            InputControlPanel.Focus();
            focused = false;
        }


        private void InputControlLabel_MouseClick(object sender, MouseEventArgs e)
        {
            if (!focused)
            {
                labelTemp = InputControlLabel.Text;
                Focus();
                InputControlPanel.BackColor = Color.CornflowerBlue;
                InputControlLabel.Text = "?";
            }
            else
            {
                if (keyDown)
                {
                    tempButton = e.Button;
                    InputControlLabel.Text += string.Format("{0}", e.Button);
                    addedMouseButton = true;
                }
                else
                {
                    InputControlPanel.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
                    UnFocus();

                    controlKey = Keys.None;
                    if (ValidateControls(controlKey, e.Button))
                    {
                        InputControlLabel.Text = string.Format("{0}", e.Button);
                        controlButton = e.Button;
                        OnControlsChanged(new ControlsChangedEventArgs(controlKey, controlButton));
                    }
                    else
                    {
                        ResetLabelToLastValue();
                    }
                }
            }
        }

        private bool addedMouseButton;
        private bool keyDown;
        private Keys tempKey;
        private MouseButtons tempButton;
        private bool focused;
        private Keys controlKey = Keys.None;
        private MouseButtons controlButton = MouseButtons.None;
        private string labelTemp;
        private int id;
    }
}