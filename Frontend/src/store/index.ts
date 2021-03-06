import Vue from 'vue';
import Vuex from 'vuex';

import account from './account.module';
import dictionary from './dictionary.module';
import acceptance from './acceptance.module';
import zone from "@/store/zone.modules";
import delivery from "@/store/delivery.module";
import providers from "@/store/providers.module";
Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    account,
    dictionary,
    acceptance,
    zone,
    delivery,
    providers
  },
});
