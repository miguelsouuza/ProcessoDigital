using Microsoft.EntityFrameworkCore;
using ProcessoDigital_Server.Data.Model;

namespace ProcessoDigital_Server.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<ProcessoModel> Processos { get; set; }
        public DbSet<AndamentoModel> Andamentos { get; set; }
        public DbSet<LancamentoModel> Lancamentos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração de relacionamento Cliente -> Processos
            modelBuilder.Entity<ClienteModel>()
                .HasMany(c => c.Processos)
                .WithOne(p => p.Cliente)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Restrict); // Evita deletar Cliente se tiver processos ativos (Segurança)

            // Configuração de relacionamento Processo -> Andamentos
            modelBuilder.Entity<ProcessoModel>()
                .HasMany(p => p.Andamentos)
                .WithOne(a => a.Processo)
                .HasForeignKey(a => a.ProcessoId)
                .OnDelete(DeleteBehavior.Cascade); // Deletou processo, somem os andamentos

            modelBuilder.Entity<LancamentoModel>(entity =>
            {
                // Configura a chave primária
                entity.HasKey(l => l.Id);

                entity.Property(l => l.Valor).HasColumnType("decimal(18,2)");
                entity.Property(l => l.Tipo)
                      .HasConversion<string>()
                      .HasMaxLength(50);

                entity.Property(l => l.Status)
                      .HasConversion<string>()
                      .HasMaxLength(50);

                entity.Property(l => l.DataPagamento).IsRequired(false);
            });
        }
    }
}