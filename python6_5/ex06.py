import serial
import threading
import tkinter as tk
from tkinter import messagebox
import json  # json 모듈 추가

# 초기 시리얼 포트 설정
arduino = None
serial_thread = None  # 스레드를 추적할 변수

user1 = {
    "name":"녹칸다",
    "age":20,
    "gender":"남성",
    "job":"스트리머"
}
user2 = {
    "name":"홍길동",
    "age":40,
    "gender":"남성",
    "job":"의로운산적"
}
user3 = {
    "name":"김땡땡",
    "age":25,
    "gender":"여성",
    "job":"학생"
}
user4 = {
    "name":"박땡땡",
    "age":62,
    "gender":"남성",
    "job":"선생님"
}
user5 = {
    "name":"정땡땡",
    "age":17,
    "gender":"여성",
    "job":"학생"
}
user6 = {
    "name":"서땡땡",
    "age":33,
    "gender":"남성",
    "job":"광부"
}

user_info = {
    "d46da22a":user1,
    "4d94a03d":user2,
    "1c75843d":user3,
    "bf5fa03d":user4,
    "1783a03d":user5,
    "6b8a03d":user6
}


def open_serial():
    global arduino, serial_thread
    try:
        arduino = serial.Serial('COM6', 9600)
        text_box.insert(tk.END, "Serial port opened.\n")
        text_box.see(tk.END)
        # 시리얼 읽기 스레드 시작
        serial_thread = threading.Thread(target=read_from_arduino)
        serial_thread.daemon = True
        serial_thread.start()
    except Exception as e:
        messagebox.showerror("Error", f"Failed to open serial port: {e}")

def close_serial():
    global arduino
    if arduino and arduino.is_open:
        arduino.close()
        text_box.insert(tk.END, "Serial port closed.\n")
        text_box.see(tk.END)
    else:
        messagebox.showinfo("Info", "Serial port is already closed.")
def read_from_arduino():
    global arduino
    try:
        while arduino and arduino.is_open:
            if arduino.in_waiting > 0:
                # 아두이노로부터 데이터를 읽음
                data = arduino.readline().decode('utf-8').strip()
                
                # JSON 데이터 역직렬화 시도
                try:
                    json_data = json.loads(data)
                    text_box.insert(tk.END, f"Received JSON: {json_data}\n")
                    tagid = json_data["tagid"]
                    myimg.config(image=user_info[tagid]["pic"])
                    myimg.image = user_info[tagid]["pic"] 
                    entry_id.delete(0, tk.END)  # 기존 텍스트를 삭제
                    entry_id.insert(0, tagid) 
                    entry_name.delete(0, tk.END)  # 기존 텍스트를 삭제
                    entry_name.insert(0, user_info[tagid]["name"]) 
                    entry_age.delete(0, tk.END)  # 기존 텍스트를 삭제
                    entry_age.insert(0, user_info[tagid]["age"]) 
                    entry_gender.delete(0, tk.END)  # 기존 텍스트를 삭제
                    entry_gender.insert(0, user_info[tagid]["gender"]) 
                    entry_job.delete(0, tk.END)  # 기존 텍스트를 삭제
                    entry_job.insert(0, user_info[tagid]["job"]) 
                except json.JSONDecodeError:
                    text_box.insert(tk.END, f"Received (not JSON): {data}\n")
    except serial.SerialException:
        text_box.insert(tk.END, "Serial connection lost.\n")
        text_box.see(tk.END)

def send_to_arduino():
    global arduino
    if arduino and arduino.is_open:
        # JSON 데이터 생성
        data_dict = {
            "key1": 123,
            "key2": 456,
            "key3": 789
        }
        
        # JSON 직렬화
        json_data = json.dumps(data_dict) + '\n'
        
        # JSON 데이터를 시리얼로 전송
        arduino.write(json_data.encode('utf-8'))
        text_box.insert(tk.END, f"Sent: {json_data}\n")
        text_box.see(tk.END)
    else:
        messagebox.showinfo("Info", "Serial port is not open.")
# GUI 설정
root = tk.Tk()
root.title("Arduino Serial Communication")

user1_img = tk.PhotoImage(file="user1.png")
user2_img = tk.PhotoImage(file="user2.png")
user3_img = tk.PhotoImage(file="user3.png")
user4_img = tk.PhotoImage(file="user4.png")
user5_img = tk.PhotoImage(file="user5.png")
user6_img = tk.PhotoImage(file="user6.png")

user1["pic"] = user1_img
user2["pic"] = user2_img
user3["pic"] = user3_img
user4["pic"] = user4_img
user5["pic"] = user5_img
user6["pic"] = user6_img

text_box = tk.Text(root, height=10, width=50)
text_box.pack()

open_button = tk.Button(root, text="Open Serial Port", command=open_serial)
open_button.pack()

close_button = tk.Button(root, text="Close Serial Port", command=close_serial)
close_button.pack()

myimg = tk.Label(root)
myimg.pack()

# 프레임을 사용하여 라벨과 엔트리 쌍을 배치
frame_id = tk.Frame(root)
frame_id.pack(pady=5)
label_id = tk.Label(frame_id, text="태그ID:",font=("Arial", 30))
label_id.pack(side="left", padx=10)
entry_id = tk.Entry(frame_id,font=("Arial", 30))
entry_id.pack(side="left", padx=10)

frame_name = tk.Frame(root)
frame_name.pack(pady=5)
label_name = tk.Label(frame_name, text="이름:",font=("Arial", 30))
label_name.pack(side="left", padx=10)
entry_name = tk.Entry(frame_name,font=("Arial", 30))
entry_name.pack(side="left", padx=10)

frame_age = tk.Frame(root)
frame_age.pack(pady=5)
label_age = tk.Label(frame_age, text="나이:",font=("Arial", 30))
label_age.pack(side="left", padx=10)
entry_age = tk.Entry(frame_age,font=("Arial", 30))
entry_age.pack(side="left", padx=10)

frame_gender = tk.Frame(root)
frame_gender.pack(pady=5)
label_gender = tk.Label(frame_gender, text="성별:",font=("Arial", 30))
label_gender.pack(side="left", padx=10)
entry_gender = tk.Entry(frame_gender,font=("Arial", 30))
entry_gender.pack(side="left", padx=10)

frame_job = tk.Frame(root)
frame_job.pack(pady=5)
label_job = tk.Label(frame_job, text="직업:",font=("Arial", 30))
label_job.pack(side="left", padx=10)
entry_job = tk.Entry(frame_job,font=("Arial", 30))
entry_job.pack(side="left", padx=10)

root.mainloop()
