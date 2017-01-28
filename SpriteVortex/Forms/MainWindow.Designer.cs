using System;

namespace SpriteVortex
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.AppThemeDefinition = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.AppToolStrip = new System.Windows.Forms.ToolStrip();
            this.BtnImportSpriteSheet = new System.Windows.Forms.ToolStripSplitButton();
            this.BtnImportSpriteSheetFromSeparateImages = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnExportSpriteSheetImage = new System.Windows.Forms.ToolStripButton();
            this.BtnExportSpriteSheetFramesImages = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnAddAnimation = new System.Windows.Forms.ToolStripButton();
            this.BtnExportGifFromAnimation = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnImportSpriteMap = new System.Windows.Forms.ToolStripButton();
            this.BtnExportSpriteMapXml = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnConfigure = new System.Windows.Forms.ToolStripButton();
            this.BottomSplitContainer = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.BtnRenameNode = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.BtnRemoveNode = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.Browser = new System.Windows.Forms.TreeView();
            this.BrowserImageList = new System.Windows.Forms.ImageList(this.components);
            this.BrowserHeader = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.TopSplitContainer = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.MainPanelSplitter = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.MainDisplayPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.PanFrameTime = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.LblFrameRate = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.ChkSetFrameRateToAllFrames = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.sldFrameTime = new ComponentFactory.Krypton.Toolkit.KryptonTrackBar();
            this.AnimConfigDisplayPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.AnimConfigDisplayButtonsPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.BtnAddContainer = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.BtnRemoveSelectedContainers = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.BtnClearSelectedContainers = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.AnimConfigChkBoxHostPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.ChkAnimLooped = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ChkAnimPingPong = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.AnimBuilderHeader = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.PropertiesView = new System.Windows.Forms.PropertyGrid();
            this.AnimPreviewDisplayHostPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.AnimPreviewDisplayControlButtonsTable = new System.Windows.Forms.TableLayoutPanel();
            this.BtnTogglePlayAnimation = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.BtnGoLastFrame = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.BtnGoNextFrame = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.BtnResetAnimation = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.BtnToggleOnionSkinning = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.AnimPreviewDisplayPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.AnimPreviewDisplayHeader = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.AnimPreviewDisplayButtonsPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.BtnAnimPreviewFitViewToContent = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importSpriteSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importSpriteSheetToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.importSpriteMapXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSpriteMapXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eraseFoldersHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spriteSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportImageFromCurrentSpriteSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSpriteImagesFromCurrentSpriteSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsAnimatedGIFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AppTimer = new System.Windows.Forms.Timer(this.components);
            this.messagePanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.lblMessageText = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.MessageTimer = new System.Windows.Forms.Timer(this.components);
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.AppToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BottomSplitContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomSplitContainer.Panel1)).BeginInit();
            this.BottomSplitContainer.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BottomSplitContainer.Panel2)).BeginInit();
            this.BottomSplitContainer.Panel2.SuspendLayout();
            this.BottomSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TopSplitContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopSplitContainer.Panel1)).BeginInit();
            this.TopSplitContainer.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TopSplitContainer.Panel2)).BeginInit();
            this.TopSplitContainer.Panel2.SuspendLayout();
            this.TopSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainPanelSplitter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainPanelSplitter.Panel1)).BeginInit();
            this.MainPanelSplitter.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainPanelSplitter.Panel2)).BeginInit();
            this.MainPanelSplitter.Panel2.SuspendLayout();
            this.MainPanelSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainDisplayPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanFrameTime)).BeginInit();
            this.PanFrameTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnimConfigDisplayPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnimConfigDisplayButtonsPanel)).BeginInit();
            this.AnimConfigDisplayButtonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnimConfigChkBoxHostPanel)).BeginInit();
            this.AnimConfigChkBoxHostPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnimPreviewDisplayHostPanel)).BeginInit();
            this.AnimPreviewDisplayHostPanel.SuspendLayout();
            this.AnimPreviewDisplayControlButtonsTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnimPreviewDisplayPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnimPreviewDisplayButtonsPanel)).BeginInit();
            this.AnimPreviewDisplayButtonsPanel.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messagePanel)).BeginInit();
            this.messagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // AppThemeDefinition
            // 
            this.AppThemeDefinition.BasePaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedNormal.Back.Color1 = System.Drawing.Color.White;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(226)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedNormal.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Linear;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedNormal.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(183)))), ((int)(((byte)(183)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedNormal.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(183)))), ((int)(((byte)(183)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedNormal.Border.Rounding = 14;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedNormal.Border.Width = 1;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedNormal.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedNormal.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedNormal.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedPressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(225)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedPressed.Back.Color2 = System.Drawing.Color.White;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedPressed.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Linear;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedPressed.Border.Color1 = System.Drawing.Color.SteelBlue;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedPressed.Border.Color2 = System.Drawing.Color.SteelBlue;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedPressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedPressed.Border.Rounding = 14;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedPressed.Border.Width = 1;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedPressed.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedPressed.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedPressed.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedTracking.Back.Color1 = System.Drawing.Color.White;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(226)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedTracking.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Linear;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedTracking.Border.Color1 = System.Drawing.Color.LightSkyBlue;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedTracking.Border.Color2 = System.Drawing.Color.LightSkyBlue;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedTracking.Border.Rounding = 14;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedTracking.Border.Width = 1;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedTracking.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedTracking.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateCheckedTracking.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateDisabled.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Linear;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateDisabled.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateDisabled.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateDisabled.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateDisabled.Border.Rounding = 14;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateDisabled.Border.Width = 1;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateNormal.Back.Color1 = System.Drawing.Color.White;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(226)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateNormal.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(183)))), ((int)(((byte)(183)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateNormal.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(183)))), ((int)(((byte)(183)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateNormal.Border.Rounding = 14;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateNormal.Border.Width = 1;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateNormal.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateNormal.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(226)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StatePressed.Back.Color2 = System.Drawing.Color.White;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StatePressed.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Linear;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StatePressed.Border.Color1 = System.Drawing.Color.SteelBlue;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StatePressed.Border.Color2 = System.Drawing.Color.SteelBlue;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StatePressed.Border.Rounding = 14;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StatePressed.Border.Width = 1;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StatePressed.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateTracking.Back.Color1 = System.Drawing.Color.White;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(226)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateTracking.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Linear;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateTracking.Border.Color1 = System.Drawing.Color.LightSkyBlue;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateTracking.Border.Color2 = System.Drawing.Color.LightSkyBlue;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateTracking.Border.Rounding = 14;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateTracking.Border.Width = 1;
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateTracking.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.ButtonStyles.ButtonStandalone.StateTracking.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.AppThemeDefinition.Images.CheckBox.CheckedDisabled = ((System.Drawing.Image)(resources.GetObject("AppThemeDefinition.Images.CheckBox.CheckedDisabled")));
            this.AppThemeDefinition.Images.CheckBox.CheckedNormal = ((System.Drawing.Image)(resources.GetObject("AppThemeDefinition.Images.CheckBox.CheckedNormal")));
            this.AppThemeDefinition.Images.CheckBox.CheckedPressed = ((System.Drawing.Image)(resources.GetObject("AppThemeDefinition.Images.CheckBox.CheckedPressed")));
            this.AppThemeDefinition.Images.CheckBox.CheckedTracking = ((System.Drawing.Image)(resources.GetObject("AppThemeDefinition.Images.CheckBox.CheckedTracking")));
            this.AppThemeDefinition.Images.CheckBox.UncheckedDisabled = ((System.Drawing.Image)(resources.GetObject("AppThemeDefinition.Images.CheckBox.UncheckedDisabled")));
            this.AppThemeDefinition.Images.CheckBox.UncheckedNormal = ((System.Drawing.Image)(resources.GetObject("AppThemeDefinition.Images.CheckBox.UncheckedNormal")));
            this.AppThemeDefinition.Images.CheckBox.UncheckedPressed = ((System.Drawing.Image)(resources.GetObject("AppThemeDefinition.Images.CheckBox.UncheckedPressed")));
            this.AppThemeDefinition.Images.CheckBox.UncheckedTracking = ((System.Drawing.Image)(resources.GetObject("AppThemeDefinition.Images.CheckBox.UncheckedTracking")));
            this.AppThemeDefinition.Images.RadioButton.CheckedDisabled = ((System.Drawing.Image)(resources.GetObject("AppThemeDefinition.Images.RadioButton.CheckedDisabled")));
            this.AppThemeDefinition.Images.RadioButton.CheckedNormal = ((System.Drawing.Image)(resources.GetObject("AppThemeDefinition.Images.RadioButton.CheckedNormal")));
            this.AppThemeDefinition.Images.RadioButton.CheckedPressed = ((System.Drawing.Image)(resources.GetObject("AppThemeDefinition.Images.RadioButton.CheckedPressed")));
            this.AppThemeDefinition.Images.RadioButton.CheckedTracking = ((System.Drawing.Image)(resources.GetObject("AppThemeDefinition.Images.RadioButton.CheckedTracking")));
            this.AppThemeDefinition.Images.RadioButton.UncheckedDisabled = ((System.Drawing.Image)(resources.GetObject("AppThemeDefinition.Images.RadioButton.UncheckedDisabled")));
            this.AppThemeDefinition.Images.RadioButton.UncheckedNormal = ((System.Drawing.Image)(resources.GetObject("AppThemeDefinition.Images.RadioButton.UncheckedNormal")));
            this.AppThemeDefinition.Images.RadioButton.UncheckedPressed = ((System.Drawing.Image)(resources.GetObject("AppThemeDefinition.Images.RadioButton.UncheckedPressed")));
            this.AppThemeDefinition.Images.RadioButton.UncheckedTracking = ((System.Drawing.Image)(resources.GetObject("AppThemeDefinition.Images.RadioButton.UncheckedTracking")));
            this.AppThemeDefinition.InputControlStyles.InputControlStandalone.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.AppThemeDefinition.InputControlStyles.InputControlStandalone.StateActive.Border.Rounding = 14;
            this.AppThemeDefinition.InputControlStyles.InputControlStandalone.StateActive.Border.Width = 2;
            this.AppThemeDefinition.InputControlStyles.InputControlStandalone.StateActive.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.InputControlStyles.InputControlStandalone.StateActive.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.InputControlStyles.InputControlStandalone.StateActive.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.AppThemeDefinition.InputControlStyles.InputControlStandalone.StateDisabled.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.AppThemeDefinition.InputControlStyles.InputControlStandalone.StateDisabled.Border.Rounding = 14;
            this.AppThemeDefinition.InputControlStyles.InputControlStandalone.StateDisabled.Border.Width = 1;
            this.AppThemeDefinition.InputControlStyles.InputControlStandalone.StateDisabled.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.AppThemeDefinition.InputControlStyles.InputControlStandalone.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.AppThemeDefinition.InputControlStyles.InputControlStandalone.StateNormal.Border.Rounding = 14;
            this.AppThemeDefinition.InputControlStyles.InputControlStandalone.StateNormal.Border.Width = 1;
            this.AppThemeDefinition.InputControlStyles.InputControlStandalone.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.InputControlStyles.InputControlStandalone.StateNormal.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.InputControlStyles.InputControlStandalone.StateNormal.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.AppThemeDefinition.LabelStyles.LabelNormalControl.StateDisabled.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.AppThemeDefinition.LabelStyles.LabelNormalControl.StateNormal.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.LabelStyles.LabelNormalControl.StateNormal.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.LabelStyles.LabelNormalControl.StateNormal.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.AppThemeDefinition.LabelStyles.LabelNormalPanel.StateDisabled.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.AppThemeDefinition.LabelStyles.LabelNormalPanel.StateNormal.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.LabelStyles.LabelNormalPanel.StateNormal.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.LabelStyles.LabelNormalPanel.StateNormal.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.AppThemeDefinition.LabelStyles.LabelTitlePanel.StateDisabled.ShortText.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.AppThemeDefinition.LabelStyles.LabelTitlePanel.StateNormal.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.LabelStyles.LabelTitlePanel.StateNormal.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.AppThemeDefinition.LabelStyles.LabelTitlePanel.StateNormal.ShortText.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.AppThemeDefinition.LabelStyles.LabelToolTip.StateDisabled.ShortText.Color1 = System.Drawing.Color.SlateGray;
            this.AppThemeDefinition.LabelStyles.LabelToolTip.StateDisabled.ShortText.Color2 = System.Drawing.Color.SteelBlue;
            this.AppThemeDefinition.LabelStyles.LabelToolTip.StateDisabled.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.AppThemeDefinition.LabelStyles.LabelToolTip.StateNormal.ShortText.Color1 = System.Drawing.Color.DodgerBlue;
            this.AppThemeDefinition.LabelStyles.LabelToolTip.StateNormal.ShortText.Color2 = System.Drawing.Color.DodgerBlue;
            this.AppThemeDefinition.LabelStyles.LabelToolTip.StateNormal.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // AppToolStrip
            // 
            this.AppToolStrip.AutoSize = false;
            this.AppToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(230)))), ((int)(((byte)(232)))));
            this.AppToolStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.AppToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.AppToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnImportSpriteSheet,
            this.BtnImportSpriteSheetFromSeparateImages,
            this.toolStripSeparator3,
            this.BtnExportSpriteSheetImage,
            this.BtnExportSpriteSheetFramesImages,
            this.toolStripSeparator1,
            this.BtnAddAnimation,
            this.BtnExportGifFromAnimation,
            this.toolStripSeparator2,
            this.BtnImportSpriteMap,
            this.BtnExportSpriteMapXml,
            this.toolStripSeparator4,
            this.BtnConfigure});
            this.AppToolStrip.Location = new System.Drawing.Point(0, 24);
            this.AppToolStrip.Name = "AppToolStrip";
            this.AppToolStrip.Size = new System.Drawing.Size(1126, 46);
            this.AppToolStrip.TabIndex = 0;
            this.AppToolStrip.Text = "toolStrip1";
            // 
            // BtnImportSpriteSheet
            // 
            this.BtnImportSpriteSheet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnImportSpriteSheet.Image = global::SpriteVortex.Properties.Resources.icon_import_spsheet;
            this.BtnImportSpriteSheet.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnImportSpriteSheet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnImportSpriteSheet.Name = "BtnImportSpriteSheet";
            this.BtnImportSpriteSheet.Size = new System.Drawing.Size(48, 43);
            this.BtnImportSpriteSheet.Text = "Import Spritesheet Image";
            this.BtnImportSpriteSheet.ToolTipText = "Import Spritesheet Image";
            this.BtnImportSpriteSheet.ButtonClick += new System.EventHandler(this.BtnImportSpriteSheetButtonClick);
            this.BtnImportSpriteSheet.DropDownOpening += new System.EventHandler(this.BtnImportSpriteSheetDropDownOpening);
            this.BtnImportSpriteSheet.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.BtnImportSpriteSheetDropDownItemClicked);
            // 
            // BtnImportSpriteSheetFromSeparateImages
            // 
            this.BtnImportSpriteSheetFromSeparateImages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnImportSpriteSheetFromSeparateImages.Image = global::SpriteVortex.Properties.Resources.icon_import_spsheet_separate;
            this.BtnImportSpriteSheetFromSeparateImages.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnImportSpriteSheetFromSeparateImages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnImportSpriteSheetFromSeparateImages.Name = "BtnImportSpriteSheetFromSeparateImages";
            this.BtnImportSpriteSheetFromSeparateImages.Size = new System.Drawing.Size(48, 43);
            this.BtnImportSpriteSheetFromSeparateImages.Text = "Import Spritesheet from separate images";
            this.BtnImportSpriteSheetFromSeparateImages.ButtonClick += new System.EventHandler(this.BtnImportSpriteSheetFromSeparateImagesButtonClick);
            this.BtnImportSpriteSheetFromSeparateImages.DropDownOpening += new System.EventHandler(this.BtnImportSpriteSheetFromSeparateImagesDropDownOpening);
            this.BtnImportSpriteSheetFromSeparateImages.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.BtnImportSpriteSheetFromSeparateImagesDropDownItemClicked);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 46);
            // 
            // BtnExportSpriteSheetImage
            // 
            this.BtnExportSpriteSheetImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnExportSpriteSheetImage.Image = global::SpriteVortex.Properties.Resources.icon_export_spsheet;
            this.BtnExportSpriteSheetImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnExportSpriteSheetImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnExportSpriteSheetImage.Name = "BtnExportSpriteSheetImage";
            this.BtnExportSpriteSheetImage.Size = new System.Drawing.Size(36, 43);
            this.BtnExportSpriteSheetImage.Text = "Export Spritesheet Image";
            this.BtnExportSpriteSheetImage.ToolTipText = "Export spritesheet Image";
            this.BtnExportSpriteSheetImage.Click += new System.EventHandler(this.BtnExportSpriteSheetImageClick);
            // 
            // BtnExportSpriteSheetFramesImages
            // 
            this.BtnExportSpriteSheetFramesImages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnExportSpriteSheetFramesImages.Image = global::SpriteVortex.Properties.Resources.icon_export_spsheet_separate;
            this.BtnExportSpriteSheetFramesImages.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnExportSpriteSheetFramesImages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnExportSpriteSheetFramesImages.Name = "BtnExportSpriteSheetFramesImages";
            this.BtnExportSpriteSheetFramesImages.Size = new System.Drawing.Size(36, 43);
            this.BtnExportSpriteSheetFramesImages.Text = "Export Spritesheet frames as separate imges";
            this.BtnExportSpriteSheetFramesImages.ToolTipText = "Export frames images";
            this.BtnExportSpriteSheetFramesImages.Click += new System.EventHandler(this.BtnExportFramesImagesClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 46);
            // 
            // BtnAddAnimation
            // 
            this.BtnAddAnimation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnAddAnimation.Image = global::SpriteVortex.Properties.Resources.icon_add_animation;
            this.BtnAddAnimation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnAddAnimation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnAddAnimation.Name = "BtnAddAnimation";
            this.BtnAddAnimation.Size = new System.Drawing.Size(36, 43);
            this.BtnAddAnimation.Text = "Add New Animation";
            this.BtnAddAnimation.ToolTipText = "Add new animation";
            this.BtnAddAnimation.Click += new System.EventHandler(this.BtnAddAnimationClick);
            // 
            // BtnExportGifFromAnimation
            // 
            this.BtnExportGifFromAnimation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnExportGifFromAnimation.Image = global::SpriteVortex.Properties.Resources.icon_export_animation_gif;
            this.BtnExportGifFromAnimation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnExportGifFromAnimation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnExportGifFromAnimation.Name = "BtnExportGifFromAnimation";
            this.BtnExportGifFromAnimation.Size = new System.Drawing.Size(36, 43);
            this.BtnExportGifFromAnimation.Text = "Create animated GIF from animation";
            this.BtnExportGifFromAnimation.Click += new System.EventHandler(this.BtnExportGifFromAnimationClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 46);
            // 
            // BtnImportSpriteMap
            // 
            this.BtnImportSpriteMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnImportSpriteMap.Image = global::SpriteVortex.Properties.Resources.importXML;
            this.BtnImportSpriteMap.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnImportSpriteMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnImportSpriteMap.Name = "BtnImportSpriteMap";
            this.BtnImportSpriteMap.Size = new System.Drawing.Size(34, 43);
            this.BtnImportSpriteMap.Text = "Import SpriteMap XML";
            this.BtnImportSpriteMap.Click += new System.EventHandler(this.BtnImportSpriteMapXmlClick);
            // 
            // BtnExportSpriteMapXml
            // 
            this.BtnExportSpriteMapXml.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnExportSpriteMapXml.Image = ((System.Drawing.Image)(resources.GetObject("BtnExportSpriteMapXml.Image")));
            this.BtnExportSpriteMapXml.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnExportSpriteMapXml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnExportSpriteMapXml.Name = "BtnExportSpriteMapXml";
            this.BtnExportSpriteMapXml.Size = new System.Drawing.Size(34, 43);
            this.BtnExportSpriteMapXml.Text = "Export SpriteMap as XML";
            this.BtnExportSpriteMapXml.Click += new System.EventHandler(this.BtnExportSpriteMapXmlClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 46);
            // 
            // BtnConfigure
            // 
            this.BtnConfigure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnConfigure.Image = global::SpriteVortex.Properties.Resources.Gear;
            this.BtnConfigure.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnConfigure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnConfigure.Name = "BtnConfigure";
            this.BtnConfigure.Size = new System.Drawing.Size(36, 43);
            this.BtnConfigure.Text = "Options";
            this.BtnConfigure.Click += new System.EventHandler(this.BtnConfigureClick);
            // 
            // BottomSplitContainer
            // 
            this.BottomSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BottomSplitContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this.BottomSplitContainer.Location = new System.Drawing.Point(0, 98);
            this.BottomSplitContainer.Name = "BottomSplitContainer";
            this.BottomSplitContainer.Palette = this.AppThemeDefinition;
            this.BottomSplitContainer.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            // 
            // BottomSplitContainer.Panel1
            // 
            this.BottomSplitContainer.Panel1.Controls.Add(this.BtnRenameNode);
            this.BottomSplitContainer.Panel1.Controls.Add(this.BtnRemoveNode);
            this.BottomSplitContainer.Panel1.Controls.Add(this.Browser);
            this.BottomSplitContainer.Panel1.Controls.Add(this.BrowserHeader);
            this.BottomSplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(2);
            this.BottomSplitContainer.Panel1.Palette = this.AppThemeDefinition;
            this.BottomSplitContainer.Panel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.BottomSplitContainer.Panel1MinSize = 200;
            // 
            // BottomSplitContainer.Panel2
            // 
            this.BottomSplitContainer.Panel2.Controls.Add(this.TopSplitContainer);
            this.BottomSplitContainer.Panel2MinSize = 300;
            this.BottomSplitContainer.SeparatorStyle = ComponentFactory.Krypton.Toolkit.SeparatorStyle.HighProfile;
            this.BottomSplitContainer.Size = new System.Drawing.Size(1126, 650);
            this.BottomSplitContainer.SplitterDistance = 245;
            this.BottomSplitContainer.TabIndex = 1;
            this.BottomSplitContainer.SplitterMoving += new System.Windows.Forms.SplitterCancelEventHandler(this.BottomSpliteContainer_SplitterMoving);
            this.BottomSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.BottomSpliteContainer_SplitterMoved);
            // 
            // BtnRenameNode
            // 
            this.BtnRenameNode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnRenameNode.Location = new System.Drawing.Point(5, 429);
            this.BtnRenameNode.Name = "BtnRenameNode";
            this.BtnRenameNode.Palette = this.AppThemeDefinition;
            this.BtnRenameNode.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.BtnRenameNode.Size = new System.Drawing.Size(235, 29);
            this.BtnRenameNode.TabIndex = 4;
            this.BtnRenameNode.Values.Text = "Rename";
            this.BtnRenameNode.Click += new System.EventHandler(this.BtnRenameNodeClick);
            // 
            // BtnRemoveNode
            // 
            this.BtnRemoveNode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnRemoveNode.Location = new System.Drawing.Point(5, 464);
            this.BtnRemoveNode.Name = "BtnRemoveNode";
            this.BtnRemoveNode.Palette = this.AppThemeDefinition;
            this.BtnRemoveNode.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.BtnRemoveNode.Size = new System.Drawing.Size(235, 29);
            this.BtnRemoveNode.TabIndex = 3;
            this.BtnRemoveNode.Values.Text = "Remove";
            this.BtnRemoveNode.Click += new System.EventHandler(this.BtnRemoveNodeClick);
            // 
            // Browser
            // 
            this.Browser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Browser.HideSelection = false;
            this.Browser.ImageIndex = 2;
            this.Browser.ImageList = this.BrowserImageList;
            this.Browser.LabelEdit = true;
            this.Browser.Location = new System.Drawing.Point(5, 39);
            this.Browser.Name = "Browser";
            this.Browser.SelectedImageIndex = 3;
            this.Browser.ShowLines = false;
            this.Browser.ShowPlusMinus = false;
            this.Browser.ShowRootLines = false;
            this.Browser.Size = new System.Drawing.Size(235, 384);
            this.Browser.TabIndex = 1;
            this.Browser.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.BrowserAfterLabelEdit);
            this.Browser.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.BrowserAfterSelect);
            this.Browser.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BrowserKeyUp);
            // 
            // BrowserImageList
            // 
            this.BrowserImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("BrowserImageList.ImageStream")));
            this.BrowserImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.BrowserImageList.Images.SetKeyName(0, "AnimBall.png");
            this.BrowserImageList.Images.SetKeyName(1, "AnimBallHover.png");
            this.BrowserImageList.Images.SetKeyName(2, "SheetBall.png");
            this.BrowserImageList.Images.SetKeyName(3, "SheetBallHover.png");
            // 
            // BrowserHeader
            // 
            this.BrowserHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.BrowserHeader.Location = new System.Drawing.Point(2, 2);
            this.BrowserHeader.Name = "BrowserHeader";
            this.BrowserHeader.Palette = this.AppThemeDefinition;
            this.BrowserHeader.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.BrowserHeader.Size = new System.Drawing.Size(241, 29);
            this.BrowserHeader.TabIndex = 0;
            this.BrowserHeader.Values.Description = "";
            this.BrowserHeader.Values.Heading = "Browser";
            this.BrowserHeader.Values.Image = null;
            // 
            // TopSplitContainer
            // 
            this.TopSplitContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this.TopSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.TopSplitContainer.Name = "TopSplitContainer";
            this.TopSplitContainer.Palette = this.AppThemeDefinition;
            this.TopSplitContainer.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            // 
            // TopSplitContainer.Panel1
            // 
            this.TopSplitContainer.Panel1.Controls.Add(this.MainPanelSplitter);
            this.TopSplitContainer.Panel1.Palette = this.AppThemeDefinition;
            this.TopSplitContainer.Panel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.TopSplitContainer.Panel1MinSize = 450;
            // 
            // TopSplitContainer.Panel2
            // 
            this.TopSplitContainer.Panel2.Controls.Add(this.PropertiesView);
            this.TopSplitContainer.Panel2.Controls.Add(this.AnimPreviewDisplayHostPanel);
            this.TopSplitContainer.Panel2.Controls.Add(this.AnimPreviewDisplayButtonsPanel);
            this.TopSplitContainer.Panel2.Padding = new System.Windows.Forms.Padding(2);
            this.TopSplitContainer.Panel2.Palette = this.AppThemeDefinition;
            this.TopSplitContainer.Panel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.TopSplitContainer.Panel2MinSize = 230;
            this.TopSplitContainer.SeparatorStyle = ComponentFactory.Krypton.Toolkit.SeparatorStyle.HighProfile;
            this.TopSplitContainer.Size = new System.Drawing.Size(876, 650);
            this.TopSplitContainer.SplitterDistance = 620;
            this.TopSplitContainer.TabIndex = 0;
            this.TopSplitContainer.SplitterMoving += new System.Windows.Forms.SplitterCancelEventHandler(this.TopSplitContainer_SplitterMoving);
            this.TopSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.TopSplitContainer_SplitterMoved);
            // 
            // MainPanelSplitter
            // 
            this.MainPanelSplitter.Cursor = System.Windows.Forms.Cursors.Default;
            this.MainPanelSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanelSplitter.Location = new System.Drawing.Point(0, 0);
            this.MainPanelSplitter.Name = "MainPanelSplitter";
            this.MainPanelSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.MainPanelSplitter.Palette = this.AppThemeDefinition;
            this.MainPanelSplitter.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            // 
            // MainPanelSplitter.Panel1
            // 
            this.MainPanelSplitter.Panel1.Controls.Add(this.MainDisplayPanel);
            this.MainPanelSplitter.Panel1.Padding = new System.Windows.Forms.Padding(4);
            this.MainPanelSplitter.Panel1.Palette = this.AppThemeDefinition;
            this.MainPanelSplitter.Panel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.MainPanelSplitter.Panel1MinSize = 300;
            // 
            // MainPanelSplitter.Panel2
            // 
            this.MainPanelSplitter.Panel2.Controls.Add(this.PanFrameTime);
            this.MainPanelSplitter.Panel2.Controls.Add(this.AnimConfigDisplayPanel);
            this.MainPanelSplitter.Panel2.Controls.Add(this.AnimConfigDisplayButtonsPanel);
            this.MainPanelSplitter.Panel2.Padding = new System.Windows.Forms.Padding(1);
            this.MainPanelSplitter.Panel2.Palette = this.AppThemeDefinition;
            this.MainPanelSplitter.Panel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.MainPanelSplitter.Panel2MinSize = 200;
            this.MainPanelSplitter.SeparatorStyle = ComponentFactory.Krypton.Toolkit.SeparatorStyle.HighProfile;
            this.MainPanelSplitter.Size = new System.Drawing.Size(620, 650);
            this.MainPanelSplitter.SplitterDistance = 398;
            this.MainPanelSplitter.TabIndex = 0;
            this.MainPanelSplitter.SplitterMoving += new System.Windows.Forms.SplitterCancelEventHandler(this.MainPanelSplitter_SplitterMoving);
            this.MainPanelSplitter.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.MainPanelSplitter_SplitterMoved);
            // 
            // MainDisplayPanel
            // 
            this.MainDisplayPanel.AllowDrop = true;
            this.MainDisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainDisplayPanel.Location = new System.Drawing.Point(4, 4);
            this.MainDisplayPanel.Name = "MainDisplayPanel";
            this.MainDisplayPanel.Palette = this.AppThemeDefinition;
            this.MainDisplayPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.MainDisplayPanel.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.MainDisplayPanel.Size = new System.Drawing.Size(612, 390);
            this.MainDisplayPanel.TabIndex = 1;
            this.MainDisplayPanel.MouseLeave += new System.EventHandler(this.MainDisplayPanelMouseLeave);
            this.MainDisplayPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainDisplayPanelDragDrop);
            this.MainDisplayPanel.Resize += new System.EventHandler(this.MainDisplayPanelResize);
            this.MainDisplayPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainDisplayPanel_DragEnter);
            this.MainDisplayPanel.MouseEnter += new System.EventHandler(this.MainDisplayPanelMouseEnter);
            // 
            // PanFrameTime
            // 
            this.PanFrameTime.Controls.Add(this.LblFrameRate);
            this.PanFrameTime.Controls.Add(this.ChkSetFrameRateToAllFrames);
            this.PanFrameTime.Controls.Add(this.sldFrameTime);
            this.PanFrameTime.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanFrameTime.Location = new System.Drawing.Point(1, 197);
            this.PanFrameTime.Name = "PanFrameTime";
            this.PanFrameTime.Padding = new System.Windows.Forms.Padding(4);
            this.PanFrameTime.Palette = this.AppThemeDefinition;
            this.PanFrameTime.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.PanFrameTime.Size = new System.Drawing.Size(618, 49);
            this.PanFrameTime.TabIndex = 4;
            // 
            // LblFrameRate
            // 
            this.LblFrameRate.Dock = System.Windows.Forms.DockStyle.Left;
            this.LblFrameRate.Location = new System.Drawing.Point(4, 4);
            this.LblFrameRate.Name = "LblFrameRate";
            this.LblFrameRate.Palette = this.AppThemeDefinition;
            this.LblFrameRate.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.LblFrameRate.Size = new System.Drawing.Size(73, 41);
            this.LblFrameRate.TabIndex = 4;
            this.LblFrameRate.Values.Text = "Frame Rate:";
            // 
            // ChkSetFrameRateToAllFrames
            // 
            this.ChkSetFrameRateToAllFrames.Checked = true;
            this.ChkSetFrameRateToAllFrames.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkSetFrameRateToAllFrames.Dock = System.Windows.Forms.DockStyle.Right;
            this.ChkSetFrameRateToAllFrames.Location = new System.Drawing.Point(516, 4);
            this.ChkSetFrameRateToAllFrames.Name = "ChkSetFrameRateToAllFrames";
            this.ChkSetFrameRateToAllFrames.Palette = this.AppThemeDefinition;
            this.ChkSetFrameRateToAllFrames.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.ChkSetFrameRateToAllFrames.Size = new System.Drawing.Size(98, 41);
            this.ChkSetFrameRateToAllFrames.TabIndex = 3;
            this.ChkSetFrameRateToAllFrames.Values.Text = "To All Frames";
            // 
            // sldFrameTime
            // 
            this.sldFrameTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sldFrameTime.DrawBackground = true;
            this.sldFrameTime.LargeChange = 10;
            this.sldFrameTime.Location = new System.Drawing.Point(83, 15);
            this.sldFrameTime.Maximum = 100;
            this.sldFrameTime.Minimum = 1;
            this.sldFrameTime.Name = "sldFrameTime";
            this.sldFrameTime.Palette = this.AppThemeDefinition;
            this.sldFrameTime.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.sldFrameTime.Size = new System.Drawing.Size(427, 27);
            this.sldFrameTime.SmallChange = 5;
            this.sldFrameTime.TabIndex = 2;
            this.sldFrameTime.Value = 1;
            this.sldFrameTime.ValueChanged += new System.EventHandler(this.SldFrameTimeValueChanged);
            this.sldFrameTime.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sldFrameTime_MouseDown);
            this.sldFrameTime.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sldFrameTime_MouseUp);
            // 
            // AnimConfigDisplayPanel
            // 
            this.AnimConfigDisplayPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AnimConfigDisplayPanel.Location = new System.Drawing.Point(3, 54);
            this.AnimConfigDisplayPanel.Name = "AnimConfigDisplayPanel";
            this.AnimConfigDisplayPanel.Palette = this.AppThemeDefinition;
            this.AnimConfigDisplayPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.AnimConfigDisplayPanel.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.AnimConfigDisplayPanel.Size = new System.Drawing.Size(614, 141);
            this.AnimConfigDisplayPanel.TabIndex = 1;
            this.AnimConfigDisplayPanel.MouseLeave += new System.EventHandler(this.AnimConfigDisplayPanelMouseLeave);
            this.AnimConfigDisplayPanel.Resize += new System.EventHandler(this.AnimConfigDisplayPanelResize);
            this.AnimConfigDisplayPanel.MouseEnter += new System.EventHandler(this.AnimConfigDisplayPanelMouseEnter);
            // 
            // AnimConfigDisplayButtonsPanel
            // 
            this.AnimConfigDisplayButtonsPanel.Controls.Add(this.BtnAddContainer);
            this.AnimConfigDisplayButtonsPanel.Controls.Add(this.BtnRemoveSelectedContainers);
            this.AnimConfigDisplayButtonsPanel.Controls.Add(this.BtnClearSelectedContainers);
            this.AnimConfigDisplayButtonsPanel.Controls.Add(this.AnimConfigChkBoxHostPanel);
            this.AnimConfigDisplayButtonsPanel.Controls.Add(this.AnimBuilderHeader);
            this.AnimConfigDisplayButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.AnimConfigDisplayButtonsPanel.Location = new System.Drawing.Point(1, 1);
            this.AnimConfigDisplayButtonsPanel.Name = "AnimConfigDisplayButtonsPanel";
            this.AnimConfigDisplayButtonsPanel.Palette = this.AppThemeDefinition;
            this.AnimConfigDisplayButtonsPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.AnimConfigDisplayButtonsPanel.Size = new System.Drawing.Size(618, 49);
            this.AnimConfigDisplayButtonsPanel.TabIndex = 0;
            // 
            // BtnAddContainer
            // 
            this.BtnAddContainer.Location = new System.Drawing.Point(283, 3);
            this.BtnAddContainer.Name = "BtnAddContainer";
            this.BtnAddContainer.Palette = this.AppThemeDefinition;
            this.BtnAddContainer.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.BtnAddContainer.Size = new System.Drawing.Size(44, 44);
            this.BtnAddContainer.TabIndex = 6;
            this.BtnAddContainer.Values.Image = global::SpriteVortex.Properties.Resources.AddContainer;
            this.BtnAddContainer.Values.Text = "";
            this.BtnAddContainer.Click += new System.EventHandler(this.BtnAddContainerClick);
            // 
            // BtnRemoveSelectedContainers
            // 
            this.BtnRemoveSelectedContainers.Location = new System.Drawing.Point(233, 3);
            this.BtnRemoveSelectedContainers.Name = "BtnRemoveSelectedContainers";
            this.BtnRemoveSelectedContainers.Palette = this.AppThemeDefinition;
            this.BtnRemoveSelectedContainers.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.BtnRemoveSelectedContainers.Size = new System.Drawing.Size(44, 44);
            this.BtnRemoveSelectedContainers.TabIndex = 5;
            this.BtnRemoveSelectedContainers.Values.Image = global::SpriteVortex.Properties.Resources.RemoveContainers;
            this.BtnRemoveSelectedContainers.Values.Text = "";
            this.BtnRemoveSelectedContainers.Click += new System.EventHandler(this.BtnRemoveSelectedContainersClick);
            // 
            // BtnClearSelectedContainers
            // 
            this.BtnClearSelectedContainers.Location = new System.Drawing.Point(183, 3);
            this.BtnClearSelectedContainers.Name = "BtnClearSelectedContainers";
            this.BtnClearSelectedContainers.Palette = this.AppThemeDefinition;
            this.BtnClearSelectedContainers.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.BtnClearSelectedContainers.Size = new System.Drawing.Size(44, 44);
            this.BtnClearSelectedContainers.TabIndex = 4;
            this.BtnClearSelectedContainers.Values.Image = global::SpriteVortex.Properties.Resources.eraseContainers;
            this.BtnClearSelectedContainers.Values.Text = "";
            this.BtnClearSelectedContainers.Click += new System.EventHandler(this.BtnClearSelectedContainersClick);
            // 
            // AnimConfigChkBoxHostPanel
            // 
            this.AnimConfigChkBoxHostPanel.Controls.Add(this.ChkAnimLooped);
            this.AnimConfigChkBoxHostPanel.Controls.Add(this.ChkAnimPingPong);
            this.AnimConfigChkBoxHostPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.AnimConfigChkBoxHostPanel.Location = new System.Drawing.Point(474, 0);
            this.AnimConfigChkBoxHostPanel.Name = "AnimConfigChkBoxHostPanel";
            this.AnimConfigChkBoxHostPanel.Palette = this.AppThemeDefinition;
            this.AnimConfigChkBoxHostPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.AnimConfigChkBoxHostPanel.Size = new System.Drawing.Size(144, 49);
            this.AnimConfigChkBoxHostPanel.TabIndex = 3;
            // 
            // ChkAnimLooped
            // 
            this.ChkAnimLooped.Dock = System.Windows.Forms.DockStyle.Left;
            this.ChkAnimLooped.Location = new System.Drawing.Point(0, 0);
            this.ChkAnimLooped.Name = "ChkAnimLooped";
            this.ChkAnimLooped.Palette = this.AppThemeDefinition;
            this.ChkAnimLooped.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.ChkAnimLooped.Size = new System.Drawing.Size(66, 49);
            this.ChkAnimLooped.TabIndex = 1;
            this.ChkAnimLooped.Values.Text = "Looped";
            this.ChkAnimLooped.CheckedChanged += new System.EventHandler(this.ChkAnimLoopedCheckedChanged);
            // 
            // ChkAnimPingPong
            // 
            this.ChkAnimPingPong.Dock = System.Windows.Forms.DockStyle.Right;
            this.ChkAnimPingPong.Location = new System.Drawing.Point(66, 0);
            this.ChkAnimPingPong.Name = "ChkAnimPingPong";
            this.ChkAnimPingPong.Palette = this.AppThemeDefinition;
            this.ChkAnimPingPong.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.ChkAnimPingPong.Size = new System.Drawing.Size(78, 49);
            this.ChkAnimPingPong.TabIndex = 2;
            this.ChkAnimPingPong.Values.Text = "PingPong";
            this.ChkAnimPingPong.CheckedChanged += new System.EventHandler(this.ChkAnimPingPongCheckedChanged);
            // 
            // AnimBuilderHeader
            // 
            this.AnimBuilderHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.AnimBuilderHeader.Location = new System.Drawing.Point(0, 0);
            this.AnimBuilderHeader.Name = "AnimBuilderHeader";
            this.AnimBuilderHeader.Palette = this.AppThemeDefinition;
            this.AnimBuilderHeader.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.AnimBuilderHeader.Size = new System.Drawing.Size(177, 49);
            this.AnimBuilderHeader.TabIndex = 0;
            this.AnimBuilderHeader.Values.Description = "";
            this.AnimBuilderHeader.Values.Heading = "Animation Manager";
            this.AnimBuilderHeader.Values.Image = null;
            // 
            // PropertiesView
            // 
            this.PropertiesView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(230)))), ((int)(((byte)(232)))));
            this.PropertiesView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PropertiesView.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(226)))));
            this.PropertiesView.Location = new System.Drawing.Point(2, 383);
            this.PropertiesView.Name = "PropertiesView";
            this.PropertiesView.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.PropertiesView.Size = new System.Drawing.Size(247, 265);
            this.PropertiesView.TabIndex = 2;
            // 
            // AnimPreviewDisplayHostPanel
            // 
            this.AnimPreviewDisplayHostPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AnimPreviewDisplayHostPanel.Controls.Add(this.AnimPreviewDisplayControlButtonsTable);
            this.AnimPreviewDisplayHostPanel.Controls.Add(this.AnimPreviewDisplayPanel);
            this.AnimPreviewDisplayHostPanel.Controls.Add(this.AnimPreviewDisplayHeader);
            this.AnimPreviewDisplayHostPanel.Location = new System.Drawing.Point(5, 61);
            this.AnimPreviewDisplayHostPanel.Name = "AnimPreviewDisplayHostPanel";
            this.AnimPreviewDisplayHostPanel.Palette = this.AppThemeDefinition;
            this.AnimPreviewDisplayHostPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.AnimPreviewDisplayHostPanel.Size = new System.Drawing.Size(241, 282);
            this.AnimPreviewDisplayHostPanel.TabIndex = 1;
            // 
            // AnimPreviewDisplayControlButtonsTable
            // 
            this.AnimPreviewDisplayControlButtonsTable.ColumnCount = 5;
            this.AnimPreviewDisplayControlButtonsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.AnimPreviewDisplayControlButtonsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.AnimPreviewDisplayControlButtonsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.AnimPreviewDisplayControlButtonsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.AnimPreviewDisplayControlButtonsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.AnimPreviewDisplayControlButtonsTable.Controls.Add(this.BtnTogglePlayAnimation, 0, 0);
            this.AnimPreviewDisplayControlButtonsTable.Controls.Add(this.BtnGoLastFrame, 1, 0);
            this.AnimPreviewDisplayControlButtonsTable.Controls.Add(this.BtnGoNextFrame, 2, 0);
            this.AnimPreviewDisplayControlButtonsTable.Controls.Add(this.BtnResetAnimation, 3, 0);
            this.AnimPreviewDisplayControlButtonsTable.Controls.Add(this.BtnToggleOnionSkinning, 4, 0);
            this.AnimPreviewDisplayControlButtonsTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.AnimPreviewDisplayControlButtonsTable.Location = new System.Drawing.Point(0, 235);
            this.AnimPreviewDisplayControlButtonsTable.Name = "AnimPreviewDisplayControlButtonsTable";
            this.AnimPreviewDisplayControlButtonsTable.RowCount = 1;
            this.AnimPreviewDisplayControlButtonsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.AnimPreviewDisplayControlButtonsTable.Size = new System.Drawing.Size(241, 47);
            this.AnimPreviewDisplayControlButtonsTable.TabIndex = 2;
            // 
            // BtnTogglePlayAnimation
            // 
            this.BtnTogglePlayAnimation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnTogglePlayAnimation.Location = new System.Drawing.Point(3, 3);
            this.BtnTogglePlayAnimation.Name = "BtnTogglePlayAnimation";
            this.BtnTogglePlayAnimation.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.BtnTogglePlayAnimation.Size = new System.Drawing.Size(42, 41);
            this.BtnTogglePlayAnimation.TabIndex = 0;
            this.BtnTogglePlayAnimation.Values.Image = global::SpriteVortex.Properties.Resources.Play;
            this.BtnTogglePlayAnimation.Values.Text = "";
            this.BtnTogglePlayAnimation.Click += new System.EventHandler(this.BtnTogglePlayAnimationClick);
            // 
            // BtnGoLastFrame
            // 
            this.BtnGoLastFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnGoLastFrame.Location = new System.Drawing.Point(51, 3);
            this.BtnGoLastFrame.Name = "BtnGoLastFrame";
            this.BtnGoLastFrame.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.BtnGoLastFrame.Size = new System.Drawing.Size(42, 41);
            this.BtnGoLastFrame.TabIndex = 2;
            this.BtnGoLastFrame.Values.Image = global::SpriteVortex.Properties.Resources.left;
            this.BtnGoLastFrame.Values.Text = "";
            this.BtnGoLastFrame.Click += new System.EventHandler(this.BtnGoLastFrameClick);
            // 
            // BtnGoNextFrame
            // 
            this.BtnGoNextFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnGoNextFrame.Location = new System.Drawing.Point(99, 3);
            this.BtnGoNextFrame.Name = "BtnGoNextFrame";
            this.BtnGoNextFrame.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.BtnGoNextFrame.Size = new System.Drawing.Size(42, 41);
            this.BtnGoNextFrame.TabIndex = 3;
            this.BtnGoNextFrame.Values.Image = global::SpriteVortex.Properties.Resources.right;
            this.BtnGoNextFrame.Values.Text = "";
            this.BtnGoNextFrame.Click += new System.EventHandler(this.BtnGoNextFrameClick);
            // 
            // BtnResetAnimation
            // 
            this.BtnResetAnimation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnResetAnimation.Location = new System.Drawing.Point(147, 3);
            this.BtnResetAnimation.Name = "BtnResetAnimation";
            this.BtnResetAnimation.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.BtnResetAnimation.Size = new System.Drawing.Size(42, 41);
            this.BtnResetAnimation.TabIndex = 4;
            this.BtnResetAnimation.Values.Image = global::SpriteVortex.Properties.Resources.PlayAll;
            this.BtnResetAnimation.Values.Text = "";
            this.BtnResetAnimation.Click += new System.EventHandler(this.BtnResetAnimationClick);
            // 
            // BtnToggleOnionSkinning
            // 
            this.BtnToggleOnionSkinning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnToggleOnionSkinning.Location = new System.Drawing.Point(195, 3);
            this.BtnToggleOnionSkinning.Name = "BtnToggleOnionSkinning";
            this.BtnToggleOnionSkinning.Size = new System.Drawing.Size(43, 41);
            this.BtnToggleOnionSkinning.StateCheckedNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(232)))), ((int)(((byte)(84)))));
            this.BtnToggleOnionSkinning.StateCheckedNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(186)))), ((int)(((byte)(67)))));
            this.BtnToggleOnionSkinning.StateCheckedNormal.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnToggleOnionSkinning.StateNormal.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnToggleOnionSkinning.TabIndex = 5;
            this.BtnToggleOnionSkinning.Values.Text = "Onion \r\n Skin";
            this.BtnToggleOnionSkinning.CheckedChanged += new System.EventHandler(this.BtnToggleOnionSkinningCheckedChanged);
            // 
            // AnimPreviewDisplayPanel
            // 
            this.AnimPreviewDisplayPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AnimPreviewDisplayPanel.Location = new System.Drawing.Point(0, 29);
            this.AnimPreviewDisplayPanel.Name = "AnimPreviewDisplayPanel";
            this.AnimPreviewDisplayPanel.Palette = this.AppThemeDefinition;
            this.AnimPreviewDisplayPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.AnimPreviewDisplayPanel.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.AnimPreviewDisplayPanel.Size = new System.Drawing.Size(241, 205);
            this.AnimPreviewDisplayPanel.TabIndex = 1;
            this.AnimPreviewDisplayPanel.MouseLeave += new System.EventHandler(this.AnimPreviewDisplayPanelMouseLeave);
            this.AnimPreviewDisplayPanel.Resize += new System.EventHandler(this.AnimPreviewDisplayPanelResize);
            this.AnimPreviewDisplayPanel.MouseEnter += new System.EventHandler(this.AnimPreviewDisplayPanelMouseEnter);
            // 
            // AnimPreviewDisplayHeader
            // 
            this.AnimPreviewDisplayHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.AnimPreviewDisplayHeader.Location = new System.Drawing.Point(0, 0);
            this.AnimPreviewDisplayHeader.Name = "AnimPreviewDisplayHeader";
            this.AnimPreviewDisplayHeader.Palette = this.AppThemeDefinition;
            this.AnimPreviewDisplayHeader.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.AnimPreviewDisplayHeader.Size = new System.Drawing.Size(241, 29);
            this.AnimPreviewDisplayHeader.TabIndex = 0;
            this.AnimPreviewDisplayHeader.Values.Description = "";
            this.AnimPreviewDisplayHeader.Values.Heading = "Animation Preview";
            this.AnimPreviewDisplayHeader.Values.Image = null;
            // 
            // AnimPreviewDisplayButtonsPanel
            // 
            this.AnimPreviewDisplayButtonsPanel.Controls.Add(this.BtnAnimPreviewFitViewToContent);
            this.AnimPreviewDisplayButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.AnimPreviewDisplayButtonsPanel.Location = new System.Drawing.Point(2, 2);
            this.AnimPreviewDisplayButtonsPanel.Name = "AnimPreviewDisplayButtonsPanel";
            this.AnimPreviewDisplayButtonsPanel.Palette = this.AppThemeDefinition;
            this.AnimPreviewDisplayButtonsPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.AnimPreviewDisplayButtonsPanel.Size = new System.Drawing.Size(247, 53);
            this.AnimPreviewDisplayButtonsPanel.TabIndex = 0;
            // 
            // BtnAnimPreviewFitViewToContent
            // 
            this.BtnAnimPreviewFitViewToContent.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnAnimPreviewFitViewToContent.Location = new System.Drawing.Point(0, 24);
            this.BtnAnimPreviewFitViewToContent.Name = "BtnAnimPreviewFitViewToContent";
            this.BtnAnimPreviewFitViewToContent.Palette = this.AppThemeDefinition;
            this.BtnAnimPreviewFitViewToContent.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.BtnAnimPreviewFitViewToContent.Size = new System.Drawing.Size(247, 29);
            this.BtnAnimPreviewFitViewToContent.TabIndex = 0;
            this.BtnAnimPreviewFitViewToContent.Values.Text = "Fit View to Content";
            this.BtnAnimPreviewFitViewToContent.Click += new System.EventHandler(this.BtnAnimPreviewFitViewToContentClick);
            // 
            // MenuStrip
            // 
            this.MenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(230)))), ((int)(((byte)(232)))));
            this.MenuStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.spriteSheetToolStripMenuItem,
            this.animationToolStripMenuItem,
            this.configureToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(1126, 24);
            this.MenuStrip.TabIndex = 2;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importSpriteSheetToolStripMenuItem,
            this.importSpriteSheetToolStripMenuItem1,
            this.importSpriteMapXMLToolStripMenuItem,
            this.exportSpriteMapXMLToolStripMenuItem,
            this.eraseFoldersHistoryToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // importSpriteSheetToolStripMenuItem
            // 
            this.importSpriteSheetToolStripMenuItem.Name = "importSpriteSheetToolStripMenuItem";
            this.importSpriteSheetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.importSpriteSheetToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.importSpriteSheetToolStripMenuItem.Text = "Import SpriteSheet";
            this.importSpriteSheetToolStripMenuItem.Click += new System.EventHandler(this.BtnImportSpriteSheetButtonClick);
            // 
            // importSpriteSheetToolStripMenuItem1
            // 
            this.importSpriteSheetToolStripMenuItem1.Name = "importSpriteSheetToolStripMenuItem1";
            this.importSpriteSheetToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.importSpriteSheetToolStripMenuItem1.Size = new System.Drawing.Size(324, 22);
            this.importSpriteSheetToolStripMenuItem1.Text = "Import SpriteSheet from separate images";
            this.importSpriteSheetToolStripMenuItem1.Click += new System.EventHandler(this.BtnImportSpriteSheetFromSeparateImagesButtonClick);
            // 
            // importSpriteMapXMLToolStripMenuItem
            // 
            this.importSpriteMapXMLToolStripMenuItem.Name = "importSpriteMapXMLToolStripMenuItem";
            this.importSpriteMapXMLToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.importSpriteMapXMLToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.importSpriteMapXMLToolStripMenuItem.Text = "Import SpriteMap XML";
            this.importSpriteMapXMLToolStripMenuItem.Click += new System.EventHandler(this.BtnImportSpriteMapXmlClick);
            // 
            // exportSpriteMapXMLToolStripMenuItem
            // 
            this.exportSpriteMapXMLToolStripMenuItem.Name = "exportSpriteMapXMLToolStripMenuItem";
            this.exportSpriteMapXMLToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.exportSpriteMapXMLToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.exportSpriteMapXMLToolStripMenuItem.Text = "Export SpriteMap XML";
            this.exportSpriteMapXMLToolStripMenuItem.Click += new System.EventHandler(this.BtnExportSpriteMapXmlClick);
            // 
            // eraseFoldersHistoryToolStripMenuItem
            // 
            this.eraseFoldersHistoryToolStripMenuItem.Name = "eraseFoldersHistoryToolStripMenuItem";
            this.eraseFoldersHistoryToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.eraseFoldersHistoryToolStripMenuItem.Text = "Erase Folders History";
            this.eraseFoldersHistoryToolStripMenuItem.Click += new System.EventHandler(this.eraseFoldersHistoryToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // spriteSheetToolStripMenuItem
            // 
            this.spriteSheetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportImageFromCurrentSpriteSheetToolStripMenuItem,
            this.exportSpriteImagesFromCurrentSpriteSheetToolStripMenuItem});
            this.spriteSheetToolStripMenuItem.Name = "spriteSheetToolStripMenuItem";
            this.spriteSheetToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.spriteSheetToolStripMenuItem.Text = "SpriteSheet";
            // 
            // exportImageFromCurrentSpriteSheetToolStripMenuItem
            // 
            this.exportImageFromCurrentSpriteSheetToolStripMenuItem.Name = "exportImageFromCurrentSpriteSheetToolStripMenuItem";
            this.exportImageFromCurrentSpriteSheetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.exportImageFromCurrentSpriteSheetToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.exportImageFromCurrentSpriteSheetToolStripMenuItem.Text = "Export Image";
            this.exportImageFromCurrentSpriteSheetToolStripMenuItem.Click += new System.EventHandler(this.BtnExportSpriteSheetImageClick);
            // 
            // exportSpriteImagesFromCurrentSpriteSheetToolStripMenuItem
            // 
            this.exportSpriteImagesFromCurrentSpriteSheetToolStripMenuItem.Name = "exportSpriteImagesFromCurrentSpriteSheetToolStripMenuItem";
            this.exportSpriteImagesFromCurrentSpriteSheetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                        | System.Windows.Forms.Keys.I)));
            this.exportSpriteImagesFromCurrentSpriteSheetToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.exportSpriteImagesFromCurrentSpriteSheetToolStripMenuItem.Text = "Export Sprites Images";
            this.exportSpriteImagesFromCurrentSpriteSheetToolStripMenuItem.Click += new System.EventHandler(this.BtnExportFramesImagesClick);
            // 
            // animationToolStripMenuItem
            // 
            this.animationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewToolStripMenuItem,
            this.exportAsAnimatedGIFToolStripMenuItem});
            this.animationToolStripMenuItem.Name = "animationToolStripMenuItem";
            this.animationToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.animationToolStripMenuItem.Text = "Animation";
            // 
            // createNewToolStripMenuItem
            // 
            this.createNewToolStripMenuItem.Name = "createNewToolStripMenuItem";
            this.createNewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.createNewToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.createNewToolStripMenuItem.Text = "Create New";
            this.createNewToolStripMenuItem.Click += new System.EventHandler(this.BtnAddAnimationClick);
            // 
            // exportAsAnimatedGIFToolStripMenuItem
            // 
            this.exportAsAnimatedGIFToolStripMenuItem.Name = "exportAsAnimatedGIFToolStripMenuItem";
            this.exportAsAnimatedGIFToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.exportAsAnimatedGIFToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.exportAsAnimatedGIFToolStripMenuItem.Text = "Export as Animated GIF";
            this.exportAsAnimatedGIFToolStripMenuItem.Click += new System.EventHandler(this.BtnExportGifFromAnimationClick);
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.configureToolStripMenuItem.Text = "Options";
            this.configureToolStripMenuItem.Click += new System.EventHandler(this.BtnConfigureClick);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpToolStripMenuItem1,
            this.AboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.HelpToolStripMenuItem.Text = "Help";
            // 
            // HelpToolStripMenuItem1
            // 
            this.HelpToolStripMenuItem1.Name = "HelpToolStripMenuItem1";
            this.HelpToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.HelpToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.HelpToolStripMenuItem1.Text = "Help";
            this.HelpToolStripMenuItem1.Click += new System.EventHandler(this.HelpToolStripMenuItem1_Click);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.AboutToolStripMenuItem.Text = "About";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // AppTimer
            // 
            this.AppTimer.Interval = 20;
            this.AppTimer.Tick += new System.EventHandler(this.AppTimerTick);
            // 
            // messagePanel
            // 
            this.messagePanel.Controls.Add(this.lblMessageText);
            this.messagePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.messagePanel.Location = new System.Drawing.Point(0, 70);
            this.messagePanel.Name = "messagePanel";
            this.messagePanel.Size = new System.Drawing.Size(1126, 25);
            this.messagePanel.StateNormal.Color1 = System.Drawing.Color.WhiteSmoke;
            this.messagePanel.StateNormal.Color2 = System.Drawing.Color.LightGray;
            this.messagePanel.StateNormal.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Linear;
            this.messagePanel.TabIndex = 3;
            // 
            // lblMessageText
            // 
            this.lblMessageText.Location = new System.Drawing.Point(535, 3);
            this.lblMessageText.Name = "lblMessageText";
            this.lblMessageText.Palette = this.AppThemeDefinition;
            this.lblMessageText.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.lblMessageText.Size = new System.Drawing.Size(56, 19);
            this.lblMessageText.StateNormal.ShortText.Color1 = System.Drawing.Color.White;
            this.lblMessageText.StateNormal.ShortText.Color2 = System.Drawing.Color.White;
            this.lblMessageText.TabIndex = 0;
            this.lblMessageText.Values.Text = "message";
            this.lblMessageText.Visible = false;
            // 
            // MessageTimer
            // 
            this.MessageTimer.Interval = 2000;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(226)))));
            this.ClientSize = new System.Drawing.Size(1126, 748);
            this.Controls.Add(this.messagePanel);
            this.Controls.Add(this.BottomSplitContainer);
            this.Controls.Add(this.AppToolStrip);
            this.Controls.Add(this.MenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.MenuStrip;
            this.MinimumSize = new System.Drawing.Size(1142, 783);
            this.Name = "MainWindow";
            this.Palette = this.AppThemeDefinition;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpriteVortex";
            this.Load += new System.EventHandler(this.MainWindowLoad);
            this.ResizeBegin += new System.EventHandler(this.MainWindow_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.MainWindow_ResizeEnd);
            this.AppToolStrip.ResumeLayout(false);
            this.AppToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BottomSplitContainer.Panel1)).EndInit();
            this.BottomSplitContainer.Panel1.ResumeLayout(false);
            this.BottomSplitContainer.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BottomSplitContainer.Panel2)).EndInit();
            this.BottomSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BottomSplitContainer)).EndInit();
            this.BottomSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TopSplitContainer.Panel1)).EndInit();
            this.TopSplitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TopSplitContainer.Panel2)).EndInit();
            this.TopSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TopSplitContainer)).EndInit();
            this.TopSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainPanelSplitter.Panel1)).EndInit();
            this.MainPanelSplitter.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainPanelSplitter.Panel2)).EndInit();
            this.MainPanelSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainPanelSplitter)).EndInit();
            this.MainPanelSplitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainDisplayPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanFrameTime)).EndInit();
            this.PanFrameTime.ResumeLayout(false);
            this.PanFrameTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnimConfigDisplayPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnimConfigDisplayButtonsPanel)).EndInit();
            this.AnimConfigDisplayButtonsPanel.ResumeLayout(false);
            this.AnimConfigDisplayButtonsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnimConfigChkBoxHostPanel)).EndInit();
            this.AnimConfigChkBoxHostPanel.ResumeLayout(false);
            this.AnimConfigChkBoxHostPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnimPreviewDisplayHostPanel)).EndInit();
            this.AnimPreviewDisplayHostPanel.ResumeLayout(false);
            this.AnimPreviewDisplayHostPanel.PerformLayout();
            this.AnimPreviewDisplayControlButtonsTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AnimPreviewDisplayPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnimPreviewDisplayButtonsPanel)).EndInit();
            this.AnimPreviewDisplayButtonsPanel.ResumeLayout(false);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messagePanel)).EndInit();
            this.messagePanel.ResumeLayout(false);
            this.messagePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPalette AppThemeDefinition;
        private System.Windows.Forms.ToolStrip AppToolStrip;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer BottomSplitContainer;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer TopSplitContainer;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader BrowserHeader;
        private System.Windows.Forms.TreeView Browser;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer MainPanelSplitter;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel MainDisplayPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel AnimConfigDisplayPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel AnimConfigDisplayButtonsPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel AnimPreviewDisplayButtonsPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel AnimPreviewDisplayHostPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader AnimPreviewDisplayHeader;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel AnimPreviewDisplayPanel;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel AnimPreviewDisplayControlButtonsTable;
        private ComponentFactory.Krypton.Toolkit.KryptonButton BtnTogglePlayAnimation;
        private ComponentFactory.Krypton.Toolkit.KryptonButton BtnGoLastFrame;
        private ComponentFactory.Krypton.Toolkit.KryptonButton BtnGoNextFrame;
        private ComponentFactory.Krypton.Toolkit.KryptonButton BtnResetAnimation;
        private System.Windows.Forms.Timer AppTimer;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ImageList BrowserImageList;
        private System.Windows.Forms.ToolStripButton BtnConfigure;
        private ComponentFactory.Krypton.Toolkit.KryptonButton BtnRemoveNode;
        private ComponentFactory.Krypton.Toolkit.KryptonButton BtnRenameNode;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader AnimBuilderHeader;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel PanFrameTime;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ChkSetFrameRateToAllFrames;
        private ComponentFactory.Krypton.Toolkit.KryptonTrackBar sldFrameTime;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel LblFrameRate;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ChkAnimPingPong;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ChkAnimLooped;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel messagePanel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblMessageText;
        private System.Windows.Forms.Timer MessageTimer;
        private ComponentFactory.Krypton.Toolkit.KryptonButton BtnAnimPreviewFitViewToContent;
        private System.Windows.Forms.ToolStripButton BtnAddAnimation;
        private System.Windows.Forms.ToolStripButton BtnExportSpriteSheetImage;
        private System.Windows.Forms.ToolStripButton BtnExportSpriteSheetFramesImages;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton BtnExportSpriteMapXml;
        private System.Windows.Forms.ToolStripButton BtnExportGifFromAnimation;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel AnimConfigChkBoxHostPanel;
        private System.Windows.Forms.ToolStripSplitButton BtnImportSpriteSheet;
        private System.Windows.Forms.ToolStripSplitButton BtnImportSpriteSheetFromSeparateImages;
        private System.Windows.Forms.ToolStripMenuItem eraseFoldersHistoryToolStripMenuItem;
        private ComponentFactory.Krypton.Toolkit.KryptonButton BtnRemoveSelectedContainers;
        private ComponentFactory.Krypton.Toolkit.KryptonButton BtnClearSelectedContainers;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton BtnToggleOnionSkinning;
        private ComponentFactory.Krypton.Toolkit.KryptonButton BtnAddContainer;
        private System.Windows.Forms.PropertyGrid PropertiesView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton BtnImportSpriteMap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem importSpriteSheetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importSpriteSheetToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem importSpriteMapXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSpriteMapXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spriteSheetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportImageFromCurrentSpriteSheetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSpriteImagesFromCurrentSpriteSheetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem animationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAsAnimatedGIFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;

        
    }
}

