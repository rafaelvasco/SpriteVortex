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
using Vortex.Input;

namespace SpriteVortex
{
    public class MouseButtonState
    {
        public bool Left;
        public bool Right;
        public bool Middle;

        public MouseButtonState(bool left, bool right, bool middle)
        {
            Left = left;
            Right = right;
            Middle = middle;
        }
    }


    public static class Input
    {
        private static Dictionary<string, MouseListener> mouseListeners;


        private static KeyboardListener keyBoardListener;
        private static bool initialized;
        private static int keysDownCount;
        private static VirtualKey currentDownKey;
        private static VirtualKey prevDownKey;
        private static Dictionary<string, MouseButtonState> currentMouseButtonStates;
        private static Dictionary<string, MouseButtonState> previousMouseButtonStates;

        private static List<string> displayNames;


        public static Dictionary<string, MouseListener> MouseListeners
        {
            get { return mouseListeners; }
        }

        public static KeyboardListener KeyBoardListener
        {
            get { return keyBoardListener; }
        }

        public static void Initialize(params RenderDisplay[] displays)
        {
            if (initialized)
            {
                return;
            }

            displayNames = new List<string>();

            mouseListeners = new Dictionary<string, MouseListener>();

            keyBoardListener = new KeyboardListener();

            previousMouseButtonStates = new Dictionary<string, MouseButtonState>();

            currentMouseButtonStates = new Dictionary<string, MouseButtonState>();


            System.Windows.Forms.Application.AddMessageFilter(keyBoardListener);

            foreach (RenderDisplay display in displays)
            {
                displayNames.Add(display.Name);
                mouseListeners.Add(display.Name, new MouseListener(display.Target.Handle));

                System.Windows.Forms.Application.AddMessageFilter(mouseListeners[display.Name]);

                var buttonState = new MouseButtonState(mouseListeners[display.Name].IsLeftDown,
                                                       mouseListeners[display.Name].IsRightDown,
                                                       mouseListeners[display.Name].IsMiddleDown);
                currentMouseButtonStates.Add(display.Name, buttonState);

                previousMouseButtonStates.Add(display.Name, new MouseButtonState(true, true, true));
            }

            initialized = true;

            prevDownKey = VirtualKey.None;

            keyBoardListener.OnKeyDown += Input_OnKeyDown;
            keyBoardListener.OnKeyUp += Input_OnKeyUp;
        }


        private static void Input_OnKeyUp(VirtualKey key)
        {
            keysDownCount = (int)MathHelper.Max(0,keysDownCount-1); //sometimes, the value was negative,I suppose that we're calling this function more than 1 time per frame
            currentDownKey = VirtualKey.None;
            prevDownKey = VirtualKey.None;
        }

        private static void Input_OnKeyDown(VirtualKey key)
        {
            currentDownKey = key;

            if (currentDownKey != prevDownKey)
            {
                keysDownCount += 1;
            }

            prevDownKey = currentDownKey;
        }


        public static void Update()
        {
            keyBoardListener.Update();

            foreach (string displayName in displayNames)
            {
                currentMouseButtonStates[displayName].Left = mouseListeners[displayName].IsLeftDown;
                currentMouseButtonStates[displayName].Right = mouseListeners[displayName].IsRightDown;
                currentMouseButtonStates[displayName].Middle = mouseListeners[displayName].IsMiddleDown;
            }
        }

        public static void PostUpdate()
        {
            foreach (string displayName in displayNames)
            {
                previousMouseButtonStates[displayName].Left = currentMouseButtonStates[displayName].Left;
                previousMouseButtonStates[displayName].Right = currentMouseButtonStates[displayName].Right;
                previousMouseButtonStates[displayName].Middle = currentMouseButtonStates[displayName].Middle;
            }
        }

        public static Point MousePosition(string display)
        {
            return mouseListeners[display].Location;
        }


        public static bool MouseIsInside(string display)
        {
            return mouseListeners[display].IsOverWindow;
        }

        public static bool MousePressed(MouseButton button, string display)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return currentMouseButtonStates[display].Left && !previousMouseButtonStates[display].Left;
                case MouseButton.Right:
                    return currentMouseButtonStates[display].Right && !previousMouseButtonStates[display].Right;
                case MouseButton.Middle:
                    return currentMouseButtonStates[display].Middle && !previousMouseButtonStates[display].Middle;
            }
            return false;
        }


        public static bool MouseReleased(MouseButton button, string display)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return !currentMouseButtonStates[display].Left && previousMouseButtonStates[display].Left;
                case MouseButton.Right:
                    return !currentMouseButtonStates[display].Right && previousMouseButtonStates[display].Right;
                case MouseButton.Middle:
                    return !currentMouseButtonStates[display].Middle && previousMouseButtonStates[display].Middle;
            }
            return false;
        }


        public static bool MouseDown(MouseButton button, string display)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return currentMouseButtonStates[display].Left && previousMouseButtonStates[display].Left;
                case MouseButton.Right:
                    return currentMouseButtonStates[display].Right && previousMouseButtonStates[display].Right;
                case MouseButton.Middle:
                    return currentMouseButtonStates[display].Middle && previousMouseButtonStates[display].Middle;
            }
            return false;
        }


        public static bool KeyPressed(Key key, string display)
        {
            return keyBoardListener.IsPressed(key);
        }

        public static bool KeyReleased(Key key, string display)
        {
            return keyBoardListener.IsReleased(key);
        }


        public static bool KeyDown(Key key, string display)
        {
            return keyBoardListener.IsDown(key);
        }

        public static bool AnyOfTheseKeysDown(string display, params Key[] keys)
        {
            foreach (Key key in keys)
            {
                if (keyBoardListener.IsDown(key))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsCombinationPressed(Key key1, Key key2, string display)
        {
            return keyBoardListener.IsPressedCombination(key1, key2);
        }

        public static bool ControlDown(ControlConfig control, string display)
        {
            if (control.Key != null)
            {
                return KeyDown((Key) control.Key, display) && MouseDown(control.MouseButton, display);
            }

            return MouseDown(control.MouseButton, display) && keysDownCount == 0;
        }

        public static bool ControlReleased(ControlConfig control, string display)
        {
            if (control.Key != null)
            {
                return KeyReleased((Key) control.Key, display) || MouseReleased(control.MouseButton, display);
            }

            return MouseReleased(control.MouseButton, display);
        }


        public static bool ControlPressed(ControlConfig control, string display)
        {
            if (control.Key != null)
            {
                return KeyDown((Key) control.Key, display) && MousePressed(control.MouseButton, display);
            }


            return MousePressed(control.MouseButton, display) && keysDownCount == 0;
        }


        public static bool AnyOfTheseKeysPressed(string display, params Key[] keys)
        {
            foreach (Key key in keys)
            {
                if (keyBoardListener.IsPressed(key))
                {
                    return true;
                }
            }
            return false;
        }


        public static void Dispose()
        {
            System.Windows.Forms.Application.RemoveMessageFilter(keyBoardListener);

            foreach (string displayName in displayNames)
            {
                System.Windows.Forms.Application.RemoveMessageFilter(mouseListeners[displayName]);
            }

            keyBoardListener.Dispose();

            mouseListeners.Clear();
            currentMouseButtonStates.Clear();
            previousMouseButtonStates.Clear();
        }
    }
}