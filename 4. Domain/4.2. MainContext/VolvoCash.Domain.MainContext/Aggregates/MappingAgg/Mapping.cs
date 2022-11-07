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
        public string Filter { get; set; }

        [MaxLength(200)]
        public string Version { get; set; }

        [MaxLength(200)]
        public string ReceiverLogicalId { get; set; }

        [MaxLength(200)]
        public string ReceiverComponentId { get; set; }

        [MaxLength(200)]
        public string SenderLogicalId { get; set; }

        [MaxLength(200)]
        public string SenderComponentId { get; set; }

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
            string feeder, string file, string username, string password, string date, string filter, string version,
            string receiverLogicalId, string receiverComponentId, string senderLogicalId, string senderComponentId)
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
            Filter = filter;
            Version = version;
            ReceiverLogicalId = receiverLogicalId;
            ReceiverComponentId = receiverComponentId;
            SenderLogicalId = senderLogicalId;
            SenderComponentId = senderComponentId;
            Status = Status.Active;
        }
        #endregion

        #region Public Methods
        #endregion
    }
}
