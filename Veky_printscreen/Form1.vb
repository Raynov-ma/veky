Imports System.IO
Imports System.Net.Mail
Public Class Form1
    Dim result As Integer
    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Int32) As Int16

    'Private Sub Form1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
    '    MsgBox(e.KeyChar)
    '    If e.KeyChar = Chr(2) Then
    '        Me.Hide()
    '    End If
    'End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'NotifyIcon1.ContextMenuStrip.Items.Add("Fermer")
        If System.IO.Directory.Exists("C:\veky") Then
            Try
                'Directory.GetFiles("C:\veky", "*.*", SearchOption.AllDirectories)
                'Dim monStreamWriter As StreamWriter
                'monStreamWriter = New StreamWriter("C:\Liste.txt")
                Dim dossiersTrouv = Directory.GetDirectories("C:\veky")
                For Each ligneD In dossiersTrouv
                    'monStreamWriter.WriteLine(ligneD)
                    Me.ComboBox1.Items.AddRange(New Object() {Path.GetFileName(ligneD)})
                Next
                'Dim fichiersTrouvé = Directory.GetFiles("C:\Documents and Settings\bl\Bureau", "*.*", SearchOption.AllDirectories)
                'For Each ligneF In fichiersTrouvé
                '    monStreamWriter.WriteLine(ligneF)
                'Next
                'monStreamWriter.Close()
                'Me.Close()
            Catch ex As Exception
                TextBox1.Text = ex.Message
            End Try
        Else
            System.IO.Directory.CreateDirectory("C:\veky")
        End If
        Me.Hide()
    End Sub

    'Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'Dim MyMailMessage As New MailMessage
    '    'MyMailMessage.From = New MailAddress(TextBox2.Text)
    '    'MyMailMessage.To.Add(TextBox2.Text)
    '    'MyMailMessage.Subject = ("Logs")
    '    'MyMailMessage.Body = (TextBox1.Text)
    '    'Dim SMTPServer As New SmtpClient("smtp.gmail.com")
    '    'SMTPServer.Port = 587
    '    'SMTPServer.Credentials = New System.Net.NetworkCredential(TextBox2.Text, TextBox3.Text)
    '    'SMTPServer.EnableSsl = True
    '    'SMTPServer.Send(MyMailMessage)
    'End Sub

    Private Sub Timer1_Tick_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        For i = 1 To 255
            result = 0
            result = GetAsyncKeyState(i)
            If result = -32767 Then
                If i = 44 Then
                    ' SaveScreen("button.gif")
                    Dim data As IDataObject = Clipboard.GetDataObject()

                    If data.GetDataPresent(GetType(System.Drawing.Bitmap)) Then

                        Dim bmp As Bitmap = CType(data.GetData(GetType(System.Drawing.Bitmap)), Bitmap)
                        PictureBox1.Image = bmp
                    End If
                    Me.Show()

                End If
                If i = 27 And Me.Visible Then
                    Me.Hide()
                End If
                ' Textbox1.Text = Textbox1.Text & i.ToString
            End If

        Next i

    End Sub

    Private Sub btnHide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bexit.Click
        Me.Hide()
    End Sub

    Public Function SaveScreen(ByVal theFile As String) As Boolean

        Try
            'SendKeys.Send("%{PRTSC}")          '<alt + printscreen>
            'Application.DoEvents()

            Dim data As IDataObject = Clipboard.GetDataObject()

            If data.GetDataPresent(GetType(System.Drawing.Bitmap)) Then

                Dim bmp As Bitmap = CType(data.GetData(GetType(System.Drawing.Bitmap)), Bitmap)
                PictureBox1.Image = bmp
                ' MsgBox("ok")
                bmp.Save(theFile, Imaging.ImageFormat.Png)
            End If
            Clipboard.SetDataObject(0)      'save memory by removing the image from the clipboard
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False

        End Try

    End Function

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub PictureBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick
        Form2.PictureBox1.Image = PictureBox1.Image
        Me.Enabled = False
        Form2.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim Inttxt As String
        Inttxt = InputBox("Entrer le nom du nouveau dossier : ", "Ajouter Dossier")
        Inttxt = Inttxt.Trim
        If Inttxt <> "" Then
            My.Computer.FileSystem.CreateDirectory("C:\veky\" & Inttxt)
            Me.ComboBox1.Items.AddRange(New Object() {Inttxt})
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox1.Text.Trim <> "" Then

            Dim namef As String
            'TextBox1.Text.Replace("\", " ")
            'TextBox1.Text.Replace("/", " ")
            'TextBox1.Text.Replace(":", " ")
            'TextBox1.Text.Replace("*", " ")
            'TextBox1.Text.Replace("?", " ")
            'TextBox1.Text.Replace("<", " ")
            'TextBox1.Text.Replace(">", " ")
            'TextBox1.Text.Replace("|", " ")
            namef = TextBox1.Text.Trim
            If TextBox1.Text.Trim = "" Then
                namef = DateTime.Now.Ticks.ToString
            End If
            MsgBox("C:\veky\" & ComboBox1.Text.Trim & "\" & namef & ".png")
            SaveScreen("C:\veky\" & ComboBox1.Text.Trim & "\" & namef & ".png")

            MsgBox("Enregistrement ok")
            Me.Hide()

        Else
            MsgBox("vous devez selectionner un Dossier")
        End If
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        If Me.Visible Then
            Me.Hide()
            clearmem()
        Else
            Me.Show()

        End If
    End Sub
    Public Function clearmem()

        Return 0
    End Function


End Class