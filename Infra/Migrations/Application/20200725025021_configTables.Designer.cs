﻿// <auto-generated />
using System;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infra.Migrations.Application
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20200725025021_configTables")]
    partial class configTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Infra.Entities.Comentario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCriacao");

                    b.HasKey("Id");

                    b.ToTable("Tb_Comentario");
                });

            modelBuilder.Entity("Infra.Entities.Consulta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataConsulta");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Detalhes");

                    b.Property<string>("Hora");

                    b.Property<int>("MedicoId");

                    b.Property<string>("Nome");

                    b.Property<int>("PessoaId");

                    b.Property<string>("SobreNome");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.ToTable("Tb_Consulta");
                });

            modelBuilder.Entity("Infra.Entities.Especialidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Tb_Especialidade");
                });

            modelBuilder.Entity("Infra.Entities.Horario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Hora");

                    b.HasKey("Id");

                    b.ToTable("Tb_Horario");
                });

            modelBuilder.Entity("Infra.Entities.Medico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<int>("EspecialidadeId");

                    b.Property<string>("Nome");

                    b.Property<int>("PessoaId");

                    b.HasKey("Id");

                    b.ToTable("Tb_Medico");
                });

            modelBuilder.Entity("Infra.Entities.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro");

                    b.Property<string>("CEP");

                    b.Property<string>("CPF");

                    b.Property<string>("Complemento");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<DateTime>("DataNascimento");

                    b.Property<string>("Endereco");

                    b.Property<string>("Nome");

                    b.Property<int>("Numero");

                    b.Property<string>("RG");

                    b.Property<string>("Sobrenome");

                    b.Property<string>("Telefone");

                    b.HasKey("Id");

                    b.ToTable("Tb_Pessoa");
                });
#pragma warning restore 612, 618
        }
    }
}
