using M17E_Ex._1.Pages.Data;
using M17E_Ex._1.Pages.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace M17E_Ex._1.Pages.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        private string Hash(string password)
        {
            using var sha = SHA256.Create();
            return Convert.ToBase64String(
                sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        public async Task<bool> Register(string username, string email, string password)
        {
            if (await _context.Clientes.AnyAsync(u => u.Email == email))
                return false;

            _context.Clientes.Add(new Cliente
            {
                Username = username,
                Email = email,
                PasswordHash = Hash(password)
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Cliente?> Login(string email, string password)
        {
            var hash = Hash(password);
            return await _context.Clientes
                .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == hash);
        }
    }
}