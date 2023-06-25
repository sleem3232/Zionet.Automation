using OpenQA.Selenium;
using System;
using System.Reflection;
using System.Xml.Linq;
using Zionet.Automation.Framework.Common;
using Zionet.Automation.Framework.Drivers.Photographer;
using Zionet.Automation.Framework.Services;
using Zionet.Automation.Framework.Services.Reporter;
using static Zionet.Automation.Framework.Common.Enums.Auth0.Auth0Enums;
using static Zionet.Automation.Framework.Common.Enums.GallerU.CommonEnums;
using static Zionet.Automation.Framework.Common.Enums.GallerU.Photographer.PhotographerEnums;

namespace Zionet.Automation.Framework.Pages.Photographer
{
    public class Photographer
    {
        private readonly PhotographerHandler photographerHandler = new PhotographerHandler();

        public Notification notification = new Notification();


        public bool isAuthentication(IWebDriver driver)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            return photographerHandler.isAuthentication(driver);
        }

        public bool isPasswordCorrect(IWebDriver driver)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            return photographerHandler.isPasswordCorrect(driver);
        }

        public void SignUp(IWebDriver driver, SignUp_Email email, SignUp_Password password, TimeSpan? timeout = null)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            photographerHandler.SignUp(driver, email, password, timeout);
        }

        public void ContinueToLogin(IWebDriver driver)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            photographerHandler.ContinueToLogin(driver);
        }
        public void Login(IWebDriver driver, Auth0Type loginType, Login_Email loginEmail, Login_Password loginPassword, TimeSpan? timeout = null)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            photographerHandler.Login(driver, loginType, loginEmail, loginPassword, timeout);
        }

        public void GoHome(IWebDriver driver)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
          //  photographerHandler.HomeClick(driver);
        }

        public void AddNewEvent(IWebDriver driver, string eventName = null, DateTime? dateTime = null, TimeSpan? timeout = null)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            photographerHandler.AddNewEvent(driver, eventName, dateTime, timeout);
        }

        //for testing all inputs
        public void CreateNewEvent(IWebDriver driver, CreateEventInputs eventname = CreateEventInputs.None, CreateEventInputs eventtype = CreateEventInputs.None, CreateEventInputs eventowner = CreateEventInputs.None, CreateEventInputs eventmobilephone = CreateEventInputs.None, CreateEventInputs eventowneremail = CreateEventInputs.None, CreateEventInputs eventfolder = CreateEventInputs.None, DateTime? dateTime = null, TimeSpan? timeout = null)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            photographerHandler.CreateNewEvent(driver, eventname, eventtype, eventowner, eventmobilephone, eventowneremail, eventfolder, dateTime, timeout);
        }

        public void GoProfile(IWebDriver driver, TimeSpan? timeout = null)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            photographerHandler.ProfileClick(driver, timeout);
        }

        public void Logout(IWebDriver driver, TimeSpan? timeout = null)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            photographerHandler.Logout(driver, timeout);
        }

        public void GoGuestURL(IWebDriver driver, TimeSpan? timeout = null)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            photographerHandler.GoGuestURL(driver, timeout);
        }

        public void GoPastEvents(IWebDriver driver, TimeSpan? timeout = null)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            photographerHandler.PastEventClick(driver, timeout);
        }

        public void GoUpcomingEvents(IWebDriver driver, TimeSpan? timeout = null)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            photographerHandler.UpcomingEventClick(driver, timeout);
        }

        public void OpenRecentEvent(IWebDriver driver, TimeSpan? timeout = null)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            photographerHandler.OpenRecentEvent(driver, timeout);
        }

        public void EventURLButtons(IWebDriver driver, EventsButtons button, TimeSpan? timeout = null)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            photographerHandler.EventURLButtons(driver, button, timeout);
        }

        public void DeleteRecentEvent(IWebDriver driver)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            photographerHandler.DeleteRecentEvent(driver);
        }

        public void Uploadphoto(IWebDriver driver, TimeSpan? timeout = null)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            photographerHandler.UploadPhotos(driver);
        }

        public void CatchAlert(IWebDriver driver, NotificationState Request, NotificationState Comment)
        {
            ReportManager.Test(MethodBase.GetCurrentMethod().Name);
            ReportManager.Component(MethodBase.GetCurrentMethod().Name);
            photographerHandler.CatchAlert(driver, notification, Request, Comment);
        }
    }
}
