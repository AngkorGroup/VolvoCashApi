using VolvoCash.Domain.Seedwork;

namespace VolvoCash.Domain.MainContext.Aggregates.UserAgg
{
    public interface IResetPasswordTokenRepository : IRepository<ResetPasswordToken>
    {
    }
}
