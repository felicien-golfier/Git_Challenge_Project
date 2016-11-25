using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour {

	public float speed = 1;
	public float threshold = 1.2f;
	public Material highlightedMaterial;
	public Material movingMaterial;
	private Material selectedMaterial;
	private MeshRenderer meshRenderer;
	private IEnumerator coroutine = null;
	private Vector3 initialScale;
	private Vector3 targetScale;

    private static float lifeTime = 0;

	private void Start()
	{
		
		meshRenderer = GetComponent<MeshRenderer> ();
		meshRenderer.enabled = false;
		selectedMaterial = meshRenderer.material;

		initialScale = transform.localScale;
		targetScale = initialScale * threshold;
	}
	public bool On 
	{
		set 
		{
			if (value)
			{
				if (coroutine == null){
					meshRenderer.enabled = true;
					coroutine = Life ();
					StartCoroutine (coroutine);
				}
			}else
			{
				coroutine = null;
				meshRenderer.enabled = false;
			} 
		}

		get 
		{
			return coroutine != null;
		}
	}

	private IEnumerator Life()
	{
		int dir = lifeTime < 0 ? -1 : 1;
		float t = lifeTime < 0 ? -lifeTime : lifeTime;
		while (coroutine != null)
		{
			t = t + dir * speed * Time.deltaTime;
            lifeTime = t * dir;
            transform.localScale = Vector3.Lerp (initialScale, targetScale, t);
			if (t >= 1)
				dir = -1;
			else if (t <= 0)
				dir = 1;
			yield return new WaitForEndOfFrame ();
		}
        transform.localScale = initialScale;
        
    }

	public void SetMove(bool active, bool enableRenderer){
		meshRenderer.material = active ? movingMaterial : selectedMaterial;
		meshRenderer.enabled = enableRenderer;
	}
}
