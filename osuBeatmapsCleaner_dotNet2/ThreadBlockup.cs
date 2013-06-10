using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace osuBeatmapsBlocker_dotNet2
{
    public class ThreadBlockup : BaseThread
    {
        #region Viriables
        private string _osuInstalledLocation = "";
        private List<string> _folders = new List<string>();
        private List<string> _customBlockFiles = new List<string>();
        private string _requestedBlockupFullFolderPath = "";
        private bool _blockVideo = false;
        private bool _blockStoryboard = false;
        private BackgroundBlockStyle _backgroundBlockStyle = BackgroundBlockStyle.none;
        private bool _blockCustomFiles = false;
        private int _delay = 5000;
        #endregion

        #region Enums
        /// <summary>
        /// 屏蔽背景图片的方式
        /// </summary>
        public enum BackgroundBlockStyle
        {
            /// <summary>
            /// 不更换背景(还原背景)
            /// </summary>
            none = 0,
            /// <summary>
            /// 换成黑色背景
            /// </summary>
            black = 1,
            /// <summary>
            /// 换成白色北京
            /// </summary>
            white = 2
        }
        #endregion

        #region ConstantValues
        public const int BACKGROUND_BLOCK_STYLE_NONE = 0;
        public const int BACKGROUND_BLOCK_STYLE_BLACK = 1;
        public const int BACKGROUND_BLOCK_STYLE_WHITE = 2;
        #endregion

        #region Properties
        private int _blockedBeatmaps = 0;
        /// <summary>
        /// 已屏蔽的beatmap数量
        /// </summary>
        public int BlockedBeatmaps
        {
            get { return _blockedBeatmaps; }
            set { _blockedBeatmaps = value; }
        }

        /// <summary>
        /// 要屏蔽的beatmap文件夹列表
        /// </summary>
        public int TotalBeatmaps { get; private set; }

        #endregion

        #region Constructors
        /// <summary>
        /// 初始化ThreadBlockup
        /// </summary>
        /// <param name="threadName">线程名称</param>
        /// <param name="osuInstalledLocation">osu!安装路径</param>
        /// <param name="folders">歌曲目录列表</param>
        /// <param name="blockCustomFiles">自定义屏蔽文件列表</param>
        /// <param name="blockVideo">是否屏蔽视频</param>
        /// <param name="blockStoryboard">是否屏蔽故事板</param>
        /// <param name="blackBackground">是否更换为黑色背景</param>
        /// <param name="blockPermanently"></param>
        public ThreadBlockup(string threadName, string osuInstalledLocation, List<string> folders, List<string> customBlockFiles, bool blockVideo, bool blockStoryboard, BackgroundBlockStyle backgroundBlockStyle, bool blockCustomFiles)
        {
            this.t.Name = threadName;
            this._osuInstalledLocation = osuInstalledLocation;
            this._folders = folders;
            this._customBlockFiles = customBlockFiles;
            this._blockVideo = blockVideo;
            this._blockStoryboard = blockStoryboard;
            this._backgroundBlockStyle = backgroundBlockStyle;
            this._blockCustomFiles = blockCustomFiles;
            this.TotalBeatmaps = _folders.Count;
        }

        /// <summary>
        /// 初始化ThreadBlockup(用于屏蔽单个歌曲)
        /// </summary>
        /// <param name="requestedBlockupFullFolderPath">要屏蔽的单个歌曲目录</param>
        /// <param name="blockCustomFiles">自定义屏蔽文件列表</param>
        /// <param name="delay">等待多少毫秒后屏蔽</param>
        /// <param name="blockVideo">是否屏蔽视频</param>
        /// <param name="blockStoryboard">是否屏蔽故事板</param>
        /// <param name="blackBackground">是否更换为黑色背景</param>
        public ThreadBlockup(string requestedBlockupFullFolderPath, List<string> customBlockFiles, int delay, bool blockVideo, bool blockStoryboard, BackgroundBlockStyle backgroundBlockStyle, bool blockCustomFiles)
        {
            this._customBlockFiles = customBlockFiles;
            this._requestedBlockupFullFolderPath = requestedBlockupFullFolderPath;
            this._delay = delay;
            this._blockVideo = blockVideo;
            this._blockStoryboard = blockStoryboard;
            this._backgroundBlockStyle = backgroundBlockStyle;
            this._blockCustomFiles = blockCustomFiles;
        }
        #endregion

        #region Methods
        public override void Run()
        {
            //判断是否为屏蔽所有歌曲
            if (_requestedBlockupFullFolderPath.Equals(""))
            {
                //屏蔽所有歌曲
                string _songsFolderLocation = _osuInstalledLocation + "\\Songs";
                //开始屏蔽
                foreach (string feitem_requestedFolder in _folders)
                {
                    blockupSong(_songsFolderLocation + "\\" + feitem_requestedFolder, _customBlockFiles, _blockVideo, _blockStoryboard, _backgroundBlockStyle, _blockCustomFiles);
                    BlockedBeatmaps++;
                }
            }
            else
            {
                //屏蔽单个歌曲
                //等待制定毫秒时间
                Thread.Sleep(_delay);
                blockupSong(_requestedBlockupFullFolderPath, _customBlockFiles, _blockVideo, _blockStoryboard, _backgroundBlockStyle, _blockCustomFiles);
                Console.Beep();
            }
            //释放资源
            _folders = null;
            _customBlockFiles = null;
            _osuInstalledLocation = null;
            _requestedBlockupFullFolderPath = null;
            _delay = -1;
            _blockVideo = false;
            _blockStoryboard = false;
            this._backgroundBlockStyle = BackgroundBlockStyle.black;
        }

        /// <summary>
        /// 根据int值取出属于BackgroundBlockStyle的枚举值
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public static BackgroundBlockStyle calcBackgroundBlockStyle(int style)
        {
            switch (style)
            {
                case (int)BackgroundBlockStyle.none:
                    return BackgroundBlockStyle.none;
                case (int)BackgroundBlockStyle.black:
                    return BackgroundBlockStyle.black;
                case (int)BackgroundBlockStyle.white:
                    return BackgroundBlockStyle.white;
            }
            return BackgroundBlockStyle.none;
        }

        /// <summary>
        /// 屏蔽一个beatmap
        /// </summary>
        /// <param name="requestedFolderFullPath">beatmap完整目录名</param>
        /// <param name="blockCustomFiles">自定义屏蔽文件列表</param>
        /// <param name="blockVideo">是否屏蔽视频</param>
        /// <param name="blockStoryboard">是否屏蔽故事板</param>
        /// <param name="blackBackground">是否更换为黑色背景</param>
        private void blockupSong(string requestedFolderFullPath, List<string> customBlockFiles, bool blockVideo, bool blockStoryboard, BackgroundBlockStyle backgroundBlockStyle, bool blockCustomFiles)
        {
            if (Directory.Exists(requestedFolderFullPath) == false)
            {
                return;
            }
            //假设一种情况,某歌曲已在Songs目录存在,用户打开个osz后osu!解压缩osz到同名的文件夹下,这样就有了源文件和屏蔽后文件同时存在的情况
            //解决方法:
            //如果原文件存在,大括号内再如果屏蔽后文件已存在,则删除原文件(sb、背景图之类的则写出一个透明图片),否则修改原文件名以屏蔽.
            //具体代码体现在下面的四个处理块
            List<FileInfo> _osuFilesInfo = OsuHelper.取目录下所有osu文件(requestedFolderFullPath);

            foreach (FileInfo feitem_osuFileInfo in _osuFilesInfo)
            {
                #region 处理视频
                if (blockVideo)
                {
                    string _videoFileName = requestedFolderFullPath + "\\" + OsuHelper.取osu文件内的视频文件名(feitem_osuFileInfo.FullName);
                    if (File.Exists(_videoFileName) == true)
                    {
                        //如果屏蔽后文件存在则删除原名文件
                        if (File.Exists(editFileNameBetweenBackupWord(_videoFileName, true)) == true)
                        {
                            File.Delete(_videoFileName);
                        }
                        else
                        {
                            File.Move(_videoFileName, editFileNameBetweenBackupWord(_videoFileName, true));
                        }
                    }
                }
                else
                {
                    string _videoFileNameBlocked = requestedFolderFullPath + "\\" + editFileNameBetweenBackupWord(OsuHelper.取osu文件内的视频文件名(feitem_osuFileInfo.FullName), true);
                    if (File.Exists(_videoFileNameBlocked) == true)
                    {
                        File.Delete(editFileNameBetweenBackupWord(_videoFileNameBlocked, false));
                        File.Move(_videoFileNameBlocked, editFileNameBetweenBackupWord(_videoFileNameBlocked, false));
                    }
                }
                #endregion
                #region 处理osu文件中的sb图片文件
                if (blockStoryboard)
                {
                    foreach (string feitem_sbFileName in OsuHelper.取osu文件内的SB文件名(feitem_osuFileInfo.FullName))
                    {
                        string _sbFileName = requestedFolderFullPath + "\\" + feitem_sbFileName;
                        //因下面的代码有处理背景图片一块,而有些sb内也写有背景文件信息,所以会有"不能移动到同名文件"的错误
                        //解决方法和处理背景图片方法一样
                        //再一个就是部分beatmap把背景图片也放在Storyboard中导致osu!提示背景图片被删除,解决方法和处理背景图片相同,先备份sb图片,然后写出一个相同文件名的透明图片.
                        if (File.Exists(_sbFileName) == true)
                        {
                            if (File.Exists(editFileNameBetweenBackupWord(_sbFileName, true)) == false)
                            {
                                File.Move(_sbFileName, editFileNameBetweenBackupWord(_sbFileName, true));
                            }
                            File.WriteAllBytes(_sbFileName, osuBeatmapsBlocker_dotNet2.Properties.Resources.onePxPic_transparent_png);
                        }
                    }
                }
                else
                {
                    foreach (string feitem_sbFileName in OsuHelper.取osu文件内的SB文件名(feitem_osuFileInfo.FullName))
                    {
                        string _sbFileName = requestedFolderFullPath + "\\" + feitem_sbFileName;
                        //如果屏蔽后文件存在则恢复
                        if (File.Exists(editFileNameBetweenBackupWord(_sbFileName, true)) == true)
                        {
                            File.Delete(_sbFileName);
                            File.Move(editFileNameBetweenBackupWord(_sbFileName, true), _sbFileName);
                        }
                    }
                }
                #endregion
                #region 处理背景图
                switch (backgroundBlockStyle)
                {
                    case BackgroundBlockStyle.none:
                        {
                            string _bgFileNameBlocked = requestedFolderFullPath + "\\" + editFileNameBetweenBackupWord(OsuHelper.取osu文件内的背景图文件名(feitem_osuFileInfo.FullName), true);
                            //如果屏蔽后的图存在则说明已经屏蔽,需要恢复
                            if (File.Exists(_bgFileNameBlocked) == true)
                            {
                                File.Delete(editFileNameBetweenBackupWord(_bgFileNameBlocked, false));
                                File.Move(_bgFileNameBlocked, editFileNameBetweenBackupWord(_bgFileNameBlocked, false));
                            }
                        }
                        break;
                    case BackgroundBlockStyle.black:
                        {
                            string _bgFileName = requestedFolderFullPath + "\\" + OsuHelper.取osu文件内的背景图文件名(feitem_osuFileInfo.FullName);
                            //如果原背景图存在
                            if (File.Exists(_bgFileName) == true)
                            {
                                //如果屏蔽后背景图文件不存在
                                if (File.Exists(editFileNameBetweenBackupWord(_bgFileName, true)) == false)
                                {
                                    File.Move(_bgFileName, editFileNameBetweenBackupWord(_bgFileName, true));
                                }
                                File.WriteAllBytes(_bgFileName, osuBeatmapsBlocker_dotNet2.Properties.Resources.onePxPic_black_png);
                            }
                        }
                        break;
                    case BackgroundBlockStyle.white:
                        {
                            string _bgFileName = requestedFolderFullPath + "\\" + OsuHelper.取osu文件内的背景图文件名(feitem_osuFileInfo.FullName);
                            //如果原背景图存在
                            if (File.Exists(_bgFileName) == true)
                            {
                                //如果屏蔽后背景图文件不存在
                                if (File.Exists(editFileNameBetweenBackupWord(_bgFileName, true)) == false)
                                {
                                    File.Move(_bgFileName, editFileNameBetweenBackupWord(_bgFileName, true));
                                }
                                File.WriteAllBytes(_bgFileName, osuBeatmapsBlocker_dotNet2.Properties.Resources.onePxPic_white_png);
                            }
                        }
                        break;
                    default:
                        break;
                }
                #endregion
            }
            #region 处理osb文件
            if (blockStoryboard)
            {
                foreach (FileInfo feitem_osbFileInfo in OsuHelper.取目录下所有osb文件(requestedFolderFullPath, false))
                {
                    if (File.Exists(editFileNameBetweenBackupWord(feitem_osbFileInfo.FullName, true)) == true)
                    {
                        feitem_osbFileInfo.Delete();
                    }
                    else
                    {
                        feitem_osbFileInfo.MoveTo(editFileNameBetweenBackupWord(feitem_osbFileInfo.FullName, true));
                    }
                }
            }
            else
            {
                foreach (FileInfo feitem_osbFileInfo in OsuHelper.取目录下所有osb文件(requestedFolderFullPath, true))
                {
                    File.Delete(editFileNameBetweenBackupWord(feitem_osbFileInfo.FullName, false));
                    feitem_osbFileInfo.MoveTo(editFileNameBetweenBackupWord(feitem_osbFileInfo.FullName, false));
                }
            }
            #endregion
            #region 处理自定义文件
            foreach (string feitem_customBlockFile in OsuHelper.取目录下所有文件(requestedFolderFullPath, customBlockFiles, blockCustomFiles))
            {
                string _customBlockFileName = requestedFolderFullPath + "\\" + feitem_customBlockFile;
                if (blockCustomFiles)
                {
                    //如果原文件已存在
                    if (File.Exists(_customBlockFileName) == true)
                    {
                        //如果已被屏蔽
                        if (File.Exists(editFileNameBetweenBackupWord(_customBlockFileName, true)) == true)
                        {
                            File.Delete(_customBlockFileName);
                        }
                        else
                        {
                            File.Move(_customBlockFileName, editFileNameBetweenBackupWord(_customBlockFileName, true));
                        }
                    }
                }
                else
                {
                    //如果备份文件存在则恢复
                    if (File.Exists(_customBlockFileName) == true)
                    {
                        File.Delete(editFileNameBetweenBackupWord(_customBlockFileName, false));
                        File.Move(_customBlockFileName, editFileNameBetweenBackupWord(_customBlockFileName, false));
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// 永久性的删除掉自定义文件
        /// </summary>
        /// <param name="requestedFolderFullPath">beatmap完整目录名</param>
        /// <param name="customDeleteFiles">自定义要删除的文件列表</param>
        private void deleteCustomFilesPermanently(string requestedFolderFullPath, List<string> customDeleteFiles)
        {
            foreach (string feitem_customFileName in customDeleteFiles)
            {
                File.Delete(requestedFolderFullPath + "\\" + feitem_customFileName);
                File.Delete(editFileNameBetweenBackupWord(requestedFolderFullPath + "\\" + feitem_customFileName, true));
            }
        }

        /// <summary>
        /// 修改文件名并处理是否带backup文字
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="backup">是否带有backup文字</param>
        /// <returns>处理好的文字</returns>
        private string editFileNameBetweenBackupWord(string fileName, bool backup)
        {
            if (backup == true)
            {
                return fileName + ".blockosu";
            }
            else if (backup == false && fileName.EndsWith(".blockosu"))
            {
                return fileName.Substring(0, fileName.Length - 9);
            }
            throw new ArgumentException(string.Format("去除.blockosu时文件名末尾并无.blockosu({0})", fileName));
        }
        #endregion
    }
}
