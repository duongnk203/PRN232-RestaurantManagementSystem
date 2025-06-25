using BCrypt.Net;

namespace Restaurant.Helpers
{
    public class SecurePasswordHasher
    {
        // Mã hóa mật khẩu
        public static string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Xác minh mật khẩu
        public static bool Verify(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
