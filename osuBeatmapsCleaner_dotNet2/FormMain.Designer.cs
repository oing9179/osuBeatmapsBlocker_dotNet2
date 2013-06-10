namespace osuBeatmapsBlocker_dotNet2
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "-----全部选中-----"}, -1, System.Drawing.Color.White, System.Drawing.Color.Black, null);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "-----全部选中-----"}, -1, System.Drawing.Color.White, System.Drawing.Color.Black, null);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.listViewBeatmaps = new System.Windows.Forms.ListView();
            this.columnHeaderBeatmapsName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewCustomBlockFiles = new System.Windows.Forms.ListView();
            this.columnHeaderCustomBlockFiles = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainerLists = new System.Windows.Forms.SplitContainer();
            this.textBoxSongListSearcher = new System.Windows.Forms.TextBox();
            this.buttonBlockUp = new System.Windows.Forms.Button();
            this.checkBoxBlockVideo = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoBlockup = new System.Windows.Forms.CheckBox();
            this.numericUpDownAutoBlockupDelay = new System.Windows.Forms.NumericUpDown();
            this.buttonRefreshList = new System.Windows.Forms.Button();
            this.fileSystemWatcherOsuSongFolder = new System.IO.FileSystemWatcher();
            this.progressBarBlockup = new System.Windows.Forms.ProgressBar();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.groupBoxBlockupOptions = new System.Windows.Forms.GroupBox();
            this.comboBoxChangeBackground = new System.Windows.Forms.ComboBox();
            this.checkBoxBlockCustomFiles = new System.Windows.Forms.CheckBox();
            this.checkBoxBlockStoryboard = new System.Windows.Forms.CheckBox();
            this.checkBoxSuspendOsuProcess = new System.Windows.Forms.CheckBox();
            this.labelMultiThreading = new System.Windows.Forms.Label();
            this.comboBoxThreads = new System.Windows.Forms.ComboBox();
            this.panelBlockUpProgressInfo = new System.Windows.Forms.Panel();
            this.buttonAbortBlockup = new System.Windows.Forms.Button();
            this.listViewThreadProgressInfo = new System.Windows.Forms.ListView();
            this.columnHeaderThreadName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderThreadProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timerBlockupProgressWatcher = new System.Windows.Forms.Timer(this.components);
            this.buttonOpenOsuFolder = new System.Windows.Forms.Button();
            this.buttonStartOsu = new System.Windows.Forms.Button();
            this.splitContainerLists.Panel1.SuspendLayout();
            this.splitContainerLists.Panel2.SuspendLayout();
            this.splitContainerLists.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoBlockupDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherOsuSongFolder)).BeginInit();
            this.groupBoxBlockupOptions.SuspendLayout();
            this.panelBlockUpProgressInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewBeatmaps
            // 
            this.listViewBeatmaps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewBeatmaps.CheckBoxes = true;
            this.listViewBeatmaps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderBeatmapsName});
            this.listViewBeatmaps.FullRowSelect = true;
            this.listViewBeatmaps.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            this.listViewBeatmaps.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listViewBeatmaps.Location = new System.Drawing.Point(0, 21);
            this.listViewBeatmaps.Name = "listViewBeatmaps";
            this.listViewBeatmaps.Size = new System.Drawing.Size(239, 415);
            this.listViewBeatmaps.TabIndex = 0;
            this.listViewBeatmaps.UseCompatibleStateImageBehavior = false;
            this.listViewBeatmaps.View = System.Windows.Forms.View.Details;
            this.listViewBeatmaps.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewBeatmaps_ItemChecked);
            // 
            // columnHeaderBeatmapsName
            // 
            this.columnHeaderBeatmapsName.Text = "Beatmap name";
            // 
            // listViewCustomBlockFiles
            // 
            this.listViewCustomBlockFiles.CheckBoxes = true;
            this.listViewCustomBlockFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderCustomBlockFiles});
            this.listViewCustomBlockFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewCustomBlockFiles.FullRowSelect = true;
            listViewItem2.StateImageIndex = 0;
            this.listViewCustomBlockFiles.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.listViewCustomBlockFiles.Location = new System.Drawing.Point(0, 0);
            this.listViewCustomBlockFiles.Name = "listViewCustomBlockFiles";
            this.listViewCustomBlockFiles.Size = new System.Drawing.Size(192, 436);
            this.listViewCustomBlockFiles.TabIndex = 1;
            this.listViewCustomBlockFiles.UseCompatibleStateImageBehavior = false;
            this.listViewCustomBlockFiles.View = System.Windows.Forms.View.Details;
            this.listViewCustomBlockFiles.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewCustomBlockFiles_ItemChecked);
            // 
            // columnHeaderCustomBlockFiles
            // 
            this.columnHeaderCustomBlockFiles.Text = "Custom block files";
            // 
            // splitContainerLists
            // 
            this.splitContainerLists.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerLists.Location = new System.Drawing.Point(279, 0);
            this.splitContainerLists.Name = "splitContainerLists";
            // 
            // splitContainerLists.Panel1
            // 
            this.splitContainerLists.Panel1.Controls.Add(this.textBoxSongListSearcher);
            this.splitContainerLists.Panel1.Controls.Add(this.listViewBeatmaps);
            // 
            // splitContainerLists.Panel2
            // 
            this.splitContainerLists.Panel2.Controls.Add(this.listViewCustomBlockFiles);
            this.splitContainerLists.Size = new System.Drawing.Size(435, 436);
            this.splitContainerLists.SplitterDistance = 239;
            this.splitContainerLists.TabIndex = 2;
            this.splitContainerLists.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainerLists_SplitterMoved);
            this.splitContainerLists.SizeChanged += new System.EventHandler(this.splitContainerLists_SizeChanged);
            // 
            // textBoxSongListSearcher
            // 
            this.textBoxSongListSearcher.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSongListSearcher.Location = new System.Drawing.Point(0, 0);
            this.textBoxSongListSearcher.Name = "textBoxSongListSearcher";
            this.textBoxSongListSearcher.Size = new System.Drawing.Size(239, 21);
            this.textBoxSongListSearcher.TabIndex = 1;
            this.textBoxSongListSearcher.TextChanged += new System.EventHandler(this.textBoxSongListSearcher_TextChanged);
            // 
            // buttonBlockUp
            // 
            this.buttonBlockUp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonBlockUp.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonBlockUp.Location = new System.Drawing.Point(12, 12);
            this.buttonBlockUp.Name = "buttonBlockUp";
            this.buttonBlockUp.Size = new System.Drawing.Size(251, 55);
            this.buttonBlockUp.TabIndex = 6;
            this.buttonBlockUp.Text = "开始处理...";
            this.buttonBlockUp.UseVisualStyleBackColor = true;
            this.buttonBlockUp.Click += new System.EventHandler(this.buttonBlockUp_Click);
            // 
            // checkBoxBlockVideo
            // 
            this.checkBoxBlockVideo.AutoSize = true;
            this.checkBoxBlockVideo.Checked = true;
            this.checkBoxBlockVideo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBlockVideo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBoxBlockVideo.Location = new System.Drawing.Point(6, 20);
            this.checkBoxBlockVideo.Name = "checkBoxBlockVideo";
            this.checkBoxBlockVideo.Size = new System.Drawing.Size(78, 17);
            this.checkBoxBlockVideo.TabIndex = 7;
            this.checkBoxBlockVideo.Text = "屏蔽视频";
            this.checkBoxBlockVideo.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoBlockup
            // 
            this.checkBoxAutoBlockup.AutoSize = true;
            this.checkBoxAutoBlockup.Checked = true;
            this.checkBoxAutoBlockup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoBlockup.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBoxAutoBlockup.Location = new System.Drawing.Point(6, 115);
            this.checkBoxAutoBlockup.Name = "checkBoxAutoBlockup";
            this.checkBoxAutoBlockup.Size = new System.Drawing.Size(144, 17);
            this.checkBoxAutoBlockup.TabIndex = 8;
            this.checkBoxAutoBlockup.Text = "自动屏蔽延迟(毫秒):";
            this.checkBoxAutoBlockup.UseVisualStyleBackColor = true;
            this.checkBoxAutoBlockup.CheckedChanged += new System.EventHandler(this.checkBoxAutoBlockup_CheckedChanged);
            // 
            // numericUpDownAutoBlockupDelay
            // 
            this.numericUpDownAutoBlockupDelay.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownAutoBlockupDelay.Location = new System.Drawing.Point(189, 114);
            this.numericUpDownAutoBlockupDelay.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownAutoBlockupDelay.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownAutoBlockupDelay.Name = "numericUpDownAutoBlockupDelay";
            this.numericUpDownAutoBlockupDelay.Size = new System.Drawing.Size(56, 21);
            this.numericUpDownAutoBlockupDelay.TabIndex = 10;
            this.numericUpDownAutoBlockupDelay.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownAutoBlockupDelay.ValueChanged += new System.EventHandler(this.numericUpDownAutoBlockupDelay_ValueChanged);
            // 
            // buttonRefreshList
            // 
            this.buttonRefreshList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonRefreshList.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonRefreshList.Location = new System.Drawing.Point(12, 73);
            this.buttonRefreshList.Name = "buttonRefreshList";
            this.buttonRefreshList.Size = new System.Drawing.Size(251, 32);
            this.buttonRefreshList.TabIndex = 12;
            this.buttonRefreshList.Text = "刷新列表";
            this.buttonRefreshList.UseVisualStyleBackColor = true;
            this.buttonRefreshList.Click += new System.EventHandler(this.buttonRefreshList_Click);
            // 
            // fileSystemWatcherOsuSongFolder
            // 
            this.fileSystemWatcherOsuSongFolder.EnableRaisingEvents = true;
            this.fileSystemWatcherOsuSongFolder.NotifyFilter = System.IO.NotifyFilters.DirectoryName;
            this.fileSystemWatcherOsuSongFolder.SynchronizingObject = this;
            this.fileSystemWatcherOsuSongFolder.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcherOsuSongFolder_Created);
            this.fileSystemWatcherOsuSongFolder.Deleted += new System.IO.FileSystemEventHandler(this.fileSystemWatcherOsuSongFolder_Deleted);
            this.fileSystemWatcherOsuSongFolder.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcherOsuSongFolder_Renamed);
            // 
            // progressBarBlockup
            // 
            this.progressBarBlockup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarBlockup.Location = new System.Drawing.Point(3, 365);
            this.progressBarBlockup.Name = "progressBarBlockup";
            this.progressBarBlockup.Size = new System.Drawing.Size(265, 15);
            this.progressBarBlockup.TabIndex = 14;
            // 
            // buttonAbout
            // 
            this.buttonAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAbout.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonAbout.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonAbout.Location = new System.Drawing.Point(12, 311);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(251, 34);
            this.buttonAbout.TabIndex = 16;
            this.buttonAbout.Text = "关于 osu! beatmaps blocker";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // groupBoxBlockupOptions
            // 
            this.groupBoxBlockupOptions.Controls.Add(this.comboBoxChangeBackground);
            this.groupBoxBlockupOptions.Controls.Add(this.checkBoxBlockCustomFiles);
            this.groupBoxBlockupOptions.Controls.Add(this.numericUpDownAutoBlockupDelay);
            this.groupBoxBlockupOptions.Controls.Add(this.checkBoxBlockStoryboard);
            this.groupBoxBlockupOptions.Controls.Add(this.checkBoxAutoBlockup);
            this.groupBoxBlockupOptions.Controls.Add(this.checkBoxBlockVideo);
            this.groupBoxBlockupOptions.Location = new System.Drawing.Point(12, 159);
            this.groupBoxBlockupOptions.Name = "groupBoxBlockupOptions";
            this.groupBoxBlockupOptions.Size = new System.Drawing.Size(251, 140);
            this.groupBoxBlockupOptions.TabIndex = 17;
            this.groupBoxBlockupOptions.TabStop = false;
            this.groupBoxBlockupOptions.Text = "屏蔽项目设置(取消勾选后将用于恢复)";
            // 
            // comboBoxChangeBackground
            // 
            this.comboBoxChangeBackground.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChangeBackground.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxChangeBackground.FormattingEnabled = true;
            this.comboBoxChangeBackground.Items.AddRange(new object[] {
            "不更换背景(还原背景)",
            "换成黑色背景",
            "换成白色背景"});
            this.comboBoxChangeBackground.Location = new System.Drawing.Point(6, 66);
            this.comboBoxChangeBackground.Name = "comboBoxChangeBackground";
            this.comboBoxChangeBackground.Size = new System.Drawing.Size(155, 20);
            this.comboBoxChangeBackground.TabIndex = 18;
            // 
            // checkBoxBlockCustomFiles
            // 
            this.checkBoxBlockCustomFiles.AutoSize = true;
            this.checkBoxBlockCustomFiles.Checked = true;
            this.checkBoxBlockCustomFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBlockCustomFiles.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBoxBlockCustomFiles.Location = new System.Drawing.Point(6, 92);
            this.checkBoxBlockCustomFiles.Name = "checkBoxBlockCustomFiles";
            this.checkBoxBlockCustomFiles.Size = new System.Drawing.Size(90, 17);
            this.checkBoxBlockCustomFiles.TabIndex = 17;
            this.checkBoxBlockCustomFiles.Text = "自定义屏蔽";
            this.checkBoxBlockCustomFiles.UseVisualStyleBackColor = true;
            // 
            // checkBoxBlockStoryboard
            // 
            this.checkBoxBlockStoryboard.AutoSize = true;
            this.checkBoxBlockStoryboard.Checked = true;
            this.checkBoxBlockStoryboard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBlockStoryboard.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBoxBlockStoryboard.Location = new System.Drawing.Point(6, 43);
            this.checkBoxBlockStoryboard.Name = "checkBoxBlockStoryboard";
            this.checkBoxBlockStoryboard.Size = new System.Drawing.Size(90, 17);
            this.checkBoxBlockStoryboard.TabIndex = 16;
            this.checkBoxBlockStoryboard.Text = "屏蔽故事版";
            this.checkBoxBlockStoryboard.UseVisualStyleBackColor = true;
            // 
            // checkBoxSuspendOsuProcess
            // 
            this.checkBoxSuspendOsuProcess.AutoSize = true;
            this.checkBoxSuspendOsuProcess.Checked = true;
            this.checkBoxSuspendOsuProcess.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSuspendOsuProcess.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBoxSuspendOsuProcess.Location = new System.Drawing.Point(18, 110);
            this.checkBoxSuspendOsuProcess.Name = "checkBoxSuspendOsuProcess";
            this.checkBoxSuspendOsuProcess.Size = new System.Drawing.Size(198, 17);
            this.checkBoxSuspendOsuProcess.TabIndex = 18;
            this.checkBoxSuspendOsuProcess.Text = "处理过程中暂停 osu!.exe 进程";
            this.checkBoxSuspendOsuProcess.UseVisualStyleBackColor = true;
            // 
            // labelMultiThreading
            // 
            this.labelMultiThreading.AutoSize = true;
            this.labelMultiThreading.Location = new System.Drawing.Point(16, 136);
            this.labelMultiThreading.Name = "labelMultiThreading";
            this.labelMultiThreading.Size = new System.Drawing.Size(71, 12);
            this.labelMultiThreading.TabIndex = 21;
            this.labelMultiThreading.Text = "多线程数量:";
            // 
            // comboBoxThreads
            // 
            this.comboBoxThreads.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxThreads.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxThreads.FormattingEnabled = true;
            this.comboBoxThreads.Location = new System.Drawing.Point(170, 133);
            this.comboBoxThreads.Name = "comboBoxThreads";
            this.comboBoxThreads.Size = new System.Drawing.Size(93, 20);
            this.comboBoxThreads.TabIndex = 22;
            // 
            // panelBlockUpProgressInfo
            // 
            this.panelBlockUpProgressInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelBlockUpProgressInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBlockUpProgressInfo.Controls.Add(this.buttonAbortBlockup);
            this.panelBlockUpProgressInfo.Controls.Add(this.listViewThreadProgressInfo);
            this.panelBlockUpProgressInfo.Controls.Add(this.progressBarBlockup);
            this.panelBlockUpProgressInfo.Location = new System.Drawing.Point(269, 1);
            this.panelBlockUpProgressInfo.Name = "panelBlockUpProgressInfo";
            this.panelBlockUpProgressInfo.Size = new System.Drawing.Size(273, 434);
            this.panelBlockUpProgressInfo.TabIndex = 23;
            this.panelBlockUpProgressInfo.Visible = false;
            // 
            // buttonAbortBlockup
            // 
            this.buttonAbortBlockup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAbortBlockup.Enabled = false;
            this.buttonAbortBlockup.Location = new System.Drawing.Point(3, 386);
            this.buttonAbortBlockup.Name = "buttonAbortBlockup";
            this.buttonAbortBlockup.Size = new System.Drawing.Size(265, 43);
            this.buttonAbortBlockup.TabIndex = 16;
            this.buttonAbortBlockup.Text = "立即停止!(&S)";
            this.buttonAbortBlockup.UseVisualStyleBackColor = true;
            this.buttonAbortBlockup.Click += new System.EventHandler(this.buttonAbortBlockup_Click);
            // 
            // listViewThreadProgressInfo
            // 
            this.listViewThreadProgressInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewThreadProgressInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderThreadName,
            this.columnHeaderThreadProgress});
            this.listViewThreadProgressInfo.FullRowSelect = true;
            this.listViewThreadProgressInfo.Location = new System.Drawing.Point(3, 3);
            this.listViewThreadProgressInfo.Name = "listViewThreadProgressInfo";
            this.listViewThreadProgressInfo.Size = new System.Drawing.Size(265, 356);
            this.listViewThreadProgressInfo.TabIndex = 15;
            this.listViewThreadProgressInfo.UseCompatibleStateImageBehavior = false;
            this.listViewThreadProgressInfo.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderThreadName
            // 
            this.columnHeaderThreadName.Text = "线程名";
            this.columnHeaderThreadName.Width = 80;
            // 
            // columnHeaderThreadProgress
            // 
            this.columnHeaderThreadProgress.Text = "进度";
            this.columnHeaderThreadProgress.Width = 140;
            // 
            // timerBlockupProgressWatcher
            // 
            this.timerBlockupProgressWatcher.Tick += new System.EventHandler(this.timerBlockupProgressWatcher_Tick);
            // 
            // buttonOpenOsuFolder
            // 
            this.buttonOpenOsuFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOpenOsuFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOpenOsuFolder.Image = global::osuBeatmapsBlocker_dotNet2.Properties.Resources.osuFolder_64x64;
            this.buttonOpenOsuFolder.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonOpenOsuFolder.Location = new System.Drawing.Point(179, 351);
            this.buttonOpenOsuFolder.Name = "buttonOpenOsuFolder";
            this.buttonOpenOsuFolder.Size = new System.Drawing.Size(84, 73);
            this.buttonOpenOsuFolder.TabIndex = 20;
            this.buttonOpenOsuFolder.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonOpenOsuFolder.UseVisualStyleBackColor = true;
            this.buttonOpenOsuFolder.Click += new System.EventHandler(this.buttonOpenOsuFolder_Click);
            // 
            // buttonStartOsu
            // 
            this.buttonStartOsu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStartOsu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonStartOsu.Image = global::osuBeatmapsBlocker_dotNet2.Properties.Resources.osuLogo_64x64;
            this.buttonStartOsu.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonStartOsu.Location = new System.Drawing.Point(12, 351);
            this.buttonStartOsu.Name = "buttonStartOsu";
            this.buttonStartOsu.Size = new System.Drawing.Size(161, 73);
            this.buttonStartOsu.TabIndex = 19;
            this.buttonStartOsu.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonStartOsu.UseVisualStyleBackColor = true;
            this.buttonStartOsu.Click += new System.EventHandler(this.buttonStartOsu_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 436);
            this.Controls.Add(this.splitContainerLists);
            this.Controls.Add(this.panelBlockUpProgressInfo);
            this.Controls.Add(this.comboBoxThreads);
            this.Controls.Add(this.labelMultiThreading);
            this.Controls.Add(this.buttonOpenOsuFolder);
            this.Controls.Add(this.buttonStartOsu);
            this.Controls.Add(this.checkBoxSuspendOsuProcess);
            this.Controls.Add(this.groupBoxBlockupOptions);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.buttonRefreshList);
            this.Controls.Add(this.buttonBlockUp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(291, 475);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "osu! beatmaps blocker by oing9179";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.splitContainerLists.Panel1.ResumeLayout(false);
            this.splitContainerLists.Panel1.PerformLayout();
            this.splitContainerLists.Panel2.ResumeLayout(false);
            this.splitContainerLists.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoBlockupDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherOsuSongFolder)).EndInit();
            this.groupBoxBlockupOptions.ResumeLayout(false);
            this.groupBoxBlockupOptions.PerformLayout();
            this.panelBlockUpProgressInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewBeatmaps;
        private System.Windows.Forms.SplitContainer splitContainerLists;
        private System.Windows.Forms.ListView listViewCustomBlockFiles;
        private System.Windows.Forms.Button buttonBlockUp;
        private System.Windows.Forms.ColumnHeader columnHeaderCustomBlockFiles;
        private System.Windows.Forms.ColumnHeader columnHeaderBeatmapsName;
        private System.Windows.Forms.CheckBox checkBoxBlockVideo;
        private System.Windows.Forms.CheckBox checkBoxAutoBlockup;
        private System.Windows.Forms.NumericUpDown numericUpDownAutoBlockupDelay;
        private System.Windows.Forms.Button buttonRefreshList;
        private System.IO.FileSystemWatcher fileSystemWatcherOsuSongFolder;
        private System.Windows.Forms.ProgressBar progressBarBlockup;
        private System.Windows.Forms.Button buttonAbout;
        private System.Windows.Forms.GroupBox groupBoxBlockupOptions;
        private System.Windows.Forms.CheckBox checkBoxBlockStoryboard;
        private System.Windows.Forms.CheckBox checkBoxBlockCustomFiles;
        private System.Windows.Forms.CheckBox checkBoxSuspendOsuProcess;
        private System.Windows.Forms.Button buttonStartOsu;
        private System.Windows.Forms.Button buttonOpenOsuFolder;
        private System.Windows.Forms.ComboBox comboBoxThreads;
        private System.Windows.Forms.Label labelMultiThreading;
        private System.Windows.Forms.Panel panelBlockUpProgressInfo;
        private System.Windows.Forms.ListView listViewThreadProgressInfo;
        private System.Windows.Forms.ColumnHeader columnHeaderThreadName;
        private System.Windows.Forms.ColumnHeader columnHeaderThreadProgress;
        private System.Windows.Forms.Button buttonAbortBlockup;
        private System.Windows.Forms.Timer timerBlockupProgressWatcher;
        private System.Windows.Forms.TextBox textBoxSongListSearcher;
        private System.Windows.Forms.ComboBox comboBoxChangeBackground;
    }
}

