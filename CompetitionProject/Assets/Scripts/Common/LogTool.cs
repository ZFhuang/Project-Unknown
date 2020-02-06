/// From: 输出Log到手机屏幕 - 云影的文章 - 知乎
///     https://zhuanlan.zhihu.com/p/34699914

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class ShowLogTool : MonoBehaviour
{
    public bool showFlag = true;
    public int logCount = 100;
    private string m_ShowLog = string.Empty;
    private Queue<string> logQueue = new Queue<string>();

    void Start()
    {
        Application.logMessageReceived += WriteUnityLog;
    }


    void WriteUnityLog(string log, string stackTrace, LogType type)
    {
        if (!showFlag) return;
        WriteInLogQueue(log);
    }

    void WriteInLogQueue(string log)
    {
        logQueue.Enqueue(log);
        while (logQueue.Count > logCount)
        {
            logQueue.Dequeue();
        }
        m_ShowLog = string.Empty;
        foreach (string onelog in logQueue)
        {
            m_ShowLog = m_ShowLog + "\r\n" + onelog;
        }
    }

    void OnGUI()
    {
        if (showFlag)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 20;
            style.normal.textColor = Color.red;

            //GUI.color = Color.red;
            GUI.Label(new Rect(0, 0, 1000, 1000), m_ShowLog, style);
        }
    }
}
