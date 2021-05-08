USE RCTTC;

-- CLEAR DATA AND IMPORT CSV BEFORE RUNNING THIS SCRIPT!
-- Insert Data Into Normalized Tables

BEGIN TRANSACTION;

    INSERT INTO Customer (FirstName, LastName, Email, [Address], Phone)
        SELECT DISTINCT
            customer_first,
            customer_last,
            customer_email,
            customer_address,
            customer_phone
        FROM [rcttc-data];
    
    SELECT * FROM Customer;

COMMIT;

BEGIN TRANSACTION;

    INSERT INTO Theater ([Name], [Address], Phone, Email )
        SELECT DISTINCT
            theater,
            theater_address,
            theater_phone,
            theater_email
        FROM [rcttc-data];

    SELECT * FROM Theater;

COMMIT;

BEGIN TRANSACTION;

    INSERT INTO Show ([Name])
        SELECT DISTINCT
            show
        FROM [rcttc-data];

    SELECT * FROM Show;

COMMIT;

BEGIN TRANSACTION;

    INSERT INTO Performance (TheaterId, ShowId, TicketPrice, [Date])
        SELECT DISTINCT
            (SELECT TOP(1) Id FROM Theater WHERE [rcttc-data].theater = Theater.Name),
            (SELECT TOP(1) Id FROM Show WHERE [rcttc-data].show = Show.Name),
            ticket_price,
            [date]
        FROM [rcttc-data];

    SELECT * FROM Show;
    SELECT * FROM Theater;
    SELECT * FROM Performance;

COMMIT;

BEGIN TRANSACTION;

    INSERT INTO Ticket (CustomerId, PerformanceId, Seat)
        SELECT DISTINCT
            (SELECT DISTINCT TOP(1) Id FROM Customer WHERE [rcttc-data].customer_email = Customer.Email),
            (SELECT TOP(1) Id FROM Performance WHERE 
                ShowId = (SELECT TOP(1) Id FROM Show WHERE [rcttc-data].show = [Name])
                AND TheaterId = (SELECT TOP(1) Id FROM Theater WHERE [rcttc-data].theater = [Name])
                AND [Date] = [rcttc-data].[date]),
            seat
        FROM [rcttc-data];

    SELECT * FROM Customer;
    SELECT * FROM Performance;
    SELECT * FROM Ticket;
COMMIT;

-- Update Outdated Data

BEGIN TRANSACTION;
    UPDATE Performance
    SET TicketPrice = 22.25
    WHERE Performance.ShowId = (SELECT TOP(1) Id FROM Show WHERE [Name] = 'The Sky Lit Up')
        AND Performance.TheaterId = (SELECT TOP(1) Id FROM Theater WHERE [Name] = 'Little Fitz')
        AND Performance.[Date] = '2021-03-01';

    SELECT * FROM Theater;
    SELECT * FROM Show;
    SELECT * FROM Performance;
COMMIT;

BEGIN TRANSACTION;
    -- Little Fritz Diagram
    -- https://docs.google.com/spreadsheets/d/1ZijqN7QzGlFvYjE-mRm624bSVlmo2eUwQtmYuXw6HIM/edit#gid=0

    UPDATE Ticket
    SET Seat = 'Z1' -- Must temporaraly set to a non-sense seat to preserve unique seating
    WHERE 
        CustomerId = (SELECT TOP(1) Id FROM Customer
            WHERE [FirstName] = 'Cullen' AND [LastName] ='Guirau')
        AND PerformanceId = (SELECT TOP(1) Id FROM Performance WHERE 
                Performance.ShowId = (SELECT TOP(1) Id FROM Show WHERE [Name] = 'The Sky Lit Up')
                    AND Performance.TheaterId = (SELECT TOP(1) Id FROM Theater WHERE [Name] = 'Little Fitz')
                    AND Performance.[Date] = '2021-03-01')
        AND Seat = 'B4';
    
    UPDATE Ticket
    SET Seat = 'B4'
    WHERE 
        CustomerId = (SELECT TOP(1) Id FROM Customer
            WHERE [FirstName] = 'Pooh' AND [LastName] ='Bedburrow')
        AND PerformanceId = (SELECT TOP(1) Id FROM Performance WHERE 
                Performance.ShowId = (SELECT TOP(1) Id FROM Show WHERE [Name] = 'The Sky Lit Up')
                    AND Performance.TheaterId = (SELECT TOP(1) Id FROM Theater WHERE [Name] = 'Little Fitz')
                    AND Performance.[Date] = '2021-03-01')
        AND Seat = 'A4';

    UPDATE Ticket
    SET Seat = 'A4'
    WHERE 
        CustomerId = (SELECT TOP(1) Id FROM Customer
            WHERE [FirstName] = 'Chiarra' AND [LastName] ='Vail')
        AND PerformanceId = (SELECT TOP(1) Id FROM Performance WHERE 
                Performance.ShowId = (SELECT TOP(1) Id FROM Show WHERE [Name] = 'The Sky Lit Up')
                    AND Performance.TheaterId = (SELECT TOP(1) Id FROM Theater WHERE [Name] = 'Little Fitz')
                    AND Performance.[Date] = '2021-03-01')
        AND Seat = 'C2';

    UPDATE Ticket
    SET Seat = 'C2'
    WHERE 
        CustomerId = (SELECT TOP(1) Id FROM Customer
            WHERE [FirstName] = 'Cullen' AND [LastName] ='Guirau')
        AND PerformanceId = (SELECT TOP(1) Id FROM Performance WHERE 
                Performance.ShowId = (SELECT TOP(1) Id FROM Show WHERE [Name] = 'The Sky Lit Up')
                    AND Performance.TheaterId = (SELECT TOP(1) Id FROM Theater WHERE [Name] = 'Little Fitz')
                    AND Performance.[Date] = '2021-03-01')
        AND Seat = 'Z1';

    SELECT Customer.FirstName, Customer.LastName, Seat FROM Ticket 
        INNER JOIN Customer ON Ticket.CustomerId = Customer.Id
        WHERE PerformanceId = (SELECT TOP(1) Id FROM Performance WHERE 
            Performance.ShowId = (SELECT TOP(1) Id FROM Show WHERE [Name] = 'The Sky Lit Up')
                AND Performance.TheaterId = (SELECT TOP(1) Id FROM Theater WHERE [Name] = 'Little Fitz')
                AND Performance.[Date] = '2021-03-01')
        ORDER BY Seat;

COMMIT;

BEGIN TRANSACTION;
    UPDATE Customer
    SET Phone = '1-801-EAT-CAKE'
    WHERE Customer.FirstName = 'Jammie' 
        AND Customer.LastName = 'Swindles';

    SELECT * FROM Customer ORDER BY LastName;
COMMIT;

-- Delete Incorrect Data

    -- Delete all single-ticket reservations at the 10 Pin. (You don't have to do it with one query.)
BEGIN TRANSACTION;
    DELETE FROM Ticket
    WHERE CustomerId = (SELECT TOP(1) Id FROM Customer
            WHERE [FirstName] = 'Hertha' AND [LastName] ='Glendining');

    DELETE FROM Ticket
    WHERE CustomerId = (SELECT TOP(1) Id FROM Customer
            WHERE [FirstName] = 'Flinn' AND [LastName] ='Crowcher');

    DELETE FROM Ticket
    WHERE CustomerId = (SELECT TOP(1) Id FROM Customer
            WHERE [FirstName] = 'Lucien' AND [LastName] ='Playdon');

    DELETE FROM Ticket
    WHERE CustomerId = (SELECT TOP(1) Id FROM Customer
            WHERE [FirstName] = 'Brian' AND [LastName] ='Bake');

    DELETE FROM Ticket
    WHERE CustomerId = (SELECT TOP(1) Id FROM Customer
            WHERE [FirstName] = 'Loralie' AND [LastName] ='Rois');

    DELETE FROM Ticket
    WHERE CustomerId = (SELECT TOP(1) Id FROM Customer
            WHERE [FirstName] = 'Emily' AND [LastName] ='Duffree');

    DELETE FROM Ticket
    WHERE CustomerId = (SELECT TOP(1) Id FROM Customer
            WHERE [FirstName] = 'Giraud' AND [LastName] ='Bachmann');

    DELETE FROM Ticket
    WHERE CustomerId = (SELECT TOP(1) Id FROM Customer
            WHERE [FirstName] = 'Caye' AND [LastName] ='Treher');

    SELECT * FROM Ticket
    INNER JOIN Customer ON CustomerId = Customer.Id
    INNER JOIN Performance ON PerformanceId = Performance.Id
    WHERE Performance.TheaterId = (SELECT TOP(1) Id FROM Theater WHERE [Name] = '10 Pin');

COMMIT;

    -- Delete the customer Liv Egle of Germany. It appears their reservations were an elaborate joke.
BEGIN TRANSACTION;
    DELETE FROM Ticket
    WHERE CustomerId = (SELECT TOP(1) Id FROM Customer
            WHERE [FirstName] = 'Liv' AND [LastName] ='Egle of Germany');
    
    DELETE FROM Customer
    WHERE [FirstName] = 'Liv' AND [LastName] ='Egle of Germany';
    
    SELECT * FROM Customer
    LEFT OUTER JOIN Ticket ON Ticket.CustomerId = Customer.Id
    WHERE [FirstName] = 'Liv' AND [LastName] ='Egle of Germany';

COMMIT;