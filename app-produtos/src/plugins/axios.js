import Axios from 'axios'

const config = ({
    baseURL: 'http://localhost:5000/api',
    headers: {
        'Content-Type': 'application/json',
        'x-apikey': '59a7ad19f5a9fa0808f11931',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'GET,PUT,POST,DELETE,PATCH,OPTIONS',
    }
})

const axios = Axios.create(config);

export default (axios);