namespace VolvoCash.Domain.Seedwork
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
