using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class ComplexType
    {
        public int ID { get; set; }
        public InnerType Inner { get; set; }

        public class InnerType
        {
            public int ID { get; set; }
            public int A { get; set; }
            public string B { get; set; }
        }
    }
}
