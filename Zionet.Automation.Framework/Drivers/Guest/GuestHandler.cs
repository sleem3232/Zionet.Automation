using OpenQA.Selenium;
using System;
using System.Threading;
using System.Xml.Linq;
using Zionet.Automation.Framework.Common;
using Zionet.Automation.Framework.Common.Enums.GallerU.Guest;
using Zionet.Automation.Framework.Drivers.CommonGUI;
using Zionet.Automation.Framework.Services;
using Zionet.Automation.Framework.Services.Reporter;
using static Zionet.Automation.Framework.Common.Enums.Auth0.Auth0Enums;
using static Zionet.Automation.Framework.Common.Enums.GallerU.CommonEnums;
using static Zionet.Automation.Framework.Common.Enums.GallerU.Photographer.PhotographerEnums;

namespace Zionet.Automation.Framework.Drivers.Guest
{
    public class GuestHandler
    {
        internal bool isAuthentication(IWebDriver driver)
        {
            var cookies = driver.Manage().Cookies.AllCookies;
            int check = 0;

            foreach (var cookie in cookies)
            {
                if ((cookie.Name.Contains(".is.authenticated") == true) && (cookie.Value == "true"))
                {
                    check++;
                }
            }

            if (check != 0)
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                var token = (String)js.ExecuteScript("return localStorage.getItem('TOKEN')");

                if (token != null)
                {
                    ReportManager.Driver("Authentication PASS");
                    ReportManager.Driver($"TOKEN >> '{token}'");
                }

                return true;
            }

            ReportManager.Error("Authentication NULL");
            return false;
        }


        internal void Login(IWebDriver driver, Auth0Type loginType, Login_Email loginEmail, Login_Password loginPassword, EventsButtons eventsButtons, TimeSpan? timeout)
        {
            Thread.Sleep(5000);
            switch (eventsButtons)
            {
                case EventsButtons.CopyToClipBoard:
                    CommonGUIHandler.GoToURL(driver, URLs.Guest, timeout);
                    Thread.Sleep(5000);

                    CommonGUIHandler.AuthenticationInput(driver, loginType, loginEmail, loginPassword);
                    break;

                case EventsButtons.QRCodeAsSvg:
                    //TODO 
                    break;

                case EventsButtons.OpenInNewTap:
                    CommonGUIHandler.AuthenticationInput(driver, loginType, loginEmail, loginPassword);
                    break;

                case EventsButtons.QRCodeAsPdf:
                    //TODO
                    break;

                default:
                    break;
            }
            //CommonGUIHandler.ClickBtn(driver, GeneralButtons.Login);
        }

        internal void Logout(IWebDriver driver, TimeSpan? timeout)
        {
            CommonGUIHandler.ClickSideBar(driver, SideBar.Profile);
            CommonGUIHandler.ClickBtn(driver, GeneralButtons.LogOut);
        }

        internal void OpenCamera(IWebDriver driver, TimeSpan? timeout)
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
            CommonGUIHandler.ClickBtn(driver, GuestEnums.Buttons.OpenCamera);
        }

        internal void TakeSelfi(IWebDriver driver, TimeSpan? timeout)
        {
            CommonGUIHandler.ClickBtn(driver, GuestEnums.Buttons.SelfiBtn);
        }

        internal void UseThisPicture(IWebDriver driver, TimeSpan? timeout)
        {
            CommonGUIHandler.ClickCheckBoxes(driver, GuestEnums.CheckBoxes.CheckBox);
            CommonGUIHandler.ClickBtn(driver, GuestEnums.Buttons.UseThisPictur);
        }

        internal void RetakePicture(IWebDriver driver, TimeSpan? timeout)
        {
            CommonGUIHandler.ClickBtn(driver, GuestEnums.Buttons.RetakePicture);
        }

        internal void CatchAlert(IWebDriver driver, Notification notification, NotificationState Request, NotificationState Comment)
        {
            CommonGUIHandler.CatchAlert(driver, notification, Request, Comment);
        }
    }
}
