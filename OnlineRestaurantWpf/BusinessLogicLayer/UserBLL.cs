using OnlineRestaurantWpf.Data;
using OnlineRestaurantWpf.Models;
using OnlineRestaurantWpf.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace OnlineRestaurantWpf.BusinessLogicLayer
{
    public class UserBLL
    {
        private readonly Func<RestaurantDbContext> _dbContextFactory;

        public UserBLL(Func<RestaurantDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<User?> AuthenticateUserAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                Debug.WriteLine("[UserBLL.AuthenticateUserAsync] Email or password was null or whitespace.");
                return null;
            }

            Debug.WriteLine($"[UserBLL.AuthenticateUserAsync] Attempting to authenticate user: {email}");
            User? user = null;
            try
            {
                using var context = _dbContextFactory();
                Debug.WriteLine("[UserBLL.AuthenticateUserAsync] DbContext created. Querying for user...");

                user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

                Debug.WriteLine(user == null
                    ? $"[UserBLL.AuthenticateUserAsync] No user found with email: {email}"
                    : $"[UserBLL.AuthenticateUserAsync] User found: {user.Email}, ID: {user.Id}");

                if (user == null)
                {
                    return null; // User not found
                }

                bool isPasswordValid = PasswordHasher.VerifyPassword(password, user.PasswordHash);
                Debug.WriteLine($"[UserBLL.AuthenticateUserAsync] Password verification result for {email}: {isPasswordValid}");

                if (!isPasswordValid)
                {
                    return null; // Password incorrect
                }

                Debug.WriteLine($"[UserBLL.AuthenticateUserAsync] Authentication successful for {email}");
                return user; // Authentication successful
            }
            catch (Exception ex)
            {
                // Log the full exception details
                Debug.WriteLine($"[UserBLL.AuthenticateUserAsync] EXCEPTION: {ex.ToString()}");
                return null;
            }
        }

        public async Task<User> RegisterUserAsync(User newUser, string password)
        {
            if (newUser == null) throw new ArgumentNullException(nameof(newUser));
            if (string.IsNullOrWhiteSpace(newUser.Email)) throw new ArgumentException("Email cannot be empty.", nameof(newUser.Email));
            if (string.IsNullOrWhiteSpace(newUser.FirstName)) throw new ArgumentException("First name cannot be empty.", nameof(newUser.FirstName));
            if (string.IsNullOrWhiteSpace(newUser.LastName)) throw new ArgumentException("Last name cannot be empty.", nameof(newUser.LastName));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Password cannot be empty.", nameof(password));

            using var context = _dbContextFactory();
            bool emailExists = await context.Users.AnyAsync(u => u.Email == newUser.Email);
            if (emailExists)
            {
                throw new InvalidOperationException($"User with email '{newUser.Email}' already exists.");
            }

            newUser.PasswordHash = PasswordHasher.HashPassword(password);
            newUser.Role = string.IsNullOrWhiteSpace(newUser.Role) ? "Client" : newUser.Role;

            context.Users.Add(newUser);
            await context.SaveChangesAsync();
            return newUser;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            using var context = _dbContextFactory();
            return await context.Users.FindAsync(userId);
        }
    }
}