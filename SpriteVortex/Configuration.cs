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
using Vortex.Input;

namespace SpriteVortex
{
    public class ControlConfig
    {
        public Key? Key;
        public MouseButton MouseButton;

        public ControlConfig()
        {
            Key = null;
            MouseButton = MouseButton.None;
        }
    }


    internal static class Configuration
    {
        public static string SETTINGS_FILE_NAME = "Settings.xml";

        static Configuration()
        {
            ReadConfig(SETTINGS_FILE_NAME);
        }


        public static void ReadConfig(string file)
        {
            var configData = ConfigIO.LoadConfig(file);

            CameraSpeed = configData.CameraSpeed;
            BackgroundColor = configData.BackgroundColor;
            FrameRectColor = configData.FrameRectColor;
            HoverFrameRectColor = configData.HoverFrameRectColor;
            SelectedFrameRectColor = configData.SelectedFrameRectColor;
            TextureFilterMode = configData.TextureFilterMode;
            FrameMargin = configData.FrameMargin;
            SpriteMarkUpControl = configData.SpriteMarkUpControl;
            DragCameraControl = configData.DragCameraControl;
            SelectSpriteControl = configData.SelectSpriteControl;
            ViewZoomControl = configData.ViewZoomControl;
            OverwriteImageWhenTransparencyModified = configData.OverwriteImageWhenTransparencyModified;
            PackSpriteSheetWhenExportingSpriteMap = configData.PackSpriteSheetWhenExportingSpriteMap;
            ForcePowTwo = configData.ForcePowTwo;
            ForceSquare = configData.ForceSquare;
            Padding = configData.Padding;
        }

        public static void WriteConfig()
        {
            var configData = new ConfigIO.ConfigData();

            configData.CameraSpeed = CameraSpeed;
            configData.BackgroundColor = BackgroundColor;
            configData.FrameRectColor = FrameRectColor;
            configData.HoverFrameRectColor = HoverFrameRectColor;
            configData.SelectedFrameRectColor = SelectedFrameRectColor;
            configData.TextureFilterMode = TextureFilterMode;
            configData.FrameMargin = FrameMargin;
            configData.SpriteMarkUpControl = SpriteMarkUpControl;
            configData.DragCameraControl = DragCameraControl;
            configData.SelectSpriteControl = SelectSpriteControl;
            configData.ViewZoomControl = ViewZoomControl;
            configData.OverwriteImageWhenTransparencyModified = OverwriteImageWhenTransparencyModified;
            configData.PackSpriteSheetWhenExportingSpriteMap = PackSpriteSheetWhenExportingSpriteMap;
            configData.ForcePowTwo = ForcePowTwo;
            configData.ForceSquare = ForceSquare;
            configData.Padding = Padding;

            ConfigIO.WriteConfig(configData, SETTINGS_FILE_NAME);
        }

        //-----------------------------Exporting Options----------------------------

        public static bool OverwriteImageWhenTransparencyModified;

        public static bool PackSpriteSheetWhenExportingSpriteMap;

        public static bool ForcePowTwo;

        public static bool ForceSquare;

        public static int Padding;

        //--------------------------- GUI Options ----------------------------------
        public static float CameraSpeed;

        public static ColorU BackgroundColor;
        public static ColorU HoverFrameRectColor;
        public static ColorU FrameRectColor;
        public static ColorU SelectedFrameRectColor;

        public static TextureFilter TextureFilterMode
        {
            get { return _textureFilterMode; }
            set
            {
                _textureFilterMode = value;
                Application.Instance.SetTextureFiltering(_textureFilterMode);
            }
        }

        public static float FrameMargin;

        private static TextureFilter _textureFilterMode;

        //---------------------------- Key and Mouse Settings ---------------------

        public static ControlConfig SpriteMarkUpControl;

        public static ControlConfig DragCameraControl;

        public static ControlConfig SelectSpriteControl;

        public static ControlConfig ViewZoomControl;
    }
}