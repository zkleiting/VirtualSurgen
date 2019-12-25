using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System;
using System.Diagnostics;

public class TopMost : MonoBehaviour {
    public enum appStyle
    {
        FullScreen,
        WindowedFullScreen,
        Windowed,
        WindowedWithoutBorder
    }
    public enum zDepth
    {
        Normal,
        Top,
        TopMost
    }
    private const uint SWP_SHOWWINDOW = 64u;
    private const int GWL_STYLE = -16;
    private const int WS_BORDER = 1;
    private const int GWL_EXSTYLE = -20;
    private const int WS_CAPTION = 12582912;
    private const int WS_POPUP = 8388608;
    private const int SM_CXSCREEN = 0;
    private const int SM_CYSCREEN = 1;
    public TopMost.appStyle AppWindowStyle = TopMost.appStyle.WindowedFullScreen;
    public TopMost.zDepth ScreenDepth = zDepth.TopMost;
    public int windowLeft = 10;
    public int windowTop = 10;
    public int windowWidth  ;
    public int windowHeight ;
    private Rect screenPosition;
    private IntPtr HWND_TOP = new IntPtr(0);
    private IntPtr HWND_TOPMOST = new IntPtr(-1);
    private IntPtr HWND_NORMAL = new IntPtr(-2);
    private int Xscreen;
    private int Yscreen;
    private int i;
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hPos, int x, int y, int cx, int cy, uint nflags);
    [DllImport("User32.dll")]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [DllImport("User32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    [DllImport("User32.dll")]
    private static extern int GetWindowLong(IntPtr hWnd, int dwNewLong);
    [DllImport("User32.dll")]
    private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wP, IntPtr IP);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SetParent(IntPtr hChild, IntPtr hParent);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetParent(IntPtr hChild);
    [DllImport("User32.dll")]
    public static extern IntPtr GetSystemMetrics(int nIndex);
    private void Start()
    {
        this.Xscreen = (int)TopMost.GetSystemMetrics(0);
        this.Yscreen = (int)TopMost.GetSystemMetrics(1);
        if (this.AppWindowStyle == TopMost.appStyle.FullScreen)
        {
            Screen.SetResolution(this.Xscreen, this.Yscreen, true);
        }
        if (this.AppWindowStyle == TopMost.appStyle.WindowedFullScreen)
        {
            Screen.SetResolution(this.Xscreen - 1, this.Yscreen - 1, false);
            this.screenPosition = new Rect(0f, 0f, (float)(this.Xscreen - 1), (float)(this.Yscreen - 1));
        }
        if (this.AppWindowStyle == TopMost.appStyle.Windowed)
        {
            Screen.SetResolution(this.windowWidth, this.windowWidth, false);
        }
        if (this.AppWindowStyle == TopMost.appStyle.WindowedWithoutBorder)
        {
            Screen.SetResolution(this.windowWidth, this.windowWidth, false);
            this.screenPosition = new Rect((float)this.windowLeft, (float)this.windowTop, (float)this.windowWidth, (float)this.windowHeight);
        }
        windowWidth = Screen.width;
        windowHeight = Screen.height;
    }
    private void Update()
    {
        if (this.i < 5)
        {
            if (this.AppWindowStyle == TopMost.appStyle.WindowedFullScreen)
            {
                TopMost.SetWindowLong(TopMost.GetForegroundWindow(), -16, 369164288);
                if (this.ScreenDepth == TopMost.zDepth.Normal)
                {
                    TopMost.SetWindowPos(TopMost.GetForegroundWindow(), this.HWND_NORMAL, (int)this.screenPosition.x, (int)this.screenPosition.y, (int)this.screenPosition.width, (int)this.screenPosition.height, 64u);
                }
                if (this.ScreenDepth == TopMost.zDepth.Top)
                {
                    TopMost.SetWindowPos(TopMost.GetForegroundWindow(), this.HWND_TOP, (int)this.screenPosition.x, (int)this.screenPosition.y, (int)this.screenPosition.width, (int)this.screenPosition.height, 64u);
                }
                if (this.ScreenDepth == TopMost.zDepth.TopMost)
                {
                    TopMost.SetWindowPos(TopMost.GetForegroundWindow(), this.HWND_TOPMOST, (int)this.screenPosition.x, (int)this.screenPosition.y, (int)this.screenPosition.width, (int)this.screenPosition.height, 64u);
                }
                TopMost.ShowWindow(TopMost.GetForegroundWindow(), 3);
            }
            if (this.AppWindowStyle == TopMost.appStyle.Windowed)
            {
                if (this.ScreenDepth == TopMost.zDepth.Normal)
                {
                    TopMost.SetWindowPos(TopMost.GetForegroundWindow(), this.HWND_NORMAL, 0, 0, 0, 0, 3u);
                    TopMost.SetWindowPos(TopMost.GetForegroundWindow(), this.HWND_NORMAL, 0, 0, 0, 0, 35u);
                }
                if (this.ScreenDepth == TopMost.zDepth.Top)
                {
                    TopMost.SetWindowPos(TopMost.GetForegroundWindow(), this.HWND_TOP, 0, 0, 0, 0, 3u);
                    TopMost.SetWindowPos(TopMost.GetForegroundWindow(), this.HWND_TOP, 0, 0, 0, 0, 35u);
                }
                if (this.ScreenDepth == TopMost.zDepth.TopMost)
                {
                    TopMost.SetWindowPos(TopMost.GetForegroundWindow(), this.HWND_TOPMOST, 0, 0, 0, 0, 3u);
                    TopMost.SetWindowPos(TopMost.GetForegroundWindow(), this.HWND_TOPMOST, 0, 0, 0, 0, 35u);
                }
            }
            if (this.AppWindowStyle == TopMost.appStyle.WindowedWithoutBorder)
            {
                TopMost.SetWindowLong(TopMost.GetForegroundWindow(), -16, 369164288);
                if (this.ScreenDepth == TopMost.zDepth.Normal)
                {
                    TopMost.SetWindowPos(TopMost.GetForegroundWindow(), this.HWND_NORMAL, (int)this.screenPosition.x, (int)this.screenPosition.y, (int)this.screenPosition.width, (int)this.screenPosition.height, 64u);
                }
                if (this.ScreenDepth == TopMost.zDepth.Top)
                {
                    TopMost.SetWindowPos(TopMost.GetForegroundWindow(), this.HWND_TOP, (int)this.screenPosition.x, (int)this.screenPosition.y, (int)this.screenPosition.width, (int)this.screenPosition.height, 64u);
                }
                if (this.ScreenDepth == TopMost.zDepth.TopMost)
                {
                    TopMost.SetWindowPos(TopMost.GetForegroundWindow(), this.HWND_TOPMOST, (int)this.screenPosition.x, (int)this.screenPosition.y, (int)this.screenPosition.width, (int)this.screenPosition.height, 64u);
                }
            }
        }
        this.i++;
    }
}
