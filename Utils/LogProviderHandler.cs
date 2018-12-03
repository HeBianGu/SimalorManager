using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.SimalorManager
{

    /// <summary> 注册事件写日志 (优势可以注册取消多个，静态注册) </summary>
    public partial class LogProviderHandler:ServiceFactory<LogProviderHandler>
    {
        /// <summary> 注册日志 p1=运行日志 p2=错误日志 p3=错误日志 </summary>
        public void RegisterLogComponent(Action<string, string> runlog, Action<string, string> errlog, Action<string, Exception> errExlog)
        {
            this.RunLog += runlog;
            this.ErrLog += errlog;
            this.ErrExLog += errExlog;
        }


        /// <summary> 清理日志 </summary>
        public void Clear()
        {

        }


        Action<string, string> _runLog;
        
        /// <summary> 运行日志委托 </summary>
        public event Action<string, string> RunLog
        {
            add
            {
                _runLog += value;
            }
            remove
            {
                if (_runLog != null)
                    _runLog -= value;
            }

        }

        Action<string, string> _errLog;

        /// <summary> 错误日志委托 </summary>
        public event Action<string, string> ErrLog
        {
            add
            {
                _errLog += value;
            }
            remove
            {
                if (_errLog != null)
                    _errLog -= value;
            }
        }

        Action<string, Exception> _errExLog;

        /// <summary> 错误日志委托 </summary>
        public event Action<string, Exception> ErrExLog
        {
            add
            {
                _errExLog += value;
            }
            remove
            {
                if (_errExLog != null)
                    _errExLog -= value;
            }
        }


        /// <summary> 此方法的说明 </summary>
        public void OnRunLog(string tatol, string message)
        {
            if (this._runLog == null) return;

            this._runLog(tatol, message);
        }

        /// <summary> 此方法的说明 </summary>
        public void OnRunLog(string message)
        {
            if (this._runLog == null) return;

            this._runLog("Run Message -" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), message);
        }

        /// <summary> 此方法的说明 </summary>
        public void OnErrLog(string tatol, string message)
        {
            if (this._errLog == null) return;

            this._errLog(tatol + " - " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), message);
        }


        /// <summary> 此方法的说明 </summary>
        public void OnErrLog(string message)
        {
            if (this._errLog == null) return;

            this._errLog("Error Message - " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), message);
        }
        /// <summary> 此方法的说明 </summary>
        public void OnErrLog(string tatol, Exception message)
        {
            if (this._errExLog == null) return;

            this._errExLog(tatol, message);
        }

        /// <summary> 此方法的说明 </summary>
        public void OnErrLog(Exception message)
        {
            if (this._errExLog == null) return;

            this._errExLog("Error Message - " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), message);
        }
    }
    
    /// <summary> 日志文本格式化类 </summary>
    public static class LogFormatHelper
    {
        /// <summary> 格式化标头 </summary>
        public static string ToTatol(this string sender, string detial = null)
        {
            if (string.IsNullOrEmpty(detial))
            {
                return string.Format("{0}  : ", sender);
            }
            return string.Format("{0}  :  {1}", sender, detial.ToDetial());
        }

        /// <summary> 格式化详细信息 </summary>
        public static string ToDetial(this string sender)
        {
            return string.Format(" {0} ", sender);
        }

        /// <summary> 格式化错误信息 </summary>
        public static string ToExc(this Exception sender)
        {
            return sender.ToString();
        }
    }



}
