using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace osuBeatmapsBlocker_dotNet2
{
    public class OsuHelper
    {
        #region Properties
        private static bool _osuIsSuspend = false;
        /// <summary>
        /// 获取或设置osu!进程是否暂停
        /// </summary>
        public static bool OsuIsSuspend
        {
            get
            {
                return _osuIsSuspend;
            }
            set
            {
                if (value == true)
                {
                    Process[] _processArray = Process.GetProcessesByName("osu!");
                    if (_processArray.Length == 1)
                    {
                        ProcessOperator.ProcessSuspend(_processArray[0].Id);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Process[] _processArray = Process.GetProcessesByName("osu!");
                    if (_processArray.Length == 1)
                    {
                        ProcessOperator.ProcessResume(_processArray[0].Id);
                    }
                    else
                    {
                        return;
                    }
                }
                _osuIsSuspend = value;
            }
        }

        #endregion

        #region Methods
        public static string 取osu安装位置()
        {
            string _osuInstalledFolder = "";
            try
            {
                RegistryKey regKey = Registry.ClassesRoot.OpenSubKey("osu!").OpenSubKey("shell").OpenSubKey("open").OpenSubKey("command");
                _osuInstalledFolder = regKey.GetValue("").ToString().Replace("\"", "").Replace("%1", "").Trim();
                if (_osuInstalledFolder.LastIndexOf("osu!.exe") != _osuInstalledFolder.Length - "osu!.exe".Length)
                {
                    return "[NOT_FOUND]";
                }
            }
            catch
            {
                return "[NOT_FOUND]";
            }
            return _osuInstalledFolder.Substring(0, _osuInstalledFolder.Length - "\\osu!.exe".Length);
        }

        public static string 取osu文件内的视频文件名(string osu文件名)
        {
            string[] _fileContent = File.ReadAllLines(osu文件名);
            foreach (string item in _fileContent)
            {
                if (item.IndexOf("Video,") == 0)
                {
                    string[] _lineContent = item.Split(',');
                    return _lineContent[_lineContent.Length - 1].Trim('\"');
                }
            }

            return "";
        }

        public static string 取osu文件内的背景图文件名(string osu文件名)
        {
            string[] _fileContent = File.ReadAllLines(osu文件名);
            for (int i = 0; i < _fileContent.Length; i++)
            {
                if (_fileContent[i].IndexOf("//Background and Video events") == 0)
                {
                    List<string> _splitedContents = new List<string>();
                    _splitedContents.AddRange(_fileContent[i + 1].Split(','));
                    _splitedContents.AddRange(_fileContent[i + 2].Split(','));
                    foreach (string feitem_pictureFileName in _splitedContents)
                    {
                        if (feitem_pictureFileName.ToLower().Contains("png") || feitem_pictureFileName.ToLower().Contains("jpg"))
                        {
                            return feitem_pictureFileName.Trim('\"');
                        }
                    }
                }
            }
            return "";
        }

        public static List<string> 取osu文件内的SB文件名(string osu文件名)
        {
            string[] _fileContent = File.ReadAllLines(osu文件名);
            List<string> _sbFiles = new List<string>();

            for (int i = 0; i < _fileContent.Length; i++)
            {
                //取Sprite对象
                if (_fileContent[i].IndexOf("Sprite,") == 0)
                {
                    string[] _lineContent = _fileContent[i].Split(',');
                    _sbFiles.Add(_lineContent[_lineContent.Length - 3].Trim('\"'));
                }
                //取Animation对象
                if (_fileContent[i].IndexOf("Animation,") == 0)
                {
                    string[] _lineContent = _fileContent[i].Split(',');//数组元素3为文件名,数组元素6为FrameCount
                    int _fileNameDotIndex = _lineContent[3].LastIndexOf('.') - 1;
                    string _animFrameFileName = _lineContent[3].Trim('\"');
                    for (int j = 0; j < int.Parse(_lineContent[6]); j++)
                    {
                        _sbFiles.Add(_animFrameFileName.Insert(_fileNameDotIndex, "" + j));
                    }
                }
                if (_fileContent[i].IndexOf("//Background Colour Transformations") == 0)
                {
                    break;
                }
            }

            return _sbFiles;
        }

        /// <summary>
        /// 过滤某目录下所有文件名,如果过滤器内某项不存在通配符,则 某项==某文件名 就会添加到结果列表中
        /// </summary>
        /// <param name="目录名">要取所有文件的目录名</param>
        /// <param name="过滤器">过滤器就是 自定义屏蔽文件列表</param>
        /// <param name="取未屏蔽的文件">true则取出未被屏蔽的文件，反之取出已经屏蔽的文件</param>
        /// <returns>处理完的文件名列表</returns>
        public static List<string> 取目录下所有文件(string 目录名, List<string> 过滤器, bool 取未屏蔽的文件)
        {
            List<string> _listFiltered = new List<string>();

            foreach (string _feitem_filter in 过滤器)
            {
                string _filterStartWith = null;
                string _fileterEndWith = null;
                int _feitem_filter_wildcardIndex = _feitem_filter.IndexOf('*');

                if (_feitem_filter_wildcardIndex != -1)
                {
                    _filterStartWith = _feitem_filter.Substring(0, _feitem_filter_wildcardIndex);
                    _fileterEndWith = _feitem_filter.Substring(_feitem_filter_wildcardIndex + 1);
                }

                foreach (FileInfo _feitem_fileInfo in new DirectoryInfo(目录名).GetFiles())
                {
                    //如果_feitem_filter_wildcardIndex==-1那么后边那俩string肯定是null，该变量值则变成false
                    bool _filtCondition = _feitem_filter_wildcardIndex != -1 && _feitem_fileInfo.Name.StartsWith(_filterStartWith) && (取未屏蔽的文件 ? _feitem_fileInfo.Name.EndsWith(_fileterEndWith) : _feitem_fileInfo.Name.EndsWith(_fileterEndWith + ".blockosu"));
                    //如果_filtCondition==true则取到了带通配符的文件名
                    if (_filtCondition || (取未屏蔽的文件 ? _feitem_fileInfo.Name.Equals(_feitem_filter) : _feitem_fileInfo.Name.Equals(_feitem_filter + ".blockosu")))
                    {
                        _listFiltered.Add(_feitem_fileInfo.Name);
                    }
                }
            }

            return _listFiltered;
        }

        public static List<FileInfo> 取目录下所有osu文件(string 目录名)
        {
            List<FileInfo> _osuFiles = new List<FileInfo>();
            foreach (FileInfo item in new DirectoryInfo(目录名).GetFiles("*.osu"))
            {
                _osuFiles.Add(item);
            }
            return _osuFiles;
        }

        public static List<FileInfo> 取目录下所有osb文件(string 目录名, bool 取已屏蔽的osb文件)
        {
            List<FileInfo> _osbFiles = new List<FileInfo>();

            foreach (FileInfo item in new DirectoryInfo(目录名).GetFiles())
            {
                if (取已屏蔽的osb文件)
                {
                    if (item.Name.EndsWith(".osb.blockosu") == true)
                    {
                        _osbFiles.Add(item);
                    }
                }
                else
                {
                    if (item.Name.EndsWith(".osb") == true)
                    {
                        _osbFiles.Add(item);
                    }
                }
            }
            return _osbFiles;
        }

        #endregion
    }
}
