using System.Threading;
using System.Threading.Tasks;
using Application.Discounts.Repositories;
using Application.Mailing;
using Application.Newsletters.Commands;
using Application.Newsletters.Repositories;
using BuildingBlocks.Commands;
using Domain.Discounts;
using Domain.Newsletters;

namespace Application.Newsletters.CommandHandlers
{
    public class SubscribeToTheNewsletterHandler : ICommandHandler<SubscribeToTheNewsletter>
    {
        private readonly IDiscountCodeGenerator _discountCodeGenerator;
        private readonly IDiscountRepository _discountRepository;
        private readonly IEmailService _emailService;
        private readonly IIdentityProvider _identityProvider;
        private readonly INewsletterRepository _newsletterRepository;

        public SubscribeToTheNewsletterHandler(
            IEmailService emailService,
            IIdentityProvider identityProvider,
            INewsletterRepository newsletterRepository,
            IDiscountRepository discountRepository,
            IDiscountCodeGenerator discountCodeGenerator)
        {
            _emailService = emailService;
            _identityProvider = identityProvider;
            _newsletterRepository = newsletterRepository;
            _discountRepository = discountRepository;
            _discountCodeGenerator = discountCodeGenerator;
        }

        public async Task HandleAsync(SubscribeToTheNewsletter command, CancellationToken cancellationToken = default)
        {
            var subscriber = Subscriber.Subscribe(command.Id, command.Email);
            var discount = _discountCodeGenerator.GenerateCodeForSubscriber(_identityProvider.Next(), subscriber.Id);
            await _newsletterRepository.Add(subscriber, cancellationToken);
            await _discountRepository.Add(discount, cancellationToken);
            await _emailService.SendEmailWithDiscountCode(discount.GetCode(), cancellationToken);
        }
    }
}