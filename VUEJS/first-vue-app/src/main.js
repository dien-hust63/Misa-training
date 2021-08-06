import Vue from 'vue'
import App from './App.vue'
import { ValidationProvider} from 'vee-validate';
Vue.component('ValidationProvider', ValidationProvider);

export const eventBus = new Vue();

Vue.config.productionTip = false

new Vue({
  render: h => h(App),
}).$mount('#app')
