namespace PisApp.API.Utils
{
    public static class PhoneNumberHelper
    {
        public static string Normalize(string phoneNumber)
        {
            return phoneNumber.Length switch
            {
                10 => $"+98{phoneNumber}",
                11 => phoneNumber.StartsWith("0") ? $"+98{phoneNumber[1..]}" : phoneNumber,
                12 => phoneNumber.StartsWith("98") ? $"+{phoneNumber}" : phoneNumber,
                _ => string.Empty
            };
        }
    }
}
