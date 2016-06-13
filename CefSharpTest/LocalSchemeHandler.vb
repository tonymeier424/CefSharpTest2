'Imports CefSharp
'Imports CefSharp.Core
'Imports System.IO

'Public Class LocalSchemeHandler
'    Implements ISchemeHandler
'    Public Function ProcessRequestAsync(request As IRequest, response As ISchemeHandlerResponse, requestCompletedCallback As OnRequestCompletedHandler) As Boolean
'        Dim u As New Uri(request.Url)
'        Dim file__1 As [String] = u.Authority + u.AbsolutePath

'        If File.Exists(file__1) Then
'            Dim bytes As [Byte]() = File.ReadAllBytes(file__1)
'            response.ResponseStream = New MemoryStream(bytes)
'            Select Case Path.GetExtension(file__1)
'                Case ".html"
'                    response.MimeType = "text/html"
'                    Exit Select
'                Case ".js"
'                    response.MimeType = "text/javascript"
'                    Exit Select
'                Case ".png"
'                    response.MimeType = "image/png"
'                    Exit Select
'                Case ".appcache", ".manifest"
'                    response.MimeType = "text/cache-manifest"
'                    Exit Select
'                Case Else
'                    response.MimeType = "application/octet-stream"
'                    Exit Select
'            End Select
'            requestCompletedCallback()
'            Return True
'        End If
'        Return False
'    End Function
'End Class
