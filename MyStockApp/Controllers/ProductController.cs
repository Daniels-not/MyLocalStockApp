using Microsoft.AspNetCore.Mvc;
using MyStockApp.Core.Application.Interfaces.Repositories;
using MyStockApp.Core.Application.Interfaces.Services;
using MyStockApp.Core.Application.ViewModels.Documents;
using MyStockApp.Core.Application.ViewModels.Products;
using MyStockApp.Middlewares;

namespace MyStockApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _LocalProductService;
        private readonly ICategoryService _LocalCategoryService;
        private readonly IDocumentService _DocumentService;
        private readonly ValidateUserSession _StockValidateUserSession;

        public ProductController(IProductService productService, ICategoryService categoryService, ValidateUserSession validateUserSession, IDocumentService documentService)
        {
            _LocalProductService = productService;
            _LocalCategoryService = categoryService;
            _StockValidateUserSession = validateUserSession;
            _DocumentService = documentService;
        }
        public async Task<IActionResult> Index()
        {
            if (!_StockValidateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(await _LocalProductService.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            if (!_StockValidateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            SaveProductViewModel data = new();
            data.Categories = await _LocalCategoryService.GetAllViewModel();
            return View("CreateProduct", data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveProductViewModel data)
        {
            if (!_StockValidateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                data.Categories = await _LocalCategoryService.GetAllViewModel();
                return View("SaveProduct", data);
            }

            SaveProductViewModel productdata = await _LocalProductService.Add(data);

            if (productdata.Id != 0 && productdata != null)
            {
                productdata.ImageUrl = UploadFile(data.File, productdata.Id);

                await _LocalProductService.Update(productdata);

                SaveDocumentViewModel document = new();

                document.ImageUrl = productdata.ImageUrl;
                document.ProductId = productdata.Id;
                document.Id = productdata.Id;

                 _DocumentService.AddDocument(document);

                if(data.File2 != null)
                {
                    document.ImageUrl = UploadFile(data.File2, productdata.Id);
                     _DocumentService.AddDocument(document);
                }
                if (data.File3 != null)
                {
                    document.ImageUrl = UploadFile(data.File3, productdata.Id);
                     _DocumentService.AddDocument(document);
                }
                if (data.File4 != null)
                {
                    document.ImageUrl = UploadFile(data.File4, productdata.Id);
                     _DocumentService.AddDocument(document);
                }

            }

            return RedirectToRoute(new { controller = "Product", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_StockValidateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            SaveProductViewModel data = await _LocalProductService.GetByIdSaveViewModel(id);
            data.Categories = await _LocalCategoryService.GetAllViewModel();
            return View("CreateProduct", data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveProductViewModel data)
        {
            if (!_StockValidateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                data.Categories = await _LocalCategoryService.GetAllViewModel();
                return View("SaveProduct", data);
            }

            SaveProductViewModel productdata = await _LocalProductService.GetByIdSaveViewModel(data.Id);
            data.ImageUrl = UploadFile(data.File, data.Id, true, productdata.ImageUrl);
            await _LocalProductService.Update(data);
            return RedirectToRoute(new { controller = "Product", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_StockValidateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _LocalProductService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!_StockValidateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _LocalProductService.Delete(id);

            string basePath = $"/Images/Products/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }

            return RedirectToRoute(new { controller = "Product", action = "Index" });
        }

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Products/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }


    }
}
