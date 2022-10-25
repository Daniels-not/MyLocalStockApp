using MyStockApp.Core.Application.ViewModels.Documents;
using MyStockApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStockApp.Core.Application.Interfaces.Repositories
{
    public interface IDocumentRepository : IGenericRepository<Document>
    {
        Task<SaveDocumentViewModel> AddDocument(SaveDocumentViewModel document);
    }
}
