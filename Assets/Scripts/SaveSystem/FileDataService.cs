using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataService : IDataService
{
    private ISerializer serializer;
    private string dataPath; // Where the save file is located;
    private string fileExtension; // We can define a custom extension if needed.

    public FileDataService(ISerializer serializer)
    {
        this.serializer = serializer;
        dataPath = Application.persistentDataPath; // C:/Users/<username>/AppData/LocalLow/COMP397-W26
        fileExtension = ".json";
    }
    // GetPathFile - To combine the dataPath, FileName and FileExtension
    private string GetPathFile(string fileName)
    {
        // C:/Users/<username>/AppData/LocalLow/COMP397-W26/fileName.json
        return Path.Combine(dataPath, string.Concat(fileName, fileExtension));
    }
    // Save
    public void Save(GameData data, bool overwrite = true)
    {
        string fileLocation = GetPathFile(data.fileName);
        if (!overwrite && File.Exists(fileLocation)) // I can't overwrite and the file exists
        {
            throw new IOException("The file already exists and can't be overwritten");
        }
        File.WriteAllText(fileLocation, serializer.Serialize(data));
    }
    // Load
    public GameData Load(string fileName)
    {
        string fileLocation = GetPathFile(fileName);
        if (!File.Exists(fileLocation))
        {
            throw new System.Exception("No persistent data found at "+fileLocation);
        }
        return serializer.Deserialize<GameData>(File.ReadAllText(fileLocation));
    }
    // Delete
    public void Delete(string fileName)
    {
        string fileLocation = GetPathFile(fileName);
        if (File.Exists(fileLocation))
        {
            File.Delete(fileLocation);
        }
    }
    // ListAllSaves
    public IEnumerable<string> ListSaves()
    {
        foreach (string path in Directory.EnumerateFiles(dataPath))
        {
            if (Path.GetExtension(path) == fileExtension)
            {
                yield return Path.GetFileNameWithoutExtension(path);
            }
        }
    }
}
