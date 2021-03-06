﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tcc_project_API.Data;

namespace tcc_project_API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210215224323_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("tcc_project_API.Models.Empresa", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("cnpj")
                        .HasColumnType("TEXT");

                    b.Property<string>("endereco")
                        .HasColumnType("TEXT");

                    b.Property<string>("nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("nomeFantasia")
                        .HasColumnType("TEXT");

                    b.Property<string>("telefone")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("empresas");

                    b.HasData(
                        new
                        {
                            id = 1,
                            cnpj = "123456789",
                            endereco = "Rua x",
                            nome = "Empresa da Tai",
                            nomeFantasia = "Empresa da Tai",
                            telefone = "123456"
                        },
                        new
                        {
                            id = 2,
                            cnpj = "2345678910",
                            endereco = "Rua x",
                            nome = "Empresa da Carol",
                            nomeFantasia = "Empresa da Carol",
                            telefone = "234567"
                        });
                });

            modelBuilder.Entity("tcc_project_API.Models.Funcionario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("cargaHoraria")
                        .HasColumnType("TEXT");

                    b.Property<string>("cpf")
                        .HasColumnType("TEXT");

                    b.Property<string>("dataAdmissao")
                        .HasColumnType("TEXT");

                    b.Property<string>("dataDesligamento")
                        .HasColumnType("TEXT");

                    b.Property<string>("dataNasc")
                        .HasColumnType("TEXT");

                    b.Property<string>("endereco")
                        .HasColumnType("TEXT");

                    b.Property<string>("nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("pis")
                        .HasColumnType("TEXT");

                    b.Property<string>("rg")
                        .HasColumnType("TEXT");

                    b.Property<string>("salario")
                        .HasColumnType("TEXT");

                    b.Property<int>("setorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("sobrenome")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("setorId");

                    b.ToTable("funcionarios");

                    b.HasData(
                        new
                        {
                            id = 1,
                            cargaHoraria = "40",
                            cpf = "08953779936",
                            dataAdmissao = "14/06/2020",
                            dataDesligamento = " ",
                            dataNasc = "14/06/2000",
                            endereco = "Rua Arnoldo Hardt",
                            nome = "Tai",
                            pis = "5678912",
                            rg = "53779936",
                            salario = "2100",
                            setorId = 1,
                            sobrenome = "Voigt"
                        },
                        new
                        {
                            id = 2,
                            cargaHoraria = "40",
                            cpf = "08953779936",
                            dataAdmissao = "22/09/2004",
                            dataDesligamento = " ",
                            dataNasc = "14/06/2000",
                            endereco = "Rua Arnoldo Hardt",
                            nome = "Carol",
                            pis = "5678912",
                            rg = "53779936",
                            salario = "2100",
                            setorId = 2,
                            sobrenome = "Voigt"
                        });
                });

            modelBuilder.Entity("tcc_project_API.Models.Setor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("empresaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("nome")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("empresaId");

                    b.ToTable("setores");

                    b.HasData(
                        new
                        {
                            id = 1,
                            empresaId = 1,
                            nome = "Setor da Empresa da Tai"
                        },
                        new
                        {
                            id = 2,
                            empresaId = 2,
                            nome = "Setor da Empresa da Carol"
                        });
                });

            modelBuilder.Entity("tcc_project_API.Models.Funcionario", b =>
                {
                    b.HasOne("tcc_project_API.Models.Setor", "setor")
                        .WithMany("funcionarios")
                        .HasForeignKey("setorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("setor");
                });

            modelBuilder.Entity("tcc_project_API.Models.Setor", b =>
                {
                    b.HasOne("tcc_project_API.Models.Empresa", "empresa")
                        .WithMany("setores")
                        .HasForeignKey("empresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("empresa");
                });

            modelBuilder.Entity("tcc_project_API.Models.Empresa", b =>
                {
                    b.Navigation("setores");
                });

            modelBuilder.Entity("tcc_project_API.Models.Setor", b =>
                {
                    b.Navigation("funcionarios");
                });
#pragma warning restore 612, 618
        }
    }
}
