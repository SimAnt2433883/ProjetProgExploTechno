using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Prog3A25_AntoineTommy_Blazor.Models;

namespace Prog3A25_AntoineTommy_Blazor.Data;

public partial class Prog3A25AntoineTommyProdContext : DbContext
{
    public Prog3A25AntoineTommyProdContext()
    {
    }

    public Prog3A25AntoineTommyProdContext(DbContextOptions<Prog3A25AntoineTommyProdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adresse> Adresses { get; set; }

    public virtual DbSet<Donnee> Donnees { get; set; }

    public virtual DbSet<Plante> Plantes { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Reponse> Reponses { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    public virtual DbSet<VueInfoUtilisateur> VueInfoUtilisateurs { get; set; }

    public virtual DbSet<VueInteractionUtilisateur> VueInteractionUtilisateurs { get; set; }

    public virtual DbSet<VueQuestionUtilisateur> VueQuestionUtilisateurs { get; set; }

    public virtual DbSet<VueReponseUtilisateur> VueReponseUtilisateurs { get; set; }

    public virtual DbSet<Wiki> Wikis { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adresse>(entity =>
        {
            entity.HasKey(e => e.NoAdresse).HasName("PK__Adresse__C5F10324A88963CC");
        });

        modelBuilder.Entity<Donnee>(entity =>
        {
            entity.HasKey(e => e.NoDonnee).HasName("PK__Donnee__DC6E7FA1B58E2843");

            entity.Property(e => e.DateHeure).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.NoPlanteNavigation).WithMany(p => p.Donnees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Donnee_Plante");
        });

        modelBuilder.Entity<Plante>(entity =>
        {
            entity.HasKey(e => e.NoPlante).HasName("PK__Plante__D6215A355A4C3630");

            entity.ToTable("Plante", tb => tb.HasTrigger("insert_wiki"));

            entity.HasOne(d => d.NoWikiNavigation).WithMany(p => p.Plantes).HasConstraintName("FK_Plante_Wiki");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.NoQuestion).HasName("PK__Question__ADD49C5042FB1CD3");

            entity.HasOne(d => d.NoUtilisateurNavigation).WithMany(p => p.Questions).HasConstraintName("FK_Question_Utilisateur");
        });

        modelBuilder.Entity<Reponse>(entity =>
        {
            entity.HasKey(e => e.NoReponse).HasName("PK__Reponse__FBCF9BA83C0E9DD3");

            entity.HasOne(d => d.NoQuestionNavigation).WithMany(p => p.Reponses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reponse_Question");

            entity.HasOne(d => d.NoUtilisateurNavigation).WithMany(p => p.Reponses).HasConstraintName("FK_Reponse_Utilisateur");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.NoUtilisateur).HasName("PK__Utilisat__CB66E30BE1239758");

            entity.Property(e => e.MotPasseHash).IsFixedLength();

            entity.HasOne(d => d.NoAdresseNavigation).WithMany(p => p.Utilisateurs).HasConstraintName("FK_Utilisateur_Adresse");

            entity.HasOne(d => d.NoPlanteNavigation).WithMany(p => p.Utilisateurs).HasConstraintName("FK_Utilisateur_Plante");

            entity.HasMany(d => d.NoQuestions).WithMany(p => p.NoUtilisateurs)
                .UsingEntity<Dictionary<string, object>>(
                    "LikesQuestion",
                    r => r.HasOne<Question>().WithMany()
                        .HasForeignKey("NoQuestion")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LikesQuestion_Question"),
                    l => l.HasOne<Utilisateur>().WithMany()
                        .HasForeignKey("NoUtilisateur")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LikesQuestion_Utilisateur"),
                    j =>
                    {
                        j.HasKey("NoUtilisateur", "NoQuestion").HasName("PK__LikesQue__D1BBAACE4E91CA47");
                        j.ToTable("LikesQuestion");
                        j.IndexerProperty<int>("NoUtilisateur").HasColumnName("noUtilisateur");
                        j.IndexerProperty<int>("NoQuestion").HasColumnName("noQuestion");
                    });

            entity.HasMany(d => d.NoReponses).WithMany(p => p.NoUtilisateurs)
                .UsingEntity<Dictionary<string, object>>(
                    "LikesReponse",
                    r => r.HasOne<Reponse>().WithMany()
                        .HasForeignKey("NoReponse")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LikesReponse_Reponse"),
                    l => l.HasOne<Utilisateur>().WithMany()
                        .HasForeignKey("NoUtilisateur")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LikesReponse_Utilisateur"),
                    j =>
                    {
                        j.HasKey("NoUtilisateur", "NoReponse").HasName("PK__LikesRep__54DA1AB1347EF210");
                        j.ToTable("LikesReponse");
                        j.IndexerProperty<int>("NoUtilisateur").HasColumnName("noUtilisateur");
                        j.IndexerProperty<int>("NoReponse").HasColumnName("noReponse");
                    });
        });

        modelBuilder.Entity<VueInfoUtilisateur>(entity =>
        {
            entity.ToView("VueInfoUtilisateur");
        });

        modelBuilder.Entity<VueInteractionUtilisateur>(entity =>
        {
            entity.ToView("VueInteractionUtilisateur");
        });

        modelBuilder.Entity<VueQuestionUtilisateur>(entity =>
        {
            entity.ToView("VueQuestionUtilisateur");
        });

        modelBuilder.Entity<VueReponseUtilisateur>(entity =>
        {
            entity.ToView("VueReponseUtilisateur");
        });

        modelBuilder.Entity<Wiki>(entity =>
        {
            entity.HasKey(e => e.NoWiki).HasName("PK__Wiki__121F63F92EFF2054");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
