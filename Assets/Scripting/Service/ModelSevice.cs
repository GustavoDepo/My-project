using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModelService : MonoBehaviour
{
    public string basePath = "Models/";
    public float layerHeight = 0.01f;

    private GameObject container;
    private CoordLoader coordLoader;
    private List<Vector2[]> points;
    private Lazer lazer;

    public void PrintingSimulation(string modelName)
    {
        if (container != null)
            Destroy(container);

        coordLoader = FindObjectOfType<CoordLoader>();
        lazer       = FindObjectOfType<Lazer>();              // <<< здесь
        container   = new GameObject("ModelContainer");
        points      = coordLoader.LoadLayersFromResources(modelName);
        StartCoroutine(PrintingCoroutine(modelName));
    }

    private IEnumerator PrintingCoroutine(string modelName)
    {
        string folderPath  = basePath + modelName;
        string modelPrefix = folderPath + "/layer_";
        GameObject[] allPrefabs = Resources.LoadAll<GameObject>(folderPath);
        int modelCount = allPrefabs.Length;

        for (int i = 0; i < modelCount; i++)
        {
            // 1) Ждём загрузки и инстантиирования i-го слайса
            yield return StartCoroutine(loadSlice(i, modelPrefix));

            // 2) Берём координаты именно для этого слоя
            Vector2[] coordSlice = points[i];                  // <<< points[i], не points[j]

            // 3) Проходим по всем точкам слоя и запускаем движение лазера
            for (int j = 0; j < coordSlice.Length; j++)
            {
                // StartCoroutine, чтобы подождать внутри lazerMove
                yield return StartCoroutine(lazer.lazerMove(coordSlice[j]));
            }

            // 4) Можно сделать небольшую паузу между слоями
            yield return null;
        }
    }

    private IEnumerator loadSlice(int i, string modelPrefix)
    {
        // поднимаем контейнер, чтобы новый слой оказался сверху
        container.transform.position -= new Vector3(0, layerHeight, 0);

        string path = modelPrefix + i.ToString("D3");
        GameObject prefab = Resources.Load<GameObject>(path);
        if (prefab != null)
        {
            var instance = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            instance.transform.SetParent(container.transform, worldPositionStays: true);
        }
        else
        {
            Debug.LogWarning($"Не найден префаб по пути {path}");
        }

        yield return null;
    }

    public GameObject GetContainer() => container;
}
