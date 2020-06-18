namespace User.API.Application.Model.Request
{
    public class AddUserRequest
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string IdentityNumber { get; set; }
        public int IdentityDocument { get; set; }
        public string Email { get; set; }
        public string SecondEmail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
    }
}
