using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EvilDICOM;
using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing;
using UnityEngine.UI;
using System.IO;

public class ImageShow : MonoBehaviour
{
    public static String FilePath;
    private UnityEngine.UI.Image unityImg;
    private InputField width;
    private InputField level;
    public static double InitWidth;
    public static double InitLevel;
    // Start is called before the first frame update
    void Start()
    {
        MemoryStream ms = new MemoryStream();
        FilePath = "Assets/Resources/Patient1/StandartDicom/IMG-0000.dcm";//初始路径
        var img = getImage(FilePath, double.MaxValue, double.MaxValue);
        //记录初始WL值
        InitWidth = img.Item2;
        InitLevel = img.Item3;

        //更新显示图像
        unityImg = GameObject.FindGameObjectWithTag("CTImage").GetComponent<UnityEngine.UI.Image>();
        img.Item1.Save(ms, ImageFormat.Png);
        Texture2D tex2 = new Texture2D(img.Item1.Width, img.Item1.Height);
        tex2.LoadImage(ms.ToArray());
        Sprite sprite = Sprite.Create(tex2, new Rect(0, 0, tex2.width, tex2.height), Vector2.zero);
        unityImg.sprite = sprite;

        //更新文本框的值
        width = GameObject.FindGameObjectWithTag("Width").GetComponent<InputField>();
        width.text = img.Item2.ToString();
        level = GameObject.FindGameObjectWithTag("Level").GetComponent<InputField>();
        level.text = img.Item3.ToString();
    }

    public static Tuple<System.Drawing.Image, double, double> getImage(string fileName, double window, double level)
    {
        var dcm = DICOMObject.Read(fileName);
        string photo = dcm.FindFirst(TagHelper.PhotometricInterpretation).DData.ToString();
        ushort bitsAllocated = (ushort)dcm.FindFirst(TagHelper.BitsAllocated).DData;
        ushort highBit = (ushort)dcm.FindFirst(TagHelper.HighBit).DData;
        ushort bitsStored = (ushort)dcm.FindFirst(TagHelper.BitsStored).DData;
        double intercept = (double)dcm.FindFirst(TagHelper.RescaleIntercept).DData;
        double slope = (double)dcm.FindFirst(TagHelper.RescaleSlope).DData;
        ushort rows = (ushort)dcm.FindFirst(TagHelper.Rows).DData;
        ushort colums = (ushort)dcm.FindFirst(TagHelper.Columns).DData;
        ushort pixelRepresentation = (ushort)dcm.FindFirst(TagHelper.PixelRepresentation).DData;
        List<byte> pixelData = (List<byte>)dcm.FindFirst(TagHelper.PixelData).DData_;
        if (window == double.MaxValue)
        {
            window = (double)dcm.FindFirst(TagHelper.WindowWidth).DData;
        }
        if (level == double.MaxValue)
        {
            level = (double)dcm.FindFirst(TagHelper.WindowCenter).DData;
        }

        if (!photo.Contains("MONOCHROME"))//仅处理灰度图
            return null;
        int index = 0;
        byte[] outPixelData = new byte[rows * colums * 4];//rgba
        ushort mask = (ushort)(ushort.MaxValue >> (bitsAllocated - bitsStored));
        double maxval = Math.Pow(2, bitsStored);
        for (int i = 0; i < pixelData.Count; i += 2)
        {
            ushort gray = (ushort)((ushort)(pixelData[i]) + (ushort)(pixelData[i + 1] << 8));
            double valgray = gray & mask;//移除无用 bits
            if (pixelRepresentation == 1)
            {
                if (valgray > (maxval / 2))
                    valgray = (valgray - maxval);

            }
            valgray = slope * valgray + intercept;
            //窗位窗宽算法
            double half = ((window - 1) / 2.0) - 0.5;
            if (valgray <= level - half)
                valgray = 0;
            else if (valgray >= level + half)
                valgray = 255;
            else
                valgray = ((valgray - (level - 0.5)) / (window - 1) + 0.5) * 255;
            outPixelData[index] = (byte)valgray;
            outPixelData[index + 1] = (byte)valgray;
            outPixelData[index + 2] = (byte)valgray;
            outPixelData[index + 3] = 255;
            index += 4;
        }
        System.Drawing.Image newimage = ImageShow.ImageFromRawBgraArray(outPixelData, colums, rows);
        return new Tuple<System.Drawing.Image, double, double>(newimage, window, level);
    }

    //渲染
    public static System.Drawing.Image ImageFromRawBgraArray(
       byte[] arr, int width, int height)
    {
        var output = new Bitmap(width, height);
        var rect = new Rectangle(0, 0, width, height);
        var bmpData = output.LockBits(rect,ImageLockMode.ReadWrite, output.PixelFormat);
        var ptr = bmpData.Scan0;
        Marshal.Copy(arr, 0, ptr, arr.Length);
        output.UnlockBits(bmpData);
        return output;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
