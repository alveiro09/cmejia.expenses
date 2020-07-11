namespace User.API.Application.Model.Response
{
    public class AddUserResponse
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool Added { get; set; }
        public string Message { get; set; }
    }
}
