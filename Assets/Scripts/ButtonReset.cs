using System.Drawing.Imaging;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ButtonReset : MonoBehaviour
{
    private Image unityImg;
    private InputField width;
    private InputField level;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate () {
            //重置文本框值
            width = GameObject.FindGameObjectWithTag("Width").GetComponent<InputField>();
            width.text = ImageShow.InitWidth.ToString();
            level = GameObject.FindGameObjectWithTag("Level").GetComponent<InputField>();
            level.text = ImageShow.InitLevel.ToString();

            //还原显示图像
            MemoryStream ms = new MemoryStream();
            var filePath = ImageShow.FilePath;
            var img = ImageShow.getImage(filePath, ImageShow.InitWidth, ImageShow.InitLevel);
            unityImg = GameObject.FindGameObjectWithTag("CTImage").GetComponent<UnityEngine.UI.Image>();
            img.Item1.Save(ms, ImageFormat.Png);
            Texture2D tex2 = new Texture2D(img.Item1.Width, img.Item1.Height);
            tex2.LoadImage(ms.ToArray());
            Sprite sprite = Sprite.Create(tex2, new Rect(0, 0, tex2.width, tex2.height), Vector2.zero);
            unityImg.sprite = sprite;
            
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
