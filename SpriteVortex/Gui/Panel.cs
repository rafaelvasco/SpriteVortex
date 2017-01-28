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

using Vortex.Drawing;

namespace SpriteVortex.Gui
{
    public class Panel : Control
    {
        public Vector2 FrameBorder;

        public Sprite BackGroundSprite { get; private set; }

        public Panel(GuiManager manager, Rect panelRect) : base(manager)
        {
            Left = (int) panelRect.Left;
            Top = (int) panelRect.Top;
            Width = (int) panelRect.Width;
            Height = (int) panelRect.Height;
            FrameBorder = new Vector2(15, 15);
        }

        public void SetBackgroundSprite(Sprite bg)
        {
            BackGroundSprite = bg;
        }


        public override void Draw(Canvas2D canvas)
        {
            canvas.DrawFrame(AbsoluteBoundingRect, BackGroundSprite, FrameBorder, true, Color);

            base.Draw(canvas);
        }
    }
}