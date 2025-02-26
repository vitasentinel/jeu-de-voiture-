﻿//Description : MainMenuEditor.cs : Create and setup buttons to load Scene game in assoction with MainMenu.cs
//Find this script on gameObject ScrollMenu_Manager on scene 
#if (UNITY_EDITOR)
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UI;

[CustomEditor(typeof(MainMenu))]
public class MainMenuEditor : Editor {
	public bool 			SeeInspector 		= false;		// use to draw default Inspector

	SerializedProperty		CheckModification;		

	SerializedProperty 		TableList;
	SerializedProperty 		nameList;
	SerializedProperty 		showParts; 
	public bool 			b_CreateNewButton 	= false;
	SerializedProperty		showPlayerPrefsInfos;				// Used to show PlayerPrefs Infos on Inspector
	public bool 			b_PlayerPrefs		= false;

	SerializedProperty 		UnlockOrLock;
	//SerializedProperty 		TableSpriteList;
	SerializedProperty 		TableBackgroundSpriteList;
	SerializedProperty 		TableVector2SpriteList;

	SerializedProperty 		ButtonLock;							
	SerializedProperty 		S_LastScene;	
	SerializedProperty 		S_CurrentScene;	
	SerializedProperty 		S_NextScene;	
	SerializedProperty 		S_CurrentBackground;
	SerializedProperty 		S_TableTitle;



	SerializedProperty 		LeadName;
	//SerializedProperty 		LeadScore;
	SerializedProperty 		ShowLeaderboard;

	private Texture2D MakeTex(int width, int height, Color col) {	// use to change the GUIStyle
		Color[] pix = new Color[width * height];
		for (int i = 0; i < pix.Length; ++i) {
			pix[i] = col;
		}
		Texture2D result = new Texture2D(width, height);
		result.SetPixels(pix);
		result.Apply();
		return result;
	}

	void OnEnable () {
		// Setup the SerializedProperties.
		CheckModification	= serializedObject.FindProperty ("CheckModification");
		showParts 			= serializedObject.FindProperty ("ShowParts");
		TableList			= serializedObject.FindProperty ("TableName");
		nameList 			= serializedObject.FindProperty ("SceneName");
		showPlayerPrefsInfos = serializedObject.FindProperty ("ShowPlayerPrefsInfos");
		UnlockOrLock 		= serializedObject.FindProperty ("UnlockOrLock");
		//TableSpriteList 	= serializedObject.FindProperty ("TableSpriteList");
		TableBackgroundSpriteList 	= serializedObject.FindProperty ("TableBackgroundSpriteList");
		TableVector2SpriteList 	= serializedObject.FindProperty ("TableVector2SpriteList");
		ButtonLock 			= serializedObject.FindProperty ("ButtonLock");
		S_LastScene 		= serializedObject.FindProperty ("S_LastScene");	
		S_CurrentScene 		= serializedObject.FindProperty ("S_CurrentScene");	
		S_NextScene 		= serializedObject.FindProperty ("S_NextScene");
		S_CurrentBackground = serializedObject.FindProperty ("S_CurrentBackground");
		S_TableTitle		= serializedObject.FindProperty ("TextTableName");

		LeadName  			= serializedObject.FindProperty ("LeadName");
		//LeadScore 			= serializedObject.FindProperty ("LeadScore");
		ShowLeaderboard 	= serializedObject.FindProperty ("ShowLeaderboard");

		CheckModification.intValue	= 0;
	}

	public override void OnInspectorGUI()
	{
// --> Draw Default Inspector 		
		SeeInspector = EditorGUILayout.Foldout(SeeInspector,"Inspector");

		if(SeeInspector)																		// If true Default Inspector is drawn on screen
			DrawDefaultInspector();

		MainMenu myScript = (MainMenu)target; 													// Access script
		serializedObject.Update ();																// Update serialized object
		GUIStyle style = new GUIStyle(GUI.skin.box);


// --> Section 1 - Setup buttons parameters
		style.normal.background = MakeTex(2, 2, new Color(1,.2f,0,.3f));							// Choose a new color for the next section
		GUILayout.Label("");
		EditorGUILayout.BeginVertical(style);
		EditorGUILayout.HelpBox("1 - Setup your buttons here."	,MessageType.Info);
			EditorGUILayout.LabelField("Scene list",EditorStyles.boldLabel);						// Display a text on the Inspector
			EditorGUILayout.LabelField("");
			if(myScript.SceneName.Count>0){															// Display buttons info on Inspector									
				for (int i = 0;i< myScript.SceneName.Count;i++){
					style.normal.background = MakeTex(2, 2, new Color(1,.2f,0,.3f));							// Choose a new color for the next section
					EditorGUILayout.BeginVertical(style);
						EditorGUILayout.BeginHorizontal();
							EditorGUILayout.LabelField("Scene number " + i.ToString() 
							+ " : ",EditorStyles.boldLabel,GUILayout.Width(100));
				
							if(GUILayout.Button("-",GUILayout.Width(20)))							// Display a button that delete a table information
							{
								Undo.RegisterFullObjectHierarchyUndo(target,"MainMenu");			// Save info for undo
								myScript.TableName.RemoveAt(i);
								myScript.TableBackgroundSpriteList.RemoveAt(i);
								myScript.TableVector2SpriteList.RemoveAt(i);
								myScript.SceneName.RemoveAt(i);
								myScript.UnlockOrLock.RemoveAt(i);
								myScript.TableSpriteList.RemoveAt(i);
								myScript.LeadName.RemoveAt(i);
								myScript.ShowLeaderboard.RemoveAt(i);


								if (myScript.TableSpriteList.Count == 1) {							// If only one button deactivate the buttons that allow to selected multiple table
									//Debug.Log ("Only One Table");
									GameObject tmp_PlayButton = GameObject.Find ("Button_Play");
									if (tmp_PlayButton) {
										Undo.RegisterFullObjectHierarchyUndo (tmp_PlayButton, "tmp_PlayButton");
										Navigation navigation = tmp_PlayButton.GetComponent<Button>().navigation;

										navigation.selectOnLeft = null;

										// reassign the struct data to the button
										tmp_PlayButton.GetComponent<Button>().navigation = navigation;
									}

									GameObject tmp_Button_Leaderboard = GameObject.Find ("Button_Leaderboard");
									if (tmp_Button_Leaderboard) {
										Undo.RegisterFullObjectHierarchyUndo (tmp_Button_Leaderboard, "tmp_Button_Leaderboard");
										Navigation navigation = tmp_Button_Leaderboard.GetComponent<Button>().navigation;

										navigation.selectOnLeft = null;

										// reassign the struct data to the button
										tmp_Button_Leaderboard.GetComponent<Button>().navigation = navigation;
									}

									GameObject tmpChooseTable = GameObject.Find ("Choose Table");
									if (tmpChooseTable) {
										Undo.RegisterFullObjectHierarchyUndo (tmpChooseTable, "tmpChooseTable");
										tmpChooseTable.SetActive (false);
									}

								}

								break;	
							}


						if(CheckModification.intValue == i) 
							style.normal.background = MakeTex(2, 2, Color.yellow);
						else
							style.normal.background = MakeTex(2, 2, new Color(1,.2f,0,.3f));

							EditorGUILayout.BeginVertical(style);
							if (GUILayout.Button ("See Modifications", GUILayout.Width (110))) {	// Display a button that delete a button
								CheckModification.intValue = i;
							}
							EditorGUILayout.EndVertical ();

						style.normal.background = MakeTex(2, 2, new Color(1,.2f,0,.3f));

						EditorGUILayout.EndHorizontal();


						EditorGUILayout.BeginHorizontal();
							EditorGUILayout.LabelField("Circuit Name : ",GUILayout.Width(130));
							EditorGUILayout.PropertyField(TableList.GetArrayElementAtIndex(i), new GUIContent (""),GUILayout.Width(150));
						EditorGUILayout.EndHorizontal();

						EditorGUILayout.BeginHorizontal();
							EditorGUILayout.LabelField("Scene Name : ",GUILayout.Width(130));
							EditorGUILayout.PropertyField(nameList.GetArrayElementAtIndex(i), new GUIContent (""),GUILayout.Width(150));
						EditorGUILayout.EndHorizontal();

						EditorGUILayout.BeginHorizontal();
							EditorGUILayout.LabelField("Unlock : ",GUILayout.Width(130));
							EditorGUILayout.PropertyField(UnlockOrLock.GetArrayElementAtIndex(i), new GUIContent (""),GUILayout.Width(30));
							if (GUILayout.Button ("Reset PlayerPrefs", GUILayout.Width (110))) {										
								PlayerPrefs.DeleteKey(nameList.GetArrayElementAtIndex(i).stringValue + "_Lock");
							}
						EditorGUILayout.EndHorizontal();

						/*EditorGUILayout.BeginHorizontal();
							EditorGUILayout.LabelField("Logo (Sprite) : ",GUILayout.Width(130));
							EditorGUILayout.PropertyField(TableSpriteList.GetArrayElementAtIndex(i), new GUIContent (""),GUILayout.Width(100));
						EditorGUILayout.EndHorizontal();

						EditorGUILayout.LabelField("");*/

						EditorGUILayout.BeginHorizontal();
							EditorGUILayout.LabelField("Background (Sprite) : ",GUILayout.Width(130));
							EditorGUILayout.PropertyField(TableBackgroundSpriteList.GetArrayElementAtIndex(i), new GUIContent (""),GUILayout.Width(100));
						EditorGUILayout.EndHorizontal();

						EditorGUILayout.BeginHorizontal();
							EditorGUILayout.LabelField("Background Scale : ",GUILayout.Width(130));
							EditorGUILayout.PropertyField(TableVector2SpriteList.GetArrayElementAtIndex(i), new GUIContent (""),GUILayout.Width(100));
						EditorGUILayout.EndHorizontal();




				EditorGUILayout.LabelField("");
				style.normal.background = MakeTex(2, 2, new Color(0,.2f,0F,.1f));							// Choose a new color for the next section
				EditorGUILayout.BeginVertical(style);
					EditorGUILayout.LabelField("Default Leaderboard : ",GUILayout.Width(120));
				EditorGUILayout.PropertyField (ShowLeaderboard.GetArrayElementAtIndex (i), new GUIContent (""), GUILayout.Width (30));


				if (ShowLeaderboard.GetArrayElementAtIndex (i).boolValue) {

					EditorGUILayout.LabelField ("");

					EditorGUILayout.BeginHorizontal ();
					if (GUILayout.Button ("+", GUILayout.Width (30))) {										// --> Add a new name and score
						Undo.RegisterFullObjectHierarchyUndo (myScript, "Myscript");
						myScript.LeadName [i].Names.Add ("JOHN");
						int tmpRandom = Random.Range (10000, 300000);
						tmpRandom *= 10;
						myScript.LeadName [i].Score.Add (tmpRandom);
						myScript.LeadName [i].Minutes.Add ("00");
						myScript.LeadName [i].Seconds.Add ("00");
						myScript.LeadName [i].Milli.Add ("000");
						break;
					}

					EditorGUILayout.LabelField ("(Add a new name and score)", GUILayout.Width (160));
					EditorGUILayout.EndHorizontal ();
					 

					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField ("Name :", GUILayout.Width (70));
					EditorGUILayout.LabelField ("", GUILayout.Width (35));
					GUILayout.Label("M", GUILayout.Width (20));
					GUILayout.Label("S", GUILayout.Width (20));
					GUILayout.Label("Ms", GUILayout.Width (30));
					EditorGUILayout.EndHorizontal ();

					for (int j = 0; j < myScript.LeadName [i].Names.Count; j++) {
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.PropertyField (LeadName.GetArrayElementAtIndex (i).FindPropertyRelative ("Names").GetArrayElementAtIndex (j), new GUIContent (""), GUILayout.Width (70));
						EditorGUILayout.LabelField ("Time :", GUILayout.Width (35));
						//EditorGUILayout.PropertyField (LeadName.GetArrayElementAtIndex (i).FindPropertyRelative ("Score").GetArrayElementAtIndex (j), new GUIContent (""), GUILayout.Width (70));
						EditorGUILayout.PropertyField (LeadName.GetArrayElementAtIndex (i).FindPropertyRelative ("Minutes").GetArrayElementAtIndex (j), new GUIContent (""), GUILayout.Width (20));
						EditorGUILayout.PropertyField (LeadName.GetArrayElementAtIndex (i).FindPropertyRelative ("Seconds").GetArrayElementAtIndex (j), new GUIContent (""), GUILayout.Width (20));
						EditorGUILayout.PropertyField (LeadName.GetArrayElementAtIndex (i).FindPropertyRelative ("Milli").GetArrayElementAtIndex (j), new GUIContent (""), GUILayout.Width (30));

						if(LeadName.GetArrayElementAtIndex (i).FindPropertyRelative ("Minutes").GetArrayElementAtIndex (j).stringValue.Length<2
							|| LeadName.GetArrayElementAtIndex (i).FindPropertyRelative ("Seconds").GetArrayElementAtIndex (j).stringValue.Length<2
							|| LeadName.GetArrayElementAtIndex (i).FindPropertyRelative ("Milli").GetArrayElementAtIndex (j).stringValue.Length<3){
							GUILayout.Label("Wrong Format", GUILayout.Width (100));
						}
						else{
							if (GUILayout.Button ("Update", GUILayout.Width (50))) {		
								LeadName.GetArrayElementAtIndex (i).FindPropertyRelative ("Score").GetArrayElementAtIndex (j).intValue = 
									(int.Parse(LeadName.GetArrayElementAtIndex (i).FindPropertyRelative ("Minutes").GetArrayElementAtIndex (j).stringValue) * 1000*60) 
									+ (int.Parse(LeadName.GetArrayElementAtIndex (i).FindPropertyRelative ("Seconds").GetArrayElementAtIndex (j).stringValue)*1000) 
									+ (int.Parse(LeadName.GetArrayElementAtIndex (i).FindPropertyRelative ("Milli").GetArrayElementAtIndex (j).stringValue));
								PlayerPrefs.DeleteKey ("LastTableLoaded");
								//Debug.Log(
							}
						}


						EditorGUILayout.EndHorizontal ();
					}


					EditorGUILayout.BeginHorizontal ();
					if (GUILayout.Button ("-", GUILayout.Width (30))) {											// --> Delete last name and score
						Undo.RegisterFullObjectHierarchyUndo (myScript, "Myscript");
						myScript.LeadName [i].Names.RemoveAt (myScript.LeadName [i].Names.Count - 1);
						myScript.LeadName [i].Score.RemoveAt (myScript.LeadName [i].Score.Count - 1);
						myScript.LeadName [i].Minutes.RemoveAt (myScript.LeadName [i].Minutes.Count - 1);
						myScript.LeadName [i].Seconds.RemoveAt (myScript.LeadName [i].Seconds.Count - 1);
						myScript.LeadName [i].Milli.RemoveAt (myScript.LeadName [i].Milli.Count - 1);
						break;
					}
					EditorGUILayout.LabelField ("(Remove last name and score)", GUILayout.Width (160));
					EditorGUILayout.EndHorizontal ();


				}
		
				EditorGUILayout.EndVertical();







				if (CheckModification.intValue == i && !EditorApplication.isPlaying) {
					// Display Table Name
					if(myScript.TextTableName.text != TableList.GetArrayElementAtIndex(i).stringValue)
					myScript.TextTableName.text = TableList.GetArrayElementAtIndex(i).stringValue;

                    // Display Table Logo
                    //if(myScript.S_CurrentScene.GetComponent<Image>().sprite != (Sprite)TableSpriteList.GetArrayElementAtIndex (i).objectReferenceValue)
                    //myScript.S_CurrentScene.GetComponent<Image>().sprite = (Sprite)TableSpriteList.GetArrayElementAtIndex (i).objectReferenceValue;

                    // Display Backgroud image



                    // if(S_CurrentBackground.objectReferenceValue)
                    displayTracks(myScript,i);
                    //if(myScript.S_CurrentBackground.GetComponent<Image>().sprite != (Sprite)TableBackgroundSpriteList.GetArrayElementAtIndex (i).objectReferenceValue)
                    //	myScript.S_CurrentBackground.GetComponent<Image>().sprite = (Sprite)TableBackgroundSpriteList.GetArrayElementAtIndex (i).objectReferenceValue;

                    // Change background image scale
                    if (myScript.TableBackgroundSpriteList [i]) {
						/*myScript.S_CurrentBackground.GetComponent<RectTransform> ().sizeDelta = new Vector2 (
							myScript.TableBackgroundSpriteList [i].rect.width
						, myScript.TableBackgroundSpriteList [i].rect.height);
                        */
						myScript.S_CurrentBackground.GetComponent<RectTransform> ().localScale
					= new Vector3 (myScript.TableVector2SpriteList [i].x,
							myScript.TableVector2SpriteList [i].y
									, 1);
					}
                }


						if(showPlayerPrefsInfos.boolValue){												// Draw PlayerPrefs name that are used to save score and if the Scene is unlock or not 
							EditorGUILayout.HelpBox("PlayerPrefs Infos : " + "\n"
								+ "Leaderboard : " + myScript.SceneName[i] + "_Lead" + "\n"
								+ "Lock  : " + myScript.SceneName[i] + "_Lock",MessageType.Info);
						}
					EditorGUILayout.EndVertical();
					EditorGUILayout.LabelField("");
				}

				EditorGUILayout.LabelField("");
				EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField("Show PlayerPrefs Infos :",EditorStyles.boldLabel,GUILayout.Width(180));
					EditorGUILayout.PropertyField(showPlayerPrefsInfos, new GUIContent (""),GUILayout.Width(30));
				EditorGUILayout.EndHorizontal();

			}
		EditorGUILayout.EndVertical();


// --> Section 2 - Add New Scene to the list
		GUILayout.Label("");
		style.normal.background = MakeTex(2, 2, new Color(1,0,0,.5f));								// Choose a new color for the next section
		EditorGUILayout.BeginVertical(style);
			EditorGUILayout.HelpBox("2 - Add a new button to scroll view.",MessageType.Info);
			EditorGUILayout.LabelField("Add a new Circuit",EditorStyles.boldLabel);

		    bool bFirstPos = true;
		    EditorGUILayout.BeginHorizontal();
			/*if (GUILayout.Button("Add New Circuit")){													// Add a button to create a New Scene
				b_CreateNewButton = true;
			}*/
		    if (GUILayout.Button("Add New Circuit (First)"))
		    {                                                   // Add a button to create a New Scene
			    b_CreateNewButton = true;
		    }
		    if (GUILayout.Button("Add New Circuit (Last)"))
		    {                                                   // Add a button to create a New Scene
			    b_CreateNewButton = true;
			    bFirstPos = false;

			}
		    EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();

		if(b_CreateNewButton)																		// --> Create a new Button
		{
			Undo.RegisterFullObjectHierarchyUndo(target,"MainMenu");

            //-> Add Track first position on the list
            if (bFirstPos)
            {
				myScript.TableName.Insert(0, "Table name");
				myScript.SceneName.Insert(0, "Scene name");
				myScript.UnlockOrLock.Insert(0, true);
				myScript.TableSpriteList.Insert(0, null);
				myScript.TableBackgroundSpriteList.Insert(0, null);
				myScript.TableVector2SpriteList.Insert(0, new Vector2(.35F, .35F));
				myScript.LeadName.Insert(0, new MainMenu._LeadName());
				myScript.ShowLeaderboard.Insert(0, true);
			}
			//-> Add Track last position on the list
			else
			{
				SerializedObject serializedObject2 = new UnityEditor.SerializedObject(myScript);
				serializedObject2.Update();

				SerializedProperty _TableName = serializedObject.FindProperty("TableName");
				SerializedProperty _SceneName = serializedObject.FindProperty("SceneName");
				SerializedProperty _UnlockOrLock = serializedObject.FindProperty("UnlockOrLock");
				SerializedProperty _TableSpriteList = serializedObject.FindProperty("TableSpriteList");
				SerializedProperty _TableBackgroundSpriteList = serializedObject.FindProperty("TableBackgroundSpriteList");
				SerializedProperty _TableVector2SpriteList = serializedObject.FindProperty("TableVector2SpriteList");
				SerializedProperty _LeadName = serializedObject.FindProperty("LeadName");
				SerializedProperty _ShowLeaderboard = serializedObject.FindProperty("ShowLeaderboard");

				_TableName.InsertArrayElementAtIndex(0);
				_SceneName.InsertArrayElementAtIndex(0);
				_UnlockOrLock.InsertArrayElementAtIndex(0);
				_TableSpriteList.InsertArrayElementAtIndex(0);
				_TableBackgroundSpriteList.InsertArrayElementAtIndex(0);
				_TableVector2SpriteList.InsertArrayElementAtIndex(0);
				_LeadName.InsertArrayElementAtIndex(0);
				_ShowLeaderboard.InsertArrayElementAtIndex(0);

				_TableName.GetArrayElementAtIndex(0).stringValue = "Table name";
				_SceneName.GetArrayElementAtIndex(0).stringValue = "Scene name";
				_UnlockOrLock.GetArrayElementAtIndex(0).boolValue = true;
				_TableSpriteList.GetArrayElementAtIndex(0).objectReferenceValue = (Sprite)null;
				_TableBackgroundSpriteList.GetArrayElementAtIndex(0).objectReferenceValue = (Sprite)null;
				_TableVector2SpriteList.GetArrayElementAtIndex(0).vector2Value = new Vector2(.35F, .35F);
				//_LeadName.GetArrayElementAtIndex(0).ClearArray();
				_ShowLeaderboard.GetArrayElementAtIndex(0).boolValue = true;


				_TableName.MoveArrayElement(0, _TableName.arraySize - 1);
				_SceneName.MoveArrayElement(0, _TableName.arraySize - 1);
				_UnlockOrLock.MoveArrayElement(0, _TableName.arraySize - 1);
				_TableSpriteList.MoveArrayElement(0, _TableName.arraySize - 1);
				_TableBackgroundSpriteList.MoveArrayElement(0, _TableName.arraySize - 1);
				_TableVector2SpriteList.MoveArrayElement(0, _TableName.arraySize - 1);
				_LeadName.MoveArrayElement(0, _TableName.arraySize - 1);
				_ShowLeaderboard.MoveArrayElement(0, _TableName.arraySize - 1);


				serializedObject2.ApplyModifiedProperties();
			}
            
			


			b_CreateNewButton = false;

			if (myScript.TableSpriteList.Count > 1) {												// If there are more than one cicuit : activate the buttons that allow to selected multiple table
				GameObject tmpCanvasMenu = GameObject.Find ("Canvas_MainMenu");

				Component[] tmpChildrebnCanvasMenu = tmpCanvasMenu.GetComponentsInChildren(typeof(Transform), true);
				foreach(Transform t in tmpChildrebnCanvasMenu){
					if(t.name == "Choose Table"){
						Undo.RegisterFullObjectHierarchyUndo (t, "tmpChooseTable");
						t.gameObject.SetActive (true);
						break;
					}

				}
					

				GameObject tmp_PlayButton = GameObject.Find ("Button_Play");
				if (tmp_PlayButton) {
					Undo.RegisterFullObjectHierarchyUndo (tmp_PlayButton, "tmp_PlayButton");
					Navigation navigation = tmp_PlayButton.GetComponent<Button>().navigation;

					GameObject Next_Table = GameObject.Find ("Next_Table");

					navigation.selectOnLeft = Next_Table.GetComponent<Button>();

					// reassign the struct data to the button
					tmp_PlayButton.GetComponent<Button>().navigation = navigation;
				}

				GameObject tmp_Button_Leaderboard = GameObject.Find ("Button_Leaderboard");
				if (tmp_Button_Leaderboard) {
					Undo.RegisterFullObjectHierarchyUndo (tmp_Button_Leaderboard, "tmp_Button_Leaderboard");
					Navigation navigation = tmp_Button_Leaderboard.GetComponent<Button>().navigation;

					GameObject Next_Table = GameObject.Find ("Next_Table");
					navigation.selectOnLeft = Next_Table.GetComponent<Button>();

					// reassign the struct data to the button
					tmp_Button_Leaderboard.GetComponent<Button>().navigation = navigation;
				}

			}
		}
		GUILayout.Label("");


// --> Section 3 - Other variables
		EditorGUILayout.BeginVertical();
		bool tmpBool = showParts.boolValue;
		tmpBool = EditorGUILayout.Toggle("Other Variables",tmpBool);
		if(tmpBool){
			
			if(ButtonLock.objectReferenceValue != null )EditorGUILayout.PropertyField (ButtonLock, new GUIContent ("Lock GameObject"));		
			if(S_LastScene.objectReferenceValue != null )EditorGUILayout.PropertyField (S_LastScene, new GUIContent ("Last Scene"));	
			if(S_CurrentScene.objectReferenceValue != null )EditorGUILayout.PropertyField (S_CurrentScene, new GUIContent ("Current Scene"));	
			if(S_NextScene.objectReferenceValue != null )EditorGUILayout.PropertyField (S_NextScene, new GUIContent ("Next Scene"));	
			if(S_CurrentBackground.objectReferenceValue != null )EditorGUILayout.PropertyField (S_CurrentBackground, new GUIContent ("Current Background"));	
			if(S_TableTitle.objectReferenceValue != null )EditorGUILayout.PropertyField (S_TableTitle, new GUIContent ("Txt Table Title"));	

		}

		myScript.ShowParts = tmpBool;


       


        EditorGUILayout.EndVertical();
		serializedObject.ApplyModifiedProperties ();										// Apply modification on serialized objects 


	}


    void displayTracks(MainMenu myScript,int i)
    {
        // inventoryOnlineTracks

        //EditorGUILayout.PropertyField(inventoryOnlineTracks, new GUIContent(""));

        SerializedObject serializedObject0 = new UnityEditor.SerializedObject(myScript.S_CurrentBackground.GetComponent<Image>());
        serializedObject0.Update();

        SerializedProperty m_Scale = serializedObject0.FindProperty("m_Sprite");

        if (m_Scale.objectReferenceValue != (Sprite)TableBackgroundSpriteList.GetArrayElementAtIndex(i).objectReferenceValue)
        {
            m_Scale.objectReferenceValue = null;
           m_Scale.objectReferenceValue = (Sprite)TableBackgroundSpriteList.GetArrayElementAtIndex(i).objectReferenceValue;
        }
       

       // if (m_Scale.objectReferenceValue != (Sprite)TableBackgroundSpriteList.GetArrayElementAtIndex(i).objectReferenceValue)
         //   m_Scale.objectReferenceValue = (Sprite)TableBackgroundSpriteList.GetArrayElementAtIndex(i).objectReferenceValue;


        serializedObject0.ApplyModifiedProperties();
    }
}
#endif