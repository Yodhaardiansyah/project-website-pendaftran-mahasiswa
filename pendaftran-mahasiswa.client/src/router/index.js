import { createRouter, createWebHistory } from 'vue-router'
import Register from '../views/Register.vue'
import Login from '../views/Login.vue'
import Dashboard from '../views/Dashboard.vue'
import Admin from '../views/Admin.vue'

const routes = [
  { path: '/register', component: Register },
  { path: '/login', component: Login },
  { path: '/dashboard', component: Dashboard },
  { path: '/admin', component: Admin },
  { path: '/', redirect: '/login' }
]

export default createRouter({
  history: createWebHistory(),
  routes
})
