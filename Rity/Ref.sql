USE Morokot
ALTER TABLE dbo.BK_Loan ADD PayOff numeric(18, 2) NULL, Ref numeric(18, 2) NULL ;
ALTER TABLE TRACE_Loan ADD PayOff numeric(18, 2) NULL, Ref numeric(18, 2) NULL ;
USE TempMorkot
ALTER TABLE dbo.BK_Loan ADD PayOff numeric(18, 2) NULL, Ref numeric(18, 2) NULL ;
ALTER TABLE TRACE_Loan ADD PayOff numeric(18, 2) NULL, Ref numeric(18, 2) NULL ;