using System.Threading;
using System.Threading.Tasks;
using Application.Mailing;

namespace Infrastructure
{
    public class EmptyMailingService : IEmailService
    {
        public async Task SendEmailWithDiscountCode(string discountCode, CancellationToken cancellationToken = default)
        {
            await Task.Yield();
        }
    }
}