@echo off
setlocal
set here=%~dp0

mkdir mstest
set targetfolder=%here%mstest

set programs=%programfiles%
if exist "%programfiles(x86)%" set programs=%programfiles(x86)%
set vs2013="%programs%\Microsoft Visual Studio 12.0"
set gac2="%windir%"\Microsoft.NET\assembly
set gac1="%windir%"\assembly

echo === Copying from Visual Studio 2013 install folder...
copy %vs2013%\Common7\IDE\mstest* "%targetfolder%"
copy %vs2013%\Common7\IDE\PrivateAssemblies\Microsoft.VisualStudio.Quality* "%targetfolder%"

echo === Copying from %gac1%...
pushd "%gac1%"
dir /s /b *.dll | findstr QualityTools | findstr 12.0.0.0 > %here%tmp.filelist
popd
for /F "tokens=*" %%f in (tmp.filelist) DO copy "%%f" "%targetfolder%"

echo === Copying from %gac2%...
pushd "%gac2%"
dir /s /b *.dll | findstr QualityTools | findstr 12.0.0.0 > %here%tmp.filelist
popd
for /F "tokens=*" %%f in (tmp.filelist) DO copy "%%f" "%targetfolder%"

del tmp.filelist

echo === Done. Check output for errors!
exit /b 0