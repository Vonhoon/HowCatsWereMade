using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBlock : MonoBehaviour
{
    //NewBlock 알림 언어 선택 기능

    public Text label;
    public Text description;
    public Text title;
    public DialogueContainer text;
    private List<string> textsToShow;

    private void Start()
    {
        if(GameManager.Instance.currentLang == "KR")
        {
            textsToShow = text.dialogues;
        }
        else
        {
            textsToShow = text.dialoguesEN;
        }
        title.text = textsToShow[0];
        label.text = textsToShow[1];
        description.text = textsToShow[2];
    }
}
