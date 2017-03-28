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
using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager.RegisterKeys.SimON
{
    /// <summary> 平衡法初始化的参数 </summary>
    public class EQUILPAR : MergeConfiger
    {
        public EQUILPAR(string _name)
            : base(_name)
        {

        }


        string szstzdhs0 = null;
        /// <summary> 1.	参考深度Dref，单位：m(米制)，feet(英制)，cm(lab)，ms(MESO) </summary>
        public string Szstzdhs0
        {
            get { return szstzdhs0; }
            set { szstzdhs0 = value; }
        }

        private string szstljs1 = null;
        /// <summary> 2.	参考深度的压力pref：单位：bar(米制)，psi(英制)，atm(lab)，Pa(MESO) </summary>
        public string Szstljs1
        {
            get { return szstljs1; }
            set { szstljs1 = value; }
        }

        private string ctstyxhs2 = null;
        /// <summary> 3.	油水界面深度OWC，在气水模型中是气水界面深度GWC，单位：m(米制)，feet(英制)，cm(lab)，um(MESO) </summary>
        public string Ctstyxhs2
        {
            get { return ctstyxhs2; }
            set { ctstyxhs2 = value; }
        }

        private string ctstyxzdhs3 = null;
        /// <summary> 4.	油水界面处的毛管力pcowc，在气水模型中是气水界面处的毛管力pcgwc，单位：bar(米制)，psi(英制)，atm(lab)，Pa(MESO) </summary>
        public string Ctstyxzdhs3
        {
            get { return ctstyxzdhs3; }
            set { ctstyxzdhs3 = value; }
        }

        private string jxstzds4 = "1";
        /// <summary> 5.	初始化时的深度步长dh，模拟器会自动调整过大或过小的步长，单位：m(米制)，feet(英制)，cm(lab)，um(MESO) </summary>
        public string Jxstzds4
        {
            get { return jxstzds4; }
            set { jxstzds4 = value; }
        }

        private string e100wgzds5 = null;
        /// <summary> 6.	油气界面深度GOC，单位：m(米制)，feet(英制)，cm(lab)，um(MESO) </summary>
        public string E100wgzds5
        {
            get { return e100wgzds5; }
            set { e100wgzds5 = value; }
        }

        private string e300jxstzds6 = null;
        /// <summary> 7.	油气界面处的毛管力pcgoc，单位：bar(米制)，psi(英制)，atm(lab)，Pa(MESO) </summary>
        public string E300jxstzds6
        {
            get { return e300jxstzds6; }
            set { e300jxstzds6 = value; }
        }


        /// <summary> 初始化 </summary>
        public void Init()
        {
            szstzdhs0 = null;
            szstljs1 = null;
            ctstyxhs2 = null;
            ctstyxzdhs3 = null;
            jxstzds4 = "1";
            e100wgzds5 = null;
            e300jxstzds6 = null;
        }



        string formatStr = @"
# Ref_dep    Ref_p    GWC/OWC  GWC_pc/OWC_pc   dh  
    {0}       {1}       {2}        {3}        {4}     
# GOC  GOC_pc
  {5}   {6}";

        string formatStrGasWater = @"
# Ref_dep    Ref_p    GWC/OWC  GWC_pc/OWC_pc   dh  
    {0}       {1}       {2}        {3}        {4}";

        /// <summary> 转换成字符串 </summary>
        public override string ToString()
        {
            if (this.BaseFile != null && this.BaseFile.Key != null)
            {
                MODELTYPE modeltype = this.BaseFile.Key.Find<MODELTYPE>();
                if (modeltype != null)
                {
                    // Todo ：气水或油水模型，平衡初始化数据只有前5个参数 
                    if (modeltype.MetricType == MetricType.GASWATER || modeltype.MetricType == MetricType.OILWATER)
                    {

                        return string.Format(formatStrGasWater, szstzdhs0.ToSDD(), szstljs1.ToSDD(), ctstyxhs2.ToSDD(), ctstyxzdhs3.ToSDD(), jxstzds4.ToSDD());
                    }
                }
            }

            return string.Format(formatStr, szstzdhs0.ToSDD(), szstljs1.ToSDD(), ctstyxhs2.ToSDD(), ctstyxzdhs3.ToSDD(),
                              jxstzds4.ToSDD(), e100wgzds5.ToSDD(), e300jxstzds6.ToSDD());


        }

        /// <summary> 解析字符串 </summary>
        public override void Build(List<string> newStr)
        {
            this.ID = Guid.NewGuid().ToString();

            for (int i = 0; i < newStr.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        this.szstzdhs0 = newStr[0];
                        break;
                    case 1:
                        this.szstljs1 = newStr[1];
                        break;
                    case 2:
                        this.ctstyxhs2 = newStr[2];
                        break;
                    case 3:
                        this.ctstyxzdhs3 = newStr[3];
                        break;
                    case 4:
                        this.jxstzds4 = newStr[4];
                        break;
                    case 5:
                        this.e100wgzds5 = newStr[5];
                        break;
                    case 6:
                        this.e300jxstzds6 = newStr[6];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
