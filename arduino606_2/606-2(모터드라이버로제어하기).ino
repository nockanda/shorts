#include <Encoder.h>

#define y_pin 2 //노란선
#define g_pin 3 //녹색선

Encoder myEnc(y_pin, g_pin);

#define IN3 4
#define IN4 5
#define ENB 6

//IN3이 LOW고 IN4가 HIGH일때 시계방향회전(정회전)
//IN3이 HIGH고 IN4가 LOW일때 반시계방향회전(역회전)

long oldPosition  = 0;

void setup() {
  Serial.begin(9600);
  //방향제어핀
  pinMode(IN3,OUTPUT);
  pinMode(IN4,OUTPUT);
}

void loop() {
  //컴퓨터에서 뭔가 명령이 전송되었다!
  if(Serial.available()){
    char c =Serial.read();
    if(c == '0'){
      //정지
      digitalWrite(IN3,LOW);
      digitalWrite(IN4,LOW);
      analogWrite(ENB,0);
    }else if(c == '1'){
      //정회전
      digitalWrite(IN3,LOW);
      digitalWrite(IN4,HIGH);
      analogWrite(ENB,255);
    }else if(c == '2'){
      //역회전
      digitalWrite(IN3,HIGH);
      digitalWrite(IN4,LOW);
      analogWrite(ENB,255);
    }
  }

  //로터리엔코더에서 현재 상태를 읽어온다!
  long newPosition = myEnc.read();

  //이전상태와 현재상태의 차이가 있으면 뭔가 회전한것이다!
  if (newPosition != oldPosition) {
    /*
    if(oldPosition - newPosition > 0){
      //이전보다 현재값이 더 떨어졌다(감소중)
      Serial.print("반시계방향회전중=");
    }else{
      //증가중
      Serial.print("시계방향회전중=");
    }*/
    oldPosition = newPosition;
    Serial.println(newPosition);
  }
}




