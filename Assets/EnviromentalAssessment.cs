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
    string swedish, english;

    public WordOption(int index, string swedish, string english)
    {
        this.index = index;
        this.swedish = swedish;
        this.english = english;
    }

    public string GetWord(int languageIndex)
    {
        if(languageIndex == 0)
        {
            return swedish;
        }
        else
        {
            return english;
        }
    }
}


public class EnviromentalAssessment : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI taskIndexText;
    [SerializeField] TextMeshProUGUI pressScreenText;
    [SerializeField] TextMeshProUGUI currentWordText;
    [SerializeField] TextMeshProUGUI idInfoText;
    [SerializeField] TextMeshProUGUI lowText;
    [SerializeField] TextMeshProUGUI highText;    

    WordOption[] words = new WordOption[] 
    {
        new WordOption(1, "modern", "modern"),
        new WordOption(2, "brokig", "cluttered"),
        new WordOption(3, "ful", "ugly"),
        new WordOption(4, "egendomlig", "strange"),
        new WordOption(5, "dyrbar", "expensive"),
        new WordOption(6, "maskulin", "masculine"),
        new WordOption(7, "stimulerande", "stimulating"),
        new WordOption(8, "sluten", "enclosed"),
        new WordOption(9, "funktionell", "functional"),
        new WordOption(10, "välvårdad", "well-maintained"),
        new WordOption(11, "vanlig", "common"),
        new WordOption(12, "trygg", "safe"),
        new WordOption(13, "stilren", "elegant"),
        new WordOption(14, "tråkig", "boring"),
        new WordOption(15, "ömtålig", "fragile"),
        new WordOption(16, "dämpad", "toned-down"),
        new WordOption(17, "tidlös", "timeless"),
        new WordOption(18, "öppen", "open"),
        new WordOption(19, "idyllisk", "idyllic"),
        new WordOption(20, "överraskande", "surprising"),
        new WordOption(21, "enkel", "simple"),
        new WordOption(22, "ålderdomlig", "historic"),
        new WordOption(23, "konsekvent", "consistent"),
        new WordOption(24, "livlig", "lively"),
        new WordOption(25, "bra", "good"),
        new WordOption(26, "avgränsad", "demarcated"),
        new WordOption(27, "kraftfull", "strong"),
        new WordOption(28, "ny", "new"),
        new WordOption(29, "påkostad", "lavish"),
        new WordOption(30, "sammansatt", "cohesive"),
        new WordOption(31, "trivsam", "pleasant"),
        new WordOption(32, "feminin", "feminine"),
        new WordOption(33, "helhetsbetonad", "complete"),
        new WordOption(34, "brutal", "brutal"),
        new WordOption(35, "speciell", "unusual"),
        new WordOption(36, "luftig", "airy")
    };

    string file;
    int wordIndex = 0;
    int participantID = 1;

    public void SetLanguage(int languageIndex)
    {
        if (languageIndex == 0)
        {
            lowText.text = "inte\nalls";
            highText.text = "väldigt";
            taskIndexText.text = "UPPGIFT 1";
            pressScreenText.text = "tryck på skärmen för att starta";
        }
        else
        {
            lowText.text = "not\nat all";
            highText.text = "very";
            taskIndexText.text = "TASK 1";
            pressScreenText.text = "press the screen to start";
        }
    }

    public void StartTask()
    {
        System.Random random = new System.Random();
        random.Shuffle(words);

        string participantsCountFile = Path.Combine(Application.persistentDataPath, "participants.csv");
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

        file = Path.Combine(Application.persistentDataPath, userID + ".csv");
        File.Create(file).Dispose();
        using (StreamWriter sw = File.AppendText(file))
        {
            sw.WriteLine(DateTime.Now.ToString());
        }

        currentWordText.text = words[wordIndex].GetWord(ExperimentalManager.GetLanguageIndex());
        
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

        string newLine = words[wordIndex].index.ToString() + "," + words[wordIndex].GetWord(ExperimentalManager.GetLanguageIndex()) + "," + choice.ToString();
        using (StreamWriter sw = File.AppendText(file))
        {
            sw.WriteLine(newLine);
        }

        ++wordIndex;
        if(wordIndex > 35)
        {
            using (StreamWriter sw = File.AppendText(file))
            {
                sw.WriteLine("###");
                sw.WriteLine("Item index,Observed object,estimated distance");
            }
            ExperimentalManager.EnvironmentDone();
        }
        else
        {
            currentWordText.text = words[wordIndex].GetWord(ExperimentalManager.GetLanguageIndex()); ;
        }
    }

    public void AddDistanceText(string objectNameAndDistance)
    {
        using (StreamWriter sw = File.AppendText(file))
        {
            sw.WriteLine(objectNameAndDistance);
        }
    }
}
