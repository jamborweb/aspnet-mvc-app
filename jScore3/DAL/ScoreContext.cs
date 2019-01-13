using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using jScore3.Models;

namespace jScore3.DAL
{
    public class ScoreContext : DbContext
    {

        public ScoreContext() : base("DefaultConnection")
        {
        }

        public DbSet<Mecz> Mecze { get; set; }
        public DbSet<Klub> Kluby { get; set; }
        public DbSet<Sezon> Sezony { get; set; }
        public DbSet<Pozycja> Pozycje { get; set; }
        public DbSet<Stadion> Stadiony { get; set; }
        public DbSet<Zawodnik> Zawodnicy { get; set; }
        public DbSet<Komentarz> Komentarze { get; set; }
        public DbSet<Profil> Profile { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Entity<ApplicationUser>().Property(r => r.Id)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<IdentityRole>().Property(r => r.Id)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<Pozycja>().HasRequired(xx => xx.Klub).WithMany(zz => zz.PozycjeGoscia).HasForeignKey(ff=>ff.ZawodnikId).WillCascadeOnDelete(false);


            modelBuilder.Entity<Mecz>().HasRequired(m => m.GospodarzKlub).WithMany(n => n.MeczeGospodarzy).HasForeignKey(k => k.GospodarzKlubId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Mecz>().HasRequired(g => g.GoscKlub).WithMany(x => x.MeczeGosci).HasForeignKey(f => f.GoscKlubId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Zawodnik>().HasRequired(z => z.Klub).WithMany(a => a.Zawodnicy).HasForeignKey(b => b.KlubId).WillCascadeOnDelete(false);

        }

        
    }
}