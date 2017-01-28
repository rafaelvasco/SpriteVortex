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
    public class GuiManager
    {
        private readonly RenderDisplay _parent;

        public bool MouseIsOverGUI { get; private set; }

        private readonly List<Control> _rootControls;

        public GuiManager(RenderDisplay parent)
        {
            _parent = parent;
            Input.MouseListeners[_parent.Name].OnButtonUp += ProcessMouseRelease;

            Input.MouseListeners[_parent.Name].OnButtonDown += ProcessMouseDown;
            Input.MouseListeners[_parent.Name].OnMove += ProcessMouseMove;

            _rootControls = new List<Control>();

            _allControlsFromControlTree = new List<Control>();
        }


        public void Add(Control control)
        {
            if (control != null && !_rootControls.Contains(control))
            {
                if (control.Parent != null)
                {
                    control.Parent.Remove(control);
                }
                _rootControls.Add(control);

                _allControlsFromControlTree.Clear();

                RefreshListOfControlsToUpdate(_rootControls);

                control.Parent = null;
            }
        }

        public void UpdateControls(float frameTime)
        {
            foreach (var control in _rootControls)
            {
                if (!control.Visible)
                {
                    continue;
                }

                control.Update(frameTime);
            }
        }

        public void DrawControls(Canvas2D canvas)
        {
            foreach (var control in _rootControls)
            {
                if (!control.Visible)
                {
                    continue;
                }

                control.Draw(canvas);
            }
        }

        public void Remove(Control control)
        {
            if (control != null)
            {
                if (control.Parent == null)
                {
                    _rootControls.Remove(control);
                }
                else
                {
                    control.Parent.Remove(control);
                }
            }
        }


        private void ProcessMouseRelease(MouseButtonActionInfo info)
        {
            if (_focusedControl != null)
            {
                _focusedControl.SendMessage(ControlMessage.Release, info);
            }
        }

        private void ProcessMouseDown(MouseButtonActionInfo info)
        {
            if (_focusedControl != null)
            {
                _focusedControl.SendMessage(ControlMessage.Press, info);
            }
        }

        private void ProcessMouseMove(MouseMoveActionInfo info)
        {
            for (int i = _allControlsFromControlTree.Count - 1; i >= 0; i--)
            {
                Control control = _allControlsFromControlTree[i];

                if (!control.Visible)
                {
                    continue;
                }


                if (CheckMouseOver(control, new Vector2(info.Location)))
                {
                    if (!control.State.Equals(Control.ControlState.Hovered))
                    {
                        if (control.Children.Count > 0 && _focusedControl != null)
                        {
                            continue;
                        }

                        if (_focusedControl != null)
                        {
                            _focusedControl.SendMessage(ControlMessage.Out, info);
                            _focusedControl = null;
                        }
                        _focusedControl = control;

                        control.SendMessage(ControlMessage.Over, info);
                    }
                }
                else
                {
                    if (control.State.Equals(Control.ControlState.Hovered))
                    {
                        if (_focusedControl != null)
                        {
                            _focusedControl.SendMessage(ControlMessage.Out, info);


                            _focusedControl = null;
                        }
                    }
                }
            }

            bool isOver = false;
            foreach (Control rootControl in _rootControls)
            {
                if (CheckMouseOver(rootControl, new Vector2(info.Location)))
                {
                    isOver = true;
                }
            }

            MouseIsOverGUI = isOver;
        }


        private bool CheckMouseOver(Control control, Vector2 position)
        {
            return control.AbsoluteBoundingRect.Contains(position);
        }

        private void RefreshListOfControlsToUpdate(ICollection<Control> rootControls)
        {
            if (rootControls.Count > 0)
            {
                foreach (var rootControl in rootControls)
                {
                    _allControlsFromControlTree.Add(rootControl);
                    RefreshListOfControlsToUpdate(rootControl.Children);
                }
            }
        }

        private readonly IList<Control> _allControlsFromControlTree;
        private Control _focusedControl;
    }
}