using MyStockApp.Core.Application.Interfaces.Repositories;
using MyStockApp.Core.Application.Interfaces.Services;
using MyStockApp.Core.Application.ViewModels.User;
using MyStockApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStockApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> Login(LoginViewModel data)
        {
            UserViewModel userData = new();
            User user = await _userRepository.LoginAsync(data);

            if (user == null)
            {
                return null;
            }

            userData.Id = user.Id;
            userData.Name = user.Name;
            userData.LastName = user.LastName;
            userData.Username = user.Username;
            userData.Phone = user.Phone;
            userData.Password = user.Password;
            userData.Email = user.Email;

            return userData;
        }

        public async Task Update(SaveUserViewModel data)
        {
            User user = await _userRepository.GetByIdAsync(data.Id);
            user.Id = data.Id;
            user.Name = data.Name;
            user.LastName = data.LastName;
            user.Username = data.Username;
            user.Password = data.Password;
            user.Phone = data.Phone;
            user.Email = data.Email;

            await _userRepository.UpdateAsync(user);
        }

        public async Task<SaveUserViewModel> Add(SaveUserViewModel data)
        {
            User user = new();
            user.Name = data.Name;
            user.LastName = data.LastName;
            user.Username = data.Username;
            user.Password = data.Password;
            user.Phone = data.Phone;
            user.Email = data.Email;

            user = await _userRepository.AddAsync(user);

            SaveUserViewModel userData = new();

            userData.Id = user.Id;
            userData.Name = user.Name;
            userData.LastName = user.LastName;
            userData.Phone = user.Phone;
            userData.Email = user.Email;
            userData.Username = user.Username;
            userData.Password = user.Password;

            return userData;
        }

        public async Task Delete(int id)
        {
            var product = await _userRepository.GetByIdAsync(id);
            await _userRepository.DeleteAsync(product);
        }

        public async Task<SaveUserViewModel> GetByIdSaveViewModel(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            SaveUserViewModel data = new();
            data.Id = user.Id;
            data.Name = user.Name;
            data.LastName = user.LastName;
            data.Username = user.Username;
            data.Password = user.Password;
            data.Phone = user.Phone;
            data.Email = user.Email;
            return data;
        }

        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            var userList = await _userRepository.GetAllWithIncludeAsync(new List<string> { "Products" });

            return userList.Select(user => new UserViewModel
            {
                Name = user.Name,
                LastName = user.LastName,
                Username = user.Username,
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Phone = user.Phone
            }).ToList();
        }

    }
}
