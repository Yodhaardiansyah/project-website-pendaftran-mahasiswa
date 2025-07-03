<template>
  <div class="h-screen flex items-center justify-center bg-gray-100 dark:bg-gray-900 px-4">
    <div class="w-full max-w-md bg-white dark:bg-gray-800 rounded-xl shadow-lg p-8">
      <h2 class="text-3xl font-semibold text-center text-gray-800 dark:text-white mb-6">
        Login Sistem
      </h2>

      <form @submit.prevent="login" class="space-y-4">
        <!-- Role -->
        <select v-model="role"
                class="w-full px-4 py-2 rounded border border-gray-300 dark:border-gray-600 dark:bg-gray-700 dark:text-white focus:ring-2 focus:ring-blue-500">
          <option value="admin">Admin</option>
          <option value="mahasiswa">Mahasiswa</option>
        </select>

        <!-- Email -->
        <div class="relative">
          <span class="absolute inset-y-0 left-0 flex items-center pl-3 text-gray-400">👤</span>
          <input v-model="email"
                 type="text"
                 placeholder="Email / Username"
                 required
                 class="w-full pl-10 pr-4 py-2 rounded border border-gray-300 dark:border-gray-600 dark:bg-gray-700 dark:text-white focus:ring-2 focus:ring-blue-500" />
        </div>

        <!-- Password -->
        <div class="relative">
          <span class="absolute inset-y-0 left-0 flex items-center pl-3 text-gray-400">🔒</span>
          <input v-model="password"
                 type="password"
                 placeholder="Password"
                 required
                 class="w-full pl-10 pr-4 py-2 rounded border border-gray-300 dark:border-gray-600 dark:bg-gray-700 dark:text-white focus:ring-2 focus:ring-blue-500" />
        </div>

        <!-- Submit -->
        <button type="submit"
                :disabled="loading"
                class="w-full py-2 rounded bg-blue-600 hover:bg-blue-700 text-white font-semibold transition disabled:opacity-50">
          {{ loading ? 'Memproses...' : 'Masuk' }}
        </button>
      </form>

      <p class="mt-4 text-sm text-center text-gray-600 dark:text-gray-400">
        Belum punya akun?
        <router-link to="/register" class="text-blue-600 hover:underline dark:text-blue-400">Daftar di sini</router-link>
      </p>

      <!-- Loading -->
      <p v-if="loading" class="mt-3 text-sm text-center text-gray-500 dark:text-gray-400">
        Mohon tunggu, sedang login...
      </p>
    </div>
  </div>
</template>

<script>
  import api from '../api'

  export default {
    data() {
      return {
        email: '',
        password: '',
        role: 'mahasiswa',
        loading: false,
        isDarkMode: localStorage.getItem('theme') === 'dark'
      }
    },
    mounted() {
      document.documentElement.classList.toggle('dark', this.isDarkMode)
    },
    methods: {
      async login() {
        this.loading = true
        try {
          const res = await api.post('/auth/login', {
            emailOrUsername: this.email,
            password: this.password,
            role: this.role
          })
          // Simpan semua data
          localStorage.setItem('role', this.role)
          localStorage.setItem('nama', res.data.nama)
          localStorage.setItem('id', res.data.id)

          if (this.role === 'admin') {
            this.$router.push('/admin')
          } else {
            this.$router.push('/dashboard')
          }

        } catch (err) {
          const msg = err.response?.data?.message || 'Login gagal.'
          alert(msg)
        } finally {
          this.loading = false
        }
      }
    }
  }
</script>
