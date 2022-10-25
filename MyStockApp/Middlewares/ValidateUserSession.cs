using Microsoft.AspNetCore.Http;
using MyStockApp.Core.Application.Helpers;
using MyStockApp.Core.Application.ViewModels.User;

namespace MyStockApp.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccess;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccess = httpContextAccessor;
        }

        public bool HasUser()
        {
            UserViewModel userViewModel = _httpContextAccess.HttpContext.Session.Get<UserViewModel>("user");

            if (userViewModel == null)
            {
                return false;
            }
            return true;
        }
    }
}
