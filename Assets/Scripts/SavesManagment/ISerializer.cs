namespace SavesManagement
{
    public interface ISerializer
    {
        public string Serialize<T>(T obj);

        T Deserialize<T>(string json);
    }
}