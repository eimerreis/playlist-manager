using System;

namespace Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(): base() {}
        public NotFoundException(Type type, string identifier): base($"{type.Name} with identifier '{identifier}' does not exist") {}
    }
}
