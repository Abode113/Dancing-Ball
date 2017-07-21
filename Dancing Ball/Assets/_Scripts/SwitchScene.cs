using UnityEngine;
using System.Collections;
// for browse your files
using UnityEditor;
// for change the scene
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

	public static string filePath="";
	private string extension="";
	private AudioSource myAudio;
	public void ChangeToScene(int SceneIndex){
		//open windows explorer / android explorer
		// openfilePanel ( the title of browse window , the default path of searching , the allowed extension
		if (SceneIndex == 1) {
			filePath = EditorUtility.OpenFilePanel ("Select an Audio File .."
			, Application.streamingAssetsPath
			, "");
			// get the file extension to check it 
			extension = filePath.Substring (filePath.LastIndexOf ('.') + 1);
			// check the extension 
			if (extension == "mp3" || extension == "wav" || extension == "ogg") {
				SceneManager.LoadScene (SceneIndex);
			} else {
				EditorUtility.DisplayDialog ("Error", "Incorrect File Type Please Select another", "OK");
			}
		} else {
			SceneManager.LoadScene (SceneIndex);
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
