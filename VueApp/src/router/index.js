import Vue from 'vue';
import Router from 'vue-router';
import { getInitMenuTree } from "../api/rbac/menu";
import getRoutes from "../router/get-routes"
import { compile } from 'vue-template-compiler';
Vue.use(Router);

const routes=[
    {
        path: '/',
        redirect: '/dashboard'
    },
    {
        path: '/',
        component: () => import(/* webpackChunkName: "home" */ '../components/common/Home.vue'),
        meta: { title: '自述文件' },
        children: [
            {
                path: '/dashboard',
                component: () => import(/* webpackChunkName: "dashboard" */ '../components/page/Dashboard.vue'),
                meta: { title: '系统首页' ,requiresAuth:true}
            } ,
            
            {
                path: '/404',
                component: () => import(/* webpackChunkName: "404" */ '../components/page/404.vue'),
                meta: { title: '404' }
            },
            // {
            //     // 权限页面
            //     path: '/permission1',
            //     component: () => import(/* webpackChunkName: "permission" */ '../components/page/Permission.vue'),
            //     meta: { title: '权限测试', permission: true }
            // },
            // // {
            //     path: '/403',
            //     component: () => import(/* webpackChunkName: "403" */ '../components/page/403.vue'),
            //     meta: { title: '403' }
            // },
            // {
            //     path: '/donate',
            //     component: () => import(/* webpackChunkName: "donate" */ '../components/page/Donate.vue'),
            //     meta: { title: '支持作者' }
            // },
            {
                path: '/actionlog',
                component: () => import(/* webpackChunkName: "donate" */ '../components/rbac/ActionLog.vue'),
                meta: { title: '操作记录',requiresAuth:true }
            },
            {
               path:'/menu',
               component:()=>import('../components/rbac/Menu.vue') ,
               meta:{title:'菜单',requiresAuth:true}
            },
            {
               path:'/role',
               component:()=>import('../components/rbac/Role') ,
               meta:{title:'角色',requiresAuth:true}
            } ,
            {
               path:'/user',
               component:()=>import('../components/rbac/User') ,
               meta:{title:'用户',requiresAuth:true}
            },
            {
               path:'/permission',
               component:()=>import('../components/rbac/Permission') ,
               meta:{title:'权限管理',requiresAuth:true}
            }
            ,
            {
               path:'/Rolepermission',
               component:()=>import('../components/rbac/RolePermission') ,
               meta:{title:'角色权限分配',requiresAuth:true}
            }
        ]
    },
    {
        path: '/login',
        component: () => import(/* webpackChunkName: "login" */ '../components/page/Login.vue'),
        meta: { title: '登录' }
    },
    {
        path: '*',
        redirect: '/404'
    }
]
//getRoutes.initsss()
//var routes=[]//getRoutes.routesList//路由集合
const router=new Router({
    routes: routes
});

//权限拦截
router.beforeEach((to, from, next) => {

    //检测路由
  
    // let token = window.localStorage.getItem('zero_token')
//console.log(to.matched.some(record => record.meta.requiresAuth));
    //验证当前路由是否需要token

    //let routes111 = window.localStorage.getItem("rezo_routes") //过滤路由

    //router.addRoutes(getRoutes.initsss())
    //console.log(router)

    // if(!routes)
    // {
    //  let routes1= getRoutes.initsss()
    //   router.addRoutes(routes1)
    //   //global.antRouter = routes
    // }
   
   let token = window.localStorage.getItem('zero_token')

    if (to.matched.some(record => record.meta.requiresAuth) && (!token || token === null)) {
      next({
        path: '/login',
        query: { redirect: to.fullPath }
      })
    } else {
      next()
    }
      

    
  })


  function routerGo(to,next){
  //  routes = window.localStorage.getItem("rezo_routes") //过滤路由
  console.log(routes)
    router.addRoutes(routes) //动态添加路由
    global.antRouter = routes //将路由数据传递给全局变量，做侧边栏菜单渲染工作
    next({ ...to, replace: true })

    // let token = window.localStorage.getItem('zero_token')
    // if (to.matched.some(record => record.meta.requiresAuth) && (!token || token === null)) {
    //   next({
    //     path: '/login',
    //     query: { redirect: to.fullPath }
    //   })
    // } else {
    //   next()
    // }
  }
export default router
