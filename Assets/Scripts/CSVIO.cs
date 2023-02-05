using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVIO
{
    public static string[,] Read(string path)
    {
        string[,] data;

        Debug.Log("パス > " + Application.dataPath + path);

        StreamReader sr = new StreamReader(Application.dataPath + path, System.Text.Encoding.GetEncoding("shift_jis"));
        string strStream = sr.ReadToEnd();

        System.StringSplitOptions option = System.StringSplitOptions.RemoveEmptyEntries;

        string[] lines = strStream.Split(new char[] { '\r', '\n' }, option);

        char[] spliter = new char[1] { ',' };

        int h = lines.Length;
        int wLength = lines[0].Split(spliter, option).Length;

        data = new string[h, wLength];

        for (int i = 0; i < h; i++)
        {
            string[] splitedData = lines[i].Split(spliter, option);

            for (int j = 0; j < wLength; j++)
            {
                data[i, j] = splitedData[j];
            }
        }

        return data;
    }


    public static string[] ReadIsIdToLine(string path, string key)
    {
        string[] dataArray;

        StreamReader sr = new StreamReader(Application.dataPath + path, System.Text.Encoding.GetEncoding("shift_jis"));
        string strStream = sr.ReadToEnd();

        System.StringSplitOptions option = System.StringSplitOptions.RemoveEmptyEntries;

        string[] lines = strStream.Split(new char[] { '\r', '\n' }, option);

        char[] spliter = new char[1] { ',' };

        int h_length = lines.Length;
        int w_length = lines[0].Split(spliter, option).Length;

        dataArray = new string[w_length];

        for (int i = 0; i < h_length; i++)
        {
            string[] splitedData = lines[i].Split(spliter, option);

            if (key == splitedData[0])
            {
                for (int j = 0; j < w_length; j++)
                {
                    dataArray[j] = splitedData[j];
                }
            }
        }

        return dataArray;
    }
}