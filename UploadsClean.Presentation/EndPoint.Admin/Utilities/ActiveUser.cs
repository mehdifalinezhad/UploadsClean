namespace EndPoint.Admin.Utilities
{
    public class ActiveUser
    {
        public string UserName { get; set; }
        public string Id { get; set; }
        public long MyKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserFullName { get; set; }
        public string Mobile { get; set; } = string.Empty;
        public string UserIpAddress { get; set; }
    }
}
