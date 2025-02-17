namespace PisApp.API.Exceptions
{
    public class UserNotFoundExceptions : ApplicationException
    {
        public UserNotFoundExceptions()
            :base("User Was Not Found")
        {
            //
        }
    }
}