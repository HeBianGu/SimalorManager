using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPT.Product.SimalorManager
{
    /// <summary> 需要和其他关键字做关联的关键字 </summary>
    public abstract class ConnectKey<T> : ItemsKey<T> where T : Item, new()
    {
        public ConnectKey(string _name)
            : base(_name)
        {

        }

        private ProertyGroup itemGroup = null;
        /// <summary> 项分组 </summary>
        public ProertyGroup ItemGroup
        {
            get { return itemGroup; }
            set { itemGroup = value; }
        }

        /// <summary> 用来记录分组（不需要连接其他项） </summary>
        public class ProertyGroup : List<T>
        {
           
        }

        /// <summary> 用来记录分组  T是本身的组 R是记录连接的组 </summary>
        public class ProertyGroup<R> : List<T> where R : Item, new()
        {
            List<R> connectGroup = new List<R>();

            /// <summary> 记录分组的连接(目前应用水体连接中) </summary>
            public List<R> ConnectGroup
            {
                get { return connectGroup; }
                set { connectGroup = value; }
            }
        }
    }
}
