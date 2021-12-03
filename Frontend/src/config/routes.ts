import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';
import Role from '@/pages/security/role/Role.vue';
import Roles from '@/pages/security/role/Roles.vue';
import User from '@/pages/security/user/User.vue';
import Users from '@/pages/security/user/Users.vue';

import { PROJECT_NAME } from '@/config';

import Settings from '@/pages/settings/Settings.vue';
import Statistic from "@/pages/statistic/Statistic.vue";
import Documents from '@/pages/documents/Documents.vue';
import Acceptances from '@/pages/acceptance/Acceptances.vue';
import ControlWrapper from '@/pages/conrol/ControlWrapper.vue'
import Delivery from "@/pages/delivery/Delivery.vue";
import Providers from "@/pages/providers/Providers.vue";

Vue.use(VueRouter);

const routes: RouteConfig[] = [
  {
    path: '/statistic',
    name: 'statistic',
    component: Statistic
  },
  {
    path: '/documents',
    name: 'documents',
    component: Documents
  },
  {
    path: '/providers',
    name: 'providers',
    component: Providers
  },
  {
    path: '/delivery',
    name: 'delivery',
    component: Delivery
  },
  {
    path: '/acceptance',
    name: 'acceptance',
    component: Acceptances
  },
  {
    path: '/control',
    name: 'control',
    component: ControlWrapper,
  },
  // {
  //   path: '/:notFound(.*)',
  //   redirect: '/',
  // },
];

export const router = new VueRouter({
  routes,
});

router.beforeEach((to, _, next) => {
  const publicPages = [];
  const authRequired = !publicPages.find((page) => page === to.path);
  const loggedIn = localStorage.getItem('user');

  if (authRequired && !loggedIn && to.path != '/login') {
    return next('/login');
  }

  next();
});

router.afterEach((to) => {
  Vue.nextTick(() => {
    const defaultTitle = (window as any).isTest
      ? `${PROJECT_NAME}-ТЕСТ`
      : PROJECT_NAME;
    document.title = to.meta && to.meta.title ? to.meta.title : defaultTitle;
  });
});
