using OnlineRestaurantWpf.Data;
using OnlineRestaurantWpf.Models;
using OnlineRestaurantWpf.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Diagnostics; // For Debug.WriteLine

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

                // Test database connection with a simpler query first
                // bool canConnect = await context.Database.CanConnectAsync();
                // Debug.WriteLine($"[UserBLL.AuthenticateUserAsync] Can connect to database: {canConnect}");
                // if (!canConnect)
                // {
                //     Debug.WriteLine("[UserBLL.AuthenticateUserAsync] Cannot connect to the database. Check connection string and SQL Server instance.");
                //     return null;
                // }

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
                // Optionally, rethrow or handle more gracefully depending on application requirements
                // For now, returning null to indicate failure to the ViewModel
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

        public async Task<User> UpdateUserProfileAsync(User userToUpdate)
        {
            if (userToUpdate == null) throw new ArgumentNullException(nameof(userToUpdate));

            using var context = _dbContextFactory();
            var existingUser = await context.Users.FindAsync(userToUpdate.Id);

            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {userToUpdate.Id} not found.");
            }

            if (existingUser.Email != userToUpdate.Email && await context.Users.AnyAsync(u => u.Email == userToUpdate.Email && u.Id != userToUpdate.Id))
            {
                throw new InvalidOperationException($"Another user with email '{userToUpdate.Email}' already exists.");
            }

            existingUser.FirstName = userToUpdate.FirstName;
            existingUser.LastName = userToUpdate.LastName;
            existingUser.Email = userToUpdate.Email;
            existingUser.Phone = userToUpdate.Phone;
            existingUser.Address = userToUpdate.Address;

            await context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(oldPassword)) throw new ArgumentException("Old password cannot be empty.", nameof(oldPassword));
            if (string.IsNullOrWhiteSpace(newPassword)) throw new ArgumentException("New password cannot be empty.", nameof(newPassword));

            using var context = _dbContextFactory();
            var user = await context.Users.FindAsync(userId);

            if (user == null || !PasswordHasher.VerifyPassword(oldPassword, user.PasswordHash))
            {
                return false;
            }

            user.PasswordHash = PasswordHasher.HashPassword(newPassword);
            await context.SaveChangesAsync();
            return true;
        }
    }
}