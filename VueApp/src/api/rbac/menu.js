import https from '../../https.js'

export const getMenuList = (params) => {

     return https.fetchGet('api/v1/rabc/sysmenu',params)

  }
  //添加
  export const cteateMenu=(params)=>{

   return https.fetchPost('api/v1/rabc/sysmenu',params)
  }
  //获取单个
  export const getMenuById=(id)=>{

   return https.fetchGet('api/v1/rabc/sysmenu/'+id)
  }

  //编辑
  export const editMenu=(id,params)=>{

   return https.fetchPut('api/v1/rabc/sysmenu/'+id,params)
  }

  //删除
  export const deleteMenu=(id,params)=>{

   return https.fetchDelete('api/v1/rabc/sysmenu/id?='+id,params)
  }

  //树
  export const getMenuTree=(id)=>{

   return https.fetchGet('api/v1/rabc/sysmenu/menutree?id='+id,{})
  }


  //初始化树
  export const getInitMenuTree=()=>{

   return https.fetchGetAsync('api/v1/rabc/sysmenu/menutreebyuserid',{})
  }