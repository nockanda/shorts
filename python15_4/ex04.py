import keyboard
import time
import random
import cv2
from ultralytics import YOLO
import logging
#yolo관련 메시지는 비활성화
logging.getLogger("ultralytics").setLevel(logging.WARNING)

# YOLOv8 모델 로드
model = YOLO('example15.pt')

# ESP32-CAM 스트리밍 URL 설정
stream_url = 'http://172.30.1.83:81/stream'


print("가위바위보 게임이 시작되었습니다! s키를 누르고 카메라에 가위바위보 3개중에 하나를 인식시켜주세요!")

nockanda_money = 100000

while True:
    #가위바위보 게임이 실행되는 부분!
    if keyboard.is_pressed('s'):
        # 기계에 코인 투입!
        nockanda_money -= 1000
        # 비디오 스트림 열기
        print("카메라와 연결!")
        cap = cv2.VideoCapture(stream_url)

        if not cap.isOpened():
            print("오류: 비디오 스트림을 열 수 없습니다.")
            exit()

        print("가위바위보 게임이 시작됩니다!")

        #사용자가 가위바위보 3개중에 하나를 인식시킬떄까지 무한대기
        nockanda_detect = "NO"
        while True:
            ret, frame = cap.read()
    
            if not ret:
                print("프레임을 가져오지 못했습니다.")
                break
    
            # YOLOv8 객체 인식 실행
            results = model(frame)
    
            # 인식된 객체들 반복 처리
            for result in results:
                for box in result.boxes:
                    # 신뢰도와 클래스 ID 가져오기
                    conf = box.conf[0]
                    class_id = box.cls[0].item()
                    class_name = model.names[class_id]
                    if conf > 0.8:
                        nockanda_detect = class_name
            if nockanda_detect != "NO":
                print("사용자가 인식시킨것="+nockanda_detect)
                print("카메라와 접속해제!")
                cap.release()
                break


        #1부터 10까지를 뽑는다!
        random_number = random.randint(1, 100)

        if random_number >= 1 and random_number <= 40:
            #파이썬승리(사용자가 낸것에서 이기는것을 낸다)
            print("파이썬이 승리했습니다!")
            if nockanda_detect == 'mook':
                print("파이썬이 낸것=빠, 녹칸다가 낸것=묵")
            elif nockanda_detect == 'ggi':
                print("파이썬이 낸것=묵, 녹칸다가 낸것=찌")
            elif nockanda_detect == 'bba':
                print("파이썬이 낸것=찌, 녹칸다가 낸것=빠")
        elif random_number >= 41 and random_number <= 70:
            #비김
            print("파이썬과 녹칸다가 비겼습니다!")
            nockanda_money += 1000 #비기면 돌려줌!
            if nockanda_detect == 'mook':
                print("파이썬이 낸것=묵, 녹칸다가 낸것=묵")
            elif nockanda_detect == 'ggi':
                print("파이썬이 낸것=찌, 녹칸다가 낸것=찌")
            elif nockanda_detect == 'bba':
                print("파이썬이 낸것=빠, 녹칸다가 낸것=빠")
        elif random_number >= 71 and random_number <= 100:
            #패배
            print("녹칸다가 승리했습니다!")
            
            if random_number == 100:
                print("대박당첨!!!!!!!!!!!!!!!!!!!")
                nockanda_money += 4000
            else:
                print("일반보상!")
                nockanda_money += 1200

            if nockanda_detect == 'mook':
                print("파이썬이 낸것=찌, 녹칸다가 낸것=묵")
            elif nockanda_detect == 'ggi':
                print("파이썬이 낸것=빠, 녹칸다가 낸것=찌")
            elif nockanda_detect == 'bba':
                print("파이썬이 낸것=묵, 녹칸다가 낸것=빠")

        print(f"현재 녹칸다가 가진 money={nockanda_money}")
        print("게임이 종료됩니다!")

    # q 키 입력이 감지되면 루프 종료
    if keyboard.is_pressed('q'):
        print("q 키가 눌려 프로그램을 종료합니다.")
        break

cv2.destroyAllWindows()