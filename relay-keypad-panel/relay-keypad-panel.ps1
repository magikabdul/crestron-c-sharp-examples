$file = "D:\crestron\c-sharp-crestron-examples\relay-keypad-panel\bin\Debug\relay_keypad_panel.cpz"
$ip = "192.168.1.85"
$slotNumber = 1
$username = "admin"
$password = "Cr@str0N"
Send-CrestronProgram -Device $ip -LocalFile $file -Password $password -ProgramSlot $slotNumber -Secure -Username $username