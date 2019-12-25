using UnityEngine;
using System.Collections;
public class MoveRotate : MonoBehaviour
{
    private float OffsetX = 0;
    private float OffsetY = 0;
    public float speed = 2f;//旋转速度

    void Update()
    {
        
        /*模型旋转功能*/
        if (Input.GetMouseButton(0) && Input.mousePosition[0] > 600 && Input.mousePosition[0] < 1800 && Input.mousePosition[1] > 100 && Input.mousePosition[1] < 950)//判断鼠标是否位于模型旋转区域
        {
            OffsetX = Input.GetAxis("Mouse X");//获取鼠标x轴的偏移量
            OffsetY = Input.GetAxis("Mouse Y");//获取鼠标y轴的偏移量
            //因未彻底理解unity旋转方式，临时采用以下办法实现旋转
            GameObject Body = GameObject.FindGameObjectWithTag("Body");
            GameObject SkinForRotate = GameObject.FindGameObjectWithTag("Skin");
            Body.transform.RotateAround(SkinForRotate.GetComponent<MeshRenderer>().bounds.center, new Vector3(OffsetY, -OffsetX, 0), speed);
            GameObject Flag = GameObject.FindGameObjectWithTag("Flag");
            GameObject FlagAxis = GameObject.FindGameObjectWithTag("Flag-Axis");
            Flag.transform.RotateAround(FlagAxis.GetComponent<MeshRenderer>().bounds.center, new Vector3(OffsetY, -OffsetX, 0), speed);
        }
    }

}