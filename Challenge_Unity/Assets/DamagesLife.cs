using UnityEngine;
using System.Collections;

public class DamagesLife : MonoBehaviour {

	public float lifeTime = 2;
	public float speed = 1;
	public TextMesh textMesh;

	public string value = "0";
	// Use this for initialization
	void Start () {
		textMesh = textMesh != null ? textMesh : GetComponent<TextMesh> ();
		textMesh.text = value;
		Invoke ("DestroyThisGo", lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (0, Time.deltaTime * speed, 0);
		transform.LookAt (Camera.current.transform.position);
	}

	private void DestroyThisGo(){
		Destroy (gameObject);
	}
}
