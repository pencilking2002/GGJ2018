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
	        	print(Manager.Instance.Game.players.Length);
	        	Manager.Instance.Game.numberOfPlayers--;

					if (Manager.Instance.Game.numberOfPlayers <= 1)
				{
					var logo = GameObject.FindGameObjectWithTag("Logo");

					Manager.Instance.Game.GameOver();

						LeanTween.move(logo, logo.transform.position + new Vector3(logo.transform.position.x+10, -165.8f,logo.transform.position.z), 1.0f)
						.setOnComplete(() => {
							LeanTween.delayedCall(3.0f, () => {
								Manager.Instance.Game.StartMenu(true);

								GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

								foreach(var p in players)
									Destroy(p);

								Manager.Instance.Game.players = null;
							});
					});
				}
					
	        });
        }

    }
}
