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

using Vortex.Drawing;

namespace SpriteVortex
{
    public class MainDisplayStateHandler
    {
        private MainDisplay _owner;

        public State CurrentState { get; private set; }

        public void Init(MainDisplay owner, State initialState)
        {
            _owner = owner;

            ChangeState(initialState);
        }


        public void Update()
        {
            if (CurrentState != null)
            {
                CurrentState.OnUpdate(_owner);
            }
        }

        public void Draw(Canvas2D canvas)
        {
            if (CurrentState != null)
            {
                CurrentState.OnDraw(canvas);
            }
        }

        public void ChangeState(State newState)
        {
            if (CurrentState != null)
            {
                CurrentState.OnExit(_owner);
            }

            CurrentState = newState;

            if (CurrentState != null)
            {
                CurrentState.OnStart(_owner);
            }
        }
    }
}