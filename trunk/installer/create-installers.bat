@ECHO OFF
SET NSIS="C:\Program Files\NSIS\makensis.exe" /V2
%NSIS% opencvdotnet.nsi
%NSIS% opencvdotnet-examples.nsi
%NSIS% opencvdotnet-invert-example.nsi
PAUSE