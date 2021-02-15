namespace tcc_project_API.Models
{
    public class Funcionario
    {
        public int id{ get; set;}
        public string nome{get; set;}
        public string sobrenome{get;set;}
        public string dataNasc{get;set;}
        public string cpf{get;set;}
        public string rg{get;set;}
        public string endereco{get;set;}
        public string pis{get;set;}
        public string cargaHoraria{get;set;}
        public string salario{get;set;}
        public string dataAdmissao{get;set;}
        public string dataDesligamento{get;set;}
        public int setorId{get;set;}

        public Setor setor{get;set;}
    }
}