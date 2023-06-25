using OpenQA.Selenium;
using System;
using System.Reflection;
using Zionet.Automation.Framework.Common;
using Zionet.Automation.Framework.Drivers.Guest;
using Zionet.Automation.Framework.Drivers.Photographer;
using Zionet.Automation.Framework.Services;
using Zionet.Automation.Framework.Services.Reporter;
using static Zionet.Automation.Framework.Common.Enums.Auth0.Auth0Enums;
using static Zionet.Automation.Framework.Common.Enums.GallerU.CommonEnums;
using static Zionet.Automation.Framework.Common.Enums.GallerU.Photographer.PhotographerEnums;

namespace Zionet.Automation.Framework.Pages.Guest
{
    public class Guest
    {
        private readonly GuestHandler guestHandler = new GuestHandler();
        public Notification notification = new Notification();
        public bool isAuthentication(IWebDriver driver)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            return guestHandler.isAuthentication(driver);
        }

        public void Login(IWebDriver driver, Auth0Type loginType, Login_Email loginEmail, Login_Password loginPassword, EventsButtons eventsButtons, TimeSpan? timeout = null)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            guestHandler.Login(driver, loginType, loginEmail, loginPassword, eventsButtons, timeout);
        }

        public void OpenCamera(IWebDriver driver, TimeSpan? timeout = null)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            guestHandler.OpenCamera(driver, timeout);
        }

        public void TakeSelfi(IWebDriver driver, TimeSpan? timeout = null)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            guestHandler.TakeSelfi(driver, timeout);
        }

        public void UseThisPicture(IWebDriver driver, TimeSpan? timeout = null)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            guestHandler.UseThisPicture(driver, timeout);
        }

        public void RetakePicture(IWebDriver driver, TimeSpan? timeout = null)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            guestHandler.RetakePicture(driver, timeout);
        }

        public void Logout(IWebDriver driver, TimeSpan? timeout = null)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            guestHandler.Logout(driver, timeout);
        }

        public void CatchAlert(IWebDriver driver, NotificationState Request, NotificationState Comment)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            guestHandler.CatchAlert(driver, notification, Request, Comment);
        }
    }
}
