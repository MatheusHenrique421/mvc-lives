using Microsoft.EntityFrameworkCore;

namespace mvc_lives.Models.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        #region Db set's
        public DbSet<Instrutor>? Instrutor { get; set; }
        public DbSet<Inscrito>? Inscrito { get; set; }
        public DbSet<Live>? Live { get; set; }
        public DbSet<Inscricao>? Inscricoes { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                       
            modelBuilder.Entity<Instrutor>()
                .HasKey(i => i.InstrutorID);

            modelBuilder.Entity<Inscrito>()
                .HasKey(i => i.InscritoID);

            modelBuilder.Entity<Live>()
                .HasKey(l => l.LiveID);

            modelBuilder.Entity<Live>()
                .HasOne(l => l.Instrutor)
                .WithMany(i => i.Lives)
                .HasForeignKey(l => l.InstrutorID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Inscricao>()
                .HasKey(i => i.InscricaoID);

            modelBuilder.Entity<Inscricao>()
                .HasOne(i => i.Live)
                .WithMany(l => l.Inscricoes)
                .HasForeignKey(i => i.LiveID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Inscricao>()
                .HasOne(i => i.Inscrito)
                .WithMany(i => i.Inscricoes)
                .HasForeignKey(i => i.InscritoID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Inscricao>()
                .Property(i => i.ValorInscricao)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Live>()
                .Property(l => l.ValorInscricao)
                .HasColumnType("decimal(18, 2)");

            // Adicione esta configuração se não tiver o enum definido          
            modelBuilder.Entity<Inscricao>()
                .Property(i => i.StatusPagamento)
                .HasConversion<int>();

        }

    }
}