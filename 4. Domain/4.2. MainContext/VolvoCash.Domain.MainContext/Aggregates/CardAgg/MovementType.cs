using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.CardAgg
{
    public class MovementType : Entity
    {
        #region Properties
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(3)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        #endregion

        #region Constructor
        public MovementType()
        {
        }

        public MovementType(string name, string description)
        {
            Name = name;
            Description = description;
        }
        #endregion
    }
}
