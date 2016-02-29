using System;
using MyMoney.Domain.Abstractions.Entities;

namespace MyMoney.Domain.BasicImplementations.Values
{
    public class MyMoneyConfiguration : DomainObject
    {
        public Guid Oid { get; set; }
        public string PersistenceMode { get; set; }
        public string CreatedOn { get; set; }
        public string LastModifiedOn { get; set; }
    }
}