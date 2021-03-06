USE [master]
GO
/****** Object:  Database [oriondb]    Script Date: 8/21/2017 6:52:16 PM ******/
CREATE DATABASE [oriondb] ON  PRIMARY 
( NAME = N'oriondb', FILENAME = N'D:\MS-SQL 2008 EN\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\oriondb.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'oriondb_log', FILENAME = N'D:\MS-SQL 2008 EN\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\oriondb_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [oriondb] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [oriondb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [oriondb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [oriondb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [oriondb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [oriondb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [oriondb] SET ARITHABORT OFF 
GO
ALTER DATABASE [oriondb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [oriondb] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [oriondb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [oriondb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [oriondb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [oriondb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [oriondb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [oriondb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [oriondb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [oriondb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [oriondb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [oriondb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [oriondb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [oriondb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [oriondb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [oriondb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [oriondb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [oriondb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [oriondb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [oriondb] SET  MULTI_USER 
GO
ALTER DATABASE [oriondb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [oriondb] SET DB_CHAINING OFF 
GO
USE [oriondb]
GO
/****** Object:  User [abhijeet]    Script Date: 8/21/2017 6:52:17 PM ******/
CREATE USER [abhijeet] FOR LOGIN [abhijeet] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [abhijeet]
GO
/****** Object:  Table [dbo].[app_error_log]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[app_error_log](
	[error_log_id] [int] IDENTITY(1,1) NOT NULL,
	[error_message] [nvarchar](max) NOT NULL,
	[user_id] [int] NULL,
	[created_date] [datetime] NOT NULL,
	[ApplicationName] [varchar](100) NULL,
 CONSTRAINT [PK_app_error_log] PRIMARY KEY CLUSTERED 
(
	[error_log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[App_status]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[App_status](
	[App_status_id] [int] IDENTITY(1,1) NOT NULL,
	[status] [varchar](50) NULL,
 CONSTRAINT [PK_App_status] PRIMARY KEY CLUSTERED 
(
	[App_status_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[city]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[city](
	[cityID] [int] IDENTITY(1,1) NOT NULL,
	[city] [varchar](100) NOT NULL,
	[zipCode] [varchar](10) NULL,
	[latitude] [varchar](50) NULL,
	[longitude] [varchar](50) NULL,
	[stateID] [int] NOT NULL,
	[dataIsCreated] [datetime] NOT NULL,
	[dataIsUpdated] [datetime] NOT NULL,
	[isActive] [bit] NOT NULL,
	[isDelete] [bit] NOT NULL,
 CONSTRAINT [PK_city] PRIMARY KEY CLUSTERED 
(
	[cityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[companyDetails]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[companyDetails](
	[companyID] [bigint] IDENTITY(1,1) NOT NULL,
	[companyName] [varchar](500) NOT NULL,
	[cityID] [int] NOT NULL,
	[address] [varchar](500) NOT NULL,
	[website] [varchar](max) NULL,
	[companyIndustry] [int] NOT NULL,
	[companyDescription] [varchar](max) NOT NULL,
	[gstNo] [varchar](500) NULL,
	[tinNo] [varchar](500) NULL,
	[ctsNo] [varchar](500) NULL,
	[logo] [varchar](max) NULL,
	[dataIsCreated] [datetime] NOT NULL,
	[isActive] [bit] NOT NULL,
	[isDeleted] [bit] NULL,
	[modifiedBy] [bigint] NULL,
	[dataIsUpdated] [datetime] NOT NULL,
	[employerTypeID] [int] NOT NULL,
 CONSTRAINT [PK_companyDetails] PRIMARY KEY CLUSTERED 
(
	[companyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[country]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[country](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[Country] [varchar](100) NOT NULL,
	[dataIsCreated] [datetime] NOT NULL,
	[dataIsUpdated] [datetime] NOT NULL,
	[isActive] [bit] NOT NULL,
	[isDelete] [bit] NOT NULL,
 CONSTRAINT [PK_country] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[currency]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[currency](
	[currencyID] [int] NOT NULL,
	[currency] [varchar](80) NOT NULL,
	[dataIsCreated] [datetime] NOT NULL,
	[dataIsUpdated] [datetime] NOT NULL,
	[isActive] [bit] NOT NULL,
	[isDelete] [bit] NOT NULL,
 CONSTRAINT [PK_currency] PRIMARY KEY CLUSTERED 
(
	[currencyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Education]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Education](
	[EducationID] [int] IDENTITY(1,1) NOT NULL,
	[educationName] [varchar](100) NOT NULL,
	[dataIsCreated] [datetime] NOT NULL,
	[dataIsUpdated] [datetime] NULL,
	[isActive] [bit] NOT NULL,
	[isDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Education] PRIMARY KEY CLUSTERED 
(
	[EducationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmployerDetails]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmployerDetails](
	[EmployerID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Mobile] [varchar](15) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[dataIsCreated] [datetime] NOT NULL,
	[isActive] [bit] NOT NULL,
	[isDelete] [bit] NOT NULL,
	[companyID] [bigint] NOT NULL,
	[roleID] [int] NOT NULL,
	[dataIsUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_EmployerDetails] PRIMARY KEY CLUSTERED 
(
	[EmployerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[employerType]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[employerType](
	[employerTypeID] [int] NOT NULL,
	[employerType] [varchar](100) NULL,
	[dataIsCreated] [datetime] NOT NULL,
	[dataIsUpdated] [datetime] NOT NULL,
	[isActive] [bit] NOT NULL,
	[isDelete] [bit] NOT NULL,
 CONSTRAINT [PK_employerType] PRIMARY KEY CLUSTERED 
(
	[employerTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmploymentType]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmploymentType](
	[EmploymentTypeID] [int] IDENTITY(1,1) NOT NULL,
	[EmploymentType] [varchar](100) NOT NULL,
	[dataIsCreated] [datetime] NOT NULL,
	[dataIsUpdated] [datetime] NOT NULL,
	[isActive] [bit] NOT NULL,
	[isDelete] [bit] NOT NULL,
 CONSTRAINT [PK_EmploymentType] PRIMARY KEY CLUSTERED 
(
	[EmploymentTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[finalQendidateList]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[finalQendidateList](
	[finalCandidateID] [bigint] IDENTITY(1,1) NOT NULL,
	[jobID] [bigint] NOT NULL,
	[qenID] [bigint] NOT NULL,
	[candidateSalary] [nvarchar](50) NULL,
	[candidatePosition] [nvarchar](200) NULL,
	[expectedJoiningDate] [datetime] NULL,
	[dataIsCreated] [datetime] NULL,
	[dataIsUpdated] [datetime] NULL,
 CONSTRAINT [PK_finalQendidateList] PRIMARY KEY CLUSTERED 
(
	[finalCandidateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[industry]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[industry](
	[industryID] [int] IDENTITY(1,1) NOT NULL,
	[industryName] [varchar](100) NULL,
	[dataIsCreated] [datetime] NOT NULL,
	[dataIsUpdated] [datetime] NOT NULL,
	[isActive] [bit] NOT NULL,
	[isDelete] [bit] NOT NULL,
 CONSTRAINT [PK_industry] PRIMARY KEY CLUSTERED 
(
	[industryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[interviewedCandidateList]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[interviewedCandidateList](
	[jobID] [bigint] NOT NULL,
	[qenID] [bigint] NOT NULL,
	[interviewResult] [nvarchar](200) NULL,
	[interviewComment] [nvarchar](500) NULL,
	[dataIsCreated] [datetime] NULL,
	[dataIsUpdated] [datetime] NULL,
	[interviewedListID] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_interviewedCandidateList] PRIMARY KEY CLUSTERED 
(
	[interviewedListID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[jobDetails]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[jobDetails](
	[jobID] [bigint] IDENTITY(1,1) NOT NULL,
	[jobTitle] [varchar](500) NOT NULL,
	[jobDescription] [text] NOT NULL,
	[workExpMin] [int] NOT NULL,
	[salary] [int] NOT NULL,
	[currency] [varchar](100) NOT NULL,
	[unit] [varchar](100) NULL,
	[industryID] [int] NOT NULL,
	[cityID] [int] NOT NULL,
	[EmploymentTypeID] [int] NOT NULL,
	[EducationReq] [varchar](500) NULL,
	[NoOfOpenings] [int] NOT NULL,
	[createdBy] [bigint] NOT NULL,
	[dataIsCreated] [datetime] NOT NULL,
	[modifiedBy] [bigint] NULL,
	[dataIsUpdated] [datetime] NULL,
	[jobStatusID] [int] NOT NULL,
	[jobContactPersonName] [varchar](500) NULL,
	[jobContactPersonMobile] [varchar](50) NULL,
	[jobContactPersonEmail] [varchar](100) NULL,
	[isActive] [bit] NOT NULL,
	[isDelete] [bit] NOT NULL,
	[CompanyName] [varchar](500) NULL,
	[CompanyDescription] [varchar](1000) NULL,
	[CompanyWebsite] [varchar](1000) NULL,
	[responsesEmailID] [varchar](100) NULL,
	[CompanyLogo] [varchar](1000) NULL,
	[compeletPosted] [bit] NOT NULL,
	[salaryVisibleToEmployee] [bit] NOT NULL,
	[otherinformation] [text] NULL,
	[companyID] [bigint] NOT NULL,
	[gender] [varchar](6) NULL,
 CONSTRAINT [PK_jobDetails] PRIMARY KEY CLUSTERED 
(
	[jobID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[jobSavedQendidate]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[jobSavedQendidate](
	[jobSavedID] [bigint] IDENTITY(1,1) NOT NULL,
	[qenID] [bigint] NOT NULL,
	[jobID] [bigint] NOT NULL,
	[jobSavedDateTime] [datetime] NULL,
	[dataIsCreated] [datetime] NULL,
	[dataIsUpdated] [datetime] NULL,
 CONSTRAINT [PK_jobSavedQendidate] PRIMARY KEY CLUSTERED 
(
	[jobSavedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[jobSkills]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[jobSkills](
	[jobSkillsID] [bigint] IDENTITY(1,1) NOT NULL,
	[jobID] [bigint] NOT NULL,
	[skillsID] [int] NOT NULL,
 CONSTRAINT [PK_jobSkills] PRIMARY KEY CLUSTERED 
(
	[jobSkillsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[jobStatus]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[jobStatus](
	[jobStatusID] [int] IDENTITY(1,1) NOT NULL,
	[statuts] [varchar](100) NOT NULL,
	[dataIsCreated] [datetime] NOT NULL,
	[dataIsUpdated] [datetime] NOT NULL,
	[isActive] [bit] NOT NULL,
	[isDelete] [bit] NOT NULL,
 CONSTRAINT [PK_jobStatus] PRIMARY KEY CLUSTERED 
(
	[jobStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProfilePerformance]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfilePerformance](
	[performanceID] [bigint] IDENTITY(1,1) NOT NULL,
	[qenID] [bigint] NOT NULL,
	[EmployerID] [bigint] NOT NULL,
	[ViewedDate] [date] NULL,
	[Downloaded] [date] NULL,
	[Contacted] [date] NULL,
	[dataIsCreated] [date] NOT NULL,
 CONSTRAINT [PK_ProfilePerformance] PRIMARY KEY CLUSTERED 
(
	[performanceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[qenAppliedJob]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[qenAppliedJob](
	[qenID] [bigint] NOT NULL,
	[jobID] [bigint] NOT NULL,
	[appliedDate] [datetime] NOT NULL,
	[AppliedJobID] [bigint] IDENTITY(1,1) NOT NULL,
	[dataIsCreated] [datetime] NOT NULL,
	[dataIsUpdated] [datetime] NOT NULL,
	[isActive] [bit] NOT NULL,
	[isDelete] [bit] NOT NULL,
	[App_status_id] [int] NOT NULL,
 CONSTRAINT [PK_qenAppliedJob] PRIMARY KEY CLUSTERED 
(
	[AppliedJobID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[qendidateGraduation]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[qendidateGraduation](
	[gradid] [bigint] IDENTITY(1,1) NOT NULL,
	[qenID] [bigint] NOT NULL,
	[collegeName] [nvarchar](100) NULL,
	[collegeUniversity] [nvarchar](100) NULL,
	[courseName] [varchar](100) NULL,
	[courseField] [nvarchar](50) NULL,
	[gradPercentage] [float] NULL,
	[subjects] [nvarchar](40) NULL,
	[dataIsCreated] [datetime] NULL,
	[dataIsUpdated] [datetime] NULL,
	[YearPassing] [int] NULL,
 CONSTRAINT [PK_qendidateGraduation] PRIMARY KEY CLUSTERED 
(
	[gradid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[qendidateList]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[qendidateList](
	[qenID] [bigint] IDENTITY(1,1) NOT NULL,
	[qenName] [nvarchar](50) NULL,
	[qenAddress] [nvarchar](100) NULL,
	[qenCityID] [int] NOT NULL,
	[qenEmail] [nvarchar](320) NULL,
	[qenLinkdInUrl] [nvarchar](320) NULL,
	[qenPhone] [bigint] NULL,
	[qenNationality] [nvarchar](30) NULL,
	[qenPassport] [nvarchar](20) NULL,
	[dataIsCreated] [datetime] NULL,
	[dataIsUpdated] [datetime] NULL,
	[roleID] [int] NOT NULL,
	[isDelete] [bit] NOT NULL,
	[isActive] [bit] NOT NULL,
	[password] [varchar](50) NULL,
	[qenResume] [varchar](max) NULL,
	[qenCoverLetter] [text] NULL,
	[IsReferenceCleared] [bit] NULL,
	[qenImage] [varchar](max) NULL,
	[qenGender] [varchar](6) NULL,
 CONSTRAINT [PK_qendidateList] PRIMARY KEY CLUSTERED 
(
	[qenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[qendidateListInJob]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[qendidateListInJob](
	[qendidateListID] [bigint] IDENTITY(1,1) NOT NULL,
	[jobID] [bigint] NOT NULL,
	[qenID] [bigint] NOT NULL,
	[category] [int] NULL,
 CONSTRAINT [PK_qendidateListInJob] PRIMARY KEY CLUSTERED 
(
	[qendidateListID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[qendidatePGraduation]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[qendidatePGraduation](
	[pggradid] [bigint] IDENTITY(1,1) NOT NULL,
	[qenID] [bigint] NOT NULL,
	[collegeName] [nvarchar](500) NULL,
	[collegeUniversity] [nvarchar](200) NULL,
	[courseName] [nvarchar](500) NULL,
	[courseField] [nvarchar](30) NULL,
	[pGradPercentage] [float] NULL,
	[subjects] [nvarchar](500) NULL,
	[dataIsCreated] [datetime] NULL,
	[dataIsUpdated] [datetime] NULL,
	[YearPassing] [int] NULL,
 CONSTRAINT [PK_qendidatePGraduation] PRIMARY KEY CLUSTERED 
(
	[pggradid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[qendidatePHD]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[qendidatePHD](
	[phdid] [bigint] IDENTITY(1,1) NOT NULL,
	[qenID] [bigint] NOT NULL,
	[collegeName] [nvarchar](100) NULL,
	[collegeUniversity] [nvarchar](100) NULL,
	[phdTitle] [nvarchar](100) NULL,
	[courseField] [nvarchar](500) NULL,
	[phdRemarks] [varchar](200) NULL,
	[phdStart] [varchar](50) NULL,
	[phdEnd] [varchar](50) NULL,
	[dataIsCreated] [datetime] NULL,
	[dataIsUpdated] [datetime] NULL,
 CONSTRAINT [PK_qendidatePHD] PRIMARY KEY CLUSTERED 
(
	[phdid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[qendidateProfileSharedWith]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[qendidateProfileSharedWith](
	[profileSharedID] [bigint] IDENTITY(1,1) NOT NULL,
	[qenID] [bigint] NOT NULL,
	[sharedWithEmailAddress] [nvarchar](300) NULL,
	[sharedDateTime] [datetime] NULL,
	[datatIsCreated] [datetime] NULL,
	[dataIsUpdated] [datetime] NULL,
 CONSTRAINT [PK_qendidateProfileSharedWith] PRIMARY KEY CLUSTERED 
(
	[profileSharedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[qendidateSavedForReference]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[qendidateSavedForReference](
	[qendidateSavedID] [bigint] IDENTITY(1,1) NOT NULL,
	[qenID] [bigint] NOT NULL,
	[jobID] [bigint] NOT NULL,
	[savedDateTime] [datetime] NULL,
	[dateIsCreated] [datetime] NULL,
	[dataIsCreated] [datetime] NULL,
 CONSTRAINT [PK_qendidateSavedForReference] PRIMARY KEY CLUSTERED 
(
	[qendidateSavedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[qendidateTestSchedule]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[qendidateTestSchedule](
	[mailSendID] [bigint] IDENTITY(1,1) NOT NULL,
	[jobID] [bigint] NOT NULL,
	[qenID] [bigint] NOT NULL,
	[mailSentTestScheduled] [bit] NULL,
	[mailReceivedscheduled] [bit] NULL,
	[testScheduledDateTime] [datetime] NULL,
	[dataIsCreated] [datetime] NULL,
	[dataIsUpdated] [datetime] NULL,
	[ReasonReschedule] [varchar](100) NULL,
	[testScheduleCountInt] [int] NULL,
 CONSTRAINT [PK_qendidateTestSchedule] PRIMARY KEY CLUSTERED 
(
	[mailSendID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[qenEmpDetails]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[qenEmpDetails](
	[qenEmploymentNum] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](100) NULL,
	[qenEmpFrom] [varchar](50) NULL,
	[jobDescription] [nvarchar](100) NULL,
	[qenEmpTo] [varchar](50) NULL,
	[qenSalary] [float] NULL,
	[qenPosition] [varchar](50) NULL,
	[dataIsCreated] [datetime] NULL,
	[dataIsUpdated] [datetime] NULL,
	[qenID] [bigint] NOT NULL,
	[qenRoleInProject] [varchar](500) NULL,
	[teamSize] [int] NULL,
	[jobLocation] [varchar](100) NULL,
	[isPresent] [bit] NULL,
	[projectDescription] [nvarchar](300) NULL,
 CONSTRAINT [PK_qenEmpDetails_1] PRIMARY KEY CLUSTERED 
(
	[qenEmploymentNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[qenExamHistory]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[qenExamHistory](
	[examAttemptID] [bigint] IDENTITY(1,1) NOT NULL,
	[JobID] [bigint] NOT NULL,
	[qenID] [bigint] NOT NULL,
	[questionPaperID] [bigint] NULL,
	[questionPaperSentDateTime] [datetime] NULL,
	[examAttemptedDateTime] [datetime] NULL,
	[examSubmittedDateTime] [datetime] NULL,
	[examMarks] [int] NULL,
	[examReScheduled] [bit] NULL,
	[reasonReScheduled] [varchar](200) NULL,
	[dataIsCreated] [datetime] NULL,
	[dataIsUpdated] [datetime] NULL,
 CONSTRAINT [PK_qenExamHistory] PRIMARY KEY CLUSTERED 
(
	[examAttemptID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[qenHigherSecondary]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[qenHigherSecondary](
	[hsecid] [bigint] IDENTITY(1,1) NOT NULL,
	[qenID] [bigint] NOT NULL,
	[schoolName] [varchar](40) NULL,
	[hSecondaryPassYear] [int] NULL,
	[hSecondaryPercentage] [float] NULL,
	[hSecondaryBoard] [nvarchar](50) NULL,
	[hSecondarySubjects] [nvarchar](100) NULL,
	[dataIsCreated] [datetime] NULL,
	[dataIsUpdated] [datetime] NULL,
 CONSTRAINT [PK_qenHigherSecondary] PRIMARY KEY CLUSTERED 
(
	[hsecid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[qenInterviewSchedule]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[qenInterviewSchedule](
	[meetID] [bigint] IDENTITY(1,1) NOT NULL,
	[JobID] [bigint] NOT NULL,
	[qenID] [bigint] NOT NULL,
	[meetScheduledMailSent] [bit] NOT NULL,
	[meetMailSentDateTime] [datetime] NOT NULL,
	[meetMailRepliedDateTime] [datetime] NOT NULL,
	[meetScheduledDateTime] [datetime] NULL,
	[meetPreferredMedium] [varchar](50) NULL,
	[reScheduled] [bit] NULL,
	[reasonReScheduled] [varchar](5000) NULL,
	[dataIsCreated] [datetime] NULL,
	[dataIsUpdated] [datetime] NULL,
	[meetScheduledMailRecieved] [bit] NOT NULL,
 CONSTRAINT [PK_qenInterviewSchedule] PRIMARY KEY CLUSTERED 
(
	[meetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[qenMialSendInterested]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[qenMialSendInterested](
	[qenMialSendInterestedID] [bigint] IDENTITY(1,1) NOT NULL,
	[jobID] [bigint] NOT NULL,
	[qenID] [bigint] NOT NULL,
	[mailSentjobChancgeInterested] [bit] NOT NULL,
	[mailSentDateTime] [datetime] NOT NULL,
	[mailReceivedjobChancgeInterested] [bit] NOT NULL,
	[mailReceivedDatetime] [datetime] NOT NULL,
	[dataIsCreated] [datetime] NOT NULL,
	[dataIsUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_qenMialSendInterested] PRIMARY KEY CLUSTERED 
(
	[qenMialSendInterestedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[qenReference]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[qenReference](
	[qenID] [bigint] NOT NULL,
	[qenRefID] [int] IDENTITY(1,1) NOT NULL,
	[qenRefName] [nvarchar](50) NULL,
	[qenRefCompany] [nvarchar](50) NULL,
	[qenRefPhone] [float] NULL,
	[qenRefEmail] [nvarchar](320) NULL,
	[qenRefPosition] [nvarchar](40) NULL,
	[dataIsCreated] [datetime] NULL,
	[dataIsUpdated] [datetime] NULL,
 CONSTRAINT [PK_qenReference] PRIMARY KEY CLUSTERED 
(
	[qenID] ASC,
	[qenRefID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[qenResume]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[qenResume](
	[qenID] [bigint] NOT NULL,
	[qenResumeID] [int] IDENTITY(1,1) NOT NULL,
	[qenResumeName] [nvarchar](40) NULL,
	[qenResume] [nvarchar](256) NULL,
 CONSTRAINT [PK_qenResume] PRIMARY KEY CLUSTERED 
(
	[qenID] ASC,
	[qenResumeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[qenSecondary]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[qenSecondary](
	[qenSecondary] [bigint] IDENTITY(1,1) NOT NULL,
	[qenID] [bigint] NOT NULL,
	[schoolName] [varchar](40) NULL,
	[secondaryPassYear] [int] NULL,
	[secondaryPercentage] [float] NULL,
	[secondaryBoard] [nvarchar](20) NULL,
	[dataIsCreated] [datetime] NULL,
	[dataIsUpdated] [datetime] NULL,
	[secondarySubjects] [nvarchar](100) NULL,
 CONSTRAINT [PK_qenSecondary] PRIMARY KEY CLUSTERED 
(
	[qenSecondary] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[qenSkills]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[qenSkills](
	[qenSkillsID] [bigint] IDENTITY(1,1) NOT NULL,
	[qenID] [bigint] NOT NULL,
	[skillsID] [int] NOT NULL,
	[yearOfExp] [int] NOT NULL,
 CONSTRAINT [PK_qenSkills] PRIMARY KEY CLUSTERED 
(
	[qenSkillsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[role]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[role](
	[roleID] [int] NOT NULL,
	[role] [varchar](100) NOT NULL,
 CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED 
(
	[roleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[role_action]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role_action](
	[role_action_id] [int] IDENTITY(1,1) NOT NULL,
	[role_id] [int] NOT NULL,
	[controller_name] [nvarchar](max) NOT NULL,
	[action_name] [nvarchar](max) NOT NULL,
	[is_active] [bit] NOT NULL,
 CONSTRAINT [PK_role_action] PRIMARY KEY CLUSTERED 
(
	[role_action_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[salaryUnit]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[salaryUnit](
	[unitID] [int] IDENTITY(1,1) NOT NULL,
	[Unit] [varchar](100) NOT NULL,
 CONSTRAINT [PK_salaryUnit] PRIMARY KEY CLUSTERED 
(
	[unitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[skills]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[skills](
	[skillsID] [int] IDENTITY(1,1) NOT NULL,
	[skillName] [varchar](100) NULL,
 CONSTRAINT [PK_skills] PRIMARY KEY CLUSTERED 
(
	[skillsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[state]    Script Date: 8/21/2017 6:52:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[state](
	[stateID] [int] IDENTITY(1,1) NOT NULL,
	[stateName] [varchar](100) NOT NULL,
	[CountryID] [int] NOT NULL,
 CONSTRAINT [PK_state] PRIMARY KEY CLUSTERED 
(
	[stateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[App_status] ON 

INSERT [dbo].[App_status] ([App_status_id], [status]) VALUES (1, N'Submitted')
INSERT [dbo].[App_status] ([App_status_id], [status]) VALUES (2, N'In Progress')
INSERT [dbo].[App_status] ([App_status_id], [status]) VALUES (3, N'Selected')
INSERT [dbo].[App_status] ([App_status_id], [status]) VALUES (4, N'Not Selected')
SET IDENTITY_INSERT [dbo].[App_status] OFF
SET IDENTITY_INSERT [dbo].[city] ON 

INSERT [dbo].[city] ([cityID], [city], [zipCode], [latitude], [longitude], [stateID], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (0, N'Bhopal', NULL, NULL, NULL, 2, CAST(0x0000A7C30187AC5F AS DateTime), CAST(0x0000A7C30187AC81 AS DateTime), 1, 0)
INSERT [dbo].[city] ([cityID], [city], [zipCode], [latitude], [longitude], [stateID], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (1, N'ahmedabad', N'380001', N'84598465', N'479879465', 1, CAST(0x0000A7BA01208503 AS DateTime), CAST(0x0000A7BA01208503 AS DateTime), 1, 0)
INSERT [dbo].[city] ([cityID], [city], [zipCode], [latitude], [longitude], [stateID], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (2, N'ahm', NULL, NULL, NULL, 2, CAST(0x0000A7D400496C3D AS DateTime), CAST(0x0000A7D400496C3D AS DateTime), 1, 0)
INSERT [dbo].[city] ([cityID], [city], [zipCode], [latitude], [longitude], [stateID], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (3, N'lucknow', NULL, NULL, NULL, 2, CAST(0x0000A7D4004F3D6D AS DateTime), CAST(0x0000A7D4004F3D6D AS DateTime), 1, 0)
INSERT [dbo].[city] ([cityID], [city], [zipCode], [latitude], [longitude], [stateID], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (4, N'wjgiu', NULL, NULL, NULL, 2, CAST(0x0000A7D400552AD9 AS DateTime), CAST(0x0000A7D400552AD9 AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[city] OFF
SET IDENTITY_INSERT [dbo].[companyDetails] ON 

INSERT [dbo].[companyDetails] ([companyID], [companyName], [cityID], [address], [website], [companyIndustry], [companyDescription], [gstNo], [tinNo], [ctsNo], [logo], [dataIsCreated], [isActive], [isDeleted], [modifiedBy], [dataIsUpdated], [employerTypeID]) VALUES (1, N'qendidate', 1, N'prahlad nagar', N'aceinfoway.in', 1, N'information technology', N'6464', N'64646', N'6464', N'/Logo/EmptyLogo.jpg', CAST(0x0000A7BA01208503 AS DateTime), 1, 0, NULL, CAST(0x0000A7C20121F332 AS DateTime), 1)
INSERT [dbo].[companyDetails] ([companyID], [companyName], [cityID], [address], [website], [companyIndustry], [companyDescription], [gstNo], [tinNo], [ctsNo], [logo], [dataIsCreated], [isActive], [isDeleted], [modifiedBy], [dataIsUpdated], [employerTypeID]) VALUES (4, N'SAP', 1, N' ', N'sap.com', 1, N' ', NULL, NULL, NULL, N'/Logo/EmptyLogo.jpg', CAST(0x0000A7BA0136E3D4 AS DateTime), 1, 0, NULL, CAST(0x0000A7BA01208503 AS DateTime), 2)
INSERT [dbo].[companyDetails] ([companyID], [companyName], [cityID], [address], [website], [companyIndustry], [companyDescription], [gstNo], [tinNo], [ctsNo], [logo], [dataIsCreated], [isActive], [isDeleted], [modifiedBy], [dataIsUpdated], [employerTypeID]) VALUES (5, N'SAP', 1, N' ', N'sap.com', 1, N' ', NULL, NULL, NULL, N'/Logo/EmptyLogo.jpg', CAST(0x0000A7BA01395FFB AS DateTime), 1, 0, NULL, CAST(0x0000A7BA01208503 AS DateTime), 2)
INSERT [dbo].[companyDetails] ([companyID], [companyName], [cityID], [address], [website], [companyIndustry], [companyDescription], [gstNo], [tinNo], [ctsNo], [logo], [dataIsCreated], [isActive], [isDeleted], [modifiedBy], [dataIsUpdated], [employerTypeID]) VALUES (85, N'qendidate', 1, N' ', N'aceinfoway.in', 1, N' ', NULL, NULL, NULL, NULL, CAST(0x0000A7C500FDDC15 AS DateTime), 0, NULL, NULL, CAST(0x0000A7C500FDDC15 AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[companyDetails] OFF
SET IDENTITY_INSERT [dbo].[country] ON 

INSERT [dbo].[country] ([CountryID], [Country], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (1, N'India', CAST(0x0000A7BB00D93563 AS DateTime), CAST(0x0000A7BB00D93563 AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[country] OFF
INSERT [dbo].[currency] ([currencyID], [currency], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (1, N'$', CAST(0x0000A7BB00D9E9E8 AS DateTime), CAST(0x0000A7BB00D9E9E8 AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[Education] ON 

INSERT [dbo].[Education] ([EducationID], [educationName], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (1, N'High School', CAST(0x0000A7BC010073D5 AS DateTime), CAST(0x0000A7BC010073D5 AS DateTime), 1, 0)
INSERT [dbo].[Education] ([EducationID], [educationName], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (2, N'Higher School', CAST(0x0000A7BC010073D5 AS DateTime), CAST(0x0000A7BC010073D5 AS DateTime), 1, 0)
INSERT [dbo].[Education] ([EducationID], [educationName], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (3, N'Graduation', CAST(0x0000A7BC010073D5 AS DateTime), CAST(0x0000A7BC010073D5 AS DateTime), 1, 0)
INSERT [dbo].[Education] ([EducationID], [educationName], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (4, N'Post Graduation', CAST(0x0000A7BC010073D5 AS DateTime), CAST(0x0000A7BC010073D5 AS DateTime), 1, 0)
INSERT [dbo].[Education] ([EducationID], [educationName], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (5, N'PHD', CAST(0x0000A7BC010073D5 AS DateTime), CAST(0x0000A7BC010073D5 AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[Education] OFF
SET IDENTITY_INSERT [dbo].[EmployerDetails] ON 

INSERT [dbo].[EmployerDetails] ([EmployerID], [Name], [Mobile], [Email], [password], [dataIsCreated], [isActive], [isDelete], [companyID], [roleID], [dataIsUpdated]) VALUES (4, N'Ace info', N'9993012042', N'demoyogesh@gmail.com', N'0E1X4E3I0B', CAST(0x0000A7BA01208503 AS DateTime), 1, 0, 1, 2, CAST(0x0000A7C2013BF128 AS DateTime))
INSERT [dbo].[EmployerDetails] ([EmployerID], [Name], [Mobile], [Email], [password], [dataIsCreated], [isActive], [isDelete], [companyID], [roleID], [dataIsUpdated]) VALUES (7, N' yogesh', N'9993012042', N'sap1@sap.com', N'123456', CAST(0x0000A7BA0136E3D5 AS DateTime), 1, 0, 1, 1, CAST(0x0000A7BB00D98635 AS DateTime))
INSERT [dbo].[EmployerDetails] ([EmployerID], [Name], [Mobile], [Email], [password], [dataIsCreated], [isActive], [isDelete], [companyID], [roleID], [dataIsUpdated]) VALUES (8, N'Abhijeet', N'9993012042', N'sap2@sap.com', N'123456', CAST(0x0000A7BA01399F4C AS DateTime), 1, 0, 1, 3, CAST(0x0000A7BB00D98635 AS DateTime))
INSERT [dbo].[EmployerDetails] ([EmployerID], [Name], [Mobile], [Email], [password], [dataIsCreated], [isActive], [isDelete], [companyID], [roleID], [dataIsUpdated]) VALUES (9, N' sagar', N'9993012042', N'sap3@sap.com', N'123456', CAST(0x0000A7BA013A3A83 AS DateTime), 1, 0, 1, 5, CAST(0x0000A7BB00D98635 AS DateTime))
INSERT [dbo].[EmployerDetails] ([EmployerID], [Name], [Mobile], [Email], [password], [dataIsCreated], [isActive], [isDelete], [companyID], [roleID], [dataIsUpdated]) VALUES (82, N'Abhijeet', N'9721522086', N'abbyastro@gmail.com', N'6R7O5J7Q7K', CAST(0x0000A7C500FDDC4F AS DateTime), 1, 0, 85, 1, CAST(0x0000A7C500FFF3EA AS DateTime))
SET IDENTITY_INSERT [dbo].[EmployerDetails] OFF
INSERT [dbo].[employerType] ([employerTypeID], [employerType], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (1, N'Company', CAST(0x0000A7BB00D9E9E8 AS DateTime), CAST(0x0000A7BB00D9E9E8 AS DateTime), 1, 0)
INSERT [dbo].[employerType] ([employerTypeID], [employerType], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (2, N'Staffing', CAST(0x0000A7BB00D9E9E8 AS DateTime), CAST(0x0000A7BB00D9E9E8 AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[EmploymentType] ON 

INSERT [dbo].[EmploymentType] ([EmploymentTypeID], [EmploymentType], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (1, N'Full Time', CAST(0x0000A7BB00D9E9E8 AS DateTime), CAST(0x0000A7BB00D9E9E8 AS DateTime), 1, 0)
INSERT [dbo].[EmploymentType] ([EmploymentTypeID], [EmploymentType], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (2, N'Part Time', CAST(0x0000A7BB00D9E9E8 AS DateTime), CAST(0x0000A7BB00D9E9E8 AS DateTime), 1, 0)
INSERT [dbo].[EmploymentType] ([EmploymentTypeID], [EmploymentType], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (3, N'Contract', CAST(0x0000A7BB00D9E9E8 AS DateTime), CAST(0x0000A7BB00D9E9E8 AS DateTime), 1, 0)
INSERT [dbo].[EmploymentType] ([EmploymentTypeID], [EmploymentType], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (4, N'Internship', CAST(0x0000A7BB00D9E9E8 AS DateTime), CAST(0x0000A7BB00D9E9E8 AS DateTime), 1, 0)
INSERT [dbo].[EmploymentType] ([EmploymentTypeID], [EmploymentType], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (5, N'Volunteer', CAST(0x0000A7BB00D9E9E8 AS DateTime), CAST(0x0000A7BB00D9E9E8 AS DateTime), 1, 0)
INSERT [dbo].[EmploymentType] ([EmploymentTypeID], [EmploymentType], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (6, N'Fresher', CAST(0x0000A7BB00D9E9E8 AS DateTime), CAST(0x0000A7BB00D9E9E8 AS DateTime), 1, 0)
INSERT [dbo].[EmploymentType] ([EmploymentTypeID], [EmploymentType], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (7, N'Walk-In', CAST(0x0000A7BB00D9E9E8 AS DateTime), CAST(0x0000A7BB00D9E9E8 AS DateTime), 1, 0)
INSERT [dbo].[EmploymentType] ([EmploymentTypeID], [EmploymentType], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (8, N'Temporary', CAST(0x0000A7BB00D9E9E8 AS DateTime), CAST(0x0000A7BB00D9E9E8 AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[EmploymentType] OFF
SET IDENTITY_INSERT [dbo].[industry] ON 

INSERT [dbo].[industry] ([industryID], [industryName], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (1, N'Information Technology', CAST(0x0000A7BB00DB2E67 AS DateTime), CAST(0x0000A7BB00DB2E67 AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[industry] OFF
SET IDENTITY_INSERT [dbo].[jobDetails] ON 

INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (1, N'test deveepr', N'test deveepr', 2, 1, N'$', N'Per Day', 1, 1, 1, N'High School', 2, 4, CAST(0x0000A7BC0100725B AS DateTime), 4, CAST(0x0000A7BC010073D5 AS DateTime), 1, N'ace', N'9993012043', N'demoyogesh@gmail.com', 1, 0, N'ace', N'<p>information technology it</p>
<p>is my tezt</p>', N'aceinfoway.in', N'demoyogesh@gmail.com', NULL, 1, 1, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (2, N'test deveepr', N'test deveepr ', 2, 200, N'1', N'1', 1, 1, 2, NULL, 2, 4, CAST(0x0000A7BC0102D41B AS DateTime), 4, CAST(0x0000A7BC0102D474 AS DateTime), 1, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (4, N'test Engineer', N'<p>this job is just a test job</p>
<p>this job is just a test job</p>', 2, 10000, N'$', N'per month', 1, 1, 3, NULL, 3, 4, CAST(0x0000A7BF00E50A5E AS DateTime), 4, CAST(0x0000A7BF00E50A5E AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (5, N'test job created', N'<p>this job is just a test job</p>', 2, 2003, N'$', N'per day', 1, 1, 2, NULL, 6, 4, CAST(0x0000A7BF00E67D14 AS DateTime), 4, CAST(0x0000A7BF00E67DCB AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 1, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (6, N'test deve created', N'<p>test deve created</p>', 2, 20, N'$', N'per day', 1, 1, 2, NULL, 5, 4, CAST(0x0000A7BF00E79CE5 AS DateTime), 4, CAST(0x0000A7BF00E79D40 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 1, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (7, N'test deveepr test', N'<p>test &nbsp;test deveepr test</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 5, 4, CAST(0x0000A7BF00E91F6D AS DateTime), 4, CAST(0x0000A7BF00E91F6D AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 1, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (8, N'test deveepr test', N'<p>test &nbsp;test deveepr test</p>', 5, 200, N'$', N'per day', 1, 1, 1, NULL, 5, 4, CAST(0x0000A7BF00F0AD33 AS DateTime), 4, CAST(0x0000A7BF00F0AD33 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 1, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (9, N'test deveepr test', N'<p>test &nbsp;test deveepr test</p>', 5, 200, N'$', N'per day', 1, 1, 1, NULL, 5, 4, CAST(0x0000A7BF00F0C548 AS DateTime), 4, CAST(0x0000A7BF00F0C548 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 1, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (10, N'test deveepr test', N'<p>test &nbsp;test deveepr test</p>', 5, 200, N'$', N'per day', 1, 1, 1, NULL, 5, 4, CAST(0x0000A7BF00F23D28 AS DateTime), 4, CAST(0x0000A7BF00F23D28 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 1, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (11, N'test deveepr test', N'<p>test &nbsp;test deveepr test</p>', 5, 200, N'$', N'per day', 1, 1, 1, NULL, 5, 4, CAST(0x0000A7BF00F64DA9 AS DateTime), 4, CAST(0x0000A7BF00F64DA9 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 1, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (12, N'test deveepr', N'<p>test deveepr</p>', 2, 1, N'$', N'per day', 1, 1, 1, NULL, 2, 4, CAST(0x0000A7CD001BB118 AS DateTime), 4, CAST(0x0000A7CD001BB194 AS DateTime), 1, N'qendidate', N'9993012042', N'demoyogesh@gmail.com', 1, 0, N'qendidate', N'<p>information technology</p>', N'aceinfoway.in', N'demoyogesh@gmail.com', N'/Logo/EmptyLogo.jpg', 1, 1, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (13, N'test deveepr', N'<p>test deveepr</p>', 2, 1, N'$', N'per day', 1, 1, 1, NULL, 2, 4, CAST(0x0000A7C100D5F0FA AS DateTime), 4, CAST(0x0000A7C100D5F0FA AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 1, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (14, N'test deveepr', N'<p>test deveepr</p>', 2, 1, N'$', N'per day', 1, 1, 1, NULL, 2, 4, CAST(0x0000A7C200E16219 AS DateTime), 4, CAST(0x0000A7C200E16219 AS DateTime), 3, N'ace infoway post', N'9993012048', N'demoyogesh@gmail.com', 1, 0, N'ace infoway post', N'<p>information technology</p>', N'aceinfoway.in', N'demoyogesh@gmail.com', N'/Logo/EmptyLogo.jpg', 0, 1, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (15, N'test abhijit', N'<p>test abhijit&nbsp;test abhijit&nbsp;test abhijit</p>', 3, 400, N'$', N'per month', 1, 1, 6, NULL, 2, 4, CAST(0x0000A7C20101AC1F AS DateTime), 4, CAST(0x0000A7C20101AC1F AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 1, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (16, N'abhijit test job', N'<p>abhijit test job&nbsp;abhijit test jobv</p>', 3, 400, N'$', N'per month', 1, 1, 5, NULL, 2, 4, CAST(0x0000A7C20104730E AS DateTime), 4, CAST(0x0000A7C20104746F AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (17, N'yogesh test job', N'<p>yogesh test job</p>', 0, 6, N'$', N'per day', 1, 1, 1, NULL, 2, 4, CAST(0x0000A7C2010E6631 AS DateTime), 4, CAST(0x0000A7C2010E66BA AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (18, N'yogesh test job', N'<p>yogesh test jobyogesh test job</p>', 0, 6, N'$', N'per day', 1, 1, 4, NULL, 6, 4, CAST(0x0000A7C2010F04F9 AS DateTime), 4, CAST(0x0000A7C2010F05E2 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (19, N'test deveepr test', N'<p>test deveepr test&nbsp;test deveepr testtest deveepr test</p>', 0, 10, N'$', N'per day', 1, 1, 1, NULL, 6, 4, CAST(0x0000A7C201101084 AS DateTime), 4, CAST(0x0000A7C201101120 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (20, N'test deveepr test', N'<p>test deveepr test&nbsp;test deveepr testtest deveepr test</p>', 0, 10, N'$', N'per day', 1, 1, 1, NULL, 6, 4, CAST(0x0000A7C20110B77A AS DateTime), 4, CAST(0x0000A7C20110B807 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (21, N'test deveepr test test deveepr test', N'<p>&nbsp;test deveepr test test deveepr test&nbsp;test deveepr test test deveepr test</p>', 0, 8, N'$', N'per day', 1, 1, 1, NULL, 0, 4, CAST(0x0000A7C201128512 AS DateTime), 4, CAST(0x0000A7C201128512 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (22, N'test deveepr test test deveepr testtest deveepr test', N'<p>&nbsp;test deveepr test test deveepr testtest deveepr test&nbsp;test deveepr test test deveepr testtest deveepr test</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 8, 4, CAST(0x0000A7C2011536FD AS DateTime), 4, CAST(0x0000A7C2011536FD AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (23, N'test deveepr test test deveepr testtest deveepr test', N'<p>test deveepr test test deveepr testtest deveepr test&nbsp;</p>
<p>test deveepr test test deveepr testtest deveepr test</p>', 0, 6, N'$', N'per day', 1, 1, 1, NULL, 0, 4, CAST(0x0000A7C20116DBC4 AS DateTime), 4, CAST(0x0000A7C20116DBC4 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (24, N'test deveepr test test deveepr testtest deveepr test', N'<p>test deveepr test test deveepr testtest deveepr test</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 0, 4, CAST(0x0000A7C20117C7A8 AS DateTime), 4, CAST(0x0000A7C20117C7A8 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (25, N'test deveepr test test deveepr testtest deveepr test', N'<p>test deveepr test test deveepr testtest deveepr test</p>', 0, 6, N'$', N'per day', 1, 1, 1, NULL, 6, 4, CAST(0x0000A7C2011A7D67 AS DateTime), 4, CAST(0x0000A7C2011A7D67 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (26, N'test deveepr test test deveepr testtest deveepr test', N'<p>test deveepr test test deveepr testtest deveepr test</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 55, 4, CAST(0x0000A7C201209E42 AS DateTime), 4, CAST(0x0000A7C201209E42 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (27, N'test deveepr test test deveepr testtest deveepr test', N'<p>test deveepr test test deveepr testtest deveepr test</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 55, 4, CAST(0x0000A7C20120DC8D AS DateTime), 4, CAST(0x0000A7C20120DC8D AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (28, N'test deveepr test test deveepr testtest deveepr test', N'<p>test deveepr test test deveepr testtest deveepr test</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 55, 4, CAST(0x0000A7C20120FC18 AS DateTime), 4, CAST(0x0000A7C20120FC18 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (29, N'test deveepr test test deveepr testtest deveepr test', N'<p>test deveepr test test deveepr testtest deveepr test</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 55, 4, CAST(0x0000A7C201214587 AS DateTime), 4, CAST(0x0000A7C201214587 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (30, N'test deveepr test test deveepr testtest deveepr test', N'<p>test deveepr test test deveepr testtest deveepr test</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 0, 4, CAST(0x0000A7C20122E253 AS DateTime), 4, CAST(0x0000A7C20122E253 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (31, N'test deveepr test test deveepr testtest deveepr test', N'<p>test deveepr test test deveepr testtest deveepr test</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 5, 4, CAST(0x0000A7C201232E70 AS DateTime), 4, CAST(0x0000A7C201232E70 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (32, N'test deveepr test test deveepr testtest deveepr test', N'<p>test deveepr test test deveepr testtest deveepr test</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 2, 4, CAST(0x0000A7C2012393E6 AS DateTime), 4, CAST(0x0000A7C2012393E6 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (33, N'test deveepr test test deveepr testtest deveepr test', N'<p>test deveepr test test deveepr testtest deveepr test</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 0, 4, CAST(0x0000A7C20125F714 AS DateTime), 4, CAST(0x0000A7C20125F714 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (34, N'test deveepr test test deveepr testtest deveepr test', N'<p>test deveepr test test deveepr testtest deveepr test</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 5, 4, CAST(0x0000A7C2012AEA65 AS DateTime), 4, CAST(0x0000A7C2012AEA65 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (35, N'test deveepr test test deveepr testtest deveepr test', N'<p>test deveepr test test deveepr testtest deveepr test</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 1, 4, CAST(0x0000A7C2012C247D AS DateTime), 4, CAST(0x0000A7C2012C247D AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (36, N'test deveepr test test deveepr testtest deveepr test', N'<p>test deveepr test test deveepr testtest deveepr test</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 6, 4, CAST(0x0000A7C2012D0DF8 AS DateTime), 4, CAST(0x0000A7C2012D0DF8 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (37, N'test deveepr test test deveepr testtest deveepr test', N'<p>&nbsp;#region Session</p>
<p>&nbsp; &nbsp; &nbsp; &nbsp; public static void SetSessionValue(String Key, String Value)</p>
<p>&nbsp; &nbsp; &nbsp; &nbsp; {</p>
<p>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;HttpContext.Current.Session[Key] = Value;</p>
<p>&nbsp; &nbsp; &nbsp; &nbsp; }</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 5, 4, CAST(0x0000A7C201307282 AS DateTime), 4, CAST(0x0000A7C201307282 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (38, N'test deveepr test test deveepr testtest deveepr test', N'<p>test deveepr test test deveepr testtest deveepr test</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 5, 4, CAST(0x0000A7C20134E4D8 AS DateTime), 4, CAST(0x0000A7C20134E4D8 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (39, N'test deveepr test test deveepr testtest deveepr test', N'<p>test deveepr test test deveepr testtest deveepr test</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 5, 4, CAST(0x0000A7C201395F6E AS DateTime), 4, CAST(0x0000A7C201395F6E AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (40, N'test deveepr test test deveepr testtest deveepr test', N'<p>test deveepr test test deveepr testtest deveepr test</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 5, 4, CAST(0x0000A7C20139BC50 AS DateTime), 4, CAST(0x0000A7C20139BC50 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (41, N'test deveepr test test deveepr testtest deveepr test', N'<p>test deveepr test test deveepr testtest deveepr test</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 5, 4, CAST(0x0000A7C20139DCE8 AS DateTime), 4, CAST(0x0000A7C20139DCE8 AS DateTime), 3, NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (42, N'test deveepr test test deveepr testtest deveepr test', N'<p>test deveepr test test deveepr testtest deveepr test</p>', 0, 0, N'$', N'per day', 1, 1, 1, NULL, 2, 4, CAST(0x0000A7C2013BEE7C AS DateTime), 4, CAST(0x0000A7C2013BEE7C AS DateTime), 3, N'qendidate1', N'9993012048', N'demoyogesh@gmail.com', 1, 0, N'qendidate1', N'<p>information technology</p>', N'aceinfoway.in', N'demoyogesh@gmail.com', N'/Logo/EmptyLogo.jpg', 0, 0, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (43, N'laptop testing & maintinance', N'<p>laptop testing &amp; maintinance laptop testing &amp; maintinance</p>', 1, 1000, N'$', N'per month', 1, 0, 2, NULL, 2, 4, CAST(0x0000A7C300BA245D AS DateTime), 4, CAST(0x0000A7C300BA24D2 AS DateTime), 1, N'qendidate 2', N'9993012042', N'demoyogesh@gmail.com', 1, 0, N'qendidate 2', N'<p>information technology</p>', N'aceinfoway.in', N'demoyogesh@gmail.com', N'/Logo/EmptyLogo.jpg', 1, 1, N'<p>all the ting are good and working fine</p>', 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (44, N'laptop testing & maintinance', N'<p>titletitletitletitletitletitletitletitletitletitletitletitletitletitletitletitletitletitletitletitle</p>', 8, 1, N'$', N'per day', 1, 1, 1, NULL, 1, 4, CAST(0x0000A7C3014D8CD1 AS DateTime), 4, CAST(0x0000A7C3014D8CD1 AS DateTime), 1, N'qendidate', N'9993012042', N'demoyogesh@gmail.com', 1, 0, N'qendidate', N'<p>information technology</p>', N'aceinfoway.in', N'demoyogesh@gmail.com', N'/Logo/EmptyLogo.jpg', 1, 1, NULL, 1, N'male')
INSERT [dbo].[jobDetails] ([jobID], [jobTitle], [jobDescription], [workExpMin], [salary], [currency], [unit], [industryID], [cityID], [EmploymentTypeID], [EducationReq], [NoOfOpenings], [createdBy], [dataIsCreated], [modifiedBy], [dataIsUpdated], [jobStatusID], [jobContactPersonName], [jobContactPersonMobile], [jobContactPersonEmail], [isActive], [isDelete], [CompanyName], [CompanyDescription], [CompanyWebsite], [responsesEmailID], [CompanyLogo], [compeletPosted], [salaryVisibleToEmployee], [otherinformation], [companyID], [gender]) VALUES (45, N'test after session', N'<p>test after session&nbsp;test after session&nbsp;test after session</p>', 0, 0, N'$', N'per week', 1, 1, 1, NULL, 2, 4, CAST(0x0000A7CD001AABAA AS DateTime), 4, CAST(0x0000A7CD001AABAA AS DateTime), 3, NULL, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 1, N'male')
SET IDENTITY_INSERT [dbo].[jobDetails] OFF
SET IDENTITY_INSERT [dbo].[jobSkills] ON 

INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (2, 1, 3)
INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (3, 1, 2)
INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (4, 1, 12)
INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (5, 9, 3)
INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (6, 9, 2)
INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (7, 9, 3)
INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (8, 9, 2)
INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (9, 9, 3)
INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (10, 9, 2)
INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (42, 14, 3)
INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (45, 42, 3)
INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (46, 43, 13)
INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (49, 44, 3)
INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (50, 44, 2)
INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (51, 45, 3)
INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (52, 45, 2)
INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (53, 45, 5)
INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (54, 12, 3)
INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (55, 12, 2)
INSERT [dbo].[jobSkills] ([jobSkillsID], [jobID], [skillsID]) VALUES (56, 12, 11)
SET IDENTITY_INSERT [dbo].[jobSkills] OFF
SET IDENTITY_INSERT [dbo].[jobStatus] ON 

INSERT [dbo].[jobStatus] ([jobStatusID], [statuts], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (1, N'Open', CAST(0x0000A7BB00D9E9E8 AS DateTime), CAST(0x0000A7BC00F9E29E AS DateTime), 1, 0)
INSERT [dbo].[jobStatus] ([jobStatusID], [statuts], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (2, N'Hold', CAST(0x0000A7BB00D9E9E8 AS DateTime), CAST(0x0000A7BB00D9E9E8 AS DateTime), 1, 0)
INSERT [dbo].[jobStatus] ([jobStatusID], [statuts], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete]) VALUES (3, N'Closed', CAST(0x0000A7BB00D9E9E8 AS DateTime), CAST(0x0000A7BB00D9E9E8 AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[jobStatus] OFF
SET IDENTITY_INSERT [dbo].[qenAppliedJob] ON 

INSERT [dbo].[qenAppliedJob] ([qenID], [jobID], [appliedDate], [AppliedJobID], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete], [App_status_id]) VALUES (1, 45, CAST(0x0000A7CC0006C2C6 AS DateTime), 1, CAST(0x0000A7CC0006C2C6 AS DateTime), CAST(0x0000A7CC0006C2C6 AS DateTime), 1, 0, 2)
INSERT [dbo].[qenAppliedJob] ([qenID], [jobID], [appliedDate], [AppliedJobID], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete], [App_status_id]) VALUES (7, 45, CAST(0x0000A7CC0006C2C6 AS DateTime), 3, CAST(0x0000A7CC0006C2C6 AS DateTime), CAST(0x0000A7CC0006C2C6 AS DateTime), 1, 0, 2)
INSERT [dbo].[qenAppliedJob] ([qenID], [jobID], [appliedDate], [AppliedJobID], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete], [App_status_id]) VALUES (7, 12, CAST(0x0000A7CC0006C2C6 AS DateTime), 4, CAST(0x0000A7CC0006C2C6 AS DateTime), CAST(0x0000A7CC0006C2C6 AS DateTime), 1, 0, 2)
INSERT [dbo].[qenAppliedJob] ([qenID], [jobID], [appliedDate], [AppliedJobID], [dataIsCreated], [dataIsUpdated], [isActive], [isDelete], [App_status_id]) VALUES (8, 45, CAST(0x0000A7CD001E7A00 AS DateTime), 5, CAST(0x0000A7CD001E7A00 AS DateTime), CAST(0x0000A7CD001E7A01 AS DateTime), 1, 0, 2)
SET IDENTITY_INSERT [dbo].[qenAppliedJob] OFF
SET IDENTITY_INSERT [dbo].[qendidateGraduation] ON 

INSERT [dbo].[qendidateGraduation] ([gradid], [qenID], [collegeName], [collegeUniversity], [courseName], [courseField], [gradPercentage], [subjects], [dataIsCreated], [dataIsUpdated], [YearPassing]) VALUES (1, 1, N'sss amarpatan', N'sss amarpatan', N'sss amarpatan', N'sss amarpatan', 60, N'sss amarpatan', CAST(0x0000A7B600C7C653 AS DateTime), CAST(0x0000A7CD000DEFD7 AS DateTime), NULL)
INSERT [dbo].[qendidateGraduation] ([gradid], [qenID], [collegeName], [collegeUniversity], [courseName], [courseField], [gradPercentage], [subjects], [dataIsCreated], [dataIsUpdated], [YearPassing]) VALUES (2, 8, N'CMS', N'CMS', N'CMS', N'CMS', 78, N'CMS', CAST(0x0000A7CA001965C0 AS DateTime), CAST(0x0000A7CD0020ECCE AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[qendidateGraduation] OFF
SET IDENTITY_INSERT [dbo].[qendidateList] ON 

INSERT [dbo].[qendidateList] ([qenID], [qenName], [qenAddress], [qenCityID], [qenEmail], [qenLinkdInUrl], [qenPhone], [qenNationality], [qenPassport], [dataIsCreated], [dataIsUpdated], [roleID], [isDelete], [isActive], [password], [qenResume], [qenCoverLetter], [IsReferenceCleared], [qenImage], [qenGender]) VALUES (1, N'demoyogesh', N'demoyogesh', 1, N'demoyogesh@gmail.com', N'https://stackoverflow.com/questions/19801125/the-t', 9993012042, N'indian', NULL, NULL, CAST(0x0000A7CD000DE8ED AS DateTime), 5, 0, 1, N'123456', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[qendidateList] ([qenID], [qenName], [qenAddress], [qenCityID], [qenEmail], [qenLinkdInUrl], [qenPhone], [qenNationality], [qenPassport], [dataIsCreated], [dataIsUpdated], [roleID], [isDelete], [isActive], [password], [qenResume], [qenCoverLetter], [IsReferenceCleared], [qenImage], [qenGender]) VALUES (7, N'abhijeet Pandey ', N'some address', 1, N'democreator666@gmail.com', NULL, 9721522086, NULL, NULL, NULL, NULL, 5, 0, 1, N'7E1P4T8@4P', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[qendidateList] ([qenID], [qenName], [qenAddress], [qenCityID], [qenEmail], [qenLinkdInUrl], [qenPhone], [qenNationality], [qenPassport], [dataIsCreated], [dataIsUpdated], [roleID], [isDelete], [isActive], [password], [qenResume], [qenCoverLetter], [IsReferenceCleared], [qenImage], [qenGender]) VALUES (8, N'Abhijeet Pandey', N'24 A samar vihar ', 1, N'abhijeet_p@hotmail.com', NULL, 9721522086, N'indian', N'm6997687', NULL, CAST(0x0000A7CD00207372 AS DateTime), 5, 0, 1, N'123456', NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[qendidateList] OFF
SET IDENTITY_INSERT [dbo].[qendidatePGraduation] ON 

INSERT [dbo].[qendidatePGraduation] ([pggradid], [qenID], [collegeName], [collegeUniversity], [courseName], [courseField], [pGradPercentage], [subjects], [dataIsCreated], [dataIsUpdated], [YearPassing]) VALUES (1, 1, N'sss amarpatan', N'sss amarpatan', N'sss amarpatan', N'sss amarpatan', 80, N'sss amarpatan', CAST(0x0000A7B600C7C6E0 AS DateTime), CAST(0x0000A7CD000DF04C AS DateTime), 2008)
INSERT [dbo].[qendidatePGraduation] ([pggradid], [qenID], [collegeName], [collegeUniversity], [courseName], [courseField], [pGradPercentage], [subjects], [dataIsCreated], [dataIsUpdated], [YearPassing]) VALUES (2, 8, N'CMS', N'CMS', N'CMS', N'CMS', 78, N'CMS', CAST(0x0000A7CA001965EB AS DateTime), CAST(0x0000A7CD0020ECFF AS DateTime), 2000)
SET IDENTITY_INSERT [dbo].[qendidatePGraduation] OFF
SET IDENTITY_INSERT [dbo].[qendidateTestSchedule] ON 

INSERT [dbo].[qendidateTestSchedule] ([mailSendID], [jobID], [qenID], [mailSentTestScheduled], [mailReceivedscheduled], [testScheduledDateTime], [dataIsCreated], [dataIsUpdated], [ReasonReschedule], [testScheduleCountInt]) VALUES (3, 45, 7, 1, 1, CAST(0x0000A7CD000DE8ED AS DateTime), CAST(0x0000A7D601212523 AS DateTime), CAST(0x0000A7D601212523 AS DateTime), N'2017-08-12 00:50:38.657', 1)
INSERT [dbo].[qendidateTestSchedule] ([mailSendID], [jobID], [qenID], [mailSentTestScheduled], [mailReceivedscheduled], [testScheduledDateTime], [dataIsCreated], [dataIsUpdated], [ReasonReschedule], [testScheduleCountInt]) VALUES (4, 45, 7, 1, 1, CAST(0x0000A7CD000DE8ED AS DateTime), CAST(0x0000A7D60123C3D8 AS DateTime), CAST(0x0000A7D60123C3D8 AS DateTime), N'2017-08-12 00:50:38.657', 1)
INSERT [dbo].[qendidateTestSchedule] ([mailSendID], [jobID], [qenID], [mailSentTestScheduled], [mailReceivedscheduled], [testScheduledDateTime], [dataIsCreated], [dataIsUpdated], [ReasonReschedule], [testScheduleCountInt]) VALUES (5, 45, 8, 1, 1, CAST(0x0000A7CD000DE8ED AS DateTime), CAST(0x0000A7D601212523 AS DateTime), CAST(0x0000A7D601212523 AS DateTime), N'2017-08-12 00:50:38.657', 1)
INSERT [dbo].[qendidateTestSchedule] ([mailSendID], [jobID], [qenID], [mailSentTestScheduled], [mailReceivedscheduled], [testScheduledDateTime], [dataIsCreated], [dataIsUpdated], [ReasonReschedule], [testScheduleCountInt]) VALUES (6, 45, 8, 1, 1, CAST(0x0000A7CD000DE8ED AS DateTime), CAST(0x0000A7D60123C3D8 AS DateTime), CAST(0x0000A7D60123C3D8 AS DateTime), N'2017-08-12 00:50:38.657', 1)
SET IDENTITY_INSERT [dbo].[qendidateTestSchedule] OFF
SET IDENTITY_INSERT [dbo].[qenHigherSecondary] ON 

INSERT [dbo].[qenHigherSecondary] ([hsecid], [qenID], [schoolName], [hSecondaryPassYear], [hSecondaryPercentage], [hSecondaryBoard], [hSecondarySubjects], [dataIsCreated], [dataIsUpdated]) VALUES (1, 1, N'sss amarpatan', 2005, 50, N'sss amarpatan', N'pcm', CAST(0x0000A7B600C7C71F AS DateTime), NULL)
INSERT [dbo].[qenHigherSecondary] ([hsecid], [qenID], [schoolName], [hSecondaryPassYear], [hSecondaryPercentage], [hSecondaryBoard], [hSecondarySubjects], [dataIsCreated], [dataIsUpdated]) VALUES (2, 8, N'CMS', 2000, 78, N'CMS', N'CMS', CAST(0x0000A7CA001965FD AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[qenHigherSecondary] OFF
SET IDENTITY_INSERT [dbo].[qenMialSendInterested] ON 

INSERT [dbo].[qenMialSendInterested] ([qenMialSendInterestedID], [jobID], [qenID], [mailSentjobChancgeInterested], [mailSentDateTime], [mailReceivedjobChancgeInterested], [mailReceivedDatetime], [dataIsCreated], [dataIsUpdated]) VALUES (1, 44, 7, 1, CAST(0x0000A7CD000DE8ED AS DateTime), 1, CAST(0x0000A7CD000DE8ED AS DateTime), CAST(0x0000A7CD000DE8ED AS DateTime), CAST(0x0000A7CD000DE8ED AS DateTime))
SET IDENTITY_INSERT [dbo].[qenMialSendInterested] OFF
SET IDENTITY_INSERT [dbo].[qenReference] ON 

INSERT [dbo].[qenReference] ([qenID], [qenRefID], [qenRefName], [qenRefCompany], [qenRefPhone], [qenRefEmail], [qenRefPosition], [dataIsCreated], [dataIsUpdated]) VALUES (1, 1, N'abhijeet243', N'bvuvo ', 9996012042, N'y@gmail.com', N'cdeveloper', CAST(0x0000A7B600D55AE8 AS DateTime), CAST(0x0000A7CD000EA074 AS DateTime))
INSERT [dbo].[qenReference] ([qenID], [qenRefID], [qenRefName], [qenRefCompany], [qenRefPhone], [qenRefEmail], [qenRefPosition], [dataIsCreated], [dataIsUpdated]) VALUES (1, 2, N'abhi243443', N'test', 9993012042, NULL, N'dev', CAST(0x0000A7B600D55CE9 AS DateTime), CAST(0x0000A7CD000EA088 AS DateTime))
INSERT [dbo].[qenReference] ([qenID], [qenRefID], [qenRefName], [qenRefCompany], [qenRefPhone], [qenRefEmail], [qenRefPosition], [dataIsCreated], [dataIsUpdated]) VALUES (8, 3, N'MR Raja', N'SAP ', 9721522086, N'abbyastro@gmail.com', N'OK POsition', CAST(0x0000A7CA0019C312 AS DateTime), NULL)
INSERT [dbo].[qenReference] ([qenID], [qenRefID], [qenRefName], [qenRefCompany], [qenRefPhone], [qenRefEmail], [qenRefPosition], [dataIsCreated], [dataIsUpdated]) VALUES (8, 4, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A7CA0019C312 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[qenReference] OFF
SET IDENTITY_INSERT [dbo].[qenSecondary] ON 

INSERT [dbo].[qenSecondary] ([qenSecondary], [qenID], [schoolName], [secondaryPassYear], [secondaryPercentage], [secondaryBoard], [dataIsCreated], [dataIsUpdated], [secondarySubjects]) VALUES (1, 1, N'sss amarpatan', 2005, 30, N'sss amarpatan', CAST(0x0000A7B600C7C756 AS DateTime), NULL, N'sss amarpatan')
INSERT [dbo].[qenSecondary] ([qenSecondary], [qenID], [schoolName], [secondaryPassYear], [secondaryPercentage], [secondaryBoard], [dataIsCreated], [dataIsUpdated], [secondarySubjects]) VALUES (2, 8, N'CMS', 2000, 78, N'CMS', CAST(0x0000A7CA00196611 AS DateTime), NULL, N'CMS')
SET IDENTITY_INSERT [dbo].[qenSecondary] OFF
SET IDENTITY_INSERT [dbo].[qenSkills] ON 

INSERT [dbo].[qenSkills] ([qenSkillsID], [qenID], [skillsID], [yearOfExp]) VALUES (2, 1, 11, 2)
INSERT [dbo].[qenSkills] ([qenSkillsID], [qenID], [skillsID], [yearOfExp]) VALUES (6, 1, 3, 0)
SET IDENTITY_INSERT [dbo].[qenSkills] OFF
INSERT [dbo].[role] ([roleID], [role]) VALUES (1, N'Admin')
INSERT [dbo].[role] ([roleID], [role]) VALUES (2, N'Employer')
INSERT [dbo].[role] ([roleID], [role]) VALUES (3, N'Employee')
INSERT [dbo].[role] ([roleID], [role]) VALUES (4, N'Data Operator')
INSERT [dbo].[role] ([roleID], [role]) VALUES (5, N'Candidate')
SET IDENTITY_INSERT [dbo].[salaryUnit] ON 

INSERT [dbo].[salaryUnit] ([unitID], [Unit]) VALUES (1, N'per day')
INSERT [dbo].[salaryUnit] ([unitID], [Unit]) VALUES (2, N'per week')
INSERT [dbo].[salaryUnit] ([unitID], [Unit]) VALUES (3, N'per month')
INSERT [dbo].[salaryUnit] ([unitID], [Unit]) VALUES (4, N'per year')
INSERT [dbo].[salaryUnit] ([unitID], [Unit]) VALUES (5, N'per hour')
SET IDENTITY_INSERT [dbo].[salaryUnit] OFF
SET IDENTITY_INSERT [dbo].[skills] ON 

INSERT [dbo].[skills] ([skillsID], [skillName]) VALUES (1, N'HTML')
INSERT [dbo].[skills] ([skillsID], [skillName]) VALUES (2, N'CSS')
INSERT [dbo].[skills] ([skillsID], [skillName]) VALUES (3, N'ASP.NET')
INSERT [dbo].[skills] ([skillsID], [skillName]) VALUES (4, N'.NET MVC')
INSERT [dbo].[skills] ([skillsID], [skillName]) VALUES (5, N'JQUERY')
INSERT [dbo].[skills] ([skillsID], [skillName]) VALUES (11, N'ajax')
INSERT [dbo].[skills] ([skillsID], [skillName]) VALUES (12, N'javaScript')
INSERT [dbo].[skills] ([skillsID], [skillName]) VALUES (13, N'maintanance & repairing')
INSERT [dbo].[skills] ([skillsID], [skillName]) VALUES (14, N'')
INSERT [dbo].[skills] ([skillsID], [skillName]) VALUES (15, N'asp')
INSERT [dbo].[skills] ([skillsID], [skillName]) VALUES (16, N'.net')
INSERT [dbo].[skills] ([skillsID], [skillName]) VALUES (17, N'siburgh')
SET IDENTITY_INSERT [dbo].[skills] OFF
SET IDENTITY_INSERT [dbo].[state] ON 

INSERT [dbo].[state] ([stateID], [stateName], [CountryID]) VALUES (1, N'Gujarat', 1)
INSERT [dbo].[state] ([stateID], [stateName], [CountryID]) VALUES (2, N'Unknown', 1)
SET IDENTITY_INSERT [dbo].[state] OFF
ALTER TABLE [dbo].[city] ADD  CONSTRAINT [DF_city_dataIsCreated]  DEFAULT (getdate()) FOR [dataIsCreated]
GO
ALTER TABLE [dbo].[city] ADD  CONSTRAINT [DF_city_dataIsUpdated]  DEFAULT (getdate()) FOR [dataIsUpdated]
GO
ALTER TABLE [dbo].[city] ADD  CONSTRAINT [DF_city_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[city] ADD  CONSTRAINT [DF_city_isDelete]  DEFAULT ((0)) FOR [isDelete]
GO
ALTER TABLE [dbo].[country] ADD  CONSTRAINT [DF_country_dataIsCreated]  DEFAULT (getdate()) FOR [dataIsCreated]
GO
ALTER TABLE [dbo].[country] ADD  CONSTRAINT [DF_country_dataIsUpdated]  DEFAULT (getdate()) FOR [dataIsUpdated]
GO
ALTER TABLE [dbo].[country] ADD  CONSTRAINT [DF_country_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[country] ADD  CONSTRAINT [DF_country_isDelete]  DEFAULT ((0)) FOR [isDelete]
GO
ALTER TABLE [dbo].[currency] ADD  CONSTRAINT [DF_currency_dataIsCreated]  DEFAULT (getdate()) FOR [dataIsCreated]
GO
ALTER TABLE [dbo].[currency] ADD  CONSTRAINT [DF_currency_dataIsUpdated]  DEFAULT (getdate()) FOR [dataIsUpdated]
GO
ALTER TABLE [dbo].[currency] ADD  CONSTRAINT [DF_currency_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[currency] ADD  CONSTRAINT [DF_currency_isDelete]  DEFAULT ((0)) FOR [isDelete]
GO
ALTER TABLE [dbo].[Education] ADD  CONSTRAINT [DF_Education_dataIsCreated]  DEFAULT (getdate()) FOR [dataIsCreated]
GO
ALTER TABLE [dbo].[Education] ADD  CONSTRAINT [DF_Education_dataIsUpdated]  DEFAULT (getdate()) FOR [dataIsUpdated]
GO
ALTER TABLE [dbo].[Education] ADD  CONSTRAINT [DF_Education_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[Education] ADD  CONSTRAINT [DF_Education_isDelete]  DEFAULT ((0)) FOR [isDelete]
GO
ALTER TABLE [dbo].[EmployerDetails] ADD  CONSTRAINT [DF_EmployerDetails_isActive]  DEFAULT ((0)) FOR [isActive]
GO
ALTER TABLE [dbo].[EmployerDetails] ADD  CONSTRAINT [DF_EmployerDetails_isDelete]  DEFAULT ((0)) FOR [isDelete]
GO
ALTER TABLE [dbo].[employerType] ADD  CONSTRAINT [DF_employerType_dataIsCreated]  DEFAULT (getdate()) FOR [dataIsCreated]
GO
ALTER TABLE [dbo].[employerType] ADD  CONSTRAINT [DF_employerType_dataIsUpdated]  DEFAULT (getdate()) FOR [dataIsUpdated]
GO
ALTER TABLE [dbo].[employerType] ADD  CONSTRAINT [DF_employerType_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[employerType] ADD  CONSTRAINT [DF_employerType_isDelete]  DEFAULT ((0)) FOR [isDelete]
GO
ALTER TABLE [dbo].[EmploymentType] ADD  CONSTRAINT [DF_EmploymentType_dataIsCreated]  DEFAULT (getdate()) FOR [dataIsCreated]
GO
ALTER TABLE [dbo].[EmploymentType] ADD  CONSTRAINT [DF_EmploymentType_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[EmploymentType] ADD  CONSTRAINT [DF_EmploymentType_isDelete]  DEFAULT ((0)) FOR [isDelete]
GO
ALTER TABLE [dbo].[industry] ADD  CONSTRAINT [DF_industry_dataIsCreated]  DEFAULT (getdate()) FOR [dataIsCreated]
GO
ALTER TABLE [dbo].[industry] ADD  CONSTRAINT [DF_industry_dataIsUpdated]  DEFAULT (getdate()) FOR [dataIsUpdated]
GO
ALTER TABLE [dbo].[industry] ADD  CONSTRAINT [DF_industry_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[industry] ADD  CONSTRAINT [DF_industry_isDelete]  DEFAULT ((0)) FOR [isDelete]
GO
ALTER TABLE [dbo].[jobDetails] ADD  CONSTRAINT [DF_jobDetails_dataIsCreated]  DEFAULT (getdate()) FOR [dataIsCreated]
GO
ALTER TABLE [dbo].[jobDetails] ADD  CONSTRAINT [DF_jobDetails_dataIsUpdated]  DEFAULT (getdate()) FOR [dataIsUpdated]
GO
ALTER TABLE [dbo].[jobDetails] ADD  CONSTRAINT [DF_jobDetails_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[jobDetails] ADD  CONSTRAINT [DF_jobDetails_isDelete]  DEFAULT ((0)) FOR [isDelete]
GO
ALTER TABLE [dbo].[jobDetails] ADD  CONSTRAINT [DF_jobDetails_compeletPosted]  DEFAULT ((0)) FOR [compeletPosted]
GO
ALTER TABLE [dbo].[jobDetails] ADD  CONSTRAINT [DF_jobDetails_salaryVisibleToEmployee]  DEFAULT ((1)) FOR [salaryVisibleToEmployee]
GO
ALTER TABLE [dbo].[jobStatus] ADD  CONSTRAINT [DF_jobStatus_dataIsCreated]  DEFAULT (getdate()) FOR [dataIsCreated]
GO
ALTER TABLE [dbo].[jobStatus] ADD  CONSTRAINT [DF_jobStatus_dataIsUpdated]  DEFAULT (getdate()) FOR [dataIsUpdated]
GO
ALTER TABLE [dbo].[jobStatus] ADD  CONSTRAINT [DF_jobStatus_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[jobStatus] ADD  CONSTRAINT [DF_jobStatus_isDelete]  DEFAULT ((0)) FOR [isDelete]
GO
ALTER TABLE [dbo].[qenAppliedJob] ADD  CONSTRAINT [DF_qenAppliedJob_dataIsCreated]  DEFAULT (getdate()) FOR [dataIsCreated]
GO
ALTER TABLE [dbo].[qenAppliedJob] ADD  CONSTRAINT [DF_qenAppliedJob_dataIsUpdated]  DEFAULT (getdate()) FOR [dataIsUpdated]
GO
ALTER TABLE [dbo].[qenAppliedJob] ADD  CONSTRAINT [DF_qenAppliedJob_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[qenAppliedJob] ADD  CONSTRAINT [DF_qenAppliedJob_isDelete]  DEFAULT ((0)) FOR [isDelete]
GO
ALTER TABLE [dbo].[qenMialSendInterested] ADD  CONSTRAINT [DF_qenMialSendInterested_dataIsCreated]  DEFAULT (getdate()) FOR [dataIsCreated]
GO
ALTER TABLE [dbo].[qenMialSendInterested] ADD  CONSTRAINT [DF_qenMialSendInterested_dataIsUpdated]  DEFAULT (getdate()) FOR [dataIsUpdated]
GO
ALTER TABLE [dbo].[qenSkills] ADD  CONSTRAINT [DF_qenSkills_yearOfExp]  DEFAULT ((0)) FOR [yearOfExp]
GO
ALTER TABLE [dbo].[role_action] ADD  CONSTRAINT [DF_role_action_is_active]  DEFAULT ((0)) FOR [is_active]
GO
ALTER TABLE [dbo].[companyDetails]  WITH CHECK ADD  CONSTRAINT [FK_companyDetails_city] FOREIGN KEY([cityID])
REFERENCES [dbo].[city] ([cityID])
GO
ALTER TABLE [dbo].[companyDetails] CHECK CONSTRAINT [FK_companyDetails_city]
GO
ALTER TABLE [dbo].[companyDetails]  WITH CHECK ADD  CONSTRAINT [FK_companyDetails_employerType] FOREIGN KEY([employerTypeID])
REFERENCES [dbo].[employerType] ([employerTypeID])
GO
ALTER TABLE [dbo].[companyDetails] CHECK CONSTRAINT [FK_companyDetails_employerType]
GO
ALTER TABLE [dbo].[companyDetails]  WITH CHECK ADD  CONSTRAINT [FK_companyDetails_industry] FOREIGN KEY([companyIndustry])
REFERENCES [dbo].[industry] ([industryID])
GO
ALTER TABLE [dbo].[companyDetails] CHECK CONSTRAINT [FK_companyDetails_industry]
GO
ALTER TABLE [dbo].[EmployerDetails]  WITH CHECK ADD  CONSTRAINT [FK_EmployerDetails_companyDetails] FOREIGN KEY([companyID])
REFERENCES [dbo].[companyDetails] ([companyID])
GO
ALTER TABLE [dbo].[EmployerDetails] CHECK CONSTRAINT [FK_EmployerDetails_companyDetails]
GO
ALTER TABLE [dbo].[EmployerDetails]  WITH CHECK ADD  CONSTRAINT [FK_EmployerDetails_EmployerDetails] FOREIGN KEY([EmployerID])
REFERENCES [dbo].[EmployerDetails] ([EmployerID])
GO
ALTER TABLE [dbo].[EmployerDetails] CHECK CONSTRAINT [FK_EmployerDetails_EmployerDetails]
GO
ALTER TABLE [dbo].[finalQendidateList]  WITH CHECK ADD  CONSTRAINT [FK_finalQendidateList_qendidateList] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[finalQendidateList] CHECK CONSTRAINT [FK_finalQendidateList_qendidateList]
GO
ALTER TABLE [dbo].[interviewedCandidateList]  WITH CHECK ADD  CONSTRAINT [FK_interviewedCandidateList_jobDetails] FOREIGN KEY([jobID])
REFERENCES [dbo].[jobDetails] ([jobID])
GO
ALTER TABLE [dbo].[interviewedCandidateList] CHECK CONSTRAINT [FK_interviewedCandidateList_jobDetails]
GO
ALTER TABLE [dbo].[interviewedCandidateList]  WITH CHECK ADD  CONSTRAINT [FK_interviewedCandidateList_qendidateList] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[interviewedCandidateList] CHECK CONSTRAINT [FK_interviewedCandidateList_qendidateList]
GO
ALTER TABLE [dbo].[jobDetails]  WITH CHECK ADD  CONSTRAINT [FK_jobDetails_city] FOREIGN KEY([cityID])
REFERENCES [dbo].[city] ([cityID])
GO
ALTER TABLE [dbo].[jobDetails] CHECK CONSTRAINT [FK_jobDetails_city]
GO
ALTER TABLE [dbo].[jobDetails]  WITH CHECK ADD  CONSTRAINT [FK_jobDetails_EmployeModified] FOREIGN KEY([modifiedBy])
REFERENCES [dbo].[EmployerDetails] ([EmployerID])
GO
ALTER TABLE [dbo].[jobDetails] CHECK CONSTRAINT [FK_jobDetails_EmployeModified]
GO
ALTER TABLE [dbo].[jobDetails]  WITH CHECK ADD  CONSTRAINT [FK_jobDetails_EmployerDetails] FOREIGN KEY([createdBy])
REFERENCES [dbo].[EmployerDetails] ([EmployerID])
GO
ALTER TABLE [dbo].[jobDetails] CHECK CONSTRAINT [FK_jobDetails_EmployerDetails]
GO
ALTER TABLE [dbo].[jobDetails]  WITH CHECK ADD  CONSTRAINT [FK_jobDetails_EmploymentType] FOREIGN KEY([EmploymentTypeID])
REFERENCES [dbo].[EmploymentType] ([EmploymentTypeID])
GO
ALTER TABLE [dbo].[jobDetails] CHECK CONSTRAINT [FK_jobDetails_EmploymentType]
GO
ALTER TABLE [dbo].[jobDetails]  WITH CHECK ADD  CONSTRAINT [FK_jobDetails_industry] FOREIGN KEY([industryID])
REFERENCES [dbo].[industry] ([industryID])
GO
ALTER TABLE [dbo].[jobDetails] CHECK CONSTRAINT [FK_jobDetails_industry]
GO
ALTER TABLE [dbo].[jobDetails]  WITH CHECK ADD  CONSTRAINT [FK_jobDetails_jobDetails] FOREIGN KEY([companyID])
REFERENCES [dbo].[companyDetails] ([companyID])
GO
ALTER TABLE [dbo].[jobDetails] CHECK CONSTRAINT [FK_jobDetails_jobDetails]
GO
ALTER TABLE [dbo].[jobDetails]  WITH CHECK ADD  CONSTRAINT [FK_jobDetails_jobStatus] FOREIGN KEY([jobStatusID])
REFERENCES [dbo].[jobStatus] ([jobStatusID])
GO
ALTER TABLE [dbo].[jobDetails] CHECK CONSTRAINT [FK_jobDetails_jobStatus]
GO
ALTER TABLE [dbo].[jobSavedQendidate]  WITH CHECK ADD  CONSTRAINT [FK_jobSavedQendidate_jobDetails] FOREIGN KEY([jobID])
REFERENCES [dbo].[jobDetails] ([jobID])
GO
ALTER TABLE [dbo].[jobSavedQendidate] CHECK CONSTRAINT [FK_jobSavedQendidate_jobDetails]
GO
ALTER TABLE [dbo].[jobSavedQendidate]  WITH CHECK ADD  CONSTRAINT [FK_jobSavedQendidate_qendidateList] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[jobSavedQendidate] CHECK CONSTRAINT [FK_jobSavedQendidate_qendidateList]
GO
ALTER TABLE [dbo].[jobSkills]  WITH CHECK ADD  CONSTRAINT [FK_jobSkills_jobDetails] FOREIGN KEY([jobID])
REFERENCES [dbo].[jobDetails] ([jobID])
GO
ALTER TABLE [dbo].[jobSkills] CHECK CONSTRAINT [FK_jobSkills_jobDetails]
GO
ALTER TABLE [dbo].[jobSkills]  WITH CHECK ADD  CONSTRAINT [FK_jobSkills_skills] FOREIGN KEY([skillsID])
REFERENCES [dbo].[skills] ([skillsID])
GO
ALTER TABLE [dbo].[jobSkills] CHECK CONSTRAINT [FK_jobSkills_skills]
GO
ALTER TABLE [dbo].[ProfilePerformance]  WITH CHECK ADD  CONSTRAINT [FK_ProfilePerformance_EmployerDetails] FOREIGN KEY([EmployerID])
REFERENCES [dbo].[EmployerDetails] ([EmployerID])
GO
ALTER TABLE [dbo].[ProfilePerformance] CHECK CONSTRAINT [FK_ProfilePerformance_EmployerDetails]
GO
ALTER TABLE [dbo].[ProfilePerformance]  WITH CHECK ADD  CONSTRAINT [FK_ProfilePerformance_qendidateList] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[ProfilePerformance] CHECK CONSTRAINT [FK_ProfilePerformance_qendidateList]
GO
ALTER TABLE [dbo].[qenAppliedJob]  WITH CHECK ADD  CONSTRAINT [FK_qenAppliedJob_App_status] FOREIGN KEY([App_status_id])
REFERENCES [dbo].[App_status] ([App_status_id])
GO
ALTER TABLE [dbo].[qenAppliedJob] CHECK CONSTRAINT [FK_qenAppliedJob_App_status]
GO
ALTER TABLE [dbo].[qenAppliedJob]  WITH CHECK ADD  CONSTRAINT [FK_qenAppliedJob_jobDetails] FOREIGN KEY([jobID])
REFERENCES [dbo].[jobDetails] ([jobID])
GO
ALTER TABLE [dbo].[qenAppliedJob] CHECK CONSTRAINT [FK_qenAppliedJob_jobDetails]
GO
ALTER TABLE [dbo].[qenAppliedJob]  WITH CHECK ADD  CONSTRAINT [FK_qenAppliedJob_qenAppliedJob] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[qenAppliedJob] CHECK CONSTRAINT [FK_qenAppliedJob_qenAppliedJob]
GO
ALTER TABLE [dbo].[qendidateGraduation]  WITH CHECK ADD  CONSTRAINT [FK__qendidate__qenID__0EA330E9] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[qendidateGraduation] CHECK CONSTRAINT [FK__qendidate__qenID__0EA330E9]
GO
ALTER TABLE [dbo].[qendidateList]  WITH CHECK ADD  CONSTRAINT [FK_qenCityID_CityID] FOREIGN KEY([qenCityID])
REFERENCES [dbo].[city] ([cityID])
GO
ALTER TABLE [dbo].[qendidateList] CHECK CONSTRAINT [FK_qenCityID_CityID]
GO
ALTER TABLE [dbo].[qendidateListInJob]  WITH CHECK ADD  CONSTRAINT [FK_qendidateListInJob_jobDetails] FOREIGN KEY([jobID])
REFERENCES [dbo].[jobDetails] ([jobID])
GO
ALTER TABLE [dbo].[qendidateListInJob] CHECK CONSTRAINT [FK_qendidateListInJob_jobDetails]
GO
ALTER TABLE [dbo].[qendidateListInJob]  WITH CHECK ADD  CONSTRAINT [FK_qendidateListInJob_qendidateList] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[qendidateListInJob] CHECK CONSTRAINT [FK_qendidateListInJob_qendidateList]
GO
ALTER TABLE [dbo].[qendidatePGraduation]  WITH CHECK ADD  CONSTRAINT [FK__qendidate__qenID__267ABA7A] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[qendidatePGraduation] CHECK CONSTRAINT [FK__qendidate__qenID__267ABA7A]
GO
ALTER TABLE [dbo].[qendidatePHD]  WITH CHECK ADD  CONSTRAINT [FK__qendidate__qenID__286302EC] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[qendidatePHD] CHECK CONSTRAINT [FK__qendidate__qenID__286302EC]
GO
ALTER TABLE [dbo].[qendidateProfileSharedWith]  WITH CHECK ADD  CONSTRAINT [FK_qendidateProfileSharedWith_qendidateList] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[qendidateProfileSharedWith] CHECK CONSTRAINT [FK_qendidateProfileSharedWith_qendidateList]
GO
ALTER TABLE [dbo].[qendidateSavedForReference]  WITH CHECK ADD  CONSTRAINT [FK_qendidateSavedForReference_jobDetails] FOREIGN KEY([jobID])
REFERENCES [dbo].[jobDetails] ([jobID])
GO
ALTER TABLE [dbo].[qendidateSavedForReference] CHECK CONSTRAINT [FK_qendidateSavedForReference_jobDetails]
GO
ALTER TABLE [dbo].[qendidateSavedForReference]  WITH CHECK ADD  CONSTRAINT [FK_qendidateSavedForReference_qendidateList] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[qendidateSavedForReference] CHECK CONSTRAINT [FK_qendidateSavedForReference_qendidateList]
GO
ALTER TABLE [dbo].[qendidateTestSchedule]  WITH CHECK ADD  CONSTRAINT [FK_qendidateTestSchedule_jobDetails] FOREIGN KEY([jobID])
REFERENCES [dbo].[jobDetails] ([jobID])
GO
ALTER TABLE [dbo].[qendidateTestSchedule] CHECK CONSTRAINT [FK_qendidateTestSchedule_jobDetails]
GO
ALTER TABLE [dbo].[qendidateTestSchedule]  WITH CHECK ADD  CONSTRAINT [FK_qendidateTestSchedule_qendidateList] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[qendidateTestSchedule] CHECK CONSTRAINT [FK_qendidateTestSchedule_qendidateList]
GO
ALTER TABLE [dbo].[qenEmpDetails]  WITH CHECK ADD  CONSTRAINT [FK_qenEmpDetails_qendidateList] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[qenEmpDetails] CHECK CONSTRAINT [FK_qenEmpDetails_qendidateList]
GO
ALTER TABLE [dbo].[qenExamHistory]  WITH CHECK ADD  CONSTRAINT [FK_qenExamHistory_jobDetails] FOREIGN KEY([JobID])
REFERENCES [dbo].[jobDetails] ([jobID])
GO
ALTER TABLE [dbo].[qenExamHistory] CHECK CONSTRAINT [FK_qenExamHistory_jobDetails]
GO
ALTER TABLE [dbo].[qenExamHistory]  WITH CHECK ADD  CONSTRAINT [FK_qenExamHistory_qendidateList] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[qenExamHistory] CHECK CONSTRAINT [FK_qenExamHistory_qendidateList]
GO
ALTER TABLE [dbo].[qenHigherSecondary]  WITH CHECK ADD  CONSTRAINT [FK__qenHigher__qenID__1273C1CD] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[qenHigherSecondary] CHECK CONSTRAINT [FK__qenHigher__qenID__1273C1CD]
GO
ALTER TABLE [dbo].[qenInterviewSchedule]  WITH CHECK ADD  CONSTRAINT [FK_qenInterviewSchedule_jobDetails] FOREIGN KEY([JobID])
REFERENCES [dbo].[jobDetails] ([jobID])
GO
ALTER TABLE [dbo].[qenInterviewSchedule] CHECK CONSTRAINT [FK_qenInterviewSchedule_jobDetails]
GO
ALTER TABLE [dbo].[qenInterviewSchedule]  WITH CHECK ADD  CONSTRAINT [FK_qenInterviewSchedule_qendidateList] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[qenInterviewSchedule] CHECK CONSTRAINT [FK_qenInterviewSchedule_qendidateList]
GO
ALTER TABLE [dbo].[qenMialSendInterested]  WITH CHECK ADD  CONSTRAINT [FK_qenMialSendInterested_jobDetails] FOREIGN KEY([jobID])
REFERENCES [dbo].[jobDetails] ([jobID])
GO
ALTER TABLE [dbo].[qenMialSendInterested] CHECK CONSTRAINT [FK_qenMialSendInterested_jobDetails]
GO
ALTER TABLE [dbo].[qenMialSendInterested]  WITH CHECK ADD  CONSTRAINT [FK_qenMialSendInterested_qendidateList] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[qenMialSendInterested] CHECK CONSTRAINT [FK_qenMialSendInterested_qendidateList]
GO
ALTER TABLE [dbo].[qenReference]  WITH CHECK ADD  CONSTRAINT [FK__qenRefere__qenID__1367E606] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[qenReference] CHECK CONSTRAINT [FK__qenRefere__qenID__1367E606]
GO
ALTER TABLE [dbo].[qenResume]  WITH CHECK ADD  CONSTRAINT [FK__qenResume__qenID__145C0A3F] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[qenResume] CHECK CONSTRAINT [FK__qenResume__qenID__145C0A3F]
GO
ALTER TABLE [dbo].[qenSecondary]  WITH CHECK ADD  CONSTRAINT [FK__qenSecond__qenID__15502E78] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[qenSecondary] CHECK CONSTRAINT [FK__qenSecond__qenID__15502E78]
GO
ALTER TABLE [dbo].[qenSkills]  WITH CHECK ADD  CONSTRAINT [FK_qenSkills_qendidateList] FOREIGN KEY([qenID])
REFERENCES [dbo].[qendidateList] ([qenID])
GO
ALTER TABLE [dbo].[qenSkills] CHECK CONSTRAINT [FK_qenSkills_qendidateList]
GO
ALTER TABLE [dbo].[qenSkills]  WITH CHECK ADD  CONSTRAINT [FK_qenSkills_skills] FOREIGN KEY([skillsID])
REFERENCES [dbo].[skills] ([skillsID])
GO
ALTER TABLE [dbo].[qenSkills] CHECK CONSTRAINT [FK_qenSkills_skills]
GO
ALTER TABLE [dbo].[role_action]  WITH CHECK ADD  CONSTRAINT [FK_role_action_role] FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([roleID])
GO
ALTER TABLE [dbo].[role_action] CHECK CONSTRAINT [FK_role_action_role]
GO
ALTER TABLE [dbo].[state]  WITH CHECK ADD  CONSTRAINT [FK_state_country] FOREIGN KEY([CountryID])
REFERENCES [dbo].[country] ([CountryID])
GO
ALTER TABLE [dbo].[state] CHECK CONSTRAINT [FK_state_country]
GO
USE [master]
GO
ALTER DATABASE [oriondb] SET  READ_WRITE 
GO
