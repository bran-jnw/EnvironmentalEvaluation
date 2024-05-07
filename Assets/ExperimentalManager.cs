using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExperimentalManager : MonoBehaviour
{
    [SerializeField] GameObject langaugePrompt;
    [SerializeField] GameObject task1Prompt;
    [SerializeField] GameObject task2Prompt;
    [SerializeField] GameObject task3Prompt;
    [SerializeField] GameObject environment;
    [SerializeField] GameObject distance;
    [SerializeField] GameObject distraction;
    

    public static ExperimentalManager instance;

    private void Start()
    {
        langaugePrompt.SetActive(true);

        task1Prompt.SetActive(false);
        task1Prompt.SetActive(false);
        task1Prompt.SetActive(false);

        environment.SetActive(false);
        distance.SetActive(false);
        distraction.SetActive(false);

        instance = this;
    }

    int languageIndex;
    public static int GetLanguageIndex()
    {
        return instance.languageIndex;
    }

    public void SelectLanguage(int i)
    {
        languageIndex = i;

        gameObject.GetComponent<EnviromentalAssessment>().SetLanguage(languageIndex);
        gameObject.GetComponent<QueryDistance>().SetLanguage(languageIndex);
        gameObject.GetComponent<QuerySound>().SetLanguage(languageIndex);

        langaugePrompt.SetActive(false);
        task1Prompt.SetActive(true);
    }

    public void StartTask1()
    {
        task1Prompt.SetActive(false);
        environment.SetActive(true);
        gameObject.GetComponent<EnviromentalAssessment>().StartTask();
    }

    public void StartTask2() 
    {
        task2Prompt.SetActive(false);
        distance.SetActive(true);
        gameObject.GetComponent<QueryDistance>().StartTask();
    }
    public void StartTask3()
    {
        task3Prompt.SetActive(false);
        instance.distraction.SetActive(true);
        gameObject.GetComponent<QuerySound>().StartTask();
    }

    public static void EnvironmentDone()
    {
        instance.environment.SetActive(false);
        instance.task2Prompt.SetActive(true);
    }

    public static void DistanceDone()
    {
        instance.distance.SetActive(false);
        instance.task3Prompt.SetActive(true);
    }

    public static void AllDone()
    {        
        instance.distraction.SetActive(false);
    }

    public void ResetApp()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
