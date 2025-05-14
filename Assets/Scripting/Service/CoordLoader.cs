using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class CoordLoader : MonoBehaviour
{
    private const string PREFIX = "Models/";

    public List<Vector2[]> LoadLayersFromResources(string dirPath)
    {
        string path = PREFIX + dirPath;
        TextAsset[] assets = Resources.LoadAll<TextAsset>(path);

        if (assets == null || assets.Length == 0)
        {
            Debug.LogWarning($"В Resources/{path} не найдено JSON-файлов");
            return new List<Vector2[]>();
        }

        var sorted = assets
            .Where(a => a.name.StartsWith("layer_"))
            .OrderBy(a =>
            {
                var parts = a.name.Split('_');
                return (parts.Length >= 2 && int.TryParse(parts[1], out int idx)) 
                    ? idx 
                    : 0;
            })
            .ToArray();

        var layers = new List<Vector2[]>(sorted.Length);

        foreach (var asset in sorted)
        {
            try
            {
                float[][] raw = JsonConvert.DeserializeObject<float[][]>(asset.text);
                Vector2[] vecs = raw
                    .Select(pair => new Vector2(pair[0], pair[1]))
                    .ToArray();
                layers.Add(vecs);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка при парсе {asset.name}: {ex.Message}");
            }
        }

        return layers;
    }
}
