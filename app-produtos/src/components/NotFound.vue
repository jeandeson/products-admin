<template>
<div>
    <div class="container">
        <div class="row">
            <div class="col-md-12 align-self-center  d-flex justify-content-center align-self-center" style="height: 90vh;">
                <div class="error-template">
                    <h1>
                        Oops!</h1>
                    <h2>
                        404 Not Found</h2>
                    <div class="error-details">
                        Desculpa, mas essa página não foi encontrada
                    </div>
                    <div class="error-actions mt-3">
                        <a @click="voltar()" class="btn btn-primary btn-lg"><span class="glyphicon glyphicon-home"></span>
                            Voltar </a><a href="https://i.imgur.com/HE0PG73.jpg" class="btn btn-default btn-lg"><span class="glyphicon glyphicon-envelope"></span> Contate o Suporte </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</template>

<script>
    export default {
        methods: {
            voltar(){
                this.$router.push('/produtos') 
            }
        },
    }
</script>

<style scoped>

</style>