import serial
import threading
import tkinter as tk
from tkinter import messagebox
import json  # json 모듈 추가

# 초기 시리얼 포트 설정
arduino = None
serial_thread = None  # 스레드를 추적할 변수

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
                    label1.config(text=json_data["degree"])
                except json.JSONDecodeError:
                    text_box.insert(tk.END, f"Received (not JSON): {data}\n")
    except serial.SerialException:
        text_box.insert(tk.END, "Serial connection lost.\n")
        text_box.see(tk.END)

def send_to_arduino(input):
    global arduino
    if arduino and arduino.is_open:
        # JSON 데이터 생성
        data_dict = {
            "servo": input
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

text_box = tk.Text(root, height=10, width=50)
text_box.pack()

open_button = tk.Button(root, text="Open Serial Port", command=open_serial)
open_button.pack()

close_button = tk.Button(root, text="Close Serial Port", command=close_serial)
close_button.pack()

label1 = tk.Label(root, text="초기값없음!", font=("Arial", 30))
label1.pack()

send_button1 = tk.Button(root, text="각도-10", command=lambda: send_to_arduino(0))
send_button1.pack()

send_button2 = tk.Button(root, text="각도+10", command=lambda: send_to_arduino(1))
send_button2.pack()

root.mainloop()
