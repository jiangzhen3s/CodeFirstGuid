using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SC
    {
        public int ID { get; set; }

        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}
