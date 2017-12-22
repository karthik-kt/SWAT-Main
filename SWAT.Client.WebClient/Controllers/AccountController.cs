using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using System.Threading.Tasks;
using SWAT.Client.WebClient.Models;

public class AccountController : Controller
{
    public ActionResult Login(string returnUrl)
    {
        ViewBag.ReturnUrl = returnUrl;
        return this.View();
    }

    [HttpPost]
    public ActionResult Login(LoginModel model, string returnUrl)
    {
        if (!this.ModelState.IsValid)
        {
            return this.View(model);
        }

        if (Membership.ValidateUser(model.UserName, model.Password))
        {
            FormsAuthentication.RedirectFromLoginPage(model.UserName, model.RememberMe);
            FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
            if (this.Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction("TFSProjects", "TestSuite");
        }

        this.ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");

        return this.View(model);
    }

    public ActionResult LogOff()
    {
        FormsAuthentication.SignOut();

        return this.RedirectToAction("Login", "Account");
    }
}