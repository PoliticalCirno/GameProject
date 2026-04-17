using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SaveLoadSystem : PersistentSingleton<SaveLoadSystem>
{
    public GameData gameData;
    IDataService dataService;

    protected override void Awake()
    {
        base.Awake();
        dataService = new FileDataService(new JsonSerializer());
    }
    public void SaveGame()
    {
        dataService.Save(gameData);
    }
    public void LoadGame(string gameName)
    {
        gameData = dataService.Load(gameName);
        if (string.IsNullOrWhiteSpace(gameData.sceneName))
        {
            gameData.sceneName = "Level 1";
        }
        EventChannelManager.instance.gameDataEvent.RaiseEvent(gameData);
        SceneManager.LoadScene(gameData.sceneName);
    }
    public void DeleteGame(string gameName)
    {
        dataService.Delete(gameName);
    }
    public IEnumerable<string> ListAllSaves()
    {
        return dataService.ListSaves();
    }
}
