using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Domain.Repositorios
{ 
    public interface IUserService
    {
         Task<int> AddPessoa(RegisterUserDto registerUser);
         
         Task<bool> PessoaExists(RegisterUserDto registerUser);
         
        
         Task<List<IdentityRole>> ObterPermissoes(string email);
    }
}