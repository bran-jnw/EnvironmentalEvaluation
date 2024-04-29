using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuerySound : MonoBehaviour
{
    public TMP_Text textItem;
    public TMP_Text[] textButton;
    public AudioSource alarm;
    public float delayAlarm = 20f;
    public bool useAlarm = true;
    private int index;

    //These are kept as public for now...
    public string[] itemTextE = { "What is the color of the blackboard?",
                                "What is the color of the ...?",
                                "What is the color of the ...?"};

    public string[] button1;
    public string[] button2;
    public string[] button3;
    public string[] button4;

    void Start()
    {
        //canvas.SetActive(false);
        index = 0;
    }

    public void SetActive()
    {
        DisplayItem();
        Invoke("LaunchAlarm", delayAlarm);
    }



    public void Click_Key(int value)
    {

        index++;

        if (index < itemTextE.Length)
        {
            DisplayItem();
        }
        else
        {
            index = 0;

        }

    }



    private void DisplayItem()
    {

        textItem.text = itemTextE[index];
        textButton[0].text = button1[index];
        textButton[1].text = button2[index];
        textButton[2].text = button3[index];
        textButton[3].text = button4[index];

    }

    void LaunchAlarm()
    {
        if (useAlarm)
        {
            alarm.Play();
        }
            
    }

}
