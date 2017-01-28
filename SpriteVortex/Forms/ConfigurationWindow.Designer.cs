namespace SpriteVortex
{
    partial class ConfigurationWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationWindow));
            this.AppThemeDefinition = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.lblCamSpeed = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtCamSpeed = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.btnConfirmConfigs = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.lblTextureFiltering = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblViewZoom = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblSelectSprite = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblMarkupSprite = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblMoveCam = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cmbTextureFilterMode = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.lblBgColor = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblFrameRectColor = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblFrameRectHoveredColor = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblFrameRectSelColor = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.generalTabTable = new System.Windows.Forms.TableLayoutPanel();
            this.tabInput = new System.Windows.Forms.TabPage();
            this.tabInputTable = new System.Windows.Forms.TableLayoutPanel();
            this.InputDetectorViewZoom = new SpriteVortex.InputControl2();
            this.InputDetectorSelectSprite = new SpriteVortex.InputControl2();
            this.InputDetectorMarkupSprite = new SpriteVortex.InputControl2();
            this.InputDetectorMoveCamera = new SpriteVortex.InputControl2();
            this.tabStyle = new System.Windows.Forms.TabPage();
            this.tabStyleTable = new System.Windows.Forms.TableLayoutPanel();
            this.ColorPickerFrameRectSelected = new SpriteVortex.ColorPicker();
            this.ColorPickerFrameRectHovered = new SpriteVortex.ColorPicker();
            this.ColorPickerFrameRect = new SpriteVortex.ColorPicker();
            this.ColorPickerBg = new SpriteVortex.ColorPicker();
            this.tabExporting = new System.Windows.Forms.TabPage();
            this.tabExportingTable = new System.Windows.Forms.TableLayoutPanel();
            this.lblImageExportOpt = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblColorClearingMode = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.radColorKeyMode = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.radAlphaMode = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.lblMappingExportOpt = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.chkPackSpriteSheet = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.lblPackingOpt = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.chkForceSquare = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkForcePowTwo = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.lblPadding = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.udPadding = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTextureFilterMode)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.generalTabTable.SuspendLayout();
            this.tabInput.SuspendLayout();
            this.tabInputTable.SuspendLayout();
            this.tabStyle.SuspendLayout();
            this.tabStyleTable.SuspendLayout();
            this.tabExporting.SuspendLayout();
            this.tabExportingTable.SuspendLayout();
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
            // lblCamSpeed
            // 
            this.lblCamSpeed.Location = new System.Drawing.Point(3, 3);
            this.lblCamSpeed.Name = "lblCamSpeed";
            this.lblCamSpeed.Palette = this.AppThemeDefinition;
            this.lblCamSpeed.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.lblCamSpeed.Size = new System.Drawing.Size(118, 19);
            this.lblCamSpeed.TabIndex = 0;
            this.lblCamSpeed.Values.Text = "View Camera Speed: ";
            // 
            // txtCamSpeed
            // 
            this.txtCamSpeed.Location = new System.Drawing.Point(152, 3);
            this.txtCamSpeed.Name = "txtCamSpeed";
            this.txtCamSpeed.Palette = this.AppThemeDefinition;
            this.txtCamSpeed.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.txtCamSpeed.Size = new System.Drawing.Size(72, 31);
            this.txtCamSpeed.TabIndex = 1;
            // 
            // btnConfirmConfigs
            // 
            this.btnConfirmConfigs.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnConfirmConfigs.Location = new System.Drawing.Point(296, 2);
            this.btnConfirmConfigs.Name = "btnConfirmConfigs";
            this.btnConfirmConfigs.Palette = this.AppThemeDefinition;
            this.btnConfirmConfigs.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.btnConfirmConfigs.Size = new System.Drawing.Size(90, 32);
            this.btnConfirmConfigs.TabIndex = 2;
            this.btnConfirmConfigs.Values.Text = "Apply";
            this.btnConfirmConfigs.Click += new System.EventHandler(this.BtnConfirmConfigsClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(386, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Palette = this.AppThemeDefinition;
            this.btnCancel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.btnCancel.Size = new System.Drawing.Size(90, 32);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnConfirmConfigs);
            this.kryptonPanel1.Controls.Add(this.btnCancel);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 475);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(2);
            this.kryptonPanel1.Palette = this.AppThemeDefinition;
            this.kryptonPanel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonPanel1.Size = new System.Drawing.Size(478, 36);
            this.kryptonPanel1.TabIndex = 4;
            // 
            // lblTextureFiltering
            // 
            this.lblTextureFiltering.Location = new System.Drawing.Point(3, 44);
            this.lblTextureFiltering.Name = "lblTextureFiltering";
            this.lblTextureFiltering.Palette = this.AppThemeDefinition;
            this.lblTextureFiltering.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.lblTextureFiltering.Size = new System.Drawing.Size(136, 19);
            this.lblTextureFiltering.TabIndex = 5;
            this.lblTextureFiltering.Values.Text = "Texture Filtering Mode:";
            // 
            // lblViewZoom
            // 
            this.lblViewZoom.Location = new System.Drawing.Point(3, 93);
            this.lblViewZoom.Name = "lblViewZoom";
            this.lblViewZoom.Palette = this.AppThemeDefinition;
            this.lblViewZoom.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.lblViewZoom.Size = new System.Drawing.Size(73, 19);
            this.lblViewZoom.TabIndex = 15;
            this.lblViewZoom.Values.Text = "View Zoom: ";
            // 
            // lblSelectSprite
            // 
            this.lblSelectSprite.Location = new System.Drawing.Point(3, 63);
            this.lblSelectSprite.Name = "lblSelectSprite";
            this.lblSelectSprite.Palette = this.AppThemeDefinition;
            this.lblSelectSprite.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.lblSelectSprite.Size = new System.Drawing.Size(80, 19);
            this.lblSelectSprite.TabIndex = 13;
            this.lblSelectSprite.Values.Text = "Select Sprite:";
            // 
            // lblMarkupSprite
            // 
            this.lblMarkupSprite.Location = new System.Drawing.Point(3, 33);
            this.lblMarkupSprite.Name = "lblMarkupSprite";
            this.lblMarkupSprite.Palette = this.AppThemeDefinition;
            this.lblMarkupSprite.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.lblMarkupSprite.Size = new System.Drawing.Size(90, 19);
            this.lblMarkupSprite.TabIndex = 11;
            this.lblMarkupSprite.Values.Text = "Markup Sprite:";
            // 
            // lblMoveCam
            // 
            this.lblMoveCam.Location = new System.Drawing.Point(3, 3);
            this.lblMoveCam.Name = "lblMoveCam";
            this.lblMoveCam.Palette = this.AppThemeDefinition;
            this.lblMoveCam.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.lblMoveCam.Size = new System.Drawing.Size(87, 19);
            this.lblMoveCam.TabIndex = 9;
            this.lblMoveCam.Values.Text = "Move Camera:";
            // 
            // cmbTextureFilterMode
            // 
            this.cmbTextureFilterMode.DropDownWidth = 109;
            this.cmbTextureFilterMode.Items.AddRange(new object[] {
            "Point",
            "Linear"});
            this.cmbTextureFilterMode.Location = new System.Drawing.Point(152, 44);
            this.cmbTextureFilterMode.MaxDropDownItems = 2;
            this.cmbTextureFilterMode.Name = "cmbTextureFilterMode";
            this.cmbTextureFilterMode.Palette = this.AppThemeDefinition;
            this.cmbTextureFilterMode.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.cmbTextureFilterMode.Size = new System.Drawing.Size(256, 32);
            this.cmbTextureFilterMode.TabIndex = 7;
            this.cmbTextureFilterMode.Text = "kryptonComboBox1";
            this.cmbTextureFilterMode.SelectedIndexChanged += new System.EventHandler(this.CmbTextureFilterModeSelectedIndexChanged);
            // 
            // lblBgColor
            // 
            this.lblBgColor.Location = new System.Drawing.Point(3, 3);
            this.lblBgColor.Name = "lblBgColor";
            this.lblBgColor.Size = new System.Drawing.Size(70, 19);
            this.lblBgColor.TabIndex = 20;
            this.lblBgColor.Values.Text = "Background";
            // 
            // lblFrameRectColor
            // 
            this.lblFrameRectColor.Location = new System.Drawing.Point(3, 33);
            this.lblFrameRectColor.Name = "lblFrameRectColor";
            this.lblFrameRectColor.Size = new System.Drawing.Size(103, 19);
            this.lblFrameRectColor.TabIndex = 21;
            this.lblFrameRectColor.Values.Text = "FrameRect Normal";
            // 
            // lblFrameRectHoveredColor
            // 
            this.lblFrameRectHoveredColor.Location = new System.Drawing.Point(3, 63);
            this.lblFrameRectHoveredColor.Name = "lblFrameRectHoveredColor";
            this.lblFrameRectHoveredColor.Size = new System.Drawing.Size(109, 19);
            this.lblFrameRectHoveredColor.TabIndex = 22;
            this.lblFrameRectHoveredColor.Values.Text = "FrameRect Hovered";
            // 
            // lblFrameRectSelColor
            // 
            this.lblFrameRectSelColor.Location = new System.Drawing.Point(3, 93);
            this.lblFrameRectSelColor.Name = "lblFrameRectSelColor";
            this.lblFrameRectSelColor.Size = new System.Drawing.Size(108, 19);
            this.lblFrameRectSelColor.TabIndex = 23;
            this.lblFrameRectSelColor.Values.Text = "FrameRect Selected";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabGeneral);
            this.tabControl1.Controls.Add(this.tabInput);
            this.tabControl1.Controls.Add(this.tabStyle);
            this.tabControl1.Controls.Add(this.tabExporting);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(478, 475);
            this.tabControl1.TabIndex = 8;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.generalTabTable);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(470, 449);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // generalTabTable
            // 
            this.generalTabTable.ColumnCount = 2;
            this.generalTabTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.28699F));
            this.generalTabTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.71301F));
            this.generalTabTable.Controls.Add(this.lblCamSpeed, 0, 0);
            this.generalTabTable.Controls.Add(this.lblTextureFiltering, 0, 1);
            this.generalTabTable.Controls.Add(this.txtCamSpeed, 1, 0);
            this.generalTabTable.Controls.Add(this.cmbTextureFilterMode, 1, 1);
            this.generalTabTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.generalTabTable.Location = new System.Drawing.Point(3, 3);
            this.generalTabTable.Name = "generalTabTable";
            this.generalTabTable.RowCount = 2;
            this.generalTabTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.345795F));
            this.generalTabTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.65421F));
            this.generalTabTable.Size = new System.Drawing.Size(464, 443);
            this.generalTabTable.TabIndex = 6;
            // 
            // tabInput
            // 
            this.tabInput.Controls.Add(this.tabInputTable);
            this.tabInput.Location = new System.Drawing.Point(4, 22);
            this.tabInput.Name = "tabInput";
            this.tabInput.Padding = new System.Windows.Forms.Padding(3);
            this.tabInput.Size = new System.Drawing.Size(470, 449);
            this.tabInput.TabIndex = 1;
            this.tabInput.Text = "Input";
            this.tabInput.UseVisualStyleBackColor = true;
            // 
            // tabInputTable
            // 
            this.tabInputTable.ColumnCount = 2;
            this.tabInputTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tabInputTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tabInputTable.Controls.Add(this.InputDetectorViewZoom, 1, 3);
            this.tabInputTable.Controls.Add(this.lblViewZoom, 0, 3);
            this.tabInputTable.Controls.Add(this.InputDetectorSelectSprite, 1, 2);
            this.tabInputTable.Controls.Add(this.lblMoveCam, 0, 0);
            this.tabInputTable.Controls.Add(this.InputDetectorMarkupSprite, 1, 1);
            this.tabInputTable.Controls.Add(this.lblMarkupSprite, 0, 1);
            this.tabInputTable.Controls.Add(this.InputDetectorMoveCamera, 1, 0);
            this.tabInputTable.Controls.Add(this.lblSelectSprite, 0, 2);
            this.tabInputTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInputTable.Location = new System.Drawing.Point(3, 3);
            this.tabInputTable.Name = "tabInputTable";
            this.tabInputTable.RowCount = 5;
            this.tabInputTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tabInputTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tabInputTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tabInputTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tabInputTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tabInputTable.Size = new System.Drawing.Size(464, 443);
            this.tabInputTable.TabIndex = 0;
            // 
            // InputDetectorViewZoom
            // 
            this.InputDetectorViewZoom.AssignedButton = System.Windows.Forms.MouseButtons.None;
            this.InputDetectorViewZoom.AssignedKey = System.Windows.Forms.Keys.None;
            this.InputDetectorViewZoom.ControlLabel = "";
            this.InputDetectorViewZoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InputDetectorViewZoom.Location = new System.Drawing.Point(235, 93);
            this.InputDetectorViewZoom.Name = "InputDetectorViewZoom";
            this.InputDetectorViewZoom.Size = new System.Drawing.Size(226, 24);
            this.InputDetectorViewZoom.TabIndex = 16;
            this.InputDetectorViewZoom.ControlsChanged += new SpriteVortex.InputControl2.ControlsChangedHandler(this.InputDetectorViewZoom_ControlsChanged);
            // 
            // InputDetectorSelectSprite
            // 
            this.InputDetectorSelectSprite.AssignedButton = System.Windows.Forms.MouseButtons.None;
            this.InputDetectorSelectSprite.AssignedKey = System.Windows.Forms.Keys.None;
            this.InputDetectorSelectSprite.ControlLabel = "";
            this.InputDetectorSelectSprite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InputDetectorSelectSprite.Location = new System.Drawing.Point(235, 63);
            this.InputDetectorSelectSprite.Name = "InputDetectorSelectSprite";
            this.InputDetectorSelectSprite.Size = new System.Drawing.Size(226, 24);
            this.InputDetectorSelectSprite.TabIndex = 14;
            this.InputDetectorSelectSprite.ControlsChanged += new SpriteVortex.InputControl2.ControlsChangedHandler(this.InputDetectorSelectSprite_ControlsChanged);
            // 
            // InputDetectorMarkupSprite
            // 
            this.InputDetectorMarkupSprite.AssignedButton = System.Windows.Forms.MouseButtons.None;
            this.InputDetectorMarkupSprite.AssignedKey = System.Windows.Forms.Keys.None;
            this.InputDetectorMarkupSprite.ControlLabel = "";
            this.InputDetectorMarkupSprite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InputDetectorMarkupSprite.Location = new System.Drawing.Point(235, 33);
            this.InputDetectorMarkupSprite.Name = "InputDetectorMarkupSprite";
            this.InputDetectorMarkupSprite.Size = new System.Drawing.Size(226, 24);
            this.InputDetectorMarkupSprite.TabIndex = 12;
            this.InputDetectorMarkupSprite.ControlsChanged += new SpriteVortex.InputControl2.ControlsChangedHandler(this.InputDetectorMarkupSprite_ControlsChanged);
            // 
            // InputDetectorMoveCamera
            // 
            this.InputDetectorMoveCamera.AssignedButton = System.Windows.Forms.MouseButtons.None;
            this.InputDetectorMoveCamera.AssignedKey = System.Windows.Forms.Keys.None;
            this.InputDetectorMoveCamera.ControlLabel = "";
            this.InputDetectorMoveCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InputDetectorMoveCamera.Location = new System.Drawing.Point(235, 3);
            this.InputDetectorMoveCamera.Name = "InputDetectorMoveCamera";
            this.InputDetectorMoveCamera.Size = new System.Drawing.Size(226, 24);
            this.InputDetectorMoveCamera.TabIndex = 10;
            this.InputDetectorMoveCamera.ControlsChanged += new SpriteVortex.InputControl2.ControlsChangedHandler(this.InputDetectorMoveCamera_ControlsChanged);
            // 
            // tabStyle
            // 
            this.tabStyle.Controls.Add(this.tabStyleTable);
            this.tabStyle.Location = new System.Drawing.Point(4, 22);
            this.tabStyle.Name = "tabStyle";
            this.tabStyle.Padding = new System.Windows.Forms.Padding(3);
            this.tabStyle.Size = new System.Drawing.Size(470, 449);
            this.tabStyle.TabIndex = 2;
            this.tabStyle.Text = "Style";
            this.tabStyle.UseVisualStyleBackColor = true;
            // 
            // tabStyleTable
            // 
            this.tabStyleTable.ColumnCount = 2;
            this.tabStyleTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tabStyleTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tabStyleTable.Controls.Add(this.lblBgColor, 0, 0);
            this.tabStyleTable.Controls.Add(this.lblFrameRectColor, 0, 1);
            this.tabStyleTable.Controls.Add(this.lblFrameRectHoveredColor, 0, 2);
            this.tabStyleTable.Controls.Add(this.ColorPickerFrameRectSelected, 1, 3);
            this.tabStyleTable.Controls.Add(this.ColorPickerFrameRectHovered, 1, 2);
            this.tabStyleTable.Controls.Add(this.ColorPickerFrameRect, 1, 1);
            this.tabStyleTable.Controls.Add(this.ColorPickerBg, 1, 0);
            this.tabStyleTable.Controls.Add(this.lblFrameRectSelColor, 0, 3);
            this.tabStyleTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabStyleTable.Location = new System.Drawing.Point(3, 3);
            this.tabStyleTable.Name = "tabStyleTable";
            this.tabStyleTable.RowCount = 5;
            this.tabStyleTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tabStyleTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tabStyleTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tabStyleTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tabStyleTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tabStyleTable.Size = new System.Drawing.Size(464, 443);
            this.tabStyleTable.TabIndex = 0;
            // 
            // ColorPickerFrameRectSelected
            // 
            this.ColorPickerFrameRectSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ColorPickerFrameRectSelected.Location = new System.Drawing.Point(235, 93);
            this.ColorPickerFrameRectSelected.Name = "ColorPickerFrameRectSelected";
            this.ColorPickerFrameRectSelected.SelectedColor = System.Drawing.Color.Empty;
            this.ColorPickerFrameRectSelected.Size = new System.Drawing.Size(226, 24);
            this.ColorPickerFrameRectSelected.TabIndex = 27;
            this.ColorPickerFrameRectSelected.ColorChanged += new SpriteVortex.ColorPicker.ColorChangedHandler(this.ColorPickerFrameRectSelected_ColorChanged);
            // 
            // ColorPickerFrameRectHovered
            // 
            this.ColorPickerFrameRectHovered.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ColorPickerFrameRectHovered.Location = new System.Drawing.Point(235, 63);
            this.ColorPickerFrameRectHovered.Name = "ColorPickerFrameRectHovered";
            this.ColorPickerFrameRectHovered.SelectedColor = System.Drawing.Color.Empty;
            this.ColorPickerFrameRectHovered.Size = new System.Drawing.Size(226, 24);
            this.ColorPickerFrameRectHovered.TabIndex = 26;
            this.ColorPickerFrameRectHovered.ColorChanged += new SpriteVortex.ColorPicker.ColorChangedHandler(this.ColorPickerFrameRectHovered_ColorChanged);
            // 
            // ColorPickerFrameRect
            // 
            this.ColorPickerFrameRect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ColorPickerFrameRect.Location = new System.Drawing.Point(235, 33);
            this.ColorPickerFrameRect.Name = "ColorPickerFrameRect";
            this.ColorPickerFrameRect.SelectedColor = System.Drawing.Color.Empty;
            this.ColorPickerFrameRect.Size = new System.Drawing.Size(226, 24);
            this.ColorPickerFrameRect.TabIndex = 25;
            this.ColorPickerFrameRect.ColorChanged += new SpriteVortex.ColorPicker.ColorChangedHandler(this.ColorPickerFrameRect_ColorChanged);
            // 
            // ColorPickerBg
            // 
            this.ColorPickerBg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ColorPickerBg.Location = new System.Drawing.Point(235, 3);
            this.ColorPickerBg.Name = "ColorPickerBg";
            this.ColorPickerBg.SelectedColor = System.Drawing.Color.Empty;
            this.ColorPickerBg.Size = new System.Drawing.Size(226, 24);
            this.ColorPickerBg.TabIndex = 24;
            this.ColorPickerBg.ColorChanged += new SpriteVortex.ColorPicker.ColorChangedHandler(this.ColorPickerBg_ColorChanged);
            // 
            // tabExporting
            // 
            this.tabExporting.Controls.Add(this.tabExportingTable);
            this.tabExporting.Location = new System.Drawing.Point(4, 22);
            this.tabExporting.Name = "tabExporting";
            this.tabExporting.Size = new System.Drawing.Size(470, 449);
            this.tabExporting.TabIndex = 3;
            this.tabExporting.Text = "Exporting";
            this.tabExporting.UseVisualStyleBackColor = true;
            // 
            // tabExportingTable
            // 
            this.tabExportingTable.ColumnCount = 3;
            this.tabExportingTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.06383F));
            this.tabExportingTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tabExportingTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 194F));
            this.tabExportingTable.Controls.Add(this.lblImageExportOpt, 0, 0);
            this.tabExportingTable.Controls.Add(this.lblColorClearingMode, 0, 1);
            this.tabExportingTable.Controls.Add(this.radColorKeyMode, 1, 1);
            this.tabExportingTable.Controls.Add(this.radAlphaMode, 2, 1);
            this.tabExportingTable.Controls.Add(this.lblMappingExportOpt, 0, 2);
            this.tabExportingTable.Controls.Add(this.chkPackSpriteSheet, 0, 3);
            this.tabExportingTable.Controls.Add(this.lblPackingOpt, 0, 4);
            this.tabExportingTable.Controls.Add(this.chkForceSquare, 0, 6);
            this.tabExportingTable.Controls.Add(this.chkForcePowTwo, 0, 5);
            this.tabExportingTable.Controls.Add(this.lblPadding, 0, 7);
            this.tabExportingTable.Controls.Add(this.udPadding, 1, 7);
            this.tabExportingTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabExportingTable.Location = new System.Drawing.Point(0, 0);
            this.tabExportingTable.Name = "tabExportingTable";
            this.tabExportingTable.RowCount = 9;
            this.tabExportingTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.21569F));
            this.tabExportingTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.78431F));
            this.tabExportingTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tabExportingTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tabExportingTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tabExportingTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tabExportingTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tabExportingTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tabExportingTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 177F));
            this.tabExportingTable.Size = new System.Drawing.Size(470, 449);
            this.tabExportingTable.TabIndex = 0;
            // 
            // lblImageExportOpt
            // 
            this.tabExportingTable.SetColumnSpan(this.lblImageExportOpt, 2);
            this.lblImageExportOpt.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitlePanel;
            this.lblImageExportOpt.Location = new System.Drawing.Point(3, 3);
            this.lblImageExportOpt.Name = "lblImageExportOpt";
            this.lblImageExportOpt.Palette = this.AppThemeDefinition;
            this.lblImageExportOpt.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.lblImageExportOpt.Size = new System.Drawing.Size(145, 24);
            this.lblImageExportOpt.TabIndex = 1;
            this.lblImageExportOpt.Values.Text = "SpriteSheet Image";
            // 
            // lblColorClearingMode
            // 
            this.lblColorClearingMode.Location = new System.Drawing.Point(3, 34);
            this.lblColorClearingMode.Name = "lblColorClearingMode";
            this.lblColorClearingMode.Palette = this.AppThemeDefinition;
            this.lblColorClearingMode.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.lblColorClearingMode.Size = new System.Drawing.Size(123, 19);
            this.lblColorClearingMode.TabIndex = 6;
            this.lblColorClearingMode.Values.Text = "Color Clearing Mode: ";
            // 
            // radColorKeyMode
            // 
            this.radColorKeyMode.Location = new System.Drawing.Point(196, 34);
            this.radColorKeyMode.Name = "radColorKeyMode";
            this.radColorKeyMode.Size = new System.Drawing.Size(70, 19);
            this.radColorKeyMode.TabIndex = 7;
            this.radColorKeyMode.Values.Text = "Color Key";
            // 
            // radAlphaMode
            // 
            this.radAlphaMode.Location = new System.Drawing.Point(279, 34);
            this.radAlphaMode.Name = "radAlphaMode";
            this.radAlphaMode.Size = new System.Drawing.Size(51, 19);
            this.radAlphaMode.TabIndex = 8;
            this.radAlphaMode.Values.Text = "Alpha";
            // 
            // lblMappingExportOpt
            // 
            this.lblMappingExportOpt.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitlePanel;
            this.lblMappingExportOpt.Location = new System.Drawing.Point(3, 82);
            this.lblMappingExportOpt.Name = "lblMappingExportOpt";
            this.lblMappingExportOpt.Palette = this.AppThemeDefinition;
            this.lblMappingExportOpt.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.lblMappingExportOpt.Size = new System.Drawing.Size(92, 24);
            this.lblMappingExportOpt.TabIndex = 11;
            this.lblMappingExportOpt.Values.Text = "Sprite Map";
            // 
            // chkPackSpriteSheet
            // 
            this.chkPackSpriteSheet.Location = new System.Drawing.Point(3, 115);
            this.chkPackSpriteSheet.Name = "chkPackSpriteSheet";
            this.chkPackSpriteSheet.Size = new System.Drawing.Size(186, 19);
            this.chkPackSpriteSheet.TabIndex = 12;
            this.chkPackSpriteSheet.Values.Text = "Pack Spritesheet when exporting";
            // 
            // lblPackingOpt
            // 
            this.lblPackingOpt.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitlePanel;
            this.lblPackingOpt.Location = new System.Drawing.Point(3, 147);
            this.lblPackingOpt.Name = "lblPackingOpt";
            this.lblPackingOpt.Palette = this.AppThemeDefinition;
            this.lblPackingOpt.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.lblPackingOpt.Size = new System.Drawing.Size(69, 24);
            this.lblPackingOpt.TabIndex = 3;
            this.lblPackingOpt.Values.Text = "Packing";
            // 
            // chkForceSquare
            // 
            this.chkForceSquare.Location = new System.Drawing.Point(3, 216);
            this.chkForceSquare.Name = "chkForceSquare";
            this.chkForceSquare.Size = new System.Drawing.Size(127, 19);
            this.chkForceSquare.TabIndex = 5;
            this.chkForceSquare.Values.Text = "Force Square Format";
            // 
            // chkForcePowTwo
            // 
            this.chkForcePowTwo.Location = new System.Drawing.Point(3, 183);
            this.chkForcePowTwo.Name = "chkForcePowTwo";
            this.chkForcePowTwo.Size = new System.Drawing.Size(122, 19);
            this.chkForcePowTwo.TabIndex = 4;
            this.chkForcePowTwo.Values.Text = "Force Power of Two";
            // 
            // lblPadding
            // 
            this.lblPadding.Location = new System.Drawing.Point(3, 248);
            this.lblPadding.Name = "lblPadding";
            this.lblPadding.Size = new System.Drawing.Size(54, 19);
            this.lblPadding.TabIndex = 9;
            this.lblPadding.Values.Text = "Padding:";
            // 
            // udPadding
            // 
            this.udPadding.Location = new System.Drawing.Point(196, 248);
            this.udPadding.Name = "udPadding";
            this.udPadding.Size = new System.Drawing.Size(77, 20);
            this.udPadding.TabIndex = 10;
            // 
            // ConfigurationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(230)))), ((int)(((byte)(232)))));
            this.ClientSize = new System.Drawing.Size(478, 511);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.kryptonPanel1);
            this.MinimumSize = new System.Drawing.Size(494, 546);
            this.Name = "ConfigurationWindow";
            this.Palette = this.AppThemeDefinition;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StateCommon.Header.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.StateCommon.Header.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.StateCommon.Header.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text = "Options";
            this.Load += new System.EventHandler(this.ConfigurationWindowLoad);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigurationWindow_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbTextureFilterMode)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.generalTabTable.ResumeLayout(false);
            this.generalTabTable.PerformLayout();
            this.tabInput.ResumeLayout(false);
            this.tabInputTable.ResumeLayout(false);
            this.tabInputTable.PerformLayout();
            this.tabStyle.ResumeLayout(false);
            this.tabStyleTable.ResumeLayout(false);
            this.tabStyleTable.PerformLayout();
            this.tabExporting.ResumeLayout(false);
            this.tabExportingTable.ResumeLayout(false);
            this.tabExportingTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPalette AppThemeDefinition;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblCamSpeed;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtCamSpeed;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnConfirmConfigs;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblTextureFiltering;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cmbTextureFilterMode;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblMoveCam;
        private InputControl2 InputDetectorMoveCamera;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblMarkupSprite;
        private InputControl2 InputDetectorMarkupSprite;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblSelectSprite;
        private InputControl2 InputDetectorSelectSprite;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblViewZoom;
        private InputControl2 InputDetectorViewZoom;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblBgColor;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblFrameRectColor;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblFrameRectHoveredColor;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblFrameRectSelColor;
        private ColorPicker ColorPickerBg;
        private ColorPicker ColorPickerFrameRect;
        private ColorPicker ColorPickerFrameRectHovered;
        private ColorPicker ColorPickerFrameRectSelected;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabInput;
        private System.Windows.Forms.TabPage tabStyle;
        private System.Windows.Forms.TabPage tabExporting;
        private System.Windows.Forms.TableLayoutPanel generalTabTable;
        private System.Windows.Forms.TableLayoutPanel tabInputTable;
        private System.Windows.Forms.TableLayoutPanel tabStyleTable;
        private System.Windows.Forms.TableLayoutPanel tabExportingTable;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblImageExportOpt;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblPackingOpt;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkForcePowTwo;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkForceSquare;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblColorClearingMode;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radColorKeyMode;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radAlphaMode;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblPadding;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown udPadding;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblMappingExportOpt;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkPackSpriteSheet;
    }
}