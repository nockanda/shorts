import cv2 #opencv(설치필요)
import tkinter as tk
from tkinter import Label, Checkbutton, IntVar
from PIL import Image, ImageTk, ImageDraw, ImageFont #Pillow모듈(설치필요)
import datetime
import time
import numpy as np #numpy 설치필요

# 전역 FPS 값 설정 (초기값: 25)
FPS = 60

# ESP32-CAM MJPEG 스트림 URL
mjpeg_url = 'http://172.30.1.83:81/stream'  # 여기에 ESP32-CAM IP 주소와 포트를 입력하세요.

# 현재 날짜와 시간으로 파일 이름을 생성합니다.
def get_new_filename():
    now = datetime.datetime.now()
    return now.strftime("%Y_%m_%d_%H_%M_%S.mp4")

# tkinter 윈도우를 생성합니다.
root = tk.Tk()
root.title("ESP32-CAM Stream")

# Label 위젯을 생성하여 비디오 프레임을 표시합니다.
label = Label(root)
label.pack()

# 체크박스를 생성합니다.
show_time_var = IntVar()
#체크를 했는지 안했는지를 show_time_var가 True인지 False인지로 알 수 있다!
show_time_checkbox = Checkbutton(root, text="현재시간추가", variable=show_time_var)
show_time_checkbox.pack()

# OpenCV를 사용하여 MJPEG 스트림을 가져옵니다.
cap = cv2.VideoCapture(mjpeg_url)

# 원본 해상도를 가져옵니다.
frame_width = int(cap.get(cv2.CAP_PROP_FRAME_WIDTH))
frame_height = int(cap.get(cv2.CAP_PROP_FRAME_HEIGHT))

# 비디오 파일 저장 관련 변수
fourcc = cv2.VideoWriter_fourcc(*'mp4v')  # mp4v 코덱을 사용하여 .mp4 파일 형식으로 저장
output_filename = get_new_filename()
out = cv2.VideoWriter(output_filename, fourcc, FPS, (frame_width, frame_height))
print(f"Saving video file: {output_filename}")

# 현재 시간을 기준으로 새 파일을 열어야 할 시간
start_time = time.time()

def update_frame():
    global out, start_time, output_filename

    ret, frame = cap.read() #ESP32가 보낸 영상을 읽는다!
    if ret:
        # OpenCV에서 읽은 이미지를 PIL 형식으로 변환합니다.
        frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
        img = Image.fromarray(frame_rgb)
        
        # 체크박스 상태에 따라 현재 시간을 오버레이합니다.
        if show_time_var.get():
            draw = ImageDraw.Draw(img)
            # 폰트 크기 설정 (3배 크게)
            font_size = 30  # 기본 폰트 크기보다 크게 설정
            try:
                font = ImageFont.truetype("arial.ttf", font_size)
            except IOError:
                font = ImageFont.load_default()  # 폰트 파일을 찾을 수 없는 경우 기본 폰트 사용
            now = datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
            text_bbox = draw.textbbox((0, 0), now, font=font)
            text_width = text_bbox[2] - text_bbox[0]
            text_height = text_bbox[3] - text_bbox[1]
            text_x = frame_width - text_width - 10
            text_y = frame_height - text_height - 15  # 텍스트를 5픽셀 위로 이동
            draw.text((text_x, text_y), now, font=font, fill=(255, 255, 255))
        
        imgtk = ImageTk.PhotoImage(image=img)
        
        # Label에 이미지를 업데이트합니다.
        label.imgtk = imgtk
        label.configure(image=imgtk)
        
        # 비디오 파일로 저장합니다.
        out.write(cv2.cvtColor(np.array(img), cv2.COLOR_RGB2BGR))
        
        # 1분마다 새로운 파일로 저장
        current_time = time.time()
        if current_time - start_time >= 60:
            out.release()  # 현재 파일 닫기
            print(f"Closing file: {output_filename}")
            output_filename = get_new_filename()  # 새로운 파일 이름 생성
            print(f"Saving new video file: {output_filename}")  # 새로운 파일 이름을 터미널에 출력
            out = cv2.VideoWriter(output_filename, fourcc, FPS, (frame_width, frame_height))
            start_time = time.time()  # 새로운 파일의 시작 시간 설정
    
    # GUI 업데이트를 위한 루프 재귀 호출
    root.after(int(1000 / FPS), update_frame)  # FPS에 맞춰 업데이트 주기 설정

# 프레임 업데이트 루프를 시작합니다.
update_frame()

# tkinter 이벤트 루프를 시작합니다.
root.mainloop()

# 비디오 파일 및 카메라 릴리스
cap.release()
out.release()
