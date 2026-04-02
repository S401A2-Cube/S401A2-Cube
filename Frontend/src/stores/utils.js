import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

import axios from 'axios'

export const useUtilsStore = defineStore('utils', ()=>
{
    // const url = "https://localhost:7163/api/"
    const url = "https://sae-401-cube-c0g9fue0cad9guey.germanywestcentral-01.azurewebsites.net/api/";
    const isConnected = ref(!!localStorage.getItem('token'));
    const user = ref(JSON.parse(localStorage.getItem('user')) || null);

    function login(token, userData) {
        localStorage.setItem('token', token);
        localStorage.setItem('user', JSON.stringify(userData));
        isConnected.value = true;
        // user.value = userData;
    }

    function logout() {
        localStorage.removeItem('token');
        localStorage.removeItem('user');
        isConnected.value = false;
        // user.value = null;
    }

    return { url, isConnected, login, logout };
});