using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System;
using Unity.Burst.Intrinsics;
using UnityEngine.EventSystems;

static class RandomExtensions
{
    public static void Shuffle<T>(this System.Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}


struct WordOption
{
    public int index;
    public string word;

    public WordOption(int index, string word)
    {
        this.index = index;
        this.word = word;
    }
}


public class EnviromentalAssessment : MonoBehaviour
{ 
    [SerializeField] TextMeshProUGUI currentWordText;
    [SerializeField] TextMeshProUGUI idInfoText;
    [SerializeField] GameObject[] objectsToDisbaleWhenDone;

    WordOption[] words = new WordOption[] 
    { 
        new WordOption(1, "modern"),
        new WordOption(2, "cluttered"),
        new WordOption(3, "ugly"),
        new WordOption(4, "strange"),
        new WordOption(5, "expensive"),
        new WordOption(6, "masculine"),
        new WordOption(7, "stimulating"),
        new WordOption(8, "enclosed"),
        new WordOption(9, "functional"),
        new WordOption(10, "well-maintained"),
        new WordOption(11, "common"),
        new WordOption(12, "safe"),
        new WordOption(13, "elegant"),
        new WordOption(14, "boring"),
        new WordOption(15, "fragile"),
        new WordOption(16, "toned-down"),
        new WordOption(17, "timeless"),
        new WordOption(18, "open"),
        new WordOption(19, "idyllic"),
        new WordOption(20, "surprising"),
        new WordOption(21, "simple"),
        new WordOption(22, "historic"),
        new WordOption(23, "consistent"),
        new WordOption(24, "lively"),
        new WordOption(25, "good"),
        new WordOption(26, "demarcated"),
        new WordOption(27, "strong"),
        new WordOption(28, "new"),
        new WordOption(29, "lavish"),
        new WordOption(30, "cohesive"),
        new WordOption(31, "pleasant"),
        new WordOption(32, "feminine"),
        new WordOption(33, "complete"),
        new WordOption(34, "brutal"),
        new WordOption(35, "unusual"),
        new WordOption(36, "airy"),
    };

    string file;
    int wordIndex = 0;
    int participantID = 1;

    private void Start()
    {
        System.Random random = new System.Random();
        random.Shuffle(words);

        string participantsCountFile = Path.Combine(UnityEngine.Application.persistentDataPath, "participants.csv");
        if (File.Exists(participantsCountFile))
        {
            string[] lines = File.ReadAllLines(participantsCountFile);
            if(lines.Length > 1)
            {
                int.TryParse(lines[lines.Length - 1].Split(",")[0], out participantID);
                ++participantID;
                using (StreamWriter sw = File.AppendText(participantsCountFile))
                {
                    sw.WriteLine(participantID + "," + DateTime.Now.ToString());
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(participantsCountFile))
                {
                    sw.WriteLine("ParticipantID,Date");
                    sw.WriteLine(participantID + "," + DateTime.Now.ToString());
                }
            }
        }
        else
        {
            File.Create(participantsCountFile).Dispose();
            using (StreamWriter sw = File.AppendText(participantsCountFile))
            {
                sw.WriteLine("ParticipantID,Date");
                sw.WriteLine(participantID + "," + DateTime.Now.ToString());
            }
        }

        string userID = participantID.ToString();
        idInfoText.text = "Participant ID: " + userID; 

        file = Path.Combine(UnityEngine.Application.persistentDataPath, userID + ".csv");
        File.Create(file).Dispose();
        using (StreamWriter sw = File.AppendText(file))
        {
            sw.WriteLine(DateTime.Now.ToString());
        }

        currentWordText.text = words[wordIndex].word;
    }

    public void Reset()
    {
        if(wordIndex > 35)
        {
            wordIndex = 0;
            participantID = 1;

            for (int i = 0; i < objectsToDisbaleWhenDone.Length; i++)
            {
                objectsToDisbaleWhenDone[i].SetActive(true);
            }
            Start();
        }        
    }

    public void ClickOn1()
    {
        SaveClick(1);
    }

    public void ClickOn2()
    {
        SaveClick(2);
    }

    public void ClickOn3()
    {
        SaveClick(3);
    }

    public void ClickOn4()
    {
        SaveClick(4);
    }

    public void ClickOn5()
    {
        SaveClick(5);
    }

    public void ClickOn6()
    {
        SaveClick(6);
    }

    public void ClickOn7()
    {
        SaveClick(7);
    }

    private void SaveClick(int choice)
    {
        if (wordIndex > 35)
        {
            return;
        }

        EventSystem.current.SetSelectedGameObject(null);

        string newLine = words[wordIndex].index.ToString() + "," + words[wordIndex].word + "," + choice.ToString();
        using (StreamWriter sw = File.AppendText(file))
        {
            sw.WriteLine(newLine);
        }

        ++wordIndex;
        if(wordIndex > 35)
        {
            /*currentWordText.text = "Thank you.";
            for (int i = 0; i < objectsToDisbaleWhenDone.Length; i++)
            {
                objectsToDisbaleWhenDone[i].SetActive(false);
            }*/
            QuestionManager.EnvironmentDone();
        }
        else
        {
            currentWordText.text = words[wordIndex].word;
        }
    }
}
