using System.ComponentModel.DataAnnotations;

namespace API_NOTICIAS.Models
{
    public class Usuario
    {
        //Modelo da entidade Usuário
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }

        [Required]
        public bool Admin { get; set; }

    }
}
