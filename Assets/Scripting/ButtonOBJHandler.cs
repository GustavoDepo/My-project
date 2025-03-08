using UnityEngine;
using UnityEngine.UI;
using SimpleFileBrowser;
using Dummiesman;
using System.IO;
using com.marufhow.meshslicer.core;

public class ButtonOBJHandler : MonoBehaviour
{

    GameObject loadedObject;
    private MHCutter mhCutter = new MHCutter();
    public string objPath = string.Empty;
    public void OnButtonClick()
    {
        FileBrowser.SetDefaultFilter( ".obj" );
        FileBrowser.ShowLoadDialog( ( paths ) => {onSeccses(paths);},
								   () => { Debug.Log( "Canceled" ); },
								   FileBrowser.PickMode.Files, false, null, null, "Select Folder", "Select" );
    }

    private void onSeccses(string[] paths)
    {
        Debug.Log( "Selected: " + paths[0]);
        objPath = paths[0];
        if(loadedObject != null) Destroy(loadedObject);
        loadedObject = new OBJLoader().Load(objPath);
        changeMaterial(loadedObject);
        cutOBJmodel(loadedObject, 1);
    }

    private void changeMaterial(GameObject obj)
    {
        Transform child = obj.transform.GetChild(0);
        Debug.Log("Найден дочерний объект: " + child.name);
        MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            Material newMaterial = Resources.Load<Material>("Materials/Lit"); // Загружаем новый материал
            if (newMaterial != null)
            {
                meshRenderer.materials = new Material[] { newMaterial }; // Заменяем все материалы на один
                Debug.Log("Поменял материал");
            }
        }

    }

    private void cutOBJmodel(GameObject model, int step)
    {
        GameObject cutObject = model.transform.GetChild(0).gameObject;
        Bounds bounds = cutObject.GetComponent<Renderer>().bounds;
        float ySize = bounds.max.y - bounds.min.y;
        int intMinY = (int) bounds.min.y;
        int layerCount = (int) (ySize / step);
        mhCutter.Cut(cutObject, step);
    }
}

