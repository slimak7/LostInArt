using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameManager : MonoBehaviour {

    public static NewGameManager instance;

    [HideInInspector]
    public string currentName;

    public InputField gameName;

    public createNewGameButton[] buttons;

    public GameObject inputField;

    private void Awake()
    {
        instance = this;

        buttons[0].load();
        buttons[1].load();
        buttons[2].load();
    }

    public void createNewGame ()
    {
        if (currentName != "")
        {
            if (gameName.text != "")
            {
                DataManager.setFileName("savedGames");

                for (int i=0;i<buttons.Length;i++)
                {
                    if (DataManager.loadString(buttons[i].name) == gameName.text)
                        return;

                }


                char[] array = gameName.text.ToCharArray();

                for (int i=0;i<array.Length;i++)
                {
                    if (array[i] == '>' || array[i] == '<')
                        return;
                }

                DataManager.setFileName("savedGames");

                string Name = DataManager.loadString(currentName);

                DataManager.setFileName(Name);

                DataManager.removeFile();

                DataManager.setFileName("savedGames");

                DataManager.saveString(gameName.text, currentName);

                DataManager.setFileName(gameName.text);

                //StartManager.instance.newGame();

                gameObject.SetActive(false);
            }
        }
    }

    public void close ()
    {
        gameObject.SetActive(false);
    }

    

    public void clear ()
    {
        if (currentName != null)
        {
            DataManager.setFileName("savedGames");

            string Name = DataManager.loadString(currentName);

            DataManager.setFileName(Name);

            DataManager.removeFile();

            DataManager.setFileName("savedGames");

            DataManager.saveString("", currentName);

            foreach (createNewGameButton c in buttons)
            {
                
                    c.load();
                c.gameObject.GetComponent<Button>().interactable = true;
            }

            inputField.SetActive(false);
        }
    }
        
}
