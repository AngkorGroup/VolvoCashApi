using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.DealerAgg
{
    public class DealerType : Entity
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
        public DealerType()
        {
        }

        public DealerType(string name, string description)
        {
            Name = name;
            Description = description;
        }
        #endregion
    }
}
