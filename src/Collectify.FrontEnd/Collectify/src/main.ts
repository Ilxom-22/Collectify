import './assets/main.css'
import './assets/colors.css'
import './assets/button.css'
import './assets/formInput.css'
import './assets/modal.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import { AppThemeService } from './infrastructure/services/appTheme/AppThemeService'

const appThemeService = new AppThemeService();

const app = createApp(App);

app.use(createPinia());

appThemeService.setAppTheme();

app.mount('#app');
