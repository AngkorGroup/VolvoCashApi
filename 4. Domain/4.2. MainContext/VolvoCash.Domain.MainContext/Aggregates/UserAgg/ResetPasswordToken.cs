using System.ComponentModel.DataAnnotations.Schema;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.UserAgg
{
    public class ResetPasswordToken : EntityGuid
    {
        #region Properties       
        [ForeignKey("Admin")]
        public int? AdminId { get; set; }

        public virtual Admin Admin { get; set; }

        [ForeignKey("Cashier")]
        public int? CashierId { get; set; }

        public virtual Cashier Cashier { get; set; }

        public Status Status { get; set; }

        public string Token { get; set; }
        #endregion

        #region Constructor
        public ResetPasswordToken()
        {
        }

        public ResetPasswordToken(Admin admin)
        {
            Admin = admin;
            Status = Status.Active;
            Token = RandomGenerator.RandomString(30);
        }
        public ResetPasswordToken(Cashier cashier)
        {
            Cashier = cashier;
            Status = Status.Active;
            Token = RandomGenerator.RandomDigits(5);
        }
        #endregion
    }
}
