using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionObj : MonoBehaviour
{

    public GameObject questionObject;
    public Button questionBtn;
    public Image outline;
    public Image icon;
    public Text questionText;

    public int chooseIndex;

    private void Start()
    {
        questionBtn.onClick.AddListener(ChooseOn);
    }

    public void Initialize(string questionText, Sprite icon)
    {
        this.questionText.text = questionText;
        this.icon.sprite = icon != null ? icon : this.icon.sprite; //This is a crutch! Need remake it.
    }

    public void ChooseOn()
    {
        GameMode.gameMode.questionIndex++;
        this.chooseIndex = GameMode.gameMode.questionIndex;


        outline.color = new Color(1f, 1f, 1f, 1f);

        switch (GameMode.gameMode.questionIndex)
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

        questionBtn.onClick.RemoveAllListeners();
        questionBtn.onClick.AddListener(ChooseOff);
    }

    public void ChooseOff()
    {
        outline.color = new Color(0f, 0f, 0f, 0f);

        questionBtn.onClick.RemoveAllListeners();
        questionBtn.onClick.AddListener(ChooseOn);

        GameMode.gameMode.questionIndex--;
        this.chooseIndex = 0;
    }
}
