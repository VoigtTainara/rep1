<template>
  <div>
    <titulo texto="Empresas" :btnVoltar="true"/>
    <div>
    <input type="text" placeholder="Razão social da empresa"
    v-model="nome" @keyup.enter="addEmpresa()">
    <button class="btn btnInput" @click="addEmpresa()">Adicionar</button>
    </div>
    <table>
      <thead>
        <th>Código</th>
        <th>Razão social</th>
        <th>Quantidade setores</th>
        <th>Opções</th>
      </thead>
      <tbody v-if="empresas.length">
        <tr v-for="(empresa,index) in empresas" :key="index"> 
          <td>{{empresa.id}}</td> 
          <td>{{empresa.nome}}</td>
          <td>{{empresa.qtdSetores}}</td>
          <td>
            <router-link 
            v-bind:to="'/setores/' + empresa.id" 
            tag="button" class="btn btnPrimary"
            style="cursor: pointer"
            >Ver/Criar setor</router-link>
            <router-link :to="'/empresaDetalhe/'+empresa.id" tag="button" class="btn btnSuccess" style="cursor:pointer">
            Editar</router-link>
            <button class="btn btnDanger" @click="remover(empresa)">Remover</button>
          </td> 
        </tr>
      </tbody>
      <tfoot v-if="!empresas.length">
          Nenhuma empresa encontrada!
      </tfoot> 
    </table>          
  </div>
</template>

<script>
import Titulo from '../Titulo/Titulo';
export default {
  components:{
    Titulo
  },
  data(){
    return{
      titulo: 'Empresa',
      nome: '',
      empresas:[],
      setor:[]
    }
  },
  created(){
    this.$http.get('http://localhost:5000/api/setor')
    .then(res => res.json())
    .then(setores => {
      this.setores = setores;
      this.carregarEmpresas();
    })
  },
  props: {

  },
  methods:{
    addEmpresa(){
      let _empresa={
        nome: this.nome,
      }
         
      if (_empresa.nome){
        this.$http.post('http://localhost:5000/api/empresa', _empresa)
        .then(res => res.json())
        .then(empresaRetornada => {
          this.empresas.push(empresaRetornada);
          this.nome= '';
        })
      }else{
          this.$alert("O nome da empresa não pode ser vazio. Preencha o campo e realize a inclusão da empresa.");
      }
    },
    pegarQtdSetoresPorEmpresa(){
      this.empresas.forEach((empresa,index)=>{
        empresa = {
          id: empresa.id,
          nome: empresa.nome,
          qtdSetores: this.setores.filter(setor =>
           setor.empresa.id == empresa.id
          ).length
        }
        this.empresas[index] = empresa
      });
    },
    carregarEmpresas(){
       this.$http
       .get('http://localhost:5000/api/empresa')
       .then(res => res.json())
       .then(empresa => {
          this.empresas = empresa
          this.pegarQtdSetoresPorEmpresa();
       });
    },
    remover(empresa){
          if (empresa.qtdSetores>0){
            this.$alert("Existem setores cadastrados na empresa! Realize a exclusão dos setores, antes de realizar "
            + "a exclusão da empresa.")
          }else if(empresa.qtdSetores<=0) {
            this.$http.delete(`http://localhost:5000/api/empresa/${empresa.id}`)
            let indice = this.empresas.indexOf(empresa);
            this.empresas.splice(indice, 1);
          }
      }
    }
  }

</script>

<style scoped>
input{
  width: 435px;
  border: 0;
  padding: 20px;
  font-size: 1.3em;
  color: #677f7f;
  display: inline;
}
.btnInput{
  width: 205px;
  display: inline;
  font-size:1.3em;
  border: 0px;
  padding: 20px;
  background-color: rgb(116,115,115);
}
.btnInput:hover{
  padding: 20px;
  margin:0px;
  border:0px;
}
</style>