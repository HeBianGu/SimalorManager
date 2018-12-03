using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SimalorManager.RegisterKeys.Eclipse
{
    /// <summary> 网格最下空隙体积 </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MINPV : ConfigerKey
    {
        public MINPV(string _name)
            : base(_name)
        {

        }

        private string zxkxtj="0";
        [CategoryAttribute("网格最小空隙体积"), DescriptionAttribute("网格最小空隙体积"), DisplayName("网格最小空隙体积")]
        public string Zxkxtj
        {
            get { return zxkxtj; }
            set { zxkxtj = value; }
        }

        string formatStr = "{0} /";

        public override string ToString()
        {
            return string.Format(formatStr, zxkxtj);
        }

        public override void WriteKey(System.IO.StreamWriter writer)
        {
            this.Lines.Insert(0, string.Format(formatStr, zxkxtj.ToD()));
            base.WriteKey(writer);
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
                        this.zxkxtj = newStr[0];
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
