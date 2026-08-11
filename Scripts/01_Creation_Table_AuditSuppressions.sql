-- ============================================
 Script : Création de la table AuditSuppressions
 Objectif : historiser les suppressions d'écritures comptables
-- ============================================

CREATE TABLE [dbo].[AuditSuppressions](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [NumeroEcriture] [numeric](10, 0) NULL,
    [DateEcriture] [datetime] NULL,
    [JournalEcriture] [nvarchar](100) NULL,
    [CompteEcriture] [nvarchar](100) NULL,
    [MontantEcriture] [numeric](13, 4) NOT NULL,
    [SensEcriture] [nvarchar](1) NULL,
    [ReferenceEcriture] [nvarchar](100) NULL,
    [DeviseEcriture] [nvarchar](100) NULL,
    [Libelle] [nvarchar](500) NOT NULL,
    [DateSuppression] [datetime] NOT NULL,
    [Motif] [nvarchar](500) NOT NULL,
    CONSTRAINT [PK_AuditSuppressions] PRIMARY KEY ([Id])
) ON [PRIMARY]
GO