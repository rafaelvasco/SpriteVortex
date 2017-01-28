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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace SpriteVortex.Helpers
{
    public static class ImageHelper
    {
        public static readonly string[] AllowedImageExtensions = new[] {"png", "bmp", "gif"};


        /// <summary>
        /// Check if image extension is allowed and image size is within max permitted size
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CheckImage(string path)
        {
            if (IsImageFile(path))
            {
                try
                {
                    Size maxTextureSize = Application.Instance.RenderDisplayManager.MaxTextureSize;

                    Size imageSize = GetImageSizeFromMetaData(path);
                    if (imageSize.Width <= maxTextureSize.Width && imageSize.Height <= maxTextureSize.Height)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Messager.ShowMessage(Messager.Mode.Exception,
                                         string.Format("Error on loading image: {0}", ex.Message));
                }
            }
            return false;
        }

        public static void AdjustImagePixelFormat(ref Bitmap image)
        {
            Bitmap temp = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(temp);

            g.DrawImage(image, new Point(0, 0));

            image.Dispose();
            image = temp;
        }

        public static Color ClearColor(ref Bitmap image, int x, int y)
        {
            return ClearColor(ref image, image.GetPixel(x, y));
        }

        public static Color ClearColor(ref Bitmap image)
        {
            return ClearColor(ref image, image.GetPixel(0, 0));
        }

        public static Color ClearColor(ref Bitmap image, Color colorToClear)
        {
            image.MakeTransparent(colorToClear);
            return colorToClear;
        }


        /// <summary>
        /// Checks if image extension is allowed
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static bool IsImageFile(string file)
        {
            if (!File.Exists(file))
                return false;

            // ToLower for string comparisons
            string fileLower = file.ToLower();

            // see if the file ends with one of our valid extensions
            foreach (string ext in AllowedImageExtensions)
                if (fileLower.EndsWith(ext))
                    return true;
            return false;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Reads and returns image size without creating a Bitmap, reading it from image header for BMP, GIF, JPG and PNG file formats.
        /// Code adapted and ported to C# from class ImageLoader written in VB.NET by
        /// Robert Chapin
        /// Chapin Information Services, Inc.  http://www.info-svc.com/
        /// </summary>
        /// <returns>Image size</returns>
        private static Size GetImageSizeFromMetaData(string filePath)
        {
            //Define constants
            const int BUFFER_SIZE = 4096;
            const int MINIMUM_FILE_LENGTH = 25;
            //Files smaller than this number of bytes are treated as invalid.


            Size Dimensions = Size.Empty;

            byte[] BMPHeader = {
                                   66,
                                   77
                               };
            byte[] GIFHeader = {
                                   71,
                                   73,
                                   70
                               };

            byte[] PNGHeader = {
                                   137,
                                   80,
                                   78,
                                   71,
                                   13,
                                   10,
                                   26,
                                   10
                               };


            int bufferedBytes;


            byte[] bBuf = new byte[BUFFER_SIZE];

            FileStream InStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            bufferedBytes = InStream.Read(bBuf, 0, BUFFER_SIZE);

            InStream.Close();

            if (bufferedBytes < MINIMUM_FILE_LENGTH)
                throw new Exception("Image file size must be bigger than 25Kb!");

            if (bBuf[0] == PNGHeader[0] && bBuf[1] == PNGHeader[1] && bBuf[2] == PNGHeader[2] && bBuf[3] == PNGHeader[3] &&
                bBuf[4] == PNGHeader[4] && bBuf[5] == PNGHeader[5] && bBuf[6] == PNGHeader[6] && bBuf[7] == PNGHeader[7])
            {
                //-----------PNG-FILE--------------------------------------------------------------

                Dimensions.Width = Mult(bBuf[16], bBuf[17], bBuf[18], bBuf[19], false);

                Dimensions.Height = Mult(bBuf[20], bBuf[21], bBuf[22], bBuf[23], false);
            }
            else if (bBuf[0] == GIFHeader[0] && bBuf[1] == GIFHeader[1] && bBuf[2] == GIFHeader[2])
            {
                //-----------GIF-FILE--------------------------------------------------------------

                Dimensions.Width = Mult(bBuf[6], bBuf[7], true);

                Dimensions.Height = Mult(bBuf[8], bBuf[9], true);
            }
            else if (bBuf[0] == BMPHeader[0] && bBuf[1] == BMPHeader[1])
            {
                //-----------BMP-FILE--------------------------------------------------------------

                Dimensions.Width = Mult(bBuf[18], bBuf[19], true);

                Dimensions.Height = Mult(bBuf[22], bBuf[23], true);
            }


            return Dimensions;
        }


        //----------------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------HELPER FUNCTIONS USED BY GetImageSizeFromMetadata FUNCTION-------------------------------------------------------------------
        /// 
        /// Coverts 2 Bytes into an Integer
        /// 
        /// The least-significant byte
        /// The most-significant byte
        /// Must be set to False when providing parameters in reverse order.
        /// 32-bit Signed Integer
        /// Parameters are in left-to-right (string) order.
        private static int Mult(byte lsb, byte msb, bool normalByteOrder)
        {
            int functionReturnValue = 0;
            if (normalByteOrder)
            {
                functionReturnValue = (Convert.ToInt32(msb) << 8) | Convert.ToInt32(lsb);
            }
            else
            {
                functionReturnValue = (Convert.ToInt32(lsb) << 8) | Convert.ToInt32(msb);
            }
            return functionReturnValue;
        }

        /// 
        /// Coverts 4 Bytes into an Integer
        /// 
        /// The least-significant byte
        /// The middle-second byte
        /// The middle-third byte
        /// The most-significant byte
        /// Must be set to False when providing parameters in reverse order.
        /// 32-bit Signed Integer
        /// Parameters are in left-to-right (string) order.
        private static int Mult(byte lsb, byte m2b, byte m3b, byte msb, bool normalByteOrder)
        {
            int functionReturnValue = 0;
            if (normalByteOrder)
            {
                functionReturnValue = (Convert.ToInt32(msb) << 24) | (Convert.ToInt32(m3b) << 16) |
                                      (Convert.ToInt32(m2b) << 8) | Convert.ToInt32(lsb);
            }
            else
            {
                functionReturnValue = (Convert.ToInt32(lsb) << 24) | (Convert.ToInt32(m2b) << 16) |
                                      (Convert.ToInt32(m3b) << 8) | Convert.ToInt32(msb);
            }
            return functionReturnValue;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}