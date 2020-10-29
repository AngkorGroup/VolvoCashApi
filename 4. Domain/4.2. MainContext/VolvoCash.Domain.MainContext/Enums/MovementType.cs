namespace VolvoCash.Domain.MainContext.Enums
{
    public enum MovementType
    {
        CTA, // Creación de tarjeta
        REC, // Recarga
        ITR, // Ingreso por transferencia
        STR, // Salida por transferencia
        CON, // Consumo
        CVT, // Cancela por vencimiento
        AVT  // Amplía vencimiento
    }
}
