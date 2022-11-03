Imports System.Threading
Imports System.Diagnostics

Public Class Form1
    Public Declare Function GetAsyncKeyState Lib "user32.dll" (ByVal vKey As Int32) As UShort

    Private Property Speed As Integer = 10
    Public Property Velocity As Vector2 = New Vector2(0, 0)

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim Forward As Integer = If(GetAsyncKeyState(Convert.ToInt32(Keys.W)), 1, 0)
        Dim Backward As Integer = If(GetAsyncKeyState(Convert.ToInt32(Keys.S)), 1, 0)
        Dim Left As Integer = If(GetAsyncKeyState(Convert.ToInt32(Keys.A)), 1, 0)
        Dim Right As Integer = If(GetAsyncKeyState(Convert.ToInt32(Keys.D)), 1, 0)

        Dim InputVector As Vector2 = New Vector2(Right - Left, Backward - Forward).Normalize()

        Velocity = Velocity.Lerp(InputVector.Mult(Speed), 0.15)

        PictureBox1.Top = Math.Min(Math.Max(PictureBox1.Top + Velocity.Y, 0), Me.Size.Height)
        PictureBox1.Left = Math.Min(Math.Max(PictureBox1.Left + Velocity.X, 0), Me.Size.Width)

        Debug.Print(Me.Size.Width)
    End Sub

End Class

Public Class Vector2
    Public Property X As Single
    Public Property Y As Single

    Public Function Lerp(Vector As Vector2, Time As Single) As Vector2
        'a + (b - a) * x
        Lerp = Vector.Sub(Me).Mult(Time).Add(Me)
    End Function

    Public Function Magnitude() As Single
        Magnitude = Math.Sqrt(X ^ 2 + Y ^ 2)
    End Function

    Public Function Normalize() As Vector2
        Dim Distance As Single = Magnitude()
        Distance = If(Distance = 0, 1, Distance)
        Normalize = New Vector2(X / Distance, Y / Distance)
    End Function

    Public Function Div(NumOrVec As Object) As Vector2
        If (IsNumeric(NumOrVec)) Then
            Div = New Vector2(X / NumOrVec, Y / NumOrVec)
        ElseIf (TypeOf NumOrVec Is Vector2) Then
            Div = New Vector2(X / NumOrVec.X, Y / NumOrVec.Y)
        End If
    End Function

    Public Function Mult(NumOrVec As Object) As Vector2
        If (IsNumeric(NumOrVec)) Then
            Mult = New Vector2(X * NumOrVec, Y * NumOrVec)
        ElseIf (TypeOf NumOrVec Is Vector2) Then
            Mult = New Vector2(X * NumOrVec.X, Y * NumOrVec.Y)
        End If
    End Function

    Public Function Add(Vector As Vector2) As Vector2
        Add = New Vector2(X + Vector.X, Y + Vector.Y)
    End Function

    Public Function [Sub](Vector As Vector2) As Vector2
        [Sub] = New Vector2(X - Vector.X, Y - Vector.Y)
    End Function

    Public Sub New(XValue As Single, YValue As Single)
        X = XValue
        Y = YValue
    End Sub

End Class