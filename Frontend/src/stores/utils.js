import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

import axios from 'axios'

export const useUtilsStore = defineStore('utils', ()=>
{
    const url = "https://sae-401-cube-c0g9fue0cad9guey.germanywestcentral-01.azurewebsites.net/api/";
    return {url};
});