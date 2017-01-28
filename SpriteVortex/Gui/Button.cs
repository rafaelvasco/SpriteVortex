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

using System.Collections.Generic;
using Vortex.Drawing;
using Vortex.Input;

namespace SpriteVortex.Gui
{
    public class Button : Control
    {
        public string Text;

        public static SpriteFont Font;

        public Vector2 FrameBorder;

        public Dictionary<ControlState, Sprite> Frames { get; private set; }

        public Sprite Overlay { get; private set; }


        public ColorU TextColor;

        public Button(GuiManager manager, string text) : base(manager)
        {
            Text = text;
            TextColor = ColorU.White;
            Frames = new Dictionary<ControlState, Sprite>();
            FrameBorder = new Vector2(4, 4);
            Font = ResourcesManager.Instance.GetFont("segoe_ui_15");
            SetFrames(ResourcesManager.Instance.ResourceImageSheet.Sprites["gui_button_normal"],
                      ResourcesManager.Instance.ResourceImageSheet.Sprites["gui_button_normal"],
                      ResourcesManager.Instance.ResourceImageSheet.Sprites["gui_button_pressed"]);

            SetOverlay(ResourcesManager.Instance.ResourceImageSheet.Sprites["gui_button_overlay"]);
        }

        public void SetFrame(Sprite frame)
        {
            Frames.Add(ControlState.Normal, frame);
        }

        public void SetFrames(Sprite normal, Sprite hovered, Sprite pressed)
        {
            Frames.Add(ControlState.Normal, normal);
            Frames.Add(ControlState.Hovered, hovered);
            Frames.Add(ControlState.Pressed, pressed);
        }

        public void SetOverlay(Sprite overlay)
        {
            Overlay = overlay;
        }

        public override void Draw(Canvas2D canvas)
        {
            canvas.DrawFrame(AbsoluteBoundingRect, Frames[State], FrameBorder,
                             true, Color);

            if (_drawOverlay)
            {
                canvas.DrawSprite(AbsoluteBoundingRect, Overlay, ColorU.White);
            }

            var textSize = Canvas2D.MeasureText(Font, Text);

            Vector2 textDrawPos = new Vector2(AbsoluteBoundingRect.CenterX - textSize.X/2,
                                              AbsoluteBoundingRect.CenterY - textSize.Y/2);

            canvas.DrawText(Font, textDrawPos.Floored, Text, TextColor);


            base.Draw(canvas);
        }

        protected override void OnHover(MouseMoveActionInfo e)
        {
            base.OnHover(e);
            _drawOverlay = true;
        }

        protected override void OnLeave(MouseMoveActionInfo e)
        {
            base.OnLeave(e);
            _drawOverlay = false;
        }

        private bool _drawOverlay;
    }
}