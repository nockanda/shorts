#include <Arduino.h>

#define Vin 5 //입력전압이 5V인 상황!
#define R2 120 //고정저항값의 크기
#define Sensor A0 //센서는 A0에 연결됨!
#define R1_MAX 186 //가장 큰 저항값
#define R1_MIN 3 //가장 낮은 저항값
#define window_size 100 //숫자가 커질수록 필터강도 증가

//윈도우 사이즈가 크면클수록 필터강도 증가함!
//단 윈도우 사이즈만큼 지연됨
float moving_window[window_size];
int cnt = 0;
//10개의 디지털핀에 LED가 연결될것이다!
byte led_pins[] = {2,3,4,5,6,7,8,9,10,11};

float get_sensor(){
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

    return percent;
}

void setup(void) {
    //결과를 시리얼통신으로 출력하겠다!
    Serial.begin(9600);
    //모든 디지털핀을 출력으로 설정하겠따!
    for(int i = 0;i<10;i++){
        pinMode(led_pins[i],OUTPUT);
    }
}

void loop(void) {
    
    float now_sensor = get_sensor();
    //배열에 값이 20개가 차기 전까지는 대입만한다!
    if(cnt < window_size){
        moving_window[cnt] = now_sensor;
        cnt++;
    }else{
        //20개의 샘플이 수집이 완료됨!
        //새롭게 측정된거를 제일 마지막에 넣고
        //제일 처음 데이터를 빼야한다!
        for(int i = 1;i<window_size;i++){
            moving_window[i-1] = moving_window[i];
        }
        moving_window[window_size-1] = now_sensor;
        //평균계산하기

        float filltered_percent = 0;
        for(int i = 0;i<window_size;i++){
            filltered_percent += moving_window[i];
        }
        filltered_percent /= window_size;

        Serial.print(now_sensor);
        Serial.print(", ");
        Serial.print(filltered_percent);

        //filltered_percent
        //이값이 1~10이면 2번핀만 HIGH
        //11~20 ...... 91~100
        int nockanda = map(filltered_percent,0,100,0,11);
        Serial.print(", ");
        Serial.println(nockanda);
        for(int i = 0;i<nockanda;i++){
            digitalWrite(led_pins[i],HIGH);
        }
        for(int i = nockanda;i<10;i++){
            digitalWrite(led_pins[i],LOW);
        }
    }

    delay(10);
}