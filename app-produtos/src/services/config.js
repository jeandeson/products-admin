import axios from 'axios'


export const http = axios.create({
    baseURL: 'http://localhost:5000/api/',
    headers: {
        'Content-Type': 'application/json',
        'x-apikey': '59a7ad19f5a9fa0808f11931',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'GET,PUT,POST,DELETE,PATCH,OPTIONS',
    }
})
export const httpimg = axios.create({
    baseURL: 'https://api.imgur.com/3/',
    headers: {
        "Content-type": "application/x-www-form-urlencoded",
        Authorization: "Client-ID 3052282645ce5e9",
    },
})