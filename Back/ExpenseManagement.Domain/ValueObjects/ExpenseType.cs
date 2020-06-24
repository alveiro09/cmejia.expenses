namespace ExpenseManagement.Domain.ValueObjects
{
    public enum ExpenseType
    {
        FIXED = 1,
        VARIABLE,
        UNEXPECTED,
        ANT,
        FLEXIBLE,
        OTHER
    }

    public static class ExpenseTypeHelper
    {
        public static string GetName(this ExpenseType input)
        {
            switch (input)
            {
                case ExpenseType.FIXED:
                    return "Fixed";
                case ExpenseType.VARIABLE:
                    return "Variable";
                case ExpenseType.UNEXPECTED:
                    return "Unexpected";
                case ExpenseType.ANT:
                    return "Ant";
                case ExpenseType.FLEXIBLE:
                    return "Flexible";
                case ExpenseType.OTHER:
                    return "Other";
                default:
                    return string.Empty;
            }
        }
    }
}
