namespace libc.extended.Validation
{
    public interface IValidator
    {
        FluentValidator Validate(object item);
    }
}