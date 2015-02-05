using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Globalization;
using System.Text;
using System;
using LitJson_Gamedonia;


public class DataScene : MonoBehaviour {

	public Texture2D backgroundImg;
	public GUISkin skin;
	

	private string errorMsg = "";
	private string statusMsg = "";
	private string console = "";

	void Awake() {

	}

	void Start() {

		GamedoniaUsers.Authenticate(OnLogin);
		printToConsole ("Starting session with Gamedonia...");

	}

	void OnGUI () {
		
		GUI.skin = skin;

		GUI.DrawTexture(UtilResize.ResizeGUI(new Rect(0,0,320,480)),backgroundImg);

		GUI.enabled = (statusMsg == "");

		//Text area control
		GUI.Label(UtilResize.ResizeGUI(new Rect(80,10,220,20)),"Console Log:","LabelBold");
		GUI.Box (UtilResize.ResizeGUI (new Rect (80, 30, 220, 380)), console);


		//Create an entity
		if (GUI.Button (UtilResize.ResizeGUI(new Rect (80,420, 60, 50)), "Create an Entity")) {
			createEntity();
		}


		//Show all entities
		if (GUI.Button (UtilResize.ResizeGUI(new Rect (160,420, 60, 50)), "Show all entities")) {
			showAllEntities();
		}


		//Remove an entity
		if (GUI.Button (UtilResize.ResizeGUI(new Rect (240,420, 60, 50)), "Remove entity")) {
			removeEntity();
		}


		/*
		//Server Push
		if (GUI.Button (UtilResize.ResizeGUI(new Rect (80,420, 220, 50)), "Generate Push With Server Code")) {
			generatePushWithServerCode();
		}

		if (errorMsg != "") {
			GUI.Box (new Rect ((Screen.width - (UtilResize.resMultiplier() * 260)),(Screen.height - (UtilResize.resMultiplier() * 50)),(UtilResize.resMultiplier() * 260),(UtilResize.resMultiplier() * 50)), errorMsg);
			if(GUI.Button(new Rect (Screen.width - 20,Screen.height - UtilResize.resMultiplier() * 45,16,16), "x","ButtonSmall")) {
				errorMsg = "";
			}
		}

		*/

		GUI.enabled = true;
		if (statusMsg != "") {
			GUI.Box (UtilResize.ResizeGUI(new Rect (80, 240 - 40, 220, 40)), statusMsg);
		}
	}


	void createEntity() {
		/*printToConsole("Creating Entity...");

		// Store all the information you want inside a dictionary
		Dictionary<string,object> movie = new Dictionary<string,object>();
		movie["name"] = "The Godfather";
		movie["director"] = "Francis Ford Coppola";
		movie["duration"] = "178 minutos";
		movie["music"] = "Nino Rota";
		
		// Make the request to store the entity inside the desired collection
		GamedoniaData.Create("movies", movie, delegate (bool success, IDictionary data){
			if (success){
				//TODO Your success processing
				printToConsole("Entity created successfully with id: "+data["_id"]);
			}
			else{
				//TODO Your success processing
				printToConsole("Failed to create entity.");
				errorMsg = Gamedonia.getLastError().ToString();
				Debug.Log(errorMsg);

			}
		});*/
		Application.LoadLevel("CreateScene");
	}

	void showAllEntities() {

		printToConsole ("Showing all entities...\n");
		GamedoniaData.Search("movies", "{}", delegate (bool success, IList list){
			if (success){
				//TODO Your success processing
				if(list != null){
					foreach (Dictionary<string,object> elem in list)
					{
						printToConsole("Name: "+elem["name"]+"\n"+"Director: "+elem["director"]+"\n"+"Duration: "+elem["duration"]+"\n"+"Music: "+elem["music"]+"\n");
					}
				}
				else{
						printToConsole("No movies found.");
				}
			}
			else {
				//TODO Your fail processing
				printToConsole("The collection doesn't exist. Create one in the Dashboard.");
			}
		});
	}

	void removeEntity() {
		GamedoniaData.Delete("movies", "54d39884e4b0b94e5d240e3f", delegate (bool success){
			if(success) {
				printToConsole("Movie deleted successfully.");
			}
			else {
				printToConsole("Failed to delete the movie.");
			}

		});
	}
	

	private void printToConsole(string msg) {
		console += msg + "\n";
	}

	void OnLogin (bool success) {

		statusMsg = "";
		if (success) {
			printToConsole("Session started successfully. uid: " + GamedoniaUsers.me._id);
		}else {
			errorMsg = Gamedonia.getLastError().ToString();
			Debug.Log(errorMsg);
		}

	}
}
