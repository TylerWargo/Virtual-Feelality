int serialData;

//(2018) Made by Tyler Wargo

void setup()
{
  Serial.begin(115200);
  //Thumb
  pinMode(2, OUTPUT);
  //Index
  pinMode(3, OUTPUT);
  //Middle
  pinMode(4, OUTPUT);
  //Ring
  pinMode(5, OUTPUT);
  //Pinky
  pinMode(6, OUTPUT);
  //LED
  pinMode(13, OUTPUT);
}

void loop()
{
  //Check for Serial Data
  if(Serial.available())
  {
    //Set Read Data to Serial Data
    serialData = Serial.read();

    //THUMB
      //ON
    if(serialData == 'A')
    {
      digitalWrite(2, HIGH);
      digitalWrite(13, HIGH);
    }
      //OFF
    else if(serialData == 'B')
    {
      digitalWrite(2, LOW);
      digitalWrite(13, LOW);
    }

    //INDEX
      //ON
    if(serialData == 'C')
    {
      digitalWrite(3, HIGH);
      digitalWrite(13, HIGH);
    }
      //OFF
    else if(serialData == 'D')
    {
      digitalWrite(3, LOW);
      digitalWrite(13, LOW);
    }

    //MIDDLE
      //ON
    if(serialData == 'E')
    {
      digitalWrite(4, HIGH);
      digitalWrite(13, HIGH);
    }
      //OFF
    else if(serialData == 'F')
    {
      digitalWrite(4, LOW);
      digitalWrite(13, LOW);
    }

    //RING
      //ON
    if(serialData == 'G')
    {
      digitalWrite(5, HIGH);
      digitalWrite(13, HIGH);
    }
      //OFF
    else if(serialData == 'H')
    {
      digitalWrite(5, LOW);
      digitalWrite(13, LOW);
    }

    //PINKY
      //ON
    if(serialData == 'I')
    {
      digitalWrite(6, HIGH);
      digitalWrite(13, HIGH);
    }
      //OFF
    else if(serialData == 'J')
    {
      digitalWrite(6, LOW);
      digitalWrite(13, LOW);
    }
  }
}
