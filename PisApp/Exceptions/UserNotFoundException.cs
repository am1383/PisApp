namespace PisApp.API.Exceptions
{
    public class UserNotFoundExceptions : ApplicationException
    {
        public UserNotFoundExceptions()
            :base("کاربر مورد نظر ویژه نمیباشد")
        {
            //
        }
    }
}