<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.txtSNum = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtHNum = New System.Windows.Forms.TextBox()
        Me.LNum = New System.Windows.Forms.Label()
        Me.HNum = New System.Windows.Forms.Label()
        Me.txtHKey = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtSNum
        '
        Me.txtSNum.Location = New System.Drawing.Point(13, 13)
        Me.txtSNum.Name = "txtSNum"
        Me.txtSNum.Size = New System.Drawing.Size(290, 20)
        Me.txtSNum.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 89)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Convert"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtHNum
        '
        Me.txtHNum.Location = New System.Drawing.Point(12, 39)
        Me.txtHNum.Name = "txtHNum"
        Me.txtHNum.Size = New System.Drawing.Size(290, 20)
        Me.txtHNum.TabIndex = 2
        '
        'LNum
        '
        Me.LNum.AutoSize = True
        Me.LNum.Location = New System.Drawing.Point(321, 16)
        Me.LNum.Name = "LNum"
        Me.LNum.Size = New System.Drawing.Size(53, 13)
        Me.LNum.TabIndex = 3
        Me.LNum.Text = "Hardware"
        '
        'HNum
        '
        Me.HNum.AutoSize = True
        Me.HNum.Location = New System.Drawing.Point(321, 39)
        Me.HNum.Name = "HNum"
        Me.HNum.Size = New System.Drawing.Size(25, 13)
        Me.HNum.TabIndex = 4
        Me.HNum.Text = "Key"
        '
        'txtHKey
        '
        Me.txtHKey.Location = New System.Drawing.Point(12, 63)
        Me.txtHKey.Name = "txtHKey"
        Me.txtHKey.Size = New System.Drawing.Size(290, 20)
        Me.txtHKey.TabIndex = 5
        Me.txtHKey.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(321, 67)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Hex Key"
        Me.Label1.Visible = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(398, 123)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtHKey)
        Me.Controls.Add(Me.HNum)
        Me.Controls.Add(Me.LNum)
        Me.Controls.Add(Me.txtHNum)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtSNum)
        Me.Name = "Form1"
        Me.Text = "Generate MD5 Reg Key"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtSNum As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents txtHNum As TextBox
    Friend WithEvents LNum As Label
    Friend WithEvents HNum As Label
    Friend WithEvents txtHKey As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents DbregDS As dbregDS
    Friend WithEvents SubscriberBindingSource As BindingSource
    Friend WithEvents SubscriberTableAdapter As dbregDSTableAdapters.subscriberTableAdapter
    Friend WithEvents TableAdapterManager As dbregDSTableAdapters.TableAdapterManager
    Friend WithEvents BindingNavigator1 As BindingNavigator
    Friend WithEvents BindingNavigatorAddNewItem As ToolStripButton
    Friend WithEvents BindingNavigatorCountItem As ToolStripLabel
    Friend WithEvents BindingNavigatorDeleteItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveFirstItem As ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As ToolStripSeparator
End Class
