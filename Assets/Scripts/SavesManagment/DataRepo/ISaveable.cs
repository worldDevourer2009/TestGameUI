namespace SavesManagement
{
    public interface ISaveable
    {
        string SaveId { get; }
        void SaveData(GameData gameData);
        void LoadData(GameData gameData);
    }
}