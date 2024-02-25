namespace Users.Core.Entities
{
    public class Employee : BaseAuditableEntity<ApplicationUser>
    {
        public string? FullName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        //public string? PhotoUrl { get; set; }
        //public string? PersonnelNumber { get; set; }
        //public Guid? CompanyId { get; set; }
        //public Company Company { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        //public string? MobilePhone { get; set; }
        //public string? WorkPhone { get; set; }
        //public string? Location { get; set; }
        //public DateTime? Birthdate { get; set; }
        //public DateTime? EmploymentDate { get; set; }
        //public bool IsFired { get; set; }
        //public Position? PositionMain => Positions.FirstOrDefault(i => i.IsMain == true);
        //public ICollection<Position> Positions { get; set; }
        //public EmployeeSettings Settings { get; set; }
        public ApplicationUser User { get; set; }
    }
}
