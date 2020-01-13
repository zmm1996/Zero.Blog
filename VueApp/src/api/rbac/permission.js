import https from '../../https.js'

export const getPermissionList = (params) => {

     return https.fetchGet('api/v1/rabc/syspermission',params)

  }
  //添加
  export const cteatePermission=(params)=>{

   return https.fetchPost('api/v1/rabc/syspermission',params)
  }
  //获取单个
  export const getPermissionById=(id)=>{

   return https.fetchGet('api/v1/rabc/syspermission/'+id)
  }

  //编辑
  export const editPermission=(id,params)=>{

   return https.fetchPut('api/v1/rabc/syspermission/'+id,params)
  }

  //删除
  export const deletePermission=(id,params)=>{

   return https.fetchDelete('api/v1/rabc/syspermission/'+id,params)
  }

  //菜单树
  export const getMenuTree=(id)=>{

   return https.fetchGet('api/v1/rabc/sysmenu/menutree?id='+id,{})
  }


  //获取权限树根据角色id

  export const getPermissonTreeByRoleId=(id)=>{

   return https.fetchGet('api/v1/rabc/syspermission/permissiontree?id='+id,{})
  }