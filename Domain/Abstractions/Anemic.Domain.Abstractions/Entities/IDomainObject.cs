using System;

namespace MyMoney.Domain.Abstractions.Entities
{
    public interface IDomainObject
    {
        Guid Oid { get; set; }
        string CreatedOn { get; set; }
        string LastModifiedOn { get; set; }
    }
}