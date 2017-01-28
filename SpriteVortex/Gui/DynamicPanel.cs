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


using SpriteVortex.Helpers.Animation;
using Vortex.Drawing;

namespace SpriteVortex.Gui
{
    public class DynamicPanel : Panel
    {
        public DynamicPanel(GuiManager manager,Rect rect):base(manager,rect)
        {
            _timers = new TimerCollection();
            _interpolators = new InterpolatorCollection();

        }

        public bool Moving
        {
            get
            {   
                return _moving;
            }
        }

        public override void Update(float frameTime)
        {
            base.Update(frameTime);

            _timers.Update(frameTime);
            _interpolators.Update(frameTime);

            
        }

      

        public void MoveVertically(float deltaY)
        {
            Move(new Vector2(0,deltaY ));
        }

        public void MoveHorizontally(float deltaX)
        {
            Move(new Vector2(deltaX,0));
        }

        public void Move(Vector2 delta)
        {
            if (!_moving)
            {
                _timers.Create(0.1f,
                           false,
                           timer =>
                           {
                               _moving = true;
                               if (delta.X != 0)
                               {
                                   _interpolators.Create(Left,
                                       Left + delta.X,
                                       0.12f,
                                       InterpolatorScales.Linear,
                                       i => Left = i.Value,
                                       i => _moving = false

                                       );
                               }
                               if (delta.Y != 0)
                               {
                                   _interpolators.Create(Top,
                                      Top + delta.Y,
                                      0.12f,
                                      InterpolatorScales.Linear,
                                      i => Top = i.Value,
                                      i => _moving = false

                                      );
                               }

                           }
                );
            }
            
            
            
        }

        private bool _moving;
        private readonly TimerCollection _timers;
        private readonly InterpolatorCollection _interpolators;

    }
}
