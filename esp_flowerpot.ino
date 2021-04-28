
#include <ESP8266WiFi.h>
#include <Arduino.h>
#include <ESP8266HTTPClient.h>
#include <Wire.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_TSL2561_U.h>

const char* ssid = "Whatever";
const char* pass = "50GK14g434p4";

#define SOILMOISTURE_3V3_PIN_SIG  A0
#define WATERPUMP_PIN D5
#define LED_PIN  D6

Adafruit_TSL2561_Unified tsl = Adafruit_TSL2561_Unified(TSL2561_ADDR_FLOAT, 12345);

const int wet = 470;
const int dry = 880;
bool manual = false;

void setup()
{ 
  Serial.begin(9600);
  
  pinMode(LED_PIN, OUTPUT);
  pinMode(WATERPUMP_PIN, OUTPUT);

  WiFi.begin(ssid, pass);
  Serial.println("Connecting");

  while (WiFi.status() != WL_CONNECTED)
  {
    delay(1000);
    Serial.print(".");
  }

  Serial.println("Connected");
  
  if(!tsl.begin())
  {
    /* There was a problem detecting the TSL2561 ... check your connections */
    Serial.print("Ooops, no TSL2561 detected ... Check your wiring or I2C ADDR!");
    while(1);
  }

}

void loop() 
{ 
    int soilMoisture = map(analogRead(SOILMOISTURE_3V3_PIN_SIG), dry, wet, 0, 100);
    //int soilMoisture = analogRead(SOILMOISTURE_3V3_PIN_SIG);

    sensors_event_t event;
    tsl.getEvent(&event);
    int luminosity = event.light;
    Serial.println(luminosity);
    Serial.println(soilMoisture);

    if(luminosity < 5 && !manual){
      digitalWrite(LED_PIN, HIGH);
    }
    else if(luminosity > 5 && !manual){
      digitalWrite(LED_PIN, LOW);
    }

    if (WiFi.status() == WL_CONNECTED){
      HTTPClient http;
      String receiver = "http://iotgecik.azurewebsites.net/receiver.aspx?";
      receiver += "moist=" + String(soilMoisture);
      receiver += "&lum=" + String(luminosity);

      http.begin(receiver);
      http.GET();
      http.end();

      String request = "http://iotgecik.azurewebsites.net/output.txt";
      http.begin(request);
      if (http.GET() > 0){
        String payload = http.getString();

        if(payload[0] == '1'){
          digitalWrite(WATERPUMP_PIN, HIGH);
          delay(5000);
          digitalWrite(WATERPUMP_PIN, LOW);
          return;
        }
        if(payload[3] == '1'){          
          digitalWrite(LED_PIN, HIGH);
          manual = true;
        }
        else if(payload[3] == '0'){          
          digitalWrite(LED_PIN, LOW);
          manual = true;
        }
        else{
          manual = false;
        }
      }
    }
    
    if(soilMoisture < 20){
      digitalWrite(WATERPUMP_PIN, HIGH);
      delay(5000);
      digitalWrite(WATERPUMP_PIN, LOW);
      return;
    }
    
    delay(5000);
}
