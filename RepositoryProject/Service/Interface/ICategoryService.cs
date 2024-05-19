using RepositoryProject.Areas.Student.Data;

namespace RepositoryProject.Service.Interface
{
    public interface ICategoryService
    {
        Task<List<Category>> List();
        Task<Category> Add(Category entity);
        Task<Category> Edit(Category entity);
        Task<bool> Delete(int idCategory);
    }
}
