using AlarmClock.Services.Interfaces;

namespace AlarmClock.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
