using OPT.Product.SimalorManager.Base.AttributeEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child
{

    #region - 普通关键字 -
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class ALL : SingleKey
    {
        public ALL(string _name)
            : base(_name)
        {
            this.TitleStr = "输出所有常用指标";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FOPTH : SingleKey
    {
        public FOPTH(string _name)
            : base(_name)
        {
            this.TitleStr = "累产油（历史）";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FOPT : SingleKey
    {
        public FOPT(string _name)
            : base(_name)
        {
            this.TitleStr = "累产油";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FOPRH : SingleKey
    {
        public FOPRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日产油(历史)";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FOPR : SingleKey
    {
        public FOPR(string _name)
            : base(_name)
        {
            this.TitleStr = "日产油";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FGIP : SingleKey
    {
        public FGIP(string _name)
            : base(_name)
        {
            this.TitleStr = "气剩余储量";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FWIP : SingleKey
    {
        public FWIP(string _name)
            : base(_name)
        {
            this.TitleStr = "水剩余储量";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FOIP : SingleKey
    {
        public FOIP(string _name)
            : base(_name)
        {
            this.TitleStr = "油剩余储量";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FLIP : SingleKey
    {
        public FLIP(string _name)
            : base(_name)
        {
            this.TitleStr = "液剩余储量";
        }
    }

    //[KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    //public class FOEW : SingleKey
    //{
    //    public FOEW(string _name)
    //        : base(_name)
    //    {
    //        this.TitleStr = "输出所有常用指标";
    //    }
    //}
    //[KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    //public class FOE : SingleKey
    //{
    //    public FOE(string _name)
    //        : base(_name)
    //    {
    //        this.TitleStr = "输出所有常用指标";
    //    }
    //}
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FLPTH : SingleKey
    {
        public FLPTH(string _name)
            : base(_name)
        {
            this.TitleStr = "累产液（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FOIRH : SingleKey
    {
        public FOIRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日注油（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FGIRH : SingleKey
    {
        public FGIRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日注气（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FLPT : SingleKey
    {
        public FLPT(string _name)
            : base(_name)
        {
            this.TitleStr = "累产液";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FOIR : SingleKey
    {
        public FOIR(string _name)
            : base(_name)
        {
            this.TitleStr = "日注油";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FGIR : SingleKey
    {
        public FGIR(string _name)
            : base(_name)
        {
            this.TitleStr = "日注气";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FLPRH : SingleKey
    {
        public FLPRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日产液（历史）";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FLPR : SingleKey
    {
        public FLPR(string _name)
            : base(_name)
        {
            this.TitleStr = "日产液";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FGPT : SingleKey
    {
        public FGPT(string _name)
            : base(_name)
        {
            this.TitleStr = "累产气";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FGPR : SingleKey
    {
        public FGPR(string _name)
            : base(_name)
        {
            this.TitleStr = "日产气";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FGORH : SingleKey
    {
        public FGORH(string _name)
            : base(_name)
        {
            this.TitleStr = "气油比（历史）";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FGOR : SingleKey
    {
        public FGOR(string _name)
            : base(_name)
        {
            this.TitleStr = "气油比";
        }
    }
    //[KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    //public class EXCEL : SingleKey
    //{
    //    public EXCEL(string _name)
    //        : base(_name)
    //    {
    //        this.TitleStr = "输出所有常用指标";
    //    }
    //}
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class DATE : SingleKey
    {
        public DATE(string _name)
            : base(_name)
        {
            this.TitleStr = "输出日期";
        }
    }
    //[KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    //public class LOTUS : SingleKey
    //{
    //    public LOTUS(string _name)
    //        : base(_name)
    //    {
    //        this.TitleStr = "输出所有常用指标";
    //    }
    //}
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FWPTH : SingleKey
    {
        public FWPTH(string _name)
            : base(_name)
        {
            this.TitleStr = "累产水（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FWPT : SingleKey
    {
        public FWPT(string _name)
            : base(_name)
        {
            this.TitleStr = "累产水";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FWPRH : SingleKey
    {
        public FWPRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日产水（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FGPRH : SingleKey
    {
        public FGPRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日产气（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FWPR : SingleKey
    {
        public FWPR(string _name)
            : base(_name)
        {
            this.TitleStr = "日产水";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FWITH : SingleKey
    {
        public FWITH(string _name)
            : base(_name)
        {
            this.TitleStr = "累注水（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FLITH : SingleKey
    {
        public FLITH(string _name)
            : base(_name)
        {
            this.TitleStr = "累注液（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FWIT : SingleKey
    {
        public FWIT(string _name)
            : base(_name)
        {
            this.TitleStr = "累注水";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FLIT : SingleKey
    {
        public FLIT(string _name)
            : base(_name)
        {
            this.TitleStr = "累注液";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FWIRH : SingleKey
    {
        public FWIRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日注水（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FLIRH : SingleKey
    {
        public FLIRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日注液（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FWIR : SingleKey
    {
        public FWIR(string _name)
            : base(_name)
        {
            this.TitleStr = "日注水";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FLIR : SingleKey
    {
        public FLIR(string _name)
            : base(_name)
        {
            this.TitleStr = "日注液";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FOIT : SingleKey
    {
        public FOIT(string _name)
            : base(_name)
        {
            this.TitleStr = "累注油";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FGIT : SingleKey
    {
        public FGIT(string _name)
            : base(_name)
        {
            this.TitleStr = "累注气";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FWCTH : SingleKey
    {
        public FWCTH(string _name)
            : base(_name)
        {
            this.TitleStr = "含水率（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FWCT : SingleKey
    {
        public FWCT(string _name)
            : base(_name)
        {
            this.TitleStr = "含水率";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FPR : SingleKey
    {
        public FPR(string _name)
            : base(_name)
        {
            this.TitleStr = "地层压力";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FOITH : SingleKey
    {
        public FOITH(string _name)
            : base(_name)
        {
            this.TitleStr = "累注油（历史）";
        }
    }


    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FGITH : SingleKey
    {
        public FGITH(string _name)
            : base(_name)
        {
            this.TitleStr = "累注气（历史）";
        }
    }


    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class FGPTH : SingleKey
    {
        public FGPTH(string _name)
            : base(_name)
        {
            this.TitleStr = "累产气（历史）";
        }
    }

    #endregion

    #region - 选择井关键字 -

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WBHP : CheckListKey
    {
        public WBHP(string _name)
            : base(_name)
        {
            this.TitleStr = "井底流压";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WLPTH : CheckListKey
    {
        public WLPTH(string _name)
            : base(_name)
        {
            this.TitleStr = "累产液（历史）";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WOPR : CheckListKey
    {
        public WOPR(string _name)
            : base(_name)
        {
            this.TitleStr = "日产油";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WOPRH : CheckListKey
    {
        public WOPRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日产油（历史）";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WOPT : CheckListKey
    {
        public WOPT(string _name)
            : base(_name)
        {
            this.TitleStr = "累产油";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WOPTH : CheckListKey
    {
        public WOPTH(string _name)
            : base(_name)
        {
            this.TitleStr = "累产油（历史）";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WWCT : CheckListKey
    {
        public WWCT(string _name)
            : base(_name)
        {
            this.TitleStr = "含水率";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WWCTH : CheckListKey
    {
        public WWCTH(string _name)
            : base(_name)
        {
            this.TitleStr = "含水率（历史）";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WWIR : CheckListKey
    {
        public WWIR(string _name)
            : base(_name)
        {
            this.TitleStr = "日注水";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WGIR : CheckListKey
    {
        public WGIR(string _name)
            : base(_name)
        {
            this.TitleStr = "日注气";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WLIR : CheckListKey
    {
        public WLIR(string _name)
            : base(_name)
        {
            this.TitleStr = "日注液";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WOIT : CheckListKey
    {
        public WOIT(string _name)
            : base(_name)
        {
            this.TitleStr = "累注油";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WWIRH : CheckListKey
    {
        public WWIRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日注水（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WGIRH : CheckListKey
    {
        public WGIRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日注气（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WLIRH : CheckListKey
    {
        public WLIRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日注液（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WOITH : CheckListKey
    {
        public WOITH(string _name)
            : base(_name)
        {
            this.TitleStr = "累注油（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WWIT : CheckListKey
    {
        public WWIT(string _name)
            : base(_name)
        {
            this.TitleStr = "累注水";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WGIT : CheckListKey
    {
        public WGIT(string _name)
            : base(_name)
        {
            this.TitleStr = "累注气";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WLIT : CheckListKey
    {
        public WLIT(string _name)
            : base(_name)
        {
            this.TitleStr = "累注液";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WWITH : CheckListKey
    {
        public WWITH(string _name)
            : base(_name)
        {
            this.TitleStr = "累注水（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WGITH : CheckListKey
    {
        public WGITH(string _name)
            : base(_name)
        {
            this.TitleStr = "累注气（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WLITH : CheckListKey
    {
        public WLITH(string _name)
            : base(_name)
        {
            this.TitleStr = "累注液（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WWPR : CheckListKey
    {
        public WWPR(string _name)
            : base(_name)
        {
            this.TitleStr = "日产水";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WWPRH : CheckListKey
    {
        public WWPRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日产水（历史）";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WWPT : CheckListKey
    {
        public WWPT(string _name)
            : base(_name)
        {
            this.TitleStr = "累产水";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WWPTH : CheckListKey
    {
        public WWPTH(string _name)
            : base(_name)
        {
            this.TitleStr = "累产水（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WBHPH : CheckListKey
    {
        public WBHPH(string _name)
            : base(_name)
        {
            this.TitleStr = "井底流压（历史）";
        }
    }
    //[KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    //public class WBP9 : CheckListKey
    //{
    //    public WBP9(string _name)
    //        : base(_name)
    //    {
    //        this.TitleStr = "输出所有常用指标";
    //    }
    //}
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WGOR : CheckListKey
    {
        public WGOR(string _name)
            : base(_name)
        {
            this.TitleStr = "水气比";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WOIR : CheckListKey
    {
        public WOIR(string _name)
            : base(_name)
        {
            this.TitleStr = "日注油";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WGORH : CheckListKey
    {
        public WGORH(string _name)
            : base(_name)
        {
            this.TitleStr = "气油比（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WGLRH : CheckListKey
    {
        public WGLRH(string _name)
            : base(_name)
        {
            this.TitleStr = "气液比（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WWGRH : CheckListKey
    {
        public WWGRH(string _name)
            : base(_name)
        {
            this.TitleStr = "水气比（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WOIRH : CheckListKey
    {
        public WOIRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日注油（历史）";
        }
    }


    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WGPR : CheckListKey
    {
        public WGPR(string _name)
            : base(_name)
        {
            this.TitleStr = "日产气";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WGPRH : CheckListKey
    {
        public WGPRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日产气（历史）";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WGPT : CheckListKey
    {
        public WGPT(string _name)
            : base(_name)
        {
            this.TitleStr = "累产气";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WLPR : CheckListKey
    {
        public WLPR(string _name)
            : base(_name)
        {
            this.TitleStr = "日产液";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WLPRH : CheckListKey
    {
        public WLPRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日产液（历史）";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WLPT : CheckListKey
    {
        public WLPT(string _name)
            : base(_name)
        {
            this.TitleStr = "累产液";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class WGPTH : CheckListKey
    {
        public WGPTH(string _name)
            : base(_name)
        {
            this.TitleStr = "累产气（历史）";
        }
    }

    #endregion

    #region - 选择井组关键字 -

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GWPR : CheckListKey
    {
        public GWPR(string _name)
            : base(_name)
        {
            this.TitleStr = "日产水";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GGPR : CheckListKey
    {
        public GGPR(string _name)
            : base(_name)
        {
            this.TitleStr = "日产气";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GOPR : CheckListKey
    {
        public GOPR(string _name)
            : base(_name)
        {
            this.TitleStr = "日产油";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GLPR : CheckListKey
    {
        public GLPR(string _name)
            : base(_name)
        {
            this.TitleStr = "日产液";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GOPT : CheckListKey
    {
        public GOPT(string _name)
            : base(_name)
        {
            this.TitleStr = "累产油";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GWPT : CheckListKey
    {
        public GWPT(string _name)
            : base(_name)
        {
            this.TitleStr = "累产水";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GGPT : CheckListKey
    {
        public GGPT(string _name)
            : base(_name)
        {
            this.TitleStr = "累产气";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GLPT : CheckListKey
    {
        public GLPT(string _name)
            : base(_name)
        {
            this.TitleStr = "累产液	";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GOIR : CheckListKey
    {
        public GOIR(string _name)
            : base(_name)
        {
            this.TitleStr = "日注油";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GWIR : CheckListKey
    {
        public GWIR(string _name)
            : base(_name)
        {
            this.TitleStr = "日注水";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GGIR : CheckListKey
    {
        public GGIR(string _name)
            : base(_name)
        {
            this.TitleStr = "日注气";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GLIR : CheckListKey
    {
        public GLIR(string _name)
            : base(_name)
        {
            this.TitleStr = "日注液";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GOIT : CheckListKey
    {
        public GOIT(string _name)
            : base(_name)
        {
            this.TitleStr = "累注油";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GWIT : CheckListKey
    {
        public GWIT(string _name)
            : base(_name)
        {
            this.TitleStr = "累注水";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GGIT : CheckListKey
    {
        public GGIT(string _name)
            : base(_name)
        {
            this.TitleStr = "累注气";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GLIT : CheckListKey
    {
        public GLIT(string _name)
            : base(_name)
        {
            this.TitleStr = "累注液";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GWCT : CheckListKey
    {
        public GWCT(string _name)
            : base(_name)
        {
            this.TitleStr = "含水率";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GGOR : CheckListKey
    {
        public GGOR(string _name)
            : base(_name)
        {
            this.TitleStr = "气油比";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GGLR : CheckListKey
    {
        public GGLR(string _name)
            : base(_name)
        {
            this.TitleStr = "气液比";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GWGR : CheckListKey
    {
        public GWGR(string _name)
            : base(_name)
        {
            this.TitleStr = "水气比";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GOPRH : CheckListKey
    {
        public GOPRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日产油（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GWPRH : CheckListKey
    {
        public GWPRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日产水（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GGPRH : CheckListKey
    {
        public GGPRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日产气（历史）";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GLPRH : CheckListKey
    {
        public GLPRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日产液（历史）";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GOPTH : CheckListKey
    {
        public GOPTH(string _name)
            : base(_name)
        {
            this.TitleStr = "累产油（历史）";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GWPTH : CheckListKey
    {
        public GWPTH(string _name)
            : base(_name)
        {
            this.TitleStr = "累产水（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GGPTH : CheckListKey
    {
        public GGPTH(string _name)
            : base(_name)
        {
            this.TitleStr = "累产气（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GLPTH : CheckListKey
    {
        public GLPTH(string _name)
            : base(_name)
        {
            this.TitleStr = "累产液（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GOIRH : CheckListKey
    {
        public GOIRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日注油（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GWIRH : CheckListKey
    {
        public GWIRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日注水（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GGIRH : CheckListKey
    {
        public GGIRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日注气（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GLIRH : CheckListKey
    {
        public GLIRH(string _name)
            : base(_name)
        {
            this.TitleStr = "日注液（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GOITH : CheckListKey
    {
        public GOITH(string _name)
            : base(_name)
        {
            this.TitleStr = "累注油（历史）";
        }
    }


    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GWITH : CheckListKey
    {
        public GWITH(string _name)
            : base(_name)
        {
            this.TitleStr = "累注水（历史）";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GGITH : CheckListKey
    {
        public GGITH(string _name)
            : base(_name)
        {
            this.TitleStr = "累注气（历史）";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GLITH : CheckListKey
    {
        public GLITH(string _name)
            : base(_name)
        {
            this.TitleStr = "累注液（历史）";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GWCTH : CheckListKey
    {
        public GWCTH(string _name)
            : base(_name)
        {
            this.TitleStr = "含水率（历史）";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GGORH : CheckListKey
    {
        public GGORH(string _name)
            : base(_name)
        {
            this.TitleStr = "气油比（历史）";
        }
    }
    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GGLRH : CheckListKey
    {
        public GGLRH(string _name)
            : base(_name)
        {
            this.TitleStr = "气液比（历史）";
        }
    }

    [KeyAttribute(EclKeyType = EclKeyType.OutPut)]
    public class GWGRH : CheckListKey
    {
        public GWGRH(string _name)
            : base(_name)
        {
            this.TitleStr = "水气比（历史）";
        }
    }

    #endregion

}
