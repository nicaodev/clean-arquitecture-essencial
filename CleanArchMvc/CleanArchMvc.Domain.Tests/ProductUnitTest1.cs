using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact]
        public void CreateProduct_WithValidParameters_ResultObjetValidState()
        {
            Action action = () => new Product(1, "Produto Novo", "Descrição", 9.99m, 99, "Imagem do Produto");
            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Produto Novo", "Descrição", 9.99m, 99, "Imagem do Produto");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Id inválido.");
        }

        [Fact]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Pr", "Descrição", 9.99m, 99, "Imagem do Produto");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Nome do produto invalido, minimo 3 caracteres.");
        }

        [Fact]
        public void CreateProduct_NullNameValue_DomainExceptionNullValue()
        {
            Action action = () => new Product(1, null, "Descrição", 9.99m, 99, "Imagem do Produto");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Nome inválido.");
        }
    }
}