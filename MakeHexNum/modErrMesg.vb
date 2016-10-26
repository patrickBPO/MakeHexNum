Module modErrMesg
    Public Function ErrorMes(ByVal intMNum As Integer, ByVal MArg As String) As String
        If StrConv(MArg, vbUpperCase) = "F" Then

            Select Case intMNum
                Case 1
                    ErrorMes = "Unable To Create Key"

                Case 2
                    ErrorMes = "Unable to Connect to Internet"

                Case 3
                    ErrorMes = "Your Computer Is Not Registered"

                Case 4
                    ErrorMes = "Your Registration Is Expired"

                Case 5
                    ErrorMes = "There Was A Problem With The Online Update"

                Case 6
                    ErrorMes = "There Was A Problem Creating The Connection"

                Case Else
                    ErrorMes = "Unhandled ErrorMes() Argument Passed [" & intMNum & "]"
                    MsgBox(ErrorMes, vbExclamation, "Critical Error")

            End Select
        Else
            If StrConv(MArg, vbUpperCase) = "S" Then
                Select Case intMNum
                    Case 1
                        ErrorMes = "The Key Was Successfully Created"
                    Case 2
                        ErrorMes = "New Key Was Successfully Created"

                    Case Else
                        ErrorMes = "Unhandled ErrorMes() Argument Passed [" & intMNum & "]"
                        MsgBox(ErrorMes, vbExclamation, "Critical Error")

                End Select
            Else
                ErrorMes = ""
            End If
        End If
    End Function
End Module
