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

namespace SpriteVortex.Gui
{

    public delegate void MouseButtonEventHandler(object sender,MouseButtonActionInfo info);
    public delegate void MouseMoveEventHandler(object sender, MouseMoveActionInfo info);

    public enum ControlMessage
    {
        Press,
        Release,
        Over,
        Out
    }

    

    public abstract class Control
    {
        public enum ControlState
        {
            Normal,
            Hovered,
            Pressed
        }


        public ColorU Color
        {
            get
            {
                return _color.MultiplyAlpha(_alpha/255f);
            }
            set
            {
                _color = value;
                
            }
        }

       

        public GuiManager Manager { get; private set; }


        public byte Alpha { get { return _alpha; } 
            set
            {
                _alpha = value;
                
            } 
        }

        public IList<Control> Children { get { return _children; } }

        public string Name;
        public Control Parent { get; internal set; }
        
        
        public float Left;
        public float Top;

        public float AbsoluteLeft
        {
            get
            {
                if (Parent != null)
                {
                    return Left + Parent.Left;
                }
                return Left;
            }
        }

        public float AbsoluteTop
        {
            get
            {
                if (Parent != null)
                {
                    return Top + Parent.Top;
                }
                return Top;
            }
        }

        public PointF Location
        {
            get
            {
                return new PointF(Left,Top);
            }
            set { 
                Left = value.X;
                Top = value.Y;
            }
        }
        public Size Size
        {
            get
            {
                return new Size(Width,Height);
            }
            set 
            { 
                Width = value.Width;
                Height = value.Height;
            }
        }

        public int Width;
        public int Height;

        public bool Visible { get; set; }

        public Rect AbsoluteBoundingRect
        {
            get
            {
                return Rect.FromBox(AbsoluteLeft, AbsoluteTop, Width, Height);
            }
        }
        public Rect BoundingRect { get { return Rect.FromBox(Left, Top, Width, Height); } }
        public ControlState State { get; private set; }

        protected Control(GuiManager manager)
        {
            State = ControlState.Normal;
            Alpha = 255;
            Color = ColorU.White;
            
            Manager = manager;
            _children = new List<Control>();

            Visible = true;

        }



        public void SendMessage(ControlMessage message, MouseActionInfo info)
        {

            switch (message)
            {


                case ControlMessage.Press: OnPress(info as MouseButtonActionInfo);
                    break;
                case ControlMessage.Release: OnRelease(info as MouseButtonActionInfo);
                    break;
                case ControlMessage.Over: OnHover(info as MouseMoveActionInfo);
                    break;
                case ControlMessage.Out: OnLeave(info as MouseMoveActionInfo);
                    break;
            }
        }

        public virtual void Update(float frameTime){}

        public virtual void Draw(Canvas2D canvas)
        {
            if (_children.Count <= 0) return;

            foreach (var child in _children)
            {
                child.Draw(canvas);
            }
        }

        public void Add(Control control)
        {
            if (control!=null)
            {
                if (!_children.Contains(control))
                {
                    _children.Add(control);
                    control.Parent = this;
                }
            }
        }

        public void Remove(Control control)
        {
            if (control != null)
            {
                _children.Remove(control);
            }
        }

        private readonly List<Control> _children;
        private ColorU _color;
        private byte _alpha;


        //----------------------------------------------EVENTS-------------------------------------------------------
        public event MouseMoveEventHandler MouseOver;
        public event MouseMoveEventHandler MouseLeave;
        public event MouseButtonEventHandler MousePress;
        public event MouseButtonEventHandler MouseRelease;

        protected virtual void OnHover(MouseMoveActionInfo info)
        {
            State = ControlState.Hovered;
            
            if (MouseOver != null)
            {
                MouseOver.Invoke(this,info);  
            }
        }

        protected virtual void OnLeave(MouseMoveActionInfo info)
        {
            State = ControlState.Normal;

            if (MouseLeave != null)
            {
                MouseLeave.Invoke(this, info);
            }
        }

        protected virtual void OnPress(MouseButtonActionInfo info)
        {
            State = ControlState.Pressed;

            if (MousePress != null)
            {
                MousePress.Invoke(this, info);
            }
        }

        protected virtual void OnRelease(MouseButtonActionInfo info)
        {
            State = ControlState.Normal;

            if (MouseRelease != null)
            {
                MouseRelease.Invoke(this, info);
            }
        }

        //-----------------------------------------------------------------------------------------------------------


    }
}
