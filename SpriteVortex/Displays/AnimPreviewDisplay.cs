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

using SpriteVortex.Gui;
using Vortex.Drawing;
using Vortex.Input;
using Control = System.Windows.Forms.Control;

namespace SpriteVortex
{
    public class AnimPreviewDisplay : RenderDisplay
    {
        public Animation CurrentAnimation { get; private set; }


        public AnimPreviewDisplay(string name, Control panel)
            : base(name, panel.Width, panel.Height, panel)
        {
            _dragOffSet = Vector2.Zero;

            _offSetPanelRect = new Rect(0, RenderRegion.Height - 70, 70, RenderRegion.Height);
            _originPanelRect = new Rect(RenderRegion.Width - 75, RenderRegion.Height - 75, RenderRegion.Width,
                                        RenderRegion.Height);
        }

        private void AnimPreviewDisplay_OnWheel(MouseWheelActionInfo mouseWheelActionInfo)
        {
            Camera.Zoom = MathHelper.CalculateCameraZoom(Camera, mouseWheelActionInfo.WheelDelta);
        }

        public bool IsPlaying
        {
            get { return CurrentAnimation != null ? !CurrentAnimation.Paused : false; }
        }


        public bool TogglePlayAnimation()
        {
            if (CurrentAnimation == null)
            {
                return false;
            }

            return PlayAnimation(CurrentAnimation.Paused);
        }

        public void ToggleGuiControls()
        {
            _offSetPanel.Visible = !_offSetPanel.Visible;
            _originPanel.Visible = !_originPanel.Visible;

            _drawFrameDebug = _offSetPanel.Visible;
        }

        public override void InitializeGUI()
        {
            base.InitializeGUI();


            _offSetPanel = new Panel(guiManager, _offSetPanelRect);
            _offSetPanel.SetBackgroundSprite(ResourcesManager.Instance.ResourceImageSheet.Sprites["guiPanel3"]);
            _offSetPanel.Alpha = 60;

            _btnOffSetLeft = new Button(guiManager, " ");
            _btnOffSetLeft.Left = 5;
            _btnOffSetLeft.Top = 25;
            _btnOffSetLeft.Width = 20;
            _btnOffSetLeft.Height = 20;
            _btnOffSetLeft.Alpha = 80;
            _btnOffSetLeft.MouseRelease += _btnOffSetLeft_MouseRelease;

            _btnOffSetRight = new Button(guiManager, " ");
            _btnOffSetRight.Left = 45;
            _btnOffSetRight.Top = 25;
            _btnOffSetRight.Width = 20;
            _btnOffSetRight.Height = 20;
            _btnOffSetRight.Alpha = 80;
            _btnOffSetRight.MouseRelease += _btnOffSetRight_MouseRelease;


            _btnOffSetUp = new Button(guiManager, " ");
            _btnOffSetUp.Left = 25;
            _btnOffSetUp.Top = 6;
            _btnOffSetUp.Width = 20;
            _btnOffSetUp.Height = 20;
            _btnOffSetUp.Alpha = 80;
            _btnOffSetUp.MouseRelease += _btnOffSetUp_MouseRelease;

            _btnOffSetDown = new Button(guiManager, " ");
            _btnOffSetDown.Left = 25;
            _btnOffSetDown.Top = 45;
            _btnOffSetDown.Width = 20;
            _btnOffSetDown.Height = 20;
            _btnOffSetDown.Alpha = 80;
            _btnOffSetDown.MouseRelease += _btnOffSetDown_MouseRelease;


            _offSetPanel.Add(_btnOffSetLeft);
            _offSetPanel.Add(_btnOffSetRight);
            _offSetPanel.Add(_btnOffSetUp);
            _offSetPanel.Add(_btnOffSetDown);

            _originPanel = new Panel(guiManager, _originPanelRect);
            _originPanel.SetBackgroundSprite(ResourcesManager.Instance.ResourceImageSheet.Sprites["guiPanel3"]);
            _originPanel.Alpha = 60;


            _btnOriginTopLeft = new Button(guiManager, "");
            _btnOriginTopLeft.Left = 8;
            _btnOriginTopLeft.Top = 8;
            _btnOriginTopLeft.Width = 20;
            _btnOriginTopLeft.Height = 20;
            _btnOriginTopLeft.Alpha = 80;
            _btnOriginTopLeft.MouseRelease += _btnOriginTopLeft_MouseRelease;


            _btnOriginTopMiddle = new Button(guiManager, "");
            _btnOriginTopMiddle.Left = 28;
            _btnOriginTopMiddle.Top = 8;
            _btnOriginTopMiddle.Width = 20;
            _btnOriginTopMiddle.Height = 20;
            _btnOriginTopMiddle.Alpha = 80;
            _btnOriginTopMiddle.MouseRelease += _btnOriginTopMiddle_MouseRelease;

            _btnOriginTopRight = new Button(guiManager, "");
            _btnOriginTopRight.Left = 48;
            _btnOriginTopRight.Top = 8;
            _btnOriginTopRight.Width = 20;
            _btnOriginTopRight.Height = 20;
            _btnOriginTopRight.Alpha = 80;
            _btnOriginTopRight.MouseRelease += _btnOriginTopRight_MouseRelease;

            _btnOriginMiddleLeft = new Button(guiManager, "");
            _btnOriginMiddleLeft.Left = 8;
            _btnOriginMiddleLeft.Top = 28;
            _btnOriginMiddleLeft.Width = 20;
            _btnOriginMiddleLeft.Height = 20;
            _btnOriginMiddleLeft.Alpha = 80;
            _btnOriginMiddleLeft.MouseRelease += _btnOriginMiddleLeft_MouseRelease;


            _btnOriginCenter = new Button(guiManager, "");
            _btnOriginCenter.Left = 28;
            _btnOriginCenter.Top = 28;
            _btnOriginCenter.Width = 20;
            _btnOriginCenter.Height = 20;
            _btnOriginCenter.Alpha = 80;
            _btnOriginCenter.MouseRelease += _btnOriginCenter_MouseRelease;

            _btnOriginMiddleRight = new Button(guiManager, "");
            _btnOriginMiddleRight.Left = 48;
            _btnOriginMiddleRight.Top = 28;
            _btnOriginMiddleRight.Width = 20;
            _btnOriginMiddleRight.Height = 20;
            _btnOriginMiddleRight.Alpha = 80;
            _btnOriginMiddleRight.MouseRelease += _btnOriginMiddleRight_MouseRelease;


            _btnOriginBottomLeft = new Button(guiManager, "");
            _btnOriginBottomLeft.Left = 8;
            _btnOriginBottomLeft.Top = 48;
            _btnOriginBottomLeft.Width = 20;
            _btnOriginBottomLeft.Height = 20;
            _btnOriginBottomLeft.Alpha = 80;
            _btnOriginBottomLeft.MouseRelease += _btnOriginBottomLeft_MouseRelease;


            _btnOriginBottomMiddle = new Button(guiManager, "");
            _btnOriginBottomMiddle.Left = 28;
            _btnOriginBottomMiddle.Top = 48;
            _btnOriginBottomMiddle.Width = 20;
            _btnOriginBottomMiddle.Height = 20;
            _btnOriginBottomMiddle.Alpha = 80;
            _btnOriginBottomMiddle.MouseRelease += _btnOriginBottomMiddle_MouseRelease;


            _btnOriginBottomRight = new Button(guiManager, "");
            _btnOriginBottomRight.Left = 48;
            _btnOriginBottomRight.Top = 48;
            _btnOriginBottomRight.Width = 20;
            _btnOriginBottomRight.Height = 20;
            _btnOriginBottomRight.Alpha = 80;
            _btnOriginBottomRight.MouseRelease += _btnOriginBottomRight_MouseRelease;


            _originPanel.Add(_btnOriginTopLeft);
            _originPanel.Add(_btnOriginTopMiddle);
            _originPanel.Add(_btnOriginTopRight);
            _originPanel.Add(_btnOriginMiddleLeft);
            _originPanel.Add(_btnOriginCenter);
            _originPanel.Add(_btnOriginMiddleRight);
            _originPanel.Add(_btnOriginBottomLeft);
            _originPanel.Add(_btnOriginBottomMiddle);
            _originPanel.Add(_btnOriginBottomRight);


            _offSetPanel.Visible = false;
            _originPanel.Visible = false;

            guiManager.Add(_offSetPanel);
            guiManager.Add(_originPanel);
        }

        private void _btnOffSetDown_MouseRelease(object sender, MouseButtonActionInfo info)
        {
            if (CurrentAnimation != null && CurrentAnimation.Frames.Count > 0 && CurrentAnimation.Paused)
            {
                CurrentAnimation.CurrentFrame.OffSetY += 1;
            }
        }

        private void _btnOffSetUp_MouseRelease(object sender, MouseButtonActionInfo info)
        {
            if (CurrentAnimation != null && CurrentAnimation.Frames.Count > 0 && CurrentAnimation.Paused)
            {
                CurrentAnimation.CurrentFrame.OffSetY -= 1;
            }
        }

        private void _btnOffSetRight_MouseRelease(object sender, MouseButtonActionInfo info)
        {
            if (CurrentAnimation != null && CurrentAnimation.Frames.Count > 0 && CurrentAnimation.Paused)
            {
                CurrentAnimation.CurrentFrame.OffSetX += 1;
            }
        }

        private void _btnOffSetLeft_MouseRelease(object sender, MouseButtonActionInfo info)
        {
            if (CurrentAnimation != null && CurrentAnimation.Frames.Count > 0 && CurrentAnimation.Paused)
            {
                CurrentAnimation.CurrentFrame.OffSetX -= 1;
            }
        }


        private void _btnOriginBottomRight_MouseRelease(object sender, MouseButtonActionInfo info)
        {
            if (CurrentAnimation == null)
            {
                return;
            }
            CurrentAnimation.SetOriginAllFrames(Origin.BottomRight);
        }

        private void _btnOriginBottomMiddle_MouseRelease(object sender, MouseButtonActionInfo info)
        {
            if (CurrentAnimation == null)
            {
                return;
            }
            CurrentAnimation.SetOriginAllFrames(Origin.MiddleBottom);
        }

        private void _btnOriginBottomLeft_MouseRelease(object sender, MouseButtonActionInfo info)
        {
            if (CurrentAnimation == null)
            {
                return;
            }
            CurrentAnimation.SetOriginAllFrames(Origin.BottomLeft);
        }

        private void _btnOriginMiddleRight_MouseRelease(object sender, MouseButtonActionInfo info)
        {
            if (CurrentAnimation == null)
            {
                return;
            }
            CurrentAnimation.SetOriginAllFrames(Origin.MiddleRight);
        }

        private void _btnOriginCenter_MouseRelease(object sender, MouseButtonActionInfo info)
        {
            if (CurrentAnimation == null)
            {
                return;
            }
            CurrentAnimation.SetOriginAllFrames(Origin.Center);
        }

        private void _btnOriginMiddleLeft_MouseRelease(object sender, MouseButtonActionInfo info)
        {
            if (CurrentAnimation == null)
            {
                return;
            }
            CurrentAnimation.SetOriginAllFrames(Origin.MiddleLeft);
        }

        private void _btnOriginTopRight_MouseRelease(object sender, MouseButtonActionInfo info)
        {
            if (CurrentAnimation == null)
            {
                return;
            }
            CurrentAnimation.SetOriginAllFrames(Origin.TopRight);
        }

        private void _btnOriginTopMiddle_MouseRelease(object sender, MouseButtonActionInfo info)
        {
            if (CurrentAnimation == null)
            {
                return;
            }
            CurrentAnimation.SetOriginAllFrames(Origin.MiddleTop);
        }

        private void _btnOriginTopLeft_MouseRelease(object sender, MouseButtonActionInfo info)
        {
            if (CurrentAnimation == null)
            {
                return;
            }
            CurrentAnimation.SetOriginAllFrames(Origin.TopLeft);
        }

        public bool PlayAnimation(bool play)
        {
            if (CurrentAnimation == null)
            {
                return false;
            }

            if (play)
            {
                CurrentAnimation.Play();
                return true;
            }

            CurrentAnimation.Pause();
            return false;
        }

        public void GoToLastFrame()
        {
            if (CurrentAnimation == null)
            {
                return;
            }
            if (CurrentAnimation.CurrentFrameIndex == 0 || !CurrentAnimation.Paused)
            {
                return;
            }
            CurrentAnimation.CurrentFrameIndex -= 1;
        }


        public void GoToNextFrame()
        {
            if (CurrentAnimation == null)
            {
                return;
            }
            if (CurrentAnimation.CurrentFrameIndex == CurrentAnimation.Frames.Count - 1 || !CurrentAnimation.Paused)
            {
                return;
            }
            CurrentAnimation.CurrentFrameIndex += 1;
        }

        public void ToggleOnionSkinning()
        {
            if (CurrentAnimation == null)
            {
                return;
            }

            CurrentAnimation.ToggleOnionSkin();
        }

        public void ResetAnimation()
        {
            if (CurrentAnimation == null)
            {
                return;
            }

            CurrentAnimation.Reset();
        }


        public void UnloadAnimation()
        {
            CurrentAnimation = null;
        }

        public void SetAnimation(Animation animation)
        {
            CurrentAnimation = animation;
        }

        public void FitViewToContent()
        {
            if (CurrentAnimation == null)
            {
                return;
            }


            Rect biggestFrameRect = FindFrameBoundingRect(CurrentAnimation.Frames[0]);

            foreach (var frame in CurrentAnimation.Frames)
            {
                var area = frame.SpriteFrame.Width*frame.SpriteFrame.Height;

                if (area > biggestFrameRect.Width*biggestFrameRect.Height)
                {
                    biggestFrameRect = FindFrameBoundingRect(frame);
                }
            }
            Camera.FitTo(biggestFrameRect);
        }

        public override void SetUpInputEvents()
        {
            Input.MouseListeners["AnimPreview"].OnWheel += AnimPreviewDisplay_OnWheel;
            Input.MouseListeners["AnimPreview"].OnClick += new MouseButtonActionHandler(AnimPreviewDisplay_OnClick);
        }

        private void AnimPreviewDisplay_OnClick(MouseButtonActionInfo info)
        {
            if (info.Button.Equals(MouseButton.Right))
            {
                ToggleGuiControls();
            }
        }

        public override void Update(float frameTime)
        {
            base.Update(frameTime);

            if (CurrentAnimation == null || CurrentAnimation.Frames.Count == 0)
            {
                return;
            }


            CurrentAnimation.Update(frameTime);


            if (CurrentAnimation.Paused && Focused)
            {
                if (Input.AnyOfTheseKeysPressed("AnimPreview", Key.A, Key.Left))
                {
                    CurrentAnimation.CurrentFrame.OffSetX -= 1;
                }
                if (Input.AnyOfTheseKeysPressed("AnimPreview", Key.D, Key.Right))
                {
                    CurrentAnimation.CurrentFrame.OffSetX += 1;
                }
                if (Input.AnyOfTheseKeysPressed("AnimPreview", Key.W, Key.Up))
                {
                    CurrentAnimation.CurrentFrame.OffSetY -= 1;
                }
                if (Input.AnyOfTheseKeysPressed("AnimPreview", Key.S, Key.Down))
                {
                    CurrentAnimation.CurrentFrame.OffSetY += 1;
                }

                if (Input.MousePressed(MouseButton.Left, "AnimPreview"))
                {
                    _dragOffSet = WorldMousePosition - CurrentAnimation.CurrentFrame.OffSet;
                }

                if (Input.MouseDown(MouseButton.Left, "AnimPreview"))
                {
                    CurrentAnimation.CurrentFrame.OffSetX = (int) (WorldMousePosition.X - _dragOffSet.X);
                    CurrentAnimation.CurrentFrame.OffSetY = (int) (WorldMousePosition.Y - _dragOffSet.Y);
                }
            }
        }


        public override void Render(Canvas2D canvas)
        {
            base.Render(canvas);

            if (CurrentAnimation != null && CurrentAnimation.Frames.Count > 0)
            {
                canvas.DrawText(Font, Vector2.Zero,
                                string.Format("Frame: {0}/{1} | Offset: {2};{3} | Origin:{4};{5}",
                                              CurrentAnimation.CurrentFrameIndex + 1, CurrentAnimation.Frames.Count,
                                              CurrentAnimation.CurrentFrame.OffSetX,
                                              CurrentAnimation.CurrentFrame.OffSetY,
                                              (int) CurrentAnimation.CurrentFrame.OriginX,
                                              (int) CurrentAnimation.CurrentFrame.OriginY), ColorU.White);
                using (canvas <= Camera.Transformation)
                {
                    CurrentAnimation.Draw(canvas);
                    if (_drawFrameDebug && CurrentAnimation.Frames.Count > 0)
                    {
                        Frame curFrame = CurrentAnimation.CurrentFrame;
                        canvas.DrawRect(
                            Rect.FromBox(curFrame.OffSetX - curFrame.SpriteFrame.Width/2f,
                                         curFrame.OffSetY - curFrame.SpriteFrame.Height/2, curFrame.SpriteFrame.Width,
                                         curFrame.SpriteFrame.Height), ColorU.Red);
                        canvas.DrawLine(-5, 0, 5, 0, _originMarkLineStyle, ColorU.GreenYellow);
                        canvas.DrawLine(0, -5, 0, 5, _originMarkLineStyle, ColorU.GreenYellow);
                    }
                }
            }

            guiManager.DrawControls(canvas);
        }

        private Rect FindFrameBoundingRect(Frame frame)
        {
            return new Rect(-frame.SpriteFrame.Width*0.5f,
                            -frame.SpriteFrame.Height*0.5f,
                            frame.SpriteFrame.Width*0.5f,
                            frame.SpriteFrame.Height*0.5f
                );
        }

        private Vector2 _dragOffSet;

        private Button _btnOffSetLeft;
        private Button _btnOffSetRight;
        private Button _btnOffSetUp;
        private Button _btnOffSetDown;

        private Button _btnOriginTopLeft;
        private Button _btnOriginTopMiddle;
        private Button _btnOriginTopRight;
        private Button _btnOriginMiddleLeft;
        private Button _btnOriginCenter;
        private Button _btnOriginMiddleRight;
        private Button _btnOriginBottomLeft;
        private Button _btnOriginBottomMiddle;
        private Button _btnOriginBottomRight;

        private Panel _offSetPanel;
        private Panel _originPanel;

        private LineStyle _originMarkLineStyle = new LineStyle(2, LineStroke.Solid);

        private bool _drawFrameDebug;

        private Rect _offSetPanelRect;
        private Rect _originPanelRect;
    }
}