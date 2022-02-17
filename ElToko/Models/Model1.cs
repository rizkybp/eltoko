using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ElToko.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Nasabah> Nasabahs { get; set; }
        public virtual DbSet<TransaksiNasabah> TransaksiNasabahs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nasabah>()
                .HasKey(e=> e.AccountId)
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TransaksiNasabah>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<TransaksiNasabah>()
                .Property(e => e.DebitCreditStatus)
                .IsUnicode(false);

            modelBuilder.Entity<TransaksiNasabah>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);
           
                
        }
    }
}
