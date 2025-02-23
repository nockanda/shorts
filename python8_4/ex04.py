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
def publish_message(led_num,led_control):
    nockanda_info = {
        "num":led_num,
        "control":led_control
    }
    payload = json.dumps(nockanda_info); #JSON으로 변환
    client.publish(PUBLISH_TOPIC, payload)  # 버튼 클릭 시 발행할 메시지
    update_text("Published: Hello from GUI!")

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
    led_state1.config(text=f"LED1: {json_data['led1']}")
    led_state2.config(text=f"LED2: {json_data['led2']}")
    led_state3.config(text=f"LED3: {json_data['led3']}")
    led_state4.config(text=f"LED4: {json_data['led4']}")


# GUI 설정
root = tk.Tk()
root.title("녹칸다의 파이썬 MQTT")

label = ttk.Label(root, text="MQTT Messages")
label.pack(pady=10)

text_box = tk.Text(root, height=10, width=50)
text_box.pack(pady=10)

led_state1 = ttk.Label(root, text="LED1 : OFF",font=("Arial", 30))
led_state1.pack(pady=10)
led_state2 = ttk.Label(root, text="LED2 : OFF",font=("Arial", 30))
led_state2.pack(pady=10)
led_state3 = ttk.Label(root, text="LED3 : OFF",font=("Arial", 30))
led_state3.pack(pady=10)
led_state4 = ttk.Label(root, text="LED4 : OFF",font=("Arial", 30))
led_state4.pack(pady=10)

button1 = ttk.Button(root, text="LED1번 OFF", command=lambda: publish_message(1,0))
button1.pack(pady=10)
button2 = ttk.Button(root, text="LED1번 ON", command=lambda: publish_message(1,1))
button2.pack(pady=10)
button3 = ttk.Button(root, text="LED2번 OFF", command=lambda: publish_message(2,0))
button3.pack(pady=10)
button4 = ttk.Button(root, text="LED2번 ON", command=lambda: publish_message(2,1))
button4.pack(pady=10)
button5 = ttk.Button(root, text="LED3번 OFF", command=lambda: publish_message(3,0))
button5.pack(pady=10)
button6 = ttk.Button(root, text="LED3번 ON", command=lambda: publish_message(3,1))
button6.pack(pady=10)
button7 = ttk.Button(root, text="LED4번 OFF", command=lambda: publish_message(4,0))
button7.pack(pady=10)
button8 = ttk.Button(root, text="LED4번 ON", command=lambda: publish_message(4,1))
button8.pack(pady=10)
button9 = ttk.Button(root, text="모든 LED OFF", command=lambda: publish_message(0,0))
button9.pack(pady=10)
button10 = ttk.Button(root, text="모든 LED ON", command=lambda: publish_message(0,1))
button10.pack(pady=10)


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

