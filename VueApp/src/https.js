import axios from 'axios'
import qs from 'qs'
import { Message } from 'element-ui' //弹框
import router from './router/index'

axios.defaults.timeout = 10000;                        //响应时间
axios.defaults.headers.post['Content-Type'] ='application/json';//'application/x-www-form-urlencoded;charset=UTF-8';        //配置请求头
//axios.defaults.headers.common['Authorization'] = 'Bearer 1eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJhZG1pTiIsImlkIjoiOGViNjYyMDQtODAyMy1jMWViLTYxNjItMzlmMjJlZjM1ZjVmIiwiYXZhdGFyIjoiIiwiTG9naW5OYW1lIjoiYWRtaW4iLCJkaXNwbGF5TmFtZSI6Iui2hee6p-euoeeQhuWRmCIsInVzZXJUeXBlIjoiMCIsIm5iZiI6MTU3ODEwNjU2NCwiZXhwIjoxNTc4NzExMzY0LCJpYXQiOjE1NzgxMDY1NjR9.h6XRM_0C5nUAck-WlFmXy-0JKJNowMyeMhvMkz0pNCw';
// axios.defaults.baseURL ='http://localhost:5000' //'http://122.51.134.124:5000/';   //配置接口地址
 axios.defaults.baseURL ='http://122.51.134.124:5000' //'http://122.51.134.124:5000/';   //配置接口地址

//POST传参序列化(添加请求拦截器)
axios.interceptors.request.use((config) => {
    //在发送请求之前做某件事

    //如果token存在未每个请求加上token
   let token= window.localStorage.getItem('zero_token')
    if (token) {
        config.headers.Authorization='Bearer '+token
    }
    if (config.method === 'post') {
       // 1.form格式，将Content-Type类型设置为application/x-www-form-urlencode，POST请求时将data序列化,
       //提交的数据会按照 key1=val1&key2=val2 的方式进行编码，key 和 val 都进行了 URL 转码

       //2.json格式，有时候后台需要body传送的是json数据，将Content-Type类型设置为application/json，
       //注意POST请求时data不要序列化

        //后台需要从formbody中获取数据，不能将数据序列化
        //config.data =config.data; //qs.stringify(config.data);
    
    }
    return config;
}, (error) => {
    console.log('错误的传参')
    return Promise.reject(error);
});

//返回状态判断(添加响应拦截器)
axios.interceptors.response.use((res) => {
    //对响应数据做些事
    if (!res.data.success) {
        return Promise.resolve(res);
    }
    return res;
}, (error) => {
    console.log(error);
    if (error.response.status === 401) {
        window.localStorage.removeItem('zero_token')
         router.push('/login')
    }
    return Promise.reject(error);
});

//返回一个Promise(发送post请求)
export function fetchPost(url, params) {
    return new Promise((resolve, reject) => {
        axios.post(url, params)
            .then(response => {
                resolve(response);
            }, err => {
                reject(err);
            })
            .catch((error) => {
                reject(error)
            })
    })
}
////返回一个Promise(发送get请求)
export function fetchGet(url, param) {
    return new Promise((resolve, reject) => {
        axios.get(url, { params: param })
            .then(response => {
                resolve(response)
            }, err => {
                reject(err)
            })
            .catch((error) => {
                reject(error)
            })
    })
}
//返回一个Promise(发送put请求)
export function fetchPut(url, params) {
    return new Promise((resolve, reject) => {
        axios.put(url, params)
            .then(response => {
                resolve(response);
            }, err => {
                reject(err);
            })
            .catch((error) => {
                reject(error)
            })
    })
}

//返回一个Promise(发送delete请求)
export function fetchDelete(url, params) {
    return new Promise((resolve, reject) => {
        axios.delete(url, params)
            .then(response => {
                resolve(response);
            }, err => {
                reject(err);
            })
            .catch((error) => {
                reject(error)
            })
    })
}

////返回一个Promise(发送异步get请求)
export  function fetchGetAsync(url, param) {
    return new Promise((resolve, reject) => {
        axios.get(url, { params: param })
            .then(response => {
                resolve(response)
            }, err => {
                reject(err)
            })
            .catch((error) => {
                reject(error)
            })
    })
}

export default {
    fetchPost,
    fetchGet,
    fetchDelete,
    fetchPut,
    fetchGetAsync
}