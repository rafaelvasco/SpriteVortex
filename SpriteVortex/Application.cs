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
using System.Windows.Forms;
using System.Xml;
using SpriteVortex.Helpers.GifComponents;
using SpriteVortex.Helpers.GifComponents.Enums;
using SpriteVortex.Properties;
using Vortex.Drawing;
using ImageHelper = SpriteVortex.Helpers.ImageHelper;

namespace SpriteVortex
{
    public class Application
    {
        public static Application Instance
        {
            get { return instance ?? (instance = new Application()); }
        }

        public static string Version = "v1.4 beta";

        public Size MaxTexSize
        {
            get { return RenderDisplayManager.MaxTextureSize; }
        }

        public RenderDisplayManager RenderDisplayManager { get; private set; }

        public ResourcesManager ResourcesManager { get; private set; }

        public SpriteSheet CurrentShownSpriteSheet
        {
            get { return _currentShownSpriteSheet; }
        }

        public Animation CurrentShownAnimation
        {
            get { return _currentShownAnimation; }
        }

        public Dictionary<string, SpriteSheet> SpriteSheets
        {
            get { return _spriteSheets; }
        }

        public Dictionary<string, Animation> Animations
        {
            get { return _animations; }
        }

        public MainWindow MainWindow { get; private set; }

        public bool Initialized { get; private set; }

        public void Initialize(MainWindow mainWindow)
        {
            RenderDisplayManager = RenderDisplayManager.Instance;
            ResourcesManager = ResourcesManager.Instance;


            RenderDisplayManager.Initialize(mainWindow.MainPanel);

            ResourcesManager.LoadResourceImages();

            var mainDisplay = RenderDisplayManager.AddTarget(new MainDisplay("MainDisplay", mainWindow.MainPanel));
            var animPreview =
                RenderDisplayManager.AddTarget(new AnimPreviewDisplay("AnimPreview", mainWindow.AnimPrevPanel));
            var animConfig = RenderDisplayManager.AddTarget(new AnimConfigDisplay("AnimConfig", mainWindow.ConfigPanel));

            Input.Initialize(mainDisplay, animPreview, animConfig);

            mainDisplay.SetUpInputEvents();
            animConfig.SetUpInputEvents();
            animPreview.SetUpInputEvents();

            mainDisplay.InitializeGUI();
            animConfig.InitializeGUI();
            animPreview.InitializeGUI();

            ((MainDisplay) mainDisplay).InitializeStateMachine();

            MainWindow = mainWindow;

            Initialized = true;
        }

        public SpriteSheet GetSpriteSheet(string spId)
        {
            if (_spriteSheets.ContainsKey(spId))
            {
                return _spriteSheets[spId];
            }
            return null;
        }

        public Animation GetAnimation(string animId)
        {
            if (_animations.ContainsKey(animId))
            {
                return _animations[animId];
            }
            return null;
        }

        public T GetDisplay<T>(string name) where T : RenderDisplay
        {
            return RenderDisplayManager[name] as T;
        }

        public RenderDisplay GetDisplay(string name)
        {
            return RenderDisplayManager[name];
        }

        public void Update(int tickInterval)
        {
            Input.Update();
            float frameTime = tickInterval*0.001f;
            RenderDisplayManager.Update(frameTime);
            Input.PostUpdate();
        }

        public void Terminate()
        {
            Input.Dispose();
            ResourcesManager.DisposeAll();
            RenderDisplayManager.Dispose();
        }

        public void UnloadCurrentShownAnimation()
        {
            if (_currentShownAnimation == null)
            {
                return;
            }
            GetDisplay<AnimConfigDisplay>("AnimConfig").UnloadAnimation();
            GetDisplay<AnimPreviewDisplay>("AnimPreview").UnloadAnimation();

            _currentShownAnimation = null;
        }

        public void UnloadCurrentShownSpriteSheet()
        {
            if (_currentShownSpriteSheet == null)
            {
                return;
            }

            GetDisplay<MainDisplay>("MainDisplay").UnloadSpriteSheet();

            _currentShownSpriteSheet = null;
        }


        public void LoadSpriteSheetDirectly(string path, bool addAnim)
        {
            MakeSpriteSheet(path, addAnim);
        }

        public void LoadSpriteSheet()
        {
            LoadSpriteSheet(string.Empty);
        }

        public void LoadSpriteSheet(string initialDirectory)
        {
            var path = GetImagePath(initialDirectory);
            if (path.Length > 0)
            {
                MakeSpriteSheet(path, true);
            }
        }

        public void LoadSpriteSheetFromSeparateImages()
        {
            LoadSpriteSheetFromSeparateImages(string.Empty);
        }


        public void LoadSpriteSheetFromSeparateImages(string initialDirectory)
        {
            var fileNames = GetImagePathList(initialDirectory);

            if (fileNames != null)
            {
                Bitmap[] images = new Bitmap[fileNames.Length];
                for (int i = 0; i < fileNames.Length; i++)
                {
                    var file = fileNames[i];
                    images[i] = new Bitmap(file);
                }
                MakeSpriteSheetFromSeparateImages(images, true);

                foreach (var bitmap in images)
                {
                    bitmap.Dispose();
                }
            }
        }


        public void ExportSpriteSheetImage()
        {
            var error = false;


            if (CurrentShownSpriteSheet != null)
            {
                try
                {
                    var path = ShowCompleteSaveToDialog("Choose location to save image...", @"PNG Image(.png)|*.png");

                    if (path == null)
                    {
                        return;
                    }

                    if (path.Length > 0)
                    {
                        Exporter.ExportSpriteSheetImage(CurrentShownSpriteSheet, path);
                    }
                    else
                    {
                        Messager.ShowMessage(Messager.Mode.Exception,
                                             string.Format("Nothing exported. The path is invalid or empty!"));
                    }
                }
                catch (Exception ex)
                {
                    error = true;
                    Messager.ShowMessage(Messager.Mode.Exception,
                                         string.Format("Could not export image: {0}", ex.Message));
                }

                if (!error)
                {
                    Messager.ShowMessage(Messager.Mode.Message, "Image exported sucessfully!");
                }
            }
            else
            {
                Messager.ShowMessage(Messager.Mode.Exception,
                                     "No spritesheet loaded! Please load a sprite sheet first by clicking the button Import Spritesheet on topbar");
            }
        }

        public void ExportFramesImages()
        {
            try
            {
                if (CurrentShownSpriteSheet != null)
                {
                    if (CurrentShownSpriteSheet.Frames.Count > 0)
                    {
                        var path = ShowSaveToFolderDialog("Choose location to save images...");

                        if (path == null)
                        {
                            return;
                        }

                        int id = 0;

                        if (path.Length > 0)
                        {
                            foreach (var spriteSheetFrame in CurrentShownSpriteSheet.Frames)
                            {
                                Exporter.ExportSpriteSheetImage(CurrentShownSpriteSheet,
                                                                spriteSheetFrame.BoundingRect.ToRectangle(),
                                                                path + "\\frame" + id + ".png");

                                id += 1;
                            }
                            Messager.ShowMessage(Messager.Mode.Message, "Images exported sucessfully!");
                        }
                        else
                        {
                            Messager.ShowMessage(Messager.Mode.Exception,
                                                 "Nothing exported. The path is invalid or empty!");
                        }
                    }
                    else
                    {
                        Messager.ShowMessage(Messager.Mode.Exception,
                                             "The selected spritesheet has no frames defined!. Either click on 'Alpha Cut' to automatically find them or mannually select them with right mouse button.");
                    }
                }
                else
                {
                    Messager.ShowMessage(Messager.Mode.Exception,
                                         "No spritesheet loaded! Please load a sprite sheet first by clicking the button Import Spritesheet on topbar");
                }
            }
            catch (Exception ex)
            {
                Messager.ShowMessage(Messager.Mode.Exception, string.Format("Could not export images: {0}", ex.Message));
            }
        }


        public void ExportSpriteMapXML()
        {
            if (CurrentShownSpriteSheet == null)
            {
                Messager.ShowMessage(Messager.Mode.Exception,
                                     "No spritesheet loaded! Please load a sprite sheet first by clicking the button 'Import Spritesheet' on topbar");
                return;
            }


            var browser = MainWindow.ObjectsBrowser;


            if (browser.SelectedNode.Level == 0)
            {
                ExportComplete(browser.SelectedNode.Tag.ToString());
            }
            else
            {
                ExportSingle(browser.SelectedNode.Tag.ToString());
            }
        }


        public void ExportSingle(string animId)
        {
            if (_animations[animId].Frames.Count == 0)
            {
                Messager.ShowMessage(Messager.Mode.Exception, "No frames to export. The animation is empty!");
                return;
            }

            List<Animation> animsToExport = new List<Animation>();

            var spSheetId = _animations[animId].SpriteSheetId;

            animsToExport.Add(_animations[animId]);

            try
            {
                var path = ShowCompleteSaveToDialog("Choose location to export XML...", @"XML File(.xml)|*.xml");

                if (path == null)
                {
                    return;
                }

                if (path.Length > 0)
                {
                    var exported = Exporter.ExportSpriteMapping(_spriteSheets[spSheetId], animsToExport, path);
                    if (exported == ExportResult.ExportedAll)
                    {
                        Messager.ShowMessage(Messager.Mode.Message,
                                             "SpriteMap and single animation exported successfully!");
                    }
                }
                else
                {
                    Messager.ShowMessage(Messager.Mode.Exception,
                                         string.Format("Nothing exported. The path is invalid or empty!"));
                }
            }
            catch (Exception ex)
            {
                Messager.ShowMessage(Messager.Mode.Exception,
                                     string.Format("Error on SpriteMap export: {0}", ex.Message));
                Vortex.Debugging.Log.Error(ex, "Error on SpriteMap export");
            }
        }

        public void ExportComplete(string spSheetId)
        {
            var spSheet = _spriteSheets[spSheetId];

            var animations = GetAnimationsFromSpriteSheet(spSheet);

            try
            {
                var path = ShowCompleteSaveToDialog("Choose location to export XML...", @"XML File(.xml)|*.xml");

                if (path == null)
                {
                    return;
                }

                if (path.Length > 0)
                {
                    var exported = Exporter.ExportSpriteMapping(spSheet, animations, path);
                    if (exported == ExportResult.ExportedAll)
                    {
                        Messager.ShowMessage(Messager.Mode.Message,
                                             "SpriteMap and all animations exported successfully!");
                    }
                    else if (exported == ExportResult.ExportedOnlySpriteMap)
                    {
                        Messager.ShowMessage(Messager.Mode.Message, "SpriteMap exported successfully!");
                    }
                    else if (exported == ExportResult.Nothing)
                    {
                        Messager.ShowMessage(Messager.Mode.Message, "Nothing exported ! No Sprites defined!");
                    }
                }
                else
                {
                    Messager.ShowMessage(Messager.Mode.Exception,
                                         string.Format("Nothing exported. The path is invalid or empty!"));
                }
            }
            catch (Exception ex)
            {
                Messager.ShowMessage(Messager.Mode.Exception,
                                     string.Format("Error on SpriteMap export: {0}", ex.Message));
                Vortex.Debugging.Log.Error(ex, "Error on SpriteMap export");
            }
        }

        public void ExportAnimationAsGif()
        {
            var error = false;

            if (CurrentShownSpriteSheet == null)
            {
                Messager.ShowMessage(Messager.Mode.Exception,
                                     "No spritesheet loaded! Please load a sprite sheet first by clicking the button Import Spritesheet on topbar");
                return;
            }

            if (CurrentShownAnimation != null)
            {
                if (CurrentShownAnimation.Frames.Count == 0)
                {
                    Messager.ShowMessage(Messager.Mode.Exception, "No frames to export. The animation is empty!");
                    return;
                }

                var path = ShowCompleteSaveToDialog("Choose location to save GIF", @"Animated GIF(.gif)|*.gif");

                if (path == null)
                {
                    return;
                }

                if (path.Length == 0)
                {
                    Messager.ShowMessage(Messager.Mode.Exception,
                                         string.Format("Nothing exported. The path is invalid or empty!"));
                    return;
                }

                try
                {
                    Size frameSize = Size.Empty;
                    Point offSetMin = Point.Empty;
                    Point offSetMax = Point.Empty;

                    foreach (var frame in CurrentShownAnimation.Frames)
                    {
                        if (frame.SpriteFrame.Width > frameSize.Width)
                        {
                            frameSize.Width = frame.SpriteFrame.Width;
                        }
                        if (frame.SpriteFrame.Height > frameSize.Height)
                        {
                            frameSize.Height = frame.SpriteFrame.Height;
                        }
                    }

                    foreach (var frame in CurrentShownAnimation.Frames)
                    {
                        if (frame.OffSetX < offSetMin.X)
                        {
                            offSetMin.X = frame.OffSetX;
                        }
                        if (frame.OffSetY < offSetMin.Y)
                        {
                            offSetMin.Y = frame.OffSetY;
                        }

                        if (frame.OffSetX > offSetMax.X)
                        {
                            offSetMax.X = frame.OffSetX;
                        }
                        if (frame.OffSetY > offSetMax.Y)
                        {
                            offSetMax.Y = frame.OffSetY;
                        }
                    }

                    frameSize.Width += Math.Abs(offSetMin.X) + offSetMax.X;

                    frameSize.Height += Math.Abs(offSetMin.Y) + offSetMax.Y;

                    frameSize.Width = frameSize.Width > frameSize.Height ? frameSize.Width : frameSize.Height;

                    frameSize.Height = frameSize.Width;

                    int repeatCount = CurrentShownAnimation.Loop ? 0 : 1;

                    ColourTableStrategy strategy = ColourTableStrategy.UseGlobal;


                    int quality = 10;

                    AnimatedGifEncoder animatedGifEncoder = new AnimatedGifEncoder
                                                                {
                                                                    LogicalScreenSize = frameSize,
                                                                    RepeatCount = repeatCount,
                                                                    ColourTableStrategy = strategy,
                                                                    SamplingFactor = quality,
                                                                };

                    var texture = CurrentShownSpriteSheet.Texture;

                    animatedGifEncoder.Transparent = Color.Black;
                    foreach (var frame in CurrentShownAnimation.Frames)
                    {
                        Bitmap frameBitMap = texture.Surface.ToBitmap(frame.SpriteFrame.Region);

                        GifFrame gifFrame = new GifFrame(frameBitMap);

                        gifFrame.Delay = 100*((int) (frame.FrameDuration));

                        if (gifFrame.Delay < 1)
                        {
                            gifFrame.Delay = 1;
                        }

                        gifFrame.Position =
                            new Point((int) ((frameSize.Width/2f - frame.SpriteFrame.Width/2f) + frame.OffSetX),
                                      (int) ((frameSize.Height/2f - frame.SpriteFrame.Height/2f) + frame.OffSetY));


                        animatedGifEncoder.AddFrame(gifFrame);
                    }


                    animatedGifEncoder.WriteToFile(path);

                    animatedGifEncoder.Dispose();
                }
                catch (Exception ex)
                {
                    error = true;
                    Messager.ShowMessage(Messager.Mode.Exception,
                                         string.Format("Error on exporting GIF: {0}", ex.Message));
                    Vortex.Debugging.Log.Error(ex, "Error on exporting GIF");
                }
                finally
                {
                    if (!error)
                    {
                        Messager.ShowMessage(Messager.Mode.Message, "GIF created successfully!");
                    }
                }
            }
            else
            {
                Messager.ShowMessage(Messager.Mode.Exception, "No animations to export!");
            }
        }

        public void ShowSpriteSheet(string spId)
        {
            if (_currentShownSpriteSheet != null)
            {
                if (!_currentShownSpriteSheet.Id.Equals(spId))
                {
                    UnloadCurrentShownSpriteSheet();
                }
                else
                {
                    return;
                }
            }

            _currentShownSpriteSheet = _spriteSheets[spId];

            GetDisplay<MainDisplay>("MainDisplay").SetSpriteSheet(_currentShownSpriteSheet);


            if (CurrentShownAnimation != null)
            {
                UnloadCurrentShownAnimation();
            }
        }

        public void RemoveSpriteSheet(string spSheetId)
        {
            if (GetDisplay<MainDisplay>("MainDisplay").CurrentSpriteSheet.Id.Equals(spSheetId))
            {
                UnloadCurrentShownSpriteSheet();
            }
            _spriteSheets.Remove(spSheetId);
            ResourcesManager.DisposeSpriteSheetImage(spSheetId);
        }

        public void MakeAnimation()
        {
            MakeAnimation("Animation" + _animations.Count, 20f, true, false);
        }

        public void MakeAnimation(string name, float frameRate, bool loop, bool pingPong)
        {
            if (Guard.CheckNull(_currentShownSpriteSheet))
            {
                Messager.ShowMessage(Messager.Mode.Exception,
                                     "Before creating an animation please import a sprite sheet image clicking on button 'Import Spritesheet' on topbar");
                return;
            }

            Animation animation = new Animation(name, string.Format("{0}{1}",
                                                                    _currentShownSpriteSheet.Id,
                                                                    _animations.Count),
                                                _currentShownSpriteSheet.Id)
                                      {FrameRate = frameRate, Loop = loop, PingPong = pingPong};

            _animations.Add(animation.Id, animation);

            ShowAnimation(animation.Id);


            var eventArgs = new AppEventArgs
                                {
                                    Type = EventType.AddedAnimation,
                                    AnimationModified = animation,
                                    SpriteSheetModified = _currentShownSpriteSheet
                                };

            MainWindow.UpdateBrowser(eventArgs);
        }

        public void AddFramesToAnimation(List<Frame> frames)
        {
            if (Guard.CheckNull(_currentShownSpriteSheet))
            {
                Messager.ShowMessage(Messager.Mode.Exception,
                                     "No spritesheet loaded! Please load a sprite sheet first by clicking the button Import Spritesheet on topbar");
                return;
            }


            if (Guard.CheckNull(CurrentShownAnimation))
            {
                Messager.ShowMessage(Messager.Mode.Exception,
                                     "There's no animations created! Please create one by clicking on 'New Animation'");
                return;
            }

            var animConfig = GetDisplay<AnimConfigDisplay>("AnimConfig");


            animConfig.AddFramesToAnimationStrip(frames);

            _currentShownSpriteSheet.UnselectAllFrames();
        }

        public List<Frame> MakeSpriteFrames(List<SpriteSheetFrame> frameRects)
        {
            if (_currentShownSpriteSheet == null)
            {
                return null;
            }

            List<Frame> spriteFrames = new List<Frame>();

            foreach (var frameRect in frameRects)
            {
                var sprite = new Sprite(_currentShownSpriteSheet.Texture, frameRect.BoundingRect);

                var spriteFrame = new Frame(sprite, frameRect.Id);

                spriteFrames.Add(spriteFrame);
            }

            return spriteFrames;
        }

        public void RemoveAnimation(string animId)
        {
            if (CurrentShownAnimation != null && CurrentShownAnimation.Id.Equals(animId))
            {
                UnloadCurrentShownAnimation();
            }

            _animations.Remove(animId);
        }

        public void RenameAnimation(string animId, string newName)
        {
            _animations[animId].Name = newName;
        }

        public void ShowAnimation(string animId)
        {
            if (_currentShownAnimation != null)
            {
                if (!CurrentShownAnimation.Id.Equals(animId))
                {
                    UnloadCurrentShownAnimation();
                }
                else
                {
                    return;
                }
            }

            ShowSpriteSheet(_animations[animId].SpriteSheetId);

            _currentShownAnimation = _animations[animId];

            GetDisplay<AnimConfigDisplay>("AnimConfig").SetAnimation(_currentShownAnimation);
            GetDisplay<AnimPreviewDisplay>("AnimPreview").SetAnimation(_currentShownAnimation);
        }

        public void SetAnimationFrameRate(int frameRate)
        {
            GetDisplay<AnimConfigDisplay>("AnimConfig").SetAnimationFrameRate(frameRate, true);
        }

        public void SetFrameRateForSelectedFrames(int frameRate)
        {
            GetDisplay<AnimConfigDisplay>("AnimConfig").SetAnimationFrameRate(frameRate, false);
        }

        public void AddFrameContainer()
        {
            GetDisplay<AnimConfigDisplay>("AnimConfig").AddFrameContainer();
        }

        public void ClearSelectedFrameContainers()
        {
            GetDisplay<AnimConfigDisplay>("AnimConfig").ClearSelectedContainers();
        }

        public void RemoveSelectedFrameContainers()
        {
            GetDisplay<AnimConfigDisplay>("AnimConfig").RemoveSelectedContainers();
        }

        public bool TogglePlayAnimation()
        {
            return GetDisplay<AnimPreviewDisplay>("AnimPreview").TogglePlayAnimation();
        }

        public void ToggleGrid()
        {
            //MainDisplay mainDisplay = GetDisplay<MainDisplay>("MainDisplay");

            // mainDisplay.GridEnabled = !mainDisplay.GridEnabled;
        }

        public void ToggleOnionSkinning()
        {
            GetDisplay<AnimPreviewDisplay>("AnimPreview").ToggleOnionSkinning();
        }

        public void AnimationDecrementFrame()
        {
            GetDisplay<AnimPreviewDisplay>("AnimPreview").GoToLastFrame();
        }

        public void AnimationIncrementFrame()
        {
            GetDisplay<AnimPreviewDisplay>("AnimPreview").GoToNextFrame();
        }

        public void ResetAnimation()
        {
            GetDisplay<AnimPreviewDisplay>("AnimPreview").ResetAnimation();
        }

        public void SetAnimationLooped(bool looped)
        {
            if (_currentShownAnimation != null)
            {
                _currentShownAnimation.Loop = looped;
            }
        }

        public void SetAnimationPingPong(bool pingpong)
        {
            if (_currentShownAnimation != null)
            {
                _currentShownAnimation.PingPong = pingpong;
            }
        }

        public void SetFrameDuration(int frameNum, int frameTime)
        {
            if (_currentShownAnimation != null)
            {
                var duration = 1/frameTime;

                _currentShownAnimation.SetFrameDuration(frameNum, duration);
            }
        }

        public Rect ShrinkFrameSelect(Texture tex, Rect selectionRect, double alphaTolerance)
        {
            var x = selectionRect.Left - _currentShownSpriteSheet.BoundingRect.Left;
            var y = selectionRect.Top - _currentShownSpriteSheet.BoundingRect.Top;
            var translatedRect = Rect.FromBox(x, y, selectionRect.Width, selectionRect.Height);

            return SpriteSheetAndTextureFuncs.ShrinkFrameSelect(tex, translatedRect, alphaTolerance);
        }

        public void CutSpriteSheetByAlpha()
        {
            if (Guard.CheckNull(_currentShownSpriteSheet))
            {
                Messager.ShowMessage(Messager.Mode.Exception,
                                     "No spritesheet loaded! Please load a sprite sheet first by clicking the button 'Import Spritesheet' on topbar");
                return;
            }

            if (_currentShownSpriteSheet.Frames.Count > 0)
            {
                _currentShownSpriteSheet.Frames.Clear();
            }

            var frameRects = SpriteSheetAndTextureFuncs.CutByAlpha(_currentShownSpriteSheet.Texture);

            foreach (var frameRect in frameRects)
            {
                _currentShownSpriteSheet.AddFrame(frameRect);
            }
        }

        public void SetTextureFiltering(TextureFilter mode)
        {
            ResourcesManager.ChangeTextureFilterModeOfAllTextures(mode);
        }


        public void OnSpriteSheetFrameRectDeleted(List<int> removedIds)
        {
            var animations = GetAnimationsFromSpriteSheet(CurrentShownSpriteSheet);

            var animConfigDisplay = GetDisplay<AnimConfigDisplay>("AnimConfig");

            var framesToDelete = new List<Frame>();

            foreach (var removedId in removedIds)
            {
                foreach (var animation in animations)
                {
                    foreach (var frame in animation.Frames)
                    {
                        if (frame.SpriteFrame.SpriteFrameId == removedId)
                        {
                            framesToDelete.Add(frame);
                        }
                    }

                    foreach (var frame in framesToDelete)
                    {
                        animation.RemoveFrame(frame);

                        if (animConfigDisplay.CurrentAnimation.Id == animation.Id)
                        {
                            animConfigDisplay.SetAnimation(animation);
                        }
                    }

                    framesToDelete.Clear();
                }
            }
        }

        public void OnSpriteSheetFrameRectChanged(SpriteSheetFrame frameChanged)
        {
            var animations = GetAnimationsFromSpriteSheet(CurrentShownSpriteSheet);

            var animConfigDisplay = GetDisplay<AnimConfigDisplay>("AnimConfig");


            foreach (var animation in animations)
            {
                foreach (var frame in animation.Frames)
                {
                    if (frame.SpriteFrame.SpriteFrameId == frameChanged.Id)
                    {
                        frame.ResetSpriteFrame(new Sprite(CurrentShownSpriteSheet.Texture, frameChanged.BoundingRect),
                                               frameChanged.Id);
                        if (animConfigDisplay.CurrentAnimation.Id == animation.Id)
                        {
                            animConfigDisplay.SetAnimation(animation);
                        }
                    }

                    break;
                }
            }
        }

        private List<Animation> GetAnimationsFromSpriteSheet(SpriteSheet spritesheet)
        {
            List<Animation> animations = new List<Animation>();

            foreach (var animation in _animations)
            {
                if (animation.Value.SpriteSheetId.Equals(spritesheet.Id))
                {
                    animations.Add(animation.Value);
                }
            }

            if (animations.Count > 0)
            {
                return animations;
            }
            return null;
        }

        private void MakeSpriteSheet(string path, bool addAnim)
        {
            Bitmap imageBitmap = new Bitmap(path);


            var spriteSheetData = ResourcesManager.LoadSpriteSheetImage(path, imageBitmap);

            imageBitmap.Dispose();

            var spriteSheet = new SpriteSheet(spriteSheetData.Texture)
                                  {
                                      Name = spriteSheetData.Name,
                                      Id = spriteSheetData.Id,
                                      FullPath = spriteSheetData.FullPath
                                  };

            _spriteSheets.Add(spriteSheet.Id, spriteSheet);

            ShowSpriteSheet(spriteSheet.Id);

            if (CurrentShownAnimation != null)
            {
                UnloadCurrentShownAnimation();
            }


            var eventArgs = new AppEventArgs
                                {
                                    Type = EventType.AddedSpriteSheet,
                                    SpriteSheetModified = spriteSheet,
                                    AnimationModified = null
                                };


            MainWindow.UpdateBrowser(eventArgs);

            if (addAnim)
            {
                MakeAnimation();
            }


            FoldersHistoryManager.WriteFolderPath(path);
        }

        private void MakeSpriteSheetFromSeparateImages(Bitmap[] images, bool addAnim)
        {
            var spriteSheetData = ResourcesManager.LoadSpriteSheetFromSeparateImages(images);

            var spriteSheet = new SpriteSheet(spriteSheetData.Texture)
                                  {Name = spriteSheetData.Name, Id = spriteSheetData.Id};


            _spriteSheets.Add(spriteSheet.Id, spriteSheet);

            ShowSpriteSheet(spriteSheet.Id);

            if (CurrentShownAnimation != null)
            {
                UnloadCurrentShownAnimation();
            }


            var eventArgs = new AppEventArgs
                                {
                                    Type = EventType.AddedSpriteSheet,
                                    SpriteSheetModified = spriteSheet,
                                    AnimationModified = null
                                };

            MainWindow.UpdateBrowser(eventArgs);
            if (addAnim)
            {
                MakeAnimation();
            }
        }

        private string GetImagePath(string initialDirectory)
        {
            MainWindow.OpenFileDlg.Title = Resources.Import_SpriteSheet_Text;
            MainWindow.OpenFileDlg.RestoreDirectory = true;
            MainWindow.OpenFileDlg.InitialDirectory = initialDirectory.Length > 0
                                                          ? initialDirectory
                                                          : Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            MainWindow.OpenFileDlg.Multiselect = false;

            MainWindow.OpenFileDlg.Filter = "Image Files|";


            foreach (var ext in ImageHelper.AllowedImageExtensions)
            {
                MainWindow.OpenFileDlg.Filter += string.Format("*.{0};", ext);
            }


            if (MainWindow.OpenFileDlg.ShowDialog() == DialogResult.OK)
            {
                if (ImageHelper.CheckImage(MainWindow.OpenFileDlg.FileName))
                {
                    return MainWindow.OpenFileDlg.FileName;
                }
                Size maxTexturesize = RenderDisplayManager.MaxTextureSize;
                Messager.ShowMessage(Messager.Mode.Exception,
                                     string.Format(
                                         "Error loading image. Image extension must be PNG, JPG, GIF or BMP and size must be lower than {0}x{1}  pixels!",
                                         maxTexturesize.Width, maxTexturesize.Height));
            }
            return string.Empty;
        }

        private string[] GetImagePathList(string initialDirectory)
        {
            MainWindow.OpenFileDlg.Title = Resources.Import_Separate_Images_Text;
            MainWindow.OpenFileDlg.RestoreDirectory = true;
            MainWindow.OpenFileDlg.InitialDirectory = initialDirectory.Length > 0
                                                          ? initialDirectory
                                                          : Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            MainWindow.OpenFileDlg.Filter = @"Image Files (*.png)|*.png";

            MainWindow.OpenFileDlg.Multiselect = true;


            if (MainWindow.OpenFileDlg.ShowDialog() == DialogResult.OK)
            {
                return MainWindow.OpenFileDlg.FileNames;
            }
            return null;
        }

        private string ShowCompleteSaveToDialog(string description, string filter)
        {
            MainWindow.SaveFileDlg.Title = description;
            MainWindow.SaveFileDlg.Filter = filter;


            if (MainWindow.SaveFileDlg.ShowDialog() == DialogResult.OK)
            {
                return MainWindow.SaveFileDlg.FileName;
            }
            return null;
        }

        private string ShowSaveToFolderDialog(string description)
        {
            MainWindow.FolderBrowserDlg.Description = description;


            if (MainWindow.FolderBrowserDlg.ShowDialog() == DialogResult.OK)
            {
                return MainWindow.FolderBrowserDlg.SelectedPath;
            }
            return null;
        }

        private string ShowOpenFileDialog(string filter, string description)
        {
            MainWindow.OpenFileDlg.Filter = filter;
            MainWindow.OpenFileDlg.Title = description;


            if (MainWindow.OpenFileDlg.ShowDialog() == DialogResult.OK)
            {
                return MainWindow.OpenFileDlg.FileName;
            }
            return null;
        }


        public void ImportSpriteMapXML()
        {
            string path = ShowOpenFileDialog(@"XML File(.xml)|*.xml", "Import SpriteMap XML...");


            if (path.Length > 0)
            {
                string xmlDirectory = Path.GetDirectoryName(path);

                XmlDocument animDoc = new XmlDocument();

                Dictionary<int, SpriteSheetFrame> SheetFrames = new Dictionary<int, SpriteSheetFrame>();

                animDoc.Load(path);

                string textureRelativePath = animDoc.SelectSingleNode("//Texture/@Path").Value;

                // You have to decode the uri, because the Bitmap class can't understand 
                // the code "%20" as a space and don't recognize the path
                textureRelativePath = Uri.UnescapeDataString(textureRelativePath);

                string textureFullPath = Path.Combine(xmlDirectory, textureRelativePath);

                LoadSpriteSheetDirectly(textureFullPath, false);

                var colorsToClear = new List<ColorU>();

                var transparentColorsNodes = animDoc.SelectNodes("//TransparentColors/Color");

                foreach (XmlNode colorNode in transparentColorsNodes)
                {
                    string colorString = colorNode.Attributes["Value"].Value;

                    ColorU color = Parser.ParseColorU(colorString);

                    colorsToClear.Add(color);

                    CurrentShownSpriteSheet.TransparentColors.Add(color);
                }

                foreach (var color in colorsToClear)
                {
                    CurrentShownSpriteSheet.ClearColor(color);
                }

                colorsToClear.Clear();

                var spriteNodes = animDoc.SelectNodes("//Sprites/Sprite");

                foreach (XmlNode spriteNode in spriteNodes)
                {
                    var name = spriteNode.Attributes["Name"].Value;
                    var id = int.Parse(spriteNode.Attributes["Id"].Value);

                    var x = int.Parse(spriteNode.SelectSingleNode("Coordinates/X").InnerText);
                    var y = int.Parse(spriteNode.SelectSingleNode("Coordinates/Y").InnerText);
                    var w = int.Parse(spriteNode.SelectSingleNode("Coordinates/Width").InnerText);
                    var h = int.Parse(spriteNode.SelectSingleNode("Coordinates/Height").InnerText);


                    var frameAdded = CurrentShownSpriteSheet.AddFrame(name, id, Rect.FromBox(x, y, w, h));
                    SheetFrames.Add(id, frameAdded);
                }

                var animationNodes = animDoc.SelectNodes("//Animations/Animation");

                foreach (XmlNode animationNode in animationNodes)
                {
                    var name = animationNode.Attributes["Name"].Value;
                    var frameRate = float.Parse(animationNode.Attributes["FrameRate"].Value);
                    var loop = bool.Parse(animationNode.Attributes["Loop"].Value);
                    var pingPong = bool.Parse(animationNode.Attributes["PingPong"].Value);

                    MakeAnimation(name, frameRate, loop, pingPong);

                    var frames = new List<Frame>();


                    var frameNodes = animationNode.SelectNodes("Frames/Frame");

                    foreach (XmlNode frameNode in frameNodes)
                    {
                        var spriteId = int.Parse(frameNode.Attributes["SpriteId"].Value);
                        var offSetX = int.Parse(frameNode.Attributes["OffSetX"].Value);
                        var offSetY = int.Parse(frameNode.Attributes["OffSetY"].Value);

                        var duration = float.Parse(frameNode.Attributes["Duration"].Value);

                        var texture = CurrentShownSpriteSheet.Texture;
                        var rect = SheetFrames[spriteId].BoundingRect;
                        var frame = new Frame(new Sprite(texture, rect), spriteId);
                        frame.OffSetX = offSetX;
                        frame.OffSetY = offSetY;

                        frame.FrameDuration = duration;

                        frames.Add(frame);
                    }

                    AddFramesToAnimation(frames);
                }
            }
        }


        private static Application instance;

        private readonly Dictionary<string, SpriteSheet> _spriteSheets;
        private readonly Dictionary<string, Animation> _animations;
        private SpriteSheet _currentShownSpriteSheet;
        private Animation _currentShownAnimation;

        private Application()
        {
            _spriteSheets = new Dictionary<string, SpriteSheet>();
            _animations = new Dictionary<string, Animation>();

            _currentShownSpriteSheet = null;
            _currentShownAnimation = null;
        }
    }
}