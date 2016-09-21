using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour {

    public AudioSource audio;

	// Use this for initialization
	void Start () {

        audio = GetComponent<AudioSource>();

        DontDestroyOnLoad(this.gameObject);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BGMStop()
    {
        audio.Stop();
    }

    public void BGMPlay()
    {
        audio.Play();
    }
}
