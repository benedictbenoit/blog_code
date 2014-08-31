int redValue = 0;
int blueValue = 0;
int greenValue = 0;

const int redPin = 11;
const int bluePin = 10;
const int greenPin = 9;

void setup(){
  pinMode(redPin, OUTPUT);
  pinMode(greenPin, OUTPUT);
  pinMode(bluePin, OUTPUT); 
}

void loop() {
  
  if (redValue + 2 <= 255) { redValue = redValue + 2;  } else {redValue = 0;}
  if (blueValue + 4 <= 255) { blueValue = blueValue + 4;  } else {blueValue = 0;}
  if (greenValue + 6 <= 255) { greenValue = greenValue + 6;  } else {greenValue = 0;}
  
  
  analogWrite(redPin, redValue);
  analogWrite(bluePin, blueValue);
  analogWrite(greenPin, greenValue);
  
  delay(45);
}
