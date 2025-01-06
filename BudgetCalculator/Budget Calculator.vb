' Overview: 
' - This Program is designed to calculate a Budget (Savings Plan) based on your Income, Taxes, and Expenses.
' - Income can be calculated on a Daily, or Weekly basis, and even supports Additional Income, also by a Daily, Weekly, Monthly, or Yearly basis.

Public Class frmMain

    ' Public Variables For Acquiring Income Values Per Frequency (Yearly, Monthly, Daily, Yearly) '
    Public dIRTY As String

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Gives Income Report ComboBoxes A Default Value
        cBoxDaysWeek.SelectedIndex = 4
        cBoxAddInc.SelectedIndex = 0

        ' Gives Budget Report ComboBoxes A Default Value
        cBoxExp1.SelectedIndex = 2
        cBoxExp2.SelectedIndex = 2
        cBoxExp3.SelectedIndex = 1
        cBoxExp4.SelectedIndex = 2
        cBoxExp5.SelectedIndex = 0

        cBoxSavings.SelectedIndex = 0

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub rbtnSourceIncNo_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnSourceIncNo.CheckedChanged
        ' Enable 2nd Question
        lblDailyWeekly.Enabled = True
        gBoxDailyWeekly.Enabled = True
        rbtnDaily.Enabled = True
        rbtnWeekly.Enabled = True

        ' Disable Additional Income '
        lblAddInc.Enabled = False
        lblAddIncPaid.Enabled = False
        cBoxAddInc.Enabled = False
        lblAddIncPaidEnterInc.Enabled = False
        tBoxAddInc.Enabled = False
        tBoxAddInc.Clear()
    End Sub

    Private Sub rbtnSourceIncYes_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnSourceIncYes.CheckedChanged
        ' Enable 2nd Question
        lblDailyWeekly.Enabled = True
        gBoxDailyWeekly.Enabled = True
        rbtnDaily.Enabled = True
        rbtnWeekly.Enabled = True

        ' Enable Additional Income '
        lblAddInc.Enabled = True
        lblAddIncPaid.Enabled = True
        cBoxAddInc.Enabled = True
        lblAddIncPaidEnterInc.Enabled = True
        tBoxAddInc.Enabled = True
        tBoxAddInc.Clear()
    End Sub

    Private Sub rbtnDaily_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnDaily.CheckedChanged
        ' Enable Income Input (Days)
        lblDays.Enabled = True
        lblDaysHours.Enabled = True
        lblDaysWeek.Enabled = True
        lblDaysIncome.Enabled = True
        tBoxDaysHours.Enabled = True
        cBoxDaysWeek.Enabled = True
        tBoxDaysIncome.Enabled = True

        tBoxDaysHours.Clear()
        tBoxDaysIncome.Clear()

        ' Disable Income Input (Weeks)
        lblWeeks.Enabled = False
        lblWeeksIncome.Enabled = False
        tBoxWeeksIncome.Enabled = False

        lblNote.Enabled = False
        lblNoteInfo.Enabled = False

        tBoxWeeksIncome.Clear()

        ' Enable Income Input (Additional) - If Additional Income & (Daily / Weekly) Are Checked
        If rbtnSourceIncYes.Checked = True AndAlso rbtnDaily.Checked = True Or rbtnSourceIncYes.Checked = True AndAlso rbtnWeekly.Checked = True Then
            lblAddInc.Enabled = True
            lblAddIncPaid.Enabled = True
            cBoxAddInc.Enabled = True
            lblAddIncPaidEnterInc.Enabled = True
            tBoxAddInc.Enabled = True
            tBoxAddInc.Clear()
        ElseIf rbtnSourceIncNo.Checked = True AndAlso rbtnDaily.Checked = True Or rbtnSourceIncNo.Checked = True AndAlso rbtnWeekly.Checked = True Then
            lblAddInc.Enabled = False
            lblAddIncPaid.Enabled = False
            cBoxAddInc.Enabled = False
            lblAddIncPaidEnterInc.Enabled = False
            tBoxAddInc.Enabled = False
            tBoxAddInc.Clear()
        End If

        ' Enable Generate Report Button
        btnGenerateIncRep.Enabled = True

        ' Enable Clear Button
        btnClearIncRep.Enabled = True
    End Sub

    Private Sub rbtnWeekly_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnWeekly.CheckedChanged
        ' Enable Income Input (Weeks)
        lblWeeks.Enabled = True
        lblWeeksIncome.Enabled = True
        tBoxWeeksIncome.Enabled = True

        lblNote.Enabled = True
        lblNoteInfo.Enabled = True

        tBoxWeeksIncome.Clear()

        ' Disable Income Input (Days)
        lblDays.Enabled = False
        lblDaysHours.Enabled = False
        lblDaysWeek.Enabled = False
        lblDaysIncome.Enabled = False
        tBoxDaysHours.Enabled = False
        cBoxDaysWeek.Enabled = False
        tBoxDaysIncome.Enabled = False

        tBoxDaysHours.Clear()
        tBoxDaysIncome.Clear()

        ' Enable Income Input (Additional) - If Additional Income & (Daily / Weekly) Are Checked
        If rbtnSourceIncYes.Checked = True AndAlso rbtnDaily.Checked = True Or rbtnSourceIncYes.Checked = True AndAlso rbtnWeekly.Checked = True Then
            lblAddInc.Enabled = True
            lblAddIncPaid.Enabled = True
            cBoxAddInc.Enabled = True
            lblAddIncPaidEnterInc.Enabled = True
            tBoxAddInc.Enabled = True
            tBoxAddInc.Clear()
        ElseIf rbtnSourceIncNo.Checked = True AndAlso rbtnDaily.Checked = True Or rbtnSourceIncNo.Checked = True AndAlso rbtnWeekly.Checked = True Then
            lblAddInc.Enabled = False
            lblAddIncPaid.Enabled = False
            cBoxAddInc.Enabled = False
            lblAddIncPaidEnterInc.Enabled = False
            tBoxAddInc.Enabled = False
            tBoxAddInc.Clear()
        End If

        ' Enable Generate Report Button
        btnGenerateIncRep.Enabled = True

        ' Enable Clear Button
        btnClearIncRep.Enabled = True
    End Sub

    Private Sub BtnGenerateIncRep_Click(sender As Object, e As EventArgs) Handles btnGenerateIncRep.Click

        ' Declare Income Input Variables (Days)
        Dim decDHW As Decimal 'Daily Hours Worked'
        Dim byteDPWW As Byte 'Days-Per-Week Worked'
        Dim decHIncome As Decimal 'Hourly Income'

        ' Declare Income Input Variables (Weeks)
        Dim decWIncome As Decimal 'Weekly Income'

        ' Declare Income Input Variables (Additional)
        Dim decAddInc As Decimal 'Additional Income'
        Dim sAddIncFreq As String 'Additional Income Frequency (Daily, Weekly, Monthly, Yearly)'




        ' Declare Conversion Variables
        Dim dHIncToDInc As Decimal 'Hourly Income --> Daily Income'
        Dim dDIncToHInc As Decimal 'Daily Income --> Hourly Income'

        Dim dDIncToWInc As Decimal 'Daily Income --> Weekly Income'
        Dim dWIncToDInc As Decimal 'Weekly Income --> Daily Income'

        Dim dWIncToMInc As Decimal 'Weekly Income --> Monthly Income'
        Dim dMIncToWInc As Decimal 'Monthly Income --> Weekly Income'

        Dim dMIncToYInc As Decimal 'Monthly Income --> Yearly Income'
        Dim dYIncToMInc As Decimal 'Yearly Income --> Monthly Income'

        ' Declare Income Output Variables (Income)
        Dim dHInc As Decimal
        Dim dDInc As Decimal
        Dim dWInc As Decimal
        Dim dMInc As Decimal
        Dim dYInc As Decimal

        ' Declare Income Output Variables (Additional)
        Dim dAHInc As Decimal
        Dim dADInc As Decimal
        Dim dAWInc As Decimal
        Dim dAMInc As Decimal
        Dim dAYInc As Decimal

        ' Declare Income Output Variables (Income + Additional)
        Dim dAddedHInc As Decimal
        Dim dAddedDInc As Decimal
        Dim dAddedWInc As Decimal
        Dim dAddedMInc As Decimal
        Dim dAddedYInc As Decimal



        ' Retrieve Values From Textboxes and Comboboxes in Days Section
        Decimal.TryParse(tBoxDaysHours.Text, decDHW)
        Byte.TryParse(cBoxDaysWeek.Text, byteDPWW)
        Decimal.TryParse(tBoxDaysIncome.Text, decHIncome)

        ' Retrieve Values From Textboxes and Comboboxes in Weeks Section
        Decimal.TryParse(tBoxWeeksIncome.Text, decWIncome)









        ' Initialize Conversion Variables (For Both Sections ("Days" and "Weeks") of the Income Calculator) '
        dWIncToMInc = 4.34524
        dMIncToWInc = 1 / dWIncToMInc

        dMIncToYInc = 12
        dYIncToMInc = 1 / dMIncToYInc





        ' Initialize Conversion Variables (For ONLY the "Weeks" Section of the Income Calculator) (See Note #1 For More Info) 
        dWIncToDInc = (1 / 5) 'Weekly Income --> Daily Income (Assumes Standard 5 Day Work Week)'
        dDIncToWInc = 5 'Daily Income --> Weekly Income (Assumes Standard 5 Day Work Week)'

        dDIncToHInc = (1 / 8) 'Daily Income --> Hourly Income (Assumes Standard 8 Hour Day)'
        dHIncToDInc = 8 'Hourly Income --> Daily Income (Assumes Standard 8 Hour Day)'








        ' Set Empty Textboxes and Comboboxes to Default Values (If No User Input Detected)
        If String.IsNullOrEmpty(tBoxDaysHours.Text) Then
            tBoxDaysHours.Text = 0
        End If
        If String.IsNullOrEmpty(tBoxDaysIncome.Text) Then
            tBoxDaysIncome.Text = 0
        End If
        If String.IsNullOrEmpty(tBoxWeeksIncome.Text) Then
            tBoxWeeksIncome.Text = 0
        End If
        If String.IsNullOrEmpty(tBoxAddInc.Text) Then
            tBoxAddInc.Text = 0
        End If





        ' Calculate Income Output Variables (Days) - Without Additional
        If lblDays.Enabled = True AndAlso lblAddInc.Enabled = False Then
            dHInc = decHIncome
            dDInc = dHInc * decDHW
            dWInc = dDInc * byteDPWW
            dMInc = dWInc * dWIncToMInc
            dYInc = dMInc * dMIncToYInc
        End If

        ' Calculate Income Output Variables (Weeks) - Without Additional
        If lblWeeks.Enabled = True AndAlso lblAddInc.Enabled = False Then
            dWInc = decWIncome
            dMInc = dWInc * dWIncToMInc
            dYInc = dMInc * dMIncToYInc

            dDInc = dWInc * dWIncToDInc
            dHInc = dDInc * dDIncToHInc
        End If





        ' Calculate Income Output Variables - With Additional
        If lblAddInc.Enabled = True AndAlso lblDays.Enabled = True Or lblAddInc.Enabled = True AndAlso lblWeeks.Enabled = True Then

            ' Daily Income Calculation (Repeated)
            If lblDays.Enabled = True Then
                dHInc = decHIncome
                dDInc = dHInc * decDHW
                dWInc = dDInc * byteDPWW
                dMInc = dWInc * dWIncToMInc
                dYInc = dMInc * dMIncToYInc
            End If

            ' Weekly Income Calculation (Repeated)
            If lblWeeks.Enabled = True Then
                dWInc = decWIncome
                dMInc = dWInc * dWIncToMInc
                dYInc = dMInc * dMIncToYInc

                dDInc = dWInc * dWIncToDInc
                dHInc = dDInc * dDIncToHInc
            End If

            ' Retrieve Values From Textboxes and Comboboxes in Additional Section
            sAddIncFreq = cBoxAddInc.SelectedItem.ToString()
            Decimal.TryParse(tBoxAddInc.Text, decAddInc)





            ' Calculate Additional Income (Based on Daily, Weekly, Monthly, Yearly)
            If sAddIncFreq = "Daily" Then

                ' Additional Income Calculation (Daily)
                dADInc = decAddInc
                dAWInc = dADInc * dDIncToWInc
                dAMInc = dAWInc * dWIncToMInc
                dAYInc = dAMInc * dMIncToYInc

                dAHInc = dADInc * dDIncToHInc

            ElseIf sAddIncFreq = "Weekly" Then

                ' Additional Income Calculation (Weekly)
                dAWInc = decAddInc
                dAMInc = dAWInc * dWIncToMInc
                dAYInc = dAMInc * dMIncToYInc

                dADInc = dAWInc * dWIncToDInc
                dAHInc = dADInc * dDIncToHInc

            ElseIf sAddIncFreq = "Monthly" Then

                ' Additional Income Calculation (Monthly)
                dAMInc = decAddInc
                dAYInc = dAMInc * dMIncToYInc

                dAWInc = dAMInc * dMIncToWInc
                dADInc = dAWInc * dWIncToDInc
                dAHInc = dADInc * dDIncToHInc

            ElseIf sAddIncFreq = "Yearly" Then

                ' Additional Income Calculation (Yearly)
                dAYInc = decAddInc
                dAMInc = dAYInc * dYIncToMInc
                dAWInc = dAMInc * dMIncToWInc
                dADInc = dAWInc * dWIncToDInc
                dAHInc = dADInc * dDIncToHInc

            End If

            ' Adding Original Income + Additional Income (Calculations)
            dAddedHInc = dHInc + dAHInc
            dAddedDInc = dDInc + dADInc
            dAddedWInc = dWInc + dAWInc
            dAddedMInc = dMInc + dAMInc
            dAddedYInc = dYInc + dAYInc

        End If






        ' Display Income Report (Excluding Additional) - No Tax
        If lblAddInc.Enabled = False Then
            tBoxIncRepHourly.Text = dHInc.ToString("C2")
            tBoxIncRepDaily.Text = dDInc.ToString("C2")
            tBoxIncRepWeekly.Text = dWInc.ToString("C2")
            tBoxIncRepMonthly.Text = dMInc.ToString("C2")
            tBoxIncRepYearly.Text = dYInc.ToString("C2")
        End If


        ' Display Income Report (Additional) - No Tax
        If lblAddInc.Enabled = True Then
            tBoxIncRepHourly.Text = dAddedHInc.ToString("C2")
            tBoxIncRepDaily.Text = dAddedDInc.ToString("C2")
            tBoxIncRepWeekly.Text = dAddedWInc.ToString("C2")
            tBoxIncRepMonthly.Text = dAddedMInc.ToString("C2")
            tBoxIncRepYearly.Text = dAddedYInc.ToString("C2")
        End If

        ' Calculate Income Report (Taxed)
        Dim dTaxYInc As Decimal
        Dim dTaxMInc As Decimal
        Dim dTaxWInc As Decimal
        Dim dTaxDInc As Decimal
        Dim dTaxHInc As Decimal

        ' Create Tax Brackets '
        Dim aTaxBracket() = New Decimal() {0.1, 0.12, 0.22, 0.24, 0.32, 0.35, 0.37}

        ' Reset Tax Bracket Textbox Colors '
        lblTaxPercent1.BackColor = SystemColors.Info
        lblTaxPercent2.BackColor = SystemColors.Info
        lblTaxPercent3.BackColor = SystemColors.Info
        lblTaxPercent4.BackColor = SystemColors.Info
        lblTaxPercent5.BackColor = SystemColors.Info
        lblTaxPercent6.BackColor = SystemColors.Info
        lblTaxPercent7.BackColor = SystemColors.Info


        ' Calculates Income Report (Taxed) - Based On If Additional Is Enabled / Disabled
        ' Also Highlights The Tax Bracket That (Yearly Income) Matches '
        If lblAddInc.Enabled = False Then
            If dYInc >= 0 And dYInc <= 9700 Then
                dTaxYInc = dYInc - (dYInc * aTaxBracket(0))
                dTaxMInc = dMInc - (dMInc * aTaxBracket(0))
                dTaxWInc = dWInc - (dWInc * aTaxBracket(0))
                dTaxDInc = dDInc - (dDInc * aTaxBracket(0))
                dTaxHInc = dHInc - (dHInc * aTaxBracket(0))
                lblTaxPercent1.BackColor = Color.LightSteelBlue ' Highlight Correlating Tax Bracket '
            ElseIf dYInc >= 9701 And dYInc <= 39475 Then
                dTaxYInc = dYInc - (dYInc * aTaxBracket(1))
                dTaxMInc = dMInc - (dMInc * aTaxBracket(1))
                dTaxWInc = dWInc - (dWInc * aTaxBracket(1))
                dTaxDInc = dDInc - (dDInc * aTaxBracket(1))
                dTaxHInc = dHInc - (dHInc * aTaxBracket(1))
                lblTaxPercent2.BackColor = Color.LightSteelBlue ' Highlight Correlating Tax Bracket '
            ElseIf dYInc >= 39476 And dYInc <= 84200 Then
                dTaxYInc = dYInc - (dYInc * aTaxBracket(2))
                dTaxMInc = dMInc - (dMInc * aTaxBracket(2))
                dTaxWInc = dWInc - (dWInc * aTaxBracket(2))
                dTaxDInc = dDInc - (dDInc * aTaxBracket(2))
                dTaxHInc = dHInc - (dHInc * aTaxBracket(2))
                lblTaxPercent3.BackColor = Color.LightSteelBlue ' Highlight Correlating Tax Bracket '
            ElseIf dYInc >= 84201 And dYInc <= 160725 Then
                dTaxYInc = dYInc - (dYInc * aTaxBracket(3))
                dTaxMInc = dMInc - (dMInc * aTaxBracket(3))
                dTaxWInc = dWInc - (dWInc * aTaxBracket(3))
                dTaxDInc = dDInc - (dDInc * aTaxBracket(3))
                dTaxHInc = dHInc - (dHInc * aTaxBracket(3))
                lblTaxPercent4.BackColor = Color.LightSteelBlue ' Highlight Correlating Tax Bracket '
            ElseIf dYInc >= 160726 And dYInc <= 204100 Then
                dTaxYInc = dYInc - (dYInc * aTaxBracket(4))
                dTaxMInc = dMInc - (dMInc * aTaxBracket(4))
                dTaxWInc = dWInc - (dWInc * aTaxBracket(4))
                dTaxDInc = dDInc - (dDInc * aTaxBracket(4))
                dTaxHInc = dHInc - (dHInc * aTaxBracket(4))
                lblTaxPercent5.BackColor = Color.LightSteelBlue ' Highlight Correlating Tax Bracket '
            ElseIf dYInc >= 204101 And dYInc <= 510300 Then
                dTaxYInc = dYInc - (dYInc * aTaxBracket(5))
                dTaxMInc = dMInc - (dMInc * aTaxBracket(5))
                dTaxWInc = dWInc - (dWInc * aTaxBracket(5))
                dTaxDInc = dDInc - (dDInc * aTaxBracket(5))
                dTaxHInc = dHInc - (dHInc * aTaxBracket(5))
                lblTaxPercent6.BackColor = Color.LightSteelBlue ' Highlight Correlating Tax Bracket '
            ElseIf dYInc >= 510301 Then
                dTaxYInc = dYInc - (dYInc * aTaxBracket(6))
                dTaxMInc = dMInc - (dMInc * aTaxBracket(6))
                dTaxWInc = dWInc - (dWInc * aTaxBracket(6))
                dTaxDInc = dDInc - (dDInc * aTaxBracket(6))
                dTaxHInc = dHInc - (dHInc * aTaxBracket(6))
                lblTaxPercent7.BackColor = Color.LightSteelBlue ' Highlight Correlating Tax Bracket '
            End If
        ElseIf lblAddInc.Enabled = True Then
            If dAddedYInc >= 0 And dAddedYInc <= 9700 Then
                dTaxYInc = dAddedYInc - (dAddedYInc * aTaxBracket(0))
                dTaxMInc = dAddedMInc - (dAddedMInc * aTaxBracket(0))
                dTaxWInc = dAddedWInc - (dAddedWInc * aTaxBracket(0))
                dTaxDInc = dAddedDInc - (dAddedDInc * aTaxBracket(0))
                dTaxHInc = dAddedHInc - (dAddedHInc * aTaxBracket(0))
                lblTaxPercent1.BackColor = Color.LightSteelBlue ' Highlight Correlating Tax Bracket '
            ElseIf dAddedYInc >= 9701 And dAddedYInc <= 39475 Then
                dTaxYInc = dAddedYInc - (dAddedYInc * aTaxBracket(1))
                dTaxMInc = dAddedMInc - (dAddedMInc * aTaxBracket(1))
                dTaxWInc = dAddedWInc - (dAddedWInc * aTaxBracket(1))
                dTaxDInc = dAddedDInc - (dAddedDInc * aTaxBracket(1))
                dTaxHInc = dAddedHInc - (dAddedHInc * aTaxBracket(1))
                lblTaxPercent2.BackColor = Color.LightSteelBlue ' Highlight Correlating Tax Bracket '
            ElseIf dAddedYInc >= 39476 And dAddedYInc <= 84200 Then
                dTaxYInc = dAddedYInc - (dAddedYInc * aTaxBracket(2))
                dTaxMInc = dAddedMInc - (dAddedMInc * aTaxBracket(2))
                dTaxWInc = dAddedWInc - (dAddedWInc * aTaxBracket(2))
                dTaxDInc = dAddedDInc - (dAddedDInc * aTaxBracket(2))
                dTaxHInc = dAddedHInc - (dAddedHInc * aTaxBracket(2))
                lblTaxPercent3.BackColor = Color.LightSteelBlue ' Highlight Correlating Tax Bracket '
            ElseIf dAddedYInc >= 84201 And dAddedYInc <= 160725 Then
                dTaxYInc = dAddedYInc - (dAddedYInc * aTaxBracket(3))
                dTaxMInc = dAddedMInc - (dAddedMInc * aTaxBracket(3))
                dTaxWInc = dAddedWInc - (dAddedWInc * aTaxBracket(3))
                dTaxDInc = dAddedDInc - (dAddedDInc * aTaxBracket(3))
                dTaxHInc = dAddedHInc - (dAddedHInc * aTaxBracket(3))
                lblTaxPercent4.BackColor = Color.LightSteelBlue ' Highlight Correlating Tax Bracket '
            ElseIf dAddedYInc >= 160726 And dAddedYInc <= 204100 Then
                dTaxYInc = dAddedYInc - (dAddedYInc * aTaxBracket(4))
                dTaxMInc = dAddedMInc - (dAddedMInc * aTaxBracket(4))
                dTaxWInc = dAddedWInc - (dAddedWInc * aTaxBracket(4))
                dTaxDInc = dAddedDInc - (dAddedDInc * aTaxBracket(4))
                dTaxHInc = dAddedHInc - (dAddedHInc * aTaxBracket(4))
                lblTaxPercent5.BackColor = Color.LightSteelBlue ' Highlight Correlating Tax Bracket '
            ElseIf dAddedYInc >= 204101 And dAddedYInc <= 510300 Then
                dTaxYInc = dAddedYInc - (dAddedYInc * aTaxBracket(5))
                dTaxMInc = dAddedMInc - (dAddedMInc * aTaxBracket(5))
                dTaxWInc = dAddedWInc - (dAddedWInc * aTaxBracket(5))
                dTaxDInc = dAddedDInc - (dAddedDInc * aTaxBracket(5))
                dTaxHInc = dAddedHInc - (dAddedHInc * aTaxBracket(5))
                lblTaxPercent6.BackColor = Color.LightSteelBlue ' Highlight Correlating Tax Bracket '
            ElseIf dAddedYInc >= 510301 Then
                dTaxYInc = dAddedYInc - (dAddedYInc * aTaxBracket(6))
                dTaxMInc = dAddedMInc - (dAddedMInc * aTaxBracket(6))
                dTaxWInc = dAddedWInc - (dAddedWInc * aTaxBracket(6))
                dTaxDInc = dAddedDInc - (dAddedDInc * aTaxBracket(6))
                dTaxHInc = dAddedHInc - (dAddedHInc * aTaxBracket(6))
                lblTaxPercent7.BackColor = Color.LightSteelBlue ' Highlight Correlating Tax Bracket '
            End If
        End If

        ' Display Income Report (Taxed)
        tBoxIncRepAfterTaxYearly.Text = dTaxYInc.ToString("C2")
        tBoxIncRepAfterTaxMonthly.Text = dTaxMInc.ToString("C2")
        tBoxIncRepAfterTaxWeekly.Text = dTaxWInc.ToString("C2")
        tBoxIncRepAfterTaxDaily.Text = dTaxDInc.ToString("C2")
        tBoxIncRepAfterTaxHourly.Text = dTaxHInc.ToString("C2")

        ' Pass Income Report (Taxed) Values To Public Variables
        dIRTY = tBoxIncRepAfterTaxYearly.Text


        ' Enable Expenses Report
        lblExp.Enabled = True
        lblExp1.Enabled = True
        lblExp2.Enabled = True
        lblExp3.Enabled = True
        lblExp4.Enabled = True
        lblExp5.Enabled = True
        tBoxExp1.Enabled = True
        tBoxExp2.Enabled = True
        tBoxExp3.Enabled = True
        tBoxExp4.Enabled = True
        tBoxExp5.Enabled = True
        cBoxExp1.Enabled = True
        cBoxExp2.Enabled = True
        cBoxExp3.Enabled = True
        cBoxExp4.Enabled = True
        cBoxExp5.Enabled = True

        ' Enable Savings
        lblSavings.Enabled = True
        lblSave.Enabled = True
        gBoxSavings.Enabled = True
        rbtnSavingsYes.Enabled = True
        rbtnSavingsNo.Enabled = True

        ' Enable Buttons (Budget Report)
        btnGenerateBudgetRep.Enabled = True
        btnClearBudgetRep.Enabled = True

        ' Enable Total Expenses Report
        lblTotalAvgExp.Enabled = True
        lblTotalAvgExpDaily.Enabled = True
        lblTotalAvgExpWeekly.Enabled = True
        lblTotalAvgExpMonthly.Enabled = True
        lblTotalAvgExpYearly.Enabled = True
        tBoxTotalAvgExpDaily.Enabled = True
        tBoxTotalAvgExpWeekly.Enabled = True
        tBoxTotalAvgExpMonthly.Enabled = True
        tBoxTotalAvgExpYearly.Enabled = True

        ' Enable Budget Report
        lblIncAfterTaxesAndExpenses.Enabled = True
        lblIncAfterTaxesAndExpensesDaily.Enabled = True
        lblIncAfterTaxesAndExpensesWeekly.Enabled = True
        lblIncAfterTaxesAndExpensesMonthly.Enabled = True
        lblIncAfterTaxesAndExpensesYearly.Enabled = True
        tBoxIncAfterTaxesAndExpensesDaily.Enabled = True
        tBoxIncAfterTaxesAndExpensesWeekly.Enabled = True
        tBoxIncAfterTaxesAndExpensesMonthly.Enabled = True
        tBoxIncAfterTaxesAndExpensesYearly.Enabled = True

    End Sub

    Private Sub btnClearIncRep_Click(sender As Object, e As EventArgs) Handles btnClearIncRep.Click

        ' Clear Text Boxes
        tBoxDaysHours.Clear()
        tBoxDaysIncome.Clear()
        tBoxWeeksIncome.Clear()
        tBoxAddInc.Clear()

        ' Clear Income Report
        tBoxIncRepHourly.Clear()
        tBoxIncRepDaily.Clear()
        tBoxIncRepWeekly.Clear()
        tBoxIncRepMonthly.Clear()
        tBoxIncRepYearly.Clear()

        ' Clear Income Report (Taxed)
        tBoxIncRepAfterTaxHourly.Clear()
        tBoxIncRepAfterTaxDaily.Clear()
        tBoxIncRepAfterTaxWeekly.Clear()
        tBoxIncRepAfterTaxMonthly.Clear()
        tBoxIncRepAfterTaxYearly.Clear()

        ' Reset Tax Bracket Textbox Colors '
        lblTaxPercent1.BackColor = SystemColors.Info
        lblTaxPercent2.BackColor = SystemColors.Info
        lblTaxPercent3.BackColor = SystemColors.Info
        lblTaxPercent4.BackColor = SystemColors.Info
        lblTaxPercent5.BackColor = SystemColors.Info
        lblTaxPercent6.BackColor = SystemColors.Info
        lblTaxPercent7.BackColor = SystemColors.Info
    End Sub



    Private Sub btnGenerateBudgetRep_Click(sender As Object, e As EventArgs) Handles btnGenerateBudgetRep.Click
        ' Set Empty Textboxes and Comboboxes to Default Values (If No User Input Detected)
        If String.IsNullOrEmpty(tBoxExp1.Text) Then
            tBoxExp1.Text = 0
        End If
        If String.IsNullOrEmpty(tBoxExp2.Text) Then
            tBoxExp2.Text = 0
        End If
        If String.IsNullOrEmpty(tBoxExp3.Text) Then
            tBoxExp3.Text = 0
        End If
        If String.IsNullOrEmpty(tBoxExp4.Text) Then
            tBoxExp4.Text = 0
        End If
        If String.IsNullOrEmpty(tBoxExp5.Text) Then
            tBoxExp5.Text = 0
        End If

        ' Declare Variables For Expenses
        Dim dExp1 As Decimal
        Dim dExp2 As Decimal
        Dim dExp3 As Decimal
        Dim dExp4 As Decimal
        Dim dExp5 As Decimal

        Dim dTotalDExp As Decimal
        Dim dTotalWExp As Decimal
        Dim dTotalMExp As Decimal
        Dim dTotalYExp As Decimal

        Dim dTotalCashRemDaily As Decimal
        Dim dTotalCashRemWeekly As Decimal
        Dim dTotalCashRemMonthly As Decimal
        Dim dTotalCashRemYearly As Decimal

        Dim dSaveAmount As Decimal
        Dim dSaveAmountDaily As Decimal
        Dim dSaveAmountWeekly As Decimal
        Dim dSaveAmountMonthly As Decimal
        Dim dSaveAmountYearly As Decimal

        Dim dSaveAmountTotalDaily As Decimal
        Dim dSaveAmountTotalWeekly As Decimal
        Dim dSaveAmountTotalMonthly As Decimal
        Dim dSaveAmountTotalYearly As Decimal

        ' Declare Conversion Variables For Savings
        Dim dSADToSAW As Decimal
        Dim dSAWToSAM As Decimal
        Dim dSAMToSAY As Decimal

        Dim dSAYToSAM As Decimal
        Dim dSAMToSAW As Decimal
        Dim dSAWToSAD As Decimal

        ' Initialize Conversion Variables For Savings
        dSADToSAW = 7
        dSAWToSAM = 4.34524
        dSAMToSAY = 12

        dSAYToSAM = (1 / dSAMToSAY)
        dSAMToSAW = (1 / dSAWToSAM)
        dSAWToSAD = (1 / dSADToSAW)

        ' Declare Conversion Variables For Expenses
        Dim dDExpToWExp As Decimal
        Dim dWExpToMExp As Decimal
        Dim dMExpToYExp As Decimal

        Dim dYExpToMExp As Decimal
        Dim dMExpToWExp As Decimal
        Dim dWExpToDExp As Decimal

        ' Initialize Conversion Variables For Expenses
        dDExpToWExp = 7
        dWExpToMExp = 4.34524
        dMExpToYExp = 12

        dYExpToMExp = 1 / dMExpToYExp
        dMExpToWExp = 1 / dWExpToMExp
        dWExpToDExp = 1 / dDExpToWExp

        ' Retrieve Values For Expenses
        Decimal.TryParse(tBoxExp1.Text, dExp1)
        Decimal.TryParse(tBoxExp2.Text, dExp2)
        Decimal.TryParse(tBoxExp3.Text, dExp3)
        Decimal.TryParse(tBoxExp4.Text, dExp4)
        Decimal.TryParse(tBoxExp5.Text, dExp5)

        ' Calculate Expenses Per Frequency - (Expense 1)
        If cBoxExp1.SelectedItem.ToString() = "Daily" Then ' If 'Daily' Selected In Dropdown Menu (Expense 1), Then... '
            dTotalDExp = dExp1

            dTotalWExp = dTotalDExp * dDExpToWExp
            dTotalMExp = dTotalWExp * dWExpToMExp
            dTotalYExp = dTotalMExp * dMExpToYExp
        ElseIf cBoxExp1.SelectedItem.ToString() = "Weekly" Then ' If 'Weekly' Selected In Dropdown Menu (Expense 1), Then... '
            dTotalWExp = dExp1
            dTotalDExp = dTotalWExp * dWExpToDExp

            dTotalMExp = dTotalWExp * dWExpToMExp
            dTotalYExp = dTotalMExp * dMExpToYExp
        ElseIf cBoxExp1.SelectedItem.ToString() = "Monthly" Then ' If 'Monthly' Selected In Dropdown Menu (Expense 1), Then... '
            dTotalMExp = dExp1
            dTotalWExp = dTotalMExp * dMExpToWExp
            dTotalDExp = dTotalWExp * dWExpToDExp

            dTotalYExp = dTotalMExp * dMExpToYExp
        ElseIf cBoxExp1.SelectedItem.ToString() = "Yearly" Then ' If 'Yearly' Selected In Dropdown Menu (Expense 1), Then... '
            dTotalYExp = dExp1
            dTotalMExp = dTotalYExp * dYExpToMExp
            dTotalWExp = dTotalMExp * dMExpToWExp
            dTotalDExp = dTotalWExp * dWExpToDExp
        End If

        ' Calculate Expenses Per Frequency - (Expense 2) - Adds TotalExp's From Previous cBox Nested If
        If cBoxExp2.SelectedItem.ToString() = "Daily" Then ' If 'Daily' Selected In Dropdown Menu (Expense 2), Then... '
            dTotalDExp = dExp2 + dTotalDExp

            dTotalWExp = dTotalDExp * dDExpToWExp
            dTotalMExp = dTotalWExp * dWExpToMExp
            dTotalYExp = dTotalMExp * dMExpToYExp
        ElseIf cBoxExp2.SelectedItem.ToString() = "Weekly" Then ' If 'Weekly' Selected In Dropdown Menu (Expense 2), Then... '
            dTotalWExp = dExp2 + dTotalWExp
            dTotalDExp = dTotalWExp * dWExpToDExp

            dTotalMExp = dTotalWExp * dWExpToMExp
            dTotalYExp = dTotalMExp * dMExpToYExp
        ElseIf cBoxExp2.SelectedItem.ToString() = "Monthly" Then ' If 'Monthly' Selected In Dropdown Menu (Expense 2), Then... '
            dTotalMExp = dExp2 + dTotalMExp
            dTotalWExp = dTotalMExp * dMExpToWExp
            dTotalDExp = dTotalWExp * dWExpToDExp

            dTotalYExp = dTotalMExp * dMExpToYExp
        ElseIf cBoxExp2.SelectedItem.ToString() = "Yearly" Then ' If 'Yearly' Selected In Dropdown Menu (Expense 2), Then... '
            dTotalYExp = dExp2 + dTotalYExp
            dTotalMExp = dTotalYExp * dYExpToMExp
            dTotalWExp = dTotalMExp * dMExpToWExp
            dTotalDExp = dTotalWExp * dWExpToDExp
        End If

        ' Calculate Expenses Per Frequency - (Expense 3) - Adds TotalExp's From Previous cBox Nested If
        If cBoxExp3.SelectedItem.ToString() = "Daily" Then ' If 'Daily' Selected In Dropdown Menu (Expense 3), Then... '
            dTotalDExp = dExp3 + dTotalDExp

            dTotalWExp = dTotalDExp * dDExpToWExp
            dTotalMExp = dTotalWExp * dWExpToMExp
            dTotalYExp = dTotalMExp * dMExpToYExp
        ElseIf cBoxExp3.SelectedItem.ToString() = "Weekly" Then ' If 'Weekly' Selected In Dropdown Menu (Expense 3), Then... '
            dTotalWExp = dExp3 + dTotalWExp
            dTotalDExp = dTotalWExp * dWExpToDExp

            dTotalMExp = dTotalWExp * dWExpToMExp
            dTotalYExp = dTotalMExp * dMExpToYExp
        ElseIf cBoxExp3.SelectedItem.ToString() = "Monthly" Then ' If 'Monthly' Selected In Dropdown Menu (Expense 3), Then... '
            dTotalMExp = dExp3 + dTotalMExp
            dTotalWExp = dTotalMExp * dMExpToWExp
            dTotalDExp = dTotalWExp * dWExpToDExp

            dTotalYExp = dTotalMExp * dMExpToYExp
        ElseIf cBoxExp3.SelectedItem.ToString() = "Yearly" Then ' If 'Yearly' Selected In Dropdown Menu (Expense 3), Then... '
            dTotalYExp = dExp3 + dTotalYExp
            dTotalMExp = dTotalYExp * dYExpToMExp
            dTotalWExp = dTotalMExp * dMExpToWExp
            dTotalDExp = dTotalWExp * dWExpToDExp
        End If

        ' Calculate Expenses Per Frequency - (Expense 4) - Adds TotalExp's From Previous cBox Nested If
        If cBoxExp4.SelectedItem.ToString() = "Daily" Then ' If 'Daily' Selected In Dropdown Menu (Expense 4), Then... '
            dTotalDExp = dExp4 + dTotalDExp

            dTotalWExp = dTotalDExp * dDExpToWExp
            dTotalMExp = dTotalWExp * dWExpToMExp
            dTotalYExp = dTotalMExp * dMExpToYExp
        ElseIf cBoxExp4.SelectedItem.ToString() = "Weekly" Then ' If 'Weekly' Selected In Dropdown Menu (Expense 4), Then... '
            dTotalWExp = dExp4 + dTotalWExp
            dTotalDExp = dTotalWExp * dWExpToDExp

            dTotalMExp = dTotalWExp * dWExpToMExp
            dTotalYExp = dTotalMExp * dMExpToYExp
        ElseIf cBoxExp4.SelectedItem.ToString() = "Monthly" Then ' If 'Monthly' Selected In Dropdown Menu (Expense 4), Then... '
            dTotalMExp = dExp4 + dTotalMExp
            dTotalWExp = dTotalMExp * dMExpToWExp
            dTotalDExp = dTotalWExp * dWExpToDExp

            dTotalYExp = dTotalMExp * dMExpToYExp
        ElseIf cBoxExp4.SelectedItem.ToString() = "Yearly" Then ' If 'Yearly' Selected In Dropdown Menu (Expense 4), Then... '
            dTotalYExp = dExp4 + dTotalYExp
            dTotalMExp = dTotalYExp * dYExpToMExp
            dTotalWExp = dTotalMExp * dMExpToWExp
            dTotalDExp = dTotalWExp * dWExpToDExp
        End If

        ' Calculate Expenses Per Frequency - (Expense 5) - Adds TotalExp's From Previous cBox Nested If
        If cBoxExp5.SelectedItem.ToString() = "Daily" Then ' If 'Daily' Selected In Dropdown Menu (Expense 5), Then... '
            dTotalDExp = dExp5 + dTotalDExp

            dTotalWExp = dTotalDExp * dDExpToWExp
            dTotalMExp = dTotalWExp * dWExpToMExp
            dTotalYExp = dTotalMExp * dMExpToYExp
        ElseIf cBoxExp5.SelectedItem.ToString() = "Weekly" Then ' If 'Weekly' Selected In Dropdown Menu (Expense 5), Then... '
            dTotalWExp = dExp5 + dTotalWExp
            dTotalDExp = dTotalWExp * dWExpToDExp

            dTotalMExp = dTotalWExp * dWExpToMExp
            dTotalYExp = dTotalMExp * dMExpToYExp
        ElseIf cBoxExp5.SelectedItem.ToString() = "Monthly" Then ' If 'Monthly' Selected In Dropdown Menu (Expense 5), Then... '
            dTotalMExp = dExp5 + dTotalMExp
            dTotalWExp = dTotalMExp * dMExpToWExp
            dTotalDExp = dTotalWExp * dWExpToDExp

            dTotalYExp = dTotalMExp * dMExpToYExp
        ElseIf cBoxExp5.SelectedItem.ToString() = "Yearly" Then ' If 'Yearly' Selected In Dropdown Menu (Expense 5), Then... '
            dTotalYExp = dExp5 + dTotalYExp
            dTotalMExp = dTotalYExp * dYExpToMExp
            dTotalWExp = dTotalMExp * dMExpToWExp
            dTotalDExp = dTotalWExp * dWExpToDExp
        End If

        ' Display Total Expenses Report
        tBoxTotalAvgExpDaily.Text = dTotalDExp.ToString("C2")
        tBoxTotalAvgExpWeekly.Text = dTotalWExp.ToString("C2")
        tBoxTotalAvgExpMonthly.Text = dTotalMExp.ToString("C2")
        tBoxTotalAvgExpYearly.Text = dTotalYExp.ToString("C2")

        ' Calculate Budget Report
        dTotalCashRemYearly = CDec(dIRTY) - dTotalYExp
        dTotalCashRemMonthly = dTotalCashRemYearly / 12
        dTotalCashRemWeekly = dTotalCashRemMonthly / 4.34524
        dTotalCashRemDaily = dTotalCashRemWeekly / 7

        ' Display Budget Report
        tBoxIncAfterTaxesAndExpensesDaily.Text = dTotalCashRemDaily.ToString("C2")
        tBoxIncAfterTaxesAndExpensesWeekly.Text = dTotalCashRemWeekly.ToString("C2")
        tBoxIncAfterTaxesAndExpensesMonthly.Text = dTotalCashRemMonthly.ToString("C2")
        tBoxIncAfterTaxesAndExpensesYearly.Text = dTotalCashRemYearly.ToString("C2")

        ' Savings
        If rbtnSavingsYes.Checked = True Then
            ' Retrieve Savings Value
            Decimal.TryParse(tBoxSavings.Text, dSaveAmount)

            If cBoxSavings.SelectedItem.ToString() = "Daily" Then
                dSaveAmountDaily = dSaveAmount
                dSaveAmountWeekly = dSaveAmountDaily * dSADToSAW
                dSaveAmountMonthly = dSaveAmountWeekly * dSAWToSAM
                dSaveAmountYearly = dSaveAmountMonthly * dSAMToSAY
            ElseIf cBoxSavings.SelectedItem.ToString() = "Weekly" Then
                dSaveAmountWeekly = dSaveAmount
                dSaveAmountDaily = dSaveAmountWeekly * dSAWToSAD

                dSaveAmountMonthly = dSaveAmountWeekly * dSAWToSAM
                dSaveAmountYearly = dSaveAmountMonthly * dSAMToSAY
            ElseIf cBoxSavings.SelectedItem.ToString() = "Monthly" Then
                dSaveAmountMonthly = dSaveAmount
                dSaveAmountWeekly = dSaveAmountMonthly * dSAMToSAW
                dSaveAmountDaily = dSaveAmountWeekly * dSAWToSAD

                dSaveAmountYearly = dSaveAmountMonthly * dSAMToSAY
            ElseIf cBoxSavings.SelectedItem.ToString() = "Yearly" Then
                dSaveAmountYearly = dSaveAmount
                dSaveAmountMonthly = dSaveAmountYearly * dSAYToSAM
                dSaveAmountWeekly = dSaveAmountMonthly * dSAMToSAW
                dSaveAmountDaily = dSaveAmountWeekly * dSAWToSAD
            End If

            ' Calculate Savings
            dSaveAmountTotalYearly = CDec(dIRTY) - dTotalYExp - dSaveAmountYearly
            dSaveAmountTotalMonthly = dSaveAmountTotalYearly / 12
            dSaveAmountTotalWeekly = dSaveAmountTotalMonthly / 4.34524
            dSaveAmountTotalDaily = dSaveAmountTotalWeekly / 7

            ' Display Savings
            tBoxBudgetReportSaveDaily.Text = dSaveAmountTotalDaily.ToString("C2")
            tBoxBudgetReportSaveWeekly.Text = dSaveAmountTotalWeekly.ToString("C2")
            tBoxBudgetReportSaveMonthly.Text = dSaveAmountTotalMonthly.ToString("C2")
            tBoxBudgetReportSaveYearly.Text = dSaveAmountTotalYearly.ToString("C2")
        End If
    End Sub

    Private Sub btnClearBudgetRep_Click(sender As Object, e As EventArgs) Handles btnClearBudgetRep.Click
        ' Clears Budget Report Values
        tBoxExp1.Clear()
        tBoxExp2.Clear()
        tBoxExp3.Clear()
        tBoxExp4.Clear()
        tBoxExp5.Clear()

        tBoxSavings.Clear()

        tBoxTotalAvgExpDaily.Clear()
        tBoxTotalAvgExpWeekly.Clear()
        tBoxTotalAvgExpMonthly.Clear()
        tBoxTotalAvgExpYearly.Clear()

        tBoxIncAfterTaxesAndExpensesDaily.Clear()
        tBoxIncAfterTaxesAndExpensesWeekly.Clear()
        tBoxIncAfterTaxesAndExpensesMonthly.Clear()
        tBoxIncAfterTaxesAndExpensesYearly.Clear()

        tBoxBudgetReportSaveDaily.Clear()
        tBoxBudgetReportSaveWeekly.Clear()
        tBoxBudgetReportSaveMonthly.Clear()
        tBoxBudgetReportSaveYearly.Clear()
    End Sub

    Private Sub rbtnSavingsYes_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnSavingsYes.CheckedChanged
        lblSavingsOften.Enabled = True
        lblSavingsFreq.Enabled = True
        tBoxSavings.Enabled = True
        cBoxSavings.Enabled = True
        tBoxSavings.Clear()

        lblIncAfterTaxesExpensesAndSavings.Enabled = True
        lblBudgetReportSaveDaily.Enabled = True
        lblBudgetReportSaveWeekly.Enabled = True
        lblBudgetReportSaveMonthly.Enabled = True
        lblBudgetReportSaveYearly.Enabled = True

        tBoxBudgetReportSaveDaily.Enabled = True
        tBoxBudgetReportSaveWeekly.Enabled = True
        tBoxBudgetReportSaveMonthly.Enabled = True
        tBoxBudgetReportSaveYearly.Enabled = True
    End Sub

    Private Sub rbtnSavingsNo_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnSavingsNo.CheckedChanged
        lblSavingsOften.Enabled = False
        lblSavingsFreq.Enabled = False
        tBoxSavings.Enabled = False
        cBoxSavings.Enabled = False
        tBoxSavings.Clear()

        lblIncAfterTaxesExpensesAndSavings.Enabled = False
        lblBudgetReportSaveDaily.Enabled = False
        lblBudgetReportSaveWeekly.Enabled = False
        lblBudgetReportSaveMonthly.Enabled = False
        lblBudgetReportSaveYearly.Enabled = False

        tBoxBudgetReportSaveDaily.Enabled = False
        tBoxBudgetReportSaveWeekly.Enabled = False
        tBoxBudgetReportSaveMonthly.Enabled = False
        tBoxBudgetReportSaveYearly.Enabled = False

        tBoxBudgetReportSaveDaily.Clear()
        tBoxBudgetReportSaveWeekly.Clear()
        tBoxBudgetReportSaveMonthly.Clear()
        tBoxBudgetReportSaveYearly.Clear()
    End Sub
End Class