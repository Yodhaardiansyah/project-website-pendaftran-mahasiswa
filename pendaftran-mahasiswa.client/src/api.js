import axios from 'axios'

const api = axios.create({
  baseURL: 'https://localhost:7271/api', 
})

export default api
