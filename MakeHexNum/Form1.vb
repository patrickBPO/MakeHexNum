Imports MakeHexNum.GlobalVar
Imports ADODB
Imports ADODB.CursorTypeEnum
Imports ADODB.LockTypeEnum
Public Class Form1


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        glbHWID = MakeHWID()
        If Not CreateRegODBC() Then
            glbErrMesg = modErrMesg.ErrorMes(6, "F")
            MsgBox(glbErrMesg, vbCritical, "Connection Creation Failed")
        Else
            If Not ChkConnection() Then
                glbErrMesg = modErrMesg.ErrorMes(2, "F")
                MsgBox(glbErrMesg, vbCritical, "Connection Failed")
                If Not CreateRegsKey(glbTRem, glbUnit) Then
                    glbErrMesg = modErrMesg.ErrorMes(1, "F")
                    MsgBox(glbErrMesg, vbCritical, "Key Creation Fail")
                Else
                    glbErrMesg = modErrMesg.ErrorMes(1, "S")
                    MsgBox(glbErrMesg, vbCritical, "Key Creation Success")
                End If
            Else
                If Not IsValidHdId() Then
                    glbErrMesg = modErrMesg.ErrorMes(3, "F")
                    MsgBox(glbErrMesg, vbCritical, "Not Registered")
                    If Not CreateRegsKey(30, glbUnit) Then
                        glbErrMesg = modErrMesg.ErrorMes(1, "F")
                        MsgBox(glbErrMesg, vbCritical, "Key Creation Fail")
                    Else
                        glbErrMesg = modErrMesg.ErrorMes(2, "S")
                        MsgBox(glbErrMesg, vbCritical, "Key Creation Success")
                    End If
                    'Me.Close()
                Else
                    If Not IsActive(Today) Then
                        glbErrMesg = modErrMesg.ErrorMes(4, "F")
                        MsgBox(glbErrMesg, vbCritical, "Registration Expired")
                    Else
                        If Not UpdateByRemote() Then
                            glbErrMesg = modErrMesg.ErrorMes(5, "F")
                            MsgBox(glbErrMesg, vbCritical, "Online Update Error")
                            '- Attempt to update locally
                            If Not CreateRegsKey(glbTRem, glbUnit) Then
                                glbErrMesg = modErrMesg.ErrorMes(1, "F")
                                MsgBox(glbErrMesg, vbCritical, "Key Creation Fail")
                            End If
                        Else
                            'return true
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Function IsValidHdId() As Boolean
        Dim myConnL As ADODB.Connection
        Dim rsCheck As ADODB.Recordset
        Dim CheckStr As String


        myConnL = New ADODB.Connection
        myConnL.CursorLocation = CursorLocationEnum.adUseClient '-- Prepare to examine the cursor location and contents ReadOnly
        myConnL.Open(glbODBC)

        CheckStr = ""
        CheckStr = " SELECT s.* " &
                    " FROM subscriber s " &
                    " WHERE s.hardware_no = '" & glbCPU & "' "

        Debug.Print(CheckStr)
        rsCheck = New ADODB.Recordset

        rsCheck.Open(CheckStr, myConnL, adOpenDynamic, adLockReadOnly)

        With rsCheck
            If Not .BOF And Not .EOF Then

                If .RecordCount > 0 Then
                    Return True
                Else
                    Return False
                End If

            End If
            .Close()
        End With

    End Function

    Private Function IsActive(ByVal Tdate As Date) As Boolean
        Dim myConnL As ADODB.Connection
        Dim rsCheck As ADODB.Recordset
        Dim CheckStr As String

        'glbHWID = MakeHWID()

        myConnL = New ADODB.Connection
        myConnL.CursorLocation = CursorLocationEnum.adUseClient '-- Prepare to examine the cursor location and contents ReadOnly
        myConnL.Open(glbODBC)

        CheckStr = ""
        CheckStr = " SELECT s.* " &
                    " FROM subscriber s " &
                    " WHERE s.hardware_no = '" & glbCPU & "' " &
                    " AND s.edate >= " & Tdate

        Debug.Print(CheckStr)
        rsCheck = New ADODB.Recordset

        rsCheck.Open(CheckStr, myConnL, adOpenDynamic, adLockReadOnly)

        With rsCheck
            If Not .BOF And Not .EOF Then

                If .RecordCount > 0 Then
                    Return True
                Else
                    Return False
                End If

            End If
            .Close()
        End With
    End Function

    Private Function UpdateByRemote() As Boolean
        Dim myConnL As ADODB.Connection
        Dim rsCheck As ADODB.Recordset
        Dim CheckStr As String

        'glbHWID = MakeHWID()

        myConnL = New ADODB.Connection
        myConnL.CursorLocation = CursorLocationEnum.adUseClient '-- Prepare to examine the cursor location and contents ReadOnly
        myConnL.Open(glbODBC)

        CheckStr = ""
        CheckStr = " SELECT s.* " &
                    " FROM subscriber s " &
                    " WHERE s.hardware_no = '" & glbCPU & "' "

        Debug.Print(CheckStr)
        rsCheck = New ADODB.Recordset

        rsCheck.Open(CheckStr, myConnL, adOpenDynamic, adLockReadOnly)

        With rsCheck
            If Not .BOF And Not .EOF Then
                'not necessary but good practice
                .MoveLast()
                .MoveFirst()

                While (Not .EOF)
                    glbBdate = rsCheck.Fields("sdate").Value
                    glbEdate = rsCheck.Fields("edate").Value
                    glbUnit = rsCheck.Fields("unit").Value
                    glbRenMesg = rsCheck.Fields("renew_msg").Value
                    glbTRem = rsCheck.Fields("time_remain").Value
                    glbPaid = rsCheck.Fields("paid_yn").Value
                    .MoveNext()
                End While

                If .RecordCount > 0 Then
                    If Not CreateRegsKey(glbTRem, glbUnit) Then
                        glbErrMesg = modErrMesg.ErrorMes(1, "F")
                        MsgBox(glbErrMesg, vbCritical, "Key Creation Fail")
                        Return False
                    Else
                        Return True
                    End If
                Else
                    Return False
                End If

            End If
            .Close()
        End With
    End Function

    Private Function ChkConnection() As Boolean
        Dim chkConn As New IConnection

        If chkConn.IsConnectionAvailable = True Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function MakeHWID() As String
        Dim hw As New clsComputerInfo

        Dim hdd As String
        Dim cpu As String
        Dim mb As String
        Dim mac As String

        cpu = hw.GetProcessorId()
        glbCPU = cpu
        hdd = hw.GetVolumeSerial("C")
        mb = hw.GetMotherBoardID()
        mac = hw.GetMACAddress()
        'MsgBox(cpu & "=cpu   " & hdd & "=hdd   " & mb & "=mb   " & mac & "=mac")

        Dim hwidid As String = Strings.UCase(hw.getMD5Hash(cpu & hdd & mb & mac)) ' MessageBox.Show(Strings.UCase(hwid))
        txtSNum.Text = cpu & "-" & hdd & "-" & mb & "-" & mac
        txtHNum.Text = hwidid
        'txtHKey.Text = Strings.UCase(hw.getMD5Hash(cpu)) & "-" &
        '               Strings.UCase(hw.getMD5Hash(hdd)) & "-" &
        '               Strings.UCase(hw.getMD5Hash(mb)) & "-" &
        '               Strings.UCase(hw.getMD5Hash(mac))

        MakeHWID = hwidid

    End Function

    Private Function UpdateRegsKey(ByVal tRemain As Integer, ByVal tUnit As String) As Boolean
        Dim hwid As String

        hwid = MakeHWID()

        If Not CreateRegKyVal(glbCPU, hwid, tRemain, tUnit, glbBdate, glbEdate, glbPaid) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function CreateRegsKey(ByVal tRemain As Integer, ByVal tUnit As String) As Boolean
        Dim hwid As String

        hwid = MakeHWID()

        If Not CreateRegKyVal(glbCPU, hwid, tRemain, tUnit, glbBdate, glbEdate, glbPaid) Then
            Return False
        Else
            Return True
        End If
    End Function
    '* --Note...to manually create ODBC datasources you have to create Registry Values in:
    '   \HKEY_CURRENT_USER\Software\ODBC\ODBC.INI\ODBC Data Sources
    '      - "ITS" Value = (MySQL ODBC 3.51 Driver)
    '   \HKEY_CURRENT_USER\Software\ODBC\ODBC.INI\ITS
    '      - DATABASE Value = (dbkolbe)
    '      - Driver Value = (C:\Windows\SysWOW64\myodbc3.dll) or 32Bit="(C:\Windows\system32\myodbc3.dll)"
    '      - PORT Value = (3308)
    '      - PWD Value = (K0lb3@xs4db)
    '      - SERVER Value = (web) or (192.168.2.14)
    '      - UID Value = (kolbe)
    Private Function CreateRegODBC() As Boolean
        Dim SubKyName As String
        Dim SubKyName1 As String
        'Dim kyVal As String
        Dim dbValNme As String
        Dim drvValNme As String
        Dim portValNme As String
        Dim pwdValNme As String
        Dim servValNme As String
        Dim uidValNme As String
        Dim odbcValNme As String
        Dim dbVal As String
        Dim drvVal As String
        Dim portVal As String
        Dim pwdVal As String
        Dim servVal As String
        Dim uidVal As String
        Dim odbcVal As String
        Dim regVersion As Microsoft.Win32.RegistryKey
        Dim regKey As Microsoft.Win32.RegistryKey

        'Dim getVal As String

        'SubKyName = "HKEY_CURRENT_USER\Software\Reg\" & cpuId
        SubKyName = "HKEY_CURRENT_USER\Software\ODBC\ODBC.INI\MYSQL_REG"
        SubKyName1 = "HKEY_CURRENT_USER\Software\ODBC\ODBC.INI\ODBC Data Sources"
        dbValNme = "DATABASE"
        drvValNme = "Driver"
        portValNme = "PORT"
        pwdValNme = "PWD"
        servValNme = "SERVER"
        uidValNme = "UID"
        odbcValNme = "MYSQL_REG"
        dbVal = "dbreg"
        drvVal = "C:\Windows\system32\myodbc3.dll"
        portVal = "3308"
        pwdVal = "password"
        servVal = "25.79.108.33"
        uidVal = "fooUser"
        odbcVal = "MySQL ODBC 3.51 Driver"

        Try
            'regVersion =
            'Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Reg", True)
            regVersion =
                Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(SubKyName, True)
            If regVersion Is Nothing Then
                'My.Computer.Registry.CurrentUser.CreateSubKey("Software\Reg\" & cpuId)
                My.Computer.Registry.CurrentUser.CreateSubKey(SubKyName)
                My.Computer.Registry.SetValue(SubKyName, dbValNme, dbVal)
                My.Computer.Registry.SetValue(SubKyName, drvValNme, drvVal)
                My.Computer.Registry.SetValue(SubKyName, portValNme, portVal)
                My.Computer.Registry.SetValue(SubKyName, pwdValNme, pwdVal)
                My.Computer.Registry.SetValue(SubKyName, servValNme, servVal)
                My.Computer.Registry.SetValue(SubKyName, uidValNme, uidVal)
                'My.Computer.Registry.CurrentUser.CreateSubKey(SubKyName1)
                regKey =
                    Microsoft.Win32.Registry.CurrentUser.OpenSubKey(SubKyName1, True)
                If regKey Is Nothing Then
                    Return True
                Else
                    My.Computer.Registry.SetValue(SubKyName1, odbcValNme, odbcVal)
                    glbODBC = odbcValNme
                    regKey.Close()
                    Return True
                End If
                regVersion.Close()
            Else
                Return True
            End If
        Catch
            Return False
        End Try

    End Function

    '{-- Create Registry Key Based on the CPU ID and Add a SubKey with 
    '    a value based on the created Composite Hardware ID
    Private Function CreateRegKyVal(ByVal cpuId As String,
                                    ByVal hwidId As String,
                                    ByVal atime As Integer, '-Subscription Time Remain
                                    ByVal tunit As String, '-Time Unit ("D","M","Y") Day, Month or Year
                                    ByVal bdate As String, '-Begin Date Subscription
                                    ByVal edate As String, '-End Date Subscription
                                    ByVal paid As Integer '-Paid Y/N
                                    ) As Boolean
        Dim SubKyName As String
        Dim KyValName As String
        Dim kyVal As String
        Dim atimeValNme As String
        Dim tunitValNme As String
        Dim bdateValNme As String
        Dim edateValNme As String
        Dim paidValNme As String
        Dim regVersion As Microsoft.Win32.RegistryKey
        Dim regKey As Microsoft.Win32.RegistryKey
        Dim integ As Integer

        'Dim getVal As String

        'SubKyName = "HKEY_CURRENT_USER\Software\Reg\" & cpuId
        SubKyName = "HKEY_CLASSES_ROOT\Installer\Products\" & hwidId
        KyValName = cpuId & "Val"
        'kyVal = hwidId
        kyVal = cpuId
        atimeValNme = "Clock"
        tunitValNme = "Unit"
        bdateValNme = "1dte"
        edateValNme = "2dte"
        paidValNme = "Paid"

        Try
            'regVersion =
            'Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Reg", True)
            regVersion =
                Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("Installer\Products\" & hwidId, True)
            If regVersion Is Nothing Then
                'My.Computer.Registry.CurrentUser.CreateSubKey("Software\Reg\" & cpuId)
                My.Computer.Registry.ClassesRoot.CreateSubKey("Installer\Products\" & hwidId)
                My.Computer.Registry.SetValue(SubKyName, KyValName, kyVal)
                My.Computer.Registry.SetValue(SubKyName, atimeValNme, atime)
                My.Computer.Registry.SetValue(SubKyName, tunitValNme, tunit)
                My.Computer.Registry.SetValue(SubKyName, bdateValNme, bdate)
                My.Computer.Registry.SetValue(SubKyName, edateValNme, edate)
                My.Computer.Registry.SetValue(SubKyName, paidValNme, paid)
                Return True
                'regVersion.Close()
            Else
                ' - Routine to Update values
                regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("Installer\Products\" & hwidId, True)
                If regKey Is Nothing Then
                    'regVersion.CreateSubKey(hwidId)
                    My.Computer.Registry.ClassesRoot.CreateSubKey("Installer\Products\" & hwidId)
                    My.Computer.Registry.SetValue(SubKyName, KyValName, kyVal)
                    My.Computer.Registry.SetValue(SubKyName, atimeValNme, atime)
                    My.Computer.Registry.SetValue(SubKyName, tunitValNme, tunit)
                    My.Computer.Registry.SetValue(SubKyName, bdateValNme, bdate)
                    My.Computer.Registry.SetValue(SubKyName, edateValNme, edate)
                    My.Computer.Registry.SetValue(SubKyName, paidValNme, paid)
                Else
                    ' 1. Delete SubKey
                    My.Computer.Registry.ClassesRoot.DeleteSubKey("Installer\Products\" & hwidId, True)
                    ' 2 Recreate SubKey
                    My.Computer.Registry.ClassesRoot.CreateSubKey("Installer\Products\" & hwidId)
                    My.Computer.Registry.SetValue(SubKyName, KyValName, kyVal)
                    My.Computer.Registry.SetValue(SubKyName, atimeValNme, atime)
                    My.Computer.Registry.SetValue(SubKyName, tunitValNme, tunit)
                    My.Computer.Registry.SetValue(SubKyName, bdateValNme, bdate)
                    My.Computer.Registry.SetValue(SubKyName, edateValNme, edate)
                    My.Computer.Registry.SetValue(SubKyName, paidValNme, paid)
                    'integ = My.Computer.Registry.ClassesRoot.GetValue(SubKyName, paidValNme, Nothing)
                    'MsgBox("PaidVal->" & integ)
                    regKey.Close()
                End If

                regVersion.Close()
            End If
            'getVal = My.Computer.Registry.GetValue(SubKyName, atimeValNme, "NONE")
            'MsgBox(getVal)
            CreateRegKyVal = True
        Catch
            CreateRegKyVal = False
        End Try

    End Function

    Private Sub SubscriberBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs)
        Me.Validate()
        Me.SubscriberBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.DbregDS)

    End Sub

    'Private Function AddSubScriber()

    'End Function
End Class
