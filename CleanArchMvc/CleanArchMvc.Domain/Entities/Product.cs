using CleanArchMvc.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }

        public Product(string name, string description, decimal price, int stock, string image)
        {
            ValidateDomain(name, description, price, stock, image);
        }
        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(id < 0, "Id inválido.");
            Id = id;
            ValidateDomain(name, description, price, stock, image);
        }

        public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            ValidateDomain(name, description, price, stock, image);
            CategoryId = categoryId;
        }

        private void ValidateDomain(string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Nome inválido.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Descrição inválida.");
            DomainExceptionValidation.When(price < 0, "Preço inválido.");
            DomainExceptionValidation.When(stock < 0, "Stock inválido.");
            DomainExceptionValidation.When(image?.Length > 250, "Nome de imagem inválido, maximo 250 caracteres.");
            DomainExceptionValidation.When(name.Length < 3, "Nome do produto invalido, minimo 3 caracteres.");

            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;

        }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
