import { http, httpimg } from './config'

export default {
    listar: () => {
        return http.get('produtos')
    },
    inserir: (produto) => {
        return http.post('produtos', produto)
    },
    inseririmg: (data) => {
        return httpimg.post('image', data)
    },
    apagar: (produto) => {
        return http.delete('produtos/' + produto.IdProduto)
    },
    limpar: (produto) => {
        produto.NomeProduto = ''
        produto.CodigoProduto = ''
        produto.ValorProduto = ''
        produto.QuantidadeProduto = ''
        produto.ImagemProduto = ''
    },

}