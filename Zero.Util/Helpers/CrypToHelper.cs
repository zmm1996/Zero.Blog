using CryptoHelper;
namespace Zero.Util.Helpers
{
    public class CrypToHelper
    {
        public static string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        public static bool VarifyPassword(string hash,string password)
        {
            return Crypto.VerifyHashedPassword(hash,password);
        }
    }
}
