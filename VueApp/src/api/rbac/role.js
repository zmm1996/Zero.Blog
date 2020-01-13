import https from '../../https.js'

export const getRoleList = (params) => {

     return https.fetchGet('api/v1/rabc/sysrole',params)

  }
  //添加
  export const cteateRole=(params)=>{

   return https.fetchPost('api/v1/rabc/sysrole',params)
  }
  //获取单个
  export const getRoleById=(params)=>{

   return https.fetchGet('api/v1/rabc/sysrole/'+params)
  }

  //编辑
  export const editRole=(id,params)=>{

   return https.fetchPut('api/v1/rabc/sysrole/'+id,params)
  }

  //删除
  export const deleteRole=(id,params)=>{

   return https.fetchDelete('api/v1/rabc/sysrole/'+id,params)
  }

  //角色分配权限
  export const assignPermission=(params)=>{

   return https.fetchPost('api/v1/rabc/sysrole/assign_permission',params)
  }


  
  //根据用户查询角色
  
  export const getRolesByUserId=(id)=>{

   return https.fetchGet('api/v1/rabc/sysrole/getrolesbyuserid/'+id,{})
  }


  export const getAllRolesByUserId=(id)=>{

   return https.fetchGet('api/v1/rabc/sysrole/GetRolesHaveAndNot/'+id,{})
  }