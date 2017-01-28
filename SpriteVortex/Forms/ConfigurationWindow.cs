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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using SpriteVortex.Helpers;
using Vortex.Drawing;
using Vortex.Input;

namespace SpriteVortex
{
    public partial class ConfigurationWindow : KryptonForm
    {
        private float _lastCameraSpeedValue;
        private int _lastFilterModeSelectedIndex;

        private ColorU _lastBgColor;
        private ColorU _lastFrameRectColor;
        private ColorU _lastFrameRectHoveredColor;
        private ColorU _lastFrameRectSelectedColor;

        private ControlConfig _tempCameraDragConfig;
        private ControlConfig _tempSpriteMarkupConfig;
        private ControlConfig _tempSpriteSelectConfig;
        private ControlConfig _tempViewZoomConfig;


        public ConfigurationWindow()
        {
            InitializeComponent();
        }

        private void BtnConfirmConfigsClick(object sender, EventArgs e)
        {
            bool ok = true;


            if (txtCamSpeed.Text.Length > 0 && txtCamSpeed.Text != Configuration.CameraSpeed.ToString())
            {
                var validator = new Regex("^[1-9]+[0-9]*$");

                ok = !validator.IsMatch(txtCamSpeed.Text);
            }

            if (ok)
            {
                Configuration.CameraSpeed = float.Parse(txtCamSpeed.Text);

                Configuration.DragCameraControl = _tempCameraDragConfig ?? Configuration.DragCameraControl;
                Configuration.SpriteMarkUpControl = _tempSpriteMarkupConfig ?? Configuration.SpriteMarkUpControl;
                Configuration.SelectSpriteControl = _tempSpriteSelectConfig ?? Configuration.SelectSpriteControl;
                Configuration.ViewZoomControl = _tempViewZoomConfig ?? Configuration.ViewZoomControl;

                Configuration.OverwriteImageWhenTransparencyModified = radAlphaMode.Checked;

                Configuration.PackSpriteSheetWhenExportingSpriteMap = chkPackSpriteSheet.Checked;

                Configuration.ForcePowTwo = chkForcePowTwo.Checked;

                Configuration.ForceSquare = chkForceSquare.Checked;

                Configuration.Padding = (int) udPadding.Value;


                Configuration.WriteConfig();

                Close();
            }
            else
            {
                KryptonMessageBox.Show("Only numeric values are accepted!", "Error!", MessageBoxButtons.OK,
                                       MessageBoxIcon.Error);
            }
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            cmbTextureFilterMode.SelectedIndex = _lastFilterModeSelectedIndex;
            Configuration.CameraSpeed = _lastCameraSpeedValue;
            _tempCameraDragConfig = null;
            _tempSpriteMarkupConfig = null;
            _tempSpriteSelectConfig = null;

            Configuration.BackgroundColor = _lastBgColor;
            Configuration.FrameRectColor = _lastFrameRectColor;
            Configuration.HoverFrameRectColor = _lastFrameRectHoveredColor;
            Configuration.SelectedFrameRectColor = _lastFrameRectSelectedColor;

            Close();
        }

        private void ConfigurationWindowLoad(object sender, EventArgs e)
        {
            _lastCameraSpeedValue = Configuration.CameraSpeed;
            _lastFilterModeSelectedIndex = Configuration.TextureFilterMode.Equals(TextureFilter.Point) ? 0 : 1;
            _lastFrameRectColor = Configuration.FrameRectColor;
            _lastFrameRectHoveredColor = Configuration.HoverFrameRectColor;
            _lastFrameRectSelectedColor = Configuration.SelectedFrameRectColor;
            _lastBgColor = Configuration.BackgroundColor;

            txtCamSpeed.Text = _lastCameraSpeedValue.ToString();
            cmbTextureFilterMode.SelectedIndex = _lastFilterModeSelectedIndex;

            ColorPickerBg.SelectedColor = Configuration.BackgroundColor.ToColor();

            radAlphaMode.Checked = Configuration.OverwriteImageWhenTransparencyModified;

            radColorKeyMode.Checked = !radAlphaMode.Checked;

            chkPackSpriteSheet.Checked = Configuration.PackSpriteSheetWhenExportingSpriteMap;

            chkForcePowTwo.Checked = Configuration.ForcePowTwo;

            chkForceSquare.Checked = Configuration.ForceSquare;

            udPadding.Value = Configuration.Padding;

            ColorPickerFrameRect.SelectedColor = Configuration.FrameRectColor.ToColor();

            ColorPickerFrameRectHovered.SelectedColor = Configuration.HoverFrameRectColor.ToColor();

            ColorPickerFrameRectSelected.SelectedColor = Configuration.SelectedFrameRectColor.ToColor();

            InputConfigurationHelper.LoadCurrentControlConfigIntoInputControl(Configuration.DragCameraControl,
                                                                              InputDetectorMoveCamera);

            InputConfigurationHelper.LoadCurrentControlConfigIntoInputControl(Configuration.SpriteMarkUpControl,
                                                                              InputDetectorMarkupSprite);

            InputConfigurationHelper.LoadCurrentControlConfigIntoInputControl(Configuration.SelectSpriteControl,
                                                                              InputDetectorSelectSprite);

            InputConfigurationHelper.LoadCurrentControlConfigIntoInputControl(Configuration.ViewZoomControl,
                                                                              InputDetectorViewZoom);

            txtCamSpeed.Focus();
        }


        private void CmbTextureFilterModeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTextureFilterMode.SelectedIndex == 0)
            {
                Configuration.TextureFilterMode = TextureFilter.Point;
            }
            else
            {
                Configuration.TextureFilterMode = TextureFilter.Linear;
            }
        }


        private ControlConfig BuildNewControlConfig(InputControl2.ControlsChangedEventArgs newControlArgs)
        {
            ControlConfig newConfig = new ControlConfig();

            if (newControlArgs.NewControlKey != Keys.None)
            {
                Key? key = InputConfigurationHelper.ConvertKey(newControlArgs.NewControlKey);

                if (key != null)
                {
                    newConfig.Key = key;
                }
            }

            MouseButton button = InputConfigurationHelper.ConvertButton(newControlArgs.NewControlMouseButton);

            if (button != MouseButton.None)
            {
                newConfig.MouseButton = button;
            }

            return newConfig;
        }

        private void InputDetectorMoveCamera_ControlsChanged(InputControl2.ControlsChangedEventArgs args)
        {
            _tempCameraDragConfig = BuildNewControlConfig(args);
        }

        private void InputDetectorMarkupSprite_ControlsChanged(InputControl2.ControlsChangedEventArgs args)
        {
            _tempSpriteMarkupConfig = BuildNewControlConfig(args);
        }

        private void InputDetectorSelectSprite_ControlsChanged(InputControl2.ControlsChangedEventArgs args)
        {
            _tempSpriteSelectConfig = BuildNewControlConfig(args);
        }

        private void InputDetectorViewZoom_ControlsChanged(InputControl2.ControlsChangedEventArgs args)
        {
            _tempViewZoomConfig = BuildNewControlConfig(args);
        }


        private void ConfigurationWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            InputControl2.ResetInternalList();
        }

        private void ColorPickerBg_ColorChanged(ColorPicker.ColorChangedEventArgs args)
        {
            Configuration.BackgroundColor = new ColorU(args.NewColor);
        }

        private void ColorPickerFrameRect_ColorChanged(ColorPicker.ColorChangedEventArgs args)
        {
            Configuration.FrameRectColor = new ColorU(args.NewColor);
        }

        private void ColorPickerFrameRectHovered_ColorChanged(ColorPicker.ColorChangedEventArgs args)
        {
            Configuration.HoverFrameRectColor = new ColorU(args.NewColor);
        }

        private void ColorPickerFrameRectSelected_ColorChanged(ColorPicker.ColorChangedEventArgs args)
        {
            Configuration.SelectedFrameRectColor = new ColorU(args.NewColor);
        }
    }
}