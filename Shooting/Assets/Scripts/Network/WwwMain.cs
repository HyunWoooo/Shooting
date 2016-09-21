using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class WwwMain : MonoBehaviour {

    private Dictionary<string, string> session = new Dictionary<string, string>();

    Text userid;
    Text passwd;
    int updatescore;

    Text nametext;
    Text playerInfo;

    GameObject LoginText;
    bool loginfail;
    bool createfail;

    int user_score;
    int user_win;
    int user_lose;

    bool LobbyInit;

    // Use this for initialization
    void Start () {
        session["Cookie"] = null;

        LoginText = GameObject.Find("LoginText");
        DontDestroyOnLoad(this.gameObject);

    }
	
	// Update is called once per frame
	void Update () {

        if (Application.loadedLevelName == "Lobby" && !LobbyInit)
        {
            StartCoroutine(lobby());
            LobbyInit = true;
        }
    }

    IEnumerator login()
    {
        LoginText = GameObject.Find("LoginText");
        userid = GameObject.Find("userid").GetComponent<Text>();
        passwd = GameObject.Find("passwd").GetComponent<Text>();

        WWWForm form = new WWWForm();
        form.AddField("userid", userid.text);
        form.AddField("passwd", passwd.text);
        WWW www = new WWW("http://203.130.66.30:80/shoot_login.php", form);
        //WWW www = new WWW("http://221.147.174.147/shoot_login.php", form);

        yield return www;

        if (www.text == "login fail")
            loginfail = true;
        else
            loginfail = false;

        session["Cookie"] = null;

        if (www.responseHeaders.ContainsKey("SET-COOKIE"))
        {
            char[] splitter = { ';' };
            string[] v = www.responseHeaders["SET-COOKIE"].Split(splitter);
            foreach (string s in v)
            {
                if (string.IsNullOrEmpty(s)) continue;
                session["Cookie"] = s;
                break;
            }
        }
        if (loginfail)
            LoginText.GetComponent<Text>().enabled = true;
        else
        {
            LoginText.GetComponent<Text>().enabled = false;
            Application.LoadLevel("Lobby");
        }
    }

    IEnumerator lobby()
    {
        playerInfo = GameObject.Find("playerInfo").GetComponent<Text>();

        WWW www = new WWW("http://203.130.66.30:80/shoot_lobby.php", null, session);
        //WWW www = new WWW("http://221.147.174.147/shoot_lobby.php", null, session);

        yield return www;


        Debug.Log(www.text);

        JsonData json = JsonMapper.ToObject(www.text);

        int num = json.Count;

        for (int i = 0; i < num; i++)
        {
            JsonData user = json[i];
            string user_name = user["name"].ToString();
            int score = Convert.ToInt32(user["score"].ToString());
            int win = Convert.ToInt32(user["win"].ToString());
            int lose = Convert.ToInt32(user["lose"].ToString());

            user_score = score;
            user_win = win;
            user_lose = lose;

            playerInfo.text = "이름: " + user_name + "\n";
            playerInfo.text += "승: " + win + "\n";
            playerInfo.text += "패: " + lose + "\n";
            playerInfo.text += "승점: " + score + "\n";
        }    
    }

    IEnumerator scoreupdate()
    {
        WWWForm form = new WWWForm();
        form.AddField("score", user_score);
        form.AddField("win", user_win);
        form.AddField("lose", user_lose);

        WWW www = new WWW("http://203.130.66.30:80/shoot_score.php", form.data, session);
        //WWW www = new WWW("http://221.147.174.147/shoot_score.php", form.data, session);

        yield return www;

        LobbyInit = false;
    }

    IEnumerator createuser()
    {
        userid = GameObject.Find("userid").GetComponent<Text>();
        passwd = GameObject.Find("passwd").GetComponent<Text>();
        nametext = GameObject.Find("name").GetComponent<Text>();

        WWWForm form = new WWWForm();
        form.AddField("userid", userid.text);
        form.AddField("passwd", passwd.text);
        form.AddField("name", nametext.text);

        WWW www = new WWW("http://203.130.66.30:80/shoot_member.php", form);
        //WWW www = new WWW("http://221.147.174.147/shoot_member.php", form);

        yield return www;

        Debug.Log(www.text);

        if (www.text == "member failed")
            createfail = true;
        else
            createfail = false;

        if (!createfail)
            Application.LoadLevel("Login");
    }

    IEnumerator userdelete()
    {
        WWW www = new WWW("http://203.130.66.30:80/shoot_delete.php", null, session);
        //WWW www = new WWW("http://221.147.174.147/shoot_delete.php", null, session);

        yield return www;

        Debug.Log(www.text);

        StartCoroutine(logout());
    }

    IEnumerator logout()
    {
        WWWForm form = new WWWForm();

        WWW www = new WWW("http://203.130.66.30:80/session_logout.php", null, session);
        //WWW www = new WWW("http://221.147.174.147/session_logout.php", null, session);

        yield return www;

        Debug.Log(www.text);

        userid = null;
        passwd = null;
        updatescore = 0;
        LobbyInit = false;

        Application.LoadLevel("Login");
    }

    public void RankButton()
    {
        Application.LoadLevel("Rank");
    }

    public void LoginButton()
    {
        StartCoroutine(login());
    }

    public void LogOutButton()
    {
        StartCoroutine(logout());
    }

    public void ScoreUpdate()
    {
        StartCoroutine(scoreupdate());
    }

    public void CreateUser()
    {
        StartCoroutine(createuser());
    }

    public void DeleteUser()
    {
        StartCoroutine(userdelete());
    }

    public void SetWin()
    {
        user_win++;
    }

    public void SetLose()
    {
        user_lose++;
    }

    public void SetScore(int score)
    {
        user_score += score;
    }
}
