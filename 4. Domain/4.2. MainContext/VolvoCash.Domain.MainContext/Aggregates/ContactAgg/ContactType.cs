using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.ContactAgg
{
    public class ContactType : Entity
    {
        #region Properties
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        #endregion

        #region Constructor
        public ContactType()
        {
        }

        public ContactType(string name, string description)
        {
            Name = name;
            Description = Description;
        }
        #endregion
    }
}
