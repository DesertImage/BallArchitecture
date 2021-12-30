namespace Components
{
    public interface IComponentHolder
    {
        T Add<T>(T component);
        T Add<T>() where T : new();

        void Remove<T>();
    }
}