using LinFx.Domain.Exceptions;
using System;

namespace Catalog.Domain.Exceptions
{
    public class CatalogDomainException : LinFxDomainException
    {
        public CatalogDomainException()
        { }

        public CatalogDomainException(string message)
            : base(message)
        { }

        public CatalogDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
