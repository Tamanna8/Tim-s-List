'=================================================================================
'Project:       Lab 2
'Title:         Tims List
'File Name:     TimsList.vb,.exe
'Date Completed:  3/14/2016

'Author:        Mannat,Tamanna,Timmy,LJ,Justo
'Class:         CS 115 Sec B

' Description: This program will allow the user to see the items of their choices 
'              through different categories.On clicking on their preferable item, 
'              the user could see the information and the image of the item.
'              The user could make the image bigger as well for clearer view.
'              The user could also exit the program.
'==================================================================================

Option Explicit On
Option Strict On

Public Class frmTimsList

    '=================================================================================
    'Description: It will form an array.
    '=================================================================================


    Dim shtSpace As Integer = 10
    Dim shtBoarder As Integer = 50

    Dim cintNumFiles As Integer = 110
    Dim cstrPathSource As String = "..\Data\"
    Dim cstrFileName(cintNumFiles) As String
    Dim cintFileLines(cintNumFiles) As Integer
    Dim cstrItemName(cintNumFiles) As String
    Dim cstrCategory(cintNumFiles) As String
    Dim cstrDetail(cintNumFiles) As String
    Dim cstrContactMethod(cintNumFiles) As String
    Dim cstrDescription(cintNumFiles) As String
    Dim cdblPrice(cintNumFiles) As Double
    Dim cstrContactInfo(cintNumFiles) As String
    Dim cstrPictureFile(cintNumFiles) As String

    Dim FullScreen As Boolean = False

    Dim radCategoryItem(30) As RadioButton
    Dim lblCategoryItem(30) As Label

    Dim strItemsList As String
    Dim strTemp As String



    Private Sub frmTimsList_Load(ByVal sender As System.Object, ByVal _
                                 e As System.EventArgs) Handles MyBase.Load

        '=================================================================================
        'Description: First it will show a message. It will set the startup environment.
        '             It opens list file and get the radio buttons in the program.  It
        '             will place the objects for sale in the form.
        '=================================================================================

        Dim strTemp As String
        Dim intFiles As Integer
        Dim i As Integer
        Dim j As Integer

        If MsgBox("Welcome to Tim's List", MsgBoxStyle.OkCancel, "Welcome") = vbCancel Then
            Me.Close()
        End If

        pnlChoices.Left = pnlCategories.Bottom + shtSpace * 2
        pnlChoices.Top = pnlCategories.Bottom + shtSpace

        rtbDescription.Left = picItemDetail.Width + shtSpace
        rtbDescription.Top = pnlChoices.Bottom + shtSpace

        picItemDetail.Left = 2
        picItemDetail.Top = pnlChoices.Bottom + shtSpace

        FileOpen(1, cstrPathSource & "list.tlf", OpenMode.Input)

        Input(1, strTemp)
        intFiles = CInt(strTemp)

        For i = 0 To intFiles - 1
            Input(1, cstrFileName(i))
        Next i

        FileClose(1)

        For i = 0 To intFiles - 1
            FileOpen(1, cstrPathSource & cstrFileName(i), OpenMode.Input)

            Input(1, strTemp)
            cintFileLines(i) = CInt(strTemp)
            Input(1, cstrItemName(i))
            Input(1, strTemp)
            Input(1, cstrCategory(i))
            Input(1, cstrDetail(i))
            Input(1, cstrContactMethod(i))
            Input(1, strTemp)

            For j = 7 To cintFileLines(i) - 4

                cstrDescription(i) &= LineInput(1) & Chr(13)
            Next j

            Input(1, strTemp)
            cdblPrice(i) = CDbl(strTemp)
            Input(1, cstrContactInfo(i))
            Input(1, cstrPictureFile(i))
            FileClose(1)

        Next i

        For i = 0 To radCategoryItem.Count - 1

            radCategoryItem(i) = New RadioButton
            lblCategoryItem(i) = New Label

            Me.lstItems.Controls.Add(radCategoryItem(i))
            Me.lstItems.Controls.Add(lblCategoryItem(i))

            radCategoryItem(i).TextAlign = ContentAlignment.BottomCenter
            radCategoryItem(i).ForeColor = Color.Tomato

            radCategoryItem(i).Width = 150
            lblCategoryItem(i).Width = 125

            radCategoryItem(i).Height = 140
            lblCategoryItem(i).Height = 150

            radCategoryItem(i).Left = 10
            lblCategoryItem(i).Left = 10 + radCategoryItem(i).Right

            radCategoryItem(i).Top = i * 150
            lblCategoryItem(i).Top = i * 150

            radCategoryItem(i).Appearance = Appearance.Button

            radCategoryItem(i).Visible = True
            lblCategoryItem(i).Visible = True

            btnfull.Visible = False

            radCategoryItem(i).BackgroundImageLayout = ImageLayout.Stretch
            AddHandler radCategoryItem(i).Click, AddressOf onChange

        Next i

    End Sub

    Private Sub onChange(sender As Object, e As EventArgs)

        '=================================================================================
        'Description: This will give a short hook of the items for sale along with the
        '             with the image of the object for sale
        '=================================================================================

        Dim rad As RadioButton = CType(sender, RadioButton)
        Dim tagArray = CType(rad.Tag, String())

        rtbDescription.Text = CType(tagArray(0), String)
        picItemDetail.Image = Image.FromFile(tagArray(1))

    End Sub

    Private Sub btnCellularPhones_Click(sender As Object,
                                        e As EventArgs) Handles btnCellularPhones.Click

        '=================================================================================
        'Description: When the user clicks on btnCellularPhones, then a loop will start
        '             in which each item which comes under the category of Cellular Phones
        '             will display in pnlChoices in lstItems.
        '=================================================================================

        Dim str As String
        Dim i As Integer
        Dim j As Integer

        For i = 0 To cstrCategory.Length - 1

            If cstrCategory(i) = "Cellular Phones" Then

                lblCategoryItem(j).Text = cstrItemName(i) & Chr(13) & Chr(13) + cstrDetail(i)
                radCategoryItem(j).BackgroundImage = Image.FromFile(
                    cstrPathSource & cstrPictureFile(i))
                rtbDescription.Visible = True
                pnlChoices.Visible = True

                picItemDetail.Visible = True
                btnfull.Visible = True
                rtbDescription.Clear()
                picItemDetail.Image = Nothing

                str = cstrItemName(i) & Chr(13) & Chr(13)
                str += cstrDescription(i) & Chr(13) & Chr(13)
                str += " Price: " & "$" & cdblPrice(i) & Chr(13) & Chr(13)
                str += cstrContactMethod(i) & Chr(13) & Chr(13)
                str += cstrContactInfo(i) & Chr(13) & Chr(13)

                radCategoryItem(j).Tag = New String() {str, cstrPathSource & cstrPictureFile(i)}

                j += 1
            End If
        Next i


        lstItems.Height = radCategoryItem(j - 1).Top + 180
        vsbList.Maximum = j * radCategoryItem(j - 1).Height + 10
        vsbList.Value = 0
        lstItems.Top = vsbList.Value

    End Sub

    Private Sub btnAutomobiles_Click(sender As Object,
                                     e As EventArgs) Handles btnAutomobiles.Click

        '=================================================================================
        'Description: When the user clicks on btnAutomobiles, then a loop will start
        '             in which each item which comes under the category of Cellular Phones
        '             will display in pnlChoices in lstItems.
        '=================================================================================

        Dim str As String
        Dim i As Integer
        Dim j As Integer

        For i = 0 To cstrCategory.Length - 1
            If cstrCategory(i) = "Automobiles" Then
                radCategoryItem(j).BackgroundImage = Image.FromFile(
                    cstrPathSource & cstrPictureFile(i))
                lblCategoryItem(j).Text = cstrItemName(i) & Chr(13) & Chr(13) + cstrDetail(i)
                rtbDescription.Visible = True
                pnlChoices.Visible = True
                btnfull.Visible = True
                rtbDescription.Clear()
                picItemDetail.Visible = True
                picItemDetail.Image = Nothing

                str = cstrItemName(i) & Chr(13) & Chr(13)
                str += cstrDescription(i) & Chr(13) & Chr(13)
                str += " Price: " & "$" & cdblPrice(i) & Chr(13) & Chr(13)
                str += cstrContactMethod(i) & Chr(13) & Chr(13)
                str += cstrContactInfo(i) & Chr(13) & Chr(13)

                radCategoryItem(j).Tag = New String() {str, cstrPathSource & cstrPictureFile(i)}
                j += 1
            End If
        Next i

        lstItems.Height = radCategoryItem(j - 1).Top + 110
        vsbList.Maximum = j * radCategoryItem(j - 1).Height + 10
        vsbList.Value = 0
        lstItems.Top = vsbList.Value

    End Sub

    Private Sub btnFarm_Click(sender As Object, e As EventArgs) Handles btnFarm.Click

        '=================================================================================
        'Description: When the user will click on btnFarm, then a loop will start
        '             in which each item which comes under the category of Cellular Phones
        '             will display in pnlChoices in lstItems.
        '=================================================================================

        Dim str As String
        Dim i As Integer
        Dim j As Integer

        For i = 0 To cstrCategory.Length - 1
            If cstrCategory(i) = "Farm/Garden" Then
                radCategoryItem(j).BackgroundImage = Image.FromFile(
                    cstrPathSource & cstrPictureFile(i))
                lblCategoryItem(j).Text = cstrItemName(i) & Chr(13) & Chr(13) + cstrDetail(i)
                rtbDescription.Visible = True
                pnlChoices.Visible = True

                btnfull.Visible = True
                picItemDetail.Visible = True
                rtbDescription.Clear()
                picItemDetail.Image = Nothing
                str = cstrItemName(i) & Chr(13) & Chr(13)
                str += cstrDescription(i) & Chr(13) & Chr(13)
                str += " Price: " & "$" & cdblPrice(i) & Chr(13) & Chr(13)
                str += cstrContactMethod(i) & Chr(13) & Chr(13)
                str += cstrContactInfo(i) & Chr(13) & Chr(13)
                radCategoryItem(j).Tag = New String() {str, cstrPathSource & cstrPictureFile(i)}

                j += 1
            End If
        Next i

        lstItems.Height = radCategoryItem(j - 1).Top + 110
        vsbList.Maximum = j * radCategoryItem(j - 1).Height + 10
        vsbList.Value = 0
        lstItems.Top = vsbList.Value

    End Sub

    Private Sub btnFurniture_Click(sender As Object,
                                   e As EventArgs) Handles btnFurniture.Click

        '=================================================================================
        'Description: When the user will click on btnFurniture, then a loop will start
        '             in which each item which comes under the category of Cellular Phones
        '             will display in pnlChoices in lstItems.
        '=================================================================================

        Dim str As String
        Dim i As Integer
        Dim j As Integer

        For i = 0 To cstrCategory.Length - 1
            If cstrCategory(i) = "Furniture" Then
                radCategoryItem(j).BackgroundImage = Image.FromFile(
                    cstrPathSource & cstrPictureFile(i))
                lblCategoryItem(j).Text = cstrItemName(i) & Chr(13) & Chr(13) + cstrDetail(i)
                rtbDescription.Visible = True
                pnlChoices.Visible = True

                btnfull.Visible = True
                picItemDetail.Visible = True
                rtbDescription.Clear()
                picItemDetail.Image = Nothing
                str = cstrItemName(i) & Chr(13) & Chr(13)
                str += cstrDescription(i) & Chr(13) & Chr(13)
                str += " Price: " & "$" & cdblPrice(i) & Chr(13) & Chr(13)
                str += cstrContactMethod(i) & Chr(13) & Chr(13)
                str += cstrContactInfo(i) & Chr(13) & Chr(13)
                radCategoryItem(j).Tag = New String() {str, cstrPathSource & cstrPictureFile(i)}

                j += 1
            End If
        Next i

        lstItems.Height = radCategoryItem(j - 1).Top + 110
        vsbList.Maximum = j * radCategoryItem(j - 1).Height + 10
        vsbList.Value = 0
        lstItems.Top = vsbList.Value


    End Sub

    Private Sub btnLaptopComputers_Click(sender As Object,
                                         e As EventArgs) Handles btnLaptopComputers.Click

        '=================================================================================
        'Description: When the user will click on btnLaptopComputers, then a loop will start
        '             in which each item which comes under the category of Cellular Phones
        '             will display in pnlChoices in lstItems.
        '=================================================================================

        Dim str As String
        Dim i As Integer
        Dim j As Integer

        For i = 0 To cstrCategory.Length - 1
            If cstrCategory(i) = "Laptop Computers" Then
                radCategoryItem(j).BackgroundImage = Image.FromFile(
                    cstrPathSource & cstrPictureFile(i))
                lblCategoryItem(j).Text = cstrItemName(i) & Chr(13) & Chr(13) + cstrDetail(i)
                rtbDescription.Clear()
                picItemDetail.Image = Nothing
                rtbDescription.Visible = True
                pnlChoices.Visible = True

                picItemDetail.Visible = True
                btnfull.Visible = True
                str = cstrItemName(i) & Chr(13) & Chr(13)
                str += cstrDescription(i) & Chr(13) & Chr(13)
                str += " Price: " & "$" & cdblPrice(i) & Chr(13) & Chr(13)
                str += cstrContactMethod(i) & Chr(13) & Chr(13)
                str += cstrContactInfo(i) & Chr(13) & Chr(13)
                radCategoryItem(j).Tag = New String() {str, cstrPathSource & cstrPictureFile(i)}

                j += 1
            End If
        Next i

        lstItems.Height = radCategoryItem(j - 1).Top + 110
        vsbList.Maximum = j * radCategoryItem(j - 1).Height + 10
        vsbList.Value = 0
        lstItems.Top = vsbList.Value

    End Sub

    Private Sub btnSporting_Click(sender As Object, e As EventArgs) Handles btnSporting.Click

        '=================================================================================
        'Description: When the user will click on btnSporting, then a loop will start
        '             in which each item which comes under the category of Cellular Phones
        '             will display in pnlChoices in lstItems.
        '=================================================================================

        Dim str As String
        Dim i As Integer
        Dim j As Integer

        For i = 0 To cstrCategory.Length - 1
            If cstrCategory(i) = "Sporting" Then
                radCategoryItem(j).BackgroundImage = Image.FromFile(
                    cstrPathSource & cstrPictureFile(i))
                lblCategoryItem(j).Text = cstrItemName(i) & Chr(13) & Chr(13) + cstrDetail(i)
                pnlChoices.Visible = True
                rtbDescription.Visible = True


                picItemDetail.Visible = True
                btnfull.Visible = True
                rtbDescription.Clear()
                picItemDetail.Image = Nothing
                str = cstrItemName(i) & Chr(13) & Chr(13)
                str += cstrDescription(i) & Chr(13) & Chr(13)
                str += " Price: " & "$" & cdblPrice(i) & Chr(13) & Chr(13)
                str += cstrContactMethod(i) & Chr(13) & Chr(13)
                str += cstrContactInfo(i) & Chr(13) & Chr(13)
                radCategoryItem(j).Tag = New String() {str, cstrPathSource & cstrPictureFile(i)}

                j += 1

            End If
        Next i

        lstItems.Height = radCategoryItem(j - 1).Top + 110
        vsbList.Maximum = j * radCategoryItem(j - 1).Height + 10
        vsbList.Value = 0
        lstItems.Top = vsbList.Value

    End Sub

    Private Sub VScrollBar1_Scroll(sender As Object,
                                   e As ScrollEventArgs) Handles vsbList.Scroll

        '=================================================================================
        'Description: It will move the scroll bar verically by which user could see the
        '             items for sale
        '=================================================================================

        lstItems.Top = vsbList.Value * -1

    End Sub

    Private Sub btnFull_Click(sender As Object, e As EventArgs) Handles btnfull.Click

        '=================================================================================
        'Description: It will allow the user to switch between full screen and 
        '             normal screen of the item selected
        '=================================================================================

        If FullScreen = False Then

            btnfull.Text = "Normal"
            picItemDetail.Visible = True
            picItemDetail.Height = 677
            picItemDetail.Width = 916
            picItemDetail.Top = 0
            picItemDetail.Left = 0
            rtbDescription.Visible = False
            pnlChoices.Visible = False

            lblNames.Visible = False
            lblClass.Visible = False
            lblQuarter.Visible = False

            FullScreen = True
        Else
            btnfull.Text = " Full Screen "
            rtbDescription.Visible = True
            pnlChoices.Visible = True

            picItemDetail.Visible = True

            lblNames.Visible = True
            lblClass.Visible = True
            lblQuarter.Visible = True

            FullScreen = False
            picItemDetail.Left = 2
            picItemDetail.Top = pnlChoices.Bottom + shtSpace
            picItemDetail.Width = 278
            picItemDetail.Height = 185


        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click

        '=================================================================================
        'Description:  It will close the program with a message
        '=================================================================================

        MessageBox.Show("Thank you for choosing Tim's List!")
        Me.Close()
    End Sub

End Class

