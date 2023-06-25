namespace Zionet.Automation.Framework.Common.Enums.GallerU.Photographer
{
    public class PhotographerEnums : CommonEnums
    {
        public enum Events
        {
            PastEvents,
            UpcomingEvents,
            LastEvent
        }

        public enum NewEventInputs
        {
            EventName,
            Date,
            EventType,
            OwnerName,
            PrefPhone,
            MobilePhone,
            OwnerEmail,
            EventFolder
        }

        public enum CreateEventInputs
        {
            None,
            InputEventName,
            InputEventType,
            InputEventOwner,
            InputEventMobilePhone,
            InputEventOwnerEmail,
            InputEventFolder
        }

        public enum Buttons
        {
            AddEvent,
            CreateEvent,
            UploadPhotos,
            EventURL
        }

        public enum EventsButtons
        {
            CopyToClipBoard,
            QRCodeAsPdf,
            QRCodeAsSvg,
            OpenInNewTap,
            Delete,
            AlertCancel,
            AlertConfirm,
            UploadPhotos
        }
    }
}

