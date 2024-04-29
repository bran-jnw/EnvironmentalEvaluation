using UnityEngine;
using TMPro;

public class QueryDistance : MonoBehaviour
{
    public TMP_Text textItem;
    public TMP_Text currentValue;

    public int index;

    //These are kept as public for now...
    public string[] itemTextE = { "End of corridor",
                                "Traffic cone",
                                "Fire extinguisher"};

    public string[] itemValue;
    public int[] order;

    void Start()
    {
        //canvas.SetActive(false);

        itemValue = new string[itemTextE.Length];

        order = new int[itemTextE.Length];
        for (int i = 0; i < itemTextE.Length; i++)
        {
            order[i] = i;
        }            
        ReshuffleInt(order);

        index = 0;

        textItem.text = itemTextE[order[index]];

    }

    public void Click_period()
    {
        if (!itemValue[order[index]].EndsWith("."))
        {
            itemValue[order[index]] += "."; // + new value
            currentValue.text = itemValue[order[index]].ToString();
        }
    }

    public void Click_numKey(int value)
    {
        //itemValue[order[index]] *= 10; // previous value * 10
        itemValue[order[index]] += value; // + new value
        currentValue.text = itemValue[order[index]].ToString();
    }

    public void Click_clear()
    {
        itemValue[order[index]] = "";
        currentValue.text = itemValue[order[index]].ToString();
    }
    public void Click_OK()
    {
        index++;
        if (index < itemTextE.Length)
        {
            currentValue.text = "";
            textItem.text = itemTextE[order[index]];
        }
        else
        {
            SaveData();
            //canvas.SetActive(false);
            index = 0;
            currentValue.text = "";
            //control.Distance_end();
            QuestionManager.DistanceDone();
        }
    }

    private void SaveData()
    {
        for (int i = 0; i < itemTextE.Length; i++)
        {
            //control.WriteAnswerLine(order[i] + ", " + itemTextE[order[i]] + ", " + itemValue[order[i]]);
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
