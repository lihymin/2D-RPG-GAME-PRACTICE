using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public Text talkText;
    public Text QuestText;
    public Image portraitImg;
    public Animator talkPanel;
    public Animator portraitAnim;
    public GameObject scanObject;
    public GameObject menuSet;
    public int talkIndex;
    public bool isAction;
    public Sprite prevPortrait;
    public TypeEffect typeEffect;

    void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            if (menuSet.activeSelf) {
                menuSet.SetActive(false);
            }
            else {
                menuSet.SetActive(true);
            }
        }
    }
    public void Action(GameObject scanObj)
    {   
        scanObject = scanObj;
        ObjData objdata = scanObject.GetComponent<ObjData>();
        Talk(objdata.id, objdata.isNpc);            
        talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = 0;
        string talkData = "";

        if (typeEffect.isEffecting) {
            typeEffect.SetMsg("");
            return;
        }

        else {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }

        if (talkData == null) {
            isAction = false;
            talkIndex = 0;
            QuestText.text = questManager.CheckQuest(id);
            return;
        }

        if (isNpc) {
            typeEffect.SetMsg(talkData.Split(':')[0]);
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
            if (prevPortrait != portraitImg.sprite) {
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite;
            }
        }

        else {
            typeEffect.SetMsg(talkData);
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        if (typeEffect.msgText.text != typeEffect.targetMsg && typeEffect.isEffecting) {
            typeEffect.msgText.text = typeEffect.targetMsg;
        }

        isAction = true;

        talkIndex++;
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
