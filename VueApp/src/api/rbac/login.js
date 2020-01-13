import https from '../../https.js'

export const login = (params) => {

     return https.fetchPost('api/oauth/auth',params)

  }

  //操作记录
  export const actionLog = (params) => {

   return https.fetchGet('api/v1/rabc/sysuserlog',params)

}
