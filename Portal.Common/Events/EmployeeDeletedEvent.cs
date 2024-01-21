using MediatR;

namespace Portal.Common.Events
{
    public class EmployeeDeletedEvent : INotification
    {
       public Guid Id { get; set; }
    }
}
