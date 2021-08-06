import { bus } from '../main.js'
export default {
    erromsg: (error) => {
        bus.$emit('Notification::show', {
            type: 'danger',
            info: 'exclamation-triangle-fill',
            message: 'Não foi possivel cadastrar o produto ' + error,
            timeout: 10000
        })
    },
    successmsg: () => {
        bus.$emit('Notification::show', {
            type: 'success',
            info: 'check-circle-fill',
            message: 'Operação Realizada com successo ',
            timeout: 10000
        })
    },
    deletemsg: () => {
        bus.$emit('Notification::show', {
            type: 'danger',
            info: 'exclamation-triangle-fill',
            message: 'Deletado com sucessso',
            timeout: 10000
        })
    },
    warningmsg: () => {
        bus.$emit('Notification::show', {
            type: 'warning',
            info: 'info-fill',
            message: 'Alguma coisa não está certa',
            timeout: 10000
        })
    },
    erroinvalid: () => {
        bus.$emit('Notification::show', {
            type: 'danger',
            info: 'info-fill',
            message: 'Erro de validação, verifique os campos e tente novamente',
            timeout: 10000
        })
    },
    uploadmsg: () => {
        bus.$emit('Notification::show', {
            type: 'warning',
            info: 'info-fill',
            message: 'Fazendo upload da imagem',
            timeout: 15000
        })
    },
    mapaerro: (centro) => {
        bus.$emit('Notification::show', {
            type: 'danger',
            info: 'danger-fill',
            message: 'Ocorreu um erro ao carregar o ' + centro.NomeCentro + ', verifique as informaçãoes cadastradas.',
            timeout: 15000
        })
    },
    mapaerroclient: (cliente) => {
        bus.$emit('Notification::show', {
            type: 'danger',
            info: 'danger-fill',
            message: 'Ocorreu um erro ao carregar o cliente ' + cliente.NomeCliente + ', verifique as informaçãoes cadastradas.',
            timeout: 15000
        })
    }
}