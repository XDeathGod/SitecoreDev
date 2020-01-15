using Sitecore;
using Sitecore.Shell.Applications.ContentEditor;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace SitecoreDev.Web.Custom.FIelds
{
    public class CreditCard : Password
    {
        private string _creditCardRegex = @"^\d{4}([\-]?)\d{4}\1\d{4}\1\d{4}$";

        public CreditCard()
        {
            Class = "scContentControl";
        }

        public override void HandleMessage(Message message)
        {
            base.HandleMessage(message);

            if(message.Name == "creditcard:validate")
            {
                string currentvalue = WebUtil.GetFormValue(ID);
                string result = Regex.IsMatch(currentvalue, _creditCardRegex) ? "Valid" : "Invalid";
                SheerResponse.SetInnerHtml("validationResult_" + ID, result);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ServerProperties["Value"] = ServerProperties["Value"];
        }

        protected override void Render(HtmlTextWriter output)
        {
            base.Render(output);

            HtmlGenericControl formatHtml = new HtmlGenericControl("div");
            formatHtml.Attributes.Add("style", "color:#FF0000;");
            formatHtml.InnerHtml = "xxxx-xxxx-xxxx-xxxx";
            formatHtml.RenderControl(output);

            HtmlGenericControl validationHtml = new HtmlGenericControl("div");
            validationHtml.Attributes.Add("ID", "validationResult_" + ID);
            validationHtml.Attributes.Add("style", "color:#FF0000;");
            validationHtml.InnerHtml = "";
            validationHtml.RenderControl(output);
        }

        protected override bool LoadPostData(string value)
        {
            value = StringUtil.GetString(new string[1] { value });

            if (!(Value != value))
                return false;

            Name = value;
            base.SetModified();
            return true;
        }
    }
}