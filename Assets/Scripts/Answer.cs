using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    public GameObject answerObject;
    public Button answerBtn;
    public Image outline;
    public Text answerText;

    public int chooseIndex;

    public void Initialize(string answerText)
    {
        this.answerText.text = answerText;
    }

    private void Start()
    {
        answerBtn.onClick.AddListener(ChooseOn);
    }

    public void ChooseOn()
    {
        GameMode.gameMode.answerIndex++;
        this.chooseIndex = GameMode.gameMode.answerIndex;

        outline.color = new Color(1f, 1f, 1f, 1f);

        switch (GameMode.gameMode.answerIndex)
        {
            case 1:
                outline.color = Color.red; break;
            case 2:
                outline.color = Color.yellow; break;
            case 3:
                outline.color = Color.green; break;
            case 4:
                outline.color = Color.blue; break;
        }

        answerBtn.onClick.RemoveAllListeners();
        answerBtn.onClick.AddListener(ChooseOff);

    }

    public void ChooseOff()
    {
        outline.color = new Color(0f, 0f, 0f, 0f);

        answerBtn.onClick.RemoveAllListeners();
        answerBtn.onClick.AddListener(ChooseOn);

        GameMode.gameMode.answerIndex--;
        this.chooseIndex = 0;
    }
}
