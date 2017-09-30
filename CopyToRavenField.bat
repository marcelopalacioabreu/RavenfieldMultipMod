@echo off
echo Copying to %1...
echo Harmony...
REM Copy the harmony DLL to the game folder specified by command line
copy .\bin\Debug\0Harmony-Debug.dll %1\0Harmony.dll
echo Mod...
REM Copy the mod DLL to the game folder
copy .\bin\Debug\RFMultipMod.dll %1\RFMultipMod.dll
echo Done
pause