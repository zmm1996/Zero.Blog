using System;
using System.Collections.Generic;
using System.Text;

namespace Zero.Core.Response
{
   public class ResponseResultModel:ResponseModel
    {

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="totalCount"></param>
        public void SetData(object data, int totalCount = 0)
        {
            Data = data;
            TotalCount = totalCount;
        }
    }
}
