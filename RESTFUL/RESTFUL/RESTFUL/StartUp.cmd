﻿@ECHO off
 
ECHO "Starting CrystalReports Installation" >> log.txt
msiexec.exe /I "CRRuntime_64bit_13_0_21.msi" /qn
ECHO "Completed CrystalReports Installation" >> log.txt