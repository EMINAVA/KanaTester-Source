namespace KanaTester
{
    public interface ISerializingService
    {
        string Serialize<T>(T item);
        T Deserialize<T>(string item);
    }
}