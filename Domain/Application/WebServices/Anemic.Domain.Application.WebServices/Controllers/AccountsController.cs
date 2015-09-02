using System;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Services.Description;
using Microsoft.AspNet.SignalR;
using MyMoney.Domain.Application.Managers;
using MyMoney.Domain.Application.WebServices.Hubs;
using MyMoney.Domain.BasicImplementations.Entities;
using MyMoney.Domain.Configuration;

namespace MyMoney.Domain.Application.WebServices.Controllers
{
    //REST - HTTP
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountsController : ApiController
    {
        private AccountManager AccountManager { get; set; }

        readonly Lazy<IHubContext> hub = new Lazy<IHubContext>(
          () => GlobalHost.ConnectionManager.GetHubContext<AccountsHub>()
        );

        protected IHubContext Hub => hub.Value;

        public AccountsController()
        {
            AccountManager = Factory.Get<AccountManager>();
        }

        [Route("api/Accounts")]
        public IHttpActionResult Get()
        {
            try
            {
                var operationResult = AccountManager.GetAllAccounts();
                if (operationResult.Errors.Count > 0)
                {
                    return InternalServerError(string.Concat(operationResult.Errors));
                }
                var accounts = operationResult.Oks[0];
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/Accounts/{oid:guid}")]
        public IHttpActionResult Get(Guid oid)
        {
            try
            {
                var operationResult = AccountManager.GetAccountByOId(oid);
                if (operationResult.Errors.Count > 0 && operationResult.Errors[0].Contains("not found"))
                {
                    return NotFound();
                }
                var account = operationResult.Oks[0] as Account;
                return Ok(account);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/Accounts")]
        public IHttpActionResult Post([FromBody]Account value)
        {
            try
            {
                var operationResult = AccountManager.CreateAccount(value);
                if (operationResult.Errors.Count > 0)
                {
                    return BadRequest("The account is probably in an invalid state...");
                }
                return Ok(operationResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/Accounts/{oid:guid}")]
        public IHttpActionResult Put(Guid oid, [FromBody]Account value)
        {
            try
            {
                var operationResult = AccountManager.UpdateAccount(oid, value);
                return Ok(operationResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [Route("api/Accounts/{oid:guid}")]
        public IHttpActionResult Delete(Guid oid)
        {
            try
            {
                var operationResult = AccountManager.DeleteAccount(oid);
                if (operationResult.Errors.Count == 0)
                {
                    Hub.Clients.All.accountWasDeleted(oid);
                    return Ok(operationResult);
                }
                return BadRequest("Errors: " + string.Concat(operationResult.Errors));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}