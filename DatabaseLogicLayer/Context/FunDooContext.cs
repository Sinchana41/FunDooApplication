using Microsoft.EntityFrameworkCore;
using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLogicLayer.Context
{
    public class FunDooContext : DbContext
    {
        public FunDooContext(DbContextOptions<FunDooContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Label1> Labels { get; set; }
        public DbSet<Collaborator> Collaborators { get; set; }

        public DbSet<NoteLabel> NoteLabels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //User -> Notes (1 : N)
            modelBuilder.Entity<Note>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //User -> Labels (1:N)

            modelBuilder.Entity<Label1>()
                .HasOne(l => l.User)
                .WithMany(u => u.Labels)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //User -> Collaborators (1 : N)

            modelBuilder.Entity<Collaborator>()
                .HasOne(l => l.User)
                .WithMany(u => u.Collaborators)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            //Note -> Collaborators (1 : N)

            modelBuilder.Entity<Collaborator>()
                .HasOne(c => c.Note)
                .WithMany(n =>  n.Collaborators)
                .HasForeignKey(c => c.NoteId)
                .OnDelete(DeleteBehavior.Cascade);

            //Notes <-> (M : N)

            modelBuilder.Entity<NoteLabel>()
                .HasKey(nl => new { nl.NotesId });
            modelBuilder.Entity<NoteLabel>()
                .HasOne(nl => nl.Note)
                .WithMany(n => n.NoteLabels)
                .HasForeignKey(nl => nl.NotesId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<NoteLabel>()
                .HasOne(nl => nl.Label)
                .WithMany(l => l.NoteLabels)
                .HasForeignKey(nl => nl.LabelId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
