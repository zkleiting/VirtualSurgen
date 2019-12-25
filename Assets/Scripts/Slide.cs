using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Slide : MonoBehaviour
{

    public Slider slider;
    string parentaname;
    public Text text;
    private Image unityImg;
    private InputField width;
    private InputField level;
    // Use this for initialization
    void Awake()
    {
        GameObject parentGameObject = this.transform.parent.gameObject;//找到操作的slider所属器官
        GameObject operateGameObject = parentGameObject.transform.GetChild(4).gameObject; //找到对应slider组件；为使脚本可在任意slider组件上使用，在unity建模时，slider与slider位置显示text应放在同一空父物体下，且slider应放在第五个物体的位置，即index=4
        slider = operateGameObject.GetComponent<Slider>();
        parentaname = parentGameObject.name;
        text = GetComponent<Text>();//找到显示slider数值的文本
        text.text = slider.value.ToString();
    }
    // Use this for initialization
    void Start()
    {
        slider.onValueChanged.AddListener(onSlider);    //当slider数值变化时，回调onSlider方法

    }

    // Update is called once per frame
    void Update()
    {

    }
    void onSlider(float value)
    {



        text.text = slider.value.ToString();//设置文本显示slider数值

        /*slider控制body中器官透明度*/
        if (parentaname == "Skin" || parentaname == "Vessel" || parentaname == "Liver")
        {
            Color color = GameObject.FindGameObjectWithTag(parentaname).GetComponent<MeshRenderer>().material.color;
            color.a = slider.value / 100;
            GameObject.FindGameObjectWithTag(parentaname).GetComponent<MeshRenderer>().material.color = color;

        }

        /*CT 图像帧切换功能*/
        if (parentaname == "Slider-Image")
        {
            var filePath = ImageShow.FilePath;
            //设置图片路径
            filePath = "Assets/Resources/Patient1/StandartDicom/";
            switch (slider.value.ToString().Length)
            {
                case 1:
                    {
                        filePath = filePath + "IMG-000" + slider.value.ToString() + ".dcm"; break;
                    }
                case 2:
                    {
                        filePath = filePath + "IMG-00" + slider.value.ToString() + ".dcm"; break;
                    }
                case 3:
                    {
                        filePath = filePath + "IMG-0" + slider.value.ToString() + ".dcm"; break;
                    }
            }
            ImageShow.FilePath = filePath;

            //CT图像显示区域切换图片
            MemoryStream ms = new MemoryStream();
            width = GameObject.FindGameObjectWithTag("Width").GetComponent<InputField>();
            level = GameObject.FindGameObjectWithTag("Level").GetComponent<InputField>();
            var img = ImageShow.getImage(filePath, double.Parse(width.text), double.Parse(level.text));
            unityImg = GameObject.FindGameObjectWithTag("CTImage").GetComponent<UnityEngine.UI.Image>();
            img.Item1.Save(ms, ImageFormat.Png);
            Texture2D tex2 = new Texture2D(img.Item1.Width, img.Item1.Height);
            tex2.LoadImage(ms.ToArray());
            Sprite sprite = Sprite.Create(tex2, new Rect(0, 0, tex2.width, tex2.height), Vector2.zero);
            unityImg.sprite = sprite;

            //移动定位标记
            GameObject CTPosition = GameObject.FindGameObjectWithTag("CTPosition");
            float pos_y = -183.7f + slider.value * 1.2f;//身体模型最低端距离+slider数值*（模型y轴高度/CT数量）
            //Debug.Log(pos_y);
            CTPosition.transform.localPosition = new Vector3(-193, pos_y, 0);
        }
    }

}
