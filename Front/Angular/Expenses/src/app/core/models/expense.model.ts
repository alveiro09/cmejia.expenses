export interface Expense {
    id: string;
    name: string;
    description: string;
    value: number;
    paidOut: boolean;
    created: string;
    datePaidOut: string;
    idExpenseType: number;
    userNameOwner: string;
    expirationDate: string;
    idExpenseStatus: number;
    idExpenseRecurrenceType: number;
}
