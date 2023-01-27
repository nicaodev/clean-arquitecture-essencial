using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArchMvc.Domain.Tests
{
    public class CategoryUnitTest1
    {
        [Fact(DisplayName = "Create Category With Valid State")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category(1, "Teste: Nova categoria");
            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Category With Invalid State")]
        public void CreateCategory_WithInvalidParameters_ResultObjectInvalidState()
        {
            Action action = () => new Category(-1, "Teste: Nova categoria");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Id inválido.");
        }
        [Fact]
        public void CreateCategory_ShortName_DomainExceptionShortName()
        {
            Action action = () => new Category(1, "Ni");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Nome muito curto, minimo 3 caracteres.");
        }

        [Fact]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Category(1, "");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Nome Inválido.");
        }

        [Fact]
        public void CreateCategory_WithNullNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Category(1, null);
            action.Should().Throw<DomainExceptionValidation>();
        }
    }
}
