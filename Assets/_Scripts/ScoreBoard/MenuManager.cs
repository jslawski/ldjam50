using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField nameField;

    public void SaveUsername()
    {
        PlayerPrefs.SetString("name", nameField.text);
        SceneLoader.instance.LoadScene("MainMenu");
    }
}
