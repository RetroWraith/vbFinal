﻿Public Class IDTable
    Public Shared Function IDPower(ID As Integer) As Integer
        'Retuns power of creature from ID
        Select Case ID
            Case 1
                Return 2
            Case 2
                Return 2
            Case 3
                Return 3
            Case 4
                Return 4
            Case 5
                Return 5
            Case Else
                'Error return, returned if ID does not exist
                Return 100
        End Select
    End Function

    Public Shared Function IDHealth(ID As Integer) As Integer
        'Retuns Health of creature from ID
        Select Case ID
            Case 1
                Return 2
            Case 2
                Return 2
            Case 3
                Return 4
            Case 4
                Return 5
            Case 5
                Return 6
            Case Else
                'Error return, returned if ID does not exist
                Return 100
        End Select
    End Function

    Public Shared Function IDAbility(c As Card) As Integer
        Select Case c.ID
            Case Is = 1
                If Form1.NeedTarget = True Then
                    'Confirms this is target of ability
                    If MsgBox("Would you like to give this creature -1/-0?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        'Applies debuff and stops ability process
                        Form1.Target.Buff(-1, 0)
                        Form1.NeedTarget = False
                    End If
                ElseIf MsgBox("Would you like to use this card's ability?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'Says a target is needed if ability is used
                    Form1.NeedTarget = True
                    Form1.IDSearchingForTarget = c.ID
                End If
                Return 1
            Case Is = 2
                '//ABILITY TEMPLATE//
                'If a target is being searched for
                If Form1.NeedTarget = True Then
                    'Confirms this is target of ability
                    If MsgBox("Would you like to [list ability]", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        'Ability code
                        'Disables target search
                        Form1.NeedTarget = False
                    End If
                    'If no target is needed currently
                ElseIf MsgBox("Would you like to use this card's ability?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'Says a target is needed if ability is used, starts search for target
                    Form1.NeedTarget = True
                    Form1.IDSearchingForTarget = c.ID
                End If
            Case Is = 3
                '//LAND TEMPLATE//
                'Ez
                landUse(c)
            Case 4
                landUse(c)
            Case 5
                landUse(c)
            Case 6
                landUse(c)
            Case 7
                landUse(c)
        End Select
        Return 0
    End Function

    Public Shared Sub landUse(c As Card)
        If c.used = False Then
            If c.tapped = False Then
                c.used = True
                Form1.AddMana(c.manaCost(0))
                c.BackgroundImage = TapImage(c)
            End If
        Else
            If c.tapped = False Then
                c.used = False
                Form1.RemMana(c.manaCost(0))
                c.BackgroundImage = IDImage(c)
            End If
        End If
    End Sub

    Public Shared Sub IDUseAbility(ID As Integer)
        Select Case ID
            Case 1
                Form1.Target.Buff(-1, 0)
            Case 2

            Case 3

            Case Is = 4

            Case Is = 5

        End Select

    End Sub

    Public Shared Function TapImage(c As Card)

        Select Case c.ID
            Case 3
                Return My.Resources.islandt
            Case 4
                Return My.Resources.swampu
            Case 5
                Return My.Resources.forestu
            Case 6
                Return My.Resources.mountainu
            Case 7
                Return My.Resources.plainsu
        End Select


    End Function

    Public Shared Function IDImage(C As Card) As Image
        'Sets the new Card's Image to the card played
        Select Case C.ID
            Case 1
                Return My.Resources.NoobMemer
            Case 2
                Return My.Resources.Ardent_Procrastinor
            Case 3
                'Island
                If C.tapped = False Then
                    Return My.Resources.island
                Else
                    Return My.Resources.islandtu
                End If

            Case 4
                'Swamp
                If C.tapped = False Then
                    Return My.Resources.swamp
                Else
                    Return My.Resources.swamptu
                End If

            Case 5
                'Forest
                If C.tapped = False Then
                    Return My.Resources.forest
                Else
                    Return My.Resources.foresttu
                End If
            Case 6
                'Mountain
                If C.tapped = False Then
                    Return My.Resources.mountain
                Else
                    Return My.Resources.mountaintu
                End If
            Case 7
                'Plains
                If C.tapped = False Then
                    Return My.Resources.plains
                Else
                    Return My.Resources.plainstu
                End If
            Case 31

        End Select
    End Function

    Public Shared Sub PlayCard(c As Card)

        Select Case c.ID
            Case 1
                PlayCreature(c)
            Case 2
                PlayCreature(c)
            Case 3
                playLand(c)
            Case 4
                playLand(c)
            Case 5
                playLand(c)
            Case 6
                playLand(c)
            Case 7
                playLand(c)

        End Select

    End Sub

    Public Shared Sub PlayCreature(c As Card)
        If PayMana(c) Then
            Form1.GenerateCreature(c.ID, True)
            Form1.handInfo.Remove(c)
            c.Dispose()
            Form1.UpdateHand()
        End If
    End Sub


    Public Shared Sub playLand(c As Card)
        If Form1.landPlayed < Form1.landMax Then

            Form1.handInfo.RemoveAt(Form1.handInfo.IndexOf(c))
            Form1.landInfo.Add(c)
            c.partOfHand = False
            Form1.UpdateHand()
            c.Name = "land" & Form1.landInfo.Count
            Form1.Controls(c.Name).Top = -50 * Form1.landInfo.Count + Form1.Height * 3 / 4
            Form1.Controls(c.Name).Left = 100
            Form1.landPlayed += 1

        End If
    End Sub

    Public Shared Function PayMana(iC As Card) As Boolean

        'Ill comment this eventually
        If Form1.manaPool.Count <> 0 Then

            Dim testList As New List(Of String)
            Static counter As Integer

            For x As Integer = 1 To Form1.manaPool.Count
                testList.Add(Form1.manaPool(x - 1))
            Next


            For I As Integer = 0 To iC.manaCost.Count - 1

                counter = 0
                Dim result As Integer = 0

                Do Until result <> 0

                    If testList.Count = 0 Then Return False

                    If iC.manaCost(I) = testList(counter) Or iC.manaCost(I) = "any" Then

                        testList.Remove(testList(counter))
                        result = 1

                    ElseIf counter = testList.Count - 1
                        Return False
                    End If

                    counter += 1

                Loop

            Next

            Form1.TMC(iC)
            Form1.ManaLabel()
            Return True

        End If

    End Function

    Public Shared Sub SetMana(c As Card)

        Select Case c.ID
            Case 1
                'Add anys last
                c.manaCost.Add("green")
                c.manaCost.Add("any")
            Case 2
                c.manaCost.Add("black")
                c.manaCost.Add("black")
                c.manaCost.Add("any")
                c.manaCost.Add("any")
                c.manaCost.Add("any")
            Case 3
                'Cost on lands is what they give
                c.manaCost.Add("blue")
            Case 4
                c.manaCost.Add("black")
            Case 5
                c.manaCost.Add("green")
            Case 6
                c.manaCost.Add("red")
            Case 7
                c.manaCost.Add("plains")


        End Select

    End Sub

End Class
