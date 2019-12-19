using System;
using System.Collections.Generic;
using System.Text;

namespace Zero.Util.Helpers
{
    public class NumberNo
    {

        /// <summary>
        /// 表示全局唯一标识符 (GUID)。
        /// </summary>
        /// <returns></returns>
        public static Guid GetGuid()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// 创建有序GUID
        /// </summary>
        /// <returns></returns>
        public static Guid SequentialGuid()
        {
            System.Threading.Thread.Sleep(1);
            return SequentialGuidGenerator.Instance
                .Create(SequentialGuidGenerator.SequentialGuidDatabaseType.SqlServer);
        }

        /// <summary>
        ///根据时间成功编号 201008251145409865
        /// </summary>
        /// <returns></returns>
        public static string CreateNo()
        {
            Random random = new Random();
            string strRandom = random.Next(1000, 10000).ToString();
            string no = DateTime.Now.ToString("yyyyMMddHHmmss") + strRandom;
            return no;
        }

    }
}
