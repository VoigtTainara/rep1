<template>
    <div v-if="!loading">
        <titulo :texto="'Funcionário:' + funcionario.nome" :btnVoltar="visualizar">
            <button v-show="visualizar" class="btn btnEditar" @click="editar()">Editar</button>
        </titulo>
        <table>
            <tbody>
                <tr>
                    <td class="colPequeno">Código</td>
                    <td>
                        <label>{{funcionario.id}}</label>
                    </td>
                </tr>
                <tr>
                    <td class="colPequeno">Nome</td>
                    <td>
                        <label v-if="visualizar">{{funcionario.nome}}</label>
                        <input v-else v-model="funcionario.nome" type="text"/>

                   </td>
                </tr>
                <tr>
                    <td class="colPequeno">Sobrenome</td>
                    <td>
                        <label v-if="visualizar">{{funcionario.sobrenome}}</label>
                        <input v-else v-model="funcionario.sobrenome" type="text"/>

                    </td>
                </tr>
                <tr>
                    <td class="colPequeno">Data de nascimento</td>
                    <td>
                        <label v-if="visualizar">{{funcionario.dataNasc}}</label>
                        <input v-else v-model="funcionario.dataNasc" type="text"/>

                   </td>
                </tr>
                 <tr>
                    <td class="colPequeno">CPF</td>
                    <td>
                        <label v-if="visualizar">{{funcionario.cpf}}</label>
                        <input v-else v-model="funcionario.cpf" type="text"/>

                    </td>
                </tr>
                 <tr>
                    <td class="colPequeno">RG</td>
                    <td>
                        <label v-if="visualizar">{{funcionario.rg}}</label>
                        <input v-else v-model="funcionario.rg" type="text"/>

                    </td>
                </tr>
                 <tr>
                    <td class="colPequeno">Endereço</td>
                    <td>
                        <label v-if="visualizar">{{funcionario.endereco}}</label>
                        <input v-else v-model="funcionario.endereco" type="text"/>

                    </td>
                </tr>
                 <tr>
                    <td class="colPequeno">Número do PIS</td>
                    <td>
                        <label v-if="visualizar">{{funcionario.pis}}</label>
                        <input v-else v-model="funcionario.pis" type="text"/>

                    </td>
                </tr>
                 <tr>
                    <td class="colPequeno">Carga horária</td>
                    <td>
                        <label v-if="visualizar">{{funcionario.cargaHoraria}}</label>
                        <input v-else v-model="funcionario.cargaHoraria" type="text"/>

                    </td>
                </tr>
                 <tr>
                    <td class="colPequeno">Salário</td>
                    <td>
                        <label v-if="visualizar">{{funcionario.salario}}</label>
                        <input v-else v-model="funcionario.salario" type="text"/>

                    </td>
                </tr>
                 <tr>
                    <td class="colPequeno">Data de admissão</td>
                    <td>
                        <label v-if="visualizar">{{funcionario.dataAdmissao}}</label>
                        <input v-else v-model="funcionario.dataAdmissao" type="text"/>

                    </td>
                </tr>
                 <tr>
                    <td class="colPequeno">Data de desligamento</td>
                    <td>
                        <label v-if="visualizar">{{funcionario.dataDesligamento}}</label>
                        <input v-else v-model="funcionario.dataDesligamento" type="text"/>

                    </td>
                </tr> 
                 <tr>
                    <td class="colPequeno">Setor</td>
                    <td>
                        <label v-if="visualizar">{{funcionario.setor.nome}}</label>
                        <select v-else v-model="funcionario.setor.id">
                           <option v-for="(setor, index) in setores" 
                           :key="index" v-bind:value="setor.id">
                           {{setor.nome}}</option>
                        </select>    
                    </td>
                </tr>    
            </tbody>    
        </table>   

        <div style="margin-top: 10px">
            <div v-if="!visualizar">
                <button class="btn btnSalvar" @click="salvar(funcionario)">Salvar</button>
                <button class="btn btnCancelar" @click="cancelar()">Cancelar</button>
            </div>    
        </div>     
    </div>
</template>

<script>
import Titulo from '../Titulo/Titulo'
    export default {
        components:{
            Titulo
        },
        data(){
            return {
                setores:[],
                funcionario: {},
                id: this.$route.params.funcionario_id,
                visualizar: true,
                loading: true
            }
        },
        created(){
        //this.carregarSetor();    
        this.carregarFuncionario();
        
        },
        methods: {
            carregarSetor(empresaId){
                this.$http
                    .get(`http://localhost:5000/api/setor/ByEmpresa/${empresaId}`)
                    .then(res => res.json())
                    .then(setor => {
                        this.setores = setor
                        //this.carregarFuncionario();
                    });
            },
            carregarFuncionario(){
                this.$http.get('http://localhost:5000/api/funcionario/' + this.id)
                    .then(res => res.json())
                    .then(funcionario => {
                        this.funcionario = funcionario;
                        this.carregarSetor(funcionario.setor.empresaId);
                        this.loading = false;
                    });

            },
            editar(){
                this.visualizar = !this.visualizar;
            },
            salvar(funcionario){
                let _funcionarioEditar={
                    id: funcionario.id,
                    nome: funcionario.nome,
                    sobrenome: funcionario.sobrenome,
                    dataNasc: funcionario.dataNasc,
                    cpf: funcionario.cpf,
                    rg: funcionario.rg,
                    endereco: funcionario.endereco,
                    pis: funcionario.pis,
                    cargaHoraria: funcionario.cargaHoraria,
                    salario: funcionario.salario,
                    dataAdmissao: funcionario.dataAdmissao,
                    dataDesligamento: funcionario.dataDesligamento,
                    setorid: funcionario.setor.id
                }

                this.$http
                    .put('http://localhost:5000/api/funcionario/'+ _funcionarioEditar.id,
                    _funcionarioEditar)
                    .then(res => res.json())
                    .then(funcionario = this.funcionario = funcionario)
                    .then(() => { 
                        this.visualizar = true;
                        this.carregarFuncionario();
                    });

                this.visualizar = !this.visualizar;
            },
            cancelar(){
                this.visualizar = !this.visualizar;
            }
        }
    }
</script>

<style scoped>
.btnEditar{
    float: right;
    background-color: rgb(76,186,249);
    padding: 10px 20px;
}
.btnSalvar{
    float: right;
    background-color: rgb(79,196,68);
    padding: 10px 20px;
}
.btnCancelar{
    float: left;
    background-color: rgb(249,186,92);
    padding: 10px 20px;
}
.colPequeno{
    width: 20%;
    text-align: right;
    background-color: rgb(125,217,245);
    font-weight: bold;
}
input, select{
    margin: 0px;
    padding: 5px 10px;
    font-size: 0.9em;
    font-family: Montserrat;
    border-radius: 5px;
    border: 1px solid silver;
    background-color: rgb(245,245,245);
    width: 95%;
}
select{
    height: 38px;
    width: 100%;
}

</style>