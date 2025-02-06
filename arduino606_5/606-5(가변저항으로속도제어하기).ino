#include <Encoder.h>

#define y_pin 2 //노란선
#define g_pin 3 //녹색선

Encoder myEnc(y_pin, g_pin);

#define IN3 4
#define IN4 5
#define ENB 6

#define btn1 8
#define btn2 9
#define btn3 10

//IN3이 LOW고 IN4가 HIGH일때 시계방향회전(정회전)
//IN3이 HIGH고 IN4가 LOW일때 반시계방향회전(역회전)

//0.1초의 간격을 만들기 위해서 필요한 번수
unsigned long t = 0;

void setup() {
  Serial.begin(9600);
  //방향제어핀
  pinMode(IN3,OUTPUT);
  pinMode(IN4,OUTPUT);
  pinMode(btn1,INPUT_PULLUP);
  pinMode(btn2,INPUT_PULLUP);
  pinMode(btn3,INPUT_PULLUP);
}

void loop() {
  if(digitalRead(btn1) == LOW){
    //정회전
    digitalWrite(IN3,LOW);
    digitalWrite(IN4,HIGH);
  }
  if(digitalRead(btn2) == LOW){
    //정지
    digitalWrite(IN3,LOW);
    digitalWrite(IN4,LOW);
  }
  if(digitalRead(btn3) == LOW){
    //역회전
    digitalWrite(IN3,HIGH);
    digitalWrite(IN4,LOW);
    
  }
  
  //0.1초마다 이 조건문이 걸리게된다
  if(millis() - t > 100){
     t = millis();
     //가변저항값을 이용해서 속도를 입력한다
     //가변저항값은 10bit(0~1023) 아날로그값이고 pwm출력값은 8bit(0~255)값이다!
     int photen = analogRead(A0); //0~1023
     photen = map(photen,0,1023,0,255);
     analogWrite(ENB,photen);
     
     //엔코더 량을 측정한다
     long newPosition = myEnc.read();
     //속도계산하기
     float myspeed = (newPosition * 600) / 900.0;
     Serial.println(myspeed);
     //현재까지 측정한 엔코더량 초기화
     myEnc.write(0);
  }
}





