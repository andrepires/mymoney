using System;
using MyMoney.Domain.Abstractions.Entities;
using MyMoney.Domain.BasicImplementations.Values;

namespace MyMoney.Domain.BasicImplementations.Entities
{
    [Serializable]
    public class Account : DomainObject
    {
        public string Name { get; set; }
        public string Description { get; set; } 
        public AccountTypes AccountType { get; set; }
        public decimal Value { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}