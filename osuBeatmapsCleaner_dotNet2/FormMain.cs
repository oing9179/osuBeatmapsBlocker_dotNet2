using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace osuBeatmapsBlocker_dotNet2
{
    public partial class FormMain : Form
    {
        #region Viriables
        private static string _FILE_NAME_osuBeatmapsBlocker_CustomBlockList_txt = "osuBeatmapsBlocker_CustomBlockList.txt";

        private string osuInstalledLocation = "";
        private ThreadBlockup threadBlockup;
        private List<ThreadBlockup> threadBlockupStack = new List<ThreadBlockup>();
        #endregion

        #region Events
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //取出CPU核心数量
            for (int i = 1; i <= CPUHelper.GetCPUCoreCount(); i++)
            {
                comboBoxThreads.Items.Add(i);
            }
            //如果 核心数>=3 则选择选择 核心数-1 的选项，例如4核心就选3，否则选择第0个
            //非常感谢ShadowPower发现这个bug.
            comboBoxThreads.SelectedIndex = comboBoxThreads.Items.Count >= 3 ? comboBoxThreads.Items.Count - 2 : 0;
            //设置默认更换背景为黑色
            comboBoxChangeBackground.SelectedIndex = 1;
            //寻找osu!安装目录
            if ((osuInstalledLocation = OsuHelper.取osu安装位置()).Equals("[NOT_FOUND]"))
            {
                OpenFileDialog ofd1 = new OpenFileDialog();
                ofd1.Title = "选择你的 osu!.exe";
                ofd1.Multiselect = false;
                ofd1.InitialDirectory = Application.StartupPath;
                ofd1.Filter = "osu!.exe|osu!.exe";
                ofd1.CheckFileExists = true;
                ofd1.CheckPathExists = true;
                if (ofd1.ShowDialog() == DialogResult.OK)
                {
                    Console.WriteLine(ofd1.FileName);
                    osuInstalledLocation = ofd1.FileName.Substring(0, ofd1.FileName.LastIndexOf("\\"));
                    if (Directory.Exists(osuInstalledLocation + "\\Songs") == false)
                    {
                        Directory.CreateDirectory(osuInstalledLocation + "\\Songs");
                        if (MessageBox.Show("你是第一次玩osu!吧,连一首歌都没有?\n确定: 退出程序并去osu!官网下载beatmap\n取消: 退出程序", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                        {
                            Process.Start("https://osu.ppy.sh/p/beatmaplist/");
                        }
                        Application.Exit();
                        return;
                    }
                }
                else
                {
                    Application.Exit();
                    return;
                }
            }
            //如果不存在要屏蔽的文件信息则写出文件
            if (File.Exists(Application.StartupPath + "\\" + _FILE_NAME_osuBeatmapsBlocker_CustomBlockList_txt) == false)
            {
                File.WriteAllBytes(Application.StartupPath + "\\" + _FILE_NAME_osuBeatmapsBlocker_CustomBlockList_txt, osuBeatmapsBlocker_dotNet2.Properties.Resources.osuBeatmapsBlocker_CustomBlockList_txt);
            }
            //设置FileSystemWatcher的监视目录
            fileSystemWatcherOsuSongFolder.Path = osuInstalledLocation + "\\Songs";
            //初始化两个listView的ColumnHeader的宽度
            listViewBeatmaps.Columns[0].Width = listViewBeatmaps.Width - 30;
            listViewCustomBlockFiles.Columns[0].Width = listViewCustomBlockFiles.Width - 30;
            //刷新列表，并让列表第一个项"全部选中"勾选上
            requestRefreshList();
            listViewBeatmaps.Items[0].Checked = true;
            listViewCustomBlockFiles.Items[0].Checked = true;
            //在标题栏显示osu!安装位置
            this.Text += string.Format(" (osu! installed={0})", osuInstalledLocation);
            //设置屏蔽时候的线程信息panelBlockUpProgressInfo的位置
            panelBlockUpProgressInfo.Location = new Point(1, 1);
        }

        private void listViewBeatmaps_SizeChanged(object sender, EventArgs e)
        {
            listViewCustomBlockFiles.Columns[0].Width = listViewCustomBlockFiles.Width - 30;
        }

        private void listViewBeatmaps_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Index == 0)
            {
                for (int i = 1; i < listViewBeatmaps.Items.Count; i++)
                {
                    listViewBeatmaps.Items[i].Checked = e.Item.Checked;
                }
            }
        }

        private void listViewCustomBlockFiles_SizeChanged(object sender, EventArgs e)
        {
            listViewBeatmaps.Columns[0].Width = listViewBeatmaps.Width - 30;
        }

        private void listViewCustomBlockFiles_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Index == 0)
            {
                for (int i = 1; i < listViewCustomBlockFiles.Items.Count; i++)
                {
                    listViewCustomBlockFiles.Items[i].Checked = e.Item.Checked;
                }
            }
        }

        private void checkBoxAutoBlockup_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownAutoBlockupDelay.Enabled = checkBoxAutoBlockup.Checked;
        }

        private void buttonStartOsu_Click(object sender, EventArgs e)
        {
            Process.Start(osuInstalledLocation + "\\osu!.exe");
            buttonAbout.Focus();
        }

        private void buttonOpenOsuFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", osuInstalledLocation);
            buttonAbout.Focus();
        }

        private void buttonRefreshList_Click(object sender, EventArgs e)
        {
            requestRefreshList();
        }

        private void buttonBlockUp_Click(object sender, EventArgs e)
        {
            List<string> _songsFolder = new List<string>();
            List<string> _customBlockFiles = new List<string>();

            if (MessageBox.Show("您真的要开始屏蔽歌曲吗，这将会花费一些时间，但您可以中断这个操作。\n屏蔽完成后将会\"beep~\"的一声通知您", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }
            //禁用一些控件并显示 线程信息panel
            buttonBlockUp.Enabled = false;
            checkBoxSuspendOsuProcess.Enabled = false;
            panelBlockUpProgressInfo.Visible = true;
            buttonAbortBlockup.Enabled = true;
            buttonAbortBlockup.Focus();
            //过滤选中的Beatmaps和SkinFiles
            for (int i = 1; i < listViewBeatmaps.Items.Count; i++)
            {
                if (listViewBeatmaps.Items[i].Checked)
                {
                    _songsFolder.Add(listViewBeatmaps.Items[i].Text);
                }
            }
            for (int i = 1; i < listViewCustomBlockFiles.Items.Count; i++)
            {
                if (listViewCustomBlockFiles.Items[i].Checked)
                {
                    _customBlockFiles.Add(listViewCustomBlockFiles.Items[i].Text);
                }
            }
            //处理多线程设置
            {
                int _indexOf_songsFolder = 0;//在_songsFolder中的索引
                //第一种计算方法，(任务数 + (线程数 - (任务数 % 线程数))) / 线程数
                //第二种计算方法为下面的代码，目测还没有人在osu!歌曲目录下还有超过 int.MaxValue 数量的歌曲
                int _singleThreadTasks = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(_songsFolder.Count) / Convert.ToDouble(comboBoxThreads.SelectedIndex + 1)));

                threadBlockupStack = new List<ThreadBlockup>();
                for (int i = 1; i <= comboBoxThreads.SelectedIndex + 1; i++)
                {
                    List<string> _songsSingleThread = new List<string>();
                    for (int j = 0; j < _singleThreadTasks; j++)
                    {
                        if (_indexOf_songsFolder <= _songsFolder.Count - 1)
                        {
                            _songsSingleThread.Add(_songsFolder[_indexOf_songsFolder]);
                            _indexOf_songsFolder++;
                        }
                    }
                    if (_songsSingleThread.Count > 0)
                    {
                        threadBlockupStack.Add(new ThreadBlockup("thread:" + i, osuInstalledLocation, _songsSingleThread, _customBlockFiles, checkBoxBlockVideo.Checked, checkBoxBlockStoryboard.Checked, ThreadBlockup.calcBackgroundBlockStyle(comboBoxChangeBackground.SelectedIndex), checkBoxBlockCustomFiles.Checked));
                    }
                }
            }
            //暂停osu!.exe进程后开始屏蔽
            if (checkBoxSuspendOsuProcess.Checked == true)
            {
                OsuHelper.OsuIsSuspend = true;
            }
            //在listViewThreadProgressInfo中创建列表项并启动线程
            listViewThreadProgressInfo.Items.Clear();
            foreach (ThreadBlockup item in threadBlockupStack)
            {
                ListViewItem lvItem = new ListViewItem(item.t.Name);
                lvItem.SubItems.Add(string.Format("0/{0}(0%)", item.TotalBeatmaps));
                listViewThreadProgressInfo.Items.Add(lvItem);
                item.Start();
            }
            //屏蔽选中的歌曲
            timerBlockupProgressWatcher.Enabled = true;
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            string _messageText = string.Format("Version: {0}\nAuthor: oing9179\nEmail: oing9179@gmail.com\nSpecial thanks: ShadowPower\nDesigned for me~", Application.ProductVersion);
            MessageBox.Show(_messageText, "About osu! beatmaps blocker", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void fileSystemWatcherOsuSongFolder_Created(object sender, FileSystemEventArgs e)
        {
            //刷新Beatmaps和SkinFiles列表
            requestRefreshList();
            if (checkBoxAutoBlockup.Checked == true)
            {
                //屏蔽单个歌曲
                List<string> _skinFiles = new List<string>();
                for (int i = 1; i < listViewCustomBlockFiles.Items.Count; i++)
                {
                    if (listViewCustomBlockFiles.Items[i].Checked)
                    {
                        _skinFiles.Add(listViewCustomBlockFiles.Items[i].Text);
                    }
                }
                threadBlockup = new ThreadBlockup(e.FullPath, _skinFiles, Convert.ToInt32(numericUpDownAutoBlockupDelay.Value), checkBoxBlockVideo.Checked, checkBoxBlockStoryboard.Checked, ThreadBlockup.calcBackgroundBlockStyle(comboBoxChangeBackground.SelectedIndex), checkBoxBlockCustomFiles.Checked);
                threadBlockup.Start();
            }

        }

        private void fileSystemWatcherOsuSongFolder_Renamed(object sender, RenamedEventArgs e)
        {
            requestRefreshList();
        }

        private void fileSystemWatcherOsuSongFolder_Deleted(object sender, FileSystemEventArgs e)
        {
            requestRefreshList();
        }

        private void timerBlockupProgressWatcher_Tick(object sender, EventArgs e)
        {
            int _threadsSuspendCount = 0;
            int _progressSum = 0;

            for (int i = 0; i < threadBlockupStack.Count; i++)
            {
                float _threadBlockedBeatmaps = threadBlockupStack[i].BlockedBeatmaps;
                float _threadTotalBeatmaps = threadBlockupStack[i].TotalBeatmaps;
                listViewThreadProgressInfo.Items[i].SubItems[1].Text = string.Format("{0}/{1}({2}%)", _threadBlockedBeatmaps, _threadTotalBeatmaps, (_threadBlockedBeatmaps / _threadTotalBeatmaps) * 100);
                _progressSum += threadBlockupStack[i].BlockedBeatmaps;
                if (threadBlockupStack[i] != null && threadBlockupStack[i].ThreadState != System.Threading.ThreadState.Running)
                {
                    _threadsSuspendCount++;
                }
            }
            if (_threadsSuspendCount != threadBlockupStack.Count)
            {
                panelBlockUpProgressInfo.Visible = true;
                fileSystemWatcherOsuSongFolder.EnableRaisingEvents = false;
                progressBarBlockup.Value = _progressSum;
            }
            else
            {
                //恢复 osu!.exe 进程
                OsuHelper.OsuIsSuspend = false;
                //禁用计时器控件
                timerBlockupProgressWatcher.Enabled = false;
                //启用fileSystemWatcher继续监视osu!的Songs目录
                fileSystemWatcherOsuSongFolder.EnableRaisingEvents = true;
                //进度条设置为最大值告诉用户处理完毕
                progressBarBlockup.Value = progressBarBlockup.Maximum;
                //启用一些控件并隐藏panel
                buttonBlockUp.Enabled = true;
                checkBoxSuspendOsuProcess.Enabled = true;
                buttonAbortBlockup.Enabled = false;
                panelBlockUpProgressInfo.Visible = false;
                //beep一声通知用户处理完成
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void splitContainerLists_SizeChanged(object sender, EventArgs e)
        {
            listViewBeatmaps.Columns[0].Width = listViewBeatmaps.Width - 30;
            listViewCustomBlockFiles.Columns[0].Width = listViewCustomBlockFiles.Width - 30;
        }

        private void splitContainerLists_SplitterMoved(object sender, SplitterEventArgs e)
        {
            listViewBeatmaps.Columns[0].Width = listViewBeatmaps.Width - 30;
            listViewCustomBlockFiles.Columns[0].Width = listViewCustomBlockFiles.Width - 30;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (ThreadBlockup item in threadBlockupStack)
            {
                item.Abort();
            }
        }

        private void buttonAbortBlockup_Click(object sender, EventArgs e)
        {
            foreach (ThreadBlockup item in threadBlockupStack)
            {
                item.Abort();
            }
        }

        private void numericUpDownAutoBlockupDelay_ValueChanged(object sender, EventArgs e)
        {
            int _blockupDelay = Convert.ToInt32(numericUpDownAutoBlockupDelay.Value);
            numericUpDownAutoBlockupDelay.Value = _blockupDelay;
            timerBlockupProgressWatcher.Interval = _blockupDelay;
        }

        private void textBoxSongListSearcher_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBoxSongListSearcher.Text.Trim();

            listViewBeatmaps.BeginUpdate();
            if (searchText == "")
            {
                listViewBeatmaps.Items[0].Checked = true;
                listViewBeatmaps.TopItem = listViewBeatmaps.Items[0];
                listViewBeatmaps.Columns[0].Text = string.Format("歌曲列表({0})", listViewBeatmaps.Items.Count - 1);
            }
            else
            {
                int foundItems = 0;
                listViewBeatmaps.Items[0].Checked = false;
                foreach (ListViewItem feitem_lvItem in listViewBeatmaps.Items)
                {
                    if (feitem_lvItem.Text.ToLower().Contains(searchText))
                    {
                        feitem_lvItem.Checked = true;
                        foundItems++;
                        if (foundItems == 1)//如果是第一个找到的项,则显示在最前边
                        {
                            listViewBeatmaps.TopItem = feitem_lvItem;
                        }
                    }
                    else
                    {
                        feitem_lvItem.Checked = false;
                    }
                }
                listViewBeatmaps.Columns[0].Text = string.Format("歌曲列表({0})[{1}]", listViewBeatmaps.Items.Count - 1, foundItems);
            }
            listViewBeatmaps.EndUpdate();
        }
        #endregion

        #region Methods
        /// <summary>
        /// 为歌曲列表和批复文件列表显示文件数量
        /// </summary>
        private void setBeatmapsAndSkinFilesCount()
        {
            string _beatmapsCount = string.Format("({0})", listViewBeatmaps.Items.Count - 1);
            string _skinFilesCount = string.Format("({0})", listViewCustomBlockFiles.Items.Count - 1);
            listViewBeatmaps.Columns[0].Text = string.Format("歌曲列表({0})", listViewBeatmaps.Items.Count - 1);
            listViewCustomBlockFiles.Columns[0].Text = string.Format("自定义屏蔽文件列表({0})", listViewCustomBlockFiles.Items.Count - 1);
        }

        /// <summary>
        /// 刷新Beatmaps和SkinFiles列表
        /// </summary>
        private void requestRefreshList()
        {
            //加载osu的Songs目录下所有歌曲文件夹
            listViewBeatmaps.BeginUpdate();
            while (listViewBeatmaps.Items.Count > 1)
            {
                listViewBeatmaps.Items.RemoveAt(1);
            }
            DirectoryInfo[] _listOsuSongsFolders = new DirectoryInfo(osuInstalledLocation + "\\Songs").GetDirectories();
            Array.Sort(_listOsuSongsFolders, new CompareDirectoryInfo());
            for (int i = 0; i < _listOsuSongsFolders.Length; i++)//为距离现在时间不同的Beatmap设置不同的ListViewItem背景色,并添加到BeatmapsList列表
            {
                ListViewItem lvItem = new ListViewItem(_listOsuSongsFolders[i].Name);
                if (DateTime.Now - _listOsuSongsFolders[i].CreationTime < new TimeSpan(1, 0, 0, 0))//24小时内的新歌曲
                {
                    lvItem.BackColor = Color.FromArgb(130, 213, 255);
                }
                else if (DateTime.Now - _listOsuSongsFolders[i].CreationTime < new TimeSpan(2, 0, 0, 0))//48小时内的新歌曲
                {
                    lvItem.BackColor = Color.FromArgb(162, 224, 255);
                }
                else if (DateTime.Now - _listOsuSongsFolders[i].CreationTime < new TimeSpan(7, 0, 0, 0))//1星期内的新歌曲
                {
                    lvItem.BackColor = Color.FromArgb(193, 234, 255);
                }
                else if (DateTime.Now - _listOsuSongsFolders[i].CreationTime < new TimeSpan(31, 0, 0, 0))//1星期内的新歌曲
                {
                    lvItem.BackColor = Color.FromArgb(225, 245, 255);
                }
                listViewBeatmaps.Items.Add(lvItem);
            }
            listViewBeatmaps.Items[0].Checked = false;
            listViewBeatmaps.Items[0].Checked = true;
            listViewBeatmaps.EndUpdate();
            //加载要屏蔽的皮肤文件列表
            string[] _customBlockFilesName = File.ReadAllLines(Application.StartupPath + "\\" + _FILE_NAME_osuBeatmapsBlocker_CustomBlockList_txt);
            listViewCustomBlockFiles.BeginUpdate();
            while (listViewCustomBlockFiles.Items.Count > 1)
            {
                listViewCustomBlockFiles.Items.RemoveAt(1);
            }
            foreach (string item in _customBlockFilesName)
            {
                if (item.StartsWith("//") == false && item.Equals("") == false)//如果这行不是注释内容或者这行不是空行
                {
                    listViewCustomBlockFiles.Items.Add(item);
                }
            }
            listViewCustomBlockFiles.Items[0].Checked = false;
            listViewCustomBlockFiles.Items[0].Checked = true;

            listViewCustomBlockFiles.EndUpdate();
            //为歌曲列表和批复文件列表显示文件数量
            setBeatmapsAndSkinFilesCount();
            //设置进度条progressBarBlockup的最大值
            progressBarBlockup.Maximum = listViewBeatmaps.Items.Count - 1;
        }

        #endregion

    }

    /// <summary>
    /// 为文件夹按日期从最近到最远排序
    /// </summary>
    class CompareDirectoryInfo : IComparer<DirectoryInfo>
    {
        public int Compare(DirectoryInfo x, DirectoryInfo y)
        {
            //Compare方法一共有3种返回值 0 -1 1
            //x>y返回1,x<y返回-1,x==y返回0
            //由于文件夹要按日期从最近早最远排序，所以 x>y会返回-1
            if (x.CreationTime > y.CreationTime) return -1;
            if (x.CreationTime < y.CreationTime) return 1;
            return 0;
        }
    }
}
