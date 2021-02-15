using System.Collections.Generic;

namespace tcc_project_API.Models
{
    public class Empresa
    {

      public int id{get;set;}
      public string cnpj{get;set;}
      public string nome{get;set;}
      public string nomeFantasia{get;set;}
      public string endereco{get;set;}
      public string telefone{get;set;}
      public List<Setor> setores{get;set;}
    }
}