using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HW23_SaveLoadManager : MonoBehaviour
{
    public List<Transform> saveTargets;

    private string savePath;

    private void Start()
    {
        savePath = Application.persistentDataPath + "/save.json";

        LoadData();
    }

    public void SaveData()
    {
        HW23_WorldData worldData = new HW23_WorldData();

        foreach (Transform target in saveTargets)
        {
            HW23_TransformData data = new HW23_TransformData();

            data.objectName = target.name;
            data.position = target.position;
            data.rotation = target.rotation;

            worldData.objects.Add(data);
        }

        string json = JsonUtility.ToJson(worldData, true);

        File.WriteAllText(savePath, json);

        Debug.Log("저장 완료");
        Debug.Log(savePath);
    }

    public void LoadData()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("저장 파일 없음");
            return;
        }

        string json = File.ReadAllText(savePath);

        HW23_WorldData worldData =
            JsonUtility.FromJson<HW23_WorldData>(json);

        for (int i = 0; i < saveTargets.Count; i++)
        {
            saveTargets[i].position = worldData.objects[i].position;
            saveTargets[i].rotation = worldData.objects[i].rotation;
        }

        Debug.Log("불러오기 완료");
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();
        }
    }
}