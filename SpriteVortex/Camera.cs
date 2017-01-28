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
using Vortex.Drawing;

namespace SpriteVortex
{
    public class Camera
    {
        public Camera(Rect screenRect)
        {
            _mScreenRrect = screenRect;
            _mZoom = 1.0f;
            _mAspectRatio = 1.0f;
            UpdateMatrix();
            _mIsSmoothMoving = false;
        }

        public Vector2 Location
        {
            get { return _mLocation; }
            set
            {
                _mLocation = value;
                UpdateMatrix();
            }
        }

        public float Zoom
        {
            get { return _mZoom; }
            set
            {
                _mZoom = value;
                if (_mZoom < 0.5f)
                {
                    _mZoom = 0.5f;
                }
                UpdateMatrix();
            }
        }

        public void FitTo(Rect rect)
        {
            _mRotationAngle = 0;
            float hScale = ScreenRect.Width/rect.Width, vScale = ScreenRect.Height/rect.Height;
            _mZoom = Math.Min(hScale, vScale);
            _mLocation = rect.Center;
            UpdateMatrix();
        }

        public float AspectRatio
        {
            get { return _mAspectRatio; }
            set
            {
                _mAspectRatio = value;
                UpdateMatrix();
            }
        }

        public float AngleRad
        {
            get { return _mRotationAngle; }
            set
            {
                _mRotationAngle = value;
                UpdateMatrix();
            }
        }

        public float AngleDeg
        {
            get { return _mRotationAngle/(float) Math.PI*180; }
            set { _mRotationAngle = value/180*(float) Math.PI; }
        }

        public Rect ScreenRect
        {
            get { return _mScreenRrect; }
            set
            {
                _mScreenRrect = value;

                UpdateMatrix();
            }
        }

        public Transform2D Transformation
        {
            get { return _mTransformation; }
        }


        public void UpdateMatrix()
        {
            Vector2 locationNeg = Location.Negative;

            Vector2 zoom = new Vector2(_mZoom*_mAspectRatio, _mZoom);

            Vector2 screenTranslate = _mScreenRrect.Center;

            Transform2D screenTranslation;

            Transform2D.CreateRotation(_mRotationAngle, out _mRotationMatrix);
            Transform2D.CreateTranslation(ref locationNeg, out _mTranslationMatrix);
            Transform2D.CreateScaling(ref zoom, out _mScalingMatrix);
            Transform2D.CreateTranslation(ref screenTranslate, out screenTranslation);

            _mTransformation = _mTranslationMatrix*_mScalingMatrix*_mRotationMatrix*screenTranslation;
        }

        public Vector2 WorldToScreen(Vector2 vector)
        {
            return _mTransformation.Multiply(vector);
        }

        public Vector2 ScreenToWorld(Vector2 vector)
        {
            return _mTransformation.ReverseMultiply(vector);
        }

        public void Reset()
        {
            Location = Vector2.Zero;
            Zoom = 1.0f;
        }

        public void MoveImediately(float posX, float posY)
        {
            Location = new Vector2(posX, posY);
        }

        public void MoveImediately(Vector2 pos)
        {
            Location = new Vector2(pos.ToPointF());
        }

        public void Move(float deltaX, float deltaY)
        {
            Move(new Vector2(deltaX, deltaY));
        }

        public void Move(Vector2 delta)
        {
            Location = Location + ((_mRotationMatrix*_mScalingMatrix).ReverseMultiply(delta))*Zoom;
        }

        public void MoveSmooth(float deltaX, float deltaY, float damping)
        {
            MoveSmooth(new Vector2(deltaX, deltaY), damping);
        }

        public void MoveSmooth(Vector2 delta, float damping)
        {
            _mIsSmoothMoving = true;
            _mDestinyLocation = _mLocation + delta;
            _mDampingFactor = damping;
        }

        public void ZoomToScreenPoint(float x, float y, float zoomFactor)
        {
            ZoomToScreenPoint(new Vector2(x, y), zoomFactor);
        }

        public void ZoomToScreenPoint(Vector2 point, float zoomFactor)
        {
            ZoomToWorldPoint(ScreenToWorld(point), zoomFactor);
        }

        public void ZoomToWorldPoint(float x, float y, float zoomFactor)
        {
            ZoomToWorldPoint(new Vector2(x, y), zoomFactor);
        }


        public void Update()
        {
            if (!_mIsSmoothMoving)
            {
                return;
            }

            Move(MathHelper.Lerp(Location.X, _mDestinyLocation.X, _mDampingFactor),
                 MathHelper.Lerp(Location.Y, _mDestinyLocation.Y, _mDampingFactor));


            if (_mIsSmoothMoving && Math.Abs(Location.X - _mDestinyLocation.X) < 0.06f &&
                Math.Abs(Location.Y - _mDestinyLocation.Y) < 0.06f)
            {
                _mIsSmoothMoving = false;
            }
        }


        private void ZoomToWorldPoint(Vector2 point, float zoomFactor)
        {
            float previousZoom = _mZoom;
            Vector2 toCenterVector = Location - point;
            Zoom = Zoom*zoomFactor;

            if (_mZoom != previousZoom)
            {
                Location = point + toCenterVector/zoomFactor;
            }
        }


        private Vector2 _mLocation;
        private Vector2 _mDestinyLocation;
        private float _mZoom;
        private float _mAspectRatio;
        private float _mRotationAngle;
        private Rect _mScreenRrect;
        private bool _mIsSmoothMoving;
        private float _mDampingFactor;

        private Transform2D _mTransformation;
        private Transform2D _mScalingMatrix;
        private Transform2D _mRotationMatrix;
        private Transform2D _mTranslationMatrix;
    }
}