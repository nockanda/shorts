#include <Arduino.h>
#include <LiquidCrystal_I2C.h>

//1602LCD의 주소는 0x27인게 있고 0x3F인것도 있음!
LiquidCrystal_I2C lcd(0x27,16,2); 

#define Vin 5 //입력전압이 5V인 상황!
#define R2 120 //고정저항값의 크기
#define Sensor A0 //센서는 A0에 연결됨!
#define R1_MAX 186 //가장 큰 저항값
#define R1_MIN 3 //가장 낮은 저항값
#define window_size 20 //숫자가 커질수록 필터강도 증가

//윈도우 사이즈가 크면클수록 필터강도 증가함!
//단 윈도우 사이즈만큼 지연됨
float moving_window[window_size];
int cnt = 0;


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
    lcd.init();
    lcd.backlight();

    lcd.setCursor(0,0);
    lcd.print("Oil Float Sensor");
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
        Serial.println(filltered_percent);
 
        String text = "      " + String(filltered_percent) + "%";
        //text문자열 길이를 16으로 맞춘다
        while(text.length() < 16){
            text += " ";
        }

        lcd.setCursor(0,1);
        lcd.print(text);
    }

    delay(100);
}