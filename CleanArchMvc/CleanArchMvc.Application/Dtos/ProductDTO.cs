using CleanArchMvc.Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        //[JsonIgnore] // Serve para ignorar a serialização em loop, caso, não use o newtonsoftJson
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}