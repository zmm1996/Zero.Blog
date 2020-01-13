using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zero.Infrastructure.Resources.ViewModels.Rabc;
using Zero.Util.Extensions;

namespace Zero.Web.Api.Extensions
{
    public static class MenuTreeExtension
    {
        /// <summary>
        /// 正常菜单树（向下递归）
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="selectid">当前选中的节点</param>
        /// <returns></returns>
        public static List<MenuTree> BuildTree(this List<MenuTree> menus, Guid? selectid)
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
                    Expand = (x.ParentId == null || x.ParentId == Guid.Empty),
                    Selected = x.Id == selectid,
                    Childen = build(x.Id)
                }).ToList();

            };
            var result = build(null);
            return result;
        }

        /// <summary>
        /// 根据子节点查找父节点
        /// </summary>
        /// <param name="menus">菜单集合</param>
        /// <param name="menuChuildren">子节点集合</param>
        /// <returns></returns>
        public static List<InitMenuTree> UpBuildTree(this IEnumerable<InitMenuTree> menus, IEnumerable<InitMenuTree> menuChuildren, bool isAdmin)
        {

            //递归时每次调用
            Action<Guid?> build = null;
            List<InitMenuTree> treeList = new List<InitMenuTree>();

            if (!isAdmin)
            {
                //查找出所有菜单
                foreach (var item in menuChuildren)
                {
                    treeList.Add(item);
                    build = childrenId =>
                    {
                        var parentLevel = menus.Where(x => x.Id == childrenId).FirstOrDefault();

                        if (parentLevel == null)
                            return;
                        treeList.Add(parentLevel);
                        build(parentLevel.ParentId);
                    };
                    build(item.ParentId);
                }
            }
            else
            {
                treeList = menus.ToList();
            }
            //自定去重方法
            var DistinctTree = treeList.MyDistinct(x => x.Id);

            //子菜单

            var loopUp = DistinctTree.ToLookup(x => x.ParentId);

            Func<Guid?, List<InitMenuTree>> func = null;

            func = pid =>
            {
                return loopUp[pid].Select(x => new InitMenuTree()
                {
                    Alias = x.Alias,
                    Id = x.Id,
                    IsDefaultRouter = x.IsDefaultRouter,
                    Level = x.Level,
                    Children = func(x.Id),
                    Component = x.Component,
                    HideInMenu = x.HideInMenu,
                    Icon = x.Icon,
                    Name = x.Name,
                    ParentId = x.ParentId,
                    ParentName = x.ParentName,
                    Sort = x.Sort,
                    Url = x.Url
                }).OrderBy(x => x.Sort).ToList();
            };



            return func(Guid.Empty);
        }
    }


    /// <summary>
    /// 去重
    /// </summary>
    public class Compare : IEqualityComparer<InitMenuTree>
    {
        public bool Equals(InitMenuTree x, InitMenuTree y)
        {
            return x.Id == y.Id;//可以自定义去重规则，此处将Id相同的就作为重复记录，不管学生的爱好是什么
        }

        public int GetHashCode(InitMenuTree obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
