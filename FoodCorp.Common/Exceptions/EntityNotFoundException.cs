namespace FoodCorp.Common.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(Guid guid)
    { }

    public EntityNotFoundException(Type type, Guid guid) : base(CreateExceptionMessage(type, guid))
    { }

    public EntityNotFoundException(Type type, string message) : base(CreateExceptionMessage(type, message))
    { }

    private static string CreateExceptionMessage(Guid guid)
    {
        return $"Guid: {guid}.";
    }

    private static string CreateExceptionMessage(Type type, Guid guid)
    {
        return $"{type.Name}. Guid: {guid}.";
    }

    private static string CreateExceptionMessage(Type type, string message)
    {
        return $"{type.Name}. {message}.";
    }
}