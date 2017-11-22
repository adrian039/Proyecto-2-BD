@ECHO off
 
ECHO "Starting CrystalReports Installation" >> log.txt
msiexec.exe /I "netcompiler.exe" /qn
msiexec.exe /I "CRRuntime_64bit_13_0_21.msi" /qn
robocopy D:inetpubwwwroot E:sitesroot /S
ECHO "Completed CrystalReports Installation" >> log.txt