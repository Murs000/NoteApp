using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NoteApp.DataAccess;
using NoteApp.Models;

namespace NoteApp.Services;

public class UserService
{
    private readonly NoteAppDB _context;

    public UserService(NoteAppDB context)
    {
        _context = context;
    }

    public async Task<bool> RegisterUserAsync(RegisterViewModel model)
    {
        var user = new User
        {
            Email = model.Email,
            PasswordHash = HashPassword(model.Password),
            IsEmailConfirmed = false
        };
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        // Send confirmation email logic here

        return true;
    }

    public async Task<User?> AuthenticateUserAsync(LoginViewModel model)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == model.Email);

        if (user == null || !VerifyPassword(model.Password, user.PasswordHash))
            return null;

        return user;
    }

    public async Task<bool> ConfirmEmailAsync(EmailConfirmationViewModel model)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == model.Email);

        if (user == null || !user.IsEmailConfirmed)
            return false;

        user.IsEmailConfirmed = true;
        await _context.SaveChangesAsync();

        return true;
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashBytes);
    }

    private bool VerifyPassword(string password, string hash)
    {
        var passwordHash = HashPassword(password);
        return passwordHash == hash;
    }
}