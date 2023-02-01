using CleanArchMvc.Domain.Validation;
using System.Collections.Generic;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Category : EntityBase

    {
        public string Name { get; private set; }

        public Category(string name)
        {
            ValidateDomain(name);
        }

        public Category(int id, string name)
        {
            DomainExceptionValidation.When(id < 0, "Id inválido.");
            Id = id;
            ValidateDomain(name);
        }

        public ICollection<Product> Products { get; private set; }

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Nome Inválido.");
            DomainExceptionValidation.When(name.Length < 3, "Nome muito curto, minimo 3 caracteres.");

            Name = name;
        }

        public void Update(string name)
        {
            ValidateDomain(Name);
        }
    }
}