import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  server:{
    port:3000,
    open:true,
    proxy:{ 
      '/api':{
        // target:'http://192.168.3.109:8081/api',
        target:'http://localhost:5294/api',
        changeOrigin:true,
        rewrite:(path) => path.replace(/^\/api/,'')
      }
    },
  },
  build:{
    target:['edge90','chrome90','firefox90','safari15']
  }
})
