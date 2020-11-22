using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Domain.Repositorios
{ 
    public interface IUserService
    {
         Task<int> AddPessoa(RegisterUserDto registerUser);

         Task<List<IdentityRole>> ObterPermissoes(string email);
    }
}