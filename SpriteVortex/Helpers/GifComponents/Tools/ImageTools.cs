#region Copyright (C) Simon Bridewell
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 3
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

// You can read the full text of the GNU General Public License at:
// http://www.gnu.org/licenses/gpl.html

// See also the Wikipedia entry on the GNU GPL at:
// http://en.wikipedia.org/wiki/GNU_General_Public_License
#endregion

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace SpriteVortex.Helpers.GifComponents.Tools
{
    /// <summary>
    /// Provides a set of static methods for working with 
    /// <see cref="System.Drawing.Image"/> instances.
    /// TODO: move into GifComponents.NUnit project
    /// </summary>
    public static class ImageTools
    {
        #region GetColours method
        /// <summary>
        /// Extracts the pixels of the supplied image into a generic collection 
        /// of <see cref="System.Drawing.Color"/> instances.
        /// </summary>
        /// <param name="image">
        /// The image from which to extract the pixels.
        /// </param>
        /// <returns>
        /// A collection of the colours of all the pixels in the supplied image.
        /// </returns>
        public static Color[] GetColours(Image image)
        {
            if (image == null)
            {
                return new Color[0];
            }
            // SB comment - this comment was present when I downloaded the
            // code from thinkedge.com
            // FEATURE: improve performance: use unsafe code
            int pixelCount = image.Height * image.Width;
            Color[] pixelColours = new Color[pixelCount];
            Bitmap tempBitmap = new Bitmap(image);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color color = tempBitmap.GetPixel(x, y);
                    pixelColours[y * image.Height + x] = color;
                }
            }
            return pixelColours;
        }
        #endregion

        #region GetDistinctColours methods

        #region GetDistinctColours( Collection<Color> ) method
        /// <summary>
        /// Gets a generic collection of all the distinct 
        /// <see cref="System.Drawing.Color"/>s contained in the supplied 
        /// image, i.e. each colour is included in the return value only once,
        /// regardless of how many pixels in the image are of that colour.
        /// </summary>
        /// <param name="imageColours">
        /// A collection of the colours of all the pixels in the image.
        /// </param>
        /// <returns></returns>
        public static Collection<Color>
            GetDistinctColours(Collection<Color> imageColours)
        {
            Hashtable t = new Hashtable();
            foreach (Color c in imageColours)
            {
                if (t.Contains(c) == false)
                {
                    t.Add(c, c);
                }
            }
            Collection<Color> distinctColours = new Collection<Color>();
            foreach (Color c in t.Values)
            {
                distinctColours.Add(c);
            }
            return distinctColours;
        }
        #endregion

        #region GetDistinctColours( Color[] ) method
        /// <summary>
        /// Gets a generic collection of all the distinct 
        /// <see cref="System.Drawing.Color"/>s contained in the supplied 
        /// image, i.e. each colour is included in the return value only once,
        /// regardless of how many pixels in the image are of that colour.
        /// </summary>
        /// <param name="imageColours">
        /// A collection of the colours of all the pixels in the image.
        /// </param>
        /// <returns></returns>
        public static Collection<Color> GetDistinctColours(Color[] imageColours)
        {
            Hashtable t = new Hashtable();
            foreach (Color c in imageColours)
            {
                if (t.Contains(c) == false)
                {
                    t.Add(c, c);
                }
            }
            Collection<Color> distinctColours = new Collection<Color>();
            foreach (Color c in t.Values)
            {
                distinctColours.Add(c);
            }
            return distinctColours;
        }
        #endregion

        #region GetDistinctColours( Image ) method
        /// <summary>
        /// Gets a generic collection of all the distinct 
        /// <see cref="System.Drawing.Color"/>s contained in the supplied 
        /// image, i.e. each colour is included in the return value only once,
        /// regardless of how many pixels in the image are of that colour.
        /// </summary>
        /// <param name="image">The image to examine</param>
        /// <returns>The distinct colours in the supplied image</returns>
        public static Collection<Color> GetDistinctColours(Image image)
        {
            Color[] colours = GetColours(image);
            return GetDistinctColours(colours);
        }
        #endregion

        #endregion

        #region GetRgbArray( Collection<Color> ) method
        /// <summary>
        /// Gets a byte array consisting of the red, green and blue intensities 
        /// of the colours in the supplied collection.
        /// </summary>
        /// <param name="colours">
        /// The collection of colours to convert to a byte array
        /// </param>
        /// <returns>
        /// A byte array consiting of red, green and blue intensities.
        /// 3 bytes per pixel - red first, then green, then blue.
        /// </returns>
        [SuppressMessage("Microsoft.Naming",
                         "CA1704:IdentifiersShouldBeSpelledCorrectly",
                         MessageId = "Rgb")]
        public static byte[] GetRgbArray(Collection<Color> colours)
        {
            int len = colours.Count;
            byte[] rgb = new byte[len * 3];
            int rgbIndex;
            for (int colourIndex = 0; colourIndex < len; colourIndex++)
            {
                rgbIndex = colourIndex * 3;
                rgb[rgbIndex] = (byte)colours[colourIndex].R;
                rgb[rgbIndex + 1] = (byte)colours[colourIndex].G;
                rgb[rgbIndex + 2] = (byte)colours[colourIndex].B;
            }
            return rgb;
        }
        #endregion

        #region GetRgbArray( Color[] method ) method
        /// <summary>
        /// Gets a byte array consisting of the red, green and blue intensities 
        /// of the colours in the supplied array of colours.
        /// </summary>
        /// <param name="colours">
        /// The array of colours to convert to a byte array
        /// </param>
        /// <returns>
        /// A byte array consiting of red, green and blue intensities.
        /// 3 bytes per pixel - red first, then green, then blue.
        /// </returns>
        [SuppressMessage("Microsoft.Naming",
                         "CA1704:IdentifiersShouldBeSpelledCorrectly",
                         MessageId = "Rgb")]
        public static byte[] GetRgbArray(Color[] colours)
        {
            if (colours == null)
            {
                throw new ArgumentNullException("colours");
            }
            int len = colours.Length;
            byte[] rgb = new byte[len * 3];
            int rgbIndex;
            for (int colourIndex = 0; colourIndex < len; colourIndex++)
            {
                rgbIndex = colourIndex * 3;
                rgb[rgbIndex] = (byte)colours[colourIndex].R;
                rgb[rgbIndex + 1] = (byte)colours[colourIndex].G;
                rgb[rgbIndex + 2] = (byte)colours[colourIndex].B;
            }
            return rgb;
        }
        #endregion

        #region GetRgbCollection( Collection<Color> ) method
        /// <summary>
        /// Gets a generic collection of bytes consisting of the red, green and 
        /// blue intensities of the colours in the supplied collection.
        /// </summary>
        /// <param name="colours">
        /// The collection of colours to convert to a collection of bytes
        /// </param>
        /// <returns>
        /// A collection of bytes consiting of red, green and blue intensities.
        /// 3 bytes per pixel - red first, then green, then blue.
        /// </returns>
        [Obsolete("Use the GetRgbArray method instead - it's around 60% faster")]
        public static Collection<byte> GetRgbCollection(Collection<Color> colours)
        {
            int len = colours.Count;
            Collection<byte> rgb = new Collection<byte>();
            for (int i = 0; i < len; i++)
            {
                rgb.Add((byte)colours[i].R);
                rgb.Add((byte)colours[i].G);
                rgb.Add((byte)colours[i].B);
            }
            return rgb;
        }
        #endregion

        #region GetRgbCollection( Color[] ) method
        /// <summary>
        /// Gets a generic collection of bytes consisting of the red, green and 
        /// blue intensities of the colours in the supplied collection.
        /// </summary>
        /// <param name="colours">
        /// The collection of colours to convert to a collection of bytes
        /// </param>
        /// <returns>
        /// A collection of bytes consiting of red, green and blue intensities.
        /// 3 bytes per pixel - red first, then green, then blue.
        /// </returns>
        [Obsolete("Use the GetRgbArray method instead - it's around 60% faster")]
        public static Collection<byte> GetRgbCollection(Color[] colours)
        {
            int len = colours.Length;
            Collection<byte> rgb = new Collection<byte>();
            for (int i = 0; i < len; i++)
            {
                rgb.Add((byte)colours[i].R);
                rgb.Add((byte)colours[i].G);
                rgb.Add((byte)colours[i].B);
            }
            return rgb;
        }
        #endregion
    }
}
