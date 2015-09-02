using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MyMoney.Domain.BasicImplementations.Entities;

namespace MyMoney.Domain.Infrastructure.EntityFramework.Mappings
{
    public class AccountMappings : EntityTypeConfiguration<Account>
    {
        public AccountMappings()
        {
            ToTable("Accounts");
            HasKey(e => e.Oid);
        }
    }
}