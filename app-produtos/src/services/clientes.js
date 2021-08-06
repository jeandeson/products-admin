import { http } from './config'

export default {
    listar: () => {
        return http.get('clientes')
    },
    inserir: (cliente) => {
        return http.post('clientes', cliente)
    },
    apagar: (cliente) => {
        return http.delete('cliente', { data: cliente })
    },
    limpar: (cliente) => {
        cliente.NomeCliente = '',
            cliente.SobrenomeCliente = '',
            cliente.EmailCliente = '',
            cliente.IdEndereco = null
    },
}