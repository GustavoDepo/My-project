using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonPrinting : MonoBehaviour
{
    public Button loadButton;         // назначить в инспекторе
    public TMP_Dropdown modelDropdown;    // или TMP_Dropdown
    public ModelService modelService;

    private void Awake()
    {
        modelService = FindObjectOfType<ModelService>();
        // Проверяем, что всё заполнено
        if (loadButton == null || modelService == null || modelDropdown == null)
        {
            Debug.LogError("Не назначены ссылки на Button, Dropdown или ModelService.");
            enabled = false;
            return;
        }

        loadButton.onClick.AddListener(OnLoadButtonClicked);
    }

    public void OnLoadButtonClicked()
    {
        // Берём выбранный в dropdown текст
        string selectedName = modelDropdown.options[modelDropdown.value].text;
        // Вызываем метод
        modelService.PrintingSimulation(selectedName);
    }
}
