SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL Serializable
GO
BEGIN TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Booking]'
GO
CREATE TABLE [dbo].[Booking]
(
[Id] [bigint] NOT NULL IDENTITY(0, 1),
[UserId] [bigint] NULL,
[Name] [nvarchar] (120) COLLATE Modern_Spanish_CI_AS NULL,
[ReservationToken] [nvarchar] (60) COLLATE Modern_Spanish_CI_AS NULL,
[Comments] [nvarchar] (255) COLLATE Modern_Spanish_CI_AS NULL,
[BookingDate] [datetime] NOT NULL,
[ExpirationDate] [datetime] NULL,
[CreationDate] [datetime] NULL,
[Canceled] [bit] NOT NULL CONSTRAINT [DF__Reservati__Cance__14270015] DEFAULT ((0)),
[IdBookingType] [int] NULL,
[TableId] [bigint] NULL,
[Email] [nvarchar] (255) COLLATE Modern_Spanish_CI_AS NULL,
[Assistants] [int] NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [sqlite_master_PK_Reservation] on [dbo].[Booking]'
GO
ALTER TABLE [dbo].[Booking] ADD CONSTRAINT [sqlite_master_PK_Reservation] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding constraints to [dbo].[Booking]'
GO
ALTER TABLE [dbo].[Booking] ADD CONSTRAINT [sqlite_autoindex_Reservation_1] UNIQUE NONCLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Image]'
GO
CREATE TABLE [dbo].[Image]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[Content] [nvarchar] (max) COLLATE Modern_Spanish_CI_AS NULL,
[Name] [nvarchar] (max) COLLATE Modern_Spanish_CI_AS NULL,
[MimeType] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL,
[Extension] [nvarchar] (20) COLLATE Modern_Spanish_CI_AS NULL,
[ContentType] [int] NULL,
[ModificationCounter] [int] NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK__Image__3214EC0721AE3BAD] on [dbo].[Image]'
GO
ALTER TABLE [dbo].[Image] ADD CONSTRAINT [PK__Image__3214EC0721AE3BAD] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Dish]'
GO
CREATE TABLE [dbo].[Dish]
(
[Id] [bigint] NOT NULL IDENTITY(0, 1),
[Name] [nvarchar] (120) COLLATE Modern_Spanish_CI_AS NULL,
[Description] [nvarchar] (4000) COLLATE Modern_Spanish_CI_AS NULL,
[Price] [decimal] (16, 10) NULL,
[IdImage] [bigint] NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [sqlite_master_PK_Dish] on [dbo].[Dish]'
GO
ALTER TABLE [dbo].[Dish] ADD CONSTRAINT [sqlite_master_PK_Dish] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Category]'
GO
CREATE TABLE [dbo].[Category]
(
[Id] [bigint] NOT NULL IDENTITY(0, 1),
[Name] [nvarchar] (120) COLLATE Modern_Spanish_CI_AS NULL,
[Description] [nvarchar] (255) COLLATE Modern_Spanish_CI_AS NULL,
[ShowOrder] [int] NULL,
[ModificationCounter] [int] NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [sqlite_master_PK_Category] on [dbo].[Category]'
GO
ALTER TABLE [dbo].[Category] ADD CONSTRAINT [sqlite_master_PK_Category] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[DishCategory]'
GO
CREATE TABLE [dbo].[DishCategory]
(
[Id] [bigint] NOT NULL IDENTITY(0, 1),
[IdDish] [bigint] NOT NULL,
[IdCategory] [bigint] NOT NULL,
[ModificationCounter] [int] NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [sqlite_master_PK_DishCategory] on [dbo].[DishCategory]'
GO
ALTER TABLE [dbo].[DishCategory] ADD CONSTRAINT [sqlite_master_PK_DishCategory] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Ingredient]'
GO
CREATE TABLE [dbo].[Ingredient]
(
[Id] [bigint] NOT NULL IDENTITY(0, 1),
[Name] [nvarchar] (120) COLLATE Modern_Spanish_CI_AS NULL,
[Description] [text] COLLATE Modern_Spanish_CI_AS NULL,
[Price] [decimal] (16, 10) NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [sqlite_master_PK_Ingredient] on [dbo].[Ingredient]'
GO
ALTER TABLE [dbo].[Ingredient] ADD CONSTRAINT [sqlite_master_PK_Ingredient] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[DishIngredient]'
GO
CREATE TABLE [dbo].[DishIngredient]
(
[Id] [bigint] NOT NULL IDENTITY(0, 1),
[IdDish] [bigint] NOT NULL,
[IdIngredient] [bigint] NOT NULL,
[ModificationCounter] [int] NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [sqlite_master_PK_DishIngredient] on [dbo].[DishIngredient]'
GO
ALTER TABLE [dbo].[DishIngredient] ADD CONSTRAINT [sqlite_master_PK_DishIngredient] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[InvitedGuest]'
GO
CREATE TABLE [dbo].[InvitedGuest]
(
[Id] [bigint] NOT NULL IDENTITY(0, 1),
[IdBooking] [bigint] NOT NULL,
[GuestToken] [nvarchar] (60) COLLATE Modern_Spanish_CI_AS NULL,
[Email] [nvarchar] (60) COLLATE Modern_Spanish_CI_AS NULL,
[Accepted] [bit] NULL,
[ModificationDate] [datetime] NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [sqlite_master_PK_InvitationGuest] on [dbo].[InvitedGuest]'
GO
ALTER TABLE [dbo].[InvitedGuest] ADD CONSTRAINT [sqlite_master_PK_InvitationGuest] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding constraints to [dbo].[InvitedGuest]'
GO
ALTER TABLE [dbo].[InvitedGuest] ADD CONSTRAINT [sqlite_autoindex_InvitationGuest_1] UNIQUE NONCLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Order]'
GO
CREATE TABLE [dbo].[Order]
(
[Id] [bigint] NOT NULL IDENTITY(0, 1),
[IdReservation] [bigint] NULL,
[IdInvitationGuest] [bigint] NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [sqlite_master_PK_Order] on [dbo].[Order]'
GO
ALTER TABLE [dbo].[Order] ADD CONSTRAINT [sqlite_master_PK_Order] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[OrderLine]'
GO
CREATE TABLE [dbo].[OrderLine]
(
[Id] [bigint] NOT NULL IDENTITY(0, 1),
[IdDish] [bigint] NOT NULL,
[Amount] [int] NULL,
[Comment] [nvarchar] (255) COLLATE Modern_Spanish_CI_AS NULL,
[IdOrder] [bigint] NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [sqlite_master_PK_OrderLine] on [dbo].[OrderLine]'
GO
ALTER TABLE [dbo].[OrderLine] ADD CONSTRAINT [sqlite_master_PK_OrderLine] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[OrderDishExtraIngredient]'
GO
CREATE TABLE [dbo].[OrderDishExtraIngredient]
(
[Id] [bigint] NOT NULL IDENTITY(0, 1),
[IdOrderLine] [bigint] NOT NULL,
[IdIngredient] [bigint] NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [sqlite_master_PK_OrderDIshExtraIngredient] on [dbo].[OrderDishExtraIngredient]'
GO
ALTER TABLE [dbo].[OrderDishExtraIngredient] ADD CONSTRAINT [sqlite_master_PK_OrderDIshExtraIngredient] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[User]'
GO
CREATE TABLE [dbo].[User]
(
[Id] [bigint] NOT NULL IDENTITY(0, 1),
[UserName] [nvarchar] (120) COLLATE Modern_Spanish_CI_AS NULL,
[Password] [nvarchar] (255) COLLATE Modern_Spanish_CI_AS NULL,
[Email] [nvarchar] (60) COLLATE Modern_Spanish_CI_AS NULL,
[IdRole] [bigint] NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [sqlite_master_PK_User] on [dbo].[User]'
GO
ALTER TABLE [dbo].[User] ADD CONSTRAINT [sqlite_master_PK_User] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Table]'
GO
CREATE TABLE [dbo].[Table]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[SeatsNumber] [int] NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK__Table__3214EC0752E0522A] on [dbo].[Table]'
GO
ALTER TABLE [dbo].[Table] ADD CONSTRAINT [PK__Table__3214EC0752E0522A] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[UserRole]'
GO
CREATE TABLE [dbo].[UserRole]
(
[Id] [bigint] NOT NULL IDENTITY(0, 1),
[Name] [nvarchar] (120) COLLATE Modern_Spanish_CI_AS NULL,
[Active] [bit] NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [sqlite_master_PK_UserRole] on [dbo].[UserRole]'
GO
ALTER TABLE [dbo].[UserRole] ADD CONSTRAINT [sqlite_master_PK_UserRole] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[UserFavourite]'
GO
CREATE TABLE [dbo].[UserFavourite]
(
[Id] [bigint] NOT NULL IDENTITY(0, 1),
[IdUser] [bigint] NOT NULL,
[IdDish] [bigint] NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [sqlite_master_PK_UserFavourite] on [dbo].[UserFavourite]'
GO
ALTER TABLE [dbo].[UserFavourite] ADD CONSTRAINT [sqlite_master_PK_UserFavourite] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[Dish]'
GO
ALTER TABLE [dbo].[Dish] WITH NOCHECK  ADD CONSTRAINT [FK_Dish_image] FOREIGN KEY ([IdImage]) REFERENCES [dbo].[Image] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[InvitedGuest]'
GO
ALTER TABLE [dbo].[InvitedGuest] ADD CONSTRAINT [FK_InvitationGuest_0_0] FOREIGN KEY ([IdBooking]) REFERENCES [dbo].[Booking] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[Order]'
GO
ALTER TABLE [dbo].[Order] ADD CONSTRAINT [FK_Order_0_0] FOREIGN KEY ([IdReservation]) REFERENCES [dbo].[Booking] ([Id])
GO
ALTER TABLE [dbo].[Order] ADD CONSTRAINT [FK_Order_0_1] FOREIGN KEY ([IdInvitationGuest]) REFERENCES [dbo].[InvitedGuest] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[Booking]'
GO
ALTER TABLE [dbo].[Booking] ADD CONSTRAINT [FK_Reservation_0_0] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Booking] ADD CONSTRAINT [FK_Reservation_table] FOREIGN KEY ([TableId]) REFERENCES [dbo].[Table] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[DishCategory]'
GO
ALTER TABLE [dbo].[DishCategory] ADD CONSTRAINT [FK_DishCategory_0_0] FOREIGN KEY ([IdCategory]) REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[DishCategory] ADD CONSTRAINT [FK_DishCategory_1_0] FOREIGN KEY ([IdDish]) REFERENCES [dbo].[Dish] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[DishIngredient]'
GO
ALTER TABLE [dbo].[DishIngredient] ADD CONSTRAINT [FK_DishIngredient_1_0] FOREIGN KEY ([IdDish]) REFERENCES [dbo].[Dish] ([Id])
GO
ALTER TABLE [dbo].[DishIngredient] ADD CONSTRAINT [FK_DishIngredient_0_0] FOREIGN KEY ([IdIngredient]) REFERENCES [dbo].[Ingredient] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[OrderLine]'
GO
ALTER TABLE [dbo].[OrderLine] ADD CONSTRAINT [FK_OrderLine_1_0] FOREIGN KEY ([IdDish]) REFERENCES [dbo].[Dish] ([Id])
GO
ALTER TABLE [dbo].[OrderLine] ADD CONSTRAINT [FK_OrderLine_0_0] FOREIGN KEY ([IdOrder]) REFERENCES [dbo].[Order] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[UserFavourite]'
GO
ALTER TABLE [dbo].[UserFavourite] ADD CONSTRAINT [FK_UserFavourite_0_0] FOREIGN KEY ([IdDish]) REFERENCES [dbo].[Dish] ([Id])
GO
ALTER TABLE [dbo].[UserFavourite] ADD CONSTRAINT [FK_UserFavourite_1_0] FOREIGN KEY ([IdUser]) REFERENCES [dbo].[User] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[OrderDishExtraIngredient]'
GO
ALTER TABLE [dbo].[OrderDishExtraIngredient] ADD CONSTRAINT [FK_OrderDIshExtraIngredient_1_0] FOREIGN KEY ([IdIngredient]) REFERENCES [dbo].[Ingredient] ([Id])
GO
ALTER TABLE [dbo].[OrderDishExtraIngredient] ADD CONSTRAINT [FK_OrderDIshExtraIngredient_0_0] FOREIGN KEY ([IdOrderLine]) REFERENCES [dbo].[OrderLine] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[User]'
GO
ALTER TABLE [dbo].[User] ADD CONSTRAINT [FK_User_0_0] FOREIGN KEY ([IdRole]) REFERENCES [dbo].[UserRole] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
COMMIT TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
DECLARE @Success AS BIT
SET @Success = 1
SET NOEXEC OFF
IF (@Success = 1) PRINT 'The database update succeeded'
ELSE BEGIN
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT 'The database update failed'
END
GO
