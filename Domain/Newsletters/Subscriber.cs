using System;

namespace Domain.Newsletters
{
    public class Subscriber
    {
        private Subscriber()
        {
        }

        protected Subscriber(Guid id, string email)
        {
            Email = email;
            Id = id;
        }

        public Guid Id { get; }
        public string Email { get; }

        public static Subscriber Subscribe(Guid id, string email)
        {
            if (id == default)
                throw new ArgumentException("Value cannot be equal to default");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Value cannot be empty");

            return new Subscriber(id, email);
        }
    }
}