using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    public Color unitColour;

    private void Awake()
    {
        // Ensure that only one instance of MainManager exists
        Debug.Log("MainManager Awake called");
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColour();
    }

    [System.Serializable]
    class SaveData
    {
        public Color _unitColor;
    }

    public void SaveColour()
    {
        Debug.Log($"Saving color: {unitColour}");
        SaveData data = new SaveData();
        data._unitColor = unitColour;

        string jsonData = JsonUtility.ToJson(data);
        string pathName = Path.Combine(Application.persistentDataPath, "gd5_week8.json");
        Debug.Log($"Saving to path: {pathName.Replace("/", "\\")}");
        File.WriteAllText(pathName, jsonData);
    }

    public void LoadColour()
    {
        string pathName = Path.Combine(Application.persistentDataPath, "gd5_week8.json");
        Debug.Log($"Loading from path: {pathName.Replace("/", "\\")}");
        if (File.Exists(pathName))
        {
            string jsonData = File.ReadAllText(pathName);
            SaveData data = JsonUtility.FromJson<SaveData>(jsonData);
            unitColour = data._unitColor;
        }
        else
        {
            Debug.LogWarning("Save file not found, using default color.");
        }

        Debug.Log($"Loaded color: {unitColour}");
    }
}
