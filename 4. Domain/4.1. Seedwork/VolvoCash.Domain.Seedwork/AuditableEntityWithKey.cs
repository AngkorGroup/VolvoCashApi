namespace VolvoCash.Domain.Seedwork
{
    public abstract class AuditableEntityWithKey<T> : AuditableEntity, IEntity<T>
    {
        public T Id { get; set; }  
    }
}
