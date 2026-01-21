using M17E_Ex._1.Pages.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace M17E_Ex._1.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly AppDbContext _context;

        public RegisterModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty] public string Username { get; set; }
        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Password { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (await _context.Clientes.AnyAsync(u => u.Email == Email))
            {
                Message = "Email já registado.";
                return Page();
            }

            _context.Clientes.Add(new Models.Cliente
            {
                Username = Username,
                Email = Email,
                PasswordHash = HashPassword(Password)
            });

            await _context.SaveChangesAsync();
            return RedirectToPage("/Login");
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            return Convert.ToBase64String(
                sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
