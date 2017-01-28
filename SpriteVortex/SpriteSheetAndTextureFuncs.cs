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
using System.Drawing.Imaging;
using Vortex.Drawing;
using PixelFormat = Vortex.Drawing.PixelFormat;

namespace SpriteVortex
{
    public static class SpriteSheetAndTextureFuncs
    {

        public static List<Rect> CutByAlpha(Texture tex)
        {
           return CutByAlpha(tex, 0, 0, 0.12, 5);
        }

        public static List<Rect> CutByAlpha(Texture tex, double transparencyLimit, int minimumAcceptedSize)
        {
           return CutByAlpha(tex, 0, 0, transparencyLimit, minimumAcceptedSize);
        }

        public static List<Rect> CutByAlpha(Texture tex, int scanStartX, int scanStartY, double transparencyLimit,int minimumAcceptedSize)
        {
            var frameRects = FindFrameRects(tex, scanStartX, scanStartY, transparencyLimit);

            ProcessFrameRects(frameRects,minimumAcceptedSize);
            return frameRects;
        }

      

        
        /// <summary>
        /// Returns a list of sprite rectangles cut from an image by alpha channel.
        /// Modified from ClanLib SDK (http://clanlib.org)
        /// </summary>
        /// <param name="tex"></param>
        /// <param name="scanStartX"></param>
        /// <param name="scanStartY"></param>
        /// <param name="transparencyLimit"></param>
        /// <returns></returns>
        private static List<Rect> FindFrameRects(Texture tex,int scanStartX, int scanStartY, double transparencyLimit)
        {
            int width = (int)tex.Size.Width;
            int height = (int)tex.Size.Height;

            List<Rect> frameRects = new List<Rect>();

            bool[] explored = new bool[width * height];

            ColorU[] texData = new ColorU[width * height];

            

            tex.Surface.GetData(texData,PixelFormat.DefaultAlpha);

            for (int y = scanStartY; y < height; y++)
            {
                for (int x = scanStartX; x < width; x++)
                {
                    int texIndex = y * width + x;

                    if (explored[texIndex]) continue;

                    explored[y * width + x] = true;
                    if ((texData[texIndex].A) <= transparencyLimit * 255) continue;

                    int x2;
                    int x1 = x2 = x;
                    int y2;
                    int y1 = y2 = y;

                    bool more = true;

                    while (more)
                    {
                        more = false;

                        for (int i = x1; i <= x2; i++)
                        {
                            if (y2 + 1 < height)
                            {
                                explored[(y2 + 1) * width] = true;
                                if ((texData[(y2 + 1) * width + i].A) > transparencyLimit * 255)
                                {
                                    more = true;
                                    y2 += 1;
                                }
                            }
                        }

                        for (int j = y1; j <= y2; j++)
                        {
                            if (x2 + 1 < width)
                            {
                                explored[j * width + x2 + 1] = true;
                                if ((texData[j * width + x2 + 1].A) > transparencyLimit * 255)
                                {
                                    more = true;
                                    x2 += 1;
                                }
                            }

                            if (x1 - 1 >= 0)
                            {
                                explored[j * width + x1 - 1] = true;
                                if ((texData[j * width + x1 - 1].A) > transparencyLimit * 255)
                                {
                                    more = true;
                                    x1 -= 1;
                                }
                            }
                        }
                    }

                    for (int i = x1; i <= x2; i++)
                    {
                        for (int j = y1; j <= y2; j++)
                        {
                            explored[j * width + i] = true;
                        }
                    }

                    frameRects.Add(new Rect(x1, y1, x2 + 1, y2 + 1));
                }
            }

            return frameRects;
        }

        private static void ProcessFrameRects(List<Rect> frameRects, int minimumAcceptedSize)
        {
           

            var toRemove = new List<Rect>();

            for (var i = 0; i < frameRects.Count; i++)
            {
                if (frameRects[i].Width <= minimumAcceptedSize && frameRects[i].Height <= minimumAcceptedSize)
                {
                    toRemove.Add(frameRects[i]);
                }
            }

            foreach (var frame in toRemove)
            {
                frameRects.Remove(frame);
            }

            SortFramesLeftToRightTopToDown(frameRects);
            

        }

        public static void SortFramesLeftToRightTopToDown(List<Rect> frameRects)
        {

            SortFramesVertically(frameRects);


            var frameLines = new List<List<Rect>>();

            var lastMarginIndex = 0;


            if (frameRects.Count > 0)
            {
                for (var i = 0; i < frameRects.Count - 1; i++)
                {
                    if (frameRects[i + 1].Top <= frameRects[i].Top + frameRects[i].Height) continue;

                    frameLines.Add(frameRects.GetRange(lastMarginIndex, (i - lastMarginIndex) + 1));
                    lastMarginIndex = i + 1;
                }

                frameLines.Add(frameRects.GetRange(lastMarginIndex, frameRects.Count - lastMarginIndex));

                foreach (var frameLine in frameLines)
                {
                    SortFramesHorizontally(frameLine);
                }

                frameRects.Clear();

                foreach (var frameLine in frameLines)
                {
                    foreach (var frame in frameLine)
                    {
                        frameRects.Add(frame);
                    }
                }
            }

        }

        private static void SortFramesHorizontally(IList<Rect> frames)
        {
            SortFramesHorizontally(frames, 0, frames.Count - 1);
        }

        private static void SortFramesHorizontally(IList<Rect> frames, int start, int end)
        {
            var i = start;
            var j = end;
            var x = frames[(int)((start + end) * 0.5f)];

            do
            {
                while ((frames[i].Left < x.Left) && (i < end)) i++;
                while ((x.Left < frames[j].Left) && (j > start)) j--;

                if (i > j) continue;

                var y = frames[i];
                frames[i] = frames[j];
                frames[j] = y;
                i++;
                j--;
            } while (i <= j);

            if (start < j) SortFramesHorizontally(frames, start, j);
            if (i < end) SortFramesHorizontally(frames, i, end);
        }

        private static void SortFramesVertically(IList<Rect> frames)
        {
            SortFramesVertically(frames,0, frames.Count - 1);
        }

        private static void SortFramesVertically(IList<Rect> frames, int start, int end)
        {
            var i = start;
            var j = end;
            var x = frames[(int)((start + end) * 0.5f)];

            do
            {
                while ((frames[i].Top < x.Top) && (i < end)) i++;
                while ((x.Top < frames[j].Top) && (j > start)) j--;

                if (i > j) continue;

                var y = frames[i];
                frames[i] = frames[j];
                frames[j] = y;
                i++;
                j--;
            } while (i <= j);

            if (start < j) SortFramesVertically(frames, start, j);
            if (i < end) SortFramesVertically(frames, i, end);
        }
        

        //-------------------------------------------------------------------------------------------------------------------------------------


        public static Rect ShrinkFrameSelect(Texture tex, Rect selectionRect, double alphaTolerance)
        {

            var selectionRectInt = selectionRect.ToRectangle();

            ColorU[] selectionData = new ColorU[selectionRectInt.Width * selectionRectInt.Height];

            
            tex.Surface.GetData(selectionRectInt, selectionData, 0, selectionRectInt.Width * selectionRectInt.Height,PixelFormat.DefaultAlpha);

            int x1 = 0, y1 = 0, x2 = 0, y2 = 0;

            int first = 0;

            bool found = false;

            for (int i = 0; i < selectionData.Length; i++)
            {
                if (selectionData[i].A >= alphaTolerance*255)
                {
                    found = true;
                    x1 = i % selectionRectInt.Width;
                    y1 = i / selectionRectInt.Width;
                    x2 = x1;
                    y2 = y1;

                    first = i;
                    break;
                }
            }
            if (found)
            {
                for (int i = first; i < selectionData.Length; i++)
                {
                    if (selectionData[i].A >= alphaTolerance*255)
                    {
                        int x = i % selectionRectInt.Width;
                        int y = i / selectionRectInt.Width;

                        if(x < x1) x1 = x;
                        if(y < y1) y1 = y;
                        if(x > x2) x2 = x;
                        if(y > y2) y2 = y;

                    }
                }
                x1 += selectionRectInt.X;
                y1 += selectionRectInt.Y;

                x2 += selectionRectInt.X + 1;
                y2 += selectionRectInt.Y + 1;

                
                return new Rect(x1,y1,x2,y2);
            }

            return Rect.Empty;
        }

        public static Texture TrimmByAlpha(Texture tex)
        {
            var optimizedRect = ShrinkFrameSelect(tex, tex.Region, 0.12).ToRectangle();

            var optimizedSpTexData = new ColorU[optimizedRect.Width * optimizedRect.Height];

            tex.Surface.GetData(optimizedRect, optimizedSpTexData,PixelFormat.DefaultAlpha);

            var finalTex = new Texture(optimizedRect.Width, optimizedRect.Height, PixelFormat.DefaultAlpha);

            finalTex.Surface.SetData(optimizedSpTexData,PixelFormat.DefaultAlpha);
            
            
            tex.Dispose();

            return finalTex;
        }
        

        
    }
}
