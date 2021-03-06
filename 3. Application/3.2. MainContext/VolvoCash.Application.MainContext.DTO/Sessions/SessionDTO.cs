using System;
using VolvoCash.Domain.MainContext.Enums;

namespace VolvoCash.Application.MainContext.DTO.Sessions
{
    public class SessionDTO 
    {
        #region Properties
        public Guid Id { get; set; }

        public int UserId { get; set; }

        public Status Status { get; set; }

        public string PushDeviceToken { get; set; }
        #endregion
    }
}
