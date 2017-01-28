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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Vortex.Drawing;
using Vortex.Drawing.Utils;
using PixelFormat = Vortex.Drawing.PixelFormat;
using ImageHelper = SpriteVortex.Helpers.ImageHelper;

namespace SpriteVortex
{
    public struct SpriteSheetResourceData
    {
        public string Name;
        public string Id;
        public Texture Texture;
        public string FullPath;

        public SpriteSheetResourceData(string imagePath, string id, Texture tex)
        {
            Name = Path.GetFileNameWithoutExtension(imagePath);
            Id = id;
            Texture = tex;
            FullPath = imagePath;
        }
    }


    public class ResourcesManager
    {
        public static ResourcesManager Instance
        {
            get { return instance ?? (instance = new ResourcesManager()); }
        }

        public void ChangeTextureFilterModeOfAllTextures(TextureFilter mode)
        {
            foreach (var spriteSheetImage in _spriteSheetsImages)
            {
                spriteSheetImage.Value.Texture.Options.Filter = mode;
            }
        }


        public void LoadResourceImages()
        {
            DirectoryInfo di = new DirectoryInfo(@"Data\GFX");

            if (!di.Exists)
            {
                Vortex.Debugging.Log.Error("Error loading resource images");
            }

            BitmapAtlas _resourceAtlas = new BitmapAtlas();


            foreach (var fileInfo in di.GetFiles())
            {
                if (fileInfo.Extension.Equals(".jpg") || fileInfo.Extension.Equals(".png"))
                {
                    try
                    {
                        _resourceAtlas.AddImage(Path.GetFileNameWithoutExtension(fileInfo.FullName), fileInfo.FullName);
                    }
                    catch (Exception e)
                    {
                        Vortex.Debugging.Log.Error(e.Message);
                    }
                }
            }

            _resourcesTexture = _resourceAtlas.ToTexture();

            _resourceAtlas.Dispose();
        }

        public Texture ResourceImageSheet
        {
            get { return _resourcesTexture; }
        }

        public Texture GetSpriteSheetImage(string id)
        {
            return _spriteSheetsImages.ContainsKey(id) ? _spriteSheetsImages[id].Texture : null;
        }

        public SpriteSheetResourceData LoadSpriteSheetImage(string imagePath, Bitmap image)
        {
            if (!(Image.IsAlphaPixelFormat(image.PixelFormat)))
            {
                ImageHelper.AdjustImagePixelFormat(ref image);

                if (Image.IsAlphaPixelFormat(image.PixelFormat))
                {
                    Messager.ShowMessage(Messager.Mode.Message, "Image converted to alpha pixel format");
                }
            }


            var spTex = new Texture(image);

            spTex.Options.Filter = Configuration.TextureFilterMode;

            var id = string.Format("{0}", _spriteSheetsImages.Count);


            var spriteSheetResourceData = new SpriteSheetResourceData(imagePath, id, spTex);

            _spriteSheetsImages.Add(id, spriteSheetResourceData);

            return spriteSheetResourceData;
        }

        public SpriteSheetResourceData LoadSpriteSheetFromSeparateImages(Bitmap[] images)
        {
            BitmapAtlas imageAtlas = new BitmapAtlas();


            for (int i = 0; i < images.Length; i++)
            {
                var image = images[i];
                if (!(Image.IsAlphaPixelFormat(image.PixelFormat)))
                {
                    ImageHelper.AdjustImagePixelFormat(ref image);
                }
                imageAtlas.AddImage(i.ToString(), image);
            }

            var spTex = imageAtlas.ToTexture(PixelFormat.DefaultAlpha);


            var finalSpTex = SpriteSheetAndTextureFuncs.TrimmByAlpha(spTex);

            finalSpTex.Options.Filter = Configuration.TextureFilterMode;

            spTex.Dispose();


            var id = string.Format("{0}", _spriteSheetsImages.Count);

            var spName = string.Format("AssembledSpriteSheet{0}", id);

            var spriteSheetResource = new SpriteSheetResourceData(spName, id, finalSpTex);

            _spriteSheetsImages.Add(id, spriteSheetResource);

            imageAtlas.Dispose();

            return spriteSheetResource;
        }

        public SpriteFont GetFont(string name)
        {
            if (_fonts.ContainsKey(name))
            {
                return _fonts[name];
            }

            SpriteFont font;
            if (name.Equals("DefaultFont", StringComparison.CurrentCultureIgnoreCase))
            {
                font = SpriteFont.Default;
                _fonts.Add(name, font);
            }
            else if (name.Equals("Console", StringComparison.CurrentCultureIgnoreCase))
            {
                font = SpriteFont.Console;
                _fonts.Add(name, font);
            }
            else if (name.Equals("SmallConsole", StringComparison.CurrentCultureIgnoreCase))
            {
                font = SpriteFont.SmallConsole;
                _fonts.Add(name, font);
            }
            else
            {
                try
                {
                    font = new SpriteFont(string.Format("Data\\FONTS\\{0}", name));
                    _fonts.Add(name, font);
                }
                catch (Exception ex)
                {
                    Vortex.Debugging.Log.Error(ex, "Error loading font resource");
                    font = null;
                }
            }

            return font;
        }

        public void DisposeAll()
        {
            foreach (var spriteFont in _fonts)
            {
                Vortex.Debugging.Log.Trace("Disposing: " + spriteFont.Key);
                spriteFont.Value.Dispose();
            }
            foreach (var spritesheet in _spriteSheetsImages)
            {
                Vortex.Debugging.Log.Trace("Disposing: " + spritesheet.Value.Name);
                spritesheet.Value.Texture.Dispose();
            }
            _spriteSheetsImages.Clear();
            _fonts.Clear();
            Vortex.Debugging.Log.Trace("Disposing Resource Atlas...");
            _resourcesTexture.Dispose();
        }

        public void DisposeSpriteSheetImage(string id)
        {
            var temp = _spriteSheetsImages[id];
            Vortex.Debugging.Log.Trace("DisposingSheet: " + temp.Name);
            _spriteSheetsImages.Remove(id);
            temp.Texture.Dispose();
        }


        private static ResourcesManager instance;
        private Dictionary<string, SpriteFont> _fonts;
        private readonly Dictionary<string, SpriteSheetResourceData> _spriteSheetsImages;

        private Texture _resourcesTexture;

        private ResourcesManager()
        {
            _fonts = new Dictionary<string, SpriteFont>();
            _spriteSheetsImages = new Dictionary<string, SpriteSheetResourceData>();
        }
    }
}