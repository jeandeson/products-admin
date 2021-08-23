<template>
    <div>
        <h2 class="text-center text-info text-uppercase font-weight-bold mt-2 mb-0 rounded-top">{{ texto }}</h2>
    </div>
</template>

<script>
    export default {
        props:{
            texto: String
        }
    }
</script>

<style scoped>
    h2{
        background:none;
        font-weight: 700;
        /*color: rgb(47, 119, 214) !important;*/
    }
</style>