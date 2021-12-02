using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GruzD.Web.Hubs
{
    public interface IClientNotificator
    {
        object Data { get; set; }

        long UserId { get; set; }

        long RoleId { get; set; }
    }
}
