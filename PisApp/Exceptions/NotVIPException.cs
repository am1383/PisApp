namespace PisApp.API.Exceptions
{
    public class NotVIPException : ApplicationException
    {
        public NotVIPException()
            :base("کاربر یافت نشد")
        {
            //
        }
    }
}