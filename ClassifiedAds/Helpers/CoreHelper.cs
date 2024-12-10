namespace ClassifiedAds.Helpers
{
    public class CoreHelper
    {
        public static string GetUniqueToken()
        {
            return Path.GetRandomFileName().Replace(".", ""); 
        }
    }
}
