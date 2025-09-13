using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_project.models
{
    public class Category : BaseModel<int>
    {
        //[Key]
        //public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(600)]
        public string Description { get; set; }
        public List<Product> Products { get; set; }


    }
}
