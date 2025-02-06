#include <Encoder.h>

#define y_pin 2 //노란선
#define g_pin 3 //녹색선

Encoder myEnc(y_pin, g_pin);

void setup() {
  Serial.begin(9600);
  Serial.println("Basic Encoder Test:");
}

long oldPosition  = -999;

void loop() {
  //로터리엔코더에서 현재 상태를 읽어온다!
  long newPosition = myEnc.read();

  //이전상태와 현재상태의 차이가 있으면 뭔가 회전한것이다!
  if (newPosition != oldPosition) {
    if(oldPosition - newPosition > 0){
      //이전보다 현재값이 더 떨어졌다(감소중)
      Serial.print("반시계방향회전중=");
    }else{
      //증가중
      Serial.print("시계방향회전중=");
    }
    oldPosition = newPosition;
    Serial.println(newPosition);
  }
}