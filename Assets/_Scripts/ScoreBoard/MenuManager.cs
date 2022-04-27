using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField nameField;
    [SerializeField]
    TextMeshProUGUI warningText;

    private bool ValidateName(string name)
    {
        if (name.All(char.IsLetterOrDigit))
        {
            return true;
        }

        return false;
    }

    public void SaveUsername()
    {
        if (this.ValidateName(nameField.text) == true)
        {
            PlayerPrefs.SetString("name", nameField.text);
            SceneLoader.instance.LoadScene("MainMenu");
        }
        else
        {
            this.warningText.enabled = true;
        }
    }
}
