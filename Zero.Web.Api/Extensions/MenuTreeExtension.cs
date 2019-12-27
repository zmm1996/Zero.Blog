using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zero.Infrastructure.Resources.ViewModels.Rabc;

namespace Zero.Web.Api.Extensions
{
    public static class MenuTreeExtension
    {

        public static List<MenuTree> BuildTree(this List<MenuTree> menus,Guid? selectid)
        {
            //lookup类似groupby，创建之后不允许添加删除
           
            var loopUp = menus.ToLookup(x => x.ParentId);
            Func<Guid?, List<MenuTree>> build = null;
            //key为ParentId，传入Id查询子节点parentid为id的
            build = pid =>
            {
                return loopUp[pid].Select(x => new MenuTree()
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    Title = x.Title,
                    Expand = (x.ParentId == null||x.ParentId==Guid.Empty),
                    Selected=x.Id==selectid,
                     Childen=build(x.Id)
                }).ToList();

            };
            var result = build(null);
            return result;
        }
    }
}
