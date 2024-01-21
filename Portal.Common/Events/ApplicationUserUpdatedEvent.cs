using MediatR;

namespace Portal.Common.Events
{
    public class ApplicationUserUpdatedEvent : INotification
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Guid? EmployeeId { get; set; }
    }
}
