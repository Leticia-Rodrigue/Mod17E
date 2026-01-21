using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace M17E_Ex._1.Pages.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [BindProperty]
        public string Email { get; set; } = "";

        [BindProperty]
        public string Password { get; set; } = "";
    }
}
