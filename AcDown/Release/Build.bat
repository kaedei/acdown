del Public\AcDown.exe /s /q
del Public\AcDown.zip /s /q
del Public\AcDownForLinux.zip /s /q
ILMerge.exe /ndebug /log:ILMerge.log /target:winexe /targetplatform:v2 /attr:AcDown.exe /out:Public\AcDown.exe AcDown.exe AcDownCore.dll AcDownDownloader.dll AcDownInterface.dll AcDownParser.dll AcPlay.dll FlvCombine.dll
cd Public
7z.exe a -tzip AcDown.zip AcDown.exe -mx9
7z.exe a -tzip AcDownForLinux.zip AcDown.exe AcDown.sh -mx9