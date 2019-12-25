using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class ButtonEnsure : MonoBehaviour
{
    private Image unityImg;
    private InputField width;
    private InputField level;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate () {
            width = GameObject.FindGameObjectWithTag("Width").GetComponent<InputField>();
            level = GameObject.FindGameObjectWithTag("Level").GetComponent<InputField>();
            var filePath = ImageShow.FilePath;
            MemoryStream ms = new MemoryStream();
            var img = ImageShow.getImage(filePath, double.Parse(width.text),double.Parse(level.text));
            unityImg = GameObject.FindGameObjectWithTag("CTImage").GetComponent<UnityEngine.UI.Image>();
            img.Item1.Save(ms, ImageFormat.Png);
            Texture2D _tex2 = new Texture2D(img.Item1.Width, img.Item1.Height);
            _tex2.LoadImage(ms.ToArray());
            Sprite sprite = Sprite.Create(_tex2, new Rect(0, 0, _tex2.width, _tex2.height), Vector2.zero);
            unityImg.sprite = sprite;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
