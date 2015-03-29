using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{    
    public class VStudent
    {
        public int ID { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

    }
}
