namespace EPQ_Raytrace_Engine
{
    partial class FormMain
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
            this.SceneTreeView = new System.Windows.Forms.TreeView();
            this.SceneTreeNodeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteNodeItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCornellBoxSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newRaytracingInOneWeekendBookCoverSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sphereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.planeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lambertianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dielectricToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diffuseLightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.constantTextureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkerTextureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.perlinNoiseTextureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turbulentNoiseTextureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.marbleNoiseTextureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageTextureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startStopPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.startStopFullResRenderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveRenderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SceneTreeGroupBox = new System.Windows.Forms.GroupBox();
            this.PropertiesGroupBox = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RenderPreviewGroupBox = new System.Windows.Forms.GroupBox();
            this.RenderPreviewPictureBox = new System.Windows.Forms.PictureBox();
            this.RenderPropertiesGroupBox = new System.Windows.Forms.GroupBox();
            this.RenderSamplesGroupBox = new System.Windows.Forms.GroupBox();
            this.RenderSamplesPanel = new System.Windows.Forms.Panel();
            this.RenderSamplesCheckBox = new System.Windows.Forms.CheckBox();
            this.RenderSamplesValueLabel = new System.Windows.Forms.Label();
            this.RenderSamplesValueTextBox = new System.Windows.Forms.TextBox();
            this.RenderResolutionGroupBox = new System.Windows.Forms.GroupBox();
            this.RenderResolutionPanel = new System.Windows.Forms.Panel();
            this.RenderResolutionHeightLabel = new System.Windows.Forms.Label();
            this.RenderResolutionHeightTextBox = new System.Windows.Forms.TextBox();
            this.RenderResolutionWidthLabel = new System.Windows.Forms.Label();
            this.RenderResolutionWidthTextBox = new System.Windows.Forms.TextBox();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.MainToolStripCurrentTaskLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainToolStripTaskProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.MainToolStripTaskLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.SceneTreeNodeContextMenuStrip.SuspendLayout();
            this.MainMenuStrip.SuspendLayout();
            this.SceneTreeGroupBox.SuspendLayout();
            this.PropertiesGroupBox.SuspendLayout();
            this.RenderPreviewGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RenderPreviewPictureBox)).BeginInit();
            this.RenderPropertiesGroupBox.SuspendLayout();
            this.RenderSamplesGroupBox.SuspendLayout();
            this.RenderSamplesPanel.SuspendLayout();
            this.RenderResolutionGroupBox.SuspendLayout();
            this.RenderResolutionPanel.SuspendLayout();
            this.MainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // SceneTreeView
            // 
            this.SceneTreeView.HotTracking = true;
            this.SceneTreeView.Location = new System.Drawing.Point(6, 19);
            this.SceneTreeView.Name = "SceneTreeView";
            this.SceneTreeView.Size = new System.Drawing.Size(188, 397);
            this.SceneTreeView.TabIndex = 0;
            this.SceneTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.SceneTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SceneTreeView_MouseDown);
            // 
            // SceneTreeNodeContextMenuStrip
            // 
            this.SceneTreeNodeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteNodeItem});
            this.SceneTreeNodeContextMenuStrip.Name = "contextMenuStrip1";
            this.SceneTreeNodeContextMenuStrip.ShowImageMargin = false;
            this.SceneTreeNodeContextMenuStrip.ShowItemToolTips = false;
            this.SceneTreeNodeContextMenuStrip.Size = new System.Drawing.Size(129, 26);
            // 
            // DeleteNodeItem
            // 
            this.DeleteNodeItem.ForeColor = System.Drawing.Color.Red;
            this.DeleteNodeItem.Name = "DeleteNodeItem";
            this.DeleteNodeItem.Size = new System.Drawing.Size(128, 22);
            this.DeleteNodeItem.Text = "Delete Element";
            this.DeleteNodeItem.Click += new System.EventHandler(this.SceneTreeNodeContextDeleteNode);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSceneToolStripMenuItem,
            this.newCornellBoxSceneToolStripMenuItem,
            this.newRaytracingInOneWeekendBookCoverSceneToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.testToolStripMenuItem.Text = "File";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // newSceneToolStripMenuItem
            // 
            this.newSceneToolStripMenuItem.Name = "newSceneToolStripMenuItem";
            this.newSceneToolStripMenuItem.Size = new System.Drawing.Size(345, 22);
            this.newSceneToolStripMenuItem.Text = "New Empty Scene";
            this.newSceneToolStripMenuItem.Click += new System.EventHandler(this.newSceneToolStripMenuItem_Click);
            // 
            // newCornellBoxSceneToolStripMenuItem
            // 
            this.newCornellBoxSceneToolStripMenuItem.Name = "newCornellBoxSceneToolStripMenuItem";
            this.newCornellBoxSceneToolStripMenuItem.Size = new System.Drawing.Size(345, 22);
            this.newCornellBoxSceneToolStripMenuItem.Text = "New Cornell Box (Simplified) Scene";
            this.newCornellBoxSceneToolStripMenuItem.Click += new System.EventHandler(this.newCornellBoxSceneToolStripMenuItem_Click);
            // 
            // newRaytracingInOneWeekendBookCoverSceneToolStripMenuItem
            // 
            this.newRaytracingInOneWeekendBookCoverSceneToolStripMenuItem.Name = "newRaytracingInOneWeekendBookCoverSceneToolStripMenuItem";
            this.newRaytracingInOneWeekendBookCoverSceneToolStripMenuItem.Size = new System.Drawing.Size(345, 22);
            this.newRaytracingInOneWeekendBookCoverSceneToolStripMenuItem.Text = "New Raytracing In One Weekend Book Cover Scene";
            this.newRaytracingInOneWeekendBookCoverSceneToolStripMenuItem.Click += new System.EventHandler(this.newRaytracingInOneWeekendBookCoverSceneToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(345, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.addToolStripMenuItem,
            this.renderToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(931, 24);
            this.MainMenuStrip.TabIndex = 1;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.materialToolStripMenuItem,
            this.textureToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sphereToolStripMenuItem,
            this.planeToolStripMenuItem,
            this.boxToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItem1.Text = "Object";
            // 
            // sphereToolStripMenuItem
            // 
            this.sphereToolStripMenuItem.Name = "sphereToolStripMenuItem";
            this.sphereToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.sphereToolStripMenuItem.Text = "Sphere";
            this.sphereToolStripMenuItem.Click += new System.EventHandler(this.sphereToolStripMenuItem_Click);
            // 
            // planeToolStripMenuItem
            // 
            this.planeToolStripMenuItem.Name = "planeToolStripMenuItem";
            this.planeToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.planeToolStripMenuItem.Text = "Plane";
            this.planeToolStripMenuItem.Click += new System.EventHandler(this.planeToolStripMenuItem_Click);
            // 
            // boxToolStripMenuItem
            // 
            this.boxToolStripMenuItem.Name = "boxToolStripMenuItem";
            this.boxToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.boxToolStripMenuItem.Text = "Box";
            this.boxToolStripMenuItem.Click += new System.EventHandler(this.boxToolStripMenuItem_Click);
            // 
            // materialToolStripMenuItem
            // 
            this.materialToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lambertianToolStripMenuItem,
            this.metalToolStripMenuItem,
            this.dielectricToolStripMenuItem,
            this.diffuseLightToolStripMenuItem});
            this.materialToolStripMenuItem.Name = "materialToolStripMenuItem";
            this.materialToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.materialToolStripMenuItem.Text = "Material";
            // 
            // lambertianToolStripMenuItem
            // 
            this.lambertianToolStripMenuItem.Name = "lambertianToolStripMenuItem";
            this.lambertianToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.lambertianToolStripMenuItem.Text = "Lambertian";
            this.lambertianToolStripMenuItem.Click += new System.EventHandler(this.lambertianToolStripMenuItem_Click);
            // 
            // metalToolStripMenuItem
            // 
            this.metalToolStripMenuItem.Name = "metalToolStripMenuItem";
            this.metalToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.metalToolStripMenuItem.Text = "Metal";
            this.metalToolStripMenuItem.Click += new System.EventHandler(this.metalToolStripMenuItem_Click);
            // 
            // dielectricToolStripMenuItem
            // 
            this.dielectricToolStripMenuItem.Name = "dielectricToolStripMenuItem";
            this.dielectricToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.dielectricToolStripMenuItem.Text = "Dielectric";
            this.dielectricToolStripMenuItem.Click += new System.EventHandler(this.dielectricToolStripMenuItem_Click);
            // 
            // diffuseLightToolStripMenuItem
            // 
            this.diffuseLightToolStripMenuItem.Name = "diffuseLightToolStripMenuItem";
            this.diffuseLightToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.diffuseLightToolStripMenuItem.Text = "DiffuseLight";
            this.diffuseLightToolStripMenuItem.Click += new System.EventHandler(this.diffuseLightToolStripMenuItem_Click);
            // 
            // textureToolStripMenuItem
            // 
            this.textureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.constantTextureToolStripMenuItem,
            this.checkerTextureToolStripMenuItem,
            this.perlinNoiseTextureToolStripMenuItem,
            this.turbulentNoiseTextureToolStripMenuItem,
            this.marbleNoiseTextureToolStripMenuItem,
            this.imageTextureToolStripMenuItem});
            this.textureToolStripMenuItem.Name = "textureToolStripMenuItem";
            this.textureToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.textureToolStripMenuItem.Text = "Texture";
            // 
            // constantTextureToolStripMenuItem
            // 
            this.constantTextureToolStripMenuItem.Name = "constantTextureToolStripMenuItem";
            this.constantTextureToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.constantTextureToolStripMenuItem.Text = "Constant Texture";
            this.constantTextureToolStripMenuItem.Click += new System.EventHandler(this.constantTextureToolStripMenuItem_Click);
            // 
            // checkerTextureToolStripMenuItem
            // 
            this.checkerTextureToolStripMenuItem.Name = "checkerTextureToolStripMenuItem";
            this.checkerTextureToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.checkerTextureToolStripMenuItem.Text = "Checker Texture";
            this.checkerTextureToolStripMenuItem.Click += new System.EventHandler(this.checkerTextureToolStripMenuItem_Click);
            // 
            // perlinNoiseTextureToolStripMenuItem
            // 
            this.perlinNoiseTextureToolStripMenuItem.Name = "perlinNoiseTextureToolStripMenuItem";
            this.perlinNoiseTextureToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.perlinNoiseTextureToolStripMenuItem.Text = "Perlin Noise Texture";
            this.perlinNoiseTextureToolStripMenuItem.Click += new System.EventHandler(this.perlinNoiseTextureToolStripMenuItem_Click);
            // 
            // turbulentNoiseTextureToolStripMenuItem
            // 
            this.turbulentNoiseTextureToolStripMenuItem.Name = "turbulentNoiseTextureToolStripMenuItem";
            this.turbulentNoiseTextureToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.turbulentNoiseTextureToolStripMenuItem.Text = "Turbulent Noise Texture";
            this.turbulentNoiseTextureToolStripMenuItem.Click += new System.EventHandler(this.turbulentNoiseTextureToolStripMenuItem_Click);
            // 
            // marbleNoiseTextureToolStripMenuItem
            // 
            this.marbleNoiseTextureToolStripMenuItem.Name = "marbleNoiseTextureToolStripMenuItem";
            this.marbleNoiseTextureToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.marbleNoiseTextureToolStripMenuItem.Text = "Marble Noise Texture";
            this.marbleNoiseTextureToolStripMenuItem.Click += new System.EventHandler(this.marbleNoiseTextureToolStripMenuItem_Click);
            // 
            // imageTextureToolStripMenuItem
            // 
            this.imageTextureToolStripMenuItem.Name = "imageTextureToolStripMenuItem";
            this.imageTextureToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.imageTextureToolStripMenuItem.Text = "Image Texture";
            this.imageTextureToolStripMenuItem.Click += new System.EventHandler(this.imageTextureToolStripMenuItem_Click);
            // 
            // renderToolStripMenuItem
            // 
            this.renderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startStopPreviewToolStripMenuItem,
            this.toolStripSeparator1,
            this.startStopFullResRenderToolStripMenuItem,
            this.saveRenderToolStripMenuItem});
            this.renderToolStripMenuItem.Name = "renderToolStripMenuItem";
            this.renderToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.renderToolStripMenuItem.Text = "Render";
            // 
            // startStopPreviewToolStripMenuItem
            // 
            this.startStopPreviewToolStripMenuItem.Name = "startStopPreviewToolStripMenuItem";
            this.startStopPreviewToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.startStopPreviewToolStripMenuItem.Text = "Start Render Preview";
            this.startStopPreviewToolStripMenuItem.Click += new System.EventHandler(this.startStopPreviewToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(179, 6);
            // 
            // startStopFullResRenderToolStripMenuItem
            // 
            this.startStopFullResRenderToolStripMenuItem.Name = "startStopFullResRenderToolStripMenuItem";
            this.startStopFullResRenderToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.startStopFullResRenderToolStripMenuItem.Text = "Start Render";
            this.startStopFullResRenderToolStripMenuItem.Click += new System.EventHandler(this.startStopFullResRenderToolStripMenuItem_Click);
            // 
            // saveRenderToolStripMenuItem
            // 
            this.saveRenderToolStripMenuItem.Name = "saveRenderToolStripMenuItem";
            this.saveRenderToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.saveRenderToolStripMenuItem.Text = "Save Render";
            // 
            // SceneTreeGroupBox
            // 
            this.SceneTreeGroupBox.Controls.Add(this.SceneTreeView);
            this.SceneTreeGroupBox.Location = new System.Drawing.Point(12, 27);
            this.SceneTreeGroupBox.Name = "SceneTreeGroupBox";
            this.SceneTreeGroupBox.Size = new System.Drawing.Size(200, 422);
            this.SceneTreeGroupBox.TabIndex = 3;
            this.SceneTreeGroupBox.TabStop = false;
            this.SceneTreeGroupBox.Text = "Scene Hierarchy";
            // 
            // PropertiesGroupBox
            // 
            this.PropertiesGroupBox.Controls.Add(this.panel1);
            this.PropertiesGroupBox.Location = new System.Drawing.Point(218, 27);
            this.PropertiesGroupBox.Name = "PropertiesGroupBox";
            this.PropertiesGroupBox.Size = new System.Drawing.Size(286, 273);
            this.PropertiesGroupBox.TabIndex = 4;
            this.PropertiesGroupBox.TabStop = false;
            this.PropertiesGroupBox.Text = "Properties";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(6, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 249);
            this.panel1.TabIndex = 0;
            // 
            // RenderPreviewGroupBox
            // 
            this.RenderPreviewGroupBox.Controls.Add(this.RenderPreviewPictureBox);
            this.RenderPreviewGroupBox.Location = new System.Drawing.Point(510, 27);
            this.RenderPreviewGroupBox.Name = "RenderPreviewGroupBox";
            this.RenderPreviewGroupBox.Size = new System.Drawing.Size(409, 422);
            this.RenderPreviewGroupBox.TabIndex = 5;
            this.RenderPreviewGroupBox.TabStop = false;
            this.RenderPreviewGroupBox.Text = "Render Preview";
            // 
            // RenderPreviewPictureBox
            // 
            this.RenderPreviewPictureBox.Location = new System.Drawing.Point(6, 18);
            this.RenderPreviewPictureBox.Name = "RenderPreviewPictureBox";
            this.RenderPreviewPictureBox.Size = new System.Drawing.Size(397, 397);
            this.RenderPreviewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.RenderPreviewPictureBox.TabIndex = 0;
            this.RenderPreviewPictureBox.TabStop = false;
            // 
            // RenderPropertiesGroupBox
            // 
            this.RenderPropertiesGroupBox.Controls.Add(this.RenderSamplesGroupBox);
            this.RenderPropertiesGroupBox.Controls.Add(this.RenderResolutionGroupBox);
            this.RenderPropertiesGroupBox.Location = new System.Drawing.Point(218, 306);
            this.RenderPropertiesGroupBox.Name = "RenderPropertiesGroupBox";
            this.RenderPropertiesGroupBox.Size = new System.Drawing.Size(286, 143);
            this.RenderPropertiesGroupBox.TabIndex = 6;
            this.RenderPropertiesGroupBox.TabStop = false;
            this.RenderPropertiesGroupBox.Text = "Render Properties";
            // 
            // RenderSamplesGroupBox
            // 
            this.RenderSamplesGroupBox.Controls.Add(this.RenderSamplesPanel);
            this.RenderSamplesGroupBox.Location = new System.Drawing.Point(6, 81);
            this.RenderSamplesGroupBox.Name = "RenderSamplesGroupBox";
            this.RenderSamplesGroupBox.Size = new System.Drawing.Size(274, 55);
            this.RenderSamplesGroupBox.TabIndex = 1;
            this.RenderSamplesGroupBox.TabStop = false;
            this.RenderSamplesGroupBox.Text = "Samples";
            // 
            // RenderSamplesPanel
            // 
            this.RenderSamplesPanel.Controls.Add(this.RenderSamplesCheckBox);
            this.RenderSamplesPanel.Controls.Add(this.RenderSamplesValueLabel);
            this.RenderSamplesPanel.Controls.Add(this.RenderSamplesValueTextBox);
            this.RenderSamplesPanel.Location = new System.Drawing.Point(6, 19);
            this.RenderSamplesPanel.Name = "RenderSamplesPanel";
            this.RenderSamplesPanel.Size = new System.Drawing.Size(262, 28);
            this.RenderSamplesPanel.TabIndex = 0;
            // 
            // RenderSamplesCheckBox
            // 
            this.RenderSamplesCheckBox.AutoSize = true;
            this.RenderSamplesCheckBox.Location = new System.Drawing.Point(130, 6);
            this.RenderSamplesCheckBox.Name = "RenderSamplesCheckBox";
            this.RenderSamplesCheckBox.Size = new System.Drawing.Size(127, 17);
            this.RenderSamplesCheckBox.TabIndex = 4;
            this.RenderSamplesCheckBox.Text = "Progressive Sampling";
            this.RenderSamplesCheckBox.UseVisualStyleBackColor = true;
            // 
            // RenderSamplesValueLabel
            // 
            this.RenderSamplesValueLabel.AutoSize = true;
            this.RenderSamplesValueLabel.Location = new System.Drawing.Point(4, 7);
            this.RenderSamplesValueLabel.Name = "RenderSamplesValueLabel";
            this.RenderSamplesValueLabel.Size = new System.Drawing.Size(34, 13);
            this.RenderSamplesValueLabel.TabIndex = 3;
            this.RenderSamplesValueLabel.Text = "Value";
            // 
            // RenderSamplesValueTextBox
            // 
            this.RenderSamplesValueTextBox.Location = new System.Drawing.Point(42, 4);
            this.RenderSamplesValueTextBox.Name = "RenderSamplesValueTextBox";
            this.RenderSamplesValueTextBox.Size = new System.Drawing.Size(51, 20);
            this.RenderSamplesValueTextBox.TabIndex = 2;
            this.RenderSamplesValueTextBox.TextChanged += new System.EventHandler(this.RenderSamplesValueTextBox_TextChanged);
            // 
            // RenderResolutionGroupBox
            // 
            this.RenderResolutionGroupBox.Controls.Add(this.RenderResolutionPanel);
            this.RenderResolutionGroupBox.Location = new System.Drawing.Point(6, 20);
            this.RenderResolutionGroupBox.Name = "RenderResolutionGroupBox";
            this.RenderResolutionGroupBox.Size = new System.Drawing.Size(274, 55);
            this.RenderResolutionGroupBox.TabIndex = 0;
            this.RenderResolutionGroupBox.TabStop = false;
            this.RenderResolutionGroupBox.Text = "Resolution";
            // 
            // RenderResolutionPanel
            // 
            this.RenderResolutionPanel.Controls.Add(this.RenderResolutionHeightLabel);
            this.RenderResolutionPanel.Controls.Add(this.RenderResolutionHeightTextBox);
            this.RenderResolutionPanel.Controls.Add(this.RenderResolutionWidthLabel);
            this.RenderResolutionPanel.Controls.Add(this.RenderResolutionWidthTextBox);
            this.RenderResolutionPanel.Location = new System.Drawing.Point(6, 19);
            this.RenderResolutionPanel.Name = "RenderResolutionPanel";
            this.RenderResolutionPanel.Size = new System.Drawing.Size(262, 28);
            this.RenderResolutionPanel.TabIndex = 0;
            // 
            // RenderResolutionHeightLabel
            // 
            this.RenderResolutionHeightLabel.AutoSize = true;
            this.RenderResolutionHeightLabel.Location = new System.Drawing.Point(138, 7);
            this.RenderResolutionHeightLabel.Name = "RenderResolutionHeightLabel";
            this.RenderResolutionHeightLabel.Size = new System.Drawing.Size(38, 13);
            this.RenderResolutionHeightLabel.TabIndex = 5;
            this.RenderResolutionHeightLabel.Text = "Height";
            // 
            // RenderResolutionHeightTextBox
            // 
            this.RenderResolutionHeightTextBox.Location = new System.Drawing.Point(181, 4);
            this.RenderResolutionHeightTextBox.Name = "RenderResolutionHeightTextBox";
            this.RenderResolutionHeightTextBox.Size = new System.Drawing.Size(75, 20);
            this.RenderResolutionHeightTextBox.TabIndex = 4;
            this.RenderResolutionHeightTextBox.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // RenderResolutionWidthLabel
            // 
            this.RenderResolutionWidthLabel.AutoSize = true;
            this.RenderResolutionWidthLabel.Location = new System.Drawing.Point(4, 7);
            this.RenderResolutionWidthLabel.Name = "RenderResolutionWidthLabel";
            this.RenderResolutionWidthLabel.Size = new System.Drawing.Size(35, 13);
            this.RenderResolutionWidthLabel.TabIndex = 3;
            this.RenderResolutionWidthLabel.Text = "Width";
            // 
            // RenderResolutionWidthTextBox
            // 
            this.RenderResolutionWidthTextBox.Location = new System.Drawing.Point(42, 4);
            this.RenderResolutionWidthTextBox.Name = "RenderResolutionWidthTextBox";
            this.RenderResolutionWidthTextBox.Size = new System.Drawing.Size(75, 20);
            this.RenderResolutionWidthTextBox.TabIndex = 2;
            this.RenderResolutionWidthTextBox.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainToolStripCurrentTaskLabel,
            this.toolStripStatusLabel3,
            this.MainToolStripTaskProgressBar,
            this.MainToolStripTaskLabel});
            this.MainStatusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 451);
            this.MainStatusStrip.MaximumSize = new System.Drawing.Size(931, 22);
            this.MainStatusStrip.MinimumSize = new System.Drawing.Size(931, 22);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(931, 22);
            this.MainStatusStrip.TabIndex = 7;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // MainToolStripCurrentTaskLabel
            // 
            this.MainToolStripCurrentTaskLabel.Name = "MainToolStripCurrentTaskLabel";
            this.MainToolStripCurrentTaskLabel.Size = new System.Drawing.Size(97, 17);
            this.MainToolStripCurrentTaskLabel.Text = "Current Task: Idle";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(25, 17);
            // 
            // MainToolStripTaskProgressBar
            // 
            this.MainToolStripTaskProgressBar.Name = "MainToolStripTaskProgressBar";
            this.MainToolStripTaskProgressBar.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.MainToolStripTaskProgressBar.Size = new System.Drawing.Size(125, 16);
            this.MainToolStripTaskProgressBar.Step = 1;
            this.MainToolStripTaskProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.MainToolStripTaskProgressBar.ToolTipText = "Render Progress ([Percentage])";
            // 
            // MainToolStripTaskLabel
            // 
            this.MainToolStripTaskLabel.Name = "MainToolStripTaskLabel";
            this.MainToolStripTaskLabel.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.MainToolStripTaskLabel.Size = new System.Drawing.Size(25, 17);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(931, 473);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.RenderPropertiesGroupBox);
            this.Controls.Add(this.RenderPreviewGroupBox);
            this.Controls.Add(this.PropertiesGroupBox);
            this.Controls.Add(this.SceneTreeGroupBox);
            this.Controls.Add(this.MainMenuStrip);
            this.MaximumSize = new System.Drawing.Size(947, 512);
            this.MinimumSize = new System.Drawing.Size(947, 512);
            this.Name = "FormMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "EPQ_Raytrace_Engine";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.SceneTreeNodeContextMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.SceneTreeGroupBox.ResumeLayout(false);
            this.PropertiesGroupBox.ResumeLayout(false);
            this.RenderPreviewGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RenderPreviewPictureBox)).EndInit();
            this.RenderPropertiesGroupBox.ResumeLayout(false);
            this.RenderSamplesGroupBox.ResumeLayout(false);
            this.RenderSamplesPanel.ResumeLayout(false);
            this.RenderSamplesPanel.PerformLayout();
            this.RenderResolutionGroupBox.ResumeLayout(false);
            this.RenderResolutionPanel.ResumeLayout(false);
            this.RenderResolutionPanel.PerformLayout();
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView SceneTreeView;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newSceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.GroupBox SceneTreeGroupBox;
        private System.Windows.Forms.GroupBox PropertiesGroupBox;
        private System.Windows.Forms.GroupBox RenderPreviewGroupBox;
        private System.Windows.Forms.ToolStripMenuItem renderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startStopPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem startStopFullResRenderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveRenderToolStripMenuItem;
        private System.Windows.Forms.PictureBox RenderPreviewPictureBox;
        private System.Windows.Forms.GroupBox RenderPropertiesGroupBox;
        private System.Windows.Forms.GroupBox RenderResolutionGroupBox;
        private System.Windows.Forms.Panel RenderResolutionPanel;
        private System.Windows.Forms.Label RenderResolutionHeightLabel;
        private System.Windows.Forms.TextBox RenderResolutionHeightTextBox;
        private System.Windows.Forms.Label RenderResolutionWidthLabel;
        private System.Windows.Forms.TextBox RenderResolutionWidthTextBox;
        private System.Windows.Forms.GroupBox RenderSamplesGroupBox;
        private System.Windows.Forms.Panel RenderSamplesPanel;
        private System.Windows.Forms.CheckBox RenderSamplesCheckBox;
        private System.Windows.Forms.Label RenderSamplesValueLabel;
        private System.Windows.Forms.TextBox RenderSamplesValueTextBox;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel MainToolStripCurrentTaskLabel;
        private System.Windows.Forms.ToolStripProgressBar MainToolStripTaskProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel MainToolStripTaskLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sphereToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem planeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem materialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lambertianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dielectricToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem diffuseLightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem constantTextureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkerTextureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem perlinNoiseTextureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turbulentNoiseTextureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem marbleNoiseTextureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageTextureToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip SceneTreeNodeContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem DeleteNodeItem;
        private System.Windows.Forms.ToolStripMenuItem newCornellBoxSceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newRaytracingInOneWeekendBookCoverSceneToolStripMenuItem;
    }
}