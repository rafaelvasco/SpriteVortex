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


namespace SpriteVortex
{
    public class RenderDisplayManager
    {
        private SingleContextDevice _mDevice;

        public Size MaxTextureSize
        {
            get { return new Size(_mDevice.Info.MaxTextureWidth, _mDevice.Info.MaxTextureHeight); }
        }


        private Canvas2D _mCanvas;

        private readonly Dictionary<string, RenderDisplay> _mTargets;

        private float _mCounter;

        public bool Initialized { get; private set; }

        public RenderDisplay this[string name]
        {
            get { return _mTargets.ContainsKey(name) ? _mTargets[name] : null; }
        }

        public void Initialize(Control target)
        {
            _mDevice = new SingleContextDevice(target.Handle);
            _mCanvas = _mDevice.Context.Canvas;
            Initialized = true;
            _mDevice.Context.Resize(target.Size);
        }

        public RenderDisplay AddTarget(RenderDisplay renderDisplay)
        {
            if (!Initialized)
            {
                return null;
            }

            _mTargets.Add(renderDisplay.Name, renderDisplay);
            return renderDisplay;
        }

        public void ResizeTarget(string name, Size newSize)
        {
            _mTargets[name].RenderRegion.Width = newSize.Width;

            _mTargets[name].RenderRegion.Height = newSize.Height;

            Rect screenRect = new Rect(Point.Empty, newSize);

            _mTargets[name].Camera.ScreenRect = screenRect;

            if (newSize.Width > _mDevice.Context.Width)
            {
                _mDevice.Context.Resize(newSize.Width, _mDevice.Context.Height);
            }
            if (newSize.Height > _mDevice.Context.Height)
            {
                _mDevice.Context.Resize(_mDevice.Context.Width, newSize.Height);
            }


            UpdateAll(_mCounter);
        }


        public void Update(float interval)
        {
            if (!Initialized) return;

            _mCounter = interval;

            UpdateAll(_mCounter);
        }


        public static RenderDisplayManager Instance
        {
            get { return instance ?? (instance = new RenderDisplayManager()); }
        }

        private void UpdateAll(float frameTime)
        {
            //UPDATE
            if (!MainWindow.Resizing)
            {
                foreach (var mTarget in _mTargets)
                {
                    mTarget.Value.Update(frameTime);
                }
            }

            //RENDER

            foreach (var mTarget in _mTargets)
            {
                if (_mDevice.BeginScene())
                {
                    mTarget.Value.Render(_mCanvas);

                    _mDevice.EndScene();

                    if (mTarget.Value.RenderRegion.Width < _mDevice.Context.Width ||
                        mTarget.Value.RenderRegion.Height < _mDevice.Context.Height)
                    {
                        _mDevice.Context.Present(mTarget.Value.Target.Handle, mTarget.Value.RenderRegion.ToRectangle(),
                                                 mTarget.Value.RenderRegion.ToRectangle());
                    }
                    else
                    {
                        _mDevice.Context.Present(mTarget.Value.Target.Handle);
                    }
                }
                else
                {
                    _mDevice.TryReset();
                }
            }
        }

        public void Dispose()
        {
            foreach (var mTarget in _mTargets)
            {
                mTarget.Value.Dispose();
            }
            _mCanvas.Dispose();
            _mDevice.Dispose();
        }

        private static RenderDisplayManager instance;

        private RenderDisplayManager()
        {
            _mTargets = new Dictionary<string, RenderDisplay>();
        }
    }
}