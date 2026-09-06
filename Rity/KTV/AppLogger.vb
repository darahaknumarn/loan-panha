Imports System.IO

''' <summary>
''' Simple application-wide logger for exceptions.
''' Writes daily log files to %APPDATA%\Morokot\Logs\yyyy-MM-dd.log
''' </summary>
Public Module AppLogger
    Public Sub LogException(ex As Exception, Optional context As String = "")
        Try
            Dim logDir As String = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Morokot", "Logs")
            Directory.CreateDirectory(logDir)
            Dim logFile As String = System.IO.Path.Combine(logDir, DateTime.Now.ToString("yyyy-MM-dd") & ".log")

            Using sw As New StreamWriter(logFile, True)
                sw.WriteLine("--------------------------------------------------")
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                If Not String.IsNullOrEmpty(context) Then
                    sw.WriteLine("Context: " & context)
                End If
                If ex IsNot Nothing Then
                    sw.WriteLine(ex.ToString())
                Else
                    sw.WriteLine("Exception object was null")
                End If
                sw.WriteLine()
            End Using
        Catch
            ' Swallow any logging errors to avoid recursive failures
        End Try
    End Sub
End Module
