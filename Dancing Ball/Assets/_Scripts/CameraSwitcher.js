// script for switching between Cameras in your scene

var Cam1 : Camera;
var Cam2 : Camera;

function Start(){
	Cam1.enabled=true;

	Cam2.enabled=false;
}

function Update(){

	//if(Input.GetKeyUp("c")){
	//	Cam1.enabled=!Cam1.enabled;
	//	Cam2.enabled=!Cam2.enabled;
	//}
	if(Input.GetKeyDown("c")){
	Cam1.enabled=false;
	Cam2.enabled=true;
	}
	if(Input.GetKeyUp("c")){
	Cam1.enabled=true;
	Cam2.enabled=false;
	}

	}

