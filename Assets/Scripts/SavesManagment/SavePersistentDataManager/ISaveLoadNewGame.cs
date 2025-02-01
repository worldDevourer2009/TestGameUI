namespace SavesManagement
{
    public interface ISaveLoadNewGame
    {
        GameData Load(string data);
        void Save(GameData data, bool overwrite = true);
    }
}