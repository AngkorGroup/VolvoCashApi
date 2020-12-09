namespace VolvoCash.Domain.MainContext.Enums
{
    public enum MovementType
    {
        CTA = 0, // Creación de tarjeta
        REC = 1, // Recarga
        ITR = 2, // Ingreso por transferencia
        STR = 3, // Salida por transferencia
        CON = 4, // Consumo
        CVT = 5, // Cancela por vencimiento
        AVT = 6  // Amplía vencimiento
    }
}
