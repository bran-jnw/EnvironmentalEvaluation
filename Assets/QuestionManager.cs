using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] GameObject environment;
    [SerializeField] GameObject distance;
    [SerializeField] GameObject distraction;

    public static QuestionManager instance;

    // Start is called before the first frame update
    void Start()
    {
        environment.SetActive(true);
        distance.SetActive(false);
        distraction.SetActive(false);

        instance = this;
    }

    public static void EnvironmentDone()
    {
        instance.environment.SetActive(false);
        instance.distance.SetActive(true);
    }

    public static void DistanceDone()
    {
        instance.distance.SetActive(false);
        instance.distraction.SetActive(true);
    }
}
