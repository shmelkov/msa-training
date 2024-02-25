namespace Users.Application.CQRS.Employees.DTOs
{
    public class BaseDto: IDto
    {
        public Guid Id { get; set; }
        public Guid? ModifiedById { get; set; }
        public DateTime Modified { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime Created { get; set; }
    }
}
