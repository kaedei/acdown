ILMerge.exe /ndebug /target:winexe /targetplatform:v2 /attr:AcDown.exe /out:AcDownRelease.exe AcDown.exe AcDownCore.dll AcDownDownloader.dll AcDownInterface.dll AcDownParser.dll AcPlay.dll FlvCombine.dll
rename AcDown.exe AcDownOriginal.exe
rename AcDownRelease.exe AcDown.exe
7z.exe a -tzip AcDown.zip AcDown.exe -mx9
7z.exe a -tzip AcDownForLinux.zip AcDown.exe AcDown.sh -mx9
del Public /s /q
mkdir Public
copy AcDown.exe Public\AcDown.exe
copy AcDown.zip Public\AcDown.zip
copy AcDownForLinux.zip Public\AcDownForLinux.zip