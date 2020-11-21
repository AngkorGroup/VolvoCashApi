using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.StatusAgg
{
    public class Status : Entity
    {
        #region Properties
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public byte Active { get; set; }
        #endregion

        #region Constructor
        public Status(int active)
        {
            if (active == 1)
            {
                Name = "Active";
                Description = "Card active";
            }
            else
            {
                Name = "Inactive";
                Description = "Card inactive";
            }
            Active = (byte)active;
        }

        public Status(string name, string description, byte active)
        {
            Name = name;
            Description = description;
            Active = active;
        }
        #endregion
    }
}
