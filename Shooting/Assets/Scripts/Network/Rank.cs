using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class Rank : MonoBehaviour {

    struct Info
    {
        public string name;
        public int win;
        public int lose;
        public int score;
    };

    Text text;

    List<Info> rank_list;

    // Use this for initialization
    void Start () {

        rank_list = new List<Info>();

        text = GetComponent<Text>();

        text.text = "랭킹\n";

        StartCoroutine(ranking());

    }

    int SortList(Info p1, Info p2)
    {
        Info temp1 = p1;
        Info temp2 = p2;

        if (temp1.score < temp2.score)
            return 1;
        else if (temp1.score > temp2.score)
            return -1;

        return 0;
    }

    IEnumerator ranking()
    {
        WWW www = new WWW("http://203.130.66.30:80/shoot_ranking.php");
        //WWW www = new WWW("http://221.147.174.147/shoot_ranking.php");
        //WWW www = new WWW("http://127.0.0.1/shoot_ranking.php");

        yield return www;

        Debug.Log(www.text);

        JsonData json = JsonMapper.ToObject(www.text);

        int num = json.Count;

        for (int i = 0; i < num; i++)
        {
            Info info;

            JsonData user = json[i];
            string user_id = user["name"].ToString();
            int score = Convert.ToInt32(user["score"].ToString());
            int win = Convert.ToInt32(user["win"].ToString());
            int lose = Convert.ToInt32(user["lose"].ToString());

            info.name = user_id;
            info.score = score;
            info.win = win;
            info.lose = lose;

            rank_list.Add(info);
            rank_list.Sort(SortList);
        }

        for (int i = 0; i < rank_list.Count; i++)
        {
            if (rank_list.Count > 10)
                break;
            text.text += i + 1 + "위:" + rank_list[i].name + "\t\twin: " + rank_list[i].win + "\t\tlose: " + rank_list[i].lose + "\t\tscore: " + rank_list[i].score + "\n";
        }
    }
}
