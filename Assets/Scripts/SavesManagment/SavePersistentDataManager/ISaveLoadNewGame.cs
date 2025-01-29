using System.Collections.Generic;
using SavesManagement;

namespace Services.SavesManagement.SavePersistentDataManager
{
    public interface ISaveLoadNewGame
    {
        GameData Load(string data);
        void Save(GameData data, bool overwrite = true);
        void Delete(string fileName);
        void DeleteAll();
        IEnumerable<string> ShowAllSaves();
    }
}