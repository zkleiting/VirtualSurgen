using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class Button : MonoBehaviour {

    public void Login()
    {
        SceneManager.LoadScene(1);//1是场景的索引
                                  // Application.LoadLevel(sceneName);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
           UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void Back()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index-1);
    }

    public void CT_login()
    {
        SceneManager.LoadScene(2);
    }

    public void Real_login()
    {
        SceneManager.LoadScene(3);
    }

    public void Load()   //临时加载按钮，加载数据功能未完善
    {
        SceneManager.LoadScene(4);
    }

    public void ButtonPlus() { 

        this.GetComponent<Slider>().value ++;
    
    }

    public void ButtonMinus()   
    {
        this.GetComponent<Slider>().value --;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
