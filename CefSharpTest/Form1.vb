Imports System.IO
Imports CefSharp
Imports CefSharp.WinForms

Public Class Form1
    Private WithEvents browser As ChromiumWebBrowser
    Dim currentURL As String 'URL / path of content being displayed at the time
    Dim counter1 As Integer = 0 'count of document number that is being displayed
    Dim counter2 As Integer = 0 'number of rotations through the entire presentation
    Dim fileCount As Integer = 1 'count of files in array that will be cycled through
    Dim dir As String = My.Application.Info.DirectoryPath 'get path where application is being launched from
    Dim currDir As New DirectoryInfo(dir)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        Dim settings As New CefSettings()
        CefSharp.Cef.Initialize(settings)

        browser = New ChromiumWebBrowser("C:\test\test.pdf")

        panBrowser.Controls.Add(browser)
        Me.TopMost = True
        Me.FormBorderStyle = 0
        Me.Size = SystemInformation.PrimaryMonitorSize
        Me.WindowState = 2

        'Cursor.Hide()
        browser.Dock = DockStyle.Fill


        Timer1 = New System.Windows.Forms.Timer()
        Timer1.Interval = 1
        'Timer1.Start()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Navigate(ByVal address As String)
        Dim open As New OpenFileDialog

        'navigate to the content passed through


        If String.IsNullOrEmpty(address) Then Return
            If address.Equals("about:blank") Then Return
            If Not address.StartsWith("http://") And
                Not address.StartsWith("https://") Then
                address = "http://" & address
            End If
        Try
            browser.Load(address)
        Catch ex As System.UriFormatException
            Return
        End Try

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Interval = 20000 'change timer to wait 20 seconds per page

        Dim listOfFiles As New ArrayList

        Dim fiArr As FileInfo() = currDir.GetFiles()


        'scan for files of various types and put into their own arrays
        Dim PDFs() As FileInfo = New IO.DirectoryInfo(Dir).GetFiles("*.pdf", IO.SearchOption.AllDirectories)
        Dim URLs() As FileInfo = New IO.DirectoryInfo(Dir).GetFiles("*.htm", IO.SearchOption.AllDirectories)
        Dim XLSXs() As FileInfo = New IO.DirectoryInfo(Dir).GetFiles("*.xlsx", IO.SearchOption.AllDirectories)
        Dim XLSs() As FileInfo = New IO.DirectoryInfo(Dir).GetFiles("*.xls", IO.SearchOption.AllDirectories)

        'compile all previous arrays into one "master" array
        For Each n In PDFs
            listOfFiles.Add(n)
        Next

        For Each m In URLs
            listOfFiles.Add(m)
        Next

        For Each m In XLSs
            listOfFiles.Add(m)
        Next

        For Each m In XLSXs
            listOfFiles.Add(m)
        Next

        'count number of objects in the listOfFiles array
        fileCount = listOfFiles.Count

        'if there are no files in the array, terminate the application and run the "logoff.bat" file
        If fileCount = 0 Then
            'System.Diagnostics.Process.Start(dir.ToString & "\logoff.bat")
            Me.Close()
        End If


        Try

            listOfFiles.Sort(New ObjectComparer())

            'determine the "link" in the array
            Dim link As String = IO.Path.Combine(dir, CType(listOfFiles(counter1).FullName, String))


            'navigate to the link and incriment for the next counter
            browser.Load(link)
            counter1 += 1
            'If the counter is the end of the array, loop back to position 0 and incriment counter2
            If counter1 >= fileCount Then
                counter1 = 0
                counter2 += 1
            End If
            'if the presentation has looped 25 times, log off the session
            If counter2 = 25 Then
                'System.Diagnostics.Process.Start(dir.ToString & "\logoff.bat")
                Me.Close()
            End If
        Catch ex As Exception
            'System.Diagnostics.Process.Start(dir.ToString & "\logoff.bat")
            Me.Close()
        End Try

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class

Class ObjectComparer
    Implements IComparer

    Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
        Return String.Compare(x.ToString, y.ToString)
    End Function
End Class


