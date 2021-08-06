import { http } from './config'

export default {
    listar: () => {
        return http.get('enderecos')
    },
    limpar: (endereco) => {
        endereco.Estado = '',
            endereco.Cidade = '',
            endereco.Bairro = '',
            endereco.Rua = '',
            endereco.Cep = ''
    },
}