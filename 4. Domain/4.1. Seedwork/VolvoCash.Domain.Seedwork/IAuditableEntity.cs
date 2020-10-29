﻿using System;

namespace VolvoCash.Domain.Seedwork
{
    public interface IAuditableEntity
    {
        string CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        string LastModifiedBy { get; set; }
        DateTime? LastModifiedAt { get; set; }
    }
}
