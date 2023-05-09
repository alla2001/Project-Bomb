// include the libraries
#include <Wire.h>

// define the pins for joystick and rotary encoder
#define JOYSTICK_X A0
#define JOYSTICK_Y A1
#define CLK 2
#define DT 3
#define SW 4


int joystick_x = 0;
int joystick_y = 0;
const int buzzer = 8; 
int counter = 0;
int currentStateCLK;
int lastStateCLK;
String currentDir ="";

void setup() {

  Serial.begin(9600);
  pinMode(buzzer, OUTPUT);
  pinMode(CLK,INPUT);
	pinMode(DT,INPUT);
	pinMode(SW, INPUT_PULLUP);
}
bool value;
void loop() {
    
  if (Serial.available() > 0) 
  {
    char c = Serial.read();
      tone(buzzer,1000);
   

  }else{
    noTone(buzzer);
  }
  Serial.flush();
  joystick_x = analogRead(JOYSTICK_X);
  joystick_y = analogRead(JOYSTICK_Y);


 
 // Read the current state of CLK
	currentStateCLK = digitalRead(CLK);

	// If last and current state of CLK are different, then pulse occurred
	// React to only 1 state change to avoid double count
	if (currentStateCLK != lastStateCLK  && currentStateCLK == 1){

		// If the DT state is different than the CLK state then
		// the encoder is rotating CCW so decrement
		if (digitalRead(DT) != currentStateCLK) {
			counter --;
			currentDir ="CCW";
		} else {
			// Encoder is rotating CW so increment
			counter ++;
			currentDir ="CW";
		}


	}

	// Remember last CLK state
	lastStateCLK = currentStateCLK;

// Read the button state
	int btnState = digitalRead(SW);

	//If we detect LOW signal, button is pressed
	

     Serial.print(joystick_x-506);
     Serial.print(",");
     Serial.print(joystick_y-519);
     Serial.print(",");
     Serial.print(counter);
     Serial.print(",");
     Serial.println(btnState);
   //wait for a short delay before sending the next values
  delay(50);
}