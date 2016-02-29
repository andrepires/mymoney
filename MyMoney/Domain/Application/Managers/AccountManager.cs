using System;
using MyMoney.Domain.Abstractions.Repositories;
using MyMoney.Domain.Abstractions.Values;
using MyMoney.Domain.BasicImplementations.Entities;

namespace MyMoney.Domain.Application.Managers
{
    public class AccountManager
    {
        private IGenericRepository<Account> AccountRepository { get; set; }

        public AccountManager(IGenericRepository<Account> repository)
        {
            AccountRepository = repository;
        }

        public IOperationResult GetAllAccounts()
        {
            return AccountRepository.GetAll();
        }

        public IOperationResult CreateAccount(Account account)
        {
            //Validate here
            return AccountRepository.Create(account);
        }

        public IOperationResult GetAccountByOId(Guid oid)
        {
            return AccountRepository.GetByOid(oid);
        }

        public IOperationResult UpdateAccount(Guid oid, Account account)
        {
            //Validate here
            return AccountRepository.Update(oid, account);
        }

        public IOperationResult DeleteAccount(Guid oid)
        {
            return AccountRepository.Delete(oid);
        }
    }
}