import { getInitMenuTree } from "../api/rbac/menu";


const routesList=[
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
            
            // {
            //     path: '/404',
            //     component: () => import(/* webpackChunkName: "404" */ '../components/page/404.vue'),
            //     meta: { title: '404' }
            // },
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
            // {
            //     path: '/actionlog',
            //     component: () => import(/* webpackChunkName: "donate" */ '../components/page/AtionLog.vue'),
            //     meta: { title: '操作记录',requiresAuth:true }
            // },
            // {
            //    path:'/menu',
            //    component:()=>import('../components/rbac/Menu.vue') ,
            //    meta:{title:'菜单',requiresAuth:true}
            // },
            // {
            //    path:'/role',
            //    component:()=>import('../components/rbac/Role') ,
            //    meta:{title:'角色',requiresAuth:true}
            // } ,
            // {
            //    path:'/user',
            //    component:()=>import('../components/rbac/User') ,
            //    meta:{title:'用户',requiresAuth:true}
            // },
            {
               path:'/permission',
               component:()=>import('../components/rbac/Permission') ,
               meta:{title:'权限管理',requiresAuth:true}
            }
            // ,
            // {
            //    path:'/Rolepermission',
            //    component:()=>import('../components/rbac/RolePermission') ,
            //    meta:{title:'角色权限分配',requiresAuth:true}
            // }
        ]
    },
    {
        path: '/login',
        component: () => import(/* webpackChunkName: "login" */ '../components/page/Login.vue'),
        meta: { title: '登录' }
    },
    // {
    //     path: '*',
    //     redirect: '/404'
    // }
]


   


function  loadtree(data)
{

    data.forEach(element => {
        let o= {
            path: element.url,
            component: () => import('../components/rbac/Permission.vue'),
            meta: { title: element.name ,requiresAuth:true}
        }
        
     
        loadtree(element.children)
      
        routesList[1].children.push(o);
         
    });

}

  function initsss()
{
    getInitMenuTree().then(res=>{
        loadtree(res.data);
        window.localStorage.setItem("rezo_routes",JSON.stringify(routesList));
        
      
       }).catch(err=>{
           console.log(err)
       })
       return routesList
}

export default{
    initsss,
    routesList
}