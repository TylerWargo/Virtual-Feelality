using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

//(2018) Made by Tyler Wargo

public class SerialControl : MonoBehaviour 
{
	//Set Serial Port Data Stream
	public SerialPort serial = new SerialPort("COM7", 115200);

	public void Update()
	{
		//Check If Serial Data Stream Is Closed
		if (!serial.IsOpen)
			//Open Data Stream If Closed
			serial.Open ();
	}
}