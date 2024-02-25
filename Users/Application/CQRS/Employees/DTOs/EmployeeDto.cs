namespace Users.Application.CQRS.Employees.DTOs
{
    public class EmployeeDto : BaseEmployeeDto
    {
        //public List<PositionShortDto> Positions { get; set; }

        public Guid? UserId { get; set; }
    }
}
