using MediatR;

namespace Portal.Common.Events
{
    public class ApplicationUserDeletedEvent : INotification
    {
        public Guid Id { get; set; }
    }
}
