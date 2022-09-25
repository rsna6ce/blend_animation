@echo off
set MSBUILD_PATH=C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe
set SOLUTION_FILE=blend_animation.sln
set BUILD_CONFIG=release
set BUILD_TYPE=rebuild

set RESX1="blend_animation\Form1.resx"
if exist %RESX1% (
    copy %RESX1% "%RESX1%2"
    copy "%RESX1%2" "%RESX1%"
    del "%RESX1%2"
)
set RESX2="blend_animation\Properties\Resources.resx"
if exist %RESX2% (
    copy %RESX2% "%RESX2%2"
    copy "%RESX2%2" "%RESX2%"
    del "%RESX2%2"
)


if not exist "%MSBUILD_PATH%" (
    echo エラー:MSBuildが存在しません
    exit /b 0
)

if not exist "%SOLUTION_FILE%" (
    echo エラー:ソリューションファイルが存在しません
    exit /b 0
)

if not "%BUILD_CONFIG%"=="debug" if not "%BUILD_CONFIG%"=="release" (
    echo エラー:ビルド構成が不正です
    exit /b 0
)

if not "%BUILD_TYPE%"=="build" if not "%BUILD_TYPE%"=="rebuild" (
    echo エラー:ビルド種類が不正です
    exit /b 0
)

%MSBUILD_PATH% %SOLUTION_FILE% /p:Configuration=%BUILD_CONFIG% /t:%BUILD_TYPE% /m

@echo ビルドが完了しました
pause
exit 0
