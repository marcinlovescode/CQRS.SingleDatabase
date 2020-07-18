using System;
using BuildingBlocks.Commands;

namespace Application.Newsletters.Commands
{
    public class SubscribeToTheNewsletter : ICommand
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}