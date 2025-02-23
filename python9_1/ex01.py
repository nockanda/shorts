import tkinter as tk
from tkinter import ttk
import paho.mqtt.client as mqtt
import threading
import json  # json 모듈 추가

# MQTT 설정
BROKER_ADDRESS = "broker.emqx.io"
SUBSCRIBE_TOPIC = "nockanda/input" #ESP32에서 받을 토픽
PUBLISH_TOPIC = "nockanda/output"  #ESP32로 보낼 토픽

# 버튼 추가
def publish_message():
    #각도입력칸에 있는 값을 변수처럼 활용해야함!
    mydegree = degree.get("1.0", "end-1c")
    nockanda_info = {
        "degree":mydegree
    }
    payload = json.dumps(nockanda_info); #JSON으로 변환
    client.publish(PUBLISH_TOPIC, payload)  # 버튼 클릭 시 발행할 메시지
    #update_text("Published: Hello from GUI!")

# GUI 업데이트 (텍스트 박스에 메시지 추가)
def update_text(message):
    text_box.insert(tk.END, f"{message}\n")
    text_box.see(tk.END)

#브로커와 접속이 수락된경우 1회 호출됨!
def on_connect(client, userdata, flags, rc):
    #콘솔에 접속이 되었다고 출력함!
    print("Connected with result code " + str(rc))
    #브로커에게 파이써쪽에서 토픽을 구독등록하는 함수!
    client.subscribe(SUBSCRIBE_TOPIC)


def on_message(client, userdata, msg):
    message = msg.payload.decode() #JSON
    json_data = json.loads(message) #역직렬화
    print(f"Received message: {json_data}") #콘솔출력
    update_text(message) #출력함수호출
    now_degree.config(text=json_data["degree"])

# GUI 설정
root = tk.Tk()
root.title("녹칸다의 파이썬 MQTT")

label = ttk.Label(root, text="MQTT Messages")
label.pack(pady=10)

text_box = tk.Text(root, height=10, width=50)
text_box.pack(pady=10)

#현재 ESP32의 각도
now_degree = ttk.Label(root, text="0",font=("Arial", 30))
now_degree.pack(pady=10)

label2 = ttk.Label(root, text="서보모터각도입력!")
label2.pack(pady=10)
#각도값을 입력받을 textbox
degree = tk.Text(root, height=1, width=5)
degree.pack(pady=10)

#버튼누르면 publish_message라는 함수가 호출됨!
button = ttk.Button(root, text="각도전송!", command=publish_message)
button.pack(pady=10)

# MQTT 클라이언트 시작
client = mqtt.Client()
#브로커랑 접속이 완료되면 내가 어떤 함수를 호출해줄게!
client.on_connect = on_connect
#누군가가 발행한 메시지를 수신하면 어떤 함수를 호출해줄게!
client.on_message = on_message

#파이썬과 브로커와 접속하기!
client.connect(BROKER_ADDRESS, 1883, 60)
#이부분은 그냥 있으면 되는거다라고 생각하기!
mqtt_thread = threading.Thread(target=client.loop_start)
mqtt_thread.daemon = True
mqtt_thread.start()

# Tkinter 메인 루프 실행
root.mainloop()
