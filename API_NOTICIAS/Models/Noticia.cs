using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace API_NOTICIAS.Models
{
    public class Noticia
    {
        //Modelo da entidade Notícia
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Categoria { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Texto { get; set; }
        public List<string> Comentarios { get; set; }

    }
}
