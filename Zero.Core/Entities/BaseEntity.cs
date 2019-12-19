using System;
using System.Collections.Generic;
using System.Text;
using Zero.Core.Intefaces;

namespace Zero.Core.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set ; }
    }
}
