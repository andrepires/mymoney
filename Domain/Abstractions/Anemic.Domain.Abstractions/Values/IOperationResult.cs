using System.Collections.Generic;

namespace MyMoney.Domain.Abstractions.Values
{
    public interface IOperationResult
    {
        dynamic Errors { get; set; }
        dynamic Oks { get; set; }
    }
}