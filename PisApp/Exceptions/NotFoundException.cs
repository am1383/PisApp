namespace PisApp.API.Exceptions
{
    public class NotFoundExceptions : ApplicationException
    {
        public NotFoundExceptions()
            :base("User Was Not Found")
        {
            //
        }
    }
}