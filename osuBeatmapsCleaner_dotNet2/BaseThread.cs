using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace osuBeatmapsBlocker_dotNet2
{
    public abstract class BaseThread
    {
        #region Viriables
        public Thread t;
        #endregion

        #region Properties
        /// <summary>
        /// 当前线程状态
        /// </summary>
        public ThreadState ThreadState
        {
            get
            {
                return t.ThreadState;
            }
        }
        #endregion

        #region Constructors
        public BaseThread()
        {
            t = new Thread(Run);
        }
        #endregion

        #region Methods
        public abstract void Run();

        public void Start()
        {
            if (t != null)
            {
                t.Start();
            }
        }

        public void Abort()
        {
            if (t != null)
            {
                t.Abort();
            }
        }

        #endregion
    }
}
