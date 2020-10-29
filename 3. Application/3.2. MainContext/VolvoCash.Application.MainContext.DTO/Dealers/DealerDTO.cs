using System;

namespace VolvoCash.Application.MainContext.DTO.Dealers
{
    public class DealerDTO
    {
        #region Properties
        public string Name { get; set; }

        public string Ruc { get; set; }

        public string ContactName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string TPCode { get; set; }

        public DateTime? ArchiveAt { get; set; }
        #endregion
    }
}
