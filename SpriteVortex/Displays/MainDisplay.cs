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
using System.Drawing;
using SpriteVortex.Gui;
using Vortex.Drawing;
using Vortex.Input;
using Control = System.Windows.Forms.Control;

namespace SpriteVortex
{

    #region ==========================States==================================

    public enum StateName
    {
        Idle,
        SelectingSprite,
        SpriteMarkuping,
        ChanginSpriteMarkup,
        MovingCamera,
        ZoomingCamera,
        ColorPicking
    }


    public abstract class State
    {
        public StateName Name { get; protected set; }

        public static MainDisplayStateHandler Handler;

        protected bool Active;


        public virtual void OnStart(MainDisplay owner)
        {
            Active = true;
        }

        public virtual void OnUpdate(MainDisplay owner)
        {
            if (CheckStateChange(owner))
            {
                Active = false;
            }
        }

        public virtual void OnDraw(Canvas2D canvas)
        {
        }

        public virtual void OnExit(MainDisplay owner)
        {
        }

        protected abstract bool CheckStateChange(MainDisplay owner);
    }

    public class IdleState : State
    {
        private static IdleState _instance;


        public static IdleState Instance
        {
            get { return _instance ?? (_instance = new IdleState()); }
        }

        private IdleState()
        {
            Name = StateName.Idle;
        }


        public override void OnStart(MainDisplay owner)
        {
            base.OnStart(owner);


            if (owner.TopPanelHidden)
            {
                owner.ShowTopPanel();
            }
        }


        public override void OnUpdate(MainDisplay owner)
        {
            base.OnUpdate(owner);

            if (Active && !owner.MouseIsOverGUI)
            {
                Vector2 mousePos = owner.WorldMousePosition;

                if (owner.CurrentSpriteSheet.Frames.Count > 0)
                {
                    foreach (SpriteSheetFrame frame in owner.CurrentSpriteSheet.Frames)
                    {
                        if (frame.RelativeBoundingRect.Contains(mousePos.X, mousePos.Y))
                        {
                            owner.CurrentSpriteSheet.HighLightFrame(frame, true);
                        }
                        else
                        {
                            owner.CurrentSpriteSheet.HighLightFrame(frame, false);
                        }
                    }
                }
            }
        }

        protected override bool CheckStateChange(MainDisplay owner)
        {
            if (owner.MouseIsOverGUI)
            {
                return false;
            }

            if (Input.ControlPressed(Configuration.SelectSpriteControl, "MainDisplay"))
            {
                Handler.ChangeState(SelectingSpriteState.Instance);
                return true;
            }
            if (Input.ControlPressed(Configuration.SpriteMarkUpControl, "MainDisplay") && owner.MouseIsInsideSpriteSheet)
            {
                if (owner.CurrentSpriteSheet.HighLightedFrame == null)
                {
                    Handler.ChangeState(SpriteMarkupingState.Instance);
                }
                else
                {
                    Handler.ChangeState(ChangingSpriteMarkupState.Instance);
                }

                return true;
            }
            if (Input.ControlPressed(Configuration.DragCameraControl, "MainDisplay"))
            {
                Handler.ChangeState(MovingCameraState.Instance);
                return true;
            }
            if (Input.ControlPressed(Configuration.ViewZoomControl, "MainDisplay"))
            {
                Handler.ChangeState(ZoomingCameraState.Instance);
                return true;
            }


            return false;
        }
    }

    public class SelectingSpriteState : State
    {
        private static SelectingSpriteState _instance;

        public static SelectingSpriteState Instance
        {
            get { return _instance ?? (_instance = new SelectingSpriteState()); }
        }

        private SelectingSpriteState()
        {
            Name = StateName.SelectingSprite;
        }


        public Rect SelectRect;
        private Vector2 _initialPressedPos;

        public override void OnStart(MainDisplay owner)
        {
            base.OnStart(owner);

            _initialPressedPos = owner.WorldMousePosition;

            // Add the frame that we have selected clicking with the mouse
            if (owner.CurrentSpriteSheet.HighLightedFrame != null)
            {
                var selected = owner.CurrentSpriteSheet.HighLightedFrame.Selected;
                owner.CurrentSpriteSheet.SelectFrame(owner.CurrentSpriteSheet.HighLightedFrame, !selected);

                if (!selected)
                {
                    Application.Instance.MainWindow.PropertiesGrid.SelectedObject =
                        owner.CurrentSpriteSheet.HighLightedFrame;
                }
            }
            else
            {
                owner.CurrentSpriteSheet.UnselectAllFrames();
            }
        }

        public override void OnExit(MainDisplay owner)
        {
            SelectRect = Rect.Empty;
            _initialPressedPos = Vector2.Zero;
        }

        public override void OnUpdate(MainDisplay owner)
        {
            base.OnUpdate(owner);

            if (Active)
            {
                SelectRect.Left = _initialPressedPos.X;
                SelectRect.Top = _initialPressedPos.Y;
                SelectRect.Right = owner.WorldMousePosition.X;
                SelectRect.Bottom = owner.WorldMousePosition.Y;

                if (SelectRect.Width != 0 && SelectRect.Height != 0)
                {
                    owner.HideTopPanel();


                    var oldDraggingRectangle = SelectRect;
                    SelectRect.Left = MathHelper.Min(oldDraggingRectangle.Left, SelectRect.Right);
                    SelectRect.Right = MathHelper.Max(oldDraggingRectangle.Left, SelectRect.Right);
                    SelectRect.Top = MathHelper.Min(oldDraggingRectangle.Top, SelectRect.Bottom);
                    SelectRect.Bottom = MathHelper.Max(oldDraggingRectangle.Top, SelectRect.Bottom);

                    if (owner.CurrentSpriteSheet.Frames.Count > 0)
                    {
                        foreach (var frame in owner.CurrentSpriteSheet.Frames)
                        {
                            owner.CurrentSpriteSheet.SelectFrame(frame,
                                                                 SelectRect.Intersects(frame.RelativeBoundingRect));
                        }
                    }
                }
            }
        }

        public override void OnDraw(Canvas2D canvas)
        {
            if (SelectRect.Width > 0 && SelectRect.Height > 0)
            {
                canvas.DrawRect(SelectRect, new LineStyle(2f, LineStroke.Dashed), ColorU.Orange);
            }
        }

        protected override bool CheckStateChange(MainDisplay owner)
        {
            if (Input.ControlReleased(Configuration.SelectSpriteControl, "MainDisplay"))
            {
                Handler.ChangeState(IdleState.Instance);
                return true;
            }
            return false;
        }
    }

    public class SpriteMarkupingState : State
    {
        private static SpriteMarkupingState _instance;

        public static SpriteMarkupingState Instance
        {
            get { return _instance ?? (_instance = new SpriteMarkupingState()); }
        }

        private SpriteMarkupingState()
        {
            Name = StateName.SpriteMarkuping;
        }


        public Rect SelectRect;
        private Vector2 _initialPressedPos;

        public override void OnStart(MainDisplay owner)
        {
            base.OnStart(owner);
            _initialPressedPos = owner.WorldMousePosition;
        }

        public override void OnUpdate(MainDisplay owner)
        {
            base.OnUpdate(owner);

            if (Active)
            {
                SelectRect.Left = _initialPressedPos.X;
                SelectRect.Top = _initialPressedPos.Y;
                SelectRect.Right = owner.WorldMousePosition.X;
                SelectRect.Bottom = owner.WorldMousePosition.Y;

                var oldDraggingRectangle = SelectRect;


                SelectRect.Left = MathHelper.Min(oldDraggingRectangle.Left, oldDraggingRectangle.Right);
                SelectRect.Right = MathHelper.Max(oldDraggingRectangle.Left, oldDraggingRectangle.Right);
                SelectRect.Top = MathHelper.Min(oldDraggingRectangle.Top, oldDraggingRectangle.Bottom);
                SelectRect.Bottom = MathHelper.Max(oldDraggingRectangle.Top, oldDraggingRectangle.Bottom);


                SelectRect.Left = MathHelper.Max(owner.CurrentSpriteSheet.BoundingRect.Left,
                                                 SelectRect.Left);
                SelectRect.Right = MathHelper.Min(owner.CurrentSpriteSheet.BoundingRect.Right,
                                                  SelectRect.Right);

                SelectRect.Top = MathHelper.Max(owner.CurrentSpriteSheet.BoundingRect.Top,
                                                SelectRect.Top);
                SelectRect.Bottom = MathHelper.Min(owner.CurrentSpriteSheet.BoundingRect.Bottom,
                                                   SelectRect.Bottom);
            }
        }

        public override void OnDraw(Canvas2D canvas)
        {
            canvas.DrawRect(SelectRect, new LineStyle(2f, LineStroke.Dashed), ColorU.Blue);
        }

        protected override bool CheckStateChange(MainDisplay owner)
        {
            if (Input.ControlReleased(Configuration.SpriteMarkUpControl, "MainDisplay"))
            {
                Handler.ChangeState(IdleState.Instance);
                return true;
            }
            return false;
        }

        public override void OnExit(MainDisplay owner)
        {
            if (SelectRect.Width > 0 && SelectRect.Height > 0)
            {
                var foundRect = Application.Instance.ShrinkFrameSelect(owner.CurrentSpriteSheet.Texture,
                                                                       SelectRect, 0.12);

                if (foundRect.Width > 0 && foundRect.Height > 0)
                {
                    owner.CurrentSpriteSheet.AddFrame(foundRect);

                    owner.CurrentSpriteSheet.SortFrames();
                }
            }

            SelectRect = Rect.Empty;
            _initialPressedPos = Vector2.Zero;
        }
    }

    public class ChangingSpriteMarkupState : State
    {
        [Flags]
        private enum FrameRectModifyMode
        {
            None = 0x0,
            ResizeLeft = 0x1,
            ResizeRight = 0x2,
            ResizeTop = 0x4,
            ResizeBottom = 0x8,
            Move = 0x10
        }

        private static ChangingSpriteMarkupState _instance;

        public static ChangingSpriteMarkupState Instance
        {
            get { return _instance ?? (_instance = new ChangingSpriteMarkupState()); }
        }

        private ChangingSpriteMarkupState()
        {
            Name = StateName.ChanginSpriteMarkup;
        }

        private Vector2 _pressedPosition;
        private SpriteSheetFrame _frameBeingChanged;
        private Rect _frameRect;
        private Vector2 _deltaPressed;

        private FrameRectModifyMode _modifyMode;

        public override void OnStart(MainDisplay owner)
        {
            base.OnStart(owner);

            _pressedPosition = owner.WorldMousePosition;

            _frameBeingChanged = owner.CurrentSpriteSheet.HighLightedFrame;

            _frameRect = _frameBeingChanged.RelativeBoundingRect;


            _deltaPressed = new Vector2(_pressedPosition.X - _frameBeingChanged.RelativeBoundingRect.CenterX,
                                        _pressedPosition.Y - _frameBeingChanged.RelativeBoundingRect.CenterY);


            if (_pressedPosition.X > _frameRect.Left + 5 && _pressedPosition.X < _frameRect.Right - 5 &&
                _pressedPosition.Y > _frameRect.Top + 5 && _pressedPosition.Y < _frameRect.Bottom - 5)
            {
                _modifyMode = FrameRectModifyMode.Move;
            }
            else
            {
                if (_pressedPosition.X < _frameRect.Left + 5)
                {
                    _modifyMode |= FrameRectModifyMode.ResizeLeft;
                }
                else if (_pressedPosition.X > _frameRect.Right - 5)
                {
                    _modifyMode |= FrameRectModifyMode.ResizeRight;
                }
                if (_pressedPosition.Y < _frameRect.Top + 5)
                {
                    _modifyMode |= FrameRectModifyMode.ResizeTop;
                }
                else if (_pressedPosition.Y > _frameRect.Bottom - 5)
                {
                    _modifyMode |= FrameRectModifyMode.ResizeBottom;
                }
            }
        }


        public override void OnExit(MainDisplay owner)
        {
            if (_frameRect.Width > 0 && _frameRect.Height > 0)
            {
                var foundRect = Application.Instance.ShrinkFrameSelect(owner.CurrentSpriteSheet.Texture,
                                                                       _frameRect, 0.12);

                if (foundRect.Width > 0 && foundRect.Height > 0)
                {
                    owner.CurrentSpriteSheet.ChangeFrameRect(_frameBeingChanged, foundRect);
                    Application.Instance.OnSpriteSheetFrameRectChanged(_frameBeingChanged);
                }
            }

            _frameBeingChanged = null;
            _frameRect = Rect.Empty;
            _deltaPressed = Vector2.Zero;
            _modifyMode = FrameRectModifyMode.None;
        }

        public override void OnUpdate(MainDisplay owner)
        {
            base.OnUpdate(owner);

            if (Active)
            {
                float mouseX = owner.WorldMousePosition.X;
                float mouseY = owner.WorldMousePosition.Y;


                if (_modifyMode == FrameRectModifyMode.Move)
                {
                    _frameRect.CenterX = mouseX - _deltaPressed.X;
                    _frameRect.CenterY = mouseY - _deltaPressed.Y;
                }

                if ((_modifyMode & FrameRectModifyMode.ResizeLeft) == FrameRectModifyMode.ResizeLeft)
                {
                    _frameRect.Left = mouseX;
                }
                else if ((_modifyMode & FrameRectModifyMode.ResizeRight) == FrameRectModifyMode.ResizeRight)
                {
                    _frameRect.Right = mouseX;
                }

                if ((_modifyMode & FrameRectModifyMode.ResizeTop) == FrameRectModifyMode.ResizeTop)
                {
                    _frameRect.Top = mouseY;
                }
                else if ((_modifyMode & FrameRectModifyMode.ResizeBottom) == FrameRectModifyMode.ResizeBottom)
                {
                    _frameRect.Bottom = mouseY;
                }


                var oldDraggingRectangle = _frameRect;


                _frameRect.Left = MathHelper.Min(oldDraggingRectangle.Left, oldDraggingRectangle.Right);
                _frameRect.Right = MathHelper.Max(oldDraggingRectangle.Left, oldDraggingRectangle.Right);
                _frameRect.Top = MathHelper.Min(oldDraggingRectangle.Top, oldDraggingRectangle.Bottom);
                _frameRect.Bottom = MathHelper.Max(oldDraggingRectangle.Top, oldDraggingRectangle.Bottom);


                _frameRect.Left = MathHelper.Max(owner.CurrentSpriteSheet.BoundingRect.Left,
                                                 _frameRect.Left);
                _frameRect.Right = MathHelper.Min(owner.CurrentSpriteSheet.BoundingRect.Right,
                                                  _frameRect.Right);

                _frameRect.Top = MathHelper.Max(owner.CurrentSpriteSheet.BoundingRect.Top,
                                                _frameRect.Top);
                _frameRect.Bottom = MathHelper.Min(owner.CurrentSpriteSheet.BoundingRect.Bottom,
                                                   _frameRect.Bottom);
            }
        }

        public override void OnDraw(Canvas2D canvas)
        {
            canvas.DrawRect(_frameRect, new LineStyle(2f, LineStroke.Solid), ColorU.Red);
        }

        protected override bool CheckStateChange(MainDisplay owner)
        {
            if (Input.ControlReleased(Configuration.SpriteMarkUpControl, "MainDisplay"))
            {
                Handler.ChangeState(IdleState.Instance);
                return true;
            }
            return false;
        }
    }

    public class MovingCameraState : State
    {
        private bool hidBottomPanel;
        private bool hidTopPanel;

        private Vector2 initialPressedPos;

        private static MovingCameraState _instance;

        public static MovingCameraState Instance
        {
            get { return _instance ?? (_instance = new MovingCameraState()); }
        }

        private MovingCameraState()
        {
            Name = StateName.MovingCamera;
        }


        public override void OnStart(MainDisplay owner)
        {
            initialPressedPos = owner.WorldMousePosition;


            base.OnStart(owner);
        }

        public override void OnExit(MainDisplay owner)
        {
            if (hidBottomPanel)
            {
                owner.ShowBottomPanel();
                hidBottomPanel = false;
            }

            if (hidTopPanel)
            {
                owner.ShowTopPanel();
                hidTopPanel = false;
            }


            base.OnExit(owner);
        }

        public override void OnUpdate(MainDisplay owner)
        {
            base.OnUpdate(owner);

            if (Active)
            {
                Vector2 mouseDelta = owner.WorldMousePosition - initialPressedPos;

                if (mouseDelta.X != 0 || mouseDelta.Y != 0)
                {
                    if (!owner.BottomPanelHidden)
                    {
                        owner.HideBottomPanel();
                        hidBottomPanel = true;
                    }
                    if (!owner.TopPanelHidden)
                    {
                        owner.HideTopPanel();
                        hidTopPanel = true;
                    }
                }

                mouseDelta = mouseDelta.Negative;

                owner.Camera.Move(mouseDelta);
            }
        }

        protected override bool CheckStateChange(MainDisplay owner)
        {
            if (Input.ControlReleased(Configuration.DragCameraControl, "MainDisplay"))
            {
                Handler.ChangeState(IdleState.Instance);
                return true;
            }
            return false;
        }
    }

    public class ZoomingCameraState : State
    {
        private float lastMouseY;

        private static ZoomingCameraState _instance;

        public static ZoomingCameraState Instance
        {
            get { return _instance ?? (_instance = new ZoomingCameraState()); }
        }

        private ZoomingCameraState()
        {
            Name = StateName.ZoomingCamera;
        }


        public override void OnStart(MainDisplay owner)
        {
            lastMouseY = owner.WorldMousePosition.Y;

            base.OnStart(owner);
        }

        public override void OnExit(MainDisplay owner)
        {
            lastMouseY = 0;

            base.OnExit(owner);
        }

        public override void OnUpdate(MainDisplay owner)
        {
            base.OnUpdate(owner);

            if (Active)
            {
                float delta = owner.WorldMousePosition.Y - lastMouseY;

                if (delta >= 0.01f || delta <= -0.01f)
                {
                    owner.Camera.ZoomToWorldPoint(owner.WorldMousePosition.X, owner.WorldMousePosition.Y,
                                                  MathHelper.CalculateCameraZoomFactor(owner.Camera, -(int) delta));
                }

                lastMouseY = owner.WorldMousePosition.Y;
            }
        }

        protected override bool CheckStateChange(MainDisplay owner)
        {
            if (Input.ControlReleased(Configuration.ViewZoomControl, "MainDisplay"))
            {
                Handler.ChangeState(IdleState.Instance);
                return true;
            }
            return false;
        }
    }

    public class ColorPickingState : State
    {
        private static ColorPickingState _instance;

        public static ColorPickingState Instance
        {
            get { return _instance ?? (_instance = new ColorPickingState()); }
        }

        private ColorPickingState()
        {
            Name = StateName.ColorPicking;
        }

        public override void OnExit(MainDisplay owner)
        {
            Vector2 mousePos = owner.WorldMousePosition;

            var positionClicked = new Vector2(mousePos.X - owner.CurrentSpriteSheet.BoundingRect.Left,
                                              mousePos.Y - owner.CurrentSpriteSheet.BoundingRect.Top).ToPoint();

            owner.CurrentSpriteSheet.ClearColor(positionClicked.X, positionClicked.Y);
        }


        protected override bool CheckStateChange(MainDisplay owner)
        {
            if (Input.MousePressed(MouseButton.Left, "MainDisplay"))
            {
                Handler.ChangeState(IdleState.Instance);
                return true;
            }
            return false;
        }
    }

    #endregion ===============================================================

    public class MainDisplay : RenderDisplay
    {
        public SpriteSheet CurrentSpriteSheet { get; private set; }

        public bool MouseIsInsideSpriteSheet
        {
            get { return CurrentSpriteSheet.BoundingRect.Contains(WorldMousePosition); }
        }

        public bool GridEnabled { get; set; }


        public override bool Focused
        {
            get { return base.Focused; }
            set
            {
                base.Focused = value;
                if (!base.Focused)
                {
                    UnFocusHighlightedFrame();
                }
            }
        }


        public MainDisplay(string name, Control panel) : base(name, panel.Width, panel.Height, panel)
        {
            Debug = false;
            BottomPanelHidden = true;
        }

        private void MainDisplay_OnWheel(MouseWheelActionInfo mouseWheelActionInfo)
        {
            Camera.ZoomToWorldPoint(WorldMousePosition.X, WorldMousePosition.Y,
                                    MathHelper.CalculateCameraZoomFactor(Camera, mouseWheelActionInfo.WheelDelta));
        }

        public void InitializeStateMachine()
        {
            _stateHandler = new MainDisplayStateHandler();
            State.Handler = _stateHandler;

            _stateHandler.Init(this, IdleState.Instance);
        }

        public override void InitializeGUI()

        {
            base.InitializeGUI();

            _topPanel = new DynamicPanel(guiManager,
                                         Rect.FromBox(topPanelPos.X, topPanelPos.Y, topPanelSize.Width,
                                                      topPanelSize.Height));
            _topPanel.SetBackgroundSprite(ResourcesManager.Instance.ResourceImageSheet.Sprites["guiPanel3"]);

            _topPanel.Alpha = 200;
            _topPanel.MouseRelease += TopPanelMouseRelease;
            _topPanel.MouseOver += TopPanelMouseOver;


            _btnAlphaCut = new Button(guiManager, "Alpha Cut")
                               {
                                   Left = 15,
                                   Top = 20,
                                   Width = 100,
                                   Height = 30,
                               };


            _btnAlphaCut.MouseRelease += _btnAlphaCut_MouseRelease;


            _btnSelectAllFrames = new Button(guiManager, "Select all Frames")
                                      {
                                          Left = _btnAlphaCut.Left + _btnAlphaCut.Width + 5,
                                          Top = 20,
                                          Width = 130,
                                          Height = 30
                                      };


            _btnSelectAllFrames.MouseRelease += BtnSelectAllFramesMouseRelease;


            _btnResetView = new Button(guiManager, "Reset View")
                                {
                                    Left = _btnSelectAllFrames.Left + _btnSelectAllFrames.Width + 5,
                                    Top = 20,
                                    Width = 110,
                                    Height = 30
                                };


            _btnResetView.MouseRelease += BtnResetViewMouseRelease;


            _btnFitViewToContent = new Button(guiManager, "Fit View")
                                       {
                                           Left = _btnResetView.Left + _btnResetView.Width + 5,
                                           Top = 20,
                                           Width = 100,
                                           Height = 30
                                       };


            _btnFitViewToContent.MouseRelease += BtnFitViewToContentMouseRelease;

            _btnPickColorToClear = new Button(guiManager, "Pick Color to Clear")
                                       {
                                           Left = _btnFitViewToContent.Left + _btnFitViewToContent.Width + 5,
                                           Top = 20,
                                           Width = 125,
                                           Height = 30
                                       };


            _btnPickColorToClear.MouseRelease += _btnPickColorToClear_MouseRelease;


            _topPanel.Add(_btnAlphaCut);
            _topPanel.Add(_btnSelectAllFrames);
            _topPanel.Add(_btnResetView);
            _topPanel.Add(_btnFitViewToContent);
            _topPanel.Add(_btnPickColorToClear);


            _bottomPanel = new DynamicPanel(guiManager,
                                            Rect.FromBox(bottomPanelPos.X, bottomPanelPos.Y, bottomPanelSize.Width,
                                                         bottomPanelSize.Height));
            _bottomPanel.SetBackgroundSprite(ResourcesManager.Instance.ResourceImageSheet.Sprites["guipanel"]);
            _bottomPanel.Color = ColorU.Black;
            _bottomPanel.Alpha = 128;
            _bottomPanel.MouseOver += BottomPanelMouseOver;


            _btnAddFramesToAnimation = new Button(guiManager, "Add Selected to Animation")
                                           {
                                               Left = 10,
                                               Top = 10,
                                               Width = 176,
                                               Height = 30
                                           };

            _btnAddFramesToAnimation.MouseRelease += _btnAddFramesToAnimation_MouseRelease;


            _bottomPanel.Add(_btnAddFramesToAnimation);


            guiManager.Add(_topPanel);
            guiManager.Add(_bottomPanel);
        }


        public override void SetUpInputEvents()
        {
            Input.MouseListeners["MainDisplay"].OnWheel += MainDisplay_OnWheel;
        }


        private void BtnFitViewToContentMouseRelease(object sender, MouseButtonActionInfo info)
        {
            FitViewToContent();
        }


        private void _btnPickColorToClear_MouseRelease(object sender, MouseButtonActionInfo info)
        {
            _stateHandler.ChangeState(ColorPickingState.Instance);
        }


        private void BtnResetViewMouseRelease(object sender, MouseButtonActionInfo info)
        {
            Camera.Reset();
        }


        private void BtnSelectAllFramesMouseRelease(object sender, MouseButtonActionInfo info)
        {
            SelectAllFrames();
        }


        public void RefreshGui()
        {
            _topPanel.Location = topPanelPos.ToPointF();
            _topPanel.Size = topPanelSize;


            _btnAlphaCut.Left = 15;

            _btnSelectAllFrames.Left = _btnAlphaCut.Left + _btnAlphaCut.Width + 5;
            _btnResetView.Left = _btnSelectAllFrames.Left + _btnSelectAllFrames.Width + 5;
            _btnFitViewToContent.Left = _btnResetView.Left + _btnResetView.Width + 5;
            _btnPickColorToClear.Left = _btnFitViewToContent.Left + _btnFitViewToContent.Width + 5;

            _bottomPanel.Location = bottomPanelPos.ToPointF();

            if (!BottomPanelHidden)
            {
                _bottomPanel.Location = new PointF(_bottomPanel.Left, _bottomPanel.Top - 60);
            }
        }

        private void _btnAlphaCut_MouseRelease(object sender, MouseButtonActionInfo info)
        {
            Application.Instance.CutSpriteSheetByAlpha();
        }


        private void BottomPanelMouseOver(object sender, MouseMoveActionInfo info)
        {
            UnFocusHighlightedFrame();
        }

        private void TopPanelMouseOver(object sender, MouseMoveActionInfo info)
        {
            UnFocusHighlightedFrame();
        }

        private void _btnAddFramesToAnimation_MouseRelease(object sender, MouseButtonActionInfo info)
        {
            var spriteFrames = Application.Instance.MakeSpriteFrames(CurrentSpriteSheet.SelectedFrames);
            Application.Instance.AddFramesToAnimation(spriteFrames);
        }

        private void TopPanelMouseRelease(object sender, MouseButtonActionInfo info)
        {
            ToggleShowTopPanel();
        }

        public void ShowBottomPanel()
        {
            if (!BottomPanelHidden || _bottomPanel.Moving)
            {
                return;
            }

            _bottomPanel.MoveVertically(-bottomPanelSize.Height);
            BottomPanelHidden = false;
        }

        public void HideBottomPanel()
        {
            if (BottomPanelHidden || _bottomPanel.Moving)
            {
                return;
            }

            _bottomPanel.MoveVertically(bottomPanelSize.Height);
            BottomPanelHidden = true;
        }

        public void ShowTopPanel()
        {
            if (!TopPanelHidden || _topPanel.Moving)
            {
                return;
            }

            _topPanel.MoveVertically(bottomPanelSize.Height);
            TopPanelHidden = false;
        }

        public void HideTopPanel()
        {
            if (TopPanelHidden || _topPanel.Moving)
            {
                return;
            }

            _topPanel.MoveVertically(-bottomPanelSize.Height);
            TopPanelHidden = true;
        }

        public void ToggleShowTopPanel()
        {
            if (_topPanel.Moving)
            {
                return;
            }
            if (!TopPanelHidden)
            {
                _topPanel.MoveVertically(-bottomPanelSize.Height);
                TopPanelHidden = true;
            }
            else
            {
                _topPanel.MoveVertically(bottomPanelSize.Height);
                TopPanelHidden = false;
            }
        }

        public void SetSpriteSheet(SpriteSheet spriteSheet)
        {
            CurrentSpriteSheet = spriteSheet;
            Camera.Reset();
        }

        public void UnloadSpriteSheet()
        {
            CurrentSpriteSheet = null;
        }

        public void SelectAllFrames()
        {
            if (CurrentSpriteSheet != null && CurrentSpriteSheet.Frames.Count > 0)
            {
                foreach (SpriteSheetFrame frame in CurrentSpriteSheet.Frames)
                {
                    CurrentSpriteSheet.SelectFrame(frame, true);
                }
            }
        }

        private void UnFocusHighlightedFrame()
        {
            if (CurrentSpriteSheet == null || CurrentSpriteSheet.HighLightedFrame == null) return;


            CurrentSpriteSheet.HighLightedFrame.Highlighted = false;

            CurrentSpriteSheet.HighLightedFrame = null;
        }

        public void FitViewToContent()
        {
            if (CurrentSpriteSheet == null)
            {
                return;
            }

            Rect contentRect = CurrentSpriteSheet.BoundingRect;

            Camera.FitTo(contentRect);
        }

        //------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------


        public override void Update(float frameTime)
        {
            base.Update(frameTime);


            if (CurrentSpriteSheet == null)
            {
                return;
            }

            _thereAreSelectedFramesNow = CurrentSpriteSheet.SelectedFrames.Count > 0;

            if (_thereAreSelectedFramesNow && !_thereWereSelectedFramesLastTime)
            {
                ShowBottomPanel();
            }
            else if (!_thereAreSelectedFramesNow && _thereWereSelectedFramesLastTime)
            {
                HideBottomPanel();
            }


            if (_bottomPanel.Top <= bottomPanelPos.Y - bottomPanelSize.Height)
            {
                _bottomPanel.Top = bottomPanelPos.Y - bottomPanelSize.Height;
            }

            if (_bottomPanel.Top >= bottomPanelPos.Y + bottomPanelSize.Height)
            {
                _bottomPanel.Top = bottomPanelPos.Y + bottomPanelSize.Height;
            }


            if (_topPanel.Top <= topPanelPos.Y - topPanelSize.Height)
            {
                _topPanel.Top = topPanelPos.Y - topPanelSize.Height;
            }

            if (_topPanel.Top >= topPanelPos.Y)
            {
                _topPanel.Top = topPanelPos.Y;
            }

            //-------------------------------------------MOUSE EVENTS---------------------------------------------------------
            //----------------------------------------------------------------------------------------------------------------
            //----------------------------------------------------------------------------------------------------------------


            if (MouseIsInside)
            {
                _stateHandler.Update();
            }


            //----KEYBOARD EVENTS-------------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------------------------------
            if (Focused)
            {
                if (Input.AnyOfTheseKeysDown("MainDisplay", Key.A, Key.Left))
                {
                    Camera.Move(-Configuration.CameraSpeed*frameTime, 0);
                }

                if (Input.AnyOfTheseKeysDown("MainDisplay", Key.D, Key.Right))
                {
                    Camera.Move(Configuration.CameraSpeed*frameTime, 0);
                }


                if (Input.AnyOfTheseKeysDown("MainDisplay", Key.W, Key.Up))
                {
                    Camera.Move(0, -Configuration.CameraSpeed*frameTime);
                }

                if (Input.AnyOfTheseKeysDown("MainDisplay", Key.S, Key.Down))
                {
                    Camera.Move(0, Configuration.CameraSpeed*frameTime);
                }


                if (Input.KeyPressed(Key.Delete, "MainDisplay"))
                {
                    if (CurrentSpriteSheet.SelectedFrames.Count == 0)
                    {
                        return;
                    }

                    var removedIds = CurrentSpriteSheet.RemoveSelectedFrames();

                    Application.Instance.OnSpriteSheetFrameRectDeleted(removedIds);

                    Application.Instance.MainWindow.PropertiesGrid.SelectedObject = null;
                }
            }

            _thereWereSelectedFramesLastTime = _thereAreSelectedFramesNow;
        }


        public override void Render(Canvas2D canvas)
        {
            base.Render(canvas);


            if (CurrentSpriteSheet != null)
            {
                using (canvas <= Camera.Transformation)
                {
                    Rect boundingRect = Rect.FromBox(CurrentSpriteSheet.BoundingRect.LeftTop.Floored,
                                                     new Vector2(CurrentSpriteSheet.BoundingRect.Width,
                                                                 CurrentSpriteSheet.BoundingRect.Height));
                    canvas.DrawRect(boundingRect, new LineStyle(2f, LineStroke.Solid), ColorU.Red);

                    canvas.DrawSprite(Vector2.Zero, 0f, CurrentSpriteSheet.Texture.ToSprite(), ColorU.White);


                    foreach (SpriteSheetFrame frame in CurrentSpriteSheet.Frames)
                    {
                        ColorU color;


                        if (frame.Highlighted && !frame.Selected)
                        {
                            color = Configuration.HoverFrameRectColor;
                        }
                        else if (frame.Selected)
                        {
                            color = Configuration.SelectedFrameRectColor;
                        }
                        else
                        {
                            color = Configuration.FrameRectColor;
                        }

                        Rect frameBoundingRect = frame.RelativeBoundingRect;
                        canvas.DrawRect(frameBoundingRect, new LineStyle(2f, LineStroke.Solid), color);
                    }

                    _stateHandler.Draw(canvas);


                    //if (GridEnabled && Camera.Zoom > 4f)
                    //{
                    //    Point start = Camera.ScreenToWorld(Vector2.Zero).ToPoint();

                    //    for (int i = start.X; i < RenderRegion.Width; i++)
                    //    {
                    //        canvas.DrawLine(i, start.Y, i, RenderRegion.Height, ColorU.Gray.MultiplyAlpha(0.5f));
                    //    }
                    //    for (int i = start.Y; i < RenderRegion.Height; i++)
                    //    {
                    //        canvas.DrawLine(start.X, i, RenderRegion.Width, i, ColorU.Gray.MultiplyAlpha(0.5f));
                    //    }
                    //}
                }
            }

            guiManager.DrawControls(canvas);
        }


        private MainDisplayStateHandler _stateHandler;


        public bool TopPanelHidden;
        public bool BottomPanelHidden;


        private DynamicPanel _topPanel;
        private DynamicPanel _bottomPanel;
        private Button _btnAlphaCut;
        private Button _btnAddFramesToAnimation;
        private Button _btnResetView;
        private Button _btnPickColorToClear;
        private Button _btnFitViewToContent;
        private Button _btnSelectAllFrames;
        private bool _thereAreSelectedFramesNow;
        private bool _thereWereSelectedFramesLastTime;

        private Size topPanelSize
        {
            get { return new Size((int) RenderRegion.Width, 75); }
        }

        private Size bottomPanelSize
        {
            get { return new Size(200, 50); }
        }

        private Vector2 topPanelPos
        {
            get { return new Vector2(0, -6); }
        }

        private Vector2 bottomPanelPos
        {
            get { return new Vector2(RenderRegion.HalfWidth - bottomPanelSize.Width/2f, RenderRegion.Height); }
        }
    }
}