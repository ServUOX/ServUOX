@SET CURPATH=%~dp0
@SET CSCPATH=%CURPATH%bin\roslyn\

@SET SRVPATH=%CURPATH%Server\

@SET EXENAME=ServUOX

@TITLE: %EXENAME% - https://www.servuox.com

::##########

@ECHO:
@ECHO: Compile %EXENAME% for Windows
@ECHO:

@PAUSE

@DEL "%CURPATH%%EXENAME%.exe"

@ECHO ON

"%CSCPATH%csc.exe" /win32icon:"%SRVPATH%servuox.ico" /r:"%CURPATH%Microsoft.CodeDom.Providers.DotNetCompilerPlatform.dll" /target:exe /out:"%CURPATH%%EXENAME%.exe" /recurse:"%SRVPATH%*.cs" /d:ServUOX /d:NEWTIMERS /d:NETFX_472 /nowarn:0618 /nologo /unsafe /optimize

@ECHO OFF

@ECHO:
@ECHO: Done!
@ECHO:

@PAUSE

@CLS

::##########

@ECHO:
@ECHO: Ready To Run!
@ECHO:

@PAUSE

@CLS

@ECHO OFF

"%CURPATH%%EXENAME%.exe"
