Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports System.Windows.Forms
Imports Newtonsoft.Json
Imports System.IO
Imports System.Linq

Partial Public Class Form1
    Inherits Form

    Private Const FileName As String = "words.json"
    Private words As New List(Of WordItem)()
    Private hotkeyMap As New Dictionary(Of Integer, WordItem)()
    Private hotkeyIdCounter As Integer = 1

    <DllImport("user32.dll")>
    Private Shared Function SetForegroundWindow(hWnd As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function RegisterHotKey(hWnd As IntPtr, id As Integer, fsModifiers As UInteger, vk As UInteger) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function UnregisterHotKey(hWnd As IntPtr, id As Integer) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function ShowWindow(hWnd As IntPtr, nCmdShow As Integer) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function IsIconic(hWnd As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetForegroundWindow() As IntPtr
    End Function

    Private Const MOD_ALT As UInteger = &H1UI
    Private Const MOD_CONTROL As UInteger = &H2UI
    Private Const MOD_SHIFT As UInteger = &H4UI
    Private Const SW_RESTORE As Integer = 9

    Public Sub New()
        InitializeComponent()
        LoadWords()
        UpdateGrid()
        RegisterAllHotkeys()

        chkSendEnter.Checked = True
        nudTabs.Value = 1
        nudDelay.Value = 80
    End Sub

    Private Sub LoadWords()
        If File.Exists(FileName) Then
            Try
                Dim json = File.ReadAllText(FileName)
                Dim list = JsonConvert.DeserializeObject(Of List(Of WordItem))(json)
                If list IsNot Nothing Then words = list
            Catch
                words = New List(Of WordItem)()
            End Try
        End If
    End Sub

    Private Sub SaveWords()
        Dim json = JsonConvert.SerializeObject(words, Formatting.Indented)
        File.WriteAllText(FileName, json)
    End Sub

    Private Sub UpdateGrid()
        dataGridViewWords.Rows.Clear()
        Dim filtered = words.AsEnumerable()

        If Not String.IsNullOrWhiteSpace(txtFilter.Text) Then
            Dim needle = txtFilter.Text
            filtered = filtered.Where(Function(w) _
                (If(w.Text, String.Empty).IndexOf(needle, StringComparison.OrdinalIgnoreCase) >= 0) OrElse
                (If(w.Category, String.Empty).IndexOf(needle, StringComparison.OrdinalIgnoreCase) >= 0) OrElse
                (If(w.Hotkey, String.Empty).IndexOf(needle, StringComparison.OrdinalIgnoreCase) >= 0))
        End If

        For Each w In filtered
            dataGridViewWords.Rows.Add(w.Text, w.Category, w.Hotkey)
        Next

        RegisterAllHotkeys()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim text = ShowInputBox("Neuen Text eingeben:", "Hinzufügen", "")
        If String.IsNullOrWhiteSpace(text) Then Return

        Dim category = ShowInputBox("Kategorie (optional):", "Hinzufügen", "")
        Dim hotkey = ShowInputBox("Hotkey (optional, z. B. Ctrl+Alt+1):", "Hinzufügen", "")

        words.Add(New WordItem With {.Text = text, .Category = If(category, ""), .Hotkey = If(hotkey, "")})
        SaveWords()
        UpdateGrid()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dataGridViewWords.SelectedRows.Count = 0 Then Return
        For Each row As DataGridViewRow In dataGridViewWords.SelectedRows
            Dim text = Convert.ToString(row.Cells(0).Value)
            Dim item = words.FirstOrDefault(Function(w) w.Text = text)
            If item IsNot Nothing Then words.Remove(item)
        Next
        SaveWords()
        UpdateGrid()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If dataGridViewWords.SelectedRows.Count <> 1 Then Return
        Dim row = dataGridViewWords.SelectedRows(0)
        Dim oldText = Convert.ToString(row.Cells(0).Value)
        Dim item = words.FirstOrDefault(Function(w) w.Text = oldText)
        If item Is Nothing Then Return

        Dim newText = ShowInputBox("Text bearbeiten:", "Bearbeiten", item.Text)
        If String.IsNullOrWhiteSpace(newText) Then Return
        Dim newCategory = ShowInputBox("Kategorie bearbeiten:", "Bearbeiten", item.Category)
        Dim newHotkey = ShowInputBox("Hotkey bearbeiten (z. B. Ctrl+Alt+1):", "Bearbeiten", item.Hotkey)

        item.Text = newText
        item.Category = If(newCategory, "")
        item.Hotkey = If(newHotkey, "")
        SaveWords()
        UpdateGrid()
    End Sub

    Private Sub txtFilter_TextChanged(sender As Object, e As EventArgs) Handles txtFilter.TextChanged
        UpdateGrid()
    End Sub

    Private Sub dataGridViewWords_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dataGridViewWords.CellMouseClick
        If e.Button = MouseButtons.Left AndAlso e.RowIndex >= 0 Then
            Dim text = Convert.ToString(dataGridViewWords.Rows(e.RowIndex).Cells(0).Value)
            If Not String.IsNullOrEmpty(text) Then
                SendWordToWindow(text)
            End If
        End If
    End Sub

    Private Function BringToFront(hWnd As IntPtr, Optional attempts As Integer = 8) As Boolean
        If hWnd = IntPtr.Zero Then Return False
        If IsIconic(hWnd) Then ShowWindow(hWnd, SW_RESTORE)
        For i = 0 To attempts - 1
            SetForegroundWindow(hWnd)
            Threading.Thread.Sleep(80 + i * 20)
            If GetForegroundWindow() = hWnd Then Return True
        Next
        Return False
    End Function

    Private Sub SendTextViaClipboard(text As String)
        Dim oldData As IDataObject = Nothing
        Try
            oldData = Clipboard.GetDataObject()
        Catch
        End Try

        Try
            Clipboard.SetText(If(text, String.Empty))
            SendKeys.SendWait("^{v}") ' Paste
        Finally
            Try
                If oldData IsNot Nothing Then Clipboard.SetDataObject(oldData)
            Catch
            End Try
        End Try
    End Sub

    Private Sub TypeSequence(text As String)
        Dim delay As Integer = CInt(nudDelay.Value)
        Dim tabCount As Integer = CInt(nudTabs.Value)

        For i = 1 To tabCount
            SendKeys.SendWait("{TAB}")
            Threading.Thread.Sleep(delay)
        Next

        SendTextViaClipboard(text)
        Threading.Thread.Sleep(delay)

        If chkSendEnter.Checked Then
            SendKeys.SendWait("{ENTER}")
            Threading.Thread.Sleep(delay)
        End If
    End Sub

    Private Sub SendWordToWindow(text As String)
        Dim target = If(txtTargetTitle.Text, "").Trim()
        If String.IsNullOrEmpty(target) Then
            MessageBox.Show("Bitte gib oben den Fenstertitel des Zielprogramms ein.", "Hinweis",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        For Each p In Process.GetProcesses()
            If String.IsNullOrEmpty(p.MainWindowTitle) Then Continue For
            If p.MainWindowTitle.IndexOf(target, StringComparison.OrdinalIgnoreCase) < 0 Then Continue For

            Try
                p.WaitForInputIdle(500)
                If Not BringToFront(p.MainWindowHandle) Then Continue For

                Threading.Thread.Sleep(120)
                TypeSequence(text)
                Return
            Catch
                ' Try next process
            End Try
        Next

        MessageBox.Show("Zielprogramm nicht gefunden oder konnte nicht fokussiert werden.")
    End Sub

    Private Sub RegisterAllHotkeys()
        ' Unregister previous
        For Each id In hotkeyMap.Keys.ToList()
            UnregisterHotKey(Me.Handle, id)
        Next
        hotkeyMap.Clear()
        hotkeyIdCounter = 1

        For Each item In words
            If String.IsNullOrWhiteSpace(item.Hotkey) Then Continue For
            Dim mods As UInteger, key As UInteger
            If Not TryParseHotkey(item.Hotkey, mods, key) Then Continue For

            Dim id = Threading.Interlocked.Increment(hotkeyIdCounter)
            If RegisterHotKey(Me.Handle, id, mods, key) Then
                hotkeyMap(id) = item
            End If
        Next
    End Sub

    Private Function TryParseHotkey(hotkey As String, ByRef modifiers As UInteger, ByRef key As UInteger) As Boolean
        modifiers = 0UI : key = 0UI
        Try
            For Each part In hotkey.Split({"+"c}, StringSplitOptions.RemoveEmptyEntries)
                Dim t = part.Trim().ToLowerInvariant()
                Select Case t
                    Case "ctrl", "control" : modifiers = modifiers Or MOD_CONTROL
                    Case "alt" : modifiers = modifiers Or MOD_ALT
                    Case "shift" : modifiers = modifiers Or MOD_SHIFT
                    Case Else
                        key = CUInt([Enum].Parse(GetType(Keys), part.Trim(), True))
                End Select
            Next
            Return key <> 0UI
        Catch
            Return False
        End Try
    End Function

    Protected Overrides Sub WndProc(ByRef m As Message)
        Const WM_HOTKEY = &H312
        If m.Msg = WM_HOTKEY Then
            Dim id = m.WParam.ToInt32()
            Dim item As WordItem = Nothing
            If hotkeyMap.TryGetValue(id, item) Then
                SendWordToWindow(item.Text)
            End If
        End If
        MyBase.WndProc(m)
    End Sub

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        For Each id In hotkeyMap.Keys.ToList()
            UnregisterHotKey(Me.Handle, id)
        Next
        hotkeyMap.Clear()
        MyBase.OnFormClosed(e)
    End Sub

    ' Simple InputBox (custom, no VB Interaction dependency)
    Private Function ShowInputBox(prompt As String, title As String, defaultValue As String) As String
        Using frm As New Form()
            Dim lbl As New Label() With {.Left = 12, .Top = 12, .AutoSize = True, .Text = prompt}
            Dim txt As New TextBox() With {.Left = 12, .Top = 36, .Width = 360, .Text = defaultValue}
            Dim ok As New Button() With {.Text = "OK", .Left = 216, .Width = 75, .Top = 72, .DialogResult = DialogResult.OK}
            Dim cancel As New Button() With {.Text = "Abbrechen", .Left = 297, .Width = 75, .Top = 72, .DialogResult = DialogResult.Cancel}

            frm.Text = title
            frm.FormBorderStyle = FormBorderStyle.FixedDialog
            frm.StartPosition = FormStartPosition.CenterParent
            frm.ClientSize = New Drawing.Size(384, 111)
            frm.Controls.AddRange(New Control() {lbl, txt, ok, cancel})
            frm.AcceptButton = ok
            frm.CancelButton = cancel

            Return If(frm.ShowDialog(Me) = DialogResult.OK, txt.Text, Nothing)
        End Using
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
