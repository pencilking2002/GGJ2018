using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
    	if (other.CompareTag("Player"))
    	{

	    	PerlinShake.Instance.Shake();
			Manager.Instance.audioManager.Play(AudioType.Falling);
	        GameObject go = other.gameObject;
	        Renderer rend = go.transform.Find("Mesh").GetComponent<Renderer>();
	        Color col = rend.material.color;

			LeanTween.value(1.0f, 0.0f, 0.3f).setOnUpdate((float val) => {
				rend.material.color = new Color(col.r, col.b, col.g, val);
			})
			.setOnComplete(() => {
	        	Destroy(go, 1.0f);
	        });
        }

    }
}
