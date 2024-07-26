using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [Header("Panels")]
    public GameObject panelMain;
    public GameObject panelSettings;
    public GameObject panelTutorial;
    public GameObject panelCredits;

    // Start is called before the first frame update
    void Start()
    {
        panelMain.SetActive(true);
        panelSettings.SetActive(false);
        panelTutorial.SetActive(false);
        panelCredits.SetActive(false);
    }

    public void OnBtnStart()
    {
        SceneManager.LoadScene(1);
    }

    public void OnBtnQuit()
    {
        Application.Quit();
    }

    public void OnBtnMain()
    {
        panelMain.SetActive(true);
        panelSettings.SetActive(false);
        panelTutorial.SetActive(false);
        panelCredits.SetActive(false);
    }

    public void OnBtnSettings()
    {
        panelMain.SetActive(false);
        panelSettings.SetActive(true);
        panelTutorial.SetActive(false);
        panelCredits.SetActive(false);
    }

    public void OnBtnTutorial()
    {
        panelMain.SetActive(false);
        panelSettings.SetActive(false);
        panelTutorial.SetActive(true);
        panelCredits.SetActive(false);
    }

    public void OnBtnCredits()
    {
        panelMain.SetActive(false);
        panelSettings.SetActive(false);
        panelTutorial.SetActive(false);
        panelCredits.SetActive(true);
    }
}
