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
using System.Drawing;
using System.Windows.Forms;
using Vortex.Drawing;
using Vortex.Input;

namespace SpriteVortex
{
    public class AnimConfigDisplay : RenderDisplay
    {
        public Vector2 StripDrawPos;

        public SizeF AnimFrameSize
        {
            get { return new SizeF(RenderRegion.Height - 40, RenderRegion.Height - 40); }
        }

        public Animation CurrentAnimation
        {
            get { return _animationStrip.CurrentAnimation; }
        }

        private readonly AnimationStrip _animationStrip;


        public AnimConfigDisplay(string name, Control panel)
            : base(name, panel.Width, panel.Height, panel)
        {
            var animStripPosition = new Vector2(0, RenderRegion.Height/2f - AnimFrameSize.Height/2f - 5f);

            animStripPosition = Camera.ScreenToWorld(animStripPosition);

            _animationStrip = new AnimationStrip(animStripPosition, AnimFrameSize, RenderRegion);
        }

        private void AnimConfigDisplay_OnWheel(MouseWheelActionInfo mouseWheelActionInfo)
        {
            MoveCamera(AnimFrameSize.Width*mouseWheelActionInfo.WheelDelta);
        }

        public List<Frame> SelectedFrames
        {
            get
            {
                if (_animationStrip.LoadedAnimation)
                {
                    var selectedFrames = new List<Frame>();
                    foreach (var frameContainer in _animationStrip.SelectedFrameContainers)
                    {
                        selectedFrames.Add(frameContainer.Frame);
                    }
                    return selectedFrames;
                }
                return null;
            }
        }

        public override bool Focused
        {
            get { return base.Focused; }
            set
            {
                base.Focused = value;
                if (!base.Focused)
                {
                    UnFocusHighglightedContainer();
                }
            }
        }


        public void SetAnimationFrameRate(float frameRate, bool allFrames)
        {
            if (CurrentAnimation == null)
            {
                return;
            }

            if (allFrames)
            {
                CurrentAnimation.FrameRate = frameRate;
            }
            else
            {
                foreach (var frame in SelectedFrames)
                {
                    CurrentAnimation.SetFrameDuration(frame.Num, 1f/frameRate);
                }
            }


            CurrentAnimation.Reset();
        }

        public override void Update(float frameTime)
        {
            base.Update(frameTime);

            if (!MouseIsInside)
            {
                return;
            }


            _animationStrip.Update(WorldMousePosition);
        }

        public override void SetUpInputEvents()
        {
            Input.MouseListeners["AnimConfig"].OnWheel += AnimConfigDisplay_OnWheel;
        }

        public void MoveCamera(float delta)
        {
            MoveCamera(delta, 0.12f);
        }

        public void MoveCamera(float delta, float damping)
        {
            Camera.MoveSmooth(delta, 0f, damping);
        }

        public void Refresh()
        {
            _animationStrip.FrameSize.Height = AnimFrameSize.Height;
            _animationStrip.FrameSize.Width = AnimFrameSize.Width;
            _animationStrip.Position =
                Camera.ScreenToWorld(new Vector2(0, RenderRegion.Height/2f - _animationStrip.FrameSize.Height/2f - 5f));

            for (int i = 0; i < _animationStrip.FrameContainers.Count; i++)
            {
                var frameContainer = _animationStrip.FrameContainers[i];

                frameContainer.X = _animationStrip.Position.X + i*_animationStrip.FrameSize.Width +
                                   (i + 1)*_animationStrip.FrameMargin;
                frameContainer.Y = _animationStrip.Position.Y;
                frameContainer.Size = _animationStrip.FrameSize;
                frameContainer.AdjustFrameScale();
            }
        }

        public void AddFrameContainer()
        {
            _animationStrip.AddContainer();
        }

        public void ClearSelectedContainers()
        {
            _animationStrip.ClearSelectedContainers();
        }

        public void RemoveSelectedContainers()
        {
            _animationStrip.RemoveSelectedContainers();
        }

        public void AddFramesToAnimationStrip(List<Frame> frames)
        {
            _animationStrip.AddFrames(frames);
        }

        public void SetAnimation(Animation animation)
        {
            _animationStrip.SetAnimation(animation);
        }

        public void UnloadAnimation()
        {
            _animationStrip.UnloadAnimation();
        }

        public void UnFocusHighglightedContainer()
        {
            _animationStrip.UnfocusHighlightedContainer();
        }

        public override void Render(Canvas2D canvas)
        {
            base.Render(canvas);

            DrawAnimStripDecoration(canvas);

            using (canvas <= Camera.Transformation)
            {
                _animationStrip.Draw(canvas);
            }
        }


        private void DrawAnimStripDecoration(Canvas2D canvas)
        {
            //-----------------------------TOP-STRIP------------------------------------------
            canvas.DrawLine(0, 2, RenderRegion.Right, 2, new LineStyle(2f, LineStroke.Solid), ColorU.Black);
            canvas.DrawLine(0, 2, RenderRegion.Right, 2, new LineStyle(2f, LineStroke.Dashed), ColorU.White);
            canvas.DrawLine(0, 3, RenderRegion.Right, 3, new LineStyle(2f, LineStroke.Solid), ColorU.Black);
            canvas.DrawLine(0, 3, RenderRegion.Right, 3, new LineStyle(2f, LineStroke.Dashed), ColorU.White);
            canvas.DrawLine(0, 4, RenderRegion.Right, 4, new LineStyle(2f, LineStroke.Solid), ColorU.Black);
            canvas.DrawLine(0, 4, RenderRegion.Right, 4, new LineStyle(2f, LineStroke.Dashed), ColorU.White);
            canvas.DrawLine(0, 5, RenderRegion.Right, 5, new LineStyle(2f, LineStroke.Solid), ColorU.Black);
            canvas.DrawLine(0, 5, RenderRegion.Right, 5, new LineStyle(2f, LineStroke.Dashed), ColorU.White);

            //-----------------------------BOTTOM-STRIP----------------------------------------
            canvas.DrawLine(0, RenderRegion.Bottom - 2, RenderRegion.Right, RenderRegion.Bottom - 2,
                            new LineStyle(2f, LineStroke.Solid), ColorU.Black);
            canvas.DrawLine(0, RenderRegion.Bottom - 2, RenderRegion.Right, RenderRegion.Bottom - 2,
                            new LineStyle(2f, LineStroke.Dashed), ColorU.White);
            canvas.DrawLine(0, RenderRegion.Bottom - 3, RenderRegion.Right, RenderRegion.Bottom - 3,
                            new LineStyle(2f, LineStroke.Solid), ColorU.Black);
            canvas.DrawLine(0, RenderRegion.Bottom - 3, RenderRegion.Right, RenderRegion.Bottom - 3,
                            new LineStyle(2f, LineStroke.Dashed), ColorU.White);
            canvas.DrawLine(0, RenderRegion.Bottom - 4, RenderRegion.Right, RenderRegion.Bottom - 4,
                            new LineStyle(2f, LineStroke.Solid), ColorU.Black);
            canvas.DrawLine(0, RenderRegion.Bottom - 4, RenderRegion.Right, RenderRegion.Bottom - 4,
                            new LineStyle(2f, LineStroke.Dashed), ColorU.White);
            canvas.DrawLine(0, RenderRegion.Bottom - 5, RenderRegion.Right, RenderRegion.Bottom - 5,
                            new LineStyle(2f, LineStroke.Solid), ColorU.Black);
            canvas.DrawLine(0, RenderRegion.Bottom - 5, RenderRegion.Right, RenderRegion.Bottom - 5,
                            new LineStyle(2f, LineStroke.Dashed), ColorU.White);

            //-----------------------------BACKGROUND--------------------------------------------

            canvas.DrawFilledRect(0, 7, RenderRegion.Width, RenderRegion.Height - 13, ColorU.Black.MultiplyAlpha(0.6f));
        }


        public override void Dispose()
        {
        }
    }
}