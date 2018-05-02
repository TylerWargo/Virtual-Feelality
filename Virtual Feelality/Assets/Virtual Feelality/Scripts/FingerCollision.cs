using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//(2018) Made by Tyler Wargo

public class FingerCollision : MonoBehaviour 
{
	//Acces Serial Script
	public SerialControl serialScript;
	//Colliding Finger GO
	public GameObject fingerGO;
	//Current Finger
	public string currentFinger;

	public void Start()
	{
		//Sets Current Finger Touching = Specific Finger GO Tag
		currentFinger = fingerGO.tag;
	}

	//Check for Collision / Touch
	public void OnCollisionEnter(Collision col)
	{
		//Check If Colliding With 'Touchable' Object
		if (col.gameObject.tag == "Touchable") 
		{
			//Thumb On Collision
			if (currentFinger == "Thumb Tip Right") 
			{
				//THUMB ON
				serialScript.serial.Write ("A");
				Debug.Log ("Thumb Touch");
			}
			//Index On Collision
			else if (currentFinger == "Index Tip Right") 
			{
				//INDEX ON
				serialScript.serial.Write ("C");
				Debug.Log ("Index Touch");
			}
			//Middle On Collision
			else if(currentFinger == "Middle Tip Right")
			{
				//MIDDLE ON
				serialScript.serial.Write ("E");
				Debug.Log ("Middle Touch");
			}
			//Ring On Collision
			else if(currentFinger == "Ring Tip Right")
			{
				//RING ON
				serialScript.serial.Write ("G");
				Debug.Log ("Ring Touch");
			}
			//Pinky On Collision
			else if(currentFinger == "Pinky Tip Right")
			{
				//PINKY ON
				serialScript.serial.Write ("I");
				Debug.Log ("Pinky Touch");
			}
		}
	}

	//Check for exiting Collision / Touch
	public void OnCollisionExit(Collision col)
	{
		//Check If NOT Colliding (Has Exited) With 'Touchable' Object
		if (col.gameObject.tag == "Touchable") 
		{
			//Thumb Off Collision
			if (currentFinger == "Thumb Tip Right") 
			{
				//THUMB OFF
				serialScript.serial.Write ("B");
			}
			//Index Off Collision
			else if (currentFinger == "Index Tip Right") 
			{
				//INDEX OFF
				serialScript.serial.Write ("D");
			}
			//Middle Off Collision
			else if (currentFinger == "Middle Tip Right") 
			{
				//MIDDLE OFF
				serialScript.serial.Write("F");
			}
			//Ring off Collision
			else if(currentFinger == "Ring Tip Right")
			{
				//RING OFF
				serialScript.serial.Write ("H");
			}
			//Pinky off Collision
			else if(currentFinger == "Pinky Tip Right")
			{
				//PINKY OFF
				serialScript.serial.Write ("J");
			}
		}
	}
}
