using MediatR;

namespace Portal.Common.Events
{
    public class EmployeeUpdatedEvent : INotification
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public Guid? PositionId { get; set; }

        public string Position { get; set; }

        public Guid? DepartmentId { get; set; }

        public string Department { get; set; }

        public string PhotoUrl { get; set; }

        public string Email { get; set; }
    }
}
