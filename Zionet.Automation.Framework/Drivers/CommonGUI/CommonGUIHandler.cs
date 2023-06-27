using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using Zionet.Automation.Framework.Common;
using Zionet.Automation.Framework.Common.Enums.GallerU;
using Zionet.Automation.Framework.Common.Enums.GallerU.Guest;
using Zionet.Automation.Framework.Common.Enums.GallerU.Photographer;
using Zionet.Automation.Framework.Config;
using Zionet.Automation.Framework.Services;
using Zionet.Automation.Framework.Services.Reporter;
using static Zionet.Automation.Framework.Common.Enums.Auth0.Auth0Enums;
using static Zionet.Automation.Framework.Common.Enums.GallerU.CommonEnums;
using static Zionet.Automation.Framework.Common.Enums.GallerU.ConversionDict;
using static Zionet.Automation.Framework.Common.Enums.GallerU.Photographer.PhotographerEnums;

namespace Zionet.Automation.Framework.Drivers.CommonGUI
{
    public abstract class CommonGUIHandler
    {
        //C:\Users\barra\OneDrive\שולחן העבודה\AutoNet7\Zionet.Automation\Zionet.Automation.Framework\Resources\ConfigFile.xml
        protected static IWebDriver driver { get; }
        private static ConfigHelper _configHelper = new ConfigHelper($@"C:\Users\barra\OneDrive\שולחן העבודה\AutoNet7\Zionet.Automation\Zionet.Automation.Framework\Resources\ConfigFile.xml");
        private static TimeSpan _timeoutDefualt = TimeSpan.FromSeconds(30);
        private static TimeSpan _interval = TimeSpan.FromSeconds(1);

        public CommonGUIHandler()
        {
            _timeoutDefualt = TimeSpan.FromSeconds(15);
        }


        /// <summary>
        /// Navigate to URL (According to ConfigFile)
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="type">Photographer or Guest</param>
        /// <param name="timeout"></param>
        public static void GoToURL(IWebDriver driver, URLs? type, TimeSpan? timeout = null)
        {
            string url = string.Empty;
            if (type == URLs.Photograoher)
            {
                url = $@"{_configHelper.GetElement("URL_Photographer", "GallerU")}";
                if (url != string.Empty)
                {
                    ReportManager.Driver($"GO To URL >>> '{url}' <<<");
                }
                else
                {
                    ReportManager.Fatal($"Can't Found Photographer URL");
                }
            }

            if (type == URLs.Guest)
            {
                url = $@"{_configHelper.GetElement("URL_Guest", "GallerU")}";
                if (url != string.Empty)
                {
                    ReportManager.Driver($"GO To URL >>> '{url}' <<<");
                    ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
                    var windowHandles = driver.WindowHandles;
                    driver.SwitchTo().Window(windowHandles[1]);
                }
                else
                {
                    ReportManager.Fatal($"Can't Found Guest URL");
                }
            }

            driver.Navigate().GoToUrl(url);

            TimeSpan waitFor;

            if (timeout == null)
            {
                waitFor = _timeoutDefualt;
                WaitForLoad(driver, waitFor);
            }
            else
            {
                WaitForLoad(driver, (TimeSpan)timeout);
            }
        }

        /// <summary>
        /// Navigate to URL
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        public static void GoToURL(IWebDriver driver, string url, TimeSpan? timeout = null)
        {
            ReportManager.Driver($"GO To URL >>> '{url}' <<<");
            driver.Navigate().GoToUrl(url);

            TimeSpan waitFor;

            if (timeout == null)
            {
                waitFor = _timeoutDefualt;
                WaitForLoad(driver, waitFor);
            }
            else
            {
                WaitForLoad(driver, (TimeSpan)timeout);
            }
        }

        public static void GetURLGuestFromCopy()
        {
            string clipboardText = string.Empty;

            Thread thread = new Thread(() =>
            {
                try
                {
                    //TODO move to.net7
                    //clipboardText = System.Windows.Clipboard.GetText();
                }
                catch (Exception)
                {
                    ReportManager.Error("Can't Get clipboardText");
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            _configHelper.SetElement(clipboardText, "URL_Guest", "GallerU");
            string url = $@"{_configHelper.GetElement("URL_Guest", "GallerU")}";
            if (url != string.Empty)
            {
                ReportManager.Driver($"GUEST URL >>> '{url}' <<<");
            }
            else
            {
                ReportManager.Fatal($"CAN'T GET GUEST URL");
            }
        }

        public static void WaitForLoad(IWebDriver driver, TimeSpan time)
        {
            ReportManager.Driver($"Waiting For Load");
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
            ReportManager.Driver($"Load Success");
        }

        public static void ClickBtn(IWebDriver driver, GeneralButtons button)
        {
            try
            {
                ReportManager.Driver($"Try Click {button} Button");
                string btnLocator = GeneralButtonsDict[button];

                var btn = driver.FindElement(By.Id(btnLocator));

                if (btn != null)
                {
                    btn.Click();
                    ReportManager.Driver($"{button} Was Clicked");
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }

                else
                {
                    ReportManager.Error($"{button} Wasn't Found");
                }
            }
            catch (Exception ex)
            {
                ReportManager.Fatal($"{button} Button Can't Found, \n {ex.Message}");
            }
        }

        public static void ClickBtn(IWebDriver driver, PhotographerEnums.Buttons button)
        {
            try
            {
                ReportManager.Driver($"Try Click {button} Button");
                string btnLocator = PhotographerButtonsDict[button];

                var btn = driver.FindElement(By.Id(btnLocator));

                if (btn != null)
                {
                    btn.Click();
                    ReportManager.Driver($"{button} Was Clicked");
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }

                else
                {
                    ReportManager.Error($"{button} Wasn't Found");
                }
            }
            catch (Exception ex)
            {
                ReportManager.Fatal($"{button} Button Can't Found, \n {ex.Message}");
            }
        }

        public static void ClickBtn(IWebDriver driver, PhotographerEnums.EventsButtons button)
        {
            try
            {
                ReportManager.Driver($"Try Click {button} Button");
                string btnLocator = EventsButtonsDict[button];

                var btn = driver.FindElement(By.Id(btnLocator));

                if (btn != null)
                {
                    btn.Click();
                    ReportManager.Driver($"{button} Was Clicked");
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }

                else
                {
                    ReportManager.Error($"{button} Wasn't Found");
                }
            }
            catch (Exception ex)
            {
                ReportManager.Fatal($"{button} Button Can't Found, \n {ex.Message}");
            }
        }

        public static void ClickBtn(IWebDriver driver, Auth0Buttons button)
        {
            try
            {
                ReportManager.Driver($"Try Click {button} Button");
                string btnLocator = Auth0ButtonsDict[button];

                var btn = driver.FindElement(By.Id(btnLocator));

                if (btn != null)
                {
                    btn.Click();
                    ReportManager.Driver($"{button} Was Clicked");
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }

                else
                {
                    ReportManager.Error($"{button} Wasn't Found");
                }
            }
            catch (Exception ex)
            {
                ReportManager.Fatal($"{button} Button Can't Found, \n {ex.Message}");
            }
        }

        public static void ClickBtn(IWebDriver driver, Auth0Type button)
        {
            try
            {
                ReportManager.Driver($"Try Click {button} Button");
                string btnLocator = Auth0TypeButtonsDic[button];

                var btn = driver.FindElement(By.XPath(btnLocator));

                if (btn != null)
                {
                    btn.Click();
                    ReportManager.Driver($"{button} Was Clicked");
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }

                else
                {
                    ReportManager.Error($"{button} Wasn't Found");
                }
            }
            catch (Exception ex)
            {
                ReportManager.Fatal($"{button} Button Can't Found, \n {ex.Message}");
            }
        }

        public static void ClickByXpath(IWebDriver driver, string xpath)
        {
            try
            {
                var element = driver.FindElement(By.XPath(xpath));
                if (element != null)
                {
                    element.Click();
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                    ReportManager.Driver($"Element Was Clicked");
                }
                else
                {
                    ReportManager.Error($"Element Wasn't Clicked");
                }
            }
            catch (Exception ex)
            {
                ReportManager.Fatal($"Can't Found Element, {ex.Message}");
            }
        }

        public static void CatchAlert(IWebDriver driver, Notification notification, NotificationState Req, NotificationState Cmt)
        {
            try
            {
                ReportManager.Driver($"Trying To Find The Alert elements ({Req} and {Cmt})");
                string request = NotificationStateDict[Req];
                string comment = NotificationStateDict[Cmt];

                // Wait for the alert messages to appear
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

                string Request = wait.Until(d => d.FindElement(By.XPath(request))).Text;
                if (!string.IsNullOrEmpty(Request))
                {
                    notification.Request = Request;
                    ReportManager.Driver($"The Element For {Request} Is Found");
                }
                else
                {
                    ReportManager.Error($"element was not found");
                }

                string Comment = wait.Until(d => d.FindElement(By.XPath(comment))).Text;
                if (!string.IsNullOrEmpty(Comment))
                {
                    notification.Comment = Comment;
                    ReportManager.Driver($"The Element For {Comment} Is Found");
                }
                else
                {
                    ReportManager.Error($"element was not found");
                }
            }
            catch (Exception ex)
            {
                ReportManager.Fatal($"Can't Found Element, {ex.Message}");
            }
        }

        public static void AuthenticationInput(IWebDriver driver, Auth0Type type, Login_Email loginEmail, Login_Password loginPassword)
        {
            switch (type)
            {
                case Auth0Type.Google:
                    ReportManager.Driver($"AuthenticationInput >> '{type}'");
                    try
                    {
                        ClickBtn(driver, Auth0Type.Google);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    break;

                case Auth0Type.Facebook:
                    ReportManager.Driver($"AuthenticationInput >> '{type}'");
                    try
                    {

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    break;

                case Auth0Type.Email:
                    try
                    {
                        ReportManager.Driver($"AuthenticationInput >> '{type}'");

                        string email = $@"{_configHelper.GetElement(LoginEmailDic[loginEmail], "GallerU")}";
                        ReportManager.Driver($"Email >> '{email}'");

                        string password = $@"{_configHelper.GetElement(LoginPasswordDic[loginPassword], "GallerU")}";
                        ReportManager.Driver($"Password >> '{password}'");

                        string emailLocator = AuthenticationDict[Authentication.Email];
                        string passwordLocator = AuthenticationDict[Authentication.Password];

                        var inputEmail = driver.FindElement(By.Id("email"));

                        if (inputEmail != null)
                        {
                            inputEmail.SendKeys(email);
                            ReportManager.Driver("Email For Login Sent");
                            Thread.Sleep(TimeSpan.FromSeconds(3));

                            try
                            {
                                ClickBtn(driver, Auth0Buttons.Continue);
                            }
                            catch (Exception ex)
                            {
                                ReportManager.Fatal(ex.Message);
                            }

                            var inputPassword = driver.FindElement(By.Id("password"));

                            if (inputPassword != null)
                            {
                                inputPassword.SendKeys(password);
                                ReportManager.Driver("Password For Login Sent");
                                Thread.Sleep(TimeSpan.FromSeconds(3));

                                try
                                {
                                    ClickBtn(driver, Auth0Buttons.Continue);
                                }
                                catch (Exception)
                                {
                                    try
                                    {
                                        //btn-login
                                        ClickByXpath(driver, $@"/html/body/div/main/section/div/div/div/form/div[3]/button");
                                    }
                                    catch (Exception ex)
                                    {
                                        ReportManager.Fatal(ex.Message);
                                    }
                                }
                            }
                            else
                            {
                                ReportManager.Fatal("Password Input Wasn't Found");
                            }

                        }
                        else
                        {
                            ReportManager.Fatal("Email Input wasn't Found");
                        }
                    }
                    catch (Exception ex)
                    {
                        ReportManager.Fatal($"{type} Can't Found, \n {ex.Message}");
                    }
                    break;

                default:
                    ReportManager.Fatal($"-!- UNSUPPORTED 'AuthenticationInput' -!-");
                    break;
            }
        }

        public static void AddNewEventInput(IWebDriver driver, string eventName = null, DateTime? dateTime = null)
        {
            try
            {
                ReportManager.Driver($"Try Add New Event");

                if (string.IsNullOrEmpty(eventName))
                {
                    eventName = $@"{_configHelper.GetElement("Input_NewEvent_EventName", "GallerU")}";
                    ReportManager.Driver($"Event Name >> '{eventName}'");
                }
                else
                {
                    ReportManager.Driver($"Event Name >> '{eventName}'");
                }

                if (dateTime.HasValue)
                {
                    ReportManager.Driver($"Event Date >> '{dateTime.ToString()}'");
                }
                else
                {
                    dateTime = (DateTime)DateTime.Now;
                    ReportManager.Driver($"Event Date >> '{dateTime.ToString()}'");
                    //ReportManager.Driver($"Event Date >> '{dateTime.ToString("yyyy-MM-dd")}'");
                }

                string eventNameLocator = NewEventInputsDict[NewEventInputs.EventName];
                string dateLocator = NewEventInputsDict[NewEventInputs.Date];

                var inputEventName = driver.FindElement(By.Id(eventNameLocator));

                if (inputEventName != null)
                {
                    inputEventName.SendKeys($"{eventName} {dateTime}");
                    ReportManager.Driver("Event Name For Event Sent");
                    Thread.Sleep(TimeSpan.FromSeconds(3));

                    var inputDate = driver.FindElement(By.Id(dateLocator));

                    if (inputDate != null)
                    {
                        inputDate.Click();
                        inputDate.SendKeys(dateTime.Value.Month.ToString());
                        inputDate.SendKeys(dateTime.Value.Day.ToString());
                        inputDate.SendKeys(dateTime.Value.Year.ToString());

                        ReportManager.Driver("Date For Event Sent");
                        Thread.Sleep(TimeSpan.FromSeconds(3));
                    }
                    else
                    {
                        ReportManager.Fatal("EventDateInput Wasn't Found");
                    }
                }
                else
                {
                    ReportManager.Fatal("EventNameInput Wasn't Found");
                }
            }
            catch (Exception ex)
            {
                ReportManager.Fatal($"{ex.Message}");
            }
        }

        /******Create Event******/
        public static void CreateEventInput(IWebDriver driver, CreateEventInputs eventname = CreateEventInputs.None, CreateEventInputs eventtype = CreateEventInputs.None, CreateEventInputs eventowner = CreateEventInputs.None, CreateEventInputs eventmobilephone = CreateEventInputs.None, CreateEventInputs eventowneremail = CreateEventInputs.None, CreateEventInputs eventfolder = CreateEventInputs.None, DateTime? dateTime = null)
        {
            try
            {
                ReportManager.Driver($"Try Add New Event");

                //Start entering data
                if (eventname != CreateEventInputs.None)
                {
                    string eventName = $@"{_configHelper.GetElement(CreateEventInputDict[eventname], "GallerU")}";
                    ReportManager.Driver($"Event Name >> '{eventName}'");

                    //Loading  input
                    string eventNameLocator = NewEventInputsDict[NewEventInputs.EventName];

                    //Start entering data
                    var inputEventName = driver.FindElement(By.Id(eventNameLocator));

                    if (inputEventName != null)
                    {
                        inputEventName.SendKeys($"{eventName}");
                        ReportManager.Driver("Event Name For Event Sent");
                        Thread.Sleep(TimeSpan.FromSeconds(3));
                    }
                    else
                    {
                        ReportManager.Fatal("EventNameInput Wasn't Found");
                    }

                }

                if (dateTime.HasValue)
                {
                    ReportManager.Driver($"Event Date >> '{dateTime.ToString()}'");
                }
                else
                {
                    dateTime = (DateTime)DateTime.Now;
                    ReportManager.Driver($"Event Date >> '{dateTime.ToString()}'");
                    //ReportManager.Driver($"Event Date >> '{dateTime.ToString("yyyy-MM-dd")}'");
                }

                string dateLocator = NewEventInputsDict[NewEventInputs.Date];


                var inputDate = driver.FindElement(By.Id(dateLocator));

                if (inputDate != null)
                {
                    inputDate.Click();
                    inputDate.SendKeys(dateTime.Value.Month.ToString());
                    inputDate.SendKeys(dateTime.Value.Day.ToString());
                    inputDate.SendKeys(dateTime.Value.Year.ToString());

                    ReportManager.Driver("Date For Event Sent");
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }
                else
                {
                    ReportManager.Fatal("EventDateInput Wasn't Found");
                }


                if (eventtype != CreateEventInputs.None)
                {
                    string eventType = $@"{_configHelper.GetElement(CreateEventInputDict[eventtype], "GallerU")}";
                    ReportManager.Driver($"Event Name >> '{eventType}'");
                    string eventTypeLocator = NewEventInputsDict[NewEventInputs.EventType];
                    var inputType = driver.FindElement(By.Id(eventTypeLocator));

                    if (inputType != null)
                    {
                        inputType.SendKeys($"{eventType}");
                        ReportManager.Driver("Event Name For Event Sent");
                        Thread.Sleep(TimeSpan.FromSeconds(3));
                    }
                    else
                    {
                        ReportManager.Fatal("EventTypeInput Wasn't Found");
                    }

                }

                if (eventowner != CreateEventInputs.None)
                {
                    string eventOwner = $@"{_configHelper.GetElement(CreateEventInputDict[eventowner], "GallerU")}";
                    ReportManager.Driver($"Event Name >> '{eventOwner}'");
                    string eventOwnerLocator = NewEventInputsDict[NewEventInputs.OwnerName];


                    var inputOwner = driver.FindElement(By.Id(eventOwnerLocator));

                    if (inputOwner != null)
                    {
                        inputOwner.SendKeys($"{eventOwner}");
                        ReportManager.Driver("Event Name For Event Sent");
                        Thread.Sleep(TimeSpan.FromSeconds(3));
                    }
                    else
                    {
                        ReportManager.Fatal("EventOwnerInput Wasn't Found");
                    }

                }

                if (eventmobilephone != CreateEventInputs.None)
                {
                    string eventMobilePhone = $@"{_configHelper.GetElement(CreateEventInputDict[eventmobilephone], "GallerU")}";
                    ReportManager.Driver($"Event Name >> '{eventMobilePhone}'");
                    string eventMobilePhoneLocator = NewEventInputsDict[NewEventInputs.MobilePhone];
                    var inputMobliePhone = driver.FindElement(By.Id(eventMobilePhoneLocator));

                    if (inputMobliePhone != null)
                    {

                        inputMobliePhone.SendKeys($"{eventMobilePhone}");
                        ReportManager.Driver("Event Name For Event Sent");
                        Thread.Sleep(TimeSpan.FromSeconds(3));
                    }
                    else
                    {
                        ReportManager.Fatal("EventMobilePhoneInput Wasn't Found");
                    }

                }

                if (eventowner != CreateEventInputs.None)
                {
                    string eventOwnerEmail = $@"{_configHelper.GetElement(CreateEventInputDict[eventowneremail], "GallerU")}";
                    ReportManager.Driver($"Event Name >> '{eventOwnerEmail}'");
                    string eventOwnerEmailLocator = NewEventInputsDict[NewEventInputs.OwnerEmail];
                    var inputOwnerEmail = driver.FindElement(By.Id(eventOwnerEmailLocator));

                    if (inputOwnerEmail != null)
                    {
                        inputOwnerEmail.SendKeys($"{eventOwnerEmail}");
                        ReportManager.Driver("Event Name For Event Sent");
                        Thread.Sleep(TimeSpan.FromSeconds(3));
                    }
                    else
                    {
                        ReportManager.Fatal("EventOwnerEmailInput Wasn't Found");
                    }


                }
                if (eventfolder != CreateEventInputs.None)
                {
                    string eventFolder = $@"{_configHelper.GetElement(CreateEventInputDict[eventfolder], "GallerU")}";
                    ReportManager.Driver($"Event Name >> '{eventFolder}'");
                    string eventFolderLocator = NewEventInputsDict[NewEventInputs.EventFolder];
                    var inputEventFolder = driver.FindElement(By.Id(eventFolderLocator));

                    if (inputEventFolder != null)
                    {
                        inputEventFolder.SendKeys($"{eventFolder}");
                        ReportManager.Driver("Event Name For Event Sent");
                        Thread.Sleep(TimeSpan.FromSeconds(3));
                    }
                    else
                    {
                        ReportManager.Fatal("EventFolderInput Wasn't Found");
                    }
                }
            }
            catch (Exception ex)
            {
                ReportManager.Fatal($"{ex.Message}");
            }
        }

        public static void ClickSideBar(IWebDriver driver, SideBar tab)
        {
            try
            {
                ReportManager.Driver($"Try Click {tab} Button");

                string tabLocator = SideBarDict[tab];

                var btn = driver.FindElement(By.Id(tabLocator));

                if (btn != null)
                {
                    btn.Click();
                    ReportManager.Driver($"{tab} Was Clicked");
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }

                else
                {
                    ReportManager.Error($"{tab} Wasn't Found");
                }

            }
            catch (Exception ex)
            {
                ReportManager.Fatal($"{tab} Can't Found, \n {ex.Message}");
            }
        }

        public static void EventURL(IWebDriver driver, EventsButtons tab)
        {
            try
            {
                ReportManager.Driver($"Try Click {tab} Button");

                string tabLocator = EventsButtonsDict[tab];

                var btn = driver.FindElement(By.Id(tabLocator));

                if (btn != null)
                {
                    btn.Click();
                    ReportManager.Driver($"{tab} Was Clicked");
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }

                else
                {
                    ReportManager.Error($"{tab} Wasn't Found");
                }

            }
            catch (Exception ex)
            {
                ReportManager.Fatal($"{tab} Can't Found, \n {ex.Message}");
            }
        }

        public static void ClickRecentEvent(IWebDriver driver)
        {
            try
            {
                ReportManager.Driver($"Try Click Recent Event");
                IWebElement recentEvent = GetRecentEvent(driver);
                if (recentEvent != null)
                {
                    recentEvent.Click();
                    ReportManager.Driver($"{recentEvent.Text} Was Clicked");
                }

                else
                {
                    ReportManager.Error($"{recentEvent} Wasn't Found");
                }
            }
            catch (Exception ex)
            {
                ReportManager.Fatal($"{ex.Message}");
            }
        }

        public static void ShowEventList(IWebDriver driver, Events list)
        {
            try
            {
                ReportManager.Driver($"Try Click {list} List");

                string tabLocator = EventsDict[list];

                var btn = driver.FindElement(By.Id(tabLocator));

                if (btn != null)
                {
                    btn.Click();
                    ReportManager.Driver($"{list} Was Clicked");
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }

                else
                {
                    ReportManager.Error($"{list} Wasn't Found");
                }

            }
            catch (Exception ex)
            {
                ReportManager.Fatal($"{list} Can't Found, \n {ex.Message}");
            }
        }

        private static IWebElement GetRecentEvent(IWebDriver driver)
        {
            string eventLocator = EventsDict[Events.LastEvent];
            List<IWebElement> ls = driver.FindElements(By.XPath($@"//*[contains(@id, '{eventLocator}')]")).ToList();
            IWebElement el = driver.FindElement(By.Id($"{eventLocator}{ls.Count - 1}"));
            return el;
        }

        public static void UploadPhotos(IWebDriver driver)
        {
            try
            {
                var input = driver.FindElement(By.XPath("//input"));

                string folderPath = $@"{_configHelper.GetElement("EventPics_Path", "GallerU")}";

                if (folderPath != null)
                {
                    DirectoryInfo dir = new DirectoryInfo(folderPath);
                    var files = dir.GetFiles().Where(f => f.Name.Contains(".jpg") || f.Name.Contains(".jpeg") || f.Name.Contains(".png"));
                    string picPath = string.Empty;

                    foreach (var pic in files)
                    {
                        picPath = $@"{folderPath}\{pic.Name}";
                        ClickBtn(driver, EventsButtons.UploadPhotos);
                        //TODO move to .net7
                        //System.Windows.Forms.SendKeys.SendWait(picPath);
                        //System.Windows.Forms.SendKeys.SendWait("{Enter}");
                        Thread.Sleep(5000);
                        ReportManager.Driver($"{pic} Was Uploaded");
                        picPath = string.Empty;
                    }
                }

                else
                {
                    ReportManager.Error($"{folderPath} Wasn't Found");
                }
            }
            catch (Exception ex)
            {
                ReportManager.Fatal($"{ex.Message}");
            }
        }


        //Guest
        public static void ClickBtn(IWebDriver driver, GuestEnums.Buttons button)
        {
            try
            {
                ReportManager.Driver($"Try Click {button} Button");
                string btnLocator = GuestButtonsDict[button];

                var btn = driver.FindElement(By.Id(btnLocator));

                if (btn != null)
                {
                    btn.Click();
                    ReportManager.Driver($"{button} Was Clicked");
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }

                else
                {
                    ReportManager.Error($"{button} Wasn't Found");
                }
            }
            catch (Exception ex)
            {
                ReportManager.Fatal($"{button} Button Can't Found, \n {ex.Message}");
            }
        }

        public static void ClickCheckBoxes(IWebDriver driver, GuestEnums.CheckBoxes checkbox)
        {
            try
            {
                ReportManager.Driver($"Try Click {checkbox} Checkbox");
                string checkboxLocator = GuestCheckBoxesDict[checkbox];

                var cbox = driver.FindElement(By.Id(checkboxLocator));

                if (cbox != null)
                {
                    cbox.Click();
                    ReportManager.Driver($"{checkbox} Was Clicked");
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }

                else
                {
                    ReportManager.Error($"{checkbox} Wasn't Found");
                }
            }
            catch (Exception ex)
            {
                ReportManager.Fatal($"{checkbox} Checkbox Can't Found, \n {ex.Message}");
            }
        }

        //Sign Up Photographer
        public static void SignUp(IWebDriver driver, SignUp_Email typeEmail, SignUp_Password typePassword)
        {
            try
            {
                ReportManager.Driver($"AuthenticationInput >> '{typeEmail}'");

                if (typeEmail == SignUp_Email.InputEmailNew)
                {
                    GenerateNewEmail();
                }

                string email = $@"{_configHelper.GetElement(SignUpEmailDict[typeEmail], "GallerU")}";

                if (email == string.Empty)
                {
                    ReportManager.Fatal($"SignUp Email - {typeEmail}, Not Found on Config File");
                }

                ReportManager.Driver($"Email >> '{email}'");

                string password = $@"{_configHelper.GetElement(SignUpPasswordDict[typePassword], "GallerU")}";
                ReportManager.Driver($"Password >> '{password}'");

                string emailLocator = AuthenticationDict[Authentication.Email];
                string passwordLocator = AuthenticationDict[Authentication.Password];

                var inputEmail = driver.FindElement(By.XPath(emailLocator));

                if (inputEmail != null)
                {
                    inputEmail.SendKeys(email);
                    ReportManager.Driver("Email For Login Sent");
                    Thread.Sleep(TimeSpan.FromSeconds(3));

                    ClickBtn(driver, Auth0Buttons.Continue);

                    var inputPassword = driver.FindElement(By.XPath(passwordLocator));

                    if (inputPassword != null)
                    {
                        inputPassword.SendKeys(password);
                        ReportManager.Driver("Password For Login Sent");
                        Thread.Sleep(TimeSpan.FromSeconds(3));

                    }
                    else
                    {
                        ReportManager.Fatal("Password Input Wasn't Found");
                    }
                }
                else
                {
                    ReportManager.Fatal("Email Input wasn't Found");
                }
            }
            catch (Exception ex)
            {
                ReportManager.Fatal($"{typeEmail} Can't Found, \n {ex.Message}");

            }
        }

        private static void GenerateNewEmail()
        {
            // Generate a random number between 100 and 999
            Random random = new Random();
            int randomNumber = random.Next(100, 1000);

            //the new email address
            string emailNickname = $"Test{randomNumber}@gmail.com";

            try
            {
                _configHelper.SetElement(emailNickname, SignUpEmailDict[SignUp_Email.InputEmailNew], "GallerU");
                ReportManager.Driver("Generated New Email Address Done");
            }
            catch (Exception ex)
            {
                ReportManager.Fatal(ex.Message);
            }
        }
    }
}
