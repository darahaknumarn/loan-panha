


CREATE PROCEDURE [dbo].[sp_rptEndBalSumByEndDay] (
@StartDate Varchar(12),
@EndDate Varchar(12),
@BrID Varchar(12))
AS
BEGIN
SET NOCOUNT ON;
Declare @SDate as DateTime;
Declare @EDate as DateTime;
Declare @KHR as Nvarchar(100);
Declare @USD as Nvarchar(100);
Set @EDate = @EndDate;
Set @SDate = @StartDate;
Set @KHR = N'រៀល';
Set @USD = N'ដុល្លារ';

Declare @EKHR as numeric (18,2)=0;
Declare @EUSD as numeric(18,2)=0;

IF @BrID = 'All' set @BrID ='%';
----Loan Repay
Select a.LD_ID,a.LR_Date,
       c.CU_ID,
       a.LR_BrID
        ,case when c.CU_ID=2 then sum(a.LR_Amount) else 0 end USD
       ,case when c.CU_ID=1 then sum(a.LR_Amount) else 0 end KHR
       ,case when c.CU_ID=2 then sum(isnull(a.LR_Charge,0)) else 0 end Charge_USD
       ,case when c.CU_ID=1 then sum(isnull(a.LR_Charge,0)) else 0 end Charge_KHR
Into #LD_Repay
From dbo.BK_LoanRepay a Inner Join dbo.BK_LoanSchedule b On a.LD_ID = b.LD_ID and a.SH_Date = b.SH_Date and a.LR_BrID = b.SH_BrId
     Inner Join dbo.BK_Loan c On c.LD_ID = a.LD_ID and c.LD_BrId = a.LR_BrID
Where a.LR_Date between @SDate and @EDate
      and a.LR_BrID = @BrID
      group by a.LD_ID,c.CU_ID,a.LR_BrID,a.LR_Date
      --and c.CU_ID = @CUID;

Select a.LR_BrID,a.LR_Date,
SUM( a.KHR) as KHR,
 sum(a.USD) as USD
 , sum(a.Charge_KHR) Charge_KHR,
 sum(a.Charge_USD) Charge_USD
   Into #TotalRepay
From #LD_Repay a
Group By a.LR_BrID,a.LR_Date;
--select * from #TotalRepay
--return
---------------------------------------------- Admin fee
Select a.LD_Dis_Date,
       a.LD_ID, 
	   case when a.CU_ID=1 then sum(isnuLL(a.LD_ChargeAmt,0)) else 0 end KHR,
	   case when a.CU_ID=2 then sum(isnuLL(a.LD_ChargeAmt,0)) else 0 end USD,
	     case when a.CU_ID=1 then sum(isnuLL(a.Ref,0)) else 0 end RefKHR,
	   case when a.CU_ID=2 then sum(isnuLL(a.Ref,0)) else 0 end RefUSD,
	           a.LD_BrId,       a.CU_ID
Into #LD_Fee
From BK_Loan a
Where a.LD_BrId = @BrID
      and a.LD_Dis_Date between @SDate and @EDate
  group by a.LD_Dis_Date, a.LD_ID,a.LD_BrId,a.CU_ID
  
Select a.LD_BrId,a.LD_Dis_Date,
    sum(a.KHR) KHR,sum(a.USD)USD,
	sum(a.RefKHR) RefKHR,sum(a.RefUSD)RefUSD
Into #Total_Fee
From #LD_Fee a 
Group By a.LD_BrId,a.LD_Dis_Date
--select * from #Total_Fee return
----------------------------------------------- Insurance
Select a.LD_Dis_Date,
       a.LD_ID, case when a.CU_ID=1 then sum(isnuLL(a.LD_InAmt,0)) else 0 end KHR,case when a.CU_ID=2 then sum(isnuLL(a.LD_InAmt,0)) else 0 end USD
       ,        a.LD_BrId,       a.CU_ID
Into #LD_Insurance
From BK_Loan a
Where a.LD_BrId = @BrID
      and a.LD_Dis_Date between @SDate and @EDate
  group by a.LD_ID,a.LD_BrId,a.CU_ID,a.LD_Dis_Date
  
Select a.LD_BrId,a.LD_Dis_Date,
    sum(a.KHR) KHR,sum(a.USD)USD
Into #Total_Insurance
From #LD_Insurance a 
Group By a.LD_BrId,a.LD_Dis_Date
--select * from #Total_Insurance return
--select * from #LD_Fee
--return
----Loan Disbursment
Select a.LD_Dis_Date,
       a.LD_ID, case when a.CU_ID=1 then sum(a.LD_Dis_Amt) else 0 end KHR,case when a.CU_ID=2 then sum(a.LD_Dis_Amt) else 0 end USD
       ,        a.LD_BrId,       a.CU_ID
Into #LD_Dis
From BK_Loan a
Where a.LD_BrId = @BrID
      and a.LD_Dis_Date between @SDate and @EDate
  group by a.LD_ID,a.LD_BrId,a.CU_ID,a.LD_Dis_Date
  
Select a.LD_BrId,a.LD_Dis_Date,
    sum(a.KHR) KHR,sum(a.USD)USD
Into #Total_Dis
From #LD_Dis a 
Group By a.LD_BrId,a.LD_Dis_Date

--Total Expense
Select a.OPDate,
       a.BrID,
     case when OPCurrency=N'រៀល' then sum(OPCost) else 0 end KHR,
        case when OPCurrency=N'ដុល្លារ' then sum(OPCost) else 0 end USD,
       a.OPCurrency
Into #EX
From ExpenseOperation a 
Where a.BrID = @BrID 
and a.OPDate between @SDate and @EDate 
group by a.BrID,OPCurrency,a.OPDate


Select a.BrID,a.OPDate,
   SUM( a.KHR) KHR,sum(a.USD) USD
Into #Total_Ex
From #EX a
Group by a.BrID,a.OPDate

--select *from #Total_Ex return
--Owner Withdrawal and Deposit
Select a.BrID,a.OPDate,
       sum(a.USD) USD,
       sum(a.KHR) KHR
Into #Total_Deposit
From OwnerTransaction a 
where a.BrID = @BrID
     and a.OPDate between @SDate and @EDate 
	 and a.OPType='Deposit' 
	 group by a.BrID,a.OPDate;

Select a.BrID,a.OPDate,
       sum(a.USD) USD,
       sum(a.KHR) KHR
Into #Total_Withdrawal
From OwnerTransaction a 
where a.BrID = @BrID
     and a.OPDate between @SDate and @EDate 
	 and a.OPType='Withdrawal' group by a.BrID,a.OPDate;

select BrId,Date_Operation,SUM( USD)USD,SUM( KHR)KHR 
into #Total_USDTOKHR
from BK_Exchange 
where Date_Operation between @SDate and @EDate and Type='USDTOKHR'
group by BrId,Date_Operation

select BrId,Date_Operation,SUM( USD)USD,SUM( KHR)KHR 
into #Total_KHRTOUSD
from BK_Exchange where Date_Operation between @SDate and @EDate and Type='KHRTOUSD'
group by BrId,Date_Operation
------------------------
select BrId,Date_Operation
,case when CU_ID=1 then sum(isnull(Amount,0)) else 0 end KHR 
,case when CU_ID=2 then sum(isnull(Amount,0)) else 0 end USD
into #OtherTem
from BK_OtherIncome 
where BrId like @BrID 
and Date_Operation between @SDate and @EndDate
group by BrId,CU_ID,Date_Operation
--
select BrId,Date_Operation,sum(isnull(KHR,0))KHR,sum(isnull(USD,0))USD 
into #OtherTotal from #OtherTem Group by BrId,Date_Operation

------------------------------------------First Ending Balance

Select a.LD_ID,
       c.CU_ID,
       a.LR_BrID
        ,case when c.CU_ID=2 then sum(a.LR_Amount) else 0 end USD
       ,case when c.CU_ID=1 then sum(a.LR_Amount) else 0 end KHR
       ,case when c.CU_ID=2 then sum(isnull(a.LR_Charge,0)) else 0 end Charge_USD
       ,case when c.CU_ID=1 then sum(isnull(a.LR_Charge,0)) else 0 end Charge_KHR
Into #LD_Repay1
From dbo.BK_LoanRepay a 
	 Inner Join dbo.BK_LoanSchedule b On a.LD_ID = b.LD_ID and a.SH_Date = b.SH_Date and a.LR_BrID = b.SH_BrId
     Inner Join dbo.BK_Loan c On c.LD_ID = a.LD_ID and c.LD_BrId = a.LR_BrID
Where  a.LR_Date <= @SDate-1
      and a.LR_BrID = @BrID
      group by a.LD_ID,c.CU_ID,a.LR_BrID
      --and c.CU_ID = @CUID;

Select a.LR_BrID,
SUM( a.KHR) as KHR,
 sum(a.USD) as USD
 , sum(a.Charge_KHR) Charge_KHR,
 sum(a.Charge_USD) Charge_USD
Into #TotalRepay1
From #LD_Repay1 a
Group By a.LR_BrID;
--select * from #TotalRepay
--return
---------------------------------------------- Admin fee
Select 
       a.LD_ID, 
	   case when a.CU_ID=1 then sum(isnuLL(a.LD_ChargeAmt,0)) else 0 end KHR,
	   case when a.CU_ID=2 then sum(isnuLL(a.LD_ChargeAmt,0)) else 0 end USD,
	     case when a.CU_ID=1 then sum(isnuLL(a.Ref,0)) else 0 end RefKHR,
	   case when a.CU_ID=2 then sum(isnuLL(a.Ref,0)) else 0 end RefUSD,
	           a.LD_BrId,       a.CU_ID
Into #LD_Fee1
From BK_Loan a
Where a.LD_BrId = @BrID
      and a.LD_Dis_Date <= @SDate-1
  group by a.LD_ID,a.LD_BrId,a.CU_ID
  
Select a.LD_BrId,
    sum(a.KHR) KHR,sum(a.USD)USD,
	sum(a.RefKHR) RefKHR,sum(a.RefUSD)RefUSD
Into #Total_Fee1
From #LD_Fee1 a 
Group By a.LD_BrId

----------------------------------------------- Insurance
Select 
       a.LD_ID, case when a.CU_ID=1 then sum(isnuLL(a.LD_InAmt,0)) else 0 end KHR,case when a.CU_ID=2 then sum(isnuLL(a.LD_InAmt,0)) else 0 end USD
       ,        a.LD_BrId,       a.CU_ID
Into #LD_Insurance1
From BK_Loan a
Where a.LD_BrId = @BrID
      and a.LD_Dis_Date <= @SDate-1
  group by a.LD_ID,a.LD_BrId,a.CU_ID
  
Select a.LD_BrId,
    sum(a.KHR) KHR,sum(a.USD)USD
Into #Total_Insurance1
From #LD_Insurance1 a 
Group By a.LD_BrId
--select * from #Total_Insurance return
--select * from #LD_Fee
--return
----Loan Disbursment
Select 
       a.LD_ID, case when a.CU_ID=1 then sum(a.LD_Dis_Amt) else 0 end KHR,case when a.CU_ID=2 then sum(a.LD_Dis_Amt) else 0 end USD
       ,        a.LD_BrId,       a.CU_ID
Into #LD_Dis1
From BK_Loan a
Where a.LD_BrId = @BrID
      and a.LD_Dis_Date <= @SDate-1
  group by a.LD_ID,a.LD_BrId,a.CU_ID
  
Select a.LD_BrId,
    sum(a.KHR) KHR,sum(a.USD)USD
Into #Total_Dis1
From #LD_Dis1 a 
Group By a.LD_BrId

--Total Expense
Select 
       a.BrID,
     case when OPCurrency=N'រៀល' then sum(OPCost) else 0 end KHR,
        case when OPCurrency=N'ដុល្លារ' then sum(OPCost) else 0 end USD,
       a.OPCurrency
Into #EX1
From ExpenseOperation a 
Where a.BrID = @BrID and a.OPDate <= @SDate-1 group by a.BrID,OPCurrency
Select a.BrID,
   SUM( a.KHR) KHR,sum(a.USD) USD
Into #Total_Ex1
From #EX1 a
Group by a.BrID
--Owner Withdrawal and Deposit
Select a.BrID,
       sum(a.USD) USD,
       sum(a.KHR) KHR
Into #Total_Deposit1
From OwnerTransaction a 
where a.BrID = @BrID
     and a.OPDate <= @SDate-1 and a.OPType='Deposit'
	  group by a.BrID;

Select a.BrID,
       sum(a.USD) USD,
       sum(a.KHR) KHR
Into #Total_Withdrawal1
From OwnerTransaction a 
where a.BrID = @BrID
     and a.OPDate <= @SDate-1 and a.OPType='Withdrawal' 
	 group by a.BrID;

select BrId,SUM( USD)USD,SUM( KHR)KHR 
into #Total_USDTOKHR1
from BK_Exchange 
where Date_Operation<=@SDate-1 and Type='USDTOKHR'
group by BrId

select BrId,SUM( USD)USD,SUM( KHR)KHR 
into #Total_KHRTOUSD1
from BK_Exchange 
where Date_Operation<=@SDate-1 and Type='KHRTOUSD'
group by BrId
------------------------
select BrId
,case when CU_ID=1 then sum(isnull(Amount,0)) else 0 end KHR 
,case when CU_ID=2 then sum(isnull(Amount,0)) else 0 end USD
into #OtherTem1
from BK_OtherIncome 
where BrId like @BrID and Date_Operation <=@SDate-1 group by BrId,CU_ID
--
select BrId,sum(isnull(KHR,0))KHR,sum(isnull(USD,0))USD 
into #OtherTotal1 from #OtherTem1 Group by BrId
-----
select a.CompanyID,a.CompanyKhmerName,@SDate-1 dates,
 (isnull(b.KHR,0)+isnull(c.KHR,0)+isnull(i.KHR,0)+isnull(j.KHR,0)+isnull(j.RefKHR,0)+isnull(c.Charge_KHR,0)+isnull(l.KHR,0)+isnull(s.KHR,0))
 - (isnull(d.KHR,0)+isnull(e.KHR,0)+isnull(f.KHR,0)+isnull(h.KHR,0)) KHR 
,(isnull(b.USD,0)+isnull(c.USD,0)+isnull(h.USD,0)+isnull(j.USD,0)+isnull(j.RefUSD,0)+isnull(c.Charge_USD,0)+isnull(l.USD,0)+isnull(s.USD,0))
- (isnull(d.USD,0)+isnull(e.USD,0)+isnull(f.USD,0)+isnull(i.USD,0))  USD 
into #firstBalance
from BK_Company a
------------------------ Income
left join #Total_Deposit1 b on a.CompanyID=b.BrID
left join #Total_Fee1 j on a.CompanyID=j.LD_BrId
left join #TotalRepay1 c on a.CompanyID=c.LR_BrID
left join #OtherTotal1 s on a.CompanyID=s.BrId
------------------------ Expense
left join #Total_Withdrawal1 d on a.CompanyID=d.BrID
left join #Total_Dis1 e on a.CompanyID=e.LD_BrId
left join #Total_Ex1 f on a.CompanyID=f.BrID
left join #Total_KHRTOUSD1 h on a.CompanyID=h.BrId
left join #Total_USDTOKHR1 i on a.CompanyID=i.BrId
left join #Total_Insurance1 l on a.CompanyID=j.LD_BrId

where a.CompanyID=@BrID

--select * from #firstBalance return
-----
--select a.CompanyID,a.CompanyKhmerName,
-- (isnull(b.KHR,0)+isnull(c.KHR,0)+isnull(i.KHR,0)+isnull(j.KHR,0)+isnull(j.RefKHR,0)+c.Charge_KHR+l.KHR+isnull(s.KHR,0))
-- - (isnull(d.KHR,0)+isnull(e.KHR,0)+isnull(f.KHR,0)+isnull(h.KHR,0)) KHR 
--,(isnull(b.USD,0)+isnull(c.USD,0)+isnull(h.USD,0)+isnull(j.USD,0)+isnull(j.RefUSD,0)+c.Charge_USD+l.USD+isnull(s.USD,0))
--- (isnull(d.USD,0)+isnull(e.USD,0)+isnull(f.USD,0)+isnull(i.USD,0))  USD 

--from BK_Company a
-------------------------- Income
--left join #Total_Deposit b on a.CompanyID=b.BrID
--left join #Total_Fee j on a.CompanyID=j.LD_BrId
--left join #TotalRepay c on a.CompanyID=c.LR_BrID
--left join #OtherTotal s on a.CompanyID=s.BrId
-------------------------- Expense
--left join #Total_Withdrawal d on a.CompanyID=d.BrID
--left join #Total_Dis e on a.CompanyID=e.LD_BrId
--left join #Total_Ex f on a.CompanyID=f.BrID
--left join #Total_KHRTOUSD h on a.CompanyID=h.BrId
--left join #Total_USDTOKHR i on a.CompanyID=i.BrId
--left join #Total_Insurance l on a.CompanyID=j.LD_BrId

--where a.CompanyID=@BrID


--print  return
--------------------------------------------------------- Create table
Create Table #LD(
TxnOrder Int,
TxnDate Date,
BrID Varchar(12),
BrName NVarchar(50),
Total_Deposit_KH Numeric(18,2),
Total_Deposit_USD Numeric(18,2),
Total_AdminFee_KHR Numeric(18,2),
Total_AdminFee_USD Numeric(18,2),
Total_RefFee_KHR Numeric(18,2),
Total_RefFee_USD Numeric(18,2),
TotalRepay_KHR Numeric(18,2),
TotalRepay_USD Numeric(18,2),
TotalCharge_KHR Numeric(18,2),
TotalCharge_USD Numeric(18,2),
OtherTotal_KHR Numeric(18,2),
OtherTotal_USD Numeric(18,2),
Total_Insurance_KHR Numeric(18,2),
Total_Insurance_USD Numeric(18,2),


Total_Withdrawal_KH Numeric(18,2),
Total_Withdrawal_USD Numeric(18,2),
Total_Dis_KHR Numeric(18,2),
Total_Dis_USD Numeric(18,2),
Total_Ex_KHR Numeric(18,2),
Total_Ex_USD Numeric(18,2),
Total_KHRTOUSD_KHR Numeric(18,2),
Total_KHRTOUSD_USD Numeric(18,2),
Total_USDTOKHR_KHR Numeric(18,2),
Total_USDTOKHR_USD Numeric(18,2),

EndBalKHR Numeric(18,2),
EndBalUSD Numeric(18,2)
);
--Insert Into #LD(TxnOrder,EndBalKHR,EndBalUSD) Values(1,@BBalKHR,@BBalUSD);
--start balance
Insert Into #LD(TxnOrder,BrID,EndBalKHR,EndBalUSD)
Select 1,a.CompanyID,a.KHR,a.USD From #firstBalance a;
-- Deposit
Insert Into #LD(TxnOrder,TxnDate,BrID,Total_Deposit_KH,Total_Deposit_USD)
Select 2,a.OPDate,a.BrID,a.KHR,a.USD From #Total_Deposit a;
--AdminFee
Insert Into #LD(TxnOrder,TxnDate,BrID,Total_AdminFee_KHR,Total_AdminFee_USD)
Select 2,a.LD_Dis_Date,a.LD_BrId,a.KHR,a.USD From #Total_Fee a;
------ RefFee
Insert Into #LD(TxnOrder,TxnDate,BrID,Total_RefFee_KHR,Total_RefFee_USD)
Select 2,a.LD_Dis_Date,a.LD_BrId,a.RefKHR,a.RefUSD From #Total_Fee a;
--Repay
Insert Into #LD(TxnOrder,TxnDate,BrID,TotalRepay_KHR,TotalRepay_USD)
Select 2,a.LR_Date,a.LR_BrID,a.KHR,a.USD From #TotalRepay a;
-- Charge
Insert Into #LD(TxnOrder,TxnDate,BrID,TotalCharge_KHR,TotalCharge_USD)
Select 2,a.LR_Date,a.LR_BrID,a.Charge_KHR,a.Charge_USD From #TotalRepay a;
--Other Income
Insert Into #LD(TxnOrder,TxnDate,BrID,OtherTotal_KHR,OtherTotal_USD)
Select 2,a.Date_Operation,a.BrId,a.KHR,a.USD From #OtherTotal a;
--Insurense
Insert Into #LD(TxnOrder,TxnDate,BrID,Total_Insurance_KHR,Total_Insurance_USD)
Select 2,a.LD_Dis_Date,a.LD_BrId,a.KHR,a.USD From #Total_Insurance a;


------------------------------Expense
--Withdrawal
Insert Into #LD(TxnOrder,TxnDate,BrID,Total_Withdrawal_KH,Total_Withdrawal_USD)
Select 2,a.OPDate,a.BrID,a.KHR,a.USD From #Total_Withdrawal a;

--Dis
Insert Into #LD(TxnOrder,TxnDate,BrID,Total_Dis_KHR,Total_Dis_USD)
Select 2,a.LD_Dis_Date,a.LD_BrId,a.KHR,a.USD From #Total_Dis a;

--Ex
Insert Into #LD(TxnOrder,TxnDate,BrID,Total_Ex_KHR,Total_Ex_USD)
Select 2,a.OPDate,a.BrID,a.KHR,a.USD From #Total_Ex a;

--KHRTOUSD
Insert Into #LD(TxnOrder,TxnDate,BrID,Total_KHRTOUSD_KHR,Total_KHRTOUSD_USD)
Select 2,a.Date_Operation,a.BrId,a.KHR,a.USD From #Total_KHRTOUSD a;

---USDTOKHR
Insert Into #LD(TxnOrder,TxnDate,BrID,Total_USDTOKHR_KHR,Total_USDTOKHR_USD)
Select 2,a.Date_Operation,a.BrId,a.KHR,a.USD From #Total_USDTOKHR a;
--select * from #Total_Deposit
--select * from #Total_Fee

Select a.TxnOrder,
       a.TxnDate,
       a.BrID,
       b.CompanyKhmerName as 'BrName',
       Sum(Total_Deposit_KH) as Total_Deposit_KH ,
       Sum(Total_Deposit_USD) as Total_Deposit_USD ,
       Sum(Total_AdminFee_KHR) as Total_AdminFee_KHR ,
       Sum(Total_AdminFee_USD) as Total_AdminFee_USD ,
	    Sum(Total_RefFee_KHR) as Total_RefFee_KHR ,
       Sum(Total_RefFee_USD) as Total_RefFee_USD ,
       Sum(TotalRepay_KHR) as TotalRepay_KHR,
       Sum(TotalRepay_USD) as TotalRepay_USD ,
	      Sum(TotalCharge_KHR) as TotalCharge_KHR,
       Sum(TotalCharge_KHR) as TotalCharge_USD ,
       Sum(OtherTotal_KHR) as OtherTotal_KHR ,
       Sum(OtherTotal_USD) as OtherTotal_USD ,
       Sum(Total_Insurance_KHR) as Total_Insurance_KHR ,
       Sum(Total_Insurance_USD) as Total_Insurance_USD ,

	   Sum(Total_Withdrawal_KH) as Total_Withdrawal_KH ,
       Sum(Total_Withdrawal_USD) as Total_Withdrawal_USD ,
       Sum(Total_Dis_KHR) as Total_Dis_KHR ,
       Sum(Total_Dis_USD) as Total_Dis_USD ,
       Sum(Total_Ex_KHR) as Total_Ex_KHR,
       Sum(Total_Ex_USD) as Total_Ex_USD ,
       Sum(Total_KHRTOUSD_KHR) as Total_KHRTOUSD_KHR ,
       Sum(Total_KHRTOUSD_USD) as Total_KHRTOUSD_USD ,
       Sum(Total_USDTOKHR_KHR) as Total_USDTOKHR_KHR ,
       Sum(Total_USDTOKHR_USD) as Total_USDTOKHR_USD ,
       Sum(EndBalKHR) as EndBalKHR ,
       Sum(EndBalUSD) as EndBalUSD
Into #LDLD1
From #LD a Left join BK_Company b on a.BrID COLLATE DATABASE_DEFAULT = b.CompanyID COLLATE DATABASE_DEFAULT
Group By a.TxnOrder,a.TxnDate,a.BrID,b.CompanyKhmerName;

--select* from #LDLD1 return
select 
@EKHR= a.EndBalKHR,@EUSD=a.EndBalUSD
from #LDLD1 a
Select 
--a.TxnOrder,
       Convert(Varchar(12),a.TxnDate,101) as 'TxnDate',
       a.BrID,
       a.BrName,
       (ISNULL(a.Total_Deposit_KH,0) + ISNULL(a.Total_AdminFee_KHR,0)+ ISNULL(a.Total_RefFee_KHR,0)+ ISNULL(a.TotalCharge_KHR,0) + ISNULL(a.TotalRepay_KHR,0) + ISNULL(a.OtherTotal_KHR,0) + ISNULL(a.Total_Insurance_KHR,0)
	   + ISNULL(a.Total_USDTOKHR_KHR,0))
	   -  (ISNULL(a.Total_Withdrawal_KH,0) + ISNULL(a.Total_Dis_KHR,0) + ISNULL(a.Total_Ex_KHR,0) + ISNULL(a.Total_KHRTOUSD_KHR,0) ) as 'TotalKHR',
	     (ISNULL(a.Total_Deposit_USD,0) + ISNULL(a.Total_AdminFee_USD,0) + ISNULL(a.TotalRepay_USD,0) + ISNULL(a.OtherTotal_USD,0) + ISNULL(a.Total_Insurance_USD,0)
	   + ISNULL(a.Total_KHRTOUSD_USD,0))
	   -  (ISNULL(a.Total_Withdrawal_USD,0) + ISNULL(a.Total_Dis_USD,0) + ISNULL(a.Total_Ex_USD,0) + ISNULL(a.Total_KHRTOUSD_USD,0) ) as 'TotalUSD',
       --ISNULL(a.DepositUSD,0) - ISNULL(a.WithdrawalUSD,0) + ISNULL(a.LRAmtUSD,0) - ISNULL(a.LDAmtUSD,0) - ISNULL(a.TotalExpUSD,0) as 'TotalUSD',
       a.EndBalKHR,
       a.EndBalUSD
	   --, (ISNULL(a.Total_Deposit_KH,0) + ISNULL(a.Total_AdminFee_KHR,0)+ ISNULL(a.Total_RefFee_KHR,0)+ ISNULL(a.TotalCharge_KHR,0) + ISNULL(a.TotalRepay_KHR,0) + ISNULL(a.OtherTotal_KHR,0) + ISNULL(a.Total_Insurance_KHR,0)
	   --+ ISNULL(a.Total_USDTOKHR_KHR,0)) income
	   --,ISNULL(a.Total_Deposit_KH,0)Total_Deposit_KH
	   --, ISNULL(a.Total_AdminFee_KHR,0)Total_AdminFee_KHR
	   --, ISNULL(a.Total_RefFee_KHR,0)Total_RefFee_KHR
	   --, ISNULL(a.TotalCharge_KHR,0)TotalCharge_KHR
	   --, ISNULL(a.TotalRepay_KHR,0)TotalRepay_KHR
	   --,ISNULL(a.OtherTotal_KHR,0)OtherTotal_KHR
	   --, ISNULL(a.Total_Insurance_KHR,0)Total_Insurance_KHR
	   --, ISNULL(a.Total_USDTOKHR_KHR,0)Total_USDTOKHR_KHR


into #Final
From #LDLD1 a




select * from #Final
End



--select * from OwnerTransaction
