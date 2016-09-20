using UnityEngine;
using System.Collections;
using System.IO;
/**
 * A simple script to take screenshots whitin the game. The Screenshot contains only the actual game screen without any parts of the OS Gui or the Unity Editor. It is not part of the game but a tool for the developers that allows easier documentation.
 */ 
public class ScreenshotTool : MonoBehaviour {

    private int count = 0;
    private string dir = "Screenshots";

    /**
    * Creates the Folder if it doesn't exist and  get the number for the next screenshot.
    */ 
    void Start () {
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        DirectoryInfo dirinf = new DirectoryInfo(dir);
        FileInfo[] info = dirinf.GetFiles("Screenshot*");
        foreach (FileInfo f in info)
        {
            string[] substrings = f.Name.Split('.');
            int filenumber = int.Parse(substrings[0].Substring(10));
            if (filenumber >= count) count = filenumber + 1;
        }
    }

    /**
    * Takes a Screenshot if 'X' is released and increases the count by 1.
    */
    void Update () {
        if (Input.GetKeyUp(KeyCode.X))       
            Application.CaptureScreenshot("Screenshots/Screenshot" + count++ + ".png");
        }
}