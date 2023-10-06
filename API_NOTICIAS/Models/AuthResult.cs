namespace API_NOTICIAS.Models
{
    public class AuthResult
    {
        //Classe criada apenas para armazenar dois atributos que precisam ser retornados juntos quando o Login é feito.
        public object Token { get; set; }
        public Guid IdUsuario { get; set; }
    }
}
