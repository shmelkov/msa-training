using MediatR;

namespace Users.Common.Events
{
    public class EmployeeDeletedEvent : INotification
    {
       public Guid Id { get; set; }
    }
}
