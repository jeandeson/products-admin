import { http } from './config'

export default {
    listar: () => {
        return http.get('centros')
    },
    inserir: (centro) => {
        return http.post('centros', centro)
    },
    apagar: (centro) => {
        return http.delete('centro', { data: centro })
    },
    limpar: (centro) => {
        centro.CodigoCentro = '',
            centro.NomeCentro = '',
            centro.IdEndereco = ''
    }
}