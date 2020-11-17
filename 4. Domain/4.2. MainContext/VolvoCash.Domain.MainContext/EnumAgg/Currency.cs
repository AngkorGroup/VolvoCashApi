using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.EnumAgg
{
    public class Currency:Entity
    {
        #region Properties
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1)]
        public char Symbol { get; set; }

        [Required]
        [MaxLength(3)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        #endregion

        #region Constructor
        public Currency()
        {
        }

        public Currency(string name, char symbol, string code, string description)
        {
            Name = name;
            Symbol = symbol;
            Code = code;
            Description = description;
        }
        #endregion
    }
}
