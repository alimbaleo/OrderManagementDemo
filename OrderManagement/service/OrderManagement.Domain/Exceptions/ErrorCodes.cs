namespace OrderManagement.Domain.Exceptions
{
    public static class ErrorCodes
    {
        public const int RESOURCE_NOT_FOUND = 404;
        public const int UNAUTHORIZED_OPERATION = 403;
        public const int INVALID_OPERATION = 400;
        public const int BAD_REQUEST = 400;
        public const int CONFLICT = 409;
        public const int INTERNAL_SERVER_ERROR = 500;

    }
}
