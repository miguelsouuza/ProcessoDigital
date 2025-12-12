using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ProcessoDigital_Server.Data.Model
{
    // Enum para diferenciar PF de PJ
    public enum TipoPessoa { Fisica, Juridica }
    public class ClienteModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "CPF ou CNPJ é obrigatório.")]
        [StringLength(18)] // Suficiente para formatado (00.000.000/0000-00)
        public string Documento { get; set; } = string.Empty;

        public TipoPessoa Tipo { get; set; } = TipoPessoa.Fisica;

        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string? Email { get; set; }

        public string? Telefone { get; set; }

        public string? Endereco { get; set; }

        // Relacionamento: Um cliente tem muitos processos
        public virtual ICollection<ProcessoModel> Processos { get; set; } = new List<ProcessoModel>();
    }
}
