# Silly code to check which ports are currently open (to show if, while and for usage)
import sys
import socket

if len(sys.argv) > 1:
    print("Using ports:" + sys.argv[1])
    ports = sys.argv[1].split(",")
else:
    ports = [80,443,3306,53,22,21,1194,5000,4200]
    print("Using ports:" + str(ports))

try_again = 1
while try_again :
    for port in ports :
        if socket.socket().connect_ex(("127.0.0.1", port)) == 0 :
            print("Port " + str(port) + " is open")
        else:
            print("Port " + str(port) + " is closed")
    user_input = input("Scan again? (Y/N): ")
    try_again = user_input == "y" or user_input == "Y"
    if try_again :
        print("\nRescanning...")
    else :
        print("\nExiting...")

