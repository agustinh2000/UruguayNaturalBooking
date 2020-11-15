export enum ReserveState {
    Creada, Pendiente_Pago, Aceptada, Rechazada, Expirada
}

export const DescriptionOfState = new Map<number, string>([
    [ReserveState.Creada, 'Creada'],
    [ReserveState.Pendiente_Pago, 'Pendiente de pago'],
    [ReserveState.Aceptada, 'Aceptada'],
    [ReserveState.Rechazada, 'Rechazada'],
    [ReserveState.Expirada, 'Expirada']
]);