
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Togglenow : MonoBehaviour {   //控制器官显示


    string parentname;
    Toggle t;
    private bool isOn;

    // Use this for initialization
    void Awake()
    {
        GameObject parentGameObject = this.transform.parent.gameObject;
        parentname = parentGameObject.name;
    }
	// Use this for initialization
	void Start () {
        t = this.GetComponent<Toggle>();
        t.onValueChanged.AddListener(IsTag);
    }
	
	// Update is called once per frame
	void Update () {
        GameObject parentGameObject = this.transform.parent.gameObject;
    }
    public void IsTag(bool on)
    {

        if (on == false)
        {
            GameObject.FindGameObjectWithTag(parentname).GetComponent<Renderer>().enabled = false; 
        
        }
        else
        {
            GameObject.FindGameObjectWithTag(parentname).GetComponent<Renderer>().enabled = true;
           
        }
    
        //SwitchOn.SetActive(on);
        //SwitchOff.SetActive(!on);
    }

}

  
    

