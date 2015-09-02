using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace MyMoney.Domain.Application.WebServices.Controllers
{
    public class AccountsControllerWithHub<THub> : AccountsController
        where THub : IHub
    {
        readonly Lazy<IHubContext> hub = new Lazy<IHubContext>(
          () => GlobalHost.ConnectionManager.GetHubContext<THub>()
        );

        protected IHubContext Hub => hub.Value;
    }

}