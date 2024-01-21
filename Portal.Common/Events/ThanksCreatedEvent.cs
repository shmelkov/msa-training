using MediatR;

namespace Portal.Common.Events
{
    public class ThanksCreatedEvent : INotification
    {
        public string Text { get; set; }

        public Guid RecipientId { get; set; }

        public Guid SenderId { get; set; }
    }
}
