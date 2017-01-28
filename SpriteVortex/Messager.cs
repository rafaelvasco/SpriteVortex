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

using System.Drawing;
using ComponentFactory.Krypton.Toolkit;
using Timer = System.Windows.Forms.Timer;


namespace SpriteVortex
{
    public static class Messager
    {
        public enum Mode
        {
            Exception,
            Message,
            None
        }

        public static int MessageDelay
        {
            get { return messageTimer.Interval; }
            set { messageTimer.Interval = value; }
        }

        private static KryptonPanel messagePanel;
        private static KryptonLabel messageLabel;
        private static Timer messageTimer;


        public static Mode MessageMode;


        public static void Initialize(KryptonPanel panel, KryptonLabel label, Timer timer)
        {
            messagePanel = panel;
            messageLabel = label;
            messageTimer = timer;
            messageTimer.Tick += MessageTimerTick;
            MessageDelay = 2000;
        }

        private static void MessageTimerTick(object sender, System.EventArgs e)
        {
            SetMode(Mode.None);
            messageLabel.Visible = false;
            messageTimer.Stop();
        }


        public static void SetMode(Mode mode)
        {
            MessageMode = mode;

            switch (mode)
            {
                case Mode.Message:
                    {
                        messagePanel.StateNormal.Color1 = Color.SkyBlue;
                        messagePanel.StateNormal.Color2 = Color.Teal;
                    }
                    break;
                case Mode.Exception:
                    {
                        messagePanel.StateNormal.Color1 = Color.Red;
                        messagePanel.StateNormal.Color2 = Color.DarkRed;
                    }
                    break;
                case Mode.None:
                    {
                        messagePanel.StateNormal.Color1 = Color.WhiteSmoke;
                        messagePanel.StateNormal.Color2 = Color.LightGray;
                    }
                    break;
            }
        }

        public static void ShowMessage(Mode mode, string message)
        {
            MessageDelay = message.Length*50;

            SetMode(mode);

            messageLabel.Visible = true;

            messageLabel.Text = message;

            messageLabel.Refresh();

            messageLabel.Left = messagePanel.Width/2 - messageLabel.Width/2;

            if (!messageTimer.Enabled)
            {
                messageTimer.Enabled = true;
            }

            messageTimer.Start();
        }
    }
}