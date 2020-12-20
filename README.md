# Solidworks Open Log Page
Open Source Log Page for Solidworks

## Table of contents
* [Setup](#setup)
* [VBA Example](#vbaExample)


## Setup
1. Download/clone files
2. Add Solidworks references either from [AngelSix](https://github.com/angelsix/solidworks-api/tree/develop/References) or "C:\Program Files\SOLIDWORKS Corp\SOLIDWORKS\api\redist"
3. Build in VS
4. Register swOLD.dll using Regasm or [AngelSix's Addin Installer](https://github.com/angelsix/solidworks-api/tree/develop/Tools/Addin%20Installer)
5. Launch Solidworks and try example below

## VBA Example
```

'This example shows how to print messages to Open Log Page

'------------------------------------------------------------------------
' Preconditions:
' 1. Build swOLD, and register .dll using Regasm (or AngelSix Installer)
' 2. Launch Solidworks
' 3. Open the OLP Taskpane
'
' Postconditions: Inspect the OLD Taskpane
'-----------------------------------------------------------------------
'Hunter Toth - December 2020 - https://github.com/huntert98/swOLP


Dim swApp As SldWorks.SldWorks
Sub main()
    'Get Solidoworks App
    Set swApp = Application.SldWorks
    
    'Get OpenLogPage App
    Dim logPage As Object
    Set logPage = swApp.GetAddInObject("SolidWorks.OpenLogPage")
    
    'Simple print, default prefix is "[MSG]" and color is Green
    logPage.write "Hello World!"
    
    'Print with custom prefix, default color is Green"
    logPage.write "You can add custom prefixes", "[CUST]"
    
    'print with custom color and prefix, Color is an ARGB byte array
    Dim byt(3) As Byte
    byt(0) = 255
    byt(1) = 255
    byt(2) = 0
    byt(3) = 0
    logPage.write "You can add custom colors", "[ERR]", byt
End Sub
```
