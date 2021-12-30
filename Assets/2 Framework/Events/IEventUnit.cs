namespace DesertImage
{
    public interface IEventUnit
    {
        void ListenEvent<T>(IListen listener);
        void UnlistenEvent<T>(IListen listener);
        void SendEvent<T>(T arguments);
    }
}