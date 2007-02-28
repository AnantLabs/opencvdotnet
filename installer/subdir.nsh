# include file
# input: SUBFOLDER, LINK

# Defines
!define REGKEY "SOFTWARE\$(^Name)"
!define VERSION 0.7
!define COMPANY "Elad Ben-Israel"
!define URL http://code.google.com/p/opencvdotnet/
!define OPENCV "C:\Program Files\OpenCV\bin"
!define OPENCVERROR "OpenCV binaries must be installed under ${OPENCV}. OpenCV can be downloaded from http://opencvlibrary.sourceforge.net"
!define SETUP "opencvdotnet-${SUBFOLDER}-${VERSION}-setup.exe"
!define UNINSTALL "$INSTDIR\opencvdotnet-${SUBFOLDER}-uninstall.exe"

# MUI defines
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\win-install.ico"
!define MUI_FINISHPAGE_NOAUTOCLOSE
!define MUI_STARTMENUPAGE_REGISTRY_ROOT HKLM
!define MUI_STARTMENUPAGE_REGISTRY_KEY ${REGKEY}
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME StartMenuGroup
!define MUI_STARTMENUPAGE_DEFAULTFOLDER OpenCVDotNet
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\win-uninstall.ico"
!define MUI_UNFINISHPAGE_NOAUTOCLOSE

# Included files
!include Sections.nsh
!include MUI.nsh

# Variables
Var StartMenuGroup

# Installer pages
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE gpl.txt
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_STARTMENU Application $StartMenuGroup
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

# Installer languages
!insertmacro MUI_LANGUAGE English

# Installer attributes
OutFile ${SETUP}
InstallDir $PROGRAMFILES\OpenCVDotNet
CRCCheck on
XPStyle on
ShowInstDetails show
VIProductVersion 0.5.0.0
VIAddVersionKey ProductName OpenCVDotNet
VIAddVersionKey ProductVersion "${VERSION}"
VIAddVersionKey CompanyName "${COMPANY}"
VIAddVersionKey CompanyWebsite "${URL}"
VIAddVersionKey FileVersion ""
VIAddVersionKey FileDescription ""
VIAddVersionKey LegalCopyright ""
InstallDirRegKey HKLM "${REGKEY}" Path
ShowUninstDetails show

# Installer sections
!macro CHECK_EXIST PATH MSG
    DetailPrint "Verifying existence of '${PATH}'..."
    IfFileExists "${PATH}" +3
        MessageBox MB_OK|MB_ICONSTOP "${MSG}"
        Abort "Unable to find ${PATH}."
!macroend    

Section -Main SEC0000
	!insertmacro CHECK_EXIST $INSTDIR\OpenCVDotNet.dll "OpenCVDotNet must be installed"
	!insertmacro CHECK_EXIST $INSTDIR\OpenCVDotNet.UI.dll "OpenCVDotNet must be installed"
	!insertmacro CHECK_EXIST $INSTDIR\OpenCVDotNet.Algs.dll "OpenCVDotNet must be installed"
	
    SetOutPath $INSTDIR\${SUBFOLDER}
    SetOverwrite on
    File /nonfatal /r ..\${SUBFOLDER}\*.cs ..\${SUBFOLDER}\*.resx
    File /nonfatal /r ..\${SUBFOLDER}\*.avi ..\${SUBFOLDER}\*.c ..\${SUBFOLDER}\*.cpp ..\${SUBFOLDER}\*.h
    File /nonfatal /r ..\${SUBFOLDER}\*.csproj ..\${SUBFOLDER}\*.vcproj ..\${SUBFOLDER}\*.sln
    
    WriteRegStr HKLM "${REGKEY}\Components" Main 1    
SectionEnd

Section -post SEC0001
    WriteRegStr HKLM "${REGKEY}" Path $INSTDIR
    SetOutPath $INSTDIR
    WriteUninstaller ${UNINSTALL}
    !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
    SetOutPath $SMPROGRAMS\$StartMenuGroup
    CreateShortcut "$SMPROGRAMS\$StartMenuGroup\Uninstall $(^Name).lnk" ${UNINSTALL}
    CreateShortcut "$SMPROGRAMS\$StartMenuGroup\${SUBFOLDER}.lnk" "${LINK}"
    !insertmacro MUI_STARTMENU_WRITE_END
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayName "$(^Name)"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayVersion "${VERSION}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" Publisher "${COMPANY}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" URLInfoAbout "${URL}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayIcon ${UNINSTALL}
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" UninstallString ${UNINSTALL}
    WriteRegDWORD HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" NoModify 1
    WriteRegDWORD HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" NoRepair 1
SectionEnd

# Macro for selecting uninstaller sections
!macro SELECT_UNSECTION SECTION_NAME UNSECTION_ID
    Push $R0
    ReadRegStr $R0 HKLM "${REGKEY}\Components" "${SECTION_NAME}"
    StrCmp $R0 1 0 next${UNSECTION_ID}
    !insertmacro SelectSection "${UNSECTION_ID}"
    GoTo done${UNSECTION_ID}
next${UNSECTION_ID}:
    !insertmacro UnselectSection "${UNSECTION_ID}"
done${UNSECTION_ID}:
    Pop $R0
!macroend

# Uninstaller sections
Section /o un.Main UNSEC0000
    Delete $INSTDIR\${SUBFOLDER}\*.*
	Delete "$SMPROGRAMS\$StartMenuGroup\Uninstall $(^Name).lnk"
    Delete "$SMPROGRAMS\$StartMenuGroup\${SUBFOLDER}.lnk"
    DeleteRegValue HKLM "${REGKEY}\Components" Main
SectionEnd

Section un.post UNSEC0001
    DeleteRegKey HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)"
    DeleteRegKey HKLM "${REGKEY}"
    RmDir $INSTDIR\${SUBFOLDER}
    Push $R0
    StrCpy $R0 $StartMenuGroup 1
    StrCmp $R0 ">" no_smgroup
no_smgroup:
    Pop $R0
SectionEnd

# Installer functions
Function .onInit
    InitPluginsDir
FunctionEnd

# Uninstaller functions
Function un.onInit
    ReadRegStr $INSTDIR HKLM "${REGKEY}" Path
    !insertmacro MUI_STARTMENU_GETFOLDER Application $StartMenuGroup
    !insertmacro SELECT_UNSECTION Main ${UNSEC0000}
FunctionEnd

