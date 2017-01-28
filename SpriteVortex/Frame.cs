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

using System.ComponentModel;
using System.Drawing;
using Vortex.Drawing;

namespace SpriteVortex
{
    public class SpriteFrame
    {
        public Sprite Sprite { get; private set; }
        public int SpriteFrameId { get; private set; }

        public SpriteFrame(Sprite sprite, int spriteFrameId)
        {
            Sprite = sprite;
            SpriteFrameId = spriteFrameId;
        }


        public int Width
        {
            get { return (int) Sprite.Size.X; }
        }

        public int Height
        {
            get { return (int) Sprite.Size.Y; }
        }

        public Rectangle Region
        {
            get { return Sprite.Region.ToRectangle(); }
        }
    }

    public class Frame
    {
        public Frame(Sprite sprite, int spriteFrameId)
        {
            SpriteFrame = new SpriteFrame(sprite, spriteFrameId);
            OffSetX = 0;
            OffSetY = 0;
        }

        public void ResetSpriteFrame(Sprite sprite, int spriteFrameId)
        {
            SpriteFrame = new SpriteFrame(sprite, spriteFrameId);
        }

        [Browsable(true)]
        [Description("Frame Num")]
        [ReadOnly(true)]
        public int Num { get; internal set; }

        [Browsable(true)]
        [Description("X Offset")]
        public int OffSetX { get; set; }

        [Browsable(true)]
        [Description("Y Offset")]
        public int OffSetY { get; set; }


        [Browsable(false)]
        public float OriginX
        {
            get { return SpriteFrame.Width/2f - OffSetX; }
        }

        [Browsable(false)]
        public float OriginY
        {
            get { return SpriteFrame.Height/2f - OffSetY; }
        }


        [Browsable(false)]
        public Vector2 OffSet
        {
            get { return new Vector2(OffSetX, OffSetY); }
        }

        [Browsable(true)]
        [Description("Frame Duration")]
        public float FrameDuration { get; set; }

        [Browsable(false)]
        public SpriteFrame SpriteFrame { get; private set; }

        [Browsable(true)]
        [Description("Region")]
        public Rectangle Region
        {
            get { return SpriteFrame.Sprite.Region.ToRectangle(); }
        }
    }
}