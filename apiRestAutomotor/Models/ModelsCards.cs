using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apiRestAutomotor.Models
{
    public class ModelsCards
    {
        public int id { get; set; }

        [Required]
        public int marca_breand { get; set; }

        [Required]
        public string modelo_models { get; set; }
    }
}
