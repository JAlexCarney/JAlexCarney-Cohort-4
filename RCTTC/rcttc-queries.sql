USE RCTTC;

-- Find all performances in the last quarter of 2021 (Oct. 1, 2021 - Dec. 31 2021).
SELECT * FROM Performance
WHERE [Date] < '2021-12-31' AND [Date] > '2021-08-01';

-- List customers without duplication.
SELECT * FROM Customer;

-- Find all customers without a .com email address.
SELECT * FROM Customer
WHERE Customer.Email NOT LIKE '%.com%';

-- Find the three cheapest shows.
SELECT TOP(3) * FROM Performance
ORDER BY TicketPrice;

-- List customers and the show they're attending with no duplication.
SELECT Distinct
    Customer.Id,
    Customer.FirstName + ' ' + Customer.LastName as [Name],
    Ticket.PerformanceId
FROM Customer
INNER JOIN Ticket ON Ticket.CustomerId = Customer.Id;

-- List customer, show, theater, and seat number in one query.
SELECT DISTINCT
    Customer.FirstName + '' + Customer.LastName AS [Customer Name],
    Show.Name,
    Theater.Name,
    T.Seat
FROM Ticket T
INNER JOIN Customer ON T.CustomerId = Customer.Id
INNER JOIN Performance P ON T.PerformanceId = P.Id
INNER JOIN Theater ON P.TheaterId = Theater.Id
INNER JOIN Show ON P.ShowId = Show.Id
ORDER BY Theater.Name, Show.Name, T.Seat;

-- Find customers without an address.
SELECT * FROM Customer WHERE [Address] IS NULL;
 
-- Recreate the spreadsheet data with a single query.
SELECT DISTINCT
    Customer.FirstName AS 'customer_first',
    Customer.LastName AS 'customer_last',
    Customer.Email AS 'customer_email',
    Customer.Phone AS 'customer_phone',
    Customer.Address AS 'customer_address',
    T.Seat AS 'seat',
    Show.Name AS 'show',
    P.TicketPrice AS 'ticket_price',
    P.[Date] AS 'date',
    Theater.Name AS 'theater',
    Theater.Address AS 'theater_address',
    Theater.Phone AS 'theater_phone',
    Theater.Email AS 'theater_email'
FROM Ticket T
INNER JOIN Customer ON T.CustomerId = Customer.Id
INNER JOIN Performance P ON T.PerformanceId = P.Id
INNER JOIN Theater ON P.TheaterId = Theater.Id
INNER JOIN Show ON P.ShowId = Show.Id
ORDER BY Theater.Name, Show.Name, P.Date, T.Seat;

-- Count total tickets purchased per customer.
SELECT
    CustomerId AS 'Customer',
    COUNT(*) AS 'TicketsPurchased'
FROM Ticket T
INNER JOIN Customer ON T.CustomerId = Customer.Id
GROUP BY CustomerId;

-- Calculate the total revenue per show based on tickets sold.
SELECT
    (SELECT TOP(1) [Name] FROM Show WHERE Show.Id = Performance.ShowId) AS 'Show',
    MAX(Performance.TicketPrice) * Count(*) AS 'Revenue'
FROM Ticket T
INNER JOIN Performance ON T.PerformanceId = Performance.Id
GROUP BY Performance.ShowId;

-- Calculate the total revenue per theater based on tickets sold.
SELECT 
    (SELECT TOP(1) [Name] FROM Theater WHERE Theater.Id = P.TheaterId) AS 'Theater',
    MAX(P.TicketPrice) * Count(*) AS 'Revenue'
FROM Ticket
INNER JOIN Performance P On PerformanceId = P.Id
GROUP BY P.TheaterId;

-- Who is the biggest supporter of RCTTC? Who spent the most in 2021?
SELECT TOP(1)
    (SELECT TOP(1) [FirstName] + ' ' + [LastName] FROM Customer WHERE Customer.Id = Ticket.CustomerId) AS 'Customer',
    MAX(P.TicketPrice) * Count(*) AS 'MoneySpent'
FROM Ticket
INNER JOIN Performance P On PerformanceId = P.Id
GROUP BY Ticket.CustomerId
ORDER BY MAX(P.TicketPrice) * Count(*) DESC;