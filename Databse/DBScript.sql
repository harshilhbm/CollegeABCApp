USE [CollegeABC]
GO
/****** Object:  Table [dbo].[CityMaster]    Script Date: 01-10-2021 13:11:51 ******/
SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CityMaster](
	[Cityid] [int] IDENTITY(1,1) NOT NULL,
	[CityName] [varchar](50) NULL,
	[StateId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_CityMaster] PRIMARY KEY CLUSTERED 
(
	[Cityid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CountryMaster]    Script Date: 01-10-2021 13:11:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CountryMaster](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [varchar](50) NULL,
 CONSTRAINT [PK_CountryMaster] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StateMaster]    Script Date: 01-10-2021 13:11:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StateMaster](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [varchar](50) NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_StateMaster] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student_Master]    Script Date: 01-10-2021 13:11:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student_Master](
	[Id] [uniqueidentifier] NULL,
	[StudentName] [varchar](50) NULL,
	[Middlename] [varchar](50) NULL,
	[Lastname] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[Gender] [varchar](50) NULL,
	[Address] [varchar](500) NULL,
	[Cityid] [int] NULL,
	[StateId] [int] NULL,
	[CountryId] [int] NULL,
	[Zipcode] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Student_Master] ADD  CONSTRAINT [DF__Student_Mast__ID__3F466844]  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  StoredProcedure [dbo].[DeleteStudent]    Script Date: 01-10-2021 13:11:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  Harshil Mistry  
-- Create date: 27-09-2021  
-- Description: Delete Student  
-- =============================================    
CREATE PROCEDURE [dbo].[DeleteStudent] (
	@Id NVARCHAR(50)
	,@ReturnCode NVARCHAR(20) OUTPUT
	)
AS
BEGIN
	SET NOCOUNT ON;
	SET @ReturnCode = 'C200'

	IF NOT EXISTS (
			SELECT 1
			FROM Student_Master
			WHERE Id = @Id
			)
	BEGIN
		SET @ReturnCode = 'C203'

		RETURN
	END
	ELSE
	BEGIN
		DELETE
		FROM Student_Master
		WHERE Id = @Id

		SET @ReturnCode = 'C200'

		RETURN
	END
END
GO
/****** Object:  StoredProcedure [dbo].[GetStudents]    Script Date: 01-10-2021 13:11:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================      
-- Author:  Harshil B. Mistry    
-- Create date: 25-09-2021    
-- Description: 'Get User Details'    
-- EXEC GetStudents
-- =============================================      
CREATE PROCEDURE [dbo].[GetStudents]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT   
		 S.*
		,CM.CityName AS City
		,SM.StateName AS [State]
		,CCM.CountryName AS Country
	FROM Student_Master(NOLOCK) AS S
	INNER JOIN CountryMaster CCM ON S.CountryId = CCM.CountryId
	INNER JOIN StateMaster SM ON S.StateId = SM.StateId
	INNER JOIN CityMaster CM ON S.Cityid = CM.Cityid
	ORDER BY Id ASC

END

GO
/****** Object:  StoredProcedure [dbo].[GetUsers]    Script Date: 01-10-2021 13:11:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- EXEC GetUsers  
-- =============================================  
CREATE PROCEDURE [dbo].[GetUsers]  
AS  
BEGIN  
 SET NOCOUNT ON;  
 SELECT * FROM Student_Master(NOLOCK) ORDER BY Id ASC  
END  
  
GO
/****** Object:  StoredProcedure [dbo].[SaveStudent]    Script Date: 01-10-2021 13:11:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================          
-- Author:  Harshil B. Mistry    
-- Create date: 25-09-2021    
-- Description: Save Student Details     
-- =============================================         
--EXEC [SaveStudent] '70719ef1-9ecf-45f9-b72a-ed3ebccb93b1',  
--'XYZ','XYZ','XYZ','1-ds-54545','Male','13 fgdsgds adfhdjd fdf fds fsfsf',  
--'London','London','UK',324234234,''  
CREATE PROCEDURE [dbo].[SaveStudent] (    
  @Id NVARCHAR(50) = null   
 ,@StudentName NVARCHAR(50)    
 ,@Middlename NVARCHAR(50)    
 ,@Lastname NVARCHAR(50)    
 ,@Phone NVARCHAR(50)    
 ,@Gender NVARCHAR(50)    
 ,@Address NVARCHAR(500)    
 ,@Cityid NVARCHAR(50)    
 ,@StateId NVARCHAR(50)    
 ,@CountryId NVARCHAR(50)    
 ,@Zipcode INT    
 ,@ReturnCode NVARCHAR(20) OUTPUT    
 )    
AS    
BEGIN    
 SET @ReturnCode = 'C200'    
    
IF (LEN(ISNULL(@Id, '')) > 0)  
 BEGIN    
  IF EXISTS (    
    SELECT 1    
    FROM Student_Master    
    WHERE Phone = @Phone AND Id <> @Id  
    )    
  BEGIN    
   SET @ReturnCode = 'C201'    
   RETURN    
  END    
    
  UPDATE Student_Master    
  SET StudentName = @StudentName    
   ,Middlename = @Middlename    
   ,Lastname = @Lastname    
   ,Phone = @Phone    
   ,Gender = @Gender    
   ,Address = @Address    
   ,Cityid = @Cityid
   ,StateId = @StateId    
   ,CountryId = @CountryId
   ,Zipcode = @Zipcode    
    WHERE Id = @Id        
  
  SET @ReturnCode = 'C2001'    
 END    
 ELSE    
 BEGIN    
  IF EXISTS (    
    SELECT 1    
    FROM Student_Master    
    WHERE Phone = @Phone    
    )    
  BEGIN    
   SET @ReturnCode = 'C202'    
    
   RETURN    
  END    
    
  INSERT INTO Student_Master (    
   StudentName    
   ,Middlename    
   ,Lastname    
   ,Phone    
   ,Gender    
   ,Address    
   ,Cityid    
   ,StateId    
   ,CountryId    
   ,Zipcode    
   )    
  VALUES (    
   @StudentName    
   ,@Middlename    
   ,@Lastname    
   ,@Phone    
   ,@Gender    
   ,@Address    
   ,@Cityid    
   ,@StateId    
   ,@CountryId    
   ,@Zipcode    
   )    
    
  SET @ReturnCode = 'C2002'    
 END    
END  
  
  
  
select * from Student_Master
GO
