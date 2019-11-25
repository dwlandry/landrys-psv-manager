;#DropDB landrys-psv-managerEasyTest

#Application landrys-psv-managerWin
#Application landrys-psv-managerWeb

*FillForm
 User Name = Admin
*Action Log In

#IfDef landrys-psv-managerWin

*Action About...
 
*Action OK

#EndIf

#IfDef landrys-psv-managerWeb
*Action Navigation(My Details)
#EndIf
