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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using SpriteVortex.Properties;

namespace SpriteVortex
{
    public partial class MainWindow : KryptonForm
    {
        public static bool Resizing;

        public Application Application;

        public TreeView ObjectsBrowser
        {
            get { return Browser; }
        }

        public OpenFileDialog OpenFileDlg
        {
            get { return OpenFileDialog; }
        }

        public FolderBrowserDialog FolderBrowserDlg
        {
            get { return FolderBrowserDialog; }
        }

        public SaveFileDialog SaveFileDlg
        {
            get { return SaveFileDialog; }
        }

        public PropertyGrid PropertiesGrid
        {
            get { return PropertiesView; }
        }

        public Panel MainPanel
        {
            get { return MainDisplayPanel; }
        }

        public Panel ConfigPanel
        {
            get { return AnimConfigDisplayPanel; }
        }

        public Panel AnimPrevPanel
        {
            get { return AnimPreviewDisplayPanel; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }


        public void UpdateBrowser(AppEventArgs args)
        {
            switch (args.Type)
            {
                case EventType.AddedSpriteSheet:
                    {
                        TreeNode spNode = new TreeNode
                                              {
                                                  Name = args.SpriteSheetModified.Id,
                                                  Text = args.SpriteSheetModified.Name,
                                                  Tag = args.SpriteSheetModified.Id
                                              };

                        spNode.ImageIndex = 2;
                        spNode.SelectedImageIndex = 3;
                        Browser.Nodes.Add(spNode);
                        Browser.SelectedNode = spNode;
                    }
                    break;
                case EventType.RemovedSpriteSheet:
                    {
                        var nodeToBeRemoved = Browser.Nodes[args.SpriteSheetModified.Id];
                        var index = nodeToBeRemoved.Index;
                        if (index == Browser.Nodes.Count - 1)
                        {
                            index -= 1;
                        }
                        Browser.Nodes.Remove(nodeToBeRemoved);
                        Browser.SelectedNode = index >= 0 ? Browser.Nodes[index] : null;
                    }
                    break;
                case EventType.AddedAnimation:
                    {
                        TreeNode animNode = new TreeNode
                                                {
                                                    Name = args.AnimationModified.Id,
                                                    Text = args.AnimationModified.Name,
                                                    Tag = args.AnimationModified.Id
                                                };


                        animNode.ImageIndex = 0;
                        animNode.SelectedImageIndex = 1;
                        Browser.Nodes[args.SpriteSheetModified.Id].Nodes.Add(animNode);
                        Browser.SelectedNode = animNode;
                    }
                    break;
                case EventType.RemovedAnimation:
                    {
                        var parentNode = Browser.Nodes[args.SpriteSheetModified.Id];
                        var nodeToBeRemoved = parentNode.Nodes[args.AnimationModified.Id];
                        var index = nodeToBeRemoved.Index;
                        if (index == parentNode.Nodes.Count - 1)
                        {
                            index -= 1;
                        }
                        parentNode.Nodes.Remove(nodeToBeRemoved);
                        Browser.SelectedNode = index >= 0 ? parentNode.Nodes[index] : parentNode;
                    }
                    break;
                case EventType.RenamedAnimation:
                    {
                        Browser.Nodes[args.AnimationModified.Id].Text = args.AnimationModified.Name;
                        Browser.Nodes[args.AnimationModified.Id].Name = args.AnimationModified.Name;
                    }
                    break;
            }

            Browser.ExpandAll();
        }

        public void DeleteBrowserNode()
        {
            if (Browser.Nodes.Count == 0)
            {
                return;
            }

            if (Browser.SelectedNode.Level == 0)
            {
                var spNode = Browser.SelectedNode;

                var spriteSheet = Application.GetSpriteSheet((string) spNode.Tag);

                var eventArgs = new AppEventArgs
                                    {
                                        Type = EventType.RemovedSpriteSheet,
                                        SpriteSheetModified = spriteSheet,
                                        AnimationModified = null
                                    };


                UpdateBrowser(eventArgs);


                Application.RemoveSpriteSheet((string) spNode.Tag);

                foreach (TreeNode animNode in spNode.Nodes)
                {
                    Application.RemoveAnimation((string) animNode.Tag);
                }
            }
            else
            {
                var animNode = Browser.SelectedNode;
                var animation = Application.GetAnimation((string) animNode.Tag);
                var spriteSheet = Application.GetSpriteSheet((string) animNode.Parent.Tag);


                var eventArgs = new AppEventArgs
                                    {
                                        Type = EventType.RemovedAnimation,
                                        SpriteSheetModified = spriteSheet,
                                        AnimationModified = animation
                                    };
                UpdateBrowser(eventArgs);

                Application.RemoveAnimation((string) animNode.Tag);

                if (Application.Animations.Count == 0)
                {
                    Application.MakeAnimation();
                }
            }
        }

        private void MainWindowLoad(object sender, EventArgs e)
        {
            Application = Application.Instance;

            Messager.Initialize(messagePanel, lblMessageText, MessageTimer);

            Application.Initialize(this);


            UpdateFoldersHistoryDropDown();

            AppTimer.Enabled = true;

            Disposed += MainWindowDisposed;
        }

        private void MainWindowDisposed(object sender, EventArgs eventArgs)
        {
            Application.Terminate();
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void AppTimerTick(object sender, EventArgs e)
        {
            if (AppTimer.Enabled)
            {
                Application.Update(AppTimer.Interval);
            }
        }

        private void MainDisplayPanelResize(object sender, EventArgs e)
        {
            if (Application != null && Application.RenderDisplayManager != null)
            {
                Application.RenderDisplayManager.ResizeTarget("MainDisplay", MainDisplayPanel.ClientSize);
                Application.GetDisplay<MainDisplay>("MainDisplay").RefreshGui();
            }
        }

        private void AnimPreviewDisplayPanelResize(object sender, EventArgs e)
        {
            if (Application != null && Application.RenderDisplayManager != null)
            {
                Application.RenderDisplayManager.ResizeTarget("AnimPreview", AnimPreviewDisplayPanel.ClientSize);
            }
        }

        private void AnimConfigDisplayPanelResize(object sender, EventArgs e)
        {
            if (Application != null && Application.RenderDisplayManager != null)
            {
                Application.RenderDisplayManager.ResizeTarget("AnimConfig", AnimConfigDisplayPanel.ClientSize);
                Application.GetDisplay<AnimConfigDisplay>("AnimConfig").Refresh();
            }
        }

        private void MainWindow_ResizeBegin(object sender, EventArgs e)
        {
            Resizing = true;
        }

        private void MainWindow_ResizeEnd(object sender, EventArgs e)
        {
            Resizing = false;
        }

        private void BottomSpliteContainer_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            Resizing = true;
        }

        private void BottomSpliteContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Resizing = false;
        }

        private void TopSplitContainer_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            Resizing = true;
        }

        private void TopSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Resizing = false;
        }

        private void MainPanelSplitter_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            Resizing = true;
        }

        private void MainPanelSplitter_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Resizing = false;
        }


        private void MainDisplayPanelMouseEnter(object sender, EventArgs e)
        {
            //Cursor.Hide();
            MainDisplayPanel.Focus();
        }

        private void MainDisplayPanelMouseLeave(object sender, EventArgs e)
        {
            // Cursor.Show();
        }

        private void AnimPreviewDisplayPanelMouseEnter(object sender, EventArgs e)
        {
            //Cursor.Hide();
            AnimPreviewDisplayPanel.Focus();
        }

        private void AnimPreviewDisplayPanelMouseLeave(object sender, EventArgs e)
        {
            //Cursor.Show();
        }

        private void AnimConfigDisplayPanelMouseEnter(object sender, EventArgs e)
        {
            //Cursor.Hide();
            AnimConfigDisplayPanel.Focus();
        }

        private void AnimConfigDisplayPanelMouseLeave(object sender, EventArgs e)
        {
            //Cursor.Show();
        }


        private static void ShowConfigurationWindow()
        {
            var configDialog = new ConfigurationWindow();
            configDialog.ShowDialog();
        }


        private void BtnConfigureClick(object sender, EventArgs e)
        {
            ShowConfigurationWindow();
        }


        public void SetTogglePlayButtonState(bool playing)
        {
            BtnTogglePlayAnimation.Values.Image = playing ? Resources.Pause : Resources.Play;
        }

        private void BrowserAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level.Equals(0))
            {
                Application.ShowSpriteSheet((string) e.Node.Tag);
            }
            else
            {
                Application.ShowAnimation((string) e.Node.Tag);
                sldFrameTime.Value = (int) Application.CurrentShownAnimation.FrameRate;
                ChkAnimLooped.Checked = Application.CurrentShownAnimation.Loop;
                ChkAnimPingPong.Checked = Application.CurrentShownAnimation.PingPong;
                SetTogglePlayButtonState(!Application.CurrentShownAnimation.Paused);
                BtnToggleOnionSkinning.Checked = Application.CurrentShownAnimation.EnableOnionSkin;
            }
        }

        private void UpdateFoldersHistoryDropDown()
        {
            BtnImportSpriteSheet.DropDownItems.Clear();
            BtnImportSpriteSheetFromSeparateImages.DropDownItems.Clear();

            var historyDirectories = FoldersHistoryManager.GetHistoryFolders();

            if (historyDirectories != null)
            {
                foreach (var historyDirectory in historyDirectories)
                {
                    BtnImportSpriteSheet.DropDownItems.Add(historyDirectory);
                    BtnImportSpriteSheetFromSeparateImages.DropDownItems.Add(historyDirectory);
                }
            }
        }

        private void RemoveFoldersHistory()
        {
            BtnImportSpriteSheet.DropDownItems.Clear();
            BtnImportSpriteSheetFromSeparateImages.DropDownItems.Clear();
            FoldersHistoryManager.EraseHistory();
        }

        private void BrowserKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Delete))
            {
                DeleteBrowserNode();
            }
            else if (e.KeyCode.Equals(Keys.R))
            {
                if (Browser.Nodes.Count == 0)
                {
                    return;
                }

                if (Browser.SelectedNode.Level == 1)
                {
                    Browser.SelectedNode.BeginEdit();
                }
            }
        }


        private void BtnRemoveNodeClick(object sender, EventArgs e)
        {
            DeleteBrowserNode();
        }

        private void BrowserAfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    var validator = new Regex("^[a-zA-Z0-9]+$");

                    if (validator.IsMatch(e.Label))
                    {
                        e.Node.EndEdit(false);

                        Application.RenameAnimation((string) e.Node.Tag, e.Label);
                    }
                    else
                    {
                        e.CancelEdit = true;

                        KryptonMessageBox.Show("Invalid name!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        e.Node.BeginEdit();
                    }
                }
            }
            else
            {
                e.CancelEdit = true;
            }
        }


        private void BtnRenameNodeClick(object sender, EventArgs e)
        {
            if (Browser.Nodes.Count == 0)
            {
                return;
            }

            if (Browser.SelectedNode.Level == 1)
            {
                Browser.SelectedNode.BeginEdit();
            }
        }

        private void SldFrameTimeValueChanged(object sender, EventArgs e)
        {
            if (ChkSetFrameRateToAllFrames.Checked)
            {
                Application.SetAnimationFrameRate(sldFrameTime.Value);
            }
            else
            {
                Application.SetFrameRateForSelectedFrames(sldFrameTime.Value);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutDlg = new AboutSplash();
            aboutDlg.ShowDialog();
        }

        private void ChkAnimLoopedCheckedChanged(object sender, EventArgs e)
        {
            Application.SetAnimationLooped(ChkAnimLooped.Checked);
        }

        private void ChkAnimPingPongCheckedChanged(object sender, EventArgs e)
        {
            Application.SetAnimationPingPong(ChkAnimPingPong.Checked);
        }


        private void BtnResetAnimationClick(object sender, EventArgs e)
        {
            Application.ResetAnimation();
        }

        private void BtnGoLastFrameClick(object sender, EventArgs e)
        {
            Application.AnimationDecrementFrame();
        }

        private void BtnGoNextFrameClick(object sender, EventArgs e)
        {
            Application.AnimationIncrementFrame();
        }

        private void BtnAnimPreviewFitViewToContentClick(object sender, EventArgs e)
        {
            Application.GetDisplay<AnimPreviewDisplay>("AnimPreview").FitViewToContent();
        }

        private void BtnAddAnimationClick(object sender, EventArgs e)
        {
            Application.MakeAnimation();
        }

        private void BtnExportSpriteSheetImageClick(object sender, EventArgs e)
        {
            Application.ExportSpriteSheetImage();
        }


        private void BtnExportFramesImagesClick(object sender, EventArgs e)
        {
            Application.ExportFramesImages();
        }


        private void BtnExportGifFromAnimationClick(object sender, EventArgs e)
        {
            Application.ExportAnimationAsGif();
        }

        private void BtnExportSpriteMapXmlClick(object sender, EventArgs e)
        {
            Application.ExportSpriteMapXML();
        }

        private void MainDisplayPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void MainDisplayPanelDragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                Application.LoadSpriteSheetDirectly(file, true);
            }
        }

        private void BtnTogglePlayAnimationClick(object sender, EventArgs e)
        {
            Application.TogglePlayAnimation();
        }

        private void BtnImportSpriteSheetDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            BtnImportSpriteSheet.DropDown.Close();
            if (Directory.Exists(e.ClickedItem.Text))
            {
                Application.LoadSpriteSheet(e.ClickedItem.Text);
            }
        }


        private void BtnImportSpriteSheetDropDownOpening(object sender, EventArgs e)
        {
            UpdateFoldersHistoryDropDown();
        }

        private void BtnImportSpriteSheetButtonClick(object sender, EventArgs e)
        {
            Application.LoadSpriteSheet();
        }

        private void BtnImportSpriteSheetFromSeparateImagesButtonClick(object sender, EventArgs e)
        {
            Application.LoadSpriteSheetFromSeparateImages();
        }

        private void BtnImportSpriteSheetFromSeparateImagesDropDownItemClicked(object sender,
                                                                               ToolStripItemClickedEventArgs e)
        {
            BtnImportSpriteSheetFromSeparateImages.DropDown.Close();
            if (Directory.Exists(e.ClickedItem.Text))
            {
                Application.LoadSpriteSheetFromSeparateImages(e.ClickedItem.Text);
            }
        }

        private void BtnImportSpriteSheetFromSeparateImagesDropDownOpening(object sender, EventArgs e)
        {
            UpdateFoldersHistoryDropDown();
        }

        private void eraseFoldersHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveFoldersHistory();
        }

        private void HelpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://spritevortex.codeplex.com/documentation");
        }

        private void BtnClearSelectedContainersClick(object sender, EventArgs e)
        {
            Application.ClearSelectedFrameContainers();
        }

        private void BtnRemoveSelectedContainersClick(object sender, EventArgs e)
        {
            Application.RemoveSelectedFrameContainers();
        }

        private void BtnToggleOnionSkinningCheckedChanged(object sender, EventArgs e)
        {
            if (BtnToggleOnionSkinning.Checked != Application.CurrentShownAnimation.EnableOnionSkin)
            {
                Application.ToggleOnionSkinning();
            }
        }

        private void BtnAddContainerClick(object sender, EventArgs e)
        {
            Application.AddFrameContainer();
        }

        private void BtnImportSpriteMapXmlClick(object sender, EventArgs e)
        {
            Application.ImportSpriteMapXML();
        }

        private void sldFrameTime_MouseDown(object sender, MouseEventArgs e)
        {
            var previewDisplay = Application.GetDisplay<AnimPreviewDisplay>("AnimPreview");

            if (previewDisplay.IsPlaying)
            {
                previewDisplay.PlayAnimation(false);
            }
        }

        private void sldFrameTime_MouseUp(object sender, MouseEventArgs e)
        {
            var previewDisplay = Application.GetDisplay<AnimPreviewDisplay>("AnimPreview");

            previewDisplay.PlayAnimation(true);
        }

        private void BtnToggleGrid_CheckedChanged(object sender, EventArgs e)
        {
            Application.ToggleGrid();
        }
    }
}