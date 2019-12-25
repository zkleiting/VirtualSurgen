using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
    
    }
	
	// Update is called once per frame
	void Update () {
           

         Image CTimage = this.GetComponent<Image>();

        Sprite sprite = Resources.Load("IMG-0000", typeof(Sprite)) as Sprite;
        CTimage.sprite = sprite;
    }
}
