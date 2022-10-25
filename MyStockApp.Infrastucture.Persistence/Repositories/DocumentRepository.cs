using Microsoft.AspNetCore.Http;
using MyStockApp.Core.Application.Interfaces.Repositories;
using MyStockApp.Core.Application.ViewModels.Documents;
using MyStockApp.Core.Application.ViewModels.User;
using MyStockApp.Core.Domain.Entities;
using MyStockApp.Infrastructure.Persistence.Repository;
using MyStockApp.Infrastucture.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStockApp.Infrastucture.Persistence.Repositories
{
    public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
    {
        private readonly ApplicationContext _dbContext;
        private readonly IDocumentRepository _documentRepository;
        private readonly IHttpContextAccessor _httpContextAccess;
        private readonly UserViewModel userViewModel;

        public DocumentRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SaveDocumentViewModel> AddDocument(SaveDocumentViewModel data)
        {
            Document document = new();
            document.ImageUrl = data.ImageUrl;
            document.ProductID = data.ProductId;

            document = await _documentRepository.AddAsync(document);

            SaveDocumentViewModel documentdata = new();

            documentdata.Id = document.Id;
            documentdata.ImageUrl = document.ImageUrl;
            documentdata.ProductId = document.ProductID;

            return documentdata;
        }

    }
}
