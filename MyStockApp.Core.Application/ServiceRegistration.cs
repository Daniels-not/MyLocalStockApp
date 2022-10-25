using MyStockApp.Core.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyStockApp.Core.Application.Interfaces.Repositories;
using MyStockApp.Core.Application.Interfaces.Services;

namespace StockApp.Core.Application
{
    /*
        cada capa del onion contiene un service registration donde se registran 
        los diferentes metodos o modelos de la capa en concreto practicamente los metodos de la clase.
     */
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region services
            services.AddTransient<IProductService, ProductService>(); // metodos de para manejar los productos.
            services.AddTransient<ICategoryService, CategoryService>(); // metodos para manejar las categorias.
            services.AddTransient<IUserService, UserService>(); // metodos para manejar los usuarios.
            services.AddTransient<IDocumentService, DocumentService>(); // metodos para manejar los documentos.
            #endregion
        }
    }
}
