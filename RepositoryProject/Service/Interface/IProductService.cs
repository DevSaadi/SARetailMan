using RepositoryProject.Areas.Student.Data;

namespace RepositoryProject.Service.Interface
{
    public interface IProductService
    {
        Task<List<Product>> List();
        Task<Product> Add(Product entity);
        Task<Product> Edit(Product entity);
        Task<bool> Delete(int idProduct);
    }
}
