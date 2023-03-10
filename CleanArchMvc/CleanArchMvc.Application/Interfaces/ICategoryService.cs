using CleanArchMvc.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();

        Task<CategoryDTO> GetByIdAsync(int? id);

        Task Add(CategoryDTO model);

        Task Update(CategoryDTO model);

        Task Remove(int? id);
    }
}