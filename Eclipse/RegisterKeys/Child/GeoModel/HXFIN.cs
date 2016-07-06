#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/2 10:38:01
 * 文件名：START
 * 说明：
 * ROCK
             0            11
/
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child
{
    /// <summary> NXFIN中参数个数=X2-X1+1</summary>
    [KeyAttribute(EclKeyType = EclKeyType.Grid, SimKeyType = SimKeyType.Eclipse)]
    public class HXFIN : DynamicSingleLineKey
    {
        public HXFIN(string _name)
            : base(_name)
        {

        }

    }

    public class DynamicSingleLineKey : DynamicKey
    {
        public DynamicSingleLineKey(string _name)
            : base(_name)
        {

        }

        /// <summary> 获取项字符串集合 格式： 4 3 3 2 / </summary>
        protected override void CmdGetWellLines(List<StringModel> pItems)
        {
            this.Lines.Clear();

            StringBuilder sb = new StringBuilder();

            string s = string.Empty;

            for (int i = 0; i < pItems.Count; i++)
            {
                //   没找到直接插入 有可能是新增
                sb.Append(pItems[i].Value.ToDD());
            }
            sb.Append(KeyConfiger.EndFlag);
            this.Lines.Add(sb.ToString());

        }

        protected override void CmdGetWellItems()
        {
            this.Items.Clear();

            string str = string.Empty;

            for (int i = 0; i < Lines.Count; i++)
            {
                str = Lines[i];
                //  读到结束符不继续读取
                if (str.StartsWith("/") && str.EndsWith("/"))
                {
                    break;
                }

                //  不为空的行
                if (str.IsWorkLine())
                {
                    List<string> newStr = str.EclExtendToPetrelArray();

                    if (newStr.Count > 0)
                    {
                        int index = 1;
                        newStr.ForEach(l => this.Items.Add(StringModel.Build(l, (index++).ToString())));
                    }
                }

            }
        }

    }
}
