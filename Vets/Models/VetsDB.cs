using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Vets.Models
{
    public class VetsDB : DbContext
    {
        //representar a Base de Dados descrevendo as tabelas que lá estão contidas

        //representar o 'construtor' desta classe
        //identifica onde se encontra a base de dados
        public VetsDB() : base("VetsDBConnection")
        { }

            //descrever as tabelas que estão na base de dados
            public virtual DbSet<Donos> Donos { get; set; }
            
            public virtual DbSet<Animais> Animais { get; set; }

            public virtual DbSet<Veterinarios> Veterinarios { get; set; }

            public virtual DbSet<Consultas> Consultas { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // não podemos usar a chave seguinte, nesta geração de tabelas
            // por causa das tabelas do Identity (gestão de utilizadores)
            // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); 
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }


}

