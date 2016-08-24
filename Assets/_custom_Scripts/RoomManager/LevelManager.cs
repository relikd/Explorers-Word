﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text;
using System.IO;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	static LevelManager Instance;
	string text = "";
	public List<string> Paragraphs = new List<string>();
	string Path = "";
	string CurrentStoryChapterName="StoryChapter0"; 

	void Start () {
//		if (Instance != null)
//			GameObject.Destroy (gameObject);
//		else {
//			GameObject.DontDestroyOnLoad (gameObject);
//			Instance = this;
//		}
		this.Path = "" + Application.dataPath + "/StoryChapters/";
		Paragraphs = GetSplitParagraphs (LoadParagraphs (this.Path + this.CurrentStoryChapterName + getCurrentLevelNumber()+ ".txt"));
	}

	void Update () {
		if (Input.GetKeyUp (KeyCode.Alpha1)) {
			SceneManager.LoadScene ("room_1");
		} else if (Input.GetKeyUp (KeyCode.Alpha2)) {
			SceneManager.LoadScene ("room_2");
		}
	}

	public void toggelExplorersBook() {
		ExplorersBook.OpenExplorersBook explorersBook = GameObject.Find ("FirstPersonCharacter").GetComponent<ExplorersBook.OpenExplorersBook> ();

		if (explorersBook != null) {
			if (explorersBook.isBookOpen()) {
				explorersBook.openExplorersBook ();		

			}
			explorersBook.enabled = !explorersBook.enabled;
		}

	}

	private List<string> GetSplitParagraphs(string text) {
		List<string> result = new List<string> ();
		result.AddRange(createLinesForParagraph (text));
		return result;
	}

	private List<string> createLinesForParagraph(string text) {
		List<string> Lines = new List<string>();
		int LineThreshhold = 48;
		string oldLine = ""; 
		string currentLine = "";

		foreach (char c in text.ToCharArray()) {
			if (c == ' ') {
				if (currentLine.Length >= LineThreshhold) {
					Lines.Add (oldLine + "\n");
					currentLine = "";
					oldLine = "" + oldLine.ToCharArray()[(oldLine.Length-1)];
				}
			} 
			if (c  == '#') {
				Lines.Add (oldLine);
			}
			currentLine += c;
			oldLine = currentLine;
		}
		return Lines;
	}

	static public void LoadNextRoom() {
//		MonoBehaviour[] allScripts = GameObject.FindObjectsOfType<MonoBehaviour> ();
//		for (int i = allScripts.Length; i > 0; i--) {
//			if (allScripts[i-1] != Instance) {
//				Destroy (allScripts[i-1]);
//			}
//		}
		string levelName = SceneManager.GetActiveScene ().name.Substring (5);
		int levelNumber = 0;
		int.TryParse (levelName, out levelNumber);
		levelNumber++;

		SceneManager.LoadScene ("room_"+levelNumber, LoadSceneMode.Single);
	}

	public List<string> getParagraphs() {
		return Paragraphs;
	}

	private int getCurrentLevelNumber() {
		string levelName = SceneManager.GetActiveScene ().name.Substring (5);
		int levelNumber = 0;
		int.TryParse (levelName, out levelNumber);
		return levelNumber;
	}

	private string LoadParagraphs(string fileName)
	{
		return text = File.ReadAllText (fileName);
	}
}