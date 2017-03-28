#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 17:43:17

 * 说明：
 * 
 *
BOX PERMX  1   5  1   1   1   1    '*'    2  
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> 范围类 </summary>
    public class BOX : ConfigerKey
    {
        public BOX(string _name)
            : base(_name)
        {
            this.CreaterHandler = (l, k) =>
                {
                    if (l is TIME)
                    {
                        l.Add(k);
                    }
                    else if (l is PERF)
                    {
                        // Todo ：添加到WELL同一级 
                        l.ParentKey.ParentKey.Add(k);
                    }
                    else
                    {
                        l.ParentKey.Add(k);
                    }
                };
        }

        private string _keyName;
        /// <summary> 关键字名称 </summary>
        public string KeyName
        {
            get { return _keyName; }
            set { _keyName = value; }
        }

        private RegionParam _region = new RegionParam();
        /// <summary> 修改范围 </summary>
        public RegionParam Region
        {
            get { return _region; }
            set { _region = value; }
        }

        private string _func;
        /// <summary> 修改方法 </summary>
        public string Func
        {
            get
            {
                switch (this._func)
                {
                    case "+":
                        return "+";
                    case "=":
                        return "=";
                    case "*":
                        return "*";
                    case "1*":
                        return "*";
                    default:
                        return _func;
                }
            }
            set { _func = value; }
        }

        private string _value;
        /// <summary> 修改的值 </summary>
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary> 执行的操作运算 </summary>
        public Func<double, double, double> Fuction
        {
            get
            {
                switch (this._func)
                {
                    case "+":
                        return (l, k) => l + k;
                    case "=":
                        return (l, k) => l = k;
                    case "*":
                        return (l, k) => l * k;
                    case "1*":
                        return (l, k) => l * k;
                    default:
                        throw new ArgumentException("解析BOX运算符错误:" + this._func);
                }
            }
        }


        public override void Build(List<string> newStr)
        {
            for (int i = 0; i < newStr.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        this._keyName = newStr[0];
                        break;
                    case 1:
                        this._region.XFrom = newStr[1].ToInt();
                        break;
                    case 2:
                        this._region.XTo = newStr[2].ToInt(); ;
                        break;
                    case 3:
                        this._region.YFrom = newStr[3].ToInt(); ;
                        break;
                    case 4:
                        this._region.YTo = newStr[4].ToInt(); ;
                        break;
                    case 5:
                        this._region.ZFrom = newStr[5].ToInt(); ;
                        break;
                    case 6:
                        this._region.ZTo = newStr[6].ToInt(); ;
                        break;
                    case 7:
                        this._func = newStr[7];
                        break;
                    case 8:
                        this.Value = newStr[8];
                        break;
                    default:
                        break;
                }
            }
        }

        public override void WriteKeyMethod(StreamWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine(this.ToString());
        }

        //  BOX PERMZ  6   10  1   1   1   1    '+'    -0.02
        string format = "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}";
        public override string ToString()
        {
            return string.Format(format, this.GetType().Name.ToDWithOutSpace(), this.KeyName.ToSDD(), (this._region.XFrom + 1).ToString().ToSDD(), this._region.XTo.ToString().ToSDD(), (this._region.YFrom + 1).ToString().ToSDD(), this._region.YTo.ToString().ToSDD(), (this._region.ZFrom + 1).ToString().ToSDD(), this._region.ZTo.ToString().ToSDD(), this.Func.ToSimONStr(), this.Value);
        }

    }
}
