using UnityEngine;
using System.Collections;

public class Instantiate_Cube_by_Song : MonoBehaviour {
	public GameObject target;
	public GameObject _sampleCubePrefab;
	private float[] LastCube = { 0.0f, 0.0f, 0.0f, 0.0f };
	private int _LastTypeOfSongValue = 0;
	public int here = 40;
	private int i = 0;

	// Use this for initialization
	void Start () {
		when_Ball_Come_From_Left_To_Right_And_Face_A_Cube_First_Type_Of_Song ();
		//when_Ball_Come_From_Left_To_Right_And_Face_A_Cube_Second_Type_Of_Song ();
		//when_Ball_Come_From_Right_To_Left_And_Face_A_Cube_First_Type_Of_Song ();
		//when_Ball_Come_From_Right_To_Left_And_Face_A_Cube_Second_Type_Of_Song ();
	}

	void ChoseTypeOfCube(){
		if (AudioPeer._TypeOfSong == 0 && AudioPeer._TypeOfSong != _LastTypeOfSongValue) {
			_LastTypeOfSongValue = 0;
			if (LastCube [3] == 0.0f) {
				when_Ball_Come_From_Left_To_Right_And_Face_A_Cube_First_Type_Of_Song ();
			}
			if (LastCube [3] == 1.0f) {
				when_Ball_Come_From_Right_To_Left_And_Face_A_Cube_First_Type_Of_Song ();
			}
		}
		if (AudioPeer._TypeOfSong == 1 && AudioPeer._TypeOfSong != _LastTypeOfSongValue) {
			_LastTypeOfSongValue = 1;
			if (LastCube [3] == 0.0f) {
				when_Ball_Come_From_Left_To_Right_And_Face_A_Cube_First_Type_Of_Song ();
			}
			if (LastCube [3] == 1.0f) {
				when_Ball_Come_From_Right_To_Left_And_Face_A_Cube_First_Type_Of_Song ();
			}
		}
		if (AudioPeer._TypeOfSong == 2 && AudioPeer._TypeOfSong != _LastTypeOfSongValue) {
			_LastTypeOfSongValue = 2;
			if (LastCube [3] == 0.0f) {
				when_Ball_Come_From_Left_To_Right_And_Face_A_Cube_Second_Type_Of_Song ();
			}
			if (LastCube [3] == 1.0f) {
				when_Ball_Come_From_Right_To_Left_And_Face_A_Cube_Second_Type_Of_Song ();
			}
		}
		if (AudioPeer._TypeOfSong == 3 && AudioPeer._TypeOfSong != _LastTypeOfSongValue) {
			_LastTypeOfSongValue = 3;
			if (LastCube [3] == 0.0f) {
				when_Ball_Come_From_Left_To_Right_And_Face_A_Cube_Second_Type_Of_Song ();
			}
			if (LastCube [3] == 1.0f) {
				when_Ball_Come_From_Right_To_Left_And_Face_A_Cube_Second_Type_Of_Song ();
			}
		}
	}

	void when_Ball_Come_From_Left_To_Right_And_Face_A_Cube_First_Type_Of_Song(){
		GameObject testObj = (GameObject)Instantiate (_sampleCubePrefab);
		testObj.transform.position = new Vector3 (LastCube[0]-3.6f, LastCube[1], LastCube[2]-1.2f);
		//target.transform.position =new Vector3(LastCube[0]-2, LastCube[1], LastCube[2]-1);
		testObj.transform.localScale = new Vector3 (20, 20, 20);
		LastCube [0] -= 3.6f;
		LastCube [2] -= 1.2f;
		LastCube [3] = 1.0f;
	}

	void when_Ball_Come_From_Left_To_Right_And_Face_A_Cube_Second_Type_Of_Song(){
		GameObject testObj = (GameObject)Instantiate (_sampleCubePrefab);
		testObj.transform.position = new Vector3 (LastCube[0]-2.4f, LastCube[1], LastCube[2]);
	//	target.transform.position =new Vector3 (LastCube[0]-2, LastCube[1], LastCube[2]);
		testObj.transform.localScale = new Vector3 (20, 20, 20);
		LastCube [0] -= 2.4f;
		//LastCube [3] = 1;
	}

	void when_Ball_Come_From_Right_To_Left_And_Face_A_Cube_First_Type_Of_Song(){
		GameObject testObj = (GameObject)Instantiate (_sampleCubePrefab);
		testObj.transform.position = new Vector3 (LastCube[0]+1.2f, LastCube[1], LastCube[2]+3.6f);
	//	target.transform.position =new Vector3 (LastCube[0]+1, LastCube[1], LastCube[2]+3);
		testObj.transform.localScale = new Vector3 (20, 20, 20);
		LastCube [0] += 1.2f;
		LastCube [2] += 3.6f;
		LastCube [3] = 0.0f;
	}

	void when_Ball_Come_From_Right_To_Left_And_Face_A_Cube_Second_Type_Of_Song(){
		GameObject testObj = (GameObject)Instantiate (_sampleCubePrefab);
		testObj.transform.position = new Vector3 (LastCube[0], LastCube[1], LastCube[2]+2.4f);
	//	target.transform.position =new Vector3 (LastCube[0], LastCube[1], LastCube[2]+2);
		testObj.transform.localScale = new Vector3 (20, 20, 20);
		LastCube [2] += 2.4f;
		//LastCube [3] = 0;
	}
	
	// Update is called once per frame
	void Update () {
		i += 3;
		if (i > here) {
			ChoseTypeOfCube ();
			i = 0;
		}
	}
}
