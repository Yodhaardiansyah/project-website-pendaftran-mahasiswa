<template>
  <div class="min-h-screen bg-white dark:bg-gray-900 text-black dark:text-white transition-colors">
    <header class="p-4 flex justify-end">
      <button @click="toggleTheme"
              class="px-4 py-2 rounded border text-sm font-medium bg-gray-200 dark:bg-gray-700 dark:text-white hover:bg-gray-300 dark:hover:bg-gray-600 transition">
        {{ isDarkMode ? '☀️ Mode Siang' : '🌙 Mode Malam' }}
      </button>
    </header>

    <main class="px-4 py-6">
      <router-view />
    </main>
  </div>
</template>

<script>
  export default {
    data() {
      return {
        isDarkMode: localStorage.getItem('theme') === 'dark'
      }
    },
    mounted() {
      document.documentElement.classList.toggle('dark', this.isDarkMode)
    },
    methods: {
      toggleTheme() {
        this.isDarkMode = !this.isDarkMode
        document.documentElement.classList.toggle('dark', this.isDarkMode)
        localStorage.setItem('theme', this.isDarkMode ? 'dark' : 'light')
      }
    }
  }
</script>
