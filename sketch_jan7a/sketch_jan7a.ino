#include <ESP8266WiFi.h>
#include <ESP8266WebServer.h>

const char* ssid = "IZZI-D533";      
const char* password = "F0AF854BD533";

ESP8266WebServer server(80);

int pinFoco = 5; // GPIO5 (D1 en NodeMCU)

void setup() {
  Serial.begin(115200);

  pinMode(pinFoco, OUTPUT); 
  digitalWrite(pinFoco, LOW); 

  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.println("Conectando a Wi-Fi...");
  }
  Serial.println("Conexión Wi-Fi establecida");
  Serial.print("IP del ESP8266: ");
  Serial.println(WiFi.localIP());

  server.on("/encender", []() {
    digitalWrite(pinFoco, HIGH);
    server.send(200, "text/plain", "Foco encendido");
  });

  server.on("/apagar", []() {
    digitalWrite(pinFoco, LOW);
    server.send(200, "text/plain", "Foco apagado");
  });

  server.begin(); // Inicia el servidor
  Serial.println("Servidor web iniciado");
}

void loop() {
  server.handleClient();
}
