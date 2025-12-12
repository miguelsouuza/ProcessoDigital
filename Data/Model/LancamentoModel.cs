namespace ProcessoDigital_Server.Data.Model
{
    public class LancamentoModel
    {
        public int Id { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; } // Opcional, data real da liquidação
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public TipoLancamento Tipo { get; set; }
        public StatusLancamento Status { get; set; }

        // Campo calculado (não persistido no banco, mas útil na UI)
        public string Referencia => (Tipo == TipoLancamento.Receita ? "REC" : "DES") + Id.ToString("D4");
        public enum TipoLancamento
        {
            Receita, // Recebimentos (Contas a Receber)
            Despesa  // Despesas (Contas a Pagar)
        }
        public enum StatusLancamento
        {
            Aberto, // Pendente de pagamento/recebimento
            Pago    // Liquidado
        }
    }
}