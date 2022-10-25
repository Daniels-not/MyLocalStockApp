using MyStockApp.Core.Application.ViewModels.Documents;
using MyStockApp.Core.Application.ViewModels.Products;
using MyStockApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStockApp.Core.Application.Interfaces.Services
{
    public interface IDocumentService : IGenericService<SaveDocumentViewModel, Document>
    {
        Task<SaveDocumentViewModel> AddDocument(SaveDocumentViewModel document);
    }
}
