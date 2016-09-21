using UnityEngine;
using System.Collections;

public class DamageText : MonoBehaviour {

    float timer;

	// Use this for initialization
	void Start () {

        GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 0, 0);
        GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 180, 0);

    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (timer >= 0.1f)
        {
            transform.localScale = new Vector3(transform.localScale.x - 20f * Time.deltaTime, transform.localScale.y - 20f * Time.deltaTime, 1);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x + 80f * Time.deltaTime, transform.localScale.y + 80f * Time.deltaTime, 1);
        }

        if (transform.localScale.x <= 0)
            Destroy(this.gameObject);

        transform.Translate(Vector3.up * Time.deltaTime * 0.5f);

    }
}
