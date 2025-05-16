using System.Security.Cryptography;
using System.Text;

namespace OnlineRestaurantWpf.Helpers
{
    public static class PasswordHasher
    {
        // IMPORTANT: For production, use a well-vetted library like BCrypt.Net or ASP.NET Core Identity's password hasher.
        // This is a simplified example and lacks features like salting per user, which is crucial for security.
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // In a real scenario, you would generate a unique salt for each user,
                // combine it with the password, and then hash. Store the salt with the hash.
                // byte[] salt = ...; 
                // byte[] passwordBytes = Encoding.UTF8.GetBytes(password + Convert.ToBase64String(salt));
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password); // Simplified: NO SALT
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashedBytes); // Using Base64 for better character compatibility
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // When verifying, you would retrieve the user's salt, combine it with the provided password,
            // hash it, and then compare with the stored hash.
            string newHash = HashPassword(password); // Simplified: NO SALT
            return newHash == hashedPassword;
        }
    }
}