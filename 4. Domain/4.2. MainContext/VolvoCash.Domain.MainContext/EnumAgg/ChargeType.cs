using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.EnumAgg
{
    public class ChargeType:Entity
    {
        #region Properties
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public int Weight { get; set; }
        #endregion

        #region Constructor
        public ChargeType()
        {
        }

        public ChargeType(string name, string description, int weight)
        {
            Name = name;
            Description = description;
            Weight = weight;
        }
        #endregion
    }
}
