CREATE DATABASE CqrsSingleDatabase;
GO

USE CqrsSingleDatabase

CREATE TABLE Orders(
	Id uniqueidentifier NOT NULL PRIMARY KEY,
	Value decimal(14, 4) NOT NULL,
    DiscountValue decimal(14, 4) NOT NULL,
    TotalValue decimal(14, 4) NOT NULL,
	DiscountId uniqueidentifier NULL)

CREATE TABLE Subscribers(
	Id uniqueidentifier NOT NULL PRIMARY KEY,
	Email varchar(255) NOT NULL)

CREATE TABLE Discounts(
	Id uniqueidentifier NOT NULL PRIMARY KEY,
	Code varchar(255) NOT NULL,
	SubscriberId uniqueidentifier NULL,
	)


ALTER TABLE Orders
ADD CONSTRAINT FK_Discount
FOREIGN KEY (DiscountId) REFERENCES Discounts(Id);

ALTER TABLE Discounts
ADD CONSTRAINT FK_Subscriber
FOREIGN KEY (SubscriberId) REFERENCES Subscribers(Id);

GO

CREATE VIEW OrderFromNewsletterView AS (
SELECT Orders.Id as OrderId,
       Orders.Value,
       Orders.DiscountValue,
       Orders.TotalValue,
       subscriber.Email as SubscriberEmail
FROM Orders
         JOIN Discounts discount on Orders.DiscountId = discount.Id
         JOIN Subscribers subscriber on discount.SubscriberId = subscriber.Id)
go



