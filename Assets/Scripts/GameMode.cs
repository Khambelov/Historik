using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class GameMode : MonoBehaviour
{

    public static GameMode gameMode;

    public Image loadingScreen;
    public GameObject loadingBar;

    List<Question> questList;

    [Header("Quest text")]
    public Text questText;

    [Header("Question Board")]
    public List<QuestionObj> questionsObj;

    [Header("Answer Board")]
    public List<Answer> answers;

    public int questionIndex;
    public int answerIndex;

    string rightAnswer;

    private void Awake()
    {
        gameMode = this;
    }

    private void Start()
    {
        LoadingScreen();
    }

    #region Loading
    void LoadingScreen()
    {
        loadingScreen.gameObject.SetActive(true);
        loadingBar.SetActive(true);
        StartCoroutine(RotateBar());
        StartCoroutine(LoadingData());
    }

    IEnumerator RotateBar()
    {
        while (true)
        {
            try
            {
                loadingBar.transform.eulerAngles = new Vector3(loadingBar.transform.eulerAngles.x, loadingBar.transform.eulerAngles.y, loadingBar.transform.eulerAngles.z - 5f);
            }
            catch
            {
                yield break;
            }

            yield return new WaitForSeconds(0.001f);
        }
    }

    IEnumerator LoadingData()
    {
        if (answers.Count != questionsObj.Count)
            yield break;

        string json = System.IO.File.ReadAllText(@"D:\Unity Works\FunProject\Historik\Assets\document.json");
        var data = JObject.Parse(json);

        questList = new List<Question>();
        List<OptionQ> optQ;
        List<OptionA> optA;

        for (int i = 0; i < JObject.Parse(json).Count; i++)
        {
            var optionQs = data[i.ToString()]["optionQ"].ToObject<JObject>(); 
            var optionAs = data[i.ToString()]["optionA"].ToObject<JObject>();

            optQ = new List<OptionQ>();
            optA = new List<OptionA>();

            for (int j = 0; j < optionQs.Count; j++)
            {
                optQ.Add(new OptionQ(optionQs[j.ToString()].Value<string>(), null));
                optA.Add(new OptionA(optionAs[j.ToString()].Value<string>(), null));

                yield return null;
            }

            questList.Add(new Question(data[i.ToString()]["question"].Value<string>(),
                                       data[i.ToString()]["answer"].Value<string>(),
                                       optQ, optA));

            yield return null;
        }

        GetQuestion();

        Destroy(loadingBar.gameObject);

        while (loadingScreen.color.a > 0f)
        {
            loadingScreen.color = new Color(loadingScreen.color.r, loadingScreen.color.g, loadingScreen.color.b, loadingScreen.color.a - 0.03f);
            yield return new WaitForSeconds(0.01f);
        }

        Destroy(loadingScreen.gameObject);

        yield return null;
    }

    void GetQuestion()
    {
        Question newQuest = questList[Random.Range(0, questList.Count)];

        if (newQuest.question.Equals(questText.text))
        {
            GetQuestion();
            return;
        }

        questText.text = newQuest.question;

        for (int i = 0; i < questionsObj.Count; i++)
        {
            questionsObj[i].Initialize(newQuest.optionsQ[i].optionText, newQuest.optionsQ[i].optionIcon);
            answers[i].Initialize(newQuest.optionsA[i].optionText);
        }

        rightAnswer = newQuest.answer;
    }
    #endregion

    public void ClearAll()
    {
        foreach (Answer answer in answers)
        {
            answer.ChooseOff();
        }

        foreach (QuestionObj questionObj in questionsObj)
        {
            questionObj.ChooseOff();
        }

        questionIndex = 0;
        answerIndex = 0;
    }

    public void Confirm()
    {
        string result = null;

        for (int i = 0; i < questionsObj.Count; i++)
        {
            result = string.Concat(result, questionsObj[i].chooseIndex, "-", answers[i].chooseIndex, ",");
        }

        result = result.TrimEnd(',');

        if (result.Equals(rightAnswer))
            Debug.Log("Right!");
        else
            Debug.Log("Wrong!");

        ClearAll();
        GetQuestion();
    }
}