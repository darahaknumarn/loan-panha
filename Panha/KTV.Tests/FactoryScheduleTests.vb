Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting

'Unit tests for the factory-payday schedule. No database is touched here.
<TestClass()>
Public Class FactoryScheduleTests

    Private Shared ReadOnly NoHolidays As Func(Of Date, Boolean) = Function(d) False

    Private Shared Function Holidays(ByVal ParamArray days() As Date) As Func(Of Date, Boolean)
        Dim set1 As New HashSet(Of Date)(days)
        Return Function(d) set1.Contains(d.Date)
    End Function

    ' ---------- paydays ----------

    <TestMethod()>
    Public Sub NextPayday_before_the_10th_is_the_10th()
        Assert.AreEqual(#11/10/2025#, FactorySchedule.NextPayday(#11/3/2025#))
    End Sub

    <TestMethod()>
    Public Sub NextPayday_on_the_10th_is_the_25th()
        Assert.AreEqual(#11/25/2025#, FactorySchedule.NextPayday(#11/10/2025#))
    End Sub

    <TestMethod()>
    Public Sub NextPayday_on_or_after_the_25th_rolls_to_next_month()
        Assert.AreEqual(#12/10/2025#, FactorySchedule.NextPayday(#11/25/2025#))
        Assert.AreEqual(#1/10/2026#, FactorySchedule.NextPayday(#12/31/2025#))
    End Sub

    <TestMethod()>
    Public Sub FirstPayday_keeps_a_gap_of_five_days()
        Assert.AreEqual(#11/10/2025#, FactorySchedule.FirstPayday(#11/5/2025#))
    End Sub

    <TestMethod()>
    Public Sub FirstPayday_skips_a_payday_closer_than_five_days()
        Assert.AreEqual(#11/25/2025#, FactorySchedule.FirstPayday(#11/6/2025#))
        Assert.AreEqual(#11/25/2025#, FactorySchedule.FirstPayday(#11/10/2025#))
    End Sub

    ' ---------- weekend / holiday shift ----------

    <TestMethod()>
    Public Sub Working_day_is_not_moved()
        Assert.AreEqual(#11/10/2025#, FactorySchedule.AdjustForNonWorkingDays(#11/10/2025#, NoHolidays)) 'Monday
    End Sub

    <TestMethod()>
    Public Sub Single_holiday_is_collected_the_day_before()
        Dim wed As Date = #11/12/2025#
        Assert.AreEqual(#11/11/2025#, FactorySchedule.AdjustForNonWorkingDays(wed, Holidays(wed)))
    End Sub

    <TestMethod()>
    Public Sub Weekend_is_collected_the_monday_after()
        Assert.AreEqual(#1/12/2026#, FactorySchedule.AdjustForNonWorkingDays(#1/10/2026#, NoHolidays)) 'Saturday
        Assert.AreEqual(#1/26/2026#, FactorySchedule.AdjustForNonWorkingDays(#1/25/2026#, NoHolidays)) 'Sunday
    End Sub

    <TestMethod()>
    Public Sub Holiday_joined_to_a_weekend_is_collected_after_the_whole_break()
        Dim fri As Date = #4/10/2026#
        Assert.AreEqual(#4/13/2026#, FactorySchedule.AdjustForNonWorkingDays(fri, Holidays(fri)))
    End Sub

    <TestMethod()>
    Public Sub Holiday_on_a_monday_after_a_weekend_is_collected_tuesday()
        Dim mon As Date = #8/10/2026#
        Assert.AreEqual(#8/11/2026#, FactorySchedule.AdjustForNonWorkingDays(mon, Holidays(mon)))
    End Sub

    <TestMethod()>
    Public Sub Two_consecutive_holidays_are_collected_after()
        Dim tue As Date = #11/25/2025#
        Assert.AreEqual(#11/27/2025#, FactorySchedule.AdjustForNonWorkingDays(tue, Holidays(tue, tue.AddDays(1))))
    End Sub

    ' ---------- interest ----------

    <TestMethod()>
    Public Sub First_interest_is_prorated_by_days_on_a_30_day_month()
        Dim i As Double = FactorySchedule.FirstInterest(1000000, 2, #11/3/2025#, #11/10/2025#)
        Assert.AreEqual(4666.67, Math.Round(i, 2))
    End Sub

    <TestMethod()>
    Public Sub Period_interest_is_half_the_monthly_rate()
        Assert.AreEqual(10000.0, FactorySchedule.PeriodInterest(1000000, 2))
    End Sub

    ' ---------- whole schedule: the customer's worked example ----------

    <TestMethod()>
    Public Sub Flat_schedule_matches_the_worked_example()
        Dim rows = FactorySchedule.BuildSchedule(#11/3/2025#, 1000000, 2, 6, False, NoHolidays)

        Assert.AreEqual(6, rows.Count)
        Dim expectedPaydays() As Date = {#11/10/2025#, #11/25/2025#, #12/10/2025#, #12/25/2025#, #1/10/2026#, #1/25/2026#}
        For k As Integer = 0 To 5
            Assert.AreEqual(expectedPaydays(k), rows(k).PayDay, "payday " & k)
        Next

        Assert.AreEqual(4666.67, Math.Round(rows(0).Interest, 2))
        For k As Integer = 1 To 5
            Assert.AreEqual(10000.0, rows(k).Interest, "interest " & k)
        Next

        For k As Integer = 0 To 4
            Assert.AreEqual(166666.67, rows(k).Principal, "principal " & k)
        Next
        Assert.AreEqual(166666.65, Math.Round(rows(5).Principal, 2), "last principal takes the remainder")
        Assert.AreEqual(0.0, rows(5).Balance, 0.000001)
    End Sub

    <TestMethod()>
    Public Sub Due_dates_move_off_weekends_but_interest_does_not_change()
        Dim rows = FactorySchedule.BuildSchedule(#11/3/2025#, 1000000, 2, 6, False, NoHolidays)
        Assert.AreEqual(#1/12/2026#, rows(4).DueDate)  '10 Jan 2026 is a Saturday
        Assert.AreEqual(#1/26/2026#, rows(5).DueDate)  '25 Jan 2026 is a Sunday
        Assert.AreEqual(10000.0, rows(4).Interest)
        Assert.AreEqual(10000.0, rows(5).Interest)
    End Sub

    <TestMethod()>
    Public Sub Declining_interest_uses_the_previous_outstanding_balance()
        Dim rows = FactorySchedule.BuildSchedule(#11/3/2025#, 1000000, 2, 4, True, NoHolidays)
        Assert.AreEqual(4666.67, Math.Round(rows(0).Interest, 2))
        Assert.AreEqual(7500.0, rows(1).Interest)   '750,000 * 1%
        Assert.AreEqual(5000.0, rows(2).Interest)   '500,000 * 1%
        Assert.AreEqual(2500.0, rows(3).Interest)   '250,000 * 1%
    End Sub

    <TestMethod()>
    Public Sub Last_installment_clears_any_rounding_remainder()
        Dim rows = FactorySchedule.BuildSchedule(#11/3/2025#, 1000, 2, 3, False, NoHolidays)
        Assert.AreEqual(333.33, rows(0).Principal)
        Assert.AreEqual(333.33, rows(1).Principal)
        Assert.AreEqual(333.34, Math.Round(rows(2).Principal, 2))
        Assert.AreEqual(0.0, rows(2).Balance, 0.000001)
    End Sub

    <TestMethod()>
    Public Sub Schedule_rejects_a_zero_term()
        Assert.ThrowsException(Of ArgumentException)(
            Sub() FactorySchedule.BuildSchedule(#11/3/2025#, 1000, 2, 0, False, NoHolidays))
    End Sub

End Class
