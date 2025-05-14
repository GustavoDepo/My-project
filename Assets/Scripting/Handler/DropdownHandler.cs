using UnityEngine;
using UnityEngine.UI; // или TMPro если TMP Dropdown
using TMPro;

public class DropdownHandler : MonoBehaviour
{
    public TMP_Dropdown dropdown; // для стандартного UI
    // public TMP_Dropdown dropdown; // если TMP

    private string selectedValue;

    void Start()
    {
        // Подписка на событие
        //dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    public void OnDropdownValueChanged(int index)
    {
        selectedValue = dropdown.options[index].text;
        ModelService loader = GetComponent<ModelService>();
        if (selectedValue != "Выберите модель"){
            //TODO: 
        }
        Debug.Log("Выбрано значение: " + selectedValue);

        // Здесь можешь использовать selectedValue как хочешь
    }
}
