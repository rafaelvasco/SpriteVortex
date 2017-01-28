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
using SpriteVortex.Gui;
using Vortex.Drawing;
using Control = System.Windows.Forms.Control;


namespace SpriteVortex
{
    public enum CursorAnimation
    {
        Idle,
        Click,
        MoveHor,
        MoveVert,
        Pick,
        ColorPick
    }

    public abstract class RenderDisplay
    {
        public string Name;
        public Rect RenderRegion;
        public Control Target;
        public bool TransparentBack;
        public Camera Camera;
        protected GuiManager guiManager;

        public virtual bool Focused { get; set; }

        public bool MouseIsInside;

        public bool MouseIsOverGUI
        {
            get { return guiManager != null ? guiManager.MouseIsOverGUI : false; }
        }

        public Vector2 ScreenMousePosition;

        public Vector2 WorldMousePosition
        {
            get { return Camera.ScreenToWorld(ScreenMousePosition); }
        }


        protected Dictionary<CursorAnimation, SimpleSpriteAnimation> CursorAnimations;

        protected CursorAnimation CurrentCursorAnimation;


        protected bool Debug;

        protected SpriteFont Font;

        protected RenderDisplay(string n, int w, int h, Control target)
        {
            Debug = true;
            Name = n;
            RenderRegion = new Rect(Point.Empty, new Size(w, h));
            Target = target;
            ScreenMousePosition = Vector2.Zero;
            CursorAnimations = new Dictionary<CursorAnimation, SimpleSpriteAnimation>();


            Camera = new Camera(RenderRegion);


            Font = Application.Instance.ResourcesManager.GetFont("DefaultFont");
        }


        public virtual void Dispose()
        {
        }

        public virtual void SetUpInputEvents()
        {
        }

        public virtual void InitializeGUI()
        {
            guiManager = new GuiManager(this);
        }

        public virtual void Update(float frameTime)
        {
            guiManager.UpdateControls(frameTime);

            Camera.Update();


            var mousePos = Input.MousePosition(Name);


            if (Input.MouseIsInside(Name))
            {
                Focused = true;
                ScreenMousePosition.X = mousePos.X;
                ScreenMousePosition.Y = mousePos.Y;

                MouseIsInside = true;
            }
            else
            {
                MouseIsInside = false;
                Focused = false;
            }
        }

        public virtual void Render(Canvas2D canvas)
        {
            canvas.Clear(Configuration.BackgroundColor);


            if (TransparentBack)
            {
                var boxWidth = (int) (0.01f*Target.Width);
                var qtdeX = (int) Math.Ceiling((double) RenderRegion.Width/boxWidth);
                var qtdeY = (int) Math.Ceiling((double) RenderRegion.Height/boxWidth);

                var color1 = ColorU.White;
                var color2 = ColorU.LightGray;

                for (var i = 0; i < qtdeX; i++)
                {
                    for (var j = 0; j < qtdeY; j++)
                    {
                        var curColor = i%2 == j%2 ? color1 : color2;
                        canvas.DrawFilledRect(i*boxWidth, j*boxWidth, boxWidth, boxWidth, curColor);
                    }
                }
            }
        }
    }
}