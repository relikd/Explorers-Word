!include "MUI.nsh"
!define VERSION "Version1.4"

Name "ExplorersWord_${VERSION}"
OutFile "ExplorersWord_${VERSION}.exe"
InstallDir $ProgramFiles\ExplorersWord



!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES

!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

!insertmacro MUI_LANGUAGE "German"

section ""
 SetOutPath $INSTDIR
 WriteUninstaller $INSTDIR\uninstallExplorersWord_${VERSION}.exe
 File /nonfatal /a /r "ExplorersWord_${VERSION}_Data"
 File "ExplorersWord_${VERSION}.exe" 
SectionEnd

Section "uninstall"
 SetOutPath "$TEMP"
 Delete $INSTDIR\uninstallExplorersWord_${VERSION}.exe
 RMDir /r "$INSTDIR\ExplorersWord_${VERSION}_Data"
 Delete $INSTDIR\ExplorersWord_${VERSION}.exe 
 RMDIR $INSTDIR
SectionEnd



