using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apiRestAutomotor.Models
{
    public class Cards
    {
        public int id { get; set; }

        [Required]
        public string marca_brand { get; set; }
    }
}
