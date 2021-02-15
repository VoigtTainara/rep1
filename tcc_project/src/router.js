import Vue from 'vue';
import Router from 'vue-router'
import Empresas from './components/Empresa/Empresas'
import EmpresaDetalhe from './components/Empresa/EmpresaDetalhe'
import Setores from './components/Setor/Setores'
import Funcionarios from './components/Funcionario/Funcionarios'
import FuncionarioDetalhe from './components/Funcionario/FuncionarioDetalhe'

Vue.use(Router);

export default new Router({
    routes:[
        {
            path:'/empresas',
            nome: 'Empresas',
            component: Empresas
        },
        {
            path: '/empresaDetalhe/:empresa_id',
            nome: 'EmpresaDetalhe',
            component: EmpresaDetalhe
        },
        {
            path: '/setores/:empresa_id',
            nome: 'Setores',
            component: Setores
        },
        {
            path: '/setores',
            nome: 'Setores',
            component: Setores
        },
        {
            path: '/todosfuncionarios',
            nome: 'Funcionarios',
            component: Funcionarios
        },
        {
            path: '/funcionarios/:setor_id',
            nome: 'Funcionarios',
            component: Funcionarios
        },
        {
            path: '/funcionarioDetalhe/:funcionario_id',
            nome: 'FuncionarioDetalhe',
            component: FuncionarioDetalhe
        }

    ]

})
