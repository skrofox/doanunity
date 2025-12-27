using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private FileDataHandle dataHandle;
    private GameData gameData;
    private List<ISaveable> allSaveables;

    [SerializeField] private string fileName = "SaveGame.json";
    [SerializeField] private bool encryptData = true;

    private void Awake()
    {
        instance = this;
    }

    private IEnumerator Start()
    {
        Debug.Log(Application.persistentDataPath);
        dataHandle = new FileDataHandle(Application.persistentDataPath, fileName, encryptData);
        allSaveables = FindISaveables();

        yield return null;
        LoadGame();
    }

    private void LoadGame()
    {
        gameData = dataHandle.LoadData();

        if (gameData == null)
        {
            Debug.Log("Không có bản lưu cũ nào, đã tạo 1 bản chơi mới.");
            gameData = new GameData();
            return;
        }

        foreach (var saveable in allSaveables)
        {
            saveable.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (var saveable in allSaveables)
        {
            saveable.SaveData(ref gameData);

            dataHandle.SaveData(gameData);
        }
    }

    public GameData GetGameData() => gameData;

    [ContextMenu("***DELETE SAVE DATA***")]
    public void DeleteSaveData()
    {
        dataHandle = new FileDataHandle(Application.persistentDataPath, fileName, encryptData);
        dataHandle.Delete();

        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveable> FindISaveables()
    {
        return FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None)
            .OfType<ISaveable>()
            .ToList();
    }
}
