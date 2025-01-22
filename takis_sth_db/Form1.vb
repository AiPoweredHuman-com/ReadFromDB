Imports System.IO.Pipelines
Imports MySql.Data.MySqlClient
Imports Org.BouncyCastle.Asn1.X500

Public Class Form1
    Dim conn As MySqlConnection
    Dim connString As String
    Dim cmd As New MySqlCommand
    Dim rd As MySqlDataReader

    Private Sub openconnection()
        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText("..\..\..\conn_data.txt")
        MsgBox(fileReader)
        connString = fileReader
        conn = New MySqlConnection()
        conn.ConnectionString = connString
        conn.Open()

        If (conn.State = ConnectionState.Open) Then
            MsgBox("Conn opended")
        End If
    End Sub

    Private Sub closeconnection()
        If (conn.State = ConnectionState.Open) Then
            conn.Close()
            MsgBox("conn closed")
        End If
    End Sub
    Private Sub executequery(query As String)
        cmd = New MySqlCommand(query, conn)
        rd = cmd.ExecuteReader()
        If rd.HasRows Then
            While (rd.Read())
                ListBox1.Items.Add("UN: " + rd("username"))
                ListBox1.Items.Add("Pass: " + rd("password"))
            End While
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        openconnection()
        Dim query As String
        query = "Select username, password from Table1"
        executequery(query)
        closeconnection()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        openconnection()
        Dim query As String
        query = "Select username, password from Table1
            where username= '" & UName.Text & "' and password = ' " & Passwrd.Text & "' order by password"
        executequery(query)


        closeconnection()
    End Sub
End Class
