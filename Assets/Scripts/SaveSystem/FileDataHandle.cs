using System;
using System.IO;
using UnityEngine;

public class FileDataHandle
{
    private string fullPath;
    private bool encryptData;
    private string codeWorld = "skrofox";

    public FileDataHandle(string dataDirPath, string dataFileName, bool encryptData)
    {
        fullPath = Path.Combine(dataDirPath, dataFileName);
        this.encryptData = encryptData;
    }

    public void SaveData(GameData gameData)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToSave = JsonUtility.ToJson(gameData, true);

            if (encryptData)
                dataToSave = EncryptDecrypt(dataToSave);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter write = new StreamWriter(stream))
                {
                    write.Write(dataToSave);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Xay ra loi khi co gang luu du lieu vao file: " + fullPath + "\nLoi: " + e);
        }
    }

    public GameData LoadData()
    {
        GameData loadData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (encryptData)
                    dataToLoad = EncryptDecrypt(dataToLoad);

                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Xay ra loi khi co gang LAY du lieu tu file: " + fullPath + "\nLoi: " + e.Message);
            }
        }

        return loadData;
    }

    public void Delete()
    {
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }

    private string EncryptDecrypt(string data)
    {
        string modifedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifedData += (char)(data[i] ^ codeWorld[i % codeWorld.Length]);
        }

        return modifedData;
    }
}
