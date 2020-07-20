export interface CreateExpense {
    name: string;
    description: string;
    value: number;
    idExpenseType: number;
    userNameOwner: string;
    expirationDate: Date;
    idExpenseStatus: number;
    idExpenseRecurrenceType: number;
}
