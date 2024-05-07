using UnityEngine;
using TMPro;

public class QueryDistance : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI taskIndexText;
    [SerializeField] TextMeshProUGUI pressScreenText;
    [SerializeField] TextMeshProUGUI distanceToText;
    [SerializeField] TMP_Text objectText;    
    [SerializeField]TMP_Text distanceValueText;
    [SerializeField] TMP_Text metersText;

    public int index;

    string[] itemsTextSwe = { "brandsläckaren", "traffikkonen", "slutet av korridoren" };
    //These are kept as public for now...
    string[] itemsTextEng = { "fire extinguisher", "traffic cone", "fire extinguisher"};
    string[] itemsText;
    

    public string[] itemValue;
    public int[] order;

    public void SetLanguage(int languageIndex)
    {
        if (languageIndex == 0)
        {
            taskIndexText.text = "UPPGIFT 2";
            pressScreenText.text = "tryck på skärmen för att starta";
            distanceToText.text = "Uppskatta avståndet till";
            metersText.text = "meter";
            itemsText = itemsTextSwe;
        }
        else
        {
            taskIndexText.text = "TASK 2";
            pressScreenText.text = "press the screen to start";
            distanceToText.text = "Estimate the distance to the";
            metersText.text = "meters";
            itemsText = itemsTextEng;
        }
    }

    public void StartTask()
    {
        itemValue = new string[itemsText.Length];

        order = new int[itemsText.Length];
        for (int i = 0; i < itemsText.Length; i++)
        {
            order[i] = i;
        }            
        ReshuffleInt(order);

        index = 0;

        objectText.text = itemsText[order[index]];

    }

    public void Click_period()
    {
        if (!itemValue[order[index]].EndsWith("."))
        {
            itemValue[order[index]] += "."; // + new value
            distanceValueText.text = itemValue[order[index]].ToString();
        }
    }

    public void Click_numKey(int value)
    {
        //itemValue[order[index]] *= 10; // previous value * 10
        itemValue[order[index]] += value; // + new value
        distanceValueText.text = itemValue[order[index]].ToString();
    }

    public void Click_clear()
    {
        itemValue[order[index]] = "";
        distanceValueText.text = itemValue[order[index]].ToString();
    }
    public void Click_OK()
    {
        index++;
        if (index < itemsText.Length)
        {
            distanceValueText.text = "";
            objectText.text = itemsText[order[index]];
        }
        else
        {
            SaveData();
            index = 0;
            distanceValueText.text = "";
            ExperimentalManager.DistanceDone();
        }
    }

    private void SaveData()
    {
        for (int i = 0; i < itemsText.Length; i++)
        {
            string line = order[i] + "," + itemsText[order[i]] + "," + itemValue[order[i]];
            gameObject.GetComponent<EnviromentalAssessment>().AddDistanceText(line);
        }
    }



    private void ReshuffleInt(int[] list)
    {
        // Knuth shuffle algorithm
        for (int t = 0; t < list.Length; t++)
        {
            int tmp = list[t];
            int r = UnityEngine.Random.Range(t, list.Length);
            list[t] = list[r];
            list[r] = tmp;
        }
    }

}
