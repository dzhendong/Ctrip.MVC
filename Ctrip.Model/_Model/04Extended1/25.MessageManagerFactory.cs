
namespace Ctrip.Model
{
    public static class MessageManagerFactory
    {
        public static MessageManager1 GetMessageManager() 
        {
            return new DefaultMessageManager();
        }
    }
}
