using System;
using System.Collections.Generic;
using System.Text;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Application.MainContext.DTO.Status
{
    public class StatusDTO : AuditableEntity
    {
        #region Properties
        public string Name { get; set; }

        public string Description { get; set; }

        public byte Active { get; set; }
        #endregion
    }
}
