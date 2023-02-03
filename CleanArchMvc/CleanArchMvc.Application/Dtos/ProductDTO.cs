using CleanArchMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Dtos
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The name is required.")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "The Price is required.")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        [MaxLength(250)]
        [DisplayName("Product Image")]
        public string Image { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

    }
}
