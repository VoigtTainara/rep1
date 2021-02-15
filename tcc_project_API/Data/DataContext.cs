using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using tcc_project_API.Models;

namespace tcc_project_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<Empresa> empresas{get;set;}
        public DbSet<Setor> setores{get;set;}
        public DbSet<Funcionario> funcionarios{get;set;}

        protected override void OnModelCreating(ModelBuilder builder){
            builder.Entity<Empresa>()
                .HasData(
                    new List<Empresa>(){
                        new Empresa(){
                            id=1,
                            cnpj="123456789",
                            nome="Empresa da Tai",
                            nomeFantasia="Empresa da Tai",
                            endereco="Rua x",
                            telefone="123456"
                        },
                        new Empresa(){
                            id=2,
                            cnpj="2345678910",
                            nome="Empresa da Carol",
                            nomeFantasia="Empresa da Carol",
                            endereco="Rua x",
                            telefone="234567"
                        }
                    }
                );
            
            builder.Entity<Setor>()
                .HasData(
                    new List<Setor>(){
                        new Setor(){
                            id=1,
                            nome="Setor da Empresa da Tai",
                            empresaId= 1
                        },
                        new Setor(){
                            id=2,
                            nome="Setor da Empresa da Carol",
                            empresaId=2
                        }
                    }    
                );

            builder.Entity<Funcionario>()
            .HasData(
                new List<Funcionario>(){
                    new Funcionario(){
                        id=1,
                        nome="Tai",
                        sobrenome="Voigt",
                        dataNasc="14/06/2000",
                        cpf="08953779936",
                        rg="53779936",
                        endereco="Rua Arnoldo Hardt",
                        pis= "5678912",
                        cargaHoraria="40",
                        salario="2100",
                        dataAdmissao="14/06/2020",
                        dataDesligamento=" ",
                        setorId=1
                    },
                    new Funcionario(){
                        id=2,
                        nome="Carol",
                        sobrenome="Voigt",
                        dataNasc="14/06/2000",
                        cpf="08953779936",
                        rg="53779936",
                        endereco="Rua Arnoldo Hardt",
                        pis= "5678912",
                        cargaHoraria="40",
                        salario="2100",
                        dataAdmissao="22/09/2004",
                        dataDesligamento=" ",
                        setorId=2
                    }
                }
            );
        }

    }
}