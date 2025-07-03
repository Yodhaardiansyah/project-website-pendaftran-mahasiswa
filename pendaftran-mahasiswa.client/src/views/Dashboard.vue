<template>
  <div class="min-h-screen bg-gray-100 dark:bg-gray-900 p-6 text-gray-800 dark:text-white flex flex-col items-center justify-center space-y-4">
    <h2 class="text-2xl font-bold">Selamat Datang, {{ nama }}</h2>
    <img :src="fotoUrl" alt="Foto Mahasiswa" class="w-48 h-48 object-cover rounded shadow border" />
    <button @click="logout" class="btn bg-red-600 hover:bg-red-700">Logout</button>
  </div>
</template>

<script>
  import api from '../api'

  export default {
    data() {
      return {
        nama: localStorage.getItem('nama'),
        fotoUrl: ''
      }
    },
    async created() {
      const id = localStorage.getItem('id')
      const role = localStorage.getItem('role')

      // 🔒 Validasi login dan role
      if (!id || role !== 'mahasiswa') {
        this.$router.push('/login')
        return
      }

      try {
        const res = await api.get(`/mahasiswa`)
        const mhs = res.data.find(m => m.id == id)
        if (mhs && mhs.foto) {
          this.fotoUrl = import.meta.env.VITE_API_URL + `${mhs.foto}`
        }
      } catch (error) {
        console.error('Gagal mengambil data mahasiswa:', error)
      }
    },
    methods: {
      logout() {
        localStorage.clear()
        this.$router.push('/login')
      }
    }
  }
</script>

<style scoped>
  .btn {
    @apply px-4 py-2 rounded text-white font-semibold text-sm transition disabled:opacity-50;
  }
</style>
