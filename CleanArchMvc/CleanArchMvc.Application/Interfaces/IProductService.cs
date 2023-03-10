using CleanArchMvc.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();

        Task<ProductDTO> GetByIdAsync(int? id);

        //Task<ProductDTO> GetProductCategory(int? id);
        Task Add(ProductDTO productDto);

        Task Update(ProductDTO productDto);

        Task Remove(int? id);
    }
}