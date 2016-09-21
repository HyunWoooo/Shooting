using UnityEngine;
using System.Collections;

public class BuffText : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Destroy(this.gameObject, 1.0f);

        transform.localScale = new Vector3(10, 10, 1);

        GetComponent<RectTransform>().anchoredPosition3D = new Vector3(435, 0, 0);
        GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 180, 0);

    }
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.up * Time.deltaTime * 0.5f);

    }
}
