using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;
    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕?:0", "이 곳에 처음 왔구나?:1" });
        talkData.Add(2000, new string[] { "오 반가워.:0", "여기엔 놀라운 전설이 있어.:1", "너가 찾아줄래?:0" });
        talkData.Add(100, new string[] { "평범한 나무상자다." });
        talkData.Add(200, new string[] { "누군가 사용했던 흔적이 있는 책상이다." });

        talkData.Add(1000 + 10, new string[] { "어서와.:0", "이곳엔 비밀이 숨겨져 있어.:1", "자세한 사항은 루도가 알려줄거야.:0" });
        talkData.Add(2000 + 11, new string[] { "오 안녕? 루나가 말했던 애구나?:0", "여기엔 놀라운 전설이 있어.:1", "너가 찾아줄래? 부탁할게.:0" });

        talkData.Add(1000 + 20, new string[] { "뭐 바위? 잘 찾아봐~:1" });
        talkData.Add(2000 + 20, new string[] { "소중한 바위야 꼭 부탁할게!:0" });
        talkData.Add(5000 + 20, new string[] { "바위를 찾았다." });
        talkData.Add(2000 + 21, new string[] { "오 찾아줘서 고마워!:0", "이 은혜 꼭 갚을게!:1" });

        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id)) {
            if (talkData.ContainsKey(id - id % 10)) {
                return GetTalk(id - id % 10, talkIndex);
            }
            
            else {
                return GetTalk(id - id % 100, talkIndex);
            }
        }

        if (talkIndex == talkData[id].Length) {
            return null;
        }

        else {
            return talkData[id][talkIndex];
        }
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
