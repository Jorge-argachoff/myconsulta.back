using System.Threading.Tasks;

namespace Domain.Repositorios
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(string emailDestino, string subject, string message,string confirmationLink);
    }
}