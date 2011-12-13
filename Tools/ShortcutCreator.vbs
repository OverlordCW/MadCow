' Copyright (C) 2011 Iker Ruiz Arnauda (Wesko)
'
' This program is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation; either version 2 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program; if not, write to the Free Software
' Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

' Desktop Mooege ShortCut Generator!
option explicit
'
' Routine to create "MadCow2011.lnk" on the Windows desktop.
'
sub CreateShortCut()
  dim objShell, strDesktopPath, objLink
  set objShell = CreateObject("WScript.Shell")
  strDesktopPath = objShell.SpecialFolders("Desktop")
  set objLink = objShell.CreateShortcut(strDesktopPath & "\MadCow.lnk")
  objLink.Description = "Shortcut to MadCow2011.exe"
  objLink.TargetPath = "MODIFY"
  objLink.WindowStyle = 1
  objLink.WorkingDirectory = "WESKO"
  objLink.Save
end sub

' Program starts running here.
call CreateShortCut()