export interface Expense {
    id: string;
    name: string;
    description: string;
    value: number;
    paidOut: boolean;
    created: Date;
    datePaidOut: Date;
    idExpenseType: number;
    userNameOwner: string;
    expirationDate: Date;
    idExpenseStatus: number;
    idExpenseRecurrenceType: number;
}
