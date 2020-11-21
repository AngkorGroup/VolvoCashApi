using System;
using VolvoCash.Application.MainContext.DTO.Status;

namespace VolvoCash.Application.MainContext.DTO.Sessions
{
    public class SessionDTO 
    {
        #region Properties
        public Guid Id { get; set; }

        public int UserId { get; set; }

        public StatusDTO Status { get; set; }

        public string PushDeviceToken { get; set; }
        #endregion
    }
}
