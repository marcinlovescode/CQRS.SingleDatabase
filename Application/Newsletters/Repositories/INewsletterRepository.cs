using System.Threading;
using System.Threading.Tasks;
using Domain.Newsletters;

namespace Application.Newsletters.Repositories
{
    public interface INewsletterRepository
    {
        Task Add(Subscriber subscriber, CancellationToken cancellationToken = default);
    }
}