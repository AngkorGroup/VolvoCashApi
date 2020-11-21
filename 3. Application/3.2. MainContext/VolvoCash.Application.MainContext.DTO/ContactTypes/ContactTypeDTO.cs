using System;
using System.Collections.Generic;
using System.Text;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Application.MainContext.DTO.ContactTypes
{
    public class ContactTypeDTO : AuditableEntity
    {
        #region Properties
        public string Name { get; set; }

        public string Description { get; set; }
        #endregion
    }
}
