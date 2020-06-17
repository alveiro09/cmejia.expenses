namespace UserManagement.Domain.ValueObjects
{
    public enum IdentityDocumentType
    {
        IDENTITYCARD = 1,
        CEDULA,
        PASSPORT
    }

    public static class IdentityDocumentTypeHelper
    {
        public static string GetName(this IdentityDocumentType input)
        {
            switch (input)
            {
                case IdentityDocumentType.IDENTITYCARD:
                    return "Identity Card";
                case IdentityDocumentType.CEDULA:
                    return "Cedula";
                case IdentityDocumentType.PASSPORT:
                    return "Passport";
                default:
                    return string.Empty;
            }
        }
    }
}
