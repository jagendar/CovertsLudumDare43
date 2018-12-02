using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimPerSecond : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        StartCoroutine(AnimTrigger());
	}
	
    IEnumerator AnimTrigger()
    {
        Animation anim = GetComponent<Animation>();
        if(anim == null)
        {
            yield break;
        }
        while(true)
        {
            anim.Play();
            yield return new WaitForSeconds(1f);
        }
    }
}
