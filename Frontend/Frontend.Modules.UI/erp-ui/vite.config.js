import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig(() => {

    return {
        plugins: [react()],
        base: '/',
        server: {
            host: true,
            port: parseInt(process.env.VITE_PORT, 10) || 3000,
        },
    };
});