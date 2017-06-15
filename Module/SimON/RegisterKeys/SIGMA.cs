#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 13:39:53

 * 说明：
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Eclipse.FileInfos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using OPT.Product.SimalorManager.Base.AttributeEx;

namespace OPT.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> 形状因子 分单值和枚举 </summary>
    [KeyAttribute(EclKeyType = EclKeyType.Grid, SimKeyType = SimKeyType.SimON)]
    public class SIGMA : TableKey
    {
        public SIGMA(string _name)
            : base(_name)
        {

        }

        public override void Build(int tableCount, int xCount, int yCount, string mmf = null)
        {
            if (this.BaseFile is SimalorManager.Eclipse.FileInfos.SimONData && this.BaseFile.DoubleType != DoubleType.DKJZMX)
            {
                // HTodo  ：SimON文件的双孔双渗只有一半数据 

                this.TransValueZ = l => l / 2;
                this.TransValueY = l => l;
                this.TransValueX = l => l;
            }
            else
            {
                this.TransValueX = l => 1;
                this.TransValueY = l => 1;
                this.TransValueZ = l => 1;
            }

            if (tableCount != 1 && xCount != 1 && yCount != 1)
            {
                if (this.Lines.FindAll(l => l.IsWorkLine()).Count == 1)
                {
                    if (this.Lines.First(l => l.IsWorkLine()).EclExceptSpaceToArray().Count == 1)
                    {
                        // HTodo  ：只有一行并且只有一个值认为是设置单值 
                        this.IsSingle = SIGMASelection.SIGMA;

                        this.TransValueX = l => 1;
                        this.TransValueY = l => 1;
                        this.TransValueZ = l => 1;
                    }
                }
            }


            base.Build(tableCount, xCount, yCount, mmf);
        }


        private SIGMASelection _isSingle = SIGMASelection.SIGMA;
        /// <summary> 是否是单值 </summary>
        public SIGMASelection IsSingle
        {
            get { return _isSingle; }
            set { _isSingle = value; }
        }

        /// <summary> 形状因子单值 </summary>
        public string Xzyz
        {
            get
            {
                if (this.Tables == null || this.Tables.Count == 0)
                {
                    return "1";
                }
                return this.Tables[0].Get(0, 0).ToString();
            }
            set
            {
                if (this.Tables != null || this.Tables.Count != 0)
                {
                    this.Tables[0].Set(0, 0, value.ToDouble());
                }
            }
        }

        string formatStr = "{0} ";


        public override void WriteKey(StreamWriter writer)
        {

            if (IsSingle == SIGMASelection.SIGMA)
            {
                WriteSingle(writer);
            }
            else
            {
                base.WriteKey(writer);
            }

        }



        /// <summary> 写单值方法 </summary> 
        void WriteSingle(StreamWriter writer)
        {

            writer.WriteLine(this.GetType().Name);
            writer.WriteLine(string.Format(formatStr, Xzyz.ToSDD()));
        }
    }

    /// <summary> 形状因子 </summary>
    public enum SIGMASelection
    {
        [Description("设置单值")]
        SIGMA = 0,
        [Description("网格枚举")]
        SIGMAV
    }
}
