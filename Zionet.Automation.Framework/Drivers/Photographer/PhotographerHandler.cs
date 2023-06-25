using OpenQA.Selenium;
using System;
using Zionet.Automation.Framework.Drivers.CommonGUI;
using Zionet.Automation.Framework.Services;
using static Zionet.Automation.Framework.Common.Enums.GallerU.CommonEnums;
using static Zionet.Automation.Framework.Common.Enums.Auth0.Auth0Enums;
using System.Threading;
using static Zionet.Automation.Framework.Common.Enums.GallerU.Photographer.PhotographerEnums;
using Zionet.Automation.Framework.Common;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;
using Zionet.Automation.Framework.Common.Enums.GallerU;
using System.Xml.Linq;
using Zionet.Automation.Framework.Services.Reporter;

namespace Zionet.Automation.Framework.Drivers.Photographer
{
    public class PhotographerHandler
    {
        internal bool isAuthentication0(IWebDriver driver)
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

        internal bool isAuthentication(IWebDriver driver)
        {
            ReportManager.Driver("isAuthentication Steps Start");

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            var token = (String)js.ExecuteScript("return localStorage.getItem('TOKEN')");

            if (token != null)
            {
                ReportManager.Driver($"TOKEN >> '{token}'");
                return true;
            }

            return false;
        }

        internal bool isPasswordCorrect(IWebDriver driver)
        {

            ReportManager.Driver("isPasswordCorrect Steps Start");

            // Find the element that has ::before content
            var listElements = driver.FindElements(By.CssSelector("li"));
            string script = "return window.getComputedStyle(arguments[0], '::before').getPropertyValue('content')";

            //check if we have 6 line in the password contain
            int checks = 0;
            foreach (var item in listElements)
            {
                // Execute JavaScript to get the content value of ::before
                var el = (string)((IJavaScriptExecutor)driver).ExecuteScript(script, item);

                if (el.Contains("✓"))
                {
                    checks++;
                }
            }

            if (checks == 6)
            {
                ReportManager.Driver("isPasswordCorrect Steps End");
                return true;
            }

            ReportManager.Driver("isPasswordCorrect Steps End");
            return false;
        }

        internal void SignUp(IWebDriver driver, SignUp_Email email, SignUp_Password password, TimeSpan? timeout)
        {
            CommonGUIHandler.GoToURL(driver, URLs.Photograoher, timeout);
            CommonGUIHandler.ClickBtn(driver, GeneralButtons.Login);
            CommonGUIHandler.ClickBtn(driver, Auth0Buttons.SignUp);
            CommonGUIHandler.SignUp(driver, email, password);
        }

        internal void ContinueToLogin(IWebDriver driver)
        {
            CommonGUIHandler.ClickBtn(driver, Auth0Buttons.Continue);
        }

        internal void Login(IWebDriver driver, Auth0Type loginType, Login_Email loginEmail, Login_Password loginPassword, TimeSpan? timeout)
        {
            CommonGUIHandler.GoToURL(driver, URLs.Photograoher, timeout);
            //CommonGUIHandler.ClickBtn(driver, GeneralButtons.Login);
            CommonGUIHandler.AuthenticationInput(driver, loginType, loginEmail, loginPassword);
        }

        internal void GoGuestURL(IWebDriver driver, TimeSpan? timeout)
        {
            CommonGUIHandler.GoToURL(driver, URLs.Guest, timeout);
        }

        internal void Logout(IWebDriver driver, TimeSpan? timeout)
        {
            CommonGUIHandler.ClickSideBar(driver, SideBar.Profile);
            CommonGUIHandler.ClickBtn(driver, GeneralButtons.LogOut);
        }

        internal void AddNewEvent(IWebDriver driver, string eventName = null, DateTime? dateTime = null, TimeSpan? timeout = null)
        {
            CommonGUIHandler.ClickSideBar(driver, SideBar.Calender);
            CommonGUIHandler.ClickBtn(driver, Buttons.AddEvent);
            CommonGUIHandler.AddNewEventInput(driver, eventName, dateTime);
            CommonGUIHandler.ClickBtn(driver, Buttons.CreateEvent);
        }
        internal void DeleteRecentEvent(IWebDriver driver)
        {
            CommonGUIHandler.ClickRecentEvent(driver);
            Thread.Sleep(TimeSpan.FromSeconds(2));
            CommonGUIHandler.ClickBtn(driver, EventsButtons.Delete);
            CommonGUIHandler.ClickBtn(driver, EventsButtons.AlertConfirm);
        }

        //for testing all inputs
        internal void CreateNewEvent(IWebDriver driver, CreateEventInputs eventname = CreateEventInputs.None, CreateEventInputs eventtype = CreateEventInputs.None, CreateEventInputs eventowner = CreateEventInputs.None, CreateEventInputs eventmobilephone = CreateEventInputs.None, CreateEventInputs eventowneremail = CreateEventInputs.None, CreateEventInputs eventfolder = CreateEventInputs.None, DateTime? dateTime = null, TimeSpan? timeout = null)
        {
            CommonGUIHandler.ClickSideBar(driver, SideBar.Calender);
            CommonGUIHandler.ClickBtn(driver, Buttons.AddEvent);
            CommonGUIHandler.CreateEventInput(driver, eventname, eventtype, eventowner, eventmobilephone, eventowneremail, eventfolder, dateTime);
            CommonGUIHandler.ClickBtn(driver, Buttons.CreateEvent);
        }

        internal void HomeClick(IWebDriver driver, TimeSpan? timeout)
        {
            CommonGUIHandler.ClickSideBar(driver, SideBar.Home);
        }

        internal void ProfileClick(IWebDriver driver, TimeSpan? timeout)
        {
            CommonGUIHandler.ClickSideBar(driver, SideBar.Profile);
        }

        internal void OpenRecentEvent(IWebDriver driver, TimeSpan? timeout)
        {
            CommonGUIHandler.ClickSideBar(driver, SideBar.Home);
            CommonGUIHandler.ClickRecentEvent(driver);
        }

        internal void PastEventClick(IWebDriver driver, TimeSpan? timeout)
        {
            CommonGUIHandler.ShowEventList(driver, Events.PastEvents);
        }

        internal void UpcomingEventClick(IWebDriver driver, TimeSpan? timeout)
        {
            CommonGUIHandler.ShowEventList(driver, Events.UpcomingEvents);
        }

        internal void EventURLButtons(IWebDriver driver, EventsButtons button, TimeSpan? timeout)
        {
            CommonGUIHandler.ClickBtn(driver, Buttons.EventURL);

            switch (button)
            {
                case EventsButtons.CopyToClipBoard:
                    {
                        CommonGUIHandler.ClickBtn(driver, button);
                        CommonGUIHandler.GetURLGuestFromCopy();
                        break;
                    }

                case EventsButtons.QRCodeAsPdf:
                    {
                        //TODO
                        break;
                    }

                case EventsButtons.QRCodeAsSvg:
                    {
                        //TODO
                        break;
                    }

                case EventsButtons.OpenInNewTap:
                    {
                        CommonGUIHandler.ClickBtn(driver, button);
                        break;
                    }
                default:
                    ReportManager.Error($"Button >'{button}' doesn't supported");
                    break;
            }
        }

        internal void UploadPhotos(IWebDriver driver)
        {
            CommonGUIHandler.ClickRecentEvent(driver);
            Thread.Sleep(TimeSpan.FromSeconds(2));
            CommonGUIHandler.UploadPhotos(driver);
        }

        internal void CatchAlert(IWebDriver driver, Notification notification, NotificationState Request, NotificationState Comment)
        {
            CommonGUIHandler.CatchAlert(driver, notification, Request, Comment);
        }
    }
}
