using System.Threading;
using System.Threading.Tasks;

namespace Application.Mailing
{
    public interface IEmailService
    {
        Task SendEmailWithDiscountCode(string discountCode, CancellationToken cancellationToken = default);
    }
}