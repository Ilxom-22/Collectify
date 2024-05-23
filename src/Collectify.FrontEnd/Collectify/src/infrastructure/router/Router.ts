import {createRouter, createWebHistory} from 'vue-router';
import { AuthenticationService } from '../services/auth/AuthenticationService';
import { useAccountStore } from '../stores/AccountStore';

const routes = [
    {
        path: '/',
        component: () => import('../../common/views/HomeView.vue')
    },
    {
        path: '/admin',
        component: () => import('../../common/views/AdminView.vue')
    },
    {
        path: '/my-collections',
        component: () => import('../../common/views/MyCollectionsView.vue')
    }
];

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: routes
});

router.beforeEach(async (to, from, next) => { 
    const authService = new AuthenticationService();
    await authService.setCurrentUser();

    const accountStore = useAccountStore();
    const authenticated = accountStore.account.isLoggedIn();

    next();
});

export default router;
