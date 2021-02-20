<template>
  <div>
    <titulo :texto="empresaid != undefined ? 'Empresa: '+ empresa.nome : 'Todos os setores'" :btnVoltar="true"/>
    <div v-if="empresaid !=undefined">
    <input type="text" placeholder="Nome do setor"
    v-model="nome" @keyup.enter="addSetor()">
    <button class="btn btnInput" @click="addSetor()">Adicionar</button>
    </div>
    <table>
      <thead>
        <th>Código</th>
        <th>Nome</th>
        <th>Quantidade funcionários</th>
        <th v-if="empresaid == undefined">Empresa</th>
        <th>Opções</th>
      </thead>
      <tbody v-if="setores.length">
        <tr v-for="(setor,index) in setores" :key="index"> 
          <td>{{setor.id}}</td> 
          <td>{{setor.nome}}</td>
          <td>{{setor.qtdFunc}}</td>
          <td v-if="empresaid == undefined">{{setor.empresa.nome}}</td>
          <td>
            <router-link 
            v-bind:to="'/funcionarios/' + setor.id" 
            tag="button" class="btn btnPrimary"
            style="cursor: pointer"
            >Ver/Criar funcionário</router-link>
            <button v-if="empresaid == undefined" class="btn btnDanger" @click="remover(setor)">Remover</button>
            <button v-else class="btn btnDanger1" @click="remover(setor)">Remover</button>
          </td> 
        </tr>
      </tbody>
      <tfoot v-if="!setores.length">
          Nenhum setor encontrado!
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
      empresaid: this.$route.params.empresa_id,
      empresa:{},
      nome: '',
      setores:[],
      funcionarios:[]
    }
  },
  created(){
    if(this.empresaid){
      this.carregarEmpresas();
      this.$http.get(`http://localhost:5000/api/setor/ByEmpresa/${this.empresaid}`)
        .then(res => res.json())
        .then(setores => {
          this.setores = setores;
          this.pegarQtdFuncionariosPorSetor();
        });
    }else{
      this.$http.get('http://localhost:5000/api/setor')
      .then(res => res.json())
      .then(setores => {
        this.setores = setores;
        this.pegarQtdFuncionariosPorSetor();
      })
    }
  },
  props: {

  },
  methods:{
    addSetor(){
      let _setor={
        nome: this.nome,
        empresaId: this.empresa.id,
      }
    
      if (_setor.nome){
        this.$http.post('http://localhost:5000/api/setor', _setor)
        .then(res => res.json())
        .then(setorRetornado => {
          setorRetornado.qtdFunc = !setorRetornado.funcionarios ? 0 : setorRetornado.funcionarios.length;
          this.setores.push(setorRetornado);
          this.nome= '';
        })
      }else{
        this.$alert("O nome do setor não pode ser vazio. Preencha o campo e realize a inclusão do setor.")
      }
    },
    pegarQtdFuncionariosPorSetor(){
      this.setores.forEach((setor,index)=>{
        setor = {
          ...setor,
          qtdFunc: !setor.funcionarios ? 0 : setor.funcionarios.length,
        }
        this.setores[index] = setor
      });
    },
    carregarEmpresas(){
        this.$http
       .get('http://localhost:5000/api/empresa/' + this.empresaid)
       .then(res => res.json())
       .then(empresa => {
          this.empresa = empresa
       });
    },
    carregarSetores(){
       this.$http
       .get('http://localhost:5000/api/setor')
       .then(res => res.json())
       .then(setor => {
          this.setores = setor
          this.pegarQtdFuncionariosPorSetor();
       });
    },
    remover(setor){
          if (setor.qtdFunc>0){
            this.$alert("Existem funcionários cadastrados no setor! Realize a exclusão dos funcionários, ou "+
            "os altere de setor.")
          }else if(setor.qtdFunc<=0) {
            this.$http.delete(`http://localhost:5000/api/setor/${setor.id}`)
            let indice = this.setores.indexOf(setor);
            this.setores.splice(indice, 1);
          }
      },
      // criarVerFuncionarios(setor){
      //       alert("teste "+ setor.id);
           
      // }
    }
  }

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
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
