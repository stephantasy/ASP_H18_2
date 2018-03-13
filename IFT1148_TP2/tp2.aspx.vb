
'   IFT1148 -TP2
'   Stéphane Barthélemy
'   20084771 - barthste

Partial Class Tp2 : Inherits System.Web.UI.Page

    'Constantes
    Protected Const TP1_NOTE_MAX As Integer = 10
    Protected Const TP2_NOTE_MAX As Integer = 15
    Protected Const TP3_NOTE_MAX As Integer = 15
    Protected Const INTRA_NOTE_MAX As Integer = 100
    Protected Const INTRA_NOTE_PERCENT As Integer = 20
    Protected Const FINAL_NOTE_MAX As Integer = 100
    Protected Const FINAL_NOTE_PERCENT As Integer = 40
    Protected Const NOTE_MAXIMUM As Integer = 100
    Protected Const NOTE_PASSAGE As Double = 49.5
    Protected Const SEUIL_PERCENT As Integer = 40

    'Messages d'erreurs
    Private Const ERROR_MESSAGE_NOTE_DEFAUT As String = "Veuillez entrer une note valide"
    Private Const ERROR_MESSAGE_NOTE_RANGE As String = "La note doit être dans l'interval prévue"
    Private Const ERROR_MESSAGE_R_FINAL As String = "Le code R ne peut pas être utilisée ici"


    'Renvoie un code binaire où les 1 indiquent qu'un R est présent dans le champ concerné 
    Private Function GetR_ByteCode() As Byte
        Dim rByteCode As Byte = 0
        rByteCode = If(Tb_NoteTp1.Text.ToUpper() = "R", rByteCode Or 1, rByteCode And 254)     '1er bit pour le TP1
        rByteCode = If(Tb_NoteTp2.Text.ToUpper() = "R", rByteCode Or 2, rByteCode And 253)     '2e bit pour le TP2
        rByteCode = If(Tb_NoteTp3.Text.ToUpper() = "R", rByteCode Or 4, rByteCode And 251)     '3e bit pour le TP3
        rByteCode = If(Tb_NoteIntra.Text.ToUpper() = "R", rByteCode Or 8, rByteCode And 247)   '4e bit pour l'intra
        Return rByteCode
    End Function


    'Calcul des notes pour les TP
    Private Sub GetNotesTp(codeR As Byte, intra As Double, final As Double, ByRef tp1Temp As Double, ByRef tp2Temp As Double, ByRef tp3Temp As Double)
        Select Case (codeR And 247) ' (on ignore le bit de l'intra)
            Case 1  ' Manque que le TP 1
                tp2Temp = Double.Parse(Tb_NoteTp2.Text)
                tp3Temp = Double.Parse(Tb_NoteTp3.Text)
                tp1Temp = ((tp2Temp / TP2_NOTE_MAX * TP1_NOTE_MAX) + (tp3Temp / TP3_NOTE_MAX * TP1_NOTE_MAX)) / 2
            Case 2 ' Manque que le TP 2
                tp1Temp = Double.Parse(Tb_NoteTp1.Text)
                tp3Temp = Double.Parse(Tb_NoteTp3.Text)
                tp2Temp = ((tp1Temp / TP1_NOTE_MAX * TP2_NOTE_MAX) + (tp3Temp / TP3_NOTE_MAX * TP2_NOTE_MAX)) / 2
            Case 4 ' Manque que le TP 3
                tp1Temp = Double.Parse(Tb_NoteTp1.Text)
                tp2Temp = Double.Parse(Tb_NoteTp2.Text)
                tp3Temp = ((tp1Temp / TP1_NOTE_MAX * TP2_NOTE_MAX) + (tp2Temp / TP2_NOTE_MAX * TP1_NOTE_MAX)) / 2
            Case 3 ' Manque les TP 1 et 2
                tp3Temp = Double.Parse(Tb_NoteTp3.Text)
                tp1Temp = (tp3Temp / TP3_NOTE_MAX * TP1_NOTE_MAX)
                tp2Temp = (tp3Temp / TP3_NOTE_MAX * TP2_NOTE_MAX)
            Case 5 ' Manque les TP 1 et 3
                tp2Temp = Double.Parse(Tb_NoteTp2.Text)
                tp1Temp = (tp2Temp / TP2_NOTE_MAX * TP1_NOTE_MAX)
                tp3Temp = (tp2Temp / TP2_NOTE_MAX * TP3_NOTE_MAX)
            Case 6 ' Manque les TP 2 et 3
                tp1Temp = Double.Parse(Tb_NoteTp1.Text)
                tp2Temp = (tp1Temp / TP1_NOTE_MAX * TP2_NOTE_MAX)
                tp3Temp = (tp1Temp / TP1_NOTE_MAX * TP3_NOTE_MAX)
            Case 7 ' Manque les TP 1, 2 et 3
                tp1Temp = ((intra / INTRA_NOTE_PERCENT * TP1_NOTE_MAX) + (final / FINAL_NOTE_PERCENT * TP1_NOTE_MAX)) / 2
                tp2Temp = ((intra / INTRA_NOTE_PERCENT * TP2_NOTE_MAX) + (final / FINAL_NOTE_PERCENT * TP2_NOTE_MAX)) / 2
                tp3Temp = ((intra / INTRA_NOTE_PERCENT * TP3_NOTE_MAX) + (final / FINAL_NOTE_PERCENT * TP3_NOTE_MAX)) / 2
            Case Else ' Toutes les notes de TP sont là
                tp1Temp = Double.Parse(Tb_NoteTp1.Text)
                tp2Temp = Double.Parse(Tb_NoteTp2.Text)
                tp3Temp = Double.Parse(Tb_NoteTp3.Text)
        End Select
    End Sub


    'Calcule les résultats des TP et des examens et les affectes aux variables passées par référence
    Private Sub CalculNotes(ByRef tp As Double, ByRef intra As Double, ByRef final As Double, ByRef exam As Double)

        Dim tp1Temp, tp2Temp, tp3Temp As Double
        Dim codeR As Byte = GetR_ByteCode()         'Récupère un Byte où les 4 premiers bits indiquent si un champ contient un R

        'Examen Final
        final = Double.Parse(Tb_NoteFinal.Text) / FINAL_NOTE_MAX * FINAL_NOTE_PERCENT

        'Traitement pour l'Intra
        If (codeR And 8) Then
            intra = Double.Parse(Tb_NoteFinal.Text) / FINAL_NOTE_MAX * INTRA_NOTE_PERCENT
        Else
            intra = Double.Parse(Tb_NoteIntra.Text) / INTRA_NOTE_MAX * INTRA_NOTE_PERCENT
        End If

        'On récupère les notes des 3 TP
        GetNotesTp(codeR, intra, final, tp1Temp, tp2Temp, tp3Temp)

        'Total des TP
        tp = tp1Temp + tp2Temp + tp3Temp

        'Total des examens
        exam = intra + final

    End Sub


    ' Renvoie si le seuil à été atteint en fonction de la note rçue en paramètre
    Private Function IsSeuilReached(note As Double) As Boolean
        Return note >= ((INTRA_NOTE_PERCENT + FINAL_NOTE_PERCENT) / NOTE_MAXIMUM * SEUIL_PERCENT)
    End Function


    'Renvoie la note littérale en fonction de la note numérique reçue en paramètre
    Private Function GetNoteLitteral(note As Double) As String
        Dim noteLitteral As String

        'Note littérale
        Select Case note
            Case Is >= 89.5
                noteLitteral = "A+"
            Case Is >= 84.5
                noteLitteral = "A"
            Case Is >= 79.5
                noteLitteral = "A-"
            Case Is >= 76.5
                noteLitteral = "B+"
            Case Is >= 72.5
                noteLitteral = "B"
            Case Is >= 69.5
                noteLitteral = "B-"
            Case Is >= 64.5
                noteLitteral = "C+"
            Case Is >= 59.5
                noteLitteral = "C"
            Case Is >= 56.5
                noteLitteral = "C-"
            Case Is >= 53.5
                noteLitteral = "D+"
            Case Is >= 49.5
                noteLitteral = "D-"
            Case Is >= 34.5
                noteLitteral = "E"
            Case Else
                noteLitteral = "F"
        End Select

        Return noteLitteral
    End Function


    'Affiche les résultats
    Private Sub AfficherResultats(resultatTravaux As Double, resultaIntra As Double, resultaFinal As Double,
                                  resultat As Double, noteLitteral As String, seuilAtteint As Boolean)
        'Notes
        Lb_ResultNotes.Text = "Résultats = Travaux : " & Math.Round(resultatTravaux, 1).ToString() & "/" & (TP1_NOTE_MAX + TP2_NOTE_MAX + TP3_NOTE_MAX) &
                        " Intra : " & Math.Round(resultaIntra, 1).ToString() & "/" & INTRA_NOTE_PERCENT &
                        " Final : " & Math.Round(resultaFinal, 1).ToString() & "/" & FINAL_NOTE_PERCENT &
                        " Total : " & Math.Round(resultat, 1).ToString() & "% (" & noteLitteral & ")"

        'Affichage de réussite
        If (resultat >= NOTE_PASSAGE) Then
            Lt_ResultCours.Text = "Vous avez réussi le cours."
        Else
            Lt_ResultCours.Text = "Vous avez échoué le cours."
        End If

        'Seuil
        If (Not seuilAtteint) Then
            Lb_Seuil.Text = "(Seuil)"
        Else
            Lb_Seuil.Text = String.Empty
        End If
    End Sub


    'Event lors du clic sur le bouton
    Protected Sub CalculerResultats(sender As Object, e As EventArgs)

        If (Page.IsValid) Then

            ' Variables
            Dim resultatTravaux, resultaIntra, resultaFinal, resultExamen, resultat As Double
            Dim seuilAtteint As Boolean = False
            Dim noteLitteral As String

            ' Calcul des résultats
            CalculNotes(resultatTravaux, resultaIntra, resultaFinal, resultExamen)

            'Contrôle du seuil
            seuilAtteint = IsSeuilReached(resultExamen)

            'Calcul de la note finale en fonction du seuil
            If (seuilAtteint) Then
                resultat = resultatTravaux + resultExamen
            Else
                resultat = resultExamen
            End If

            'Récupération de la note littérale
            noteLitteral = GetNoteLitteral(resultat)

            'Affichage des résultats
            AfficherResultats(resultatTravaux, resultaIntra, resultaFinal, resultat, noteLitteral, seuilAtteint)

        End If
    End Sub


    'Renvoie si la note est dans l'interval prévu
    Private Function IsInRange(textBoxId As String, value As Double) As Boolean
        Select Case textBoxId
            Case Tb_NoteTp1.ID
                Return value >= 0 And value <= TP1_NOTE_MAX
            Case Tb_NoteTp2.ID
                Return value >= 0 And value <= TP2_NOTE_MAX
            Case Tb_NoteTp3.ID
                Return value >= 0 And value <= TP3_NOTE_MAX
            Case Tb_NoteIntra.ID
                Return value >= 0 And value <= INTRA_NOTE_MAX
            Case Tb_NoteFinal.ID
                Return value >= 0 And value <= FINAL_NOTE_MAX
            Case Else
                Return False
        End Select
    End Function


    Protected Sub CustomServerValidation(source As Object, args As ServerValidateEventArgs)

        Dim localizedValue As String    'Valeur du champ "localisé" (avec "." ou "," selon la localisation)
        Dim NumberValue As Double       'Valeur numérique du champ
        Dim textBoxTraite As TextBox = FindControl(source.ControlToValidate)    'TextBox controlée

        ' Non valide par défaut
        args.IsValid = False

        'Est-ce un "R"
        If (args.Value.ToUpper() = "R") Then
            ' Sauf pour l'examen final
            If (textBoxTraite.ID = Tb_NoteFinal.ID) Then
                source.ErrorMessage = ERROR_MESSAGE_R_FINAL
            Else
                'Sinon, c'est valide
                args.IsValid = True
            End If
            Return
        End If

        'Si la culture locale est en anglais, on remplace les "," par des ".", sinon on fait l'inverse.
        If (Globalization.CultureInfo.CurrentCulture.ToString.IndexOf("en") >= 0) Then
            localizedValue = args.Value.Replace(",", ".")
        Else
            localizedValue = args.Value.Replace(".", ",")
        End If

        'Est-ce bien un nombre (à contrôler après la localisation !)
        Try
            NumberValue = Double.Parse(localizedValue)
        Catch ex As Exception
            'Ce n'est pas un nombre, on arrête la validation
            source.ErrorMessage = ERROR_MESSAGE_NOTE_DEFAUT
            Return
        End Try

        'Le nombre est dans le bon interval
        If (IsInRange(textBoxTraite.ID, NumberValue)) Then

            'On remplace avec le bon séparateur (au besoin), avec 1 décimale
            textBoxTraite.Text = Math.Round(NumberValue, 1).ToString()

            'Validation Ok
            args.IsValid = True
        Else
            'Hors interval
            source.ErrorMessage = ERROR_MESSAGE_NOTE_RANGE
        End If
    End Sub

End Class
