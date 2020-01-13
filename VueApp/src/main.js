import Vue from 'vue'
import App from './App.vue'
 import './plugins/element.js'
 import router from './router/index.js'
//图形表格
 import Echarts from 'echarts'

 import './assets/css/icon.css';

 // 引入axios，并加到原型链中
import axios from 'axios';
Vue.prototype.$axios = axios;
import QS from 'qs'
Vue.prototype.qs = QS;


Vue.config.productionTip = false

Vue.prototype.$echarts=Echarts

new Vue({
  router,//挂载路由
  render: h => h(App),
}).$mount('#app')
