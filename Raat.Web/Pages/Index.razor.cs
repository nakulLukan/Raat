using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Raat.Web.Pages
{
    public class IndexBase : ComponentBase
    {
        public string WelcomeText = "Welcome to RaaT";

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected void Click()
        {
            Console.WriteLine("Clicked me");
        }
    }
}
