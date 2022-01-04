using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour {

    private Resolution[] resolutions;

    public Dropdown resolutionsDropdown;
    public Dropdown graphicsDropdown;

    public AudioMixer AM;

    public Slider music;
    

    [Header("Pocz")]
    public GameObject menu;

    

    private GameObject pauseCanvas;

	// Use this for initialization
	void Awake () {

        pauseCanvas = GameObject.FindGameObjectWithTag("Pause");

        resolutions = Screen.resolutions;

        resolutionsDropdown.ClearOptions();

        List<string> resolutionStrings = new List<string>();

        int currentIndex = 0;



        for (int i=0;i<resolutions.Length;i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionStrings.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentIndex = i;

        }

        resolutionsDropdown.AddOptions(resolutionStrings);
        resolutionsDropdown.value = currentIndex;
        resolutionsDropdown.RefreshShownValue();

       
        graphicsDropdown.value = QualitySettings.GetQualityLevel();


        float vol = PlayerPrefs.GetFloat(FileNames.musicVolume);

        music.value = vol;
        
        
        if (menu != null)
            menu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseCanvas != null)
            pauseCanvas.SetActive(true);

            closeSet();
        }
    }

    public void setResolution (int resIndex)
    {
        if (resIndex < 0 || resIndex > resolutions.Length - 1)
            return;
        
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, true);
    }

    public void closeSet ()
    {
        if (menu != null)
            menu.SetActive(true);

        gameObject.SetActive(false);
    }

    public void setQuality (int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
	
    public void setVolMusic (float volume)
    {
        AM.SetFloat("Volume", volume);
        //DataManager2.saveData(volume, FileNames.musicVolume);
        PlayerPrefs.SetFloat(FileNames.musicVolume, volume);
    }
    
    

}
