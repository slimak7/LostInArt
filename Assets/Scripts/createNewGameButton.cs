using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class createNewGameButton : MonoBehaviour {

    public string name;

    public Text text;

    public Button[] b;

    public Button clearButton;

    public GameObject inputField;

private void Start()
    {

        //load();
    }

    public void load ()
    {
        DataManager.setFileName("savedGames");

        text.text = DataManager.loadString(name);

        if (text.text == "")
        {
            text.text = "<empty>";
        }
    }

    public void onClick ()
    {
        foreach (Button button in b)
        {
            button.interactable = true;
        }

        GetComponent<Button>().interactable = false;

        NewGameManager.instance.currentName = name;

        if (text.text.ToString() != "<empty>")
            clearButton.interactable = true;
        else
            clearButton.interactable = false;

        inputField.transform.position = transform.position;
        inputField.SetActive(true);
        EventSystem.current.SetSelectedGameObject(inputField, null);
        //inputField.OnPointerClick(null);
        
    }
}
