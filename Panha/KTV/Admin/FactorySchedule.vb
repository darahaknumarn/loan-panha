Imports System.Collections.Generic

'Repayment schedule for factory-worker loans (loan unit "រោងចក្រ").
'
'Factory workers are paid on the 10th and the 25th, so every installment is due
'on one of those two paydays instead of a fixed number of days after the last
'one. Interest is a fixed half-month amount per installment; only the first
'installment is pro-rated for the days between disbursement and the first payday.
'
'This module is pure date and amount arithmetic so it can be unit tested
'(KTV.Tests links it as source). frmDisburshment supplies the holiday lookup,
'applies the riel rounding and writes the BK_LoanSchedule rows.
Module FactorySchedule

    'LU_Name of the BK_LoanUnit row (LU_ID = UnitCode) that selects this schedule.
    Public Const UnitName As String = "រោងចក្រ"
    Public Const UnitCode As Integer = 5

    'The factory paydays.
    Public Const PayDay1 As Integer = 10
    Public Const PayDay2 As Integer = 25

    'A first payday closer than this to the disbursement date is skipped.
    Public Const MinFirstGapDays As Integer = 5

    'Day basis used to pro-rate the first installment from the monthly rate.
    Public Const DaysPerMonth As Integer = 30

    Public Class Installment
        Public Property PayDay As Date      'the nominal 10th / 25th
        Public Property DueDate As Date     'PayDay moved off a weekend or holiday
        Public Property Principal As Double
        Public Property Interest As Double
        Public Property Balance As Double   'outstanding after this installment
    End Class

    'The first payday strictly after the given date.
    Public Function NextPayday(ByVal after As Date) As Date
        Dim d As Date = after.Date
        If d.Day < PayDay1 Then Return New Date(d.Year, d.Month, PayDay1)
        If d.Day < PayDay2 Then Return New Date(d.Year, d.Month, PayDay2)
        Dim n As Date = New Date(d.Year, d.Month, 1).AddMonths(1)
        Return New Date(n.Year, n.Month, PayDay1)
    End Function

    'The payday of the first installment: the next payday after disbursement,
    'unless it comes too soon, in which case the one after it.
    Public Function FirstPayday(ByVal disburseDate As Date) As Date
        Dim p As Date = NextPayday(disburseDate)
        If (p - disburseDate.Date).Days < MinFirstGapDays Then
            p = NextPayday(p)
        End If
        Return p
    End Function

    Public Function IsWorkingDay(ByVal d As Date, ByVal isHoliday As Func(Of Date, Boolean)) As Boolean
        If d.DayOfWeek = DayOfWeek.Saturday OrElse d.DayOfWeek = DayOfWeek.Sunday Then Return False
        If isHoliday IsNot Nothing AndAlso isHoliday(d.Date) Then Return False
        Return True
    End Function

    'Moves a payday that falls on a non-working day. A single closed day is
    'collected the working day before (the worker is paid before the break);
    'a run of two or more closed days (a weekend, a long holiday) is collected
    'the first working day after it.
    Public Function AdjustForNonWorkingDays(ByVal payDay As Date, ByVal isHoliday As Func(Of Date, Boolean)) As Date
        Dim d As Date = payDay.Date
        If IsWorkingDay(d, isHoliday) Then Return d
        Dim first As Date = d
        While Not IsWorkingDay(first.AddDays(-1), isHoliday)
            first = first.AddDays(-1)
        End While
        Dim last As Date = d
        While Not IsWorkingDay(last.AddDays(1), isHoliday)
            last = last.AddDays(1)
        End While
        If (last - first).Days = 0 Then
            Return first.AddDays(-1)
        End If
        Return last.AddDays(1)
    End Function

    'Interest of the first installment: the monthly rate spread over 30 days,
    'charged on the disbursed amount for the days up to the first payday.
    Public Function FirstInterest(ByVal amount As Double, ByVal monthlyRatePct As Double,
                                  ByVal disburseDate As Date, ByVal firstPayday As Date) As Double
        Dim days As Integer = (firstPayday.Date - disburseDate.Date).Days
        Return amount * (monthlyRatePct * 0.01) / DaysPerMonth * days
    End Function

    'Interest of every later installment: half the monthly rate on the basis
    '(disbursed amount for a flat loan, outstanding balance for a declining one).
    'The real gap between paydays, 15 or 16 days, does not change it.
    Public Function PeriodInterest(ByVal basis As Double, ByVal monthlyRatePct As Double) As Double
        Return basis * (monthlyRatePct * 0.01) / 2
    End Function

    'Builds the whole schedule. Amounts are unrounded (the _Org values); the
    'caller rounds riel amounts the same way as the other loan units.
    Public Function BuildSchedule(ByVal disburseDate As Date, ByVal amount As Double,
                                  ByVal monthlyRatePct As Double, ByVal term As Integer,
                                  ByVal declining As Boolean,
                                  ByVal isHoliday As Func(Of Date, Boolean)) As List(Of Installment)
        If term <= 0 Then Throw New ArgumentException("Term must be at least one installment.")
        If amount <= 0 Then Throw New ArgumentException("Disbursed amount must be positive.")

        Dim rows As New List(Of Installment)
        Dim principal As Double = Math.Round(amount / term, 2)
        Dim balance As Double = amount
        Dim payDay As Date = FirstPayday(disburseDate)

        For j As Integer = 0 To term - 1
            Dim row As New Installment
            If j > 0 Then payDay = NextPayday(payDay)
            row.PayDay = payDay
            row.DueDate = AdjustForNonWorkingDays(payDay, isHoliday)

            If j = 0 Then
                row.Interest = FirstInterest(amount, monthlyRatePct, disburseDate, payDay)
            ElseIf declining Then
                row.Interest = PeriodInterest(balance, monthlyRatePct)
            Else
                row.Interest = PeriodInterest(amount, monthlyRatePct)
            End If

            If j = term - 1 Then
                row.Principal = balance          'last installment clears the rest
                balance = 0
            Else
                row.Principal = principal
                balance = balance - principal
            End If
            row.Balance = balance
            rows.Add(row)
        Next
        Return rows
    End Function

End Module
