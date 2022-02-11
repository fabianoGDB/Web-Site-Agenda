using System.ComponentModel.DataAnnotations;

namespace Web.Site.Agenda.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(50, ErrorMessage = "{0} muito longo")]
        public string Nome { get; set; }
        
        [StringLength(50, ErrorMessage = "{0} muito longo")]
        public  string NomeFantasia { get; set; }
        
        [Required(ErrorMessage = "A {0} é obrigatório")]
        public int Capacidade { get; set; }
        
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O {0} é muito longo")]
        [EmailAddress(ErrorMessage = "{0} inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [StringLength(15, ErrorMessage = "{0} muito longo")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório")]
        public TipoTelefone TipoTelefone { get; set; }

        public string Foto { get; set; }
        
    }
}