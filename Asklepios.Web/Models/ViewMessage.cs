using Asklepios.Web.Enums;

namespace Asklepios.Web.Models
{
    public class ViewMessage
    {
        public static readonly  string MESSAGE_KEY= "MESSAGE";
        public string Message { get; set; }
        public AlertMessageType  MessageType { get; set; }
    }
}
