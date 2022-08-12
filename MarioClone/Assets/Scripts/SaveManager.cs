using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }

    public int[] coinsInLevel;
    public int[] levelLock;
    public ScriptableObject[] levels;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.dat", FileMode.Open);
            Data_Storage data = (Data_Storage)bf.Deserialize(file);

            coinsInLevel = data.coinsInLevel;
            levelLock = data.levelLock;

            file.Close();
        } else
        {
            coinsInLevel = new int[levels.Length + 1];
            levelLock = new int[levels.Length + 1];
            levelLock[1] = 1;
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.dat");
        Data_Storage data = new Data_Storage();
        data.coinsInLevel = coinsInLevel;
        data.levelLock = levelLock;

        bf.Serialize(file, data);
        file.Close();
    }
}

[Serializable]
class Data_Storage
{
    public int[] coinsInLevel;
    public int[] levelLock;
}
