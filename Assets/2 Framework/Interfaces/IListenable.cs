namespace DesertImage
{
    public interface IListenable
    {
        void listen<T>(IListen listener);
        void unlisten<T>(IListen listener);
        void send<T>(T arguments); 
    }
}