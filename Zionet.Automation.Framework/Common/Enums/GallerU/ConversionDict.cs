using System.Collections.Generic;
using Zionet.Automation.Framework.Common.Enums.Auth0;
using Zionet.Automation.Framework.Common.Enums.GallerU.Guest;
using Zionet.Automation.Framework.Common.Enums.GallerU.Photographer;
using static Zionet.Automation.Framework.Common.Enums.Auth0.Auth0Enums;
using static Zionet.Automation.Framework.Common.Enums.GallerU.CommonEnums;
using static Zionet.Automation.Framework.Common.Enums.GallerU.Photographer.PhotographerEnums;

namespace Zionet.Automation.Framework.Common.Enums.GallerU
{
    public static class ConversionDict
    {
        public static Dictionary<GeneralButtons, string> GeneralButtonsDict = new Dictionary<GeneralButtons, string>()
        {
            { GeneralButtons.Login,        $"sel-Login-page-btn-login" },
            { GeneralButtons.LogOut,       $"sel-Profile-page-btn-logout" },
        };

        public static Dictionary<PhotographerEnums.Buttons, string> PhotographerButtonsDict = new Dictionary<PhotographerEnums.Buttons, string>()
        {
            { PhotographerEnums.Buttons.AddEvent,     $"sel-GalleruCalendar-widget-btn-addEvent" },
            { PhotographerEnums.Buttons.CreateEvent,  $"sel-GalleruEventCreate-widget-submit-createEvent" },
            { PhotographerEnums.Buttons.EventURL,     $"sel-Gallery-page-btn-leftArrow" },
            { PhotographerEnums.Buttons.UploadPhotos, $"Sel-image-uploader-btn-uploadPicture" }


        };

        public static Dictionary<Authentication, string> AuthenticationDict = new Dictionary<Authentication, string>()
        {
            { Authentication.Email,         $@"//*[@inputmode=""email""]" },
            { Authentication.Password,      $@"//*[@id=""password""]" },
            { Authentication.Continue,      $@"/html/body/div/main/section/div/div/div/form/div[3]/button" }
        };

        public static Dictionary<SideBar, string> SideBarDict = new Dictionary<SideBar, string>()
        {
            { SideBar.Home,         $"sel-AppMenuBar-component-btn-home" },
            { SideBar.Calender,     $"sel-AppMenuBar-component-btn-calendar" },
            { SideBar.Profile,      $"sel-AppMenuBar-component-btn-profile" },
        };
        
        public static Dictionary<Auth0Buttons, string> Auth0ButtonsDict = new Dictionary<Auth0Buttons, string>()
        {
            { Auth0Buttons.Continue,                    $"btn-login" },
            { Auth0Buttons.SignUp,                      $"btn-signup" },
        };

        public static Dictionary<NewEventInputs, string> NewEventInputsDict = new Dictionary<NewEventInputs, string>()
        {
            { NewEventInputs.EventName,                $"sel-GalleruEventCreate-widget-input-eventName" },
            { NewEventInputs.Date,                     $"sel-GalleruEventCreate-widget-input-date" },
            { NewEventInputs.EventType,                $"sel-GalleruEventCreate-widget-input-eventType" },
            { NewEventInputs.OwnerName,                $"sel-GalleruEventCreate-widget-input-owner" },
            { NewEventInputs.MobilePhone,              $"sel-GalleruEventCreate-widget-input-mobilePhone" },
            { NewEventInputs.OwnerEmail,               $"sel-GalleruEventCreate-widget-input-ownerEmail" },
            { NewEventInputs.EventFolder,              $"sel-GalleruEventCreate-widget-input-eventFolder" },
        };

        public static Dictionary<CreateEventInputs, string> CreateEventInputDict = new Dictionary<CreateEventInputs, string>()
        {
            { CreateEventInputs.InputEventName,                $"Input_NewEvent_EventName" },
            { CreateEventInputs.InputEventType,                $"Input_NewEvent_EventType" },
            { CreateEventInputs.InputEventOwner,               $"Input_NewEvent_EventOwner" },
            { CreateEventInputs.InputEventMobilePhone,         $"Input_NewEvent_EventMobilePhone" },
            { CreateEventInputs.InputEventOwnerEmail,          $"Input_NewEvent_EventOwnerEmail" },
            { CreateEventInputs.InputEventFolder,              $"Input_NewEvent_EventFolder" },
        };

        public static Dictionary<Events, string> EventsDict = new Dictionary<Events, string>()
        {
            { Events.LastEvent,         $"sel-GallerUEvent-component-btn-eventsDetails-" },
            { Events.PastEvents,        $"sel-GallerUEvent-component-tab-events-0" },
            { Events.UpcomingEvents,    $"sel-GallerUEvent-component-tab-events-1" },
        };

        public static Dictionary<EventsButtons, string> EventsButtonsDict = new Dictionary<EventsButtons, string>()
        {
            { EventsButtons.QRCodeAsPdf,        $"sel-QrcodeButton-component-btn-downloadQRCodeAsPDF" },
            { EventsButtons.QRCodeAsSvg,        $"sel-QrcodeButton-component-btn-downloadQRCodeAsSVG" },
            { EventsButtons.CopyToClipBoard,    $"sel-CopyToClipboardButton-component-btn-copyToClipboard" },
            { EventsButtons.OpenInNewTap,       $"sel-Gallery-page-btn-openInNewTab" },
            { EventsButtons.Delete,             $"sel-GalleruEventUpdate-widget-btn-delete" },
            { EventsButtons.AlertCancel,        $"sel-GalleruDialog-alert-btn-cancel" },
            { EventsButtons.AlertConfirm,       $"sel-GalleruDialog-alert-btn-confirm" },
            { EventsButtons.UploadPhotos,       $"Sel-image-uploader-btn-uploadPicture" },
        };

        public static Dictionary<GuestEnums.Buttons, string> GuestButtonsDict = new Dictionary<GuestEnums.Buttons, string>()
        {
            { GuestEnums.Buttons.OpenCamera,        $"sel-Selfie-page-btn-selfieButton" },
            { GuestEnums.Buttons.SelfiBtn,          $"sel-GalleruWebcam-component-btn-selfie" },
            { GuestEnums.Buttons.UseThisPictur,     $"sel-SelfieRetake-page-btn-use-this-picture" },
            { GuestEnums.Buttons.RetakePicture,     $"sel-SelfieRetake-page-btn-retake" },
        };

        public static Dictionary<GuestEnums.CheckBoxes, string> GuestCheckBoxesDict = new Dictionary<GuestEnums.CheckBoxes, string>()
        {
            { GuestEnums.CheckBoxes.CheckBox,    $"terms"},
        };

        public static Dictionary<Auth0Enums.Auth0Type, string> Auth0TypeButtonsDic = new Dictionary<Auth0Type, string>()
        {
            { Auth0Enums.Auth0Type.Google,      $"/html/body/div/main/section/div/div[2]/div/div[4]/form[1]/button" },
            { Auth0Enums.Auth0Type.Facebook,    $"/html/body/div/main/section/div/div[2]/div/div[4]/form[2]/button" },
        };

        public static Dictionary<Auth0Enums.Login_Email, string> LoginEmailDic = new Dictionary<Login_Email, string>()
        {
            { Auth0Enums.Login_Email.InputEmail,        $"Input_Email" },
            { Auth0Enums.Login_Email.InputEmailFake,    $"Input_Email_Fake" },
        };

        public static Dictionary<Auth0Enums.Login_Password, string> LoginPasswordDic = new Dictionary<Login_Password, string>()
        {
            { Auth0Enums.Login_Password.InputPassword,          $"Input_Password" },
            { Auth0Enums.Login_Password.InputPasswordUc,        $"Input_Password_Uc" },
            { Auth0Enums.Login_Password.InputPasswordLc,        $"Input_Password_Lc" },
            { Auth0Enums.Login_Password.InputPasswordEmpty,     $"Input_Password_Empty" },
        };

        public static Dictionary<Auth0Enums.SignUp_Email, string> SignUpEmailDict = new Dictionary<SignUp_Email, string>()
        {
            { Auth0Enums.SignUp_Email.InputEmailNew,   $"Input_Email_New" },
        };


        public static Dictionary<Auth0Enums.SignUp_Password, string> SignUpPasswordDict = new Dictionary<SignUp_Password, string>()
        {
            { Auth0Enums.SignUp_Password.InputPasswordUperCase,              $"Input_Password_UperCase" },
            { Auth0Enums.SignUp_Password.InputPasswordLowerCase,             $"Input_Password_LowerCase" },
            { Auth0Enums.SignUp_Password.InputPasswordNumbers,               $"Input_Password_Numbers" },
            { Auth0Enums.SignUp_Password.InputPasswordSpecialCharacters,     $"Input_Password_SpecialCharacters" },
            { Auth0Enums.SignUp_Password.InputPasswordMix,                   $"Input_Password_Mix" },
        };

        public static Dictionary<NotificationAction, string> RequestDict = new Dictionary<NotificationAction, string>()
        {
            { NotificationAction.Create,        $"Event creation request sent" },
            { NotificationAction.Delete,        $"Event delete request sent" },
        };

        public static Dictionary<NotificationAction, string> CommentDict = new Dictionary<NotificationAction, string>()
        {
            { NotificationAction.Create,        $"Event successfully created!" },
            { NotificationAction.Delete,        $"Event successfully deleted!" },
        };

        public static Dictionary<NotificationState, string> NotificationStateDict = new Dictionary<NotificationState, string>()
        {
            { NotificationState.ReqCreate,         $"//div[text()='{RequestDict[NotificationAction.Create]}']" },
            { NotificationState.ReqDelete,         $"//div[text()='{RequestDict[NotificationAction.Delete]}']" },
            { NotificationState.CmtCreate,         $"//div[text()='{CommentDict[NotificationAction.Create]}']" },
            { NotificationState.CmtDelete,         $"//div[text()='{CommentDict[NotificationAction.Delete]}']" },
        };
    }
}