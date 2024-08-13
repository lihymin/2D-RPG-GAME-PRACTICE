using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public Text talkText;
    public Text questText;
    public Image portraitImg;
    public Animator talkPanel;
    public Animator portraitAnim;
    public GameObject scanObject;
    public GameObject menuSet;
    public int talkIndex;
    public bool isAction;
    public Sprite prevPortrait;
    public TypeEffect typeEffect;
    public GameObject player;
    void Start()
    {
        GameLoad();
        questText.text = questManager.CheckQuest();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            CancelPanel();
        }
    }

    public void CancelPanel()
    {
        if (menuSet.activeSelf) {
            menuSet.SetActive(false);
        }
        else {
            menuSet.SetActive(true);
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
            questText.text = questManager.CheckQuest(id);
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

        isAction = true;

        talkIndex++;
    }
    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }
    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX")) {
            return;
        }

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QuestId");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObject();
    }
    public void GameExit()
    {
        Application.Quit();
    }

}
