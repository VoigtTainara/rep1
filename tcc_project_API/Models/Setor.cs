using System.Collections.Generic;

namespace tcc_project_API.Models
{
    public class Setor
    {
        public int id{ get; set;}
        public string nome{get; set;}

        public int empresaId{get;set;}

        public Empresa empresa{get;set;}
        public List<Funcionario> funcionarios{get;set;}
    }
}