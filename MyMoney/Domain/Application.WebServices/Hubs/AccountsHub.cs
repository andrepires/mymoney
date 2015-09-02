using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace MyMoney.Domain.Application.WebServices.Hubs
{
    [HubName("accountsHub")]
    public class AccountsHub : Hub
    {
        //Configure Hub here
    }
}