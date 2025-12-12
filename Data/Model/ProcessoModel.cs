using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProcessoDigital_Server.Data.Model
{
    // Enum para o status do processo
    public enum StatusProcesso { Ativo, Suspenso, Arquivado, EmRecurso }
    public class ProcessoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Número do processo é obrigatório.")]
        [StringLength(25, ErrorMessage = "Número CNJ geralmente tem 20 caracteres.")]
        public string NumeroCNJ { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Acao { get; set; } = string.Empty; // Ex: Ação de Cobrança, Divórcio

        [StringLength(50)]
        public string Tribunal { get; set; } = string.Empty; // Ex: TJSP, TRT-2

        [StringLength(50)]
        public string Vara { get; set; } = string.Empty; // Ex: 1ª Vara Cível

        [StringLength(100)]
        public string? Juiz { get; set; }

        [StringLength(100)]
        public string? ParteContraria { get; set; } // Quem o cliente está processando ou sendo processado

        public StatusProcesso Status { get; set; } = StatusProcesso.Ativo;

        public DateTime DataDistribuicao { get; set; } = DateTime.Now;

        public decimal? ValorCausa { get; set; }

        // Chave Estrangeira
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual ClienteModel? Cliente { get; set; }

        // Relacionamento: Um processo tem muitos andamentos
        public virtual ICollection<AndamentoModel> Andamentos { get; set; } = new List<AndamentoModel>();
    }      
}
