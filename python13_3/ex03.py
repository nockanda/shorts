import cv2
import serial
import json
import threading
import logging
from ultralytics import YOLO

#yolo관련 메시지는 비활성화
logging.getLogger("ultralytics").setLevel(logging.WARNING)

# 초기 시리얼 포트 설정
arduino = None
serial_thread = None

def read_from_arduino():
    global arduino
    try:
        while arduino and arduino.is_open:
            if arduino.in_waiting > 0:
                data = arduino.readline().decode('utf-8').strip()
                print(data)
    except serial.SerialException:
        print("Serial connection lost.\n")

try:
    arduino = serial.Serial('COM6', 9600) #포트번호와 통신속도 확인하기
    print("파이썬과 아두이노가 연결되었습니다!\n")
    # 시리얼 읽기 스레드 시작
    serial_thread = threading.Thread(target=read_from_arduino)
    serial_thread.daemon = True
    serial_thread.start()
except Exception as e:
    print(f"접속실패!: {e}")

# YOLOv8 모델 로드
model = YOLO('best1.pt')

# ESP32-CAM 스트리밍 URL 설정
stream_url = 'http://172.30.1.83:81/stream'

# 비디오 스트림 열기
cap = cv2.VideoCapture(stream_url)

if not cap.isOpened():
    print("오류: 비디오 스트림을 열 수 없습니다.")
    exit()

while True:
    ret, frame = cap.read()
    
    if not ret:
        print("프레임을 가져오지 못했습니다.")
        break
    
    # YOLOv8 객체 인식 실행
    results = model(frame)
    
    #미리 학습되어있는 클래스명을 알아둬야한다!
    nockanda_obj = {
        "greenzone":0,
        "redzone":0
    }

    #레드존
    cv2.rectangle(frame, (320, 0), (635, 475), (0, 0, 255), 4)
    #그린존
    cv2.rectangle(frame, (0, 0), (315, 475), (0, 255, 0), 4)

    # 인식된 객체들 반복 처리
    for result in results:
        for box in result.boxes:
            # 신뢰도와 클래스 ID 가져오기
            conf = box.conf[0]
            class_id = box.cls[0].item()
            class_name = model.names[class_id]

            # 바운딩 박스 좌표 추출
            x1, y1, x2, y2 = box.xyxy[0]
            x1, y1, x2, y2 = int(x1), int(y1), int(x2), int(y2)

            if x2 > 320:
                nockanda_obj["redzone"] += 1
            else:            
                nockanda_obj["greenzone"] += 1



            # 사각형 그리기
            cv2.rectangle(frame, (x1, y1), (x2, y2), (255, 0, 0), 2)
                
            # 클래스명과 신뢰도 출력
            label = f'{class_name}: {conf:.2f}'
            cv2.putText(frame, label, (x1, y1 - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 255, 255), 2)

    #결과출력(아두이노로 전송)
    #print(nockanda_obj)
    if arduino and arduino.is_open:
        # JSON 직렬화
        json_data = json.dumps(nockanda_obj) + '\n'
        arduino.write(json_data.encode('utf-8'))

    # 바운딩 박스와 클래스명이 포함된 프레임 표시
    cv2.imshow("ESP32-CAM YOLOv8 Detection", frame)
    
    # 'q' 키를 눌러 루프 종료
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

#만약 파이썬과 아두이노가 연결되어있다면 종료하겠다!
if arduino and arduino.is_open:
    arduino.close()
else:
    print("파이썬과 아두이노가 연결에 실패했거나 시리얼포트가 이미 닫혀있습니다!")

# 비디오 캡처 객체 해제 및 OpenCV 창 닫기
cap.release()
cv2.destroyAllWindows()
