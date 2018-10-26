using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question
{
    public string question;
    public string answer; //Answer looks like this: 1-1,2-3,3-4,4-2
    public List<OptionQ> optionsQ;
    public List<OptionA> optionsA;

    public Question(string question, string answer, List<OptionQ> optionsQ, List<OptionA> optionsA)
    {
        this.question = question;
        this.answer = answer;
        this.optionsQ = optionsQ;
        this.optionsA = optionsA;
    }
}

public class OptionQ : MonoBehaviour
{
    public string optionText;
    public Sprite optionIcon;

    public OptionQ(string optionText, Sprite optionIcon)
    {
        this.optionText = optionText;
        this.optionIcon = optionIcon;
    }
}

public class OptionA : MonoBehaviour
{
    public string optionText;
    public Sprite optionIcon;

    public OptionA(string optionText, Sprite optionIcon)
    {
        this.optionText = optionText;
        this.optionIcon = optionIcon;
    }

}