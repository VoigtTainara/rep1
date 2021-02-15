<template>
  <div>
    <titulo :texto="setorid != undefined ? 'Setor: '+ setor.nome : 'Todos os funcionários'" :btnVoltar="true"/>
    <div v-if="setorid !=undefined">
    <input type="text" placeholder="Nome do funcionário"
    v-model="nome" @keyup.enter="addFuncionario()">
    <button class="btn btnInput" @click="addFuncionario()">Adicionar</button>
    </div>
    <table>
      <thead>
        <th>Código</th>
        <th>Nome</th>
        <th v-if="setorid == undefined">Setor</th>
        <th>Opções</th>
      </thead>
      <tbody v-if="funcionarios.length">
        <tr v-for="(funcionario,index) in funcionarios" :key="index"> 
          <td>{{funcionario.id}}</td> 
          <td>{{funcionario.nome}} {{funcionario.sobrenome}}</td>
          <td v-if="setorid == undefined">{{funcionario.setor.nome}}</td>
          <td>
            <router-link :to="'/funcionarioDetalhe/'+funcionario.id" tag="button" class="btn btnSuccess" style="cursor:pointer">
            Editar</router-link>
            <button class="btn btnDanger" @click="remover(funcionario)">Remover</button>
          </td> 
        </tr>
      </tbody>
      <tfoot v-if="!funcionarios.length">
          Nenhum funcionário encontrado!
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
      //titulo: 'Funcionário',
      setorid: this.$route.params.setor_id,
      setor: {},
      empresa:{},
      nome: '',
      funcionarios:[]
    }
  },
  created(){
    if (this.setorid){
      this.carregarSetores();
      this.$http.get('http://localhost:5000/api/funcionario/BySetor/${this.setorid}')
        .then(res => res.json())
        .then(funcionarios => this.funcionarios = funcionarios)
    }else{
      this.$http.get('http://localhost:5000/api/funcionario')
        .then(res => res.json())
        .then(funcionarios => {
          this.funcionarios = funcionarios
        })
    }
    
  },
  props: {

  },
  methods:{
    addFuncionario(){
      let _funcionario={
        nome: this.nome,
        sobrenome: "",
        setorid: this.setor.id    
      }
    
      if (_funcionario.nome){
        this.$http.post('http://localhost:5000/api/funcionario', _funcionario)
        .then(res => res.json())
        .then(funcionarioRetornado => {
          this.funcionarios.push(funcionarioRetornado);
          this.nome= '';
        })
      }else{
        this.$alert("O nome do funcionário não pode ser vazio. Preencha o campo e realize a inclusão do funcionário.")
      }
    },
    remover(funcionario){
      this.$http.delete(`http://localhost:5000/api/funcionario/${funcionario.id}`)
      .then(() => {
          let indice = this.funcionarios.indexOf(funcionario);
          this.funcionarios.splice(indice, 1);
      }) 
    },
    carregarSetores(){
    this.$http
       .get('http://localhost:5000/api/setor/' + this.setorid)
       .then(res => res.json())
       .then(setor => {
          this.setor = setor
        });   
    },
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
