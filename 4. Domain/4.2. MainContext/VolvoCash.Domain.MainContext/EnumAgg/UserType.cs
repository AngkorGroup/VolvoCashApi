﻿using System.ComponentModel.DataAnnotations;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.EnumAgg
{
    public class UserType:Entity
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
        public UserType()
        {
        }

        public UserType(string name, string description)
        {
            Name = name;
            Description = description;
        }
        #endregion
    }
}