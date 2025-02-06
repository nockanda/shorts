#include <Arduino.h>

#define Vin 5 //입력전압이 5V인 상황!
#define R2 120 //고정저항값의 크기
#define Sensor A0 //센서는 A0에 연결됨!
#define R1_MAX 186 //가장 큰 저항값
#define R1_MIN 3 //가장 낮은 저항값

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
    //저항값이 지정된 범위가 되도록 하기!
    R1 = map(R1,R1_MIN,R1_MAX,0,190);
    R1 = constrain(R1,0,190);
    //퍼센테이지 계산하기
    float percent = R1/190 * 100;

    Serial.print(analog);
    Serial.print(", ");
    Serial.print(Vout);
    Serial.print(", ");
    Serial.print(R1); //현재 저항값!
    Serial.print(", ");
    Serial.println(percent);
    delay(100);
}