<template>
    <div>
        <titulo :texto="'Empresa:' + empresa.nome" :btnVoltar="visualizar">
            <button v-show="visualizar" class="btn btnEditar" @click="editar()">Editar</button>
        </titulo>
        <table>
            <tbody>
                <tr>
                    <td class="colPequeno">Código</td>
                    <td>
                        <label v-if="visualizar">{{empresa.id}}</label>
                    </td>
                </tr>
                <tr>
                    <td class="colPequeno">CNPJ</td>
                    <td>
                        <label v-if="visualizar">{{empresa.cnpj}}</label>
                        <input v-else v-model="empresa.cnpj" type="text"/>

                    </td>
                </tr>
                <tr>
                    <td class="colPequeno">Razão social</td>
                    <td>
                        <label v-if="visualizar">{{empresa.nome}}</label>
                        <input v-else v-model="empresa.nome" type="text"/>
                   </td>
                </tr>
                <tr>
                    <td class="colPequeno">Nome fantasia</td>
                    <td>
                        <label v-if="visualizar">{{empresa.nomeFantasia}}</label>
                        <input v-else v-model="empresa.nomeFantasia" type="text"/>
                    </td>
                </tr>
                <tr>
                    <td class="colPequeno">Endereço</td>
                    <td>
                        <label v-if="visualizar">{{empresa.endereco}}</label>
                        <input v-else v-model="empresa.endereco" type="text"/>

                   </td>
                </tr>
                 <tr>
                    <td class="colPequeno">Telefone</td>
                    <td>
                        <label v-if="visualizar">{{empresa.telefone}}</label>
                        <input v-else v-model="empresa.telefone" type="text"/>
                    </td>
                </tr>
            </tbody>    
        </table>   

        <div style="margin-top: 10px">
            <div v-if="!visualizar">
                <button class="btn btnSalvar" @click="salvar(empresa)">Salvar</button>
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
                empresa:{},
                id: this.$route.params.empresa_id,
                visualizar: true,
            }
        },
        created(){
        this.$http.get('http://localhost:5000/api/empresa/' + this.id)
            .then(res => res.json())
            .then(empresa => (this.empresa = empresa));
        },

        // this.$http
        //     .get('http://localhost:3000/setores')
        //     .then(res => res.json())
        //     .then(setor => (this.setores = setor));
        // },
        methods: {
            editar(){
                this.visualizar = !this.visualizar;
            },
            salvar(empresa){
                let _empresaEditar={
                    id: empresa.id,
                    cnpj: empresa.cnpj,
                    nome: empresa.nome,
                    nomeFantasia: empresa.nomeFantasia,
                    endereco: empresa.endereco,
                    telefone: empresa.telefone,
                }

                this.$http
                    .put('http://localhost:5000/api/empresa/'+ _empresaEditar.id,
                    _empresaEditar)

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
}
.btnSalvar{
    float: right;
    background-color: rgb(79,196,68);
}
.btnCancelar{
    float: left;
    background-color: rgb(249,186,92);
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