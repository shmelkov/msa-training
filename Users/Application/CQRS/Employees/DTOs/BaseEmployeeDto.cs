namespace Users.Application.CQRS.Employees.DTOs
{
    public class BaseEmployeeDto : BaseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? FullName { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string? MobilePhone { get; set; }
        public DateTime? Birthdate { get; set; }
        public string PersonnelNumber { get; set; }
        public string? Location { get; set; }
        public string? WorkPhone { get; set; }
        public string? PhotoUrl { get; set; }
        public bool IsBirthdayHidden { get; set; }
        public bool IsBirthYearHidden { get; set; }
        public bool IsMobilePhoneHidden { get; set; }
        public Guid? CompanyId { get; set; }
        public string? Company { get; set; }
        public bool IsFired { get; set; }
        public DateTime? EmploymentDate { get; set; }

    }
}
