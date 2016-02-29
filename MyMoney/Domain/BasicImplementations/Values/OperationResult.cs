using System.Collections.Generic;
using MyMoney.Domain.Abstractions.Entities;
using MyMoney.Domain.Abstractions.Values;

namespace MyMoney.Domain.BasicImplementations.Values
{
    public class OperationResult : DomainObject, IOperationResult
    {
        public dynamic Errors { get; set; }
        public dynamic Oks { get; set; }

        public OperationResult()
        {
            Errors = new List<dynamic>();
            Oks = new List<dynamic>();
        }

        public static OperationResult operator &(OperationResult me, OperationResult other)
        {
            foreach (var ok in other.Oks)
            {
                me.Oks.Add(ok);
            }
            foreach (var error in other.Errors)
            {
                me.Errors.Add(error);
            }
            return me;
        }
    }
}