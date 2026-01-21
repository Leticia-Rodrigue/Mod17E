using M17E_Ex._1.Pages.Data;
using M17E_Ex._1.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;

namespace M17E_Ex._1.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        // Garante que estas propriedades têm o [BindProperty]
        [BindProperty]
        public string Email { get; set; } = "";

        [BindProperty]
        public string Password { get; set; } = "";

        public string ErrorMessage { get; set; } = "";

        public async Task<IActionResult> OnPostAsync()
        {
            // O ERRO ACONTECIA AQUI: Adicionamos esta proteção
            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Email))
            {
                ErrorMessage = "Credenciais inválidas.";
                return Page();
            }

            string passwordHash = HashPassword(Password);

            var user = await _context.Clientes
                .FirstOrDefaultAsync(u => u.Email == Email && u.Password == passwordHash);

            if (user == null)
            {
                ErrorMessage = "Email ou password incorretos.";
                return Page();
            }

            // Sessão
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username ?? "Utilizador");

            return RedirectToPage("/Index");
        }

        private string HashPassword(string password)
        {
            // PROTEÇÃO FINAL: Se por acaso a password for null, isto evita o erro 'Parameter s'
            if (string.IsNullOrEmpty(password)) return "";

            using var sha = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha.ComputeHash(inputBytes);
            return Convert.ToBase64String(hashBytes);
        }
}

