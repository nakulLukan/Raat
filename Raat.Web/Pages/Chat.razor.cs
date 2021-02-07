using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raat.Web.Pages
{
    public class ChatBase : ComponentBase
    {
        [Parameter]
        public string ConnectionId { get; set; }
    }
}
