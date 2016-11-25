using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using System.Security.Cryptography.X509Certificates;

public class ProjectileLife : MonoBehaviour {

	public float highThreshold = 1f;
	public ParticleSystem ps;
	public float speed = 1;
	public MeshRenderer meshRenderer;

	public GameObject damages;

	[HideInInspector]
	public int shownDamages;
	[HideInInspector]
	public Vector3 targetPosition;

	private Vector3 initialPosition;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position + new Vector3(0,highThreshold,0);
		targetPosition += new Vector3 (0, highThreshold, 0);
		StartCoroutine (Life ());
	}
	
	private IEnumerator Life()
	{
		float t = 0;
		float dist = Vector3.Distance (initialPosition, targetPosition);
		//ps.emission.rate = speed*10;
		while (t<1)
		{
			t += speed * Time.deltaTime / dist;
			transform.position = Vector3.Lerp (initialPosition, targetPosition, t);
			yield return new WaitForEndOfFrame ();
		}
		meshRenderer.enabled = false;
		ps.Emit (100);
		Invoke ("DestroyThisGo",ps.startLifetime);
		DamagesLife dl = Instantiate (damages).GetComponent<DamagesLife>();
		dl.value = shownDamages.ToString ();
		dl.transform.position = targetPosition + new Vector3 (0, highThreshold, 0);
	}

	private void DestroyThisGo()
	{
		Destroy (this.gameObject);
	}
}
