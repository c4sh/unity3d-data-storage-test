using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CreateScene : MonoBehaviour {
	
	public Texture2D backgroundImg;
	public GUISkin skin;
	
	private string errorMsg = "";
	private string name = "";
	private string director = "";
	private string duration = "";
	private string music = "";
	
	void OnGUI () {
		
		GUI.skin = skin;

		// Make a text field that modifies stringToEdit.
		GUI.DrawTexture(UtilResize.ResizeGUI(new Rect(0,0,320,480)),backgroundImg);
		
		GUI.Label(UtilResize.ResizeGUI(new Rect(80,10,220,20)),"Name*","LabelBold");
		name = GUI.TextField (UtilResize.ResizeGUI(new Rect (80, 30, 220, 40)), name, 100);
		
		GUI.Label(UtilResize.ResizeGUI(new Rect(80,75,220,20)),"Director*","LabelBold");
		director = GUI.TextField (UtilResize.ResizeGUI(new Rect (80, 100, 220, 40)), director, 100);
		
		GUI.Label(UtilResize.ResizeGUI(new Rect(80,145,200,20)),"Duration*","LabelBold");
		duration = GUI.TextField (UtilResize.ResizeGUI(new Rect (80, 170, 220, 40)), duration, 100);
		
		GUI.Label(UtilResize.ResizeGUI(new Rect(80,215,200,20)),"Music*","LabelBold");
		music = GUI.TextField (UtilResize.ResizeGUI(new Rect (80, 240, 220, 40)), music, 25);
		
		
		if (GUI.Button (UtilResize.ResizeGUI(new Rect (80,290, 220, 50)), "Create")) {
			
			if ((name != "")
			    && (director != "")
			    && (director != "")
			    && (duration != "")
			    && (music !=  "")) {

				createEntity();
				
			}else {
				errorMsg = "Fill all the fields with (*) correctly";
				Debug.Log(errorMsg);				
			}
		}
		
		if (GUI.Button (UtilResize.ResizeGUI(new Rect (80,345, 220, 50)), "Cancel")) {			
			Application.LoadLevel ("DataScene");		
		}
		
		if (errorMsg != "") {
			GUI.Box (new Rect ((Screen.width - (UtilResize.resMultiplier() * 260)),(Screen.height - (UtilResize.resMultiplier() * 50)),(UtilResize.resMultiplier() * 260),(UtilResize.resMultiplier() * 50)), errorMsg);
			if(GUI.Button(new Rect (Screen.width - 20,Screen.height - UtilResize.resMultiplier() * 45,16,16), "x","ButtonSmall")) {
				errorMsg = "";
			}
		}
		
	}

	void createEntity() {
		//printToConsole("Creating Entity...");
		
		// Store all the information you want inside a dictionary
		Dictionary<string,object> movie = new Dictionary<string,object>();
		movie ["name"] = name;
		movie["director"] = director;
		movie["duration"] = duration;
		movie["music"] = music;
		
		// Make the request to store the entity inside the desired collection
		GamedoniaData.Create("movies", movie, delegate (bool success, IDictionary data){
			if (success){
				//TODO Your success processing
				Application.LoadLevel ("DataScene");
			}
			else{
				//TODO Your success processing
				//printToConsole("Failed to create entity.");
				errorMsg = Gamedonia.getLastError().ToString();
				Debug.Log(errorMsg);
				
			}
		});
	}
	
}
