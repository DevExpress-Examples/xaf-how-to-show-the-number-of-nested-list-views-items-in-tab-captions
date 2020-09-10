;#DropDB ListViewCountInTabsEasyTest

#Application ListViewCountInTabsWin
#Application ListViewCountInTabsWeb
#IfDef ListViewCountInTabsWin

*Action About...
 
*Action OK

#EndIf

#IfDef ListViewCountInTabsWeb
!Action Navigation(My Details)
#EndIf
