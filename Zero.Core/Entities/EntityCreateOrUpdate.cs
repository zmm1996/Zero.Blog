using System;
using Zero.Util.Helpers;
using Zero.Web.Util.Extensions.AuthContext;

namespace Zero.Core.Entities
{
    public class EntityCreateOrUpdate
    {
        public void Create()
        {
            var entity = this as IBaseEntity;
            entity.Id = NumberNo.SequentialGuid();
            entity.CreatedByUserId = AuthContextService.CurrentUser.Guid;
            entity.CreatedByUserName = AuthContextService.CurrentUser.LoginName;
            entity.CreatedTime = DateTime.Now;

            entity.ModifiedByUserId = AuthContextService.CurrentUser.Guid;
            entity.ModifiedByUserName = AuthContextService.CurrentUser.LoginName;
            entity.ModifiedTime = DateTime.Now;
        }
        public void Update()
        {
            var entity = this as IBaseEntity;
          
            entity.ModifiedByUserId = AuthContextService.CurrentUser.Guid;
            entity.ModifiedByUserName = AuthContextService.CurrentUser.LoginName;
            entity.ModifiedTime = DateTime.Now;
        }

    }
}
