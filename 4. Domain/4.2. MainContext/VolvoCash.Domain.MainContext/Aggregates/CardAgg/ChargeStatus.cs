using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.CardAgg
{
    public class ChargeStatus : AuditableEntityWithKey<int>
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
        public ChargeStatus()
        {
        }

        public ChargeStatus(string name, string description, int weight)
        {
            Name = name;
            Description = description;
            Weight = weight;
        }
        #endregion
    }
}
