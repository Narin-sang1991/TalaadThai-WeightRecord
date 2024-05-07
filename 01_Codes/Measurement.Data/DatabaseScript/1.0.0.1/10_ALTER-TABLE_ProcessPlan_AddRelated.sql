

ALTER TABLE [Mm].[ProcessPlan]
	DROP COLUMN [CoilQty]
GO
ALTER TABLE [Mm].[ProcessPlan]
	DROP COLUMN [CoilRelatedQty]
Go

EXEC sp_rename 'Mm.ProcessPlan.OrderNo', 'PosNo', 'COLUMN';
Go

EXEC sp_rename 'Mm.ProcessPlan.CoilPackingCode', 'CoilNo', 'COLUMN';
Go

ALTER TABLE [Mm].[ProcessPlan]
	ADD [RelatedId] uniqueidentifier
Go

ALTER TABLE [Mm].[ProcessPlan]
	ADD [IsDeleted] bit not null default 0
Go
