using System;

namespace VolvoCash.Domain.Seedwork
{
    public abstract class AuditableEntity : Entity, IAuditableEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}
