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

namespace SpriteVortex
{
    public enum Origin
    {
        TopLeft,
        MiddleTop,
        TopRight,
        MiddleLeft,
        Center,
        MiddleRight,
        BottomLeft,
        MiddleBottom,
        BottomRight
    }

    public class Animation
    {
        public string SpriteSheetId { get; internal set; }
        public string Id;
        public string Name;
        public List<Frame> Frames { get; internal set; }
        public bool EnableOnionSkin;


        public float FrameRate
        {
            get { return _frameRate; }
            set
            {
                if (value > 0.0f)
                {
                    _frameRate = value;
                    foreach (var frame in Frames)
                    {
                        frame.FrameDuration = 1/value;
                    }
                    _frameTime = 0;
                }
                else
                {
                    Messager.ShowMessage(Messager.Mode.Exception, "Frame rate must be greater than zero!");
                }
            }
        }

        public void SetFrameDuration(int frame, float duration)
        {
            if (duration <= 0)
            {
                Messager.ShowMessage(Messager.Mode.Exception, "Frame duration must be greater than zero!");
            }
            Frames[frame].FrameDuration = duration;
            _frameTime = 0;
        }

        public Frame CurrentFrame
        {
            get { return Frames[_currentFrameIndex]; }
        }

        public int CurrentFrameIndex
        {
            get { return _currentFrameIndex; }
            set
            {
                _currentFrameIndex = value;

                if (_currentFrameIndex < 0)
                {
                    _currentFrameIndex = 0;
                }
                else if (_currentFrameIndex > Frames.Count - 1)
                {
                    _currentFrameIndex = Frames.Count - 1;
                }

                _frameTime = 0;
            }
        }

        public void ToggleOnionSkin()
        {
            EnableOnionSkin = !EnableOnionSkin;
        }

        public void Reset()
        {
            _frameTime = 0;
            _currentFrameIndex = 0;
            Direction = 1;
        }

        public void SetOriginAllFrames(Origin mode)
        {
            _defaultOrigin = mode;

            foreach (var frame in Frames)
            {
                SetFrameOrigin(frame, mode);
            }
        }

        public void SetFrameOrigin(Frame frame, Origin mode)
        {
            switch (mode)
            {
                case Origin.TopLeft:
                    {
                        frame.OffSetX = frame.SpriteFrame.Width/2;
                        frame.OffSetY = frame.SpriteFrame.Height/2;
                        break;
                    }
                case Origin.MiddleTop:
                    {
                        frame.OffSetX = 0;
                        frame.OffSetY = frame.SpriteFrame.Height/2;
                        break;
                    }
                case Origin.TopRight:
                    {
                        frame.OffSetX = -frame.SpriteFrame.Width/2;
                        frame.OffSetY = frame.SpriteFrame.Height/2;
                        break;
                    }
                case Origin.MiddleLeft:
                    {
                        frame.OffSetX = frame.SpriteFrame.Width/2;
                        frame.OffSetY = 0;
                        break;
                    }
                case Origin.Center:
                    {
                        frame.OffSetX = 0;
                        frame.OffSetY = 0;
                        break;
                    }
                case Origin.MiddleRight:
                    {
                        frame.OffSetX = -frame.SpriteFrame.Width/2;
                        frame.OffSetY = 0;
                        break;
                    }
                case Origin.BottomLeft:
                    {
                        frame.OffSetX = frame.SpriteFrame.Width/2;
                        frame.OffSetY = -frame.SpriteFrame.Height/2;
                        break;
                    }
                case Origin.MiddleBottom:
                    {
                        frame.OffSetX = 0;
                        frame.OffSetY = -frame.SpriteFrame.Height/2;
                        break;
                    }
                case Origin.BottomRight:
                    {
                        frame.OffSetX = -frame.SpriteFrame.Width/2;
                        frame.OffSetY = -frame.SpriteFrame.Height/2;
                        break;
                    }
            }
        }


        public int Direction { get; private set; }

        public bool Loop
        {
            get { return _loop; }
            set
            {
                Reset();
                _loop = value;
            }
        }

        public bool PingPong
        {
            get { return _pingPong; }
            set
            {
                Reset();
                _pingPong = value;
            }
        }

        public bool Paused
        {
            get { return _paused; }
            set
            {
                _paused = value;


                Application.Instance.MainWindow.SetTogglePlayButtonState(!_paused);
            }
        }

        public void TogglePlay()
        {
            Paused = !Paused;
        }

        public void Play()
        {
            Paused = false;
        }

        public void Pause()
        {
            Paused = true;
        }

        public int FrameToGoWhenTerminate
        {
            get { return _frameToGoWhenTerminate; }
            set { _frameToGoWhenTerminate = value; }
        }

        public Animation(string name, string id, string spId)
        {
            Name = name;
            SpriteSheetId = spId;
            Frames = new List<Frame>();
            Id = id;
            EnableOnionSkin = true;

            _frameRate = 20f;
            _currentFrameIndex = 0;
            Direction = 1;
            _loop = true;
            _pingPong = false;
            Paused = true;
        }

        public Animation(string name, string id, string spId, IEnumerable<Frame> frames)
        {
            Name = name;
            SpriteSheetId = spId;
            Frames = new List<Frame>();
            Id = id;

            _frameRate = 20f;
            _currentFrameIndex = 0;
            Direction = 1;
            _loop = true;
            _pingPong = false;
            _frameToGoWhenTerminate = 0;
            Paused = true;


            foreach (var frame in frames)
            {
                AddFrame(frame);
            }
        }


        public void AddFrame(Frame frame)
        {
            var frameDuration = 1/_frameRate;

            frame.Num = Frames.Count;
            frame.FrameDuration = frameDuration;


            Frames.Add(frame);

            SetFrameOrigin(frame, _defaultOrigin);
        }

        public void RemoveFrame(Frame frame)
        {
            Frames.Remove(frame);
        }

        public void Clear()
        {
            if (Frames.Count == 0)
            {
                return;
            }
            Frames.Clear();
        }


        public void Update(float timeDelta)
        {
            if (Frames.Count == 0)
            {
                return;
            }


            if (!_paused)
            {
                _frameTime += timeDelta;
                if (_frameTime >= Frames[_currentFrameIndex].FrameDuration)
                {
                    _currentFrameIndex += Direction;
                    _frameTime = 0;
                    if (_currentFrameIndex > Frames.Count - 1 || _currentFrameIndex < 0)
                    {
                        if (_loop)
                        {
                            if (!_pingPong)
                            {
                                _currentFrameIndex = 0;
                            }
                            else
                            {
                                Direction = -Direction;
                                _currentFrameIndex += Direction;
                            }
                        }


                        if (!_loop)
                        {
                            if (!_pingPong)
                            {
                                Paused = true;
                                _currentFrameIndex = _frameToGoWhenTerminate;
                            }
                            else
                            {
                                if (_currentFrameIndex < 0)
                                {
                                    Paused = true;
                                    _currentFrameIndex = 0;
                                    Direction = 1;
                                }
                                else if (_currentFrameIndex > Frames.Count - 1)
                                {
                                    Direction = -1;
                                    _currentFrameIndex += Direction;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Draw(Canvas2D canvas)
        {
            if (Frames.Count == 0)
            {
                return;
            }


            if (EnableOnionSkin)
            {
                if (_currentFrameIndex > 0)
                {
                    canvas.DrawSprite(Frames[_currentFrameIndex - 1].OffSet,
                                      Frames[_currentFrameIndex - 1].SpriteFrame.Sprite,
                                      ColorU.White.MultiplyAlpha(0.5f));
                }
            }

            Frame frame = Frames[_currentFrameIndex];
            canvas.DrawSprite(frame.OffSet, frame.SpriteFrame.Sprite, ColorU.White);
        }


        private int _currentFrameIndex;
        private float _frameRate;
        private float _frameTime;
        private bool _loop;
        private bool _pingPong;
        private int _frameToGoWhenTerminate;

        private Origin _defaultOrigin = Origin.Center;


        private bool _paused;
    }
}