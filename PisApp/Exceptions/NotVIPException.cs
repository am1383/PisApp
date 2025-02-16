namespace PisApp.API.Exceptions
{
    public class NotVIPException : ApplicationException
    {
        public NotVIPException()
            :base("User Is Not VIP")
        {
            //
        }
    }
}