using System;
using System.Collections.Generic;
using System.Text;

namespace Zero.Core.Response
{
   public class ResponseModel
    {
        public ResponseModel()
        {
            Code = 200;
            Message = "操作成功";
        }
        public string Message{ get; set; }

        public int Code { get; set; }
        public object Data { get; set; }


        /// <summary>
        /// 设置状态为成功
        /// </summary>
        /// <param name="message"></param>
        public void SetSuccess(string message="操作成功")
        {
            Code = 200;
            Message = message;
        }

        /// <summary>
        /// 设置响应状态为失败
        /// </summary>
        /// <param name="message"></param>
        public void SetFailed(string message = "失败")
        {
            Message = message;
            Code = 999;
        }

        /// <summary>
        /// 设置响应状态为体验版(返回失败结果)
        /// </summary>
        /// <param name="message"></param>
        public void SetIsTrial(string message = "体验版,功能已被关闭")
        {
            Message = message;
            Code = 999;
        }

        /// <summary>
        /// 设置响应状态为错误
        /// </summary>
        /// <param name="message"></param>
        public void SetError(string message = "错误")
        {
            Code = 500;
            Message = message;
        }

        /// <summary>
        /// 设置响应状态为未知资源
        /// </summary>
        /// <param name="message"></param>
        public void SetNotFound(string message = "未知资源")
        {
            Message = message;
            Code = 404;
        }

        /// <summary>
        /// 设置响应状态为无权限
        /// </summary>
        /// <param name="message"></param>
        public void SetNoPermission(string message = "无权限")
        {
            Message = message;
            Code = 401;
        }

        /// <summary>
        /// 设置响应数据
        /// </summary>
        /// <param name="data"></param>
        public void SetData(object data)
        {
            Data = data;
        }




    }
}
