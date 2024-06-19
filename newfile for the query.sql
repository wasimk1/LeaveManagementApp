
CREATE TABLE [dbo].[USERS_RECORDS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TXT_NAME] [varchar](200) NULL,
	[TOT_SICK_LV] [decimal](18, 1) NULL,
	[TOT_CASUAL_LV] [decimal](18, 1) NULL,
	[SYS_DATE_TIME] [datetime] NULL,
	[ALL_BAL_LEAVE] [decimal](18, 1) NULL,
	[USER_MODE] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[USERS_RECORDS] ADD  DEFAULT ((0)) FOR [USER_MODE]
GO

CREATE TABLE LOGINANDOUT(REF_ID BIGINT identity(1,1), EMP_ID BIGINT, TXT_USERNAME VARCHAR(100), 
LOGIN_TIME DATETIME, LOGOUT_TIME DATETIME, EMP_STATUS VARCHAR(50))

CREATE SEQUENCE SEQ_EMP_ID
AS BIGINT
START WITH 1001
MINVALUE 1001
MAXVALUE 99999
INCREMENT BY 1
CACHE 
GO