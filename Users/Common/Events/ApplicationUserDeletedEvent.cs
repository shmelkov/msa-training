using MediatR;

namespace Users.Common.Events
{
    public class ApplicationUserDeletedEvent : INotification
    {
        public Guid Id { get; set; }
    }
}
