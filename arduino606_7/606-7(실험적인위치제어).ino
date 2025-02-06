#include <Encoder.h>

#define y_pin 2 //노란선
#define g_pin 3 //녹색선

Encoder myEnc(y_pin, g_pin);

#define IN3 4
#define IN4 5
#define ENB 6

//IN3이 LOW고 IN4가 HIGH일때 시계방향회전(정회전)
//IN3이 HIGH고 IN4가 LOW일때 반시계방향회전(역회전)

//0.1초의 간격을 만들기 위해서 필요한 번수
unsigned long t = 0;
int pos = 0;

int direct = 0; //0이면 멈춤 1이면 정회전 -1면 역회전

void setup() {
  Serial.begin(9600);
  //방향제어핀
  pinMode(IN3,OUTPUT);
  pinMode(IN4,OUTPUT);

  analogWrite(ENB,130);
}

void loop() {
  //녹칸다에게 어디로 이동할지 위치를 입력받는다
  if(Serial.available()){
    String text = Serial.readStringUntil('\n'); //LF
    pos = text.toInt();//문자열을 숫자로 바꿔라! 

    if(pos > myEnc.read()){
      //정회전해야하는상황
      Serial.println("정회전출동!");
      direct = 1;
    }else if(pos < myEnc.read()){
      //역회전해야하는상황
      Serial.println("역회전출동!");
      direct = -1;
    }else{
      Serial.println("현재위치와 목표위치가 같습니다!");
    }
  }

  if(direct == 1){
    //정회전을 해야하는 상황
    digitalWrite(IN3,LOW);
    digitalWrite(IN4,HIGH);

    if(pos <= myEnc.read()){
      Serial.println("정회전하다가 멈추는 상황!");
      Serial.print("오차=");
      Serial.println(pos - myEnc.read());
      digitalWrite(IN3,LOW);
      digitalWrite(IN4,LOW);
      direct = 0;
      //오차가 있는건 알겠는데 현재위치를 목표위치와 같게하라
      delay(1000);
      myEnc.write(pos);
      Serial.print("현재 설정된 엔코더값=");
      Serial.println(myEnc.read());
    }
  }else if(direct == -1){
    //역회전을 해야하는 상황
    digitalWrite(IN3,HIGH);
    digitalWrite(IN4,LOW);
    if(pos >= myEnc.read()){
      Serial.println("역회전하다가 멈추는 상황!");
      Serial.print("오차=");
      Serial.println(pos - myEnc.read());
      digitalWrite(IN3,LOW);
      digitalWrite(IN4,LOW);
      direct = 0;
      //오차가 있는건 알겠는데 현재위치를 목표위치와 같게하라
      delay(1000);
      myEnc.write(pos);
      Serial.print("현재 설정된 엔코더값=");
      Serial.println(myEnc.read());
    }
  }



  /*
  //0.1초마다 이 조건문이 걸리게된다
  if(millis() - t > 100){
     t = millis();     
     //엔코더 량을 측정한다
     long newPosition = myEnc.read();
     //속도계산하기
     float myspeed = (newPosition * 600) / 900.0;
     Serial.println(myspeed);
     display.showNumberDec(myspeed, false);
     //현재까지 측정한 엔코더량 초기화
     myEnc.write(0);
  }
  */
}






