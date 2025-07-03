<template>
  <div class="min-h-screen bg-gray-100 dark:bg-gray-900 p-6 text-gray-800 dark:text-white">
    <div class="max-w-6xl mx-auto space-y-6">
      <div class="flex items-center justify-between">
        <h2 class="text-3xl font-bold mb-4">Admin Panel</h2>
        <button @click="logout" class="btn bg-red-600 p-2 hover:bg-red-700">Logout</button>
      </div>

      <!-- Filter -->
      <label class="flex items-center space-x-2 text-sm">
        <input type="checkbox" v-model="showIncompleteOnly" class="form-checkbox text-blue-600" />
        <span>Hanya tampilkan yang belum lengkap</span>
      </label>

      <!-- Tabel Mahasiswa -->
      <div class="overflow-auto rounded-lg shadow border dark:border-gray-700">
        <table class="w-full text-sm text-left">
          <thead class="bg-gray-200 dark:bg-gray-700">
            <tr>
              <th class="px-4 py-3">No. Pendaftaran</th>
              <th class="px-4 py-3">Nama</th>
              <th class="px-4 py-3">Email</th>
              <th class="px-4 py-3">Status</th>
              <th class="px-4 py-3">Aksi</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="mhs in filteredMahasiswas" :key="mhs.id" class="border-t dark:border-gray-700">
              <td class="px-4 py-2">{{ mhs.nomorPendaftaran || '-' }}</td>
              <td class="px-4 py-2">{{ mhs.namaLengkap }}</td>
              <td class="px-4 py-2">{{ mhs.email }}</td>
              <td class="px-4 py-2">
                {{ mhs.status }}
                <span v-if="!isDataLengkap(mhs)" class="text-orange-500 font-semibold">⚠ Tidak Lengkap</span>
              </td>
              <td class="px-4 py-2">
                <div class="flex flex-wrap gap-2">
                  <button @click="lihatDetail(mhs)" class="btn bg-blue-500 hover:bg-blue-600 px-3 py-1">Detail</button>
                  <button @click="openChecklistModal(mhs)" class="btn bg-green-600 hover:bg-green-700 px-3 py-1">Approve</button>
                  <button @click="ubahStatus(mhs.id, 'Rejected')" class="btn bg-red-600 hover:bg-red-700 px-3 py-1">Reject</button>
                  <button @click="hapusMahasiswa(mhs.id)" class="btn bg-gray-600 hover:bg-gray-700 px-3 py-1">Hapus</button>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>

      <!-- Modal Detail -->
      <div v-if="selectedMahasiswa" class="modal">
        <div class="modal-content w-full max-w-3xl">
          <h3 class="text-xl font-bold mb-4">Detail Mahasiswa</h3>

          <div v-if="selectedMahasiswa.foto" class="mb-4">
            <img :src="baseURL + selectedMahasiswa.foto" alt="Foto Mahasiswa" class="h-32 object-contain rounded border" />
          </div>

          <div class="grid grid-cols-1 md:grid-cols-2 gap-3 text-sm">
            <div><strong>Nomor Pendaftaran:</strong> {{ selectedMahasiswa.nomorPendaftaran || '-' }}</div>
            <div><strong>Nama:</strong> {{ selectedMahasiswa.namaLengkap }}</div>
            <div><strong>Email:</strong> {{ selectedMahasiswa.email }}</div>
            <div><strong>Alamat Asal:</strong> {{ selectedMahasiswa.alamatAsal || '-' }}</div>
            <div><strong>Alamat Semarang:</strong> {{ selectedMahasiswa.alamatSemarang || '-' }}</div>
            <div><strong>Tempat/Tgl Lahir:</strong> {{ selectedMahasiswa.tempatLahir || '-' }} / {{ selectedMahasiswa.tanggalLahir?.substring(0, 10) || '-' }}</div>
            <div><strong>Agama:</strong> {{ selectedMahasiswa.agama || '-' }}</div>
            <div><strong>Jenis Kelamin:</strong> {{ selectedMahasiswa.jenisKelamin || '-' }}</div>
            <div><strong>Golongan Darah:</strong> {{ selectedMahasiswa.golonganDarah || '-' }}</div>
            <div><strong>Asal Sekolah:</strong> {{ selectedMahasiswa.asalSekolah || '-' }}</div>
            <div><strong>Jurusan SMU:</strong> {{ selectedMahasiswa.jurusanSMU || '-' }}</div>
            <div><strong>Tahun Lulus:</strong> {{ selectedMahasiswa.tahunLulus || '-' }}</div>
            <div><strong>Jurusan Dipilih:</strong> {{ selectedMahasiswa.jurusanDipilih || '-' }}</div>
            <div><strong>Fakultas Dipilih:</strong> {{ selectedMahasiswa.fakultasDipilih || '-' }}</div>
            <div><strong>No. Telp Rumah:</strong> {{ selectedMahasiswa.noTelpRumah || '-' }}</div>
            <div><strong>No. HP:</strong> {{ selectedMahasiswa.noHP || '-' }}</div>
            <div><strong>Nama Orang Tua:</strong> {{ selectedMahasiswa.namaOrangTua || '-' }}</div>
            <div><strong>Alamat Orang Tua:</strong> {{ selectedMahasiswa.alamatOrangTua || '-' }}</div>
            <div><strong>No. HP Orang Tua:</strong> {{ selectedMahasiswa.noOrtu || '-' }}</div>
          </div>

          <div class="mt-6 text-right">
            <button class="btn bg-gray-600 p-2 hover:bg-gray-700" @click="selectedMahasiswa = null">Tutup</button>
          </div>
        </div>
      </div>

      <!-- Modal Checklist -->
      <div v-if="checklistMahasiswa" class="modal">
        <div class="modal-content w-full max-w-md">
          <h3 class="text-xl font-bold mb-4">Checklist Kelengkapan Data</h3>

          <div class="grid grid-cols-2 gap-3 text-sm">
            <div class="flex items-center justify-between">
              <span>Alamat Asal</span>
              <span :class="checkClass(checklistMahasiswa.alamatAsal)">
                {{ checklistMahasiswa.alamatAsal ? '✔' : '✘' }}
              </span>
            </div>
            <div class="flex items-center justify-between">
              <span>Tanggal Lahir</span>
              <span :class="checkClass(checklistMahasiswa.tanggalLahir)">
                {{ checklistMahasiswa.tanggalLahir ? '✔' : '✘' }}
              </span>
            </div>
            <div class="flex items-center justify-between">
              <span>Asal Sekolah</span>
              <span :class="checkClass(checklistMahasiswa.asalSekolah)">
                {{ checklistMahasiswa.asalSekolah ? '✔' : '✘' }}
              </span>
            </div>
            <div class="flex items-center justify-between">
              <span>Jurusan Dipilih</span>
              <span :class="checkClass(checklistMahasiswa.jurusanDipilih)">
                {{ checklistMahasiswa.jurusanDipilih ? '✔' : '✘' }}
              </span>
            </div>
            <div class="flex items-center justify-between">
              <span>No HP</span>
              <span :class="checkClass(checklistMahasiswa.noHP)">
                {{ checklistMahasiswa.noHP ? '✔' : '✘' }}
              </span>
            </div>
          </div>

          <div class="mt-6 flex justify-end gap-2">
            <button class="btn bg-gray-500 hover:bg-gray-600" @click="checklistMahasiswa = null">Batal</button>
            <button class="btn bg-green-600 hover:bg-green-700"
                    :disabled="!isDataLengkap(checklistMahasiswa)"
                    @click="approveAfterCheck">
              Approve Sekarang
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>

  import api from '../api'

  export default {
    data() {
      return {
        mahasiswas: [],
        selectedMahasiswa: null,
        checklistMahasiswa: null,
        showIncompleteOnly: false,
        baseURL: import.meta.env.VITE_API_URL
      }
    },
    computed: {
      filteredMahasiswas() {
        return this.showIncompleteOnly
          ? this.mahasiswas.filter(m => !this.isDataLengkap(m))
          : this.mahasiswas
      }
    },
    async created() {
      const role = localStorage.getItem('role')
      const isLoggedIn = localStorage.getItem('id')

      // ⛔ Cek apakah login dan role = admin
      if (!isLoggedIn || role !== 'admin') {
        this.$router.push('/login')
        return
      }

      try {
        const res = await api.get('/mahasiswa')
        this.mahasiswas = res.data
      } catch (error) {
        console.error('Gagal mengambil data:', error)
      }
    },
    methods: {
      logout() {
        localStorage.clear()
        this.$router.push('/login')
      },
      isDataLengkap(m) {
        return m.alamatAsal && m.tanggalLahir && m.asalSekolah && m.jurusanDipilih && m.noHP
      },
      checkClass(value) {
        return value ? 'text-green-500 font-bold' : 'text-red-500 font-bold'
      },
      lihatDetail(mhs) {
        this.selectedMahasiswa = mhs
      },
      openChecklistModal(mhs) {
        this.checklistMahasiswa = mhs
      },
      async hapusMahasiswa(id) {
        if (confirm('Yakin ingin menghapus data ini?')) {
          try {
            await api.delete(`/mahasiswa/${id}`)
            this.mahasiswas = this.mahasiswas.filter(m => m.id !== id)
            alert('Data berhasil dihapus.')
          } catch (error) {
            console.error('Gagal menghapus:', error)
            alert('Terjadi kesalahan saat menghapus.')
          }
        }
      },

      async approveAfterCheck() {
        const id = this.checklistMahasiswa.id
        await api.put(`/mahasiswa/approve/${id}?status=Approved`)
        this.mahasiswas = this.mahasiswas.map(m =>
          m.id === id ? { ...m, status: 'Approved' } : m
        )
        this.checklistMahasiswa = null
        alert('Mahasiswa berhasil di-approve!')
      },
      async ubahStatus(id, status) {
        await api.put(`/mahasiswa/approve/${id}?status=${status}`)
        this.mahasiswas = this.mahasiswas.map(m =>
          m.id === id ? { ...m, status } : m
        )
      }
    }
  }
</script>

<style scoped>
  .modal {
    @apply fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50;
  }

  .modal-content {
    @apply bg-white dark:bg-gray-800 p-6 rounded-lg shadow-lg max-h-[90vh] overflow-y-auto;
  }

  .btn {
    @apply rounded text-white font-semibold text-sm transition disabled:opacity-50;
  }
</style>
