
//#define LOG

using UnityEngine;
using System.Collections;
using System;
using System.IO;

 
public static class LogWriter  {
    static string dir = @"Logs";
    private static string path = @"Logs/GameLog "+ DateTime.Now.ToString("yyyy-MM-dd")+" "+ DateTime.Now.ToString("HH-mm-ss") + ".txt";
    private static bool newgame = true;
    
    
    static LogWriter()
    {
        
    }

    public static void WriteLog(String line)
    // schreibt eine Zeile ins Logfile, erstellt falls nötig das File und das directory
    {

    #if (LOG)
        if (newgame)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(dir);
            }
            //Fügt eine Zeile mit der Startuhrzeit ein
            using (StreamWriter sw = File.CreateText(path)) sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + ":");
            newgame = false;
        }
        using (StreamWriter sw = File.AppendText(path))
        {
            sw.WriteLine(line);
        }
     #endif

    }
}
