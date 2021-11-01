drop database BookStoreDB

----------------------------------------------

create database BookStoreDB

use BookStoreDB


-------------------------------------------------------
create table category
(
	CategoryId	int identity(101,1) primary key,
	CategoryName varchar(20) ,
	[Status] bit ,
	Position int,
	CreatedAt date default getdate(),
)
create table books
(
	BookId int identity(101,1) primary key,
	CategoryId	int foreign key references category(CategoryId),
	Title varchar(30),
	ISBN int,
	[Year] int,
	Price float,
	[Description] varchar(100),
	Position int,
	[Status] bit,
	[Image] varchar(150),
	Author varchar(20),
	Stock int
)




create table users
(
	UserId int identity(101,1) primary key,
	Username varchar(20),
	[Password] varchar(20),
	[Role] varchar(20),
	isActive bit ,
	
	constraint chk_role check ([Role] in ('Admin','Customer'))
)

create table shipping
(
	id int identity (101,1) primary key,
	userId nvarchar(128) foreign key references [User](Id),
	[name] varchar(20),
	[address] varchar(50),
	country varchar(20),
	[state] varchar (20),
	pincode int
)

create table coupons
(
    couponcode varchar(20) primary key,
	discount float ,
	stock int 
)

create table wishlist
(
    id int primary key identity(101,1),
	booksid int foreign key references books(BookId),
	userid nvarchar(128) foreign key references [User](Id)
)


create table orders
(
	OrderId int identity(101,1) primary key,
	userId nvarchar(128) foreign key references [User](Id),
	TotalPrice float
)

create table cart
(
	Id int identity(101,1) primary key ,
	BookId int foreign key references books(BookId),
	userId nvarchar(128) foreign key references [User](Id),
	OrderId int foreign key references orders(OrderId),
	BookQuantity int
)





insert into users(Username,Password,Role) values('admin','admin123','Admin')
insert into users(Username,Password,Role) values('basith','basith123','customer')
select * from users

select * from category
insert into category(CategoryName,[Status],Position,CreatedAt) values ('war',1,1,GETDATE())

select * from books
insert into books(CategoryId,Title,ISBN,Year,Price,[Description],Position,[Status],[Image],Author,Stock)
			values (101,'world-war',1001,2010,200,'book on world war',1,1,null,'unknown',10)

insert into books(CategoryId,Title,ISBN,Year,Price,[Description],Position,[Status],[Image],Author,Stock)
			values (101,'civil-war',1002,2015,150,'book on civil war',2,1,(SELECT 
	BulkColumn FROM OPENROWSET(BULK N'C:\Users\bsthp\Downloads\harrypotter.jpeg', SINGLE_BLOB) image),'unknown',15)
delete from books where BookId = 101

select * from [User]