$file = "D:\crestron\c-sharp-crestron-examples\basic-XPanel\basic-XPanel\bin\Debug\basic_XPanel.cpz"
$ip = "192.168.1.85"
$slotNumber = 1
$username = "admin"
$password = "Cr@str0N"
Send-CrestronProgram -Device $ip -LocalFile $file -Password $password -ProgramSlot $slotNumber -Secure -Username $username