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
using Vortex.Drawing;
using Vortex.Input;

namespace SpriteVortex
{
    public class AnimationStrip
    {
        public class FrameContainer
        {
            public Frame Frame { get; private set; }

            public bool Highlighted;
            public bool Selected;

            public float X { get; internal set; }

            public float Y { get; internal set; }

            internal int PositionIndex;

            public bool Dragged { get; internal set; }

            public PointF FrameDrawPos;

            public Rect BoundingRect
            {
                get { return new Rect(new PointF(X, Y), Size); }
            }

            public SizeF Size;
            public Vector2 Scale;

            private static readonly LineStyle LineStyleExternalRect = new LineStyle(4f, LineStroke.Solid);
            private static readonly LineStyle LineStyleInternalRect = new LineStyle(2f, LineStroke.Solid);


            public FrameContainer(PointF position, SizeF size, int positionIndex)
                : this(null, position, size, positionIndex)
            {
            }

            public FrameContainer(Frame frame, PointF position, SizeF size, int positionIndex)
            {
                Frame = frame;
                Highlighted = false;
                Selected = false;
                PositionIndex = positionIndex;
                Size = size;
                if (frame != null)
                {
                    AdjustFrameScale();
                }

                X = position.X;
                Y = position.Y;
            }


            public void SetFrame(Frame frame)
            {
                Frame = frame;
                AdjustFrameScale();
            }

            public void ClearFrame()
            {
                Frame = null;
            }


            public void AdjustFrameScale()
            {
                if (Frame == null)
                {
                    return;
                }

                float scaleX = Size.Width/Frame.SpriteFrame.Width;
                float scaleY = Size.Height/Frame.SpriteFrame.Height;

                Scale.X = MathHelper.Min(scaleX, scaleY);
                Scale.Y = Scale.X;
            }


            public void Draw(Canvas2D canvas, SpriteFont font)
            {
                var drawRect = new Rect(new PointF(X, Y), Size);

                ColorU color;

                if (Highlighted && !Selected)
                {
                    color = ColorU.Red;
                }
                else if (Selected)
                {
                    color = ColorU.Yellow;
                }
                else
                {
                    color = ColorU.WhiteSmoke;
                }

                //-------------------EXTERNAL-BORDER------------------------------------------------------------
                canvas.DrawRect(drawRect.Left - LineStyleExternalRect.Width,
                                drawRect.Top - LineStyleExternalRect.Width,
                                drawRect.Width + LineStyleExternalRect.Width*2,
                                drawRect.Height + LineStyleExternalRect.Width*2,
                                LineStyleExternalRect,
                                color);

                //------------------INTERNAL-BORDER-------------------------------------------------------------
                canvas.DrawRect(drawRect, LineStyleInternalRect, ColorU.Black);

                //------------------BACKGROUND------------------------------------------------------------------                
                canvas.DrawFilledRect(drawRect, ColorU.Black.MultiplyAlpha(0.5f));


                var frameLabelRect = new Rect(new PointF(drawRect.Left,
                                                         drawRect.Top + drawRect.Height + LineStyleExternalRect.Width +
                                                         LineStyleInternalRect.Width), new SizeF(drawRect.Width, 20f));

                //------------------LABEL------------------------------------------------------------------------
                canvas.DrawFilledRect(frameLabelRect, ColorU.Black.MultiplyAlpha(0.5f));


                //------------------SPRITE-----------------------------------------------------------------------
                if (Frame != null)
                {
                    //using (canvas[0] <= new SamplerState(Configuration.TextureFilterMode, ColorU.White))
                    //{
                    canvas.DrawText(font, new Vector2(frameLabelRect.Left + 5f, frameLabelRect.Top),
                                    string.Format("{0}", Frame.Num), ColorU.AliceBlue);
                    canvas.DrawText(font, new Vector2(frameLabelRect.Left + 30f, frameLabelRect.Top),
                                    string.Format("FrameRate: {0}", 1f/Frame.FrameDuration), ColorU.Red);
                    if (!Dragged)
                    {
                        canvas.DrawSprite(drawRect.CenterX, drawRect.CenterY, Frame.SpriteFrame.Width*Scale.X,
                                          Frame.SpriteFrame.Height*Scale.Y, Frame.SpriteFrame.Sprite, ColorU.White);
                    }
                    else
                    {
                        canvas.DrawSprite(FrameDrawPos.X, FrameDrawPos.Y, Frame.SpriteFrame.Width*Scale.X,
                                          Frame.SpriteFrame.Height*Scale.Y, Frame.SpriteFrame.Sprite,
                                          ColorU.White);
                    }

                    //} 
                }
            }
        }


        public float FrameMargin;

        public Vector2 Position;

        public SizeF FrameSize;


        public Animation CurrentAnimation { get; private set; }

        /// <summary>
        /// List containing currently selected frame containers
        /// </summary>
        public List<FrameContainer> SelectedFrameContainers
        {
            get { return _frameContainersSelected; }
        }

        /// <summary>
        /// Flag indicating if Animation Strip has an animation loaded
        /// </summary>
        public bool LoadedAnimation { get; private set; }

        private readonly SpriteFont _font;

        /// <summary>
        /// List of Frame Containers of Animation Strip
        /// </summary>
        public List<FrameContainer> FrameContainers
        {
            get { return _frameContainers; }
        }

        /// <summary>
        /// Creates an AnimationStrip
        /// </summary>
        /// <param name="position">Top left position of strip</param>
        /// <param name="frameSize">Initial size of containers</param>
        /// <param name="parentRenderRegion">Rectangle representing render region of display the strip is in</param>
        public AnimationStrip(Vector2 position, SizeF frameSize, Rect parentRenderRegion)
        {
            _font = ResourcesManager.Instance.GetFont("DefaultFont");
            FrameMargin = Configuration.FrameMargin;
            FrameSize = frameSize;
            _frameContainers = new List<FrameContainer>();
            _frameContainersSelected = new List<FrameContainer>();
            LoadedAnimation = false;
            Position = position;
            _containerPositionReference = Position.X + FrameMargin;

            _intialContainerQtde = (int) (parentRenderRegion.Width/frameSize.Width);


            AddContainer(_intialContainerQtde);
        }


        /// <summary>
        /// Adds a new container
        /// </summary>
        public void AddContainer()
        {
            AddContainer(1);
        }


        public void AddContainer(int qtde)
        {
            for (int i = 0; i < qtde; i++)
            {
                var positionToAdd = new PointF(_containerPositionReference, Position.Y);
                _frameContainers.Add(new FrameContainer(positionToAdd, FrameSize, _frameContainers.Count));
                _containerPositionReference += FrameSize.Width + FrameMargin;
            }
        }

        /// <summary>
        /// Clear the frames of all containers
        /// </summary>
        public void ResetContainers()
        {
            UnselectAll();

            _frameContainerHighlighted = null;

            _originFrameContainer = null;

            foreach (var frameContainer in _frameContainers)
            {
                frameContainer.ClearFrame();
            }

            _frameContainers.Clear();

            _containerPositionReference = Position.X + FrameMargin;

            AddContainer(_intialContainerQtde);
        }


        /// <summary>
        /// Clear the frames of currently selected containers
        /// </summary>
        public void ClearSelectedContainers()
        {
            if (_frameContainersSelected.Count == 0)
            {
                return;
            }
            foreach (var frameContainer in _frameContainersSelected)
            {
                if (frameContainer.Frame != null)
                {
                    frameContainer.ClearFrame();
                }
            }

            UnselectAll();

            RefreshAnimation();
        }


        /// <summary>
        /// Removes currently selected Containers
        /// </summary>
        public void RemoveSelectedContainers()
        {
            if (_frameContainersSelected.Count == 0)
            {
                return;
            }


            foreach (var frameContainer in _frameContainersSelected)
            {
                _containerPositionReference -= FrameSize.Width + FrameMargin;

                _frameContainers.Remove(frameContainer);
            }

            UnselectAll();

            RefreshAnimation();

            RearrangeContainers();
        }


        public void AddFrames(List<Frame> frames)
        {
            if (_frameContainersSelected.Count > 0)
            {
                if (_frameContainersSelected.Count >= frames.Count)
                {
                    for (int i = 0; i < frames.Count; i++)
                    {
                        _frameContainersSelected[i].SetFrame(frames[i]);
                    }
                }
                else
                {
                    Messager.ShowMessage(Messager.Mode.Exception,
                                         string.Format(
                                             "You've selected {0} frames to add but only selected {1} containers!",
                                             frames.Count, _frameContainersSelected.Count));
                }
            }
            else
            {
                int indexToBeginAdding = -1;
                int emptyContainersOnEnd = 0;

                for (int i = _frameContainers.Count - 1; i >= 0; i--)
                {
                    if (_frameContainers[i].Frame == null)
                    {
                        emptyContainersOnEnd += 1;
                        indexToBeginAdding = i;
                    }
                    else break;
                }

                int delta = frames.Count - emptyContainersOnEnd;

                if (delta > 0)
                {
                    AddContainer(delta);
                    if (indexToBeginAdding == -1)
                    {
                        indexToBeginAdding = _frameContainers.Count - delta;
                    }
                }

                for (int i = 0; i < frames.Count; i++)
                {
                    _frameContainers[indexToBeginAdding++].SetFrame(frames[i]);
                }
            }

            UnselectAll();
            RefreshAnimation();
        }

        private void RefreshAnimation()
        {
            if (CurrentAnimation == null)
            {
                return;
            }

            CurrentAnimation.Clear();
            foreach (var frameContainer in _frameContainers)
            {
                if (frameContainer.Frame != null)
                {
                    CurrentAnimation.AddFrame(frameContainer.Frame);
                }
            }
        }

        private void RearrangeContainers()
        {
            float contWidth = FrameSize.Width;


            for (int i = 0; i < _frameContainers.Count; i++)
            {
                int shift = _frameContainers[i].PositionIndex - i;

                if (shift == 0)
                    continue;


                _frameContainers[i].X -= shift*(contWidth + FrameMargin);

                _frameContainers[i].PositionIndex -= shift;
            }
        }

        public void UnselectAll()
        {
            foreach (var frameContainer in _frameContainersSelected)
            {
                frameContainer.Selected = false;
            }

            _frameContainersSelected.Clear();
        }

        internal void UnfocusHighlightedContainer()
        {
            if (_frameContainerHighlighted == null)
            {
                return;
            }
            _frameContainerHighlighted.Highlighted = false;
            _frameContainerHighlighted = null;
        }

        public void UnloadAnimation()
        {
            ResetContainers();
            CurrentAnimation = null;
            LoadedAnimation = false;
        }


        public void SetAnimation(Animation animation)
        {
            if (animation == null)
            {
                UnloadAnimation();
                return;
            }

            ResetContainers();
            CurrentAnimation = animation;


            if (animation.Frames.Count > 0)
            {
                AddFrames(animation.Frames);
            }


            LoadedAnimation = true;
        }


        public void Update(Vector2 mousePos)
        {
            if (FrameContainers.Count > 0)
            {
                foreach (var frameContainer in FrameContainers)
                {


                    if (frameContainer.BoundingRect.Contains(mousePos))
                    {
                        if (!frameContainer.Highlighted)
                        {
                            _highLightedCount += 1;
                        }

                        frameContainer.Highlighted = true;
                        _frameContainerHighlighted = frameContainer;
                    }
                    else
                    {
                        if (frameContainer.Highlighted)
                        {
                            _highLightedCount -= 1;
                        }

                        frameContainer.Highlighted = false;
                    }
                }
                if (_highLightedCount == 0)
                {
                    _frameContainerHighlighted = null;
                }
            }
            if (Input.MousePressed(MouseButton.Left, "AnimConfig"))
            {
                if (!Input.KeyDown(Key.LeftControl, "AnimConfig") && !Input.KeyDown(Key.RightControl, "AnimConfig"))
                {
                    if (_frameContainersSelected.Count > 0)
                    {
                        foreach (var frameContainer in _frameContainersSelected)
                        {
                            frameContainer.Selected = false;
                        }
                        Application.Instance.MainWindow.PropertiesGrid.SelectedObject = null;
                        _frameContainersSelected.Clear();
                    }
                }


                if (_frameContainerHighlighted != null && !_frameContainerHighlighted.Selected)
                {
                    _frameContainerHighlighted.Selected = true;

                    Application.Instance.MainWindow.PropertiesGrid.SelectedObject = _frameContainerHighlighted.Frame;

                    _frameContainersSelected.Add(_frameContainerHighlighted);
                }
                
            }
            if (Input.MouseDown(MouseButton.Left, "AnimConfig") && _frameContainerHighlighted != null)
            {
                if (!_draggingFrame)
                {
                    if (_mouseDownCounter < 5)
                    {
                        _mouseDownCounter += 1;
                    }
                    else
                    {
                        if (_frameContainersSelected.Count > 0)
                        {
                            foreach (var frameContainer in _frameContainersSelected)
                            {
                                frameContainer.Selected = false;
                            }
                            Application.Instance.MainWindow.PropertiesGrid.SelectedObject = null;
                            _frameContainersSelected.Clear();
                        }
                        _originFrameContainer = _frameContainerHighlighted;
                        _originFrameContainer.Dragged = true;
                        _draggedFrame = _originFrameContainer.Frame;


                        _draggingFrame = true;
                    }
                }
            }
            else if (Input.MouseReleased(MouseButton.Left, "AnimConfig"))
            {
                _mouseDownCounter = 0;
                if (_draggingFrame)
                {
                    foreach (var frameContainer in _frameContainers)
                    {
                        if (frameContainer.BoundingRect.Contains(mousePos))
                        {
                            if (!frameContainer.Equals(_originFrameContainer))
                            {
                                if (frameContainer.Frame != null)
                                {
                                    _originFrameContainer.SetFrame(frameContainer.Frame);
                                }
                                else
                                {
                                    _originFrameContainer.ClearFrame();
                                }

                                frameContainer.SetFrame(_draggedFrame);

                                RefreshAnimation();
                            }
                            break;
                        }
                    }


                    _draggingFrame = false;
                    _originFrameContainer.Dragged = false;
                    _originFrameContainer = null;
                    _draggedFrame = null;
                }
            }

            if (_draggingFrame && _originFrameContainer != null)
            {
                _originFrameContainer.FrameDrawPos = mousePos.ToPointF();
            }
        }


        public void Draw(Canvas2D canvas)
        {
            for (int i = 0; i < _frameContainers.Count; i++)
            {
                var container = _frameContainers[i];

                if (_draggingFrame && _originFrameContainer.Equals(container))
                {
                    continue;
                }
                container.Draw(canvas, _font);
            }

            if (_draggingFrame)
            {
                _originFrameContainer.Draw(canvas, _font);
            }
        }

        private readonly List<FrameContainer> _frameContainers;
        private readonly List<FrameContainer> _frameContainersSelected;
        private FrameContainer _originFrameContainer;
        private Frame _draggedFrame;
        private FrameContainer _frameContainerHighlighted;
        private float _containerPositionReference;
        private int _intialContainerQtde;
        private bool _draggingFrame;
        private int _highLightedCount;
        private int _mouseDownCounter;
    }
}