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
using System.IO;
using System.Xml;
using SpriteVortex.Helpers;
using Vortex.Drawing;
using Vortex.Input;

namespace SpriteVortex
{
    public static class ConfigIO
    {
        public class ConfigData
        {
            //-------------General-------------------
            public float CameraSpeed;
            public TextureFilter TextureFilterMode;
            public float FrameMargin;
            //---------------------------------------

            //-------------Style----------------------            
            public ColorU FrameRectColor;
            public ColorU HoverFrameRectColor;
            public ColorU SelectedFrameRectColor;
            public ColorU BackgroundColor;
            //----------------------------------------


            //----------------Exporting----------------------------
            public bool OverwriteImageWhenTransparencyModified;

            public bool PackSpriteSheetWhenExportingSpriteMap;

            public bool ForcePowTwo;

            public bool ForceSquare;

            public int Padding;

            public ControlConfig SpriteMarkUpControl;
            public ControlConfig DragCameraControl;
            public ControlConfig SelectSpriteControl;
            public ControlConfig ViewZoomControl;
        }


        public static ConfigData LoadConfig(string file)
        {
            ConfigData readConfigData = new ConfigData();

            if (!File.Exists(file))
            {
                var configDataToWrite = new ConfigData();

                configDataToWrite = new ConfigData()
                                        {
                                            CameraSpeed = 400,
                                            BackgroundColor = ColorU.DodgerBlue,
                                            FrameRectColor = ColorU.GreenYellow,
                                            HoverFrameRectColor = ColorU.Tomato,
                                            SelectedFrameRectColor = ColorU.Yellow,
                                            TextureFilterMode = TextureFilter.Point,
                                            OverwriteImageWhenTransparencyModified = true,
                                            PackSpriteSheetWhenExportingSpriteMap = false,
                                            ForcePowTwo = false,
                                            ForceSquare = false,
                                            Padding = 0,
                                            FrameMargin = 20,
                                            SpriteMarkUpControl =
                                                new ControlConfig()
                                                    {Key = Key.LeftControl, MouseButton = MouseButton.Right},
                                            DragCameraControl = new ControlConfig() {MouseButton = MouseButton.Right},
                                            SelectSpriteControl = new ControlConfig() {MouseButton = MouseButton.Left},
                                            ViewZoomControl =
                                                new ControlConfig()
                                                    {Key = Key.LeftControl, MouseButton = MouseButton.Left}
                                        };

                WriteConfig(configDataToWrite, file);
            }

            XmlDocument configDoc = new XmlDocument();
            configDoc.Load(file);

            string cameraSpeedString =
                configDoc.SelectSingleNode("//Settings/GeneralSettings/CameraSpeed/@Value").Value;

            string backGroundColorString =
                configDoc.SelectSingleNode("//Settings/GeneralSettings/BackgroundColor/@Value").Value;

            string frameRectColorString =
                configDoc.SelectSingleNode("//Settings/GeneralSettings/FrameRectColor/@Value").Value;

            string hoveFrameRectColorString =
                configDoc.SelectSingleNode("//Settings/GeneralSettings/HoverFrameRectColor/@Value").Value;

            string selectedFrameRectColorString =
                configDoc.SelectSingleNode("//Settings/GeneralSettings/SelectedFrameRectColor/@Value").Value;

            string textureFilterString =
                configDoc.SelectSingleNode("//Settings/GeneralSettings/TextureFilterMode/@Value").Value;

            string frameMarginString =
                configDoc.SelectSingleNode("//Settings/GeneralSettings/FrameMargin/@Value").Value;


            string spriteMarkupControlKeyString =
                configDoc.SelectSingleNode("//Settings/CommandSettings/SpriteMarkup/@Key").Value;

            string spriteMarkupControlButtonString =
                configDoc.SelectSingleNode("//Settings/CommandSettings/SpriteMarkup/@Button").Value;

            string dragCameraControlKeyString =
                configDoc.SelectSingleNode("//Settings/CommandSettings/DragCamera/@Key").Value;

            string dragCameraControlButtonString =
                configDoc.SelectSingleNode("//Settings/CommandSettings/DragCamera/@Button").Value;

            string selectSpriteControlKeyString =
                configDoc.SelectSingleNode("//Settings/CommandSettings/SelectSprite/@Key").Value;

            string selectSpriteControlButtonString =
                configDoc.SelectSingleNode("//Settings/CommandSettings/SelectSprite/@Button").Value;

            string viewZoomControlKeyString =
                configDoc.SelectSingleNode("//Settings/CommandSettings/ViewZoom/@Key").Value;

            string viewZoomControlButtonString =
                configDoc.SelectSingleNode("//Settings/CommandSettings/ViewZoom/@Button").Value;

            string overwriteImageWhenTransparencyModifiedString =
                configDoc.SelectSingleNode("//Settings/ExportingSettings/ImageOverwriteWhenTransparencyModified/@Value")
                    .Value;

            string PackSpriteSheetWhenExportingSpriteMapString =
                configDoc.SelectSingleNode("//Settings/ExportingSettings/PackSpriteSheet/@Value")
                    .Value;

            string ForcePowTwoString =
                configDoc.SelectSingleNode("//Settings/ExportingSettings/ForcePowTwo/@Value").Value;

            string ForceSquareString =
                configDoc.SelectSingleNode("//Settings/ExportingSettings/ForceSquare/@Value").Value;

            string PaddingString = configDoc.SelectSingleNode("//Settings/ExportingSettings/Padding/@Value").Value;

            readConfigData.CameraSpeed = Parser.ParseFloat(cameraSpeedString);

            readConfigData.BackgroundColor = Parser.ParseColorU(backGroundColorString);

            readConfigData.FrameRectColor = Parser.ParseColorU(frameRectColorString);

            readConfigData.HoverFrameRectColor = Parser.ParseColorU(hoveFrameRectColorString);

            readConfigData.SelectedFrameRectColor = Parser.ParseColorU(selectedFrameRectColorString);

            readConfigData.TextureFilterMode = Parser.ParseEnum<TextureFilter>(textureFilterString);

            readConfigData.FrameMargin = Parser.ParseFloat(frameMarginString);

            readConfigData.SpriteMarkUpControl = Parser.ParseControl(spriteMarkupControlKeyString,
                                                                     spriteMarkupControlButtonString);
            readConfigData.DragCameraControl = Parser.ParseControl(dragCameraControlKeyString,
                                                                   dragCameraControlButtonString);

            readConfigData.SelectSpriteControl = Parser.ParseControl(selectSpriteControlKeyString,
                                                                     selectSpriteControlButtonString);

            readConfigData.ViewZoomControl = Parser.ParseControl(viewZoomControlKeyString, viewZoomControlButtonString);

            readConfigData.OverwriteImageWhenTransparencyModified =
                Parser.ParseBool(overwriteImageWhenTransparencyModifiedString);

            readConfigData.PackSpriteSheetWhenExportingSpriteMap =
                Parser.ParseBool(PackSpriteSheetWhenExportingSpriteMapString);

            readConfigData.ForcePowTwo = Parser.ParseBool(ForcePowTwoString);

            readConfigData.ForceSquare = Parser.ParseBool(ForceSquareString);

            readConfigData.Padding = Parser.ParseInt(PaddingString);

            return readConfigData;
        }


        public static void WriteConfig(ConfigData data, string file)
        {
            XMLWriter configWriter = new XMLWriter(file);

            configWriter.CreateDocWithRoot("Settings", "Configuration definition of Sprite Vortex");
            //-----------------------------------------------------------------------------------------------------
            configWriter.OpenNewElement("GeneralSettings");


            //-----------------------------------------------------------------------------------------------------
            configWriter.AddCompleteElementWithAttribute("CameraSpeed", "Value",
                                                         GlobalizationHelper.FormatNumber(data.CameraSpeed));
            configWriter.AddCompleteElementWithAttribute("BackgroundColor", "Value",
                                                         data.BackgroundColor.Argb.ToString());
            configWriter.AddCompleteElementWithAttribute("FrameRectColor", "Value", data.FrameRectColor.Argb.ToString());
            configWriter.AddCompleteElementWithAttribute("HoverFrameRectColor", "Value",
                                                         data.HoverFrameRectColor.Argb.ToString());
            configWriter.AddCompleteElementWithAttribute("SelectedFrameRectColor", "Value",
                                                         data.SelectedFrameRectColor.Argb.ToString());
            configWriter.AddCompleteElementWithAttribute("TextureFilterMode", "Value",
                                                         Enum.GetName(typeof (TextureFilter), data.TextureFilterMode));
            configWriter.AddCompleteElementWithAttribute("FrameMargin", "Value",
                                                         GlobalizationHelper.FormatNumber(data.FrameMargin));
            //-----------------------------------------------------------------------------------------------------

            configWriter.CloseCurrentElement();
            //-----------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------

            configWriter.OpenNewElement("CommandSettings");
            //-----------------------------------------------------------------------------------------------------

            configWriter.OpenNewElement("SpriteMarkup");
            configWriter.AddAttribute("Key",
                                      data.SpriteMarkUpControl.Key != null
                                          ? data.SpriteMarkUpControl.Key.ToString()
                                          : "none");
            configWriter.AddAttribute("Button", Enum.GetName(typeof (MouseButton), data.SpriteMarkUpControl.MouseButton));
            configWriter.CloseCurrentElement();
            //-----------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------
            configWriter.OpenNewElement("DragCamera");
            configWriter.AddAttribute("Key",
                                      data.DragCameraControl.Key != null
                                          ? data.DragCameraControl.Key.ToString()
                                          : "none");
            configWriter.AddAttribute("Button", Enum.GetName(typeof (MouseButton), data.DragCameraControl.MouseButton));
            configWriter.CloseCurrentElement();
            //-----------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------
            configWriter.OpenNewElement("SelectSprite");
            configWriter.AddAttribute("Key",
                                      data.SelectSpriteControl.Key != null
                                          ? data.SelectSpriteControl.Key.ToString()
                                          : "none");
            configWriter.AddAttribute("Button", Enum.GetName(typeof (MouseButton), data.SelectSpriteControl.MouseButton));
            configWriter.CloseCurrentElement();
            //-----------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------
            configWriter.OpenNewElement("ViewZoom");
            configWriter.AddAttribute("Key",
                                      data.ViewZoomControl.Key != null ? data.ViewZoomControl.Key.ToString() : "none");
            configWriter.AddAttribute("Button", Enum.GetName(typeof (MouseButton), data.ViewZoomControl.MouseButton));
            configWriter.CloseCurrentElement();
            //-----------------------------------------------------------------------------------------------------


            configWriter.CloseCurrentElement();

            //-----------------------------------------------------------------------------------------------------
            configWriter.OpenNewElement("ExportingSettings");

            //-----------------------------------------------------------------------------------------------------
            configWriter.OpenNewElement("ImageOverwriteWhenTransparencyModified");
            configWriter.AddAttribute("Value", data.OverwriteImageWhenTransparencyModified.ToString());
            configWriter.CloseCurrentElement();
            //-----------------------------------------------------------------------------------------------------

            //-----------------------------------------------------------------------------------------------------
            configWriter.OpenNewElement("PackSpriteSheet");
            configWriter.AddAttribute("Value", data.PackSpriteSheetWhenExportingSpriteMap.ToString());
            configWriter.CloseCurrentElement();
            //-----------------------------------------------------------------------------------------------------

            //-----------------------------------------------------------------------------------------------------
            configWriter.OpenNewElement("ForcePowTwo");
            configWriter.AddAttribute("Value", data.ForcePowTwo.ToString());
            configWriter.CloseCurrentElement();
            //-----------------------------------------------------------------------------------------------------


            //-----------------------------------------------------------------------------------------------------
            configWriter.OpenNewElement("ForceSquare");
            configWriter.AddAttribute("Value", data.ForceSquare.ToString());
            configWriter.CloseCurrentElement();
            //-----------------------------------------------------------------------------------------------------

            //-----------------------------------------------------------------------------------------------------
            configWriter.OpenNewElement("Padding");
            configWriter.AddAttribute("Value", data.Padding.ToString());
            configWriter.CloseCurrentElement();
            //-----------------------------------------------------------------------------------------------------

            configWriter.CloseCurrentElement();

            configWriter.Save();
        }
    }
}