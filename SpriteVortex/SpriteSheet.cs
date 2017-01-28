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
using System.ComponentModel;
using System.Drawing;
using Vortex.Drawing;
using ImageHelper = SpriteVortex.Helpers.ImageHelper;

namespace SpriteVortex
{
    public class SpriteSheetFrame
    {
        [Browsable(false)]
        public Rect BoundingRect
        {
            get { return Rect.FromBox(X, Y, Width, Height); }
            internal set
            {
                X = (int) value.Left;
                Y = (int) value.Top;
                Width = (int) value.Width;
                Height = (int) value.Height;
            }
        }

        [Browsable(true)]
        [Description("X")]
        [Category("Bounding Rect")]
        public int X { get; set; }

        [Browsable(true)]
        [Description("Y")]
        [Category("Bounding Rect")]
        public int Y { get; set; }


        [Browsable(true)]
        [Description("Width")]
        [Category("Bounding Rect")]
        public int Width { get; set; }


        [Browsable(true)]
        [Description("Height")]
        [Category("Bounding Rect")]
        public int Height { get; set; }

        [Browsable(true)]
        [Description("Tag Name")]
        public string TagName { get; set; }

        [Browsable(true)]
        [Description("ID")]
        public int Id { get; internal set; }

        [Browsable(false)]
        public Rect RelativeBoundingRect
        {
            get
            {
                float x = (BoundingRect.Left - Parent.Texture.Size.Width*0.5f);
                float y = (BoundingRect.Top - Parent.Texture.Size.Height*0.5f);
                return Rect.FromBox(x, y, BoundingRect.Width, BoundingRect.Height);
            }
        }

        [Browsable(false)]
        public bool Highlighted { get; set; }

        [Browsable(false)]
        public bool Selected { get; set; }

        [Browsable(false)]
        public SpriteSheet Parent { get; private set; }

        public SpriteSheetFrame(Rect rect, SpriteSheet parent)
        {
            BoundingRect = rect;
            Parent = parent;
        }
    }

    public class SpriteSheet
    {
        public string Name;
        public string Id;
        public string FullPath;
        public Texture Texture;

        public List<ColorU> TransparentColors;

        public List<SpriteSheetFrame> Frames { get; private set; }
        public List<SpriteSheetFrame> SelectedFrames { get; private set; }
        public SpriteSheetFrame HighLightedFrame { get; set; }

        public Rect BoundingRect
        {
            get
            {
                return new Rect(-Texture.Size.Width*0.5f,
                                -Texture.Size.Height*0.5f,
                                Texture.Size.Width*0.5f,
                                Texture.Size.Height*0.5f
                    );
            }
        }


        public SpriteSheet(Texture texture)
        {
            Texture = texture;
            Frames = new List<SpriteSheetFrame>();
            SelectedFrames = new List<SpriteSheetFrame>();
            TransparentColors = new List<ColorU>();
        }

        internal void ResetTextureFromBitmap(Bitmap image)
        {
            if (Texture != null)
            {
                Texture.Dispose();
            }

            Texture = new Texture(image);
        }

        public void ClearColor(int x, int y)
        {
            Bitmap spriteSheetTexBitmap = Texture.Surface.ToBitmap();

            Color clearedColor = ImageHelper.ClearColor(ref spriteSheetTexBitmap, x, y);

            ColorU clearedColorConverted = new ColorU(clearedColor);

            if (!TransparentColors.Contains(clearedColorConverted))
            {
                TransparentColors.Add(clearedColorConverted);
            }


            ResetTextureFromBitmap(spriteSheetTexBitmap);

            spriteSheetTexBitmap.Dispose();
        }

        public void ClearColor(ColorU color)
        {
            Bitmap spriteSheetTexBitmap = Texture.Surface.ToBitmap();

            Color clearedColor = ImageHelper.ClearColor(ref spriteSheetTexBitmap, color.ToColor());

            TransparentColors.Add(new ColorU(clearedColor));

            ResetTextureFromBitmap(spriteSheetTexBitmap);

            spriteSheetTexBitmap.Dispose();
        }

        public SpriteSheet BlankClone()
        {
            SpriteSheet clone = new SpriteSheet(null);
            clone.Name = this.Name;
            clone.FullPath = this.FullPath;
            clone.Id = this.Id;
            clone.TransparentColors = this.TransparentColors;
            return clone;
        }

        public SpriteSheetFrame AddFrame(Rect frameRect)
        {
            return AddFrame("Sprite" + Frames.Count, Frames.Count, frameRect);
        }

        public SpriteSheetFrame AddFrame(string name, int id, Rect frameRect)
        {
            SpriteSheetFrame frame = new SpriteSheetFrame(frameRect, this) {TagName = name, Id = id};

            Frames.Add(frame);

            return frame;
        }

        public void SortFrames()
        {
            var rects = new List<Rect>();

            foreach (SpriteSheetFrame frameRect in Frames)
            {
                rects.Add(frameRect.BoundingRect);
            }

            Frames.Clear();

            SpriteSheetAndTextureFuncs.SortFramesLeftToRightTopToDown(rects);

            foreach (Rect rect in rects)
            {
                AddFrame(rect);
            }
        }


        public void ChangeFrameRect(SpriteSheetFrame frame, Rect newFrameRect)
        {
            frame.BoundingRect = newFrameRect;
        }

        public List<int> RemoveSelectedFrames()
        {
            var selectedFrames = new List<SpriteSheetFrame>(SelectedFrames);

            var removedIds = new List<int>();

            foreach (SpriteSheetFrame frame in selectedFrames)
            {
                SelectFrame(frame, false);
                HighLightFrame(frame, false);
                removedIds.Add(frame.Id);
                Frames.Remove(frame);
            }
            selectedFrames.Clear();

            return removedIds;
        }

        public void UnselectAllFrames()
        {
            foreach (SpriteSheetFrame frame in Frames)
            {
                SelectFrame(frame, false);
            }
        }

        public void ToggleSelectFrame(SpriteSheetFrame spriteSheetFrame)
        {
            SelectFrame(spriteSheetFrame, !spriteSheetFrame.Selected);
        }

        public void SelectFrame(SpriteSheetFrame spriteSheetFrame, bool select)
        {
            if (select && !spriteSheetFrame.Selected)
            {
                SelectedFrames.Add(spriteSheetFrame);
            }
            else if (!select && spriteSheetFrame.Selected)
            {
                SelectedFrames.Remove(spriteSheetFrame);
            }

            spriteSheetFrame.Selected = select;
        }

        public void HighLightFrame(SpriteSheetFrame spriteSheetFrame, bool highlight)
        {
            if (spriteSheetFrame.Highlighted == highlight)
            {
                return;
            }

            spriteSheetFrame.Highlighted = highlight;

            if (highlight)
            {
                HighLightedFrame = spriteSheetFrame;
            }
            else
            {
                HighLightedFrame = null;
            }
        }
    }
}