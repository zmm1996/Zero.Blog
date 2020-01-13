import https from '../../https.js'

export const getUserList = (params) => {

     return https.fetchGet('api/v1/rabc/sysuser',params)

  }
  //添加
  export const cteateUser=(params)=>{

   return https.fetchPost('api/v1/rabc/sysuser',params)
  }
  //获取单个
  export const getUserById=(id)=>{

   return https.fetchGet('api/v1/rabc/sysuser/'+id)
  }

  //编辑
  export const editUser=(id,params)=>{

   return https.fetchPut('api/v1/rabc/sysuser/'+id,params)
  }

  //删除
  export const deleteUser=(id,params)=>{

   return https.fetchDelete('api/v1/rabc/sysuser/'+id,params)
  }

  //用户分配角色

  export const saveRoles=(params)=>{

   return https.fetchPost('api/v1/rabc/sysuser/saveroles',params)
  }