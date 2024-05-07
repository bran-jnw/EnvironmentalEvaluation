using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuerySound : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI taskIndexText;
    [SerializeField] TextMeshProUGUI pressScreenText;
    public TMP_Text questionText;
    public TMP_Text currentItemText;
    public TMP_Text[] textButton;
    public AudioSource alarm;
    public float delayAlarm = 20f;
    public bool useAlarm = true;
    private int index;

    string[] itemsSwe = { "golvet", "trafikkonen", "brandsl�ckaren", "taket", "r�ren", "n�dskyltningen", "golvlisten", "handkontrollen", "ljuset", "golvet" };
    string[] itemsEng = { "floor", "traffic cone", "fire extinguisher", "ceiling", "pipes", "emergency signage", "floor skirting", "hand controller", "ligh", "floor" };

    //These are kept as public for now...
    string[] items;

    string[] button1Swe = { "svart", "vit & gul", "djupr�d", "gr�", "aluminium", "ljusgr�n", "moln gr�", "kobolt", "gul", "gr�n" };
    string[] button2Swe = { "m�rkgr�", "vit & orange", "m�rkr�d", "ljusgr�", "borstat st�l", "smaragdgr�n", "m�rkgr�", "svart", "vit & orange", "bl�" };
    string[] button3Swe = { "ljusgr�", "gul & orange", "karmin", "m�rkgr�", "metallisk", "gr�sgr�n", "naturvit", "tr�kol", "solljus", "m�rkgr�" };
    string[] button4Swe = { "brun", "vit & r�d", "schalakansr�tt", "l�tt st�l", "koppar", "olivgr�n", "ljusgr�", "midnatt svart", "lysr�r", "svart" };

    string[] button1Eng = { "black", "white & yellow", "crimson", "gray", "aluminum", "light green", "cloud gray", "cobalt", "yellow", "green" };
    string[] button2Eng = { "dark gray", "white & orange", "dark red", "light gray", "brushed steel", "emerald green", "dark gray", "black", "white", "blue" };
    string[] button3Eng = { "light gray", "yellow & orange", "carmine", "dark gray", "metalic", "grass green", "off-white", "charcoal", "sun light", "dark gray" };
    string[] button4Eng = { "brown", "white & red", "scarlet", "light steel", "copper", "olive green", "light gray", "midnight black", "fluorescent", "black" };

    string[] button1;
    string[] button2;
    string[] button3;
    string[] button4;

    public void SetLanguage(int languageIndex)
    {
        if (languageIndex == 0)
        {
            taskIndexText.text = "UPPGIFT 3";
            pressScreenText.text = "tryck p� sk�rmen f�r att starta";
            questionText.text = "Vad �r f�rgen p�";
            items = itemsSwe;
            button1 = button1Swe;
            button2 = button2Swe;
            button3 = button3Swe;
            button4 = button4Swe;
        }
        else
        {
            taskIndexText.text = "TASK 3";
            pressScreenText.text = "press the screen to start";
            questionText.text = "What is the color of the";
            items = itemsEng;
            button1 = button1Eng;
            button2 = button2Eng;
            button3 = button3Eng;
            button4 = button4Eng;
        }
    }

    public void StartTask()
    {
        index = 0;
        StartCountdown();
    }

    private void StartCountdown()
    {
        DisplayItem();
        Invoke("LaunchAlarm", delayAlarm);
    }


    public void Click_Key()
    {
        index++;

        if (index < items.Length)
        {
            DisplayItem();
        }
        else
        {
            index = 0;
            ExperimentalManager.AllDone();
        }
    }



    private void DisplayItem()
    {
        currentItemText.text = items[index];
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
