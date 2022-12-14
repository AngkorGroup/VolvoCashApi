using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.MappingAgg
{
    public class Mapping : AuditableEntityWithKey<int>
    {
        #region Properties
        [MaxLength(200)]
        public string MappingNumber { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Type { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [MaxLength(200)]
        public string Company { get; set; }

        [MaxLength(200)]
        public string Feeder { get; set; }

        [MaxLength(200)]
        public string File { get; set; }

        [MaxLength(200)]
        public string Username { get; set; }

        [MaxLength(200)]
        public string Password { get; set; }

        [MaxLength(200)]
        public string Date { get; set; }

        [MaxLength(200)]
        public string Filler { get; set; }

        [MaxLength(200)]
        public string Version { get; set; }

        [MaxLength(200)]
        public string ReceiverLogicalID { get; set; }

        [MaxLength(200)]
        public string ReceiverComponentID { get; set; }

        [MaxLength(200)]
        public string SenderLogicalID { get; set; }

        [MaxLength(200)]
        public string SenderComponentID { get; set; }

        public DateTime? ArchiveAt { get; set; }

        public Status Status { get; set; }

        public virtual ICollection<MappingHeader> MappingHeaders { get; set; } = new List<MappingHeader>();
        #endregion

        #region NotMapped Properties
        #endregion

        #region Constructor
        public Mapping()
        {
        }

        public Mapping(string mappingNumber, string name, string type, string description, string company,
            string feeder, string file, string username, string password, string date, string filler, string version,
            string receiverLogicalID, string receiverComponentID, string senderLogicalID, string senderComponentID)
        {
            MappingNumber = mappingNumber;
            Name = name;
            Type = type;
            Description = description;
            Company = company;
            Feeder = feeder;
            File = file;
            Username = username;
            Password = password;
            Date = date;
            Filler = filler;
            Version = version;
            ReceiverLogicalID = receiverLogicalID;
            ReceiverComponentID = receiverComponentID;
            SenderLogicalID = senderLogicalID;
            SenderComponentID = senderComponentID;
            Status = Status.Active;
        }
        #endregion

        #region Public Methods
        #endregion
    }
}
