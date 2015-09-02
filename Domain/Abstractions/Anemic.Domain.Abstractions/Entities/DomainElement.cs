using System;

namespace MyMoney.Domain.Abstractions.Entities
{
    [Serializable]
    public abstract class DomainObject : IDomainObject
    {
        public Guid Oid { get; set; }
        public string CreatedOn { get; set; }
        public string LastModifiedOn { get; set; }

        protected DomainObject()
        {
            Oid = Guid.NewGuid();
            var timeStamp = CreateTimeStamp(DateTime.Now);
            CreatedOn = timeStamp;
            LastModifiedOn = timeStamp;
        }

        private string CreateTimeStamp(DateTime now)
        {
            return now.ToString("yyyyMMddHHmmssffff");
        }
    }
}