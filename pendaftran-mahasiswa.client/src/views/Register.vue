<template>
  <div class="min-h-screen bg-gray-100 dark:bg-gray-900 flex items-center justify-center px-4 py-10">
    <div class="w-full max-w-3xl bg-white dark:bg-gray-800 shadow-lg rounded-xl p-8 space-y-6">
      <h2 class="text-3xl font-bold text-center text-gray-800 dark:text-white">Formulir Pendaftaran</h2>

      <form @submit.prevent="register" class="space-y-6">
        <!-- BAGIAN 1: Akun -->
        <div>
          <h3 class="text-lg font-semibold mb-2 text-gray-700 dark:text-white">Pendaftaran Akun</h3>
          <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
            <input v-model="form.email" type="email" placeholder="Email *" required class="input" />
            <input v-model="form.password" type="password" placeholder="Password *" required class="input" />
          </div>
        </div>

        <!-- BAGIAN 2: Data Calon Mahasiswa -->
        <div>
          <h3 class="text-lg font-semibold mb-2 text-gray-700 dark:text-white">Data Calon Mahasiswa</h3>
          <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
            <input v-model="form.namaLengkap" type="text" placeholder="Nama Lengkap *" required class="input" />
            <input v-model="form.alamatAsal" type="text" placeholder="Alamat Asal" class="input" />
            <input v-model="form.alamatSemarang" type="text" placeholder="Alamat di Semarang" class="input" />
            <input v-model="form.tempatLahir" type="text" placeholder="Tempat Lahir" class="input" />
            <input v-model="form.tanggalLahir" type="date" class="input" />
            <input v-model="form.agama" type="text" placeholder="Agama" class="input" />
            <select v-model="form.jenisKelamin" class="input">
              <option disabled value="">Jenis Kelamin</option>
              <option value="L">Laki-laki</option>
              <option value="P">Perempuan</option>
            </select>
            <input v-model="form.golonganDarah" type="text" placeholder="Golongan Darah" class="input" />
            <input v-model="form.asalSekolah" type="text" placeholder="Asal Sekolah" class="input" />
            <input v-model="form.jurusanSMU" type="text" placeholder="Jurusan SMU" class="input" />
            <input v-model="form.tahunLulus" type="number" placeholder="Tahun Lulus" class="input" />
            <input v-model="form.jurusanDipilih" type="text" placeholder="Jurusan yang Dipilih" class="input" />
            <input v-model="form.fakultasDipilih" type="text" placeholder="Fakultas yang Dipilih" class="input" />
            <input v-model="form.noTelpRumah" type="text" placeholder="Nomor Telepon Rumah" class="input" />
            <input v-model="form.noHP" type="text" placeholder="Nomor HP" class="input" />
          </div>

          <!-- Foto -->
          <div class="mt-4">
            <label class="block mb-2 text-sm font-medium text-gray-700 dark:text-white">
              Upload Foto 3x4 (format JPG/PNG) *
            </label>
            <input type="file" @change="onFileChange" accept="image/*" class="input" required />
            <div v-if="previewFoto" class="mt-4">
              <img :src="previewFoto" alt="Preview Foto" class="h-32 object-contain rounded border" />
            </div>
          </div>
        </div>

        <!-- BAGIAN 3: Data Orang Tua/Wali -->
        <div>
          <h3 class="text-lg font-semibold mb-2 text-gray-700 dark:text-white">Data Orang Tua / Wali</h3>
          <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
            <input v-model="form.namaOrangTua" type="text" placeholder="Nama Orang Tua / Wali" class="input" />
            <input v-model="form.alamatOrangTua" type="text" placeholder="Alamat Orang Tua / Wali" class="input" />
            <input v-model="form.noOrtu" type="text" placeholder="Nomor HP / Telepon Orang Tua" class="input" />
          </div>
        </div>

        <!-- Tombol Submit -->
        <div class="text-center">
          <button type="submit" class="px-6 py-2 bg-green-600 hover:bg-green-700 text-white rounded font-semibold transition">
            Daftar Sekarang
          </button>
        </div>
      </form>

      <p class="mt-4 text-sm text-center text-gray-600 dark:text-gray-400">
        Sudah punya akun?
        <router-link to="/login" class="text-blue-600 hover:underline dark:text-blue-400">Login di sini</router-link>
      </p>

    </div>
  </div>
</template>

<script>
  import api from '../api'

  export default {
    data() {
      return {
        previewFoto: null,
        form: {
          email: '',
          password: '',
          namaLengkap: '',
          alamatAsal: '',
          alamatSemarang: '',
          tempatLahir: '',
          tanggalLahir: '',
          agama: '',
          jenisKelamin: '',
          golonganDarah: '',
          asalSekolah: '',
          jurusanSMU: '',
          tahunLulus: '',
          jurusanDipilih: '',
          fakultasDipilih: '',
          noTelpRumah: '',
          noHP: '',
          namaOrangTua: '',
          alamatOrangTua: '',
          noOrtu: '',
          foto: null
        }
      }
    },
    methods: {
      onFileChange(e) {
        const file = e.target.files[0]
        if (!file) return

        // Validasi format file
        const validTypes = ['image/jpeg', 'image/png']
        if (!validTypes.includes(file.type)) {
          alert('Format foto tidak valid. Harus JPG atau PNG.')
          this.form.foto = null
          this.previewFoto = null
          return
        }

        // Validasi ukuran rasio 3:4
        const img = new Image()
        img.onload = () => {
          const ratio = img.width / img.height
          const expectedRatio = 3 / 4
          const tolerance = 0.1 // 10% toleransi

          if (Math.abs(ratio - expectedRatio) > tolerance) {
            alert('Ukuran foto harus rasio 3x4, misalnya 300x400 piksel.')
            this.form.foto = null
            this.previewFoto = null
          } else {
            this.form.foto = file
            this.previewFoto = URL.createObjectURL(file)
          }
        }
        img.onerror = () => {
          alert('Gagal membaca gambar.')
          this.form.foto = null
          this.previewFoto = null
        }

        img.src = URL.createObjectURL(file)
      },
      async register() {
        const formData = new FormData()
        for (const key in this.form) {
          formData.append(key, this.form[key])
        }

        try {
          await api.post('/mahasiswa/register', formData)
          alert('Pendaftaran berhasil! Tunggu persetujuan admin.')
          this.$router.push('/login')
        } catch (error) {
          alert('Pendaftaran gagal. Pastikan data terisi dengan benar.')
        }
      }
    }
  }
</script>

<style scoped>
  .input {
    @apply w-full px-4 py-2 rounded border border-gray-300 dark:border-gray-600 dark:bg-gray-700 dark:text-white focus:ring-2 focus:ring-blue-500;
  }
</style>
