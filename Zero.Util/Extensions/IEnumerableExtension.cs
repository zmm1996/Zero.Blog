
/**************************************************************************
*   
*   =================================
*   CLR版本    ：4.0.30319.42000
*   命名空间    ：Zero.Util.Extensions
*   文件名称    ：IEnumerableExtension.cs
*   =================================
*   创 建 者    ：zhangmeng
*   创建日期    ：2020/1/9 14:21:20 
*   邮箱        ：1458976756@qq.com
*   =================================
*  
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zero.Util.Extensions
{

    /// <summary>
    /// 集合扩展方法
    /// </summary>
    public static class IEnumerableExtension
    {

        /// <summary>
        /// 集合去重
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="source"></param>
        /// <param name="getfield"></param>
        /// <returns></returns>
        public static IEnumerable<T> MyDistinct<T,C>(this IEnumerable<T> source,Func<T,C> getfield)
        {
           return source.Distinct(new Compare<T,C>(getfield));
        }
    }

    public class Compare<T, C> : IEqualityComparer<T>
    {
        private readonly Func<T, C> _getfield;

        public Compare(Func<T,C> getfield)
        {
            this._getfield = getfield;
        }
        public bool Equals(T x, T y)
        {
            //可以自定义去重规则，此处将c相同的就作为重复记录，
            return EqualityComparer<C>.Default.Equals(_getfield(x), _getfield(y));
        }

        public int GetHashCode(T obj)
        {
            return EqualityComparer<C>.Default.GetHashCode(this._getfield(obj));
        }
    }
}
