using System;
using Zero.Web.Util.Extensions.AuthContext;

namespace Zero.Core.Entities
{
    public class EntityCreateOrUpdate
    {
        public void Create()
        {
            var entity = this as IBaseEntity;
            entity.Id = Guid.NewGuid();
            entity.CreatedByUserId = AuthContextService.CurrentUser.Guid;
            entity.CreatedByUserName = AuthContextService.CurrentUser.DisplayName;
            entity.CreatedTime = DateTime.Now;

            entity.ModifiedByUserId = AuthContextService.CurrentUser.Guid;
            entity.ModifiedByUserName = AuthContextService.CurrentUser.DisplayName;
            entity.ModifiedTime = DateTime.Now;
        }
        public void Update()
        {
            var entity = this as IBaseEntity;
          
            entity.ModifiedByUserId = AuthContextService.CurrentUser.Guid;
            entity.ModifiedByUserName = AuthContextService.CurrentUser.DisplayName;
            entity.ModifiedTime = DateTime.Now;
        }

    }
}
