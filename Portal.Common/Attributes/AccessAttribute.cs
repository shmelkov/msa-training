namespace Portal.Common.Attributes
{
    public class AccessAttribute : Attribute
    {
        public string Roles { get; set; }

        public bool CanAccessOwnEntity { get; set; }
    }
}
