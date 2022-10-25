using Microsoft.AspNetCore.Http;
using MyStockApp.Core.Application.Helpers;
using MyStockApp.Core.Application.Interfaces.Repositories;
using MyStockApp.Core.Application.Interfaces.Services;
using MyStockApp.Core.Application.ViewModels.Documents;
using MyStockApp.Core.Application.ViewModels.Products;
using MyStockApp.Core.Application.ViewModels.User;
using MyStockApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStockApp.Core.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IHttpContextAccessor _httpContextAccess;
        private readonly UserViewModel userViewModel;

        public DocumentService(IDocumentRepository documentRepository, IHttpContextAccessor httpContextAccessor)
        {
            _documentRepository = documentRepository;
            _httpContextAccess = httpContextAccessor;
            userViewModel = _httpContextAccess.HttpContext.Session.Get<UserViewModel>("user");
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

        Task<SaveDocumentViewModel> IGenericService<SaveDocumentViewModel, Document>.Add(SaveDocumentViewModel vm)
        {
            throw new NotImplementedException();
        }

        Task IGenericService<SaveDocumentViewModel, Document>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<Document>> IGenericService<SaveDocumentViewModel, Document>.GetAllViewModel()
        {
            throw new NotImplementedException();
        }

        Task<SaveDocumentViewModel> IGenericService<SaveDocumentViewModel, Document>.GetByIdSaveViewModel(int id)
        {
            throw new NotImplementedException();
        }

        Task IGenericService<SaveDocumentViewModel, Document>.Update(SaveDocumentViewModel vm)
        {
            throw new NotImplementedException();
        }
    }


}
