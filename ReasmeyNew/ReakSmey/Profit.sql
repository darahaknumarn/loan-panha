USE [M]
GO

/****** Object:  StoredProcedure [dbo].[sp_rptProfit]    Script Date: 1/28/2020 10:29:43 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [dbo].[sp_rptProfit] (
@StartDate Varchar(12),
@EndDate Varchar(12),@BrId varchar (3))
AS
BEGIN
SET NOCOUNT ON;
Declare @SDate as DateTime;
Declare @EDate as DateTime;
Declare @KHR as NVarchar(12);
Declare @USD as NVarchar(12);
Set @KHR=N'រៀល';
Set @USD=N'ដុល្លារ';
Set @SDate = @StartDate;
Set @EDate = @EndDate;
IF @BrID = 'All' set @BrID ='%';
-------------------------------- Other interest
---------------------------------------------------- Get Loan Payoff
select LD_ID,LD_BrId,LD_Dis_Amt,CU_ID 
into #payoff1
from BK_Loan a
where a.Date_Payoff between @SDate and @EDate and LD_BrId like @BrId
---------------------------------------------------
select b.LD_ID,b.LR_BrID,b.LR_Date, isnull(b.Prn_Paid,0)-a.LD_Dis_Amt  prn,b.LR_Date_Create,CU_ID
into #b1 
from BK_Loan a 
inner join (select max(LR_Date) LR_Date,max(LR_Date_Create)LR_Date_Create,LD_ID,LR_BrID ,sum(Prn) Prn_Paid from BK_LoanRepay where LR_Date <= @EDate and LD_ID in ( select LD_ID from #payoff1) and LR_BrID like @BrID group by LD_ID,LR_BrID)b  
on a.LD_ID=b.LD_ID and a.LD_BrId=b.LR_BrID 
where b.Prn_Paid-a.LD_Dis_Amt >0
-------
select LR_BrID,case when CU_ID=1 then sum(prn) else 0 end KHR,case when CU_ID=2 then sum(prn) else 0 end USD 
into #c1
from #b1 group by LR_BrID,CU_ID
-------
select LR_BrID,sum(isnull(KHR,0))KHR,sum(isnull(USD,0))USD 
into #d1
from #c1 group by LR_BrID
----- Loan OS
Select a.LD_BrId
,case when CU_ID=1 then sum(LD_Dis_Amt-isnull(b.Prn,0)) else 0  end LD_OS_KH,LD_Dis_Amt
,sum(isnull(b.Prn,0))PrnPaid,sum(LD_Dis_Amt-isnull(b.Prn,0)) LDOS,
Case when CU_ID=2 then SUM(LD_Dis_Amt-isnull(b.Prn,0)) else 0 end LD_OS_USD ,a.CU_ID ,a.LD_ID,a.LD_Status,a.Date_Payoff
into #LDOS1 
from BK_Loan a 
Left join 
(Select LD_ID,LR_BrID,sum(Prn)Prn from BK_LoanRepay where LR_Date<=@EDate and LR_BrID like @BrId group by LD_ID,LR_BrID) b
on a.LD_BrId=b.LR_BrID and a.LD_ID=b.LD_ID
where LD_Dis_Date<=@EDate 
and LD_BrId like @BrId
and a.Date_Payoff > @EDate
group by a.LD_BrId,a.CU_ID,a.LD_ID,LD_Dis_Amt,a.LD_Status,a.Date_Payoff
END
--select * from #LDOS1 where LDOS>0 return
-------------------------------------
select LD_BrId,case when CU_ID=1 then sum(LDOS) else 0 end LD_OS_KH ,
case when CU_ID=2 then sum(LDOS) else 0 end LD_OS_USD
into #LDOS2
from #LDOS1 where LDOS>0 and LD_BrId like @BrId group by LD_BrId,CU_ID
-----------------------------------------------
select LD_BrId,sum(isnull(LD_OS_KH,0))LD_OS_KH,sum(isnull(LD_OS_USD,0))LD_OS_USD 
into #LDOS
from #LDOS2 Group by LD_BrId
----------------------
select LD_BrId,case when CU_ID=1 then sum(PrnPaid-LD_Dis_Amt) else 0 end IntKHR,case when CU_ID=2 then sum(PrnPaid-LD_Dis_Amt) else 0 end IntUSD
into #Int11 
from #LDOS1 
where PrnPaid>LD_Dis_Amt and Date_Payoff between @SDate and @EDate group by LD_BrId,CU_ID
select LD_BrId,sum(isnull(IntKHR,0))IntKHR,sum(isnull(IntUSD,0))IntUSD into #Int1 from #Int11 Group by LD_BrId
-------------- Cash on hand end date
----Loan Repay
Select a.LD_ID,
       c.CU_ID,
       a.LR_BrID
        ,case when c.CU_ID=2 then sum(a.LR_Amount) else 0 end USD
       ,case when c.CU_ID=1 then sum(a.LR_Amount) else 0 end KHR
       ,case when c.CU_ID=2 then sum(isnull(a.LR_Charge,0)) else 0 end Charge_USD
       ,case when c.CU_ID=1 then sum(isnull(a.LR_Charge,0)) else 0 end Charge_KHR
Into #LD_Repay
From dbo.BK_LoanRepay a Inner Join dbo.BK_LoanSchedule b On a.LD_ID = b.LD_ID and a.SH_Date = b.SH_Date and a.LR_BrID = b.SH_BrId
     Inner Join dbo.BK_Loan c On c.LD_ID = a.LD_ID and c.LD_BrId = a.LR_BrID
Where  a.LR_Date <= @EDate
      and a.LR_BrID like @BrID
      group by a.LD_ID,c.CU_ID,a.LR_BrID
      --and c.CU_ID = @CUID;
Select a.LR_BrID,
SUM( a.KHR) as KHR,
 sum(a.USD) as USD
 , sum(a.Charge_KHR) Charge_KHR,
 sum(a.Charge_USD) Charge_USD
   Into #TotalRepay
From #LD_Repay a
Group By a.LR_BrID;
---------------------------------------------- Admin fee
Select 
       a.LD_ID, case when a.CU_ID=1 then sum(isnuLL(a.LD_ChargeAmt,0)) else 0 end KHR,case when a.CU_ID=2 then sum(isnuLL(a.LD_ChargeAmt,0)) else 0 end USD
      , case when a.CU_ID=1 then sum(isnuLL(a.LD_InAmt,0)) else 0 end InsuranceKHR,case when a.CU_ID=2 then sum(isnuLL(a.LD_InAmt,0)) else 0 end InsuranceUSD
	   ,        a.LD_BrId,       a.CU_ID
Into #LD_Fee
From BK_Loan a
Where a.LD_BrId like @BrID
      and a.LD_Dis_Date <= @EDate
  group by a.LD_ID,a.LD_BrId,a.CU_ID
  Select a.LD_BrId,
    sum(a.KHR)+sum(InsuranceKHR) KHR
	,sum(a.USD)+sum(InsuranceUSD)USD
	,sum(a.KHR) AdminKHR,sum(a.USD) AdminUSD
	,sum(InsuranceKHR)InsuranceKHR,sum(InsuranceUSD) InsuranceUSD
Into #Total_Fee
From #LD_Fee a 
Group By a.LD_BrId

----Loan Disbursment
Select 
       a.LD_ID, case when a.CU_ID=1 then sum(a.LD_Dis_Amt) else 0 end KHR,case when a.CU_ID=2 then sum(a.LD_Dis_Amt) else 0 end USD
       ,        a.LD_BrId,       a.CU_ID
Into #LD_Dis
From BK_Loan a
Where a.LD_BrId like @BrID
      and a.LD_Dis_Date <= @EDate
  group by a.LD_ID,a.LD_BrId,a.CU_ID
Select a.LD_BrId,
    sum(a.KHR) KHR,sum(a.USD)USD
Into #Total_Dis
From #LD_Dis a 
Group By a.LD_BrId

--Total Expense
Select 
       a.BrID,
     case when OPCurrency=N'រៀល' then sum(OPCost) else 0 end KHR,
        case when OPCurrency=N'ដុល្លារ' then sum(OPCost) else 0 end USD,
       a.OPCurrency
Into #EX
From ExpenseOperation a 
Where a.BrID like @BrID and a.OPDate <= @EDate group by a.BrID,OPCurrency
Select a.BrID,
   SUM( a.KHR) KHR,sum(a.USD) USD
Into #Total_Ex
From #EX a
Group by a.BrID

--Owner Withdrawal and Deposit
Select a.BrID,
       sum(a.USD) USD,
       sum(a.KHR) KHR
Into #Total_Deposit
From OwnerTransaction a 
where a.BrID like @BrID
     and a.OPDate <= @EDate and a.OPType='Deposit' group by a.BrID;

Select a.BrID,
       sum(a.USD) USD,
       sum(a.KHR) KHR
Into #Total_Withdrawal
From OwnerTransaction a 
where a.BrID like @BrID
     and a.OPDate <= @EDate and a.OPType='Withdrawal' group by a.BrID;

select BrId,SUM( USD)USD,SUM( KHR)KHR 
into #Total_USDTOKHR
from BK_Exchange where Date_Operation<=@EDate and Type='USDTOKHR'
group by BrId

select BrId,SUM( USD)USD,SUM( KHR)KHR 
into #Total_KHRTOUSD
from BK_Exchange where Date_Operation<=@EDate and Type='KHRTOUSD'
group by BrId
----------------------------------------- OtherIncome
select BrId,Amount,CU_ID into #OtherIncome
from BK_OtherIncome where Date_Operation <= @EDate and BrId like @BrId
select BrId
,case when CU_ID=1 then sum(ISNULL(Amount,0)) else 0 end KHR
,case when CU_ID=2 then sum(ISNULL(Amount,0)) else 0 end USD
into #OtherIncomeTem
 from #OtherIncome group by BrId,CU_ID
 --
 select BrId,sum(isnull(KHR,0))KHR,sum(isnull(USD,0))USD 
 into #OtherIncomeTotal
 from #OtherIncomeTem group by BrId
 ----------------------------------------- OtherIncome by date
select BrId,Amount,CU_ID into #OtherIncome2
from BK_OtherIncome where Date_Operation between @SDate and @EDate and BrId like @BrId
select BrId
,case when CU_ID=1 then sum(ISNULL(Amount,0)) else 0 end KHR
,case when CU_ID=2 then sum(ISNULL(Amount,0)) else 0 end USD
into #OtherIncomeTem2
 from #OtherIncome2 group by BrId,CU_ID
 --
 select BrId,sum(isnull(KHR,0))KHR,sum(isnull(USD,0))USD 
 into #OtherIncomeTotal2
 from #OtherIncomeTem2 group by BrId
------------------------------------------------------------------------------
select a.CompanyID,a.CompanyKhmerName,
 (isnull(b.KHR,0)+isnull(c.KHR,0)+isnull(i.KHR,0)+isnull(j.KHR,0)+isnull(c.Charge_KHR,0)+isnull(s.KHR,0))
 - (isnull(d.KHR,0)+isnull(e.KHR,0)+isnull(f.KHR,0)+isnull(h.KHR,0)) KHR 
,(isnull(b.USD,0)+isnull(c.USD,0)+isnull(h.USD,0)+isnull(j.USD,0)+isnull(c.Charge_USD,0)+isnull(s.USD,0))
- (isnull(d.USD,0)+isnull(e.USD,0)+isnull(f.USD,0)+isnull(i.USD,0))  USD 
--,b.USD'Depos',j.USD'Fee',c.Charge_USD 'charge',s.USD 'Other',d.USD 'With',e.USD 'Dis',f.USD 'Ex',i.USD 'USDToKHR'
into #cashonhand
from BK_Company a

------------------------------------------------------------------------------ Income
left join #Total_Deposit b on a.CompanyID=b.BrID
left join #Total_Fee j on a.CompanyID=j.LD_BrId
left join #TotalRepay c on a.CompanyID=c.LR_BrID
left join #OtherIncomeTotal s on a.CompanyID= s.BrId
------------------------------------------------------------------------------ Expense
left join #Total_Withdrawal d on a.CompanyID=d.BrID
left join #Total_Dis e on a.CompanyID=e.LD_BrId
left join #Total_Ex f on a.CompanyID=f.BrID
left join #Total_KHRTOUSD h on a.CompanyID=h.BrId
left join #Total_USDTOKHR i on a.CompanyID=i.BrId
where a.CompanyID LIKE @BrID 

--SELECT * FROM #cashonhand return
--RETURN
------------------------------------------------------------------------------- Balance in National Bank
Select BrId,sum(isnull(KHR,0))KHR,sum(isnull(USD,0))USD 
into #National
from Bank_Transaction where T_ID=5 and Date_Operation <=@EDate AND BrId LIKE @BrId GROUP by BrId
-------------------------------------------------------------------------------bank balance
select a.CompanyID,b.KHR-c.KHR KHR,b.USD-c.USD USD 
into #BankBalance 
FROM BK_Company a
left join (select BrId,sum(isnull(KHR,0))KHR,sum(isnull(USD,0))USD from Bank_Transaction where T_ID in (1,3) and Date_Operation<= @EDate and BrId LIKE @BrId group by BrId) b
on a.CompanyID=b.BrId
left join (select BrId,sum(isnull(KHR,0))KHR,sum(isnull(USD,0))USD from Bank_Transaction where T_ID in (2,4) and Date_Operation<= @EDate and BrId LIKE @BrId group by BrId) c
on a.CompanyID=c.BrId
-------------------------------------------------------------------------------Balance Asset
select a.OPCode,a.BrID,sum(a.ExCost) as ExCost
Into #tbl1
from ExpenseSchedule a 
where a.ExDate <= @EndDate
	  and a.BrID Like @BrID
Group By a.OPCode,a.BrID;

--select * from #tbl1
--return
Select a.BrID, b.ASID
,b.Name
,Convert(Varchar(12),a.OPDate,101) as OPDate
,a.OPCode
,a.OPDescription
,Case a.OPCurrency When @USD Then a.OPCost Else 0 End as 'Cost-USD'
,Case a.OPCurrency When @USD Then c.ExCost Else 0 End as 'Paid-USD'
,Case a.OPCurrency When @USD Then CONVERT(Numeric(18,2),a.OPCost - c.ExCost) Else 0 End as 'Bal-USD'

,Case a.OPCurrency When @KHR Then a.OPCost Else 0 End as 'Cost-KHR'
,Case a.OPCurrency When @KHR Then c.ExCost Else 0 End as 'Paid-KHR'
,Case a.OPCurrency When @KHR Then Convert(Numeric(18,0), a.OPCost - c.ExCost) Else 0 End as 'Bal-KHR'
into #Asset
From dbo.ExpenseOperation a 
Inner Join dbo.Asset b On a.ASID= b.ASID and a.BrID = b.BrID
Inner Join #tbl1 c on c.OPCode = a.OPCode and b.BrID=c.BrID
Where 
a.BrID Like @BrID
and a.OPTerm >1
and a.OPMatDate >= @EDate
Order By a.OPDate
--select * from Bank_Transaction
select BrID,sum([Bal-KHR])BalKHR,sum([Bal-USD])BalUSD 
into #AssetBalance
from #Asset group by BrID
--------------------------------------------- First Capital
select top 1 BrId,isnull(KHR,0)KHR,isnull(USD,0)USD into #FirstBank 
from  Bank_Transaction a 
where T_ID=3 and Date_Operation<=@EDate order by Date_Operation
---------------------------------------------- add capital
select a.BrId,sum(a.KHR)-isnull(b.KHR,0) KHR,sum(a.USD)-isnull(b.USD,0) USD
into #BankinDate
from Bank_Transaction a inner join #FirstBank b on a.BrId=b.BrId 
where T_ID=3 and Date_Operation<=@EDate
group by a.BrId,a.T_ID,b.KHR,b.USD
 ---------------------------------------- Interest
 select LR_BrID,LD_ID,sum(Int)Interest into #a from BK_LoanRepay  where LR_Date between @SDate and @EDate group by LR_BrID,LD_ID
 --select * from #a return
 select a.LR_BrID,case when b.CU_ID=1 then sum(a.Interest) else 0 end KHR,case when b.CU_ID=2 then sum(a.Interest) else 0 end USD
  into #IntFirst
  from #a a inner join BK_Loan b on a.LD_ID=b.LD_ID and a.LR_BrID=b.LD_BrId where b.LD_Dis_Date<=@EDate
  group by LR_BrID,b.CU_ID
  --select * from #IntFirst return
  -------------------------------------------
select LR_BrID,SUM(KHR)KHR,SUM(USD)USD into #Int from #IntFirst group by LR_BrID
  --------------------------------------- Admin fee
  select LD_BrId, case when CU_ID=1 then sum(isnull(LD_ChargeAmt,0)) else 0 end AdminKHR, 
  case when CU_ID=2 then sum(isnull(LD_ChargeAmt,0)) else 0 end AdminUSD
  , case when CU_ID=1 then sum(isnull(LD_InAmt,0)) else 0 end InsuranceKHR, 
  case when CU_ID=2 then sum(isnull(LD_InAmt,0)) else 0 end InsuranceUSD
  into #AdminFee1 
  from BK_Loan where LD_Dis_Date between @SDate and @EDate group by LD_BrId,CU_ID
  ------------
  Select LD_BrId,sum(isnull(AdminKHR,0))AdminKHR,sum(isnull(AdminUSD,0))AdminUSD
,sum(isnull(InsuranceKHR,0))InsuranceKHR,sum(isnull(InsuranceUSD,0))InsuranceUSD
  into #AdminFee
  from #AdminFee1 group by LD_BrId
  --------------------------------------- LD_Service income
  select LR_BrID,LD_ID,sum(isnull(LR_Service,0)) LR_Service
  into #firstService from BK_LoanRepay a 
  where LR_Date between @SDate and @EndDate and LR_BrID like @BrId
  group by LR_BrID,LD_ID

  select a.LR_BrID,case when b.CU_ID=1 then sum(a.LR_Service) else 0 end KHR,
  case when b.CU_ID=2 then sum(a.LR_Service) else 0 end USD
  into #LD_Service1
  from #firstService a inner join BK_Loan b on a.LD_ID=b.LD_ID and a.LR_BrID=b.LD_BrId 
  where b.LD_Dis_Date<= @EDate and b.LD_BrId like @BrId
  group by LR_BrID,CU_ID
  ---------------------------------------
  Select LR_BrID,SUM(isnull(KHR,0))KHR,SUM(isnull(USD,0))USD into #LD_Service
  from #LD_Service1 group by LR_BrID
  --------------------------------------- Other income
 select LR_BrID,LD_ID,sum(LR_Charge)Charge into #a1 from BK_LoanRepay  where LR_Date between @SDate and @EDate group by LR_BrID,LD_ID
 select a.LR_BrID,case when b.CU_ID=1 then sum(a.Charge) else 0 end KHR,case when b.CU_ID=2 then sum(a.Charge) else 0 end USD
  into #Charge1
  from #a1 a inner join BK_Loan b on a.LD_ID=b.LD_ID and a.LR_BrID=b.LD_BrId where b.LD_Dis_Date<=@EDate
  group by LR_BrID,b.CU_ID
  
  select LR_BrID,sum(isnull(KHR,0))KHR,sum(isnull(USD,0))USD into #Charge from #Charge1 group by LR_BrID
  ---------------------------------------- Expense
  select BrID, OPCode,sum(ExCost)ExCost into #b 
  from ExpenseSchedule 
  where ExDate between @SDate and @EDate and BrID like @BrId group by BrID,OPCode
  -------------------------------------------------------------------------
  select a.BrID,case when b.OPCurrency=@KHR then sum(ExCost) else 0 end KHR 
  ,case when b.OPCurrency=@USD then sum(ExCost) else 0 end USD 
  into #ExWith1
  from #b a 
  inner join ExpenseOperation b on a.BrID=b.BrID and a.OPCode=b.OPCode where b.OPTerm>1 group by a.BrID,b.OPCurrency
  
  select BrID,sum(KHR)KHR,sum(USD)USD 
  into #ExWith
  from #ExWith1 group by BrID
  -----------------------------------------------
  select BrID,ASID,case when OPCurrency=@KHR then sum(OPCost) else 0 end KHR,case when OPCurrency=@USD then sum(OPCost) else 0 end USD
  into #ExWithout1 
  from ExpenseOperation 
  where OPDate between @SDate and @EDate and OPTerm=1 and BrID like @BrId
  group by OPCurrency,BrID,ASID
  
  select a.BrID,a.ASID
  --,b.Name
  ,sum(KHR)KHR,sum(USD)USD  
  into #ExWithoutF 
  from #ExWithout1 a 
  --inner join Asset b on a.ASID=b.ASID and a.BrID=b.BrID 
  group by a.BrID,a.ASID
  --,b.Name

  ---------------------------------------
  select a.BrID,a.ASID
  ,b.Name
  ,KHR,USD   
  into #ExWithout
  from #ExWithoutF a
  inner join Asset b on a.ASID=b.ASID and a.BrID=b.BrID
  where b.BrID like @BrId
  --where 
    --group by a.BrID,a.ASID,b.Name
	  --select *from #ExWithout return
  ------------------------------------------------- Other Deposit
  select case when CU_ID=@KHR then sum(isnull(LD_Dis_Amt,0)) else 0 end KHR,
  case when CU_ID=@USD then sum(isnull(LD_Dis_Amt,0)) else 0 end USD,a.LD_BrId
  --,isnull(convert(int,a.LD_ID),0)-isnull(convert(int,b.LD_ID),0) a
  into #Other
  from OtherDeposit a 
  left join (select * from BK_OtherDepositPayoff where Date_Payoff <=@EndDate and LD_BrId like @BrId) b on 
  a.LD_BrId=b.LD_BrId and a.LD_ID=b.LD_ID
  where LD_Dis_Date <=@EDate and a.LD_BrId like @BrId
   and isnull(convert(int,a.LD_ID),0)-isnull(convert(int,b.LD_ID),0) <>0
  group by a.LD_BrId,CU_ID,a.LD_ID,b.LD_ID

  --select * from #Other
  select LD_BrId,sum(isnull(KHR,0))KHR,sum(isnull(USD,0))USD 
  into #OtherDeposit
  from #Other group by LD_BrId
  --select * from #OtherDeposit return


  ------------------------------------ Create Temp table
 Create Table #LD(
TxnOrder Int,
BrID Varchar(12),Descriptions nvarchar(100),
KHR Numeric(18,2),
USD Numeric(18,2),
);
--------------------------------------------------------------------------------------- Start insert data to temp table
--------------------------------------------------------------------------------- Loan Outstanding
Insert Into #LD(TxnOrder,BrID,Descriptions,KHR,USD)
Select 1,'','Loan Outstanding',SUM(ISNULL(a.LD_OS_KH,0))KHR,SUM(ISNULL(a.LD_OS_USD,0))USD
From BK_Company b left join #LDOS a on b.CompanyID=a.LD_BrId 
where b.CompanyID like @BrId;
--select * from #LDOS
----------------------------------------------------------------------------------- Cash on hand
Insert Into #LD(TxnOrder,BrID,Descriptions,KHR,USD)
Select 2,'','Cash on Hand',SUM(ISNULL(a.KHR,0))KHR,SUM(ISNULL(a.USD,0))USD 
From BK_Company b 
LEFT join #cashonhand a on b.CompanyID=a.CompanyID
where b.CompanyID like @BrId;
----------------------------------------------------------------------------------- Bank Balance
Insert Into #LD(TxnOrder,BrID,Descriptions,KHR,USD)
Select 3,'','Bank Balance',SUM(ISNULL(a.KHR,0))KHR,SUM(ISNULL(a.USD,0))USD 
From BK_Company b 
LEFT join #BankBalance a on b.CompanyID=a.CompanyID
where b.CompanyID like @BrId;
----------------------------------------------------------------------------------- National Balance
Insert Into #LD(TxnOrder,BrID,Descriptions,KHR,USD)
Select 4,'','Balance in NBC',SUM(ISNULL(a.KHR,0))KHR,SUM(ISNULL(a.USD,0))USD 
From BK_Company b 
LEFT join #National a on b.CompanyID=a.BrId
where b.CompanyID like @BrId;
----------------------------------------------------------------------------------- Asset Balance
Insert Into #LD(TxnOrder,BrID,Descriptions,KHR,USD)
Select 5,'','Asset Balance',SUM(ISNULL(a.BalKHR,0))KHR,SUM(ISNULL(a.BalUSD,0))USD 
From BK_Company b 
LEFT join #AssetBalance a on b.CompanyID=a.BrID
where b.CompanyID like @BrId;
-------------------------------------------------------------------------------------Capital
insert into #LD(BrID,KHR,USD) 
select '',SUM(ISNULL(b.KHR,0)+isnull(c.KHR,0)+isnull(d.KHR,0)) KHR,SUM(ISNULL(b.USD,0)+isnull(c.USD,0)+isnull(d.USD,0)) USD
from BK_Company a 
left join #FirstBank b on a.CompanyID=b.BrId
left join #BankinDate c on a.CompanyID=c.BrId
left join #OtherDeposit d on a.CompanyID=d.LD_BrId
where CompanyID like @BrId;
---------- Detail Capital
Insert Into #LD(TxnOrder,BrID,Descriptions,KHR,USD)
Select 5,'','First Capital',SUM(ISNULL(a.KHR,0))KHR,SUM(ISNULL(a.USD,0))USD 
From BK_Company b 
LEFT join #FirstBank a on b.CompanyID=a.BrId
where b.CompanyID like @BrId;

Insert Into #LD(TxnOrder,BrID,Descriptions,KHR,USD)
Select 6,'','Add capital',SUM(ISNULL(b.KHR,0))KHR,SUM(ISNULL(b.USD,0))USD 
From BK_Company a 
LEFT join #BankinDate b on a.CompanyID=b.BrId
where a.CompanyID like @BrId;

Insert Into #LD(TxnOrder,BrID,Descriptions,KHR,USD)
Select 7,'','Other Deposit',SUM(ISNULL(b.KHR,0))KHR,SUM(ISNULL(b.USD,0))USD 
From BK_Company a 
LEFT join #OtherDeposit b on a.CompanyID=b.LD_BrId
where a.CompanyID like @BrId;

Insert Into #LD(TxnOrder,BrID,Descriptions,KHR,USD)
values( 8,'','Retain Earning',0,0 )
--From BK_Company a where CompanyID like @BrId;
----------------------------------------------------------------------------------------- Income
insert into #LD(BrID,KHR,USD) 
SELECT ''
,SUM(ISNULL(b.KHR,0)+isnull(c.IntKHR,0)+isnull(d.AdminKHR,0)+InsuranceKHR+isnull(e.KHR,0)+isnull(f.KHR,0)+isnull(g.KHR,0)+isnull(s.KHR,0)) KHR,
SUM(ISNULL(b.USD,0)+isnull(c.IntUSD,0)+isnull(d.AdminUSD,0)+InsuranceUSD+ isnull(e.USD,0)+isnull(f.USD,0)+isnull(g.USD,0)+isnull(s.USD,0)) USD
from BK_Company a 
left join #Int b on a.CompanyID=b.LR_BrID
left join #Int1 c on a.CompanyID=c.LD_BrId
left join #AdminFee d on a.CompanyID=d.LD_BrId
left join #Charge e on a.CompanyID = e.LR_BrID
left join #LD_Service f on a.CompanyID=f.LR_BrID
left join #d1 g on a.CompanyID=g.LR_BrID
left join #OtherIncomeTotal2 s on a.CompanyID=s.BrId

where CompanyID like @BrId;
----------- income detail
Insert Into #LD(TxnOrder,BrID,Descriptions,KHR,USD)
Select 9,'','Interest Income'
,SUM(ISNULL(a.KHR,0)+isnull(b.IntKHR,0)+isnull(c.KHR,0))
,SUM(ISNULL(a.USD,0)+isnull(b.IntUSD,0)+ isnull(c.USD,0))
From BK_Company d 
left join #Int a on d.CompanyID=a.LR_BrID
left join #Int1 b on a.LR_BrID=b.LD_BrId
left join #d1 c on a.LR_BrID=c.LR_BrID
where d.CompanyID like @BrId;
--select * from #d1
Insert Into #LD(TxnOrder,BrID,Descriptions,KHR,USD)
Select 10,'','Admin Fee Income',SUM(ISNULL(a.AdminKHR,0))AdminKHR,SUM(ISNULL(a.AdminUSD,0))AdminUSD 
From BK_Company b 
LEFT join #AdminFee a on b.CompanyID=a.LD_BrId
where b.CompanyID like @BrId;

Insert Into #LD(TxnOrder,BrID,Descriptions,KHR,USD)
Select 11,'','Insurance Fee Income'
,SUM(ISNULL(a.InsuranceKHR,0))InsuranceKHR,SUM(ISNULL(a.InsuranceUSD,0))InsuranceUSD 
From BK_Company b left join #AdminFee a on b.CompanyID=a.LD_BrId
where b.CompanyID like @BrId;

Insert Into #LD(TxnOrder,BrID,Descriptions,KHR,USD)
Select 12,'','Charge Income',SUM(ISNULL(a.KHR,0))KHR,SUM(ISNULL(a.USD,0)) USD
From BK_Company b 
LEFT join #Charge a on b.CompanyID=a.LR_BrID
where b.CompanyID like @BrId;

Insert Into #LD(TxnOrder,BrID,Descriptions,KHR,USD)
Select 13,'','Operation Fee Income',
SUM(ISNULL(a.KHR,0))KHR,SUM(ISNULL(a.USD,0))USD 
From BK_Company b 
LEFT join #LD_Service a on b.CompanyID=a.LR_BrID
where b.CompanyID like @BrId;

Insert Into #LD(TxnOrder,BrID,Descriptions,KHR,USD)
VALUES( 14,'','Written Off Income',0,0 )
--From BK_Company a where CompanyID like @BrId;

Insert Into #LD(TxnOrder,BrID,Descriptions,KHR,USD)
Select 15,'','Other Income',SUM(ISNULL(s.KHR,0))KHR,SUM(ISNULL(s.USD,0))USD 
From BK_Company a 
left join #OtherIncomeTotal2 s on a.CompanyID=s.BrId
where CompanyID like @BrId;
------------------------------------------------------------------------------------------ Expense
insert into #LD(BrID,KHR,USD) 
select '' ,SUM(ISNULL(b.KHR,0)+isnull(c.KHR,0) )KHR,SUM(ISNULL(b.USD,0)+isnull(c.USD,0)) USD
from BK_Company a 
inner join (select BrID,sum(isnull(KHR,0))KHR,sum(isnull(USD,0))USD from #ExWithout where BrID like @BrId group by BrID) b on a.CompanyID=b.BrID
inner join #ExWith c on a.CompanyID=c.BrID
where CompanyID like @BrId;
--select * from #ExWith
--select * from #ExWithout return
----------------------------- Detail
Insert Into #LD(TxnOrder,BrID,Descriptions,KHR,USD)
Select 16,a.BrID,a.Name,a.KHR,a.USD From #ExWithout a;

Insert Into #LD(TxnOrder,BrID,Descriptions,KHR,USD)
Select 17,'','Depreciation Expense',
SUM(ISNULL(a.KHR,0))KHR,sum(ISNULL(a.USD,0))USD 
From BK_Company b 
LEFT join #ExWith a on b.CompanyID=a.BrID
where b.CompanyID like @BrId;

--select * from #LD Return
----------------------------------------------------------------- Output
select Descriptions,'-' Code,KHR,USD from #LD

  --select * from #ExWith
  --select * from #ExWithout1
  --select * from #Int
  --select * from #AdminFee



GO

