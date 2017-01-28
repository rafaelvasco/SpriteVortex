#region License

/**
     * Copyright (C) 2010 Rafael Vasco (rafaelvasco87@gmail.com)
     * Copyright (c) 2009-2010 Nick Gravelyn (nick@gravelyn.com)
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
using System.Collections.Generic;
using System.Drawing;
using Vortex.Drawing;


namespace SpriteVortex.Helpers.Packing
{
    public class SpriteSheetPacker
    {
        private Dictionary<int, Rect> testSpriteMapping = new Dictionary<int, Rect>();
        private Dictionary<int, Rect> finalSpriteMapping = new Dictionary<int, Rect>();

        private int padding;

        private bool requirePowTwo;

        private bool requireSquare;

        private SpriteSheet tempSpriteSheet;

        private int finalSpriteSheetWidth;

        private int finalSpriteSheetHeight;


        public bool PackSpriteSheet(SpriteSheet originalSpriteSheet, bool requirePowTwo, bool requireSquare, int padding,
                                    bool generateMap, int maxWidth, int maxHeight, out SpriteSheet packed)
        {
            tempSpriteSheet = originalSpriteSheet.BlankClone();

            finalSpriteSheetWidth = maxWidth;
            finalSpriteSheetHeight = maxHeight;

            this.requirePowTwo = requirePowTwo;
            this.requireSquare = requireSquare;

            this.padding = padding;


            var sprites = originalSpriteSheet.Frames;


            sprites.Sort(
                (i1, i2) =>
                    {
                        Size b1 = new Size(i1.Width, i1.Height);
                        Size b2 = new Size(i2.Width, i2.Height);

                        int c = -b1.Width.CompareTo(b2.Width);
                        if (c != 0)
                            return c;

                        c = -b1.Height.CompareTo(b2.Height);
                        if (c != 0)
                            return c;

                        return 0;
                    }
                );

            if (!PackBruteForce(originalSpriteSheet.Frames))
            {
                packed = null;
                return false;
            }


            Bitmap packedTexture = BuildPackedTexture(originalSpriteSheet);

            tempSpriteSheet.ResetTextureFromBitmap(packedTexture);

            if (generateMap)
            {
                int[] keys = new int[finalSpriteMapping.Keys.Count];
                finalSpriteMapping.Keys.CopyTo(keys, 0);
                foreach (var key in keys)
                {
                    Rect r = finalSpriteMapping[key];

                    r.Right -= padding;
                    r.Bottom -= padding;

                    finalSpriteMapping[key] = r;
                }

                foreach (var map in finalSpriteMapping)
                {
                    SpriteSheetFrame newFrame = new SpriteSheetFrame(map.Value, tempSpriteSheet);
                    newFrame.Id = map.Key;
                    newFrame.TagName = originalSpriteSheet.Frames[newFrame.Id].TagName;
                    tempSpriteSheet.Frames.Add(newFrame);
                }
            }


            packed = tempSpriteSheet;

            testSpriteMapping.Clear();
            finalSpriteMapping.Clear();

            return true;
        }

        private bool PackBruteForce(List<SpriteSheetFrame> sprites)
        {
            int testWidth;
            int testHeight;

            int smallestWidth = int.MaxValue;
            int smallestHeight = int.MaxValue;


            foreach (var sprite in sprites)
            {
                smallestWidth = Math.Min(smallestWidth, sprite.Width);
                smallestHeight = Math.Min(smallestHeight, sprite.Height);
            }

            testWidth = finalSpriteSheetWidth;
            testHeight = finalSpriteSheetHeight;

            bool shrinkVertical = false;


            while (true)
            {
                testSpriteMapping.Clear();

                if (!TryPackSprites(sprites, testWidth, testHeight))
                {
                    if (finalSpriteMapping.Count == 0)
                    {
                        return false;
                    }

                    if (shrinkVertical)
                    {
                        return true;
                    }

                    shrinkVertical = true;


                    testWidth += smallestWidth + padding + padding;
                    testHeight += smallestHeight + padding + padding;

                    continue;
                }

                finalSpriteMapping.Clear();

                foreach (var pair in testSpriteMapping)
                {
                    finalSpriteMapping.Add(pair.Key, pair.Value);
                }

                testWidth = testHeight = 0;


                foreach (var pair in finalSpriteMapping)
                {
                    testWidth = Math.Max(testWidth, (int) pair.Value.Right);
                    testHeight = Math.Max(testHeight, (int) pair.Value.Bottom);
                }

                if (!shrinkVertical)
                {
                    testWidth -= padding;
                }

                testHeight -= padding;


                if (requirePowTwo)
                {
                    testWidth = MathHelper.FindNextPowerOfTwo(testWidth);
                    testHeight = MathHelper.FindNextPowerOfTwo(testHeight);
                }

                if (requireSquare)
                {
                    int max = Math.Max(testWidth, testHeight);
                    testWidth = testHeight = max;
                }

                if (testWidth == finalSpriteSheetWidth && testHeight == finalSpriteSheetHeight)
                {
                    if (shrinkVertical)
                        return true;

                    shrinkVertical = true;
                }


                finalSpriteSheetWidth = testWidth;
                finalSpriteSheetHeight = testHeight;

                if (!shrinkVertical)
                {
                    testWidth -= smallestWidth;
                }
                testHeight -= smallestHeight;
            }
        }


        private bool TryPackSprites(List<SpriteSheetFrame> sprites, int width, int height)
        {
            ArevaloRectanglePacker rectanglePacker = new ArevaloRectanglePacker(width, height);


            foreach (var sprite in sprites)
            {
                Point origin;

                if (!rectanglePacker.TryPack(sprite.Width + padding, sprite.Height + padding, out origin))
                {
                    return false;
                }

                testSpriteMapping.Add(sprite.Id,
                                      Rect.FromBox(origin.X, origin.Y, sprite.Width + padding, sprite.Height + padding));
            }

            return true;
        }

        private Bitmap BuildPackedTexture(SpriteSheet originalSpriteSheet)
        {
            Bitmap combinedBitmap = new Bitmap(finalSpriteSheetWidth, finalSpriteSheetHeight);


            for (int i = 0; i < originalSpriteSheet.Frames.Count; i++)

            {
                SpriteSheetFrame spriteSheetFrame = originalSpriteSheet.Frames[i];

                Bitmap spriteBitmap =
                    originalSpriteSheet.Texture.Surface.ToBitmap(spriteSheetFrame.BoundingRect.ToRectangle());


                Rectangle spriteRect = finalSpriteMapping[spriteSheetFrame.Id].ToRectangle();

                for (int x = 0; x < spriteBitmap.Width; x++)
                    for (int y = 0; y < spriteBitmap.Height; y++)
                        combinedBitmap.SetPixel(spriteRect.X + x, spriteRect.Y + y, spriteBitmap.GetPixel(x, y));
            }


            return combinedBitmap;
        }
    }
}