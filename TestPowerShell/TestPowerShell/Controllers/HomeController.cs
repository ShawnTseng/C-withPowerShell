using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Web.Mvc;

namespace TestPowerShell.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            WSManConnectionInfo connectionInfo = new WSManConnectionInfo();
            connectionInfo.ComputerName = "";

            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            using (PowerShell ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                ps.AddScript("Get-Process");
                var results = ps.Invoke();
                // Do something with result ... 
                foreach (var result in results)
                {
                    ViewBag.Message += result.Members["Id"].Value + " ";
                    ViewBag.Message += result.Members["ProcessName"].Value + " ";
                    ViewBag.Message += result.Members["PrivateMemorySize64"].Value + "<br>";
                }
            }
            runspace.Close();


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}