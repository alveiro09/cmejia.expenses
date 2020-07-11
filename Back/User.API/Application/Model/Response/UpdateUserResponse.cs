namespace User.API.Application.Model.Response
{
    public class UpdateUserResponse
    {
        public string UserName { get; set; }
        public bool Updated { get; set; }
        public string Message { get; set; }
    }
}
