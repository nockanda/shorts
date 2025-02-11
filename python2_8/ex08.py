import serial
import threading
import tkinter as tk
from tkinter import messagebox

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
        #arduino라는 변수가 생성이 되고 아두이노와 확실히 연결이 되었다면~
        while arduino and arduino.is_open:
            if arduino.in_waiting > 0:
                data = arduino.readline().decode('utf-8').strip()
                text_box2.delete("1.0",tk.END)
                text_box2.insert(tk.END, data)
                label.config(text=data)
    except serial.SerialException:
        text_box.insert(tk.END, "Serial connection lost.\n")
        text_box.see(tk.END)

def send_to_arduino():
    global arduino
    if arduino and arduino.is_open:
        arduino.write(b'test\n')
    else:
        messagebox.showinfo("Info", "Serial port is not open.")

# GUI 설정
root = tk.Tk()
root.title("Arduino Serial Communication")

#아두이노가 보낸 데이터를 출력하는 부분
text_box = tk.Text(root, height=4, width=10, font=("Arial", 30))
text_box.pack()
text_box2 = tk.Text(root, height=2, width=10, font=("Arial", 30))
text_box2.pack()
label = tk.Label(root, text="0", font=("Arial", 14))
label.pack()
#아두이노와 연결하는 부분
open_button = tk.Button(root, text="Open Serial Port", command=open_serial)
open_button.pack()
#아두이노와 연결을 끊는부분
close_button = tk.Button(root, text="Close Serial Port", command=close_serial)
close_button.pack()
#아두이노에게 데이터를 전송하는부분
send_button = tk.Button(root, text="Send 'test'", command=send_to_arduino)
send_button.pack()
#파이썬에서 GUI가 유지되기위하서 실행되는 메인스레드!
root.mainloop()
