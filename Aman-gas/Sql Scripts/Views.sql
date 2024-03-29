USE [aman-gas]
GO
/****** Object:  View [dbo].[CarUsersBalance_V]    Script Date: 26/08/2023 14:34:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create View [dbo].[CarUsersBalance_V] AS
SELECT ASS.UserId , Concat(US.FirstName , ' ' , US.LastName) Name ,
Concat (CC.FirstChar,' ', CC.SecondChar ,' ' , CC.ThirdChar ,' ',' ' , CC.CarNumbers  ) Car ,
isnull(C.Credit,0)Credit , isnull(D.Debit,0) Debit , (isnull(C.Credit,0) - isnull(D.Debit,0)) Balance
FROM [AssignPointss] ASS
join ASPNETUSERS US on  ASS.UserId = Us.Id 
left Join Cars CC on Us.Username = CC.[User] 
left join (Select UserId, isnull(Sum(Isnull([Count],0)),0) as Credit from [AssignPointss] where Status = 1 group by UserId)C on C.UserId = ASS.UserId
left join (Select UserId, isnull(Sum(Isnull([Count],0)),0) as Debit from [AssignPointss] where Status = 2 group by UserId)D on D.UserId = ASS.UserId
group by ASS.UserId,C.Credit , D.Debit ,US.FirstName , US.LastName,
CC.FirstChar , CC.SecondChar ,CC.ThirdChar,CC.CarNumbers
GO
/****** Object:  View [dbo].[SalesManPointDailyTracker_V]    Script Date: 26/08/2023 14:34:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [dbo].[SalesManPointDailyTracker_V] as
Select C.SalesManId , C.Name , isnull(C.Points,0) CreditPoints , isnull(D.Points,0) DebitPoints ,  C.Day ,C.DayInString
from (select SM.Name ,Sum(ASS.[Count]) Points , FFC.SalesManId ,
Case
when (Month(FFC.Date)>=10 and Day(FFC.Date) >=10) Then cast(Concat(YEAR(FFC.Date) , '/' , Month(FFC.Date),'/' , Day(FFC.Date)) as datetime) 
when (Month(FFC.Date)>=10 and Day(FFC.Date) <10 ) Then  cast(Concat(YEAR(FFC.Date) , '/' , Month(FFC.Date),'/0',Day(FFC.Date)) as datetime) 
when (Month(FFC.Date)<10 and Day(FFC.Date) >=10 ) Then cast( Concat(YEAR(FFC.Date) , '/0' , Month(FFC.Date),'/',Day(FFC.Date)) as datetime) 
when (Month(FFC.Date)>=10 ) Then  cast(Concat(YEAR(FFC.Date) , '/' , Month(FFC.Date)) as datetime) 
Else  cast(Concat(YEAR(FFC.Date) , '/0' , Month(FFC.Date),'/0',Day(FFC.Date))as datetime) 
End Day ,
Case
when (Month(FFC.Date)>=10 and Day(FFC.Date) >=10) Then Concat(YEAR(FFC.Date) , '/' , Month(FFC.Date),'/' , Day(FFC.Date))
when (Month(FFC.Date)>=10 and Day(FFC.Date) <10 )Then Concat(YEAR(FFC.Date) , '/' , Month(FFC.Date),'/0',Day(FFC.Date)) 
when (Month(FFC.Date)<10 and Day(FFC.Date) >=10 ) Then Concat(YEAR(FFC.Date) , '/0' , Month(FFC.Date),'/',Day(FFC.Date)) 
when (Month(FFC.Date)>=10 ) Then Concat(YEAR(FFC.Date) , '/' , Month(FFC.Date)) 
Else Concat(YEAR(FFC.Date) , '/0' , Month(FFC.Date),'/0',Day(FFC.Date))
End DayInString
 from AssignPointss ASS 
 left join (Select Id,Date , SalesManId from Fuelings where Status = 1 ) FFC  on FFC.Id = ASS.FuelingId 
 right join SalesMen SM on FFC.SalesManId =SM.Id 
 group by SM.Name,FFC.SalesManId ,YEAR(FFC.Date) , Month(FFC.Date) ,DAY(FFC.Date) )C 
full join (select SM.Name ,Sum(ASS.[Count]) Points , FFC.SalesManId ,
Case
when (Month(FFC.Date)>=10 and Day(FFC.Date) >=10) Then cast(Concat(YEAR(FFC.Date) , '/' , Month(FFC.Date),'/' , Day(FFC.Date)) as datetime) 
when (Month(FFC.Date)>=10 and Day(FFC.Date) <10 ) Then  cast(Concat(YEAR(FFC.Date) , '/' , Month(FFC.Date),'/0',Day(FFC.Date)) as datetime) 
when (Month(FFC.Date)<10 and Day(FFC.Date) >=10 ) Then cast( Concat(YEAR(FFC.Date) , '/0' , Month(FFC.Date),'/',Day(FFC.Date)) as datetime) 
when (Month(FFC.Date)>=10 ) Then  cast(Concat(YEAR(FFC.Date) , '/' , Month(FFC.Date)) as datetime) 
Else  cast(Concat(YEAR(FFC.Date) , '/0' , Month(FFC.Date),'/0',Day(FFC.Date))as datetime) 
End Day ,
Case
when (Month(FFC.Date)>=10 and Day(FFC.Date) >=10) Then Concat(YEAR(FFC.Date) , '/' , Month(FFC.Date),'/' , Day(FFC.Date))
when (Month(FFC.Date)>=10 and Day(FFC.Date) <10 )Then Concat(YEAR(FFC.Date) , '/' , Month(FFC.Date),'/0',Day(FFC.Date)) 
when (Month(FFC.Date)<10 and Day(FFC.Date) >=10 ) Then Concat(YEAR(FFC.Date) , '/0' , Month(FFC.Date),'/',Day(FFC.Date)) 
when (Month(FFC.Date)>=10 ) Then Concat(YEAR(FFC.Date) , '/' , Month(FFC.Date)) 
Else Concat(YEAR(FFC.Date) , '/0' , Month(FFC.Date),'/0',Day(FFC.Date))
End DayInString
from AssignPointss ASS 
left join (Select Id,Date , SalesManId from Fuelings where Status = 2 ) FFC  on FFC.Id = ASS.FuelingId 
right join SalesMen SM on FFC.SalesManId =SM.Id 
group by SM.Name,FFC.SalesManId ,YEAR(FFC.Date) , Month(FFC.Date) ,DAY(FFC.Date))D
on C.Name = D.Name and C.Day = D.Day and C.SalesManId = D.SalesManId and C.DayInString= D.DayInString
where C.SalesManId is not null

GO
/****** Object:  View [dbo].[SalesManPointMonthlyTracker_V]    Script Date: 26/08/2023 14:34:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[SalesManPointMonthlyTracker_V] as 
Select C.SalesManId , C.Name , isnull(C.Points,0) CreditPoints , isnull(D.Points,0) DebitPoints ,  C.[Month]
from ( select SM.Name ,Sum(ASS.[Count]) Points , FFC.SalesManId ,
Case when (Month(FFC.Date)>=10) Then Concat(YEAR(FFC.Date) , '/' , Month(FFC.Date)) Else Concat(YEAR(FFC.Date) , '/0' , Month(FFC.Date))
End [Month]
 from AssignPointss ASS 
 left join (Select Id,Date , SalesManId from Fuelings where Status = 1 ) FFC  on FFC.Id = ASS.FuelingId 
 right join SalesMen SM on FFC.SalesManId =SM.Id 
 group by SM.Name,FFC.SalesManId ,YEAR(FFC.Date) , Month(FFC.Date))C 
full join (select SM.Name ,Sum(ASS.[Count]) Points , FFC.SalesManId ,
Case when (Month(FFC.Date)>=10) Then Concat(YEAR(FFC.Date) , '/' , Month(FFC.Date)) Else Concat(YEAR(FFC.Date) , '/0' , Month(FFC.Date))
End [Month]
from AssignPointss ASS 
left join (Select Id,Date , SalesManId from Fuelings where Status = 2 ) FFC  on FFC.Id = ASS.FuelingId 
right join SalesMen SM on FFC.SalesManId =SM.Id 
group by SM.Name,FFC.SalesManId ,YEAR(FFC.Date) , Month(FFC.Date) )D
on C.Name = D.Name and C.Month = D.Month and C.SalesManId = D.SalesManId 
where C.SalesManId is not null
GO
/****** Object:  View [dbo].[StationPointDailyTracker_V]    Script Date: 26/08/2023 14:34:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[StationPointDailyTracker_V] as
Select C.StationId , C.Name , isnull(C.Points,0) CreditPoints , isnull(D.Points,0) DebitPoints ,  C.Day , C.DayInString
from ( select ST.Name ,Sum(ASS.[Count]) Points , FFS.StationId ,
Case
when (Month(FFS.Date)>=10 and Day(FFS.Date) >=10) Then cast(Concat(YEAR(FFS.Date) , '/' , Month(FFS.Date),'/' , Day(FFS.Date)) as datetime)
when (Month(FFS.Date)>=10 and Day(FFS.Date) <10 ) Then cast(Concat(YEAR(FFS.Date) , '/' , Month(FFS.Date),'/0',Day(FFS.Date)) as datetime)
when (Month(FFS.Date)<10 and Day(FFS.Date) >=10 ) Then cast(Concat(YEAR(FFS.Date) , '/0' , Month(FFS.Date),'/',Day(FFS.Date)) as datetime)
when (Month(FFS.Date)>=10 ) Then cast(Concat(YEAR(FFS.Date) , '/' , Month(FFS.Date)) as datetime)
Else cast(Concat(YEAR(FFS.Date) , '/0' , Month(FFS.Date),'/0',Day(FFS.Date))as datetime)
End Day ,
Case
when (Month(FFS.Date)>=10 and Day(FFS.Date) >=10) Then Concat(YEAR(FFS.Date) , '/' , Month(FFS.Date),'/' , Day(FFS.Date))
when (Month(FFS.Date)>=10 and Day(FFS.Date) <10 ) Then Concat(YEAR(FFS.Date) , '/' , Month(FFS.Date),'/0',Day(FFS.Date)) 
when (Month(FFS.Date)<10 and Day(FFS.Date) >=10 ) Then Concat(YEAR(FFS.Date) , '/0' , Month(FFS.Date),'/',Day(FFS.Date)) 
when (Month(FFS.Date)>=10 ) Then Concat(YEAR(FFS.Date) , '/' , Month(FFS.Date)) 
Else Concat(YEAR(FFS.Date) , '/0' , Month(FFS.Date),'/0',Day(FFS.Date))
End DayInString
from AssignPointss ASS 
left join (Select Id,Date , StationId from Fuelings where Status = 1 ) FFS  on FFS.Id = ASS.FuelingId 
right join Stations ST on FFS.StationId =ST.Id 
group by ST.Name,FFS.StationId ,YEAR(FFS.Date) , Month(FFS.Date),Day(FFS.Date))C 
 full join (  select ST.Name ,Sum(ASS.[Count]) Points , FFS.StationId ,
Case
when (Month(FFS.Date)>=10 and Day(FFS.Date) >=10) Then cast(Concat(YEAR(FFS.Date) , '/' , Month(FFS.Date),'/' , Day(FFS.Date)) as datetime)
when (Month(FFS.Date)>=10 and Day(FFS.Date) <10 ) Then cast(Concat(YEAR(FFS.Date) , '/' , Month(FFS.Date),'/0',Day(FFS.Date)) as datetime)
when (Month(FFS.Date)<10 and Day(FFS.Date) >=10 ) Then cast(Concat(YEAR(FFS.Date) , '/0' , Month(FFS.Date),'/',Day(FFS.Date)) as datetime)
when (Month(FFS.Date)>=10 ) Then cast(Concat(YEAR(FFS.Date) , '/' , Month(FFS.Date)) as datetime)
Else cast(Concat(YEAR(FFS.Date) , '/0' , Month(FFS.Date),'/0',Day(FFS.Date)) as datetime)
End Day ,
Case
when (Month(FFS.Date)>=10 and Day(FFS.Date) >=10) Then Concat(YEAR(FFS.Date) , '/' , Month(FFS.Date),'/' , Day(FFS.Date))
when (Month(FFS.Date)>=10 and Day(FFS.Date) <10 ) Then Concat(YEAR(FFS.Date) , '/' , Month(FFS.Date),'/0',Day(FFS.Date)) 
when (Month(FFS.Date)<10 and Day(FFS.Date) >=10 ) Then Concat(YEAR(FFS.Date) , '/0' , Month(FFS.Date),'/',Day(FFS.Date)) 
when (Month(FFS.Date)>=10 ) Then Concat(YEAR(FFS.Date) , '/' , Month(FFS.Date)) 
Else Concat(YEAR(FFS.Date) , '/0' , Month(FFS.Date),'/0',Day(FFS.Date))
End DayInString
from AssignPointss ASS 
left join (Select Id,Date , StationId from Fuelings where Status = 2 ) FFS  on FFS.Id = ASS.FuelingId 
right join Stations ST on FFS.StationId =ST.Id 
group by ST.Name,FFS.StationId ,YEAR(FFS.Date) , Month(FFS.Date) , Day(FFS.Date)
)D
on C.Name = D.Name and C.Day = D.Day and C.StationId = D.StationId and C.DayInString = D.DayInString
where C.StationId is not null

GO
/****** Object:  View [dbo].[StationPointMonthlyTracker_V]    Script Date: 26/08/2023 14:34:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[StationPointMonthlyTracker_V] as 
Select C.StationId , C.Name , isnull(C.Points,0) CreditPoints , isnull(D.Points,0) DebitPoints ,  C.[Month]
from ( select ST.Name ,Sum(ASS.[Count]) Points , FFS.StationId ,
Case when (Month(FFS.Date)>=10) Then Concat(YEAR(FFS.Date) , '/' , Month(FFS.Date)) Else Concat(YEAR(FFS.Date) , '/0' , Month(FFS.Date))
End [Month]
from AssignPointss ASS 
left join (Select Id,Date , StationId from Fuelings where Status = 1 ) FFS  on FFS.Id = ASS.FuelingId 
right join Stations ST on FFS.StationId =ST.Id 
group by ST.Name,FFS.StationId ,YEAR(FFS.Date) , Month(FFS.Date))C 
 full join (  select ST.Name ,Sum(ASS.[Count]) Points , FFS.StationId ,
Case when (Month(FFS.Date)>=10) Then Concat(YEAR(FFS.Date) , '/' , Month(FFS.Date)) Else Concat(YEAR(FFS.Date) , '/0' , Month(FFS.Date))
End [Month]
from AssignPointss ASS 
left join (Select Id,Date , StationId from Fuelings where Status = 2 ) FFS  on FFS.Id = ASS.FuelingId 
right join Stations ST on FFS.StationId =ST.Id 
group by ST.Name,FFS.StationId ,YEAR(FFS.Date) , Month(FFS.Date)
)D
on C.Name = D.Name and C.Month = D.Month and C.StationId = D.StationId
where C.StationId is not null
GO
/****** Object:  View [dbo].[TestView]    Script Date: 26/08/2023 14:34:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[TestView] 
as 

select c.FirstChar + '-' +c.SecondChar +'-' + c.ThirdChar + '-' + c.CarNumbers  as Name , c.[User], ct.ARName CarTypeAN from cars c 
 join CarTypes ct
on c.CarTypeId = ct.Id
GO
