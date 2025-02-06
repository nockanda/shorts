#include <Arduino.h>

#define Vin 5 //입력전압이 5V인 상황!
#define R2 120 //고정저항값의 크기
#define Sensor A0 //센서는 A0에 연결됨!

void setup(void) {
    //결과를 시리얼통신으로 출력하겠다!
    Serial.begin(9600);
}

void loop(void) {
    //아날로그 0번핀에 10bit 아날로그값을 측정함!
    int analog = analogRead(Sensor); //0~1023
    //Vout계산하기!
    float Vout = Vin * analog/1023.0;
    //저항값 계산하기!
    float R1 = (Vin*R2-Vout*R2)/Vout;

    Serial.print(analog);
    Serial.print(", ");
    Serial.print(Vout);
    Serial.print(", ");
    Serial.println(R1); //현재 저항값!
    delay(100);
}