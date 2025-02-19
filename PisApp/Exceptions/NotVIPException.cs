namespace PisApp.API.Exceptions
{
    public class NotVIPException : ApplicationException
    {
        public NotVIPException()
            :base("کاربر مورد نظر ویژه نمیباشد")
        {
            //
        }
    }
}