namespace MRH.Backend.Identity.API.Exceptions
{
    public class NotFoundExceptions : ApplicationException
    {
        public NotFoundExceptions(string name = "")
            :base($"{name} was not found")
        {
            //
        }
    }
}