namespace SavesManagement
{
    public interface ISaveSystem
    {
        void NewGame();
        void SaveGameData();
        void LoadGameData(string gameName);
    }
}