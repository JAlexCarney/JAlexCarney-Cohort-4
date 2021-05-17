INSERT INTO SecurityClearance (SecurityClearanceName)
    VALUES 
    ('None'),
    ('Retired'),
    ('Secret'),
    ('Top Secret'),
    ('Black Ops');

Select * From SecurityClearance;

Insert Into Agent (FirstName, LastName, DateOfBirth, Height)
    VALUES
    ('Bertie','Knoles','8/12/2019',4.8),
    ('Meridel', 'Suatt', '1/25/1949', 5.3),
    ('Wyatan', 'Proudlove', '11/8/1914', 4.2),
    ('Barry', 'Simonyi', '2/9/1965', 6.6),
    ('Eve', 'Arro', '5/21/1905', 6.7),
    ('Julieta', 'Tranckle', '3/18/1930', 4.4),
    ('Andreas', 'Caddick', '2/16/1919', 4.9),
    ('Christos', 'Mantrip', '10/5/1988', 6.7),
    ('Kaitlin', 'Ferrarotti', '10/10/2000', 5.1),
    ('Evangelina', 'Donat', '3/7/2007', 6.1),
    ('Ardella', 'Mariotte', '5/17/1959', 6.7);

Select * From Agent;

insert into Alias (AgentId, AliasName, InterpolId, Persona) values (8, 'Orran Culmer', 'ff228bbb-2477-455a-bbb4-8b1782cdb0ac', 'encompassing');
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (2, 'Addy Thowless', 'a426d8e5-44fc-41c4-9afc-6faa1b751e75', 'application');
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (2, 'Seth Mayes', 'b34f48e0-07e4-4a73-9592-02fb23622a6f', 'installation');
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (1, 'Suki Ebdon', 'c32c3228-2e2d-493c-b99e-c8441bde0bd4', 'initiative');
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (2, 'Ernestus Rasper', '3f7dd986-53b4-446f-83fd-01dadcaceef4', 'adapter');
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (10, 'Randee Cescon', '97fb640c-8366-46f5-a66c-f5a2402dea8a', 'time-frame');
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (3, 'Leyla Santry', 'dec7e84b-0240-493d-a423-9f6e6db3e256', 'implementation');
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (4, 'Gwenore Garvie', '5f2d9e91-0cd8-488e-9e93-7d596b00a435', 'Robust');
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (1, 'Bink D''eath', '835449f0-de16-435a-b903-11bf1ae0c48f', 'Object-based');
insert into Alias (AgentId, AliasName, InterpolId, Persona) values (5, 'Hinze Colloby', 'd6f6dbfc-888c-4813-8c6b-f1b15315bffc', 'Optional');

Select * From Alias;

insert into Agency (ShortName, LongName) values ('Gigaclub', 'Bison bison');
insert into Agency (ShortName, LongName) values ('Thoughtsphere', 'Bugeranus caruncalatus');
insert into Agency (ShortName, LongName) values ('Geba', 'Crotaphytus collaris');
insert into Agency (ShortName, LongName) values ('Shufflebeat', 'Geochelone radiata');
insert into Agency (ShortName, LongName) values ('Realbridge', 'Pseudalopex gymnocercus');
insert into Agency (ShortName, LongName) values ('Shuffletag', 'Phalaropus fulicarius');
insert into Agency (ShortName, LongName) values ('Zoomdog', 'Sciurus niger');
insert into Agency (ShortName, LongName) values ('Meevee', 'Vombatus ursinus');
insert into Agency (ShortName, LongName) values ('Youspan', 'Cervus elaphus');
insert into Agency (ShortName, LongName) values ('Janyx', 'Bassariscus astutus');

Select * From Agency;

insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (6, 5, 2, 'e08af0bb-0f9a-4941-be86-fb0de0e9c60c', '11/30/2020', '8/18/2020', 0);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (5, 3, 5, '2c20b2c4-112f-4c62-8b43-451e6415b5d0', '5/8/2021', null, 1);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (5, 2, 2, '3cb52afb-3bd0-4214-b9db-697b451f09b2', '6/13/2020', '11/8/2020', 0);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (8, 2, 2, '31eaef68-f7b9-4991-a247-59f866ebb8e8', '9/18/2020', '9/1/2020', 0);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (10, 8, 2, 'c684ef05-355b-4946-beec-525ed037b641', '1/15/2021', '5/24/2020', 0);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (5, 1, 2, 'f7723856-4a04-43ea-8287-91be1295a1f2', '1/26/2021', '7/9/2020', 0);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (1, 5, 4, '2418aee0-31fe-4ac3-aec4-c9fd606fef9f', '6/24/2020', null, 1);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (8, 10, 3, '1c6d03ae-2662-48c7-b83d-ae258a5fa5f1', '2/19/2021', null, 1);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (8, 5, 5, 'a1bc1fae-dfd2-42ac-a1d2-7bd8334b4955', '10/4/2020', null, 1);
insert into AgencyAgent (AgencyId, AgentId, SecurityClearanceId, BadgeId, ActivationDate, DeactivationDate, IsActive) values (1, 1, 5, '3e427d40-76a2-429f-a5c5-d2d125377969', '12/7/2020', null, 1);

Select * From AgencyAgent;

insert into Location (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (2, 'Thiel Inc', '812 David Crossing', null, 'Am Djarass', 'XAF', 'TD');
insert into Location (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (8, 'Aufderhar, Beer and Jakubowski', '776 Prentice Parkway', null, 'Tapas', 'PHP', 'PH');
insert into Location (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (6, 'Koelpin and Sons', '91 Fair Oaks Drive', null, 'Alenquer', 'BRL', 'BR');
insert into Location (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (8, 'Von-Stroman', '501 Reinke Pass', null, 'Busilak', 'PHP', 'PH');
insert into Location (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (5, 'Carter, Stanton and Herzog', '30455 Veith Crossing', null, 'Luoqiao', 'CNY', 'CN');
insert into Location (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (4, 'Graham, Johns and Hintz', '09073 Summit Drive', '00 Hagan Court', 'Jiangzhang', 'CNY', 'CN');
insert into Location (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (6, 'Kirlin Group', '97 Elgar Alley', null, 'Masaling', 'PHP', 'PH');
insert into Location (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (8, 'Brekke-Schroeder', '680 Quincy Avenue', null, 'Cincinnati', 'USD', 'US');
insert into Location (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (2, 'Kohler and Sons', '7 Katie Court', null, 'Wutun', 'CNY', 'CN');
insert into Location (AgencyId, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (7, 'Feest-McCullough', '0 Algoma Place', null, 'Sangat', 'PHP', 'PH');

Select * From [Location];

insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (9, 'Lueilwitz, Pagac and Ratke', '3/31/2020', '9/27/2020', null, null, null);
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (2, 'Bauch, Beahan and Gottlieb', '10/19/2020', '6/20/2021', '7/23/2020', 467.55, 'dui vel nisl duis ac nibh fusce lacus purus aliquet at feugiat non pretium quis');
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (6, 'Torp-Gerhold', '8/26/2018', '8/7/2022', null, null, null);
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (8, 'Beer-Wolff', '6/30/2020', '9/9/2021', '10/6/2020', 603.86, 'tincidunt eget tempus vel pede morbi porttitor');
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (6, 'Jones-Ferry', '9/9/2018', '7/12/2020', '4/23/2021', 820.01, 'molestie hendrerit at vulputate vitae nisl aenean lectus pellentesque eget nunc donec quis orci eget orci');
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (7, 'Nolan, Lynch and Crona', '8/18/2018', '10/23/2021', '8/15/2022', 760.14, 'lacinia sapien quis libero nullam sit');
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (7, 'Osinski LLC', '10/19/2019', '10/15/2020', null, null, null);
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (2, 'Morissette Inc', '8/25/2018', '7/7/2020', null, null, null);
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (9, 'Crona, Mertz and Price', '8/1/2020', '1/27/2023', '5/14/2022', 961.76, 'varius integer ac leo pellentesque ultrices mattis odio donec vitae nisi nam ultrices');
insert into Mission (AgencyId, CodeName, StartDate, ProjectedEndDate, ActualEndDate, OperationalCost, Notes) values (7, 'Lubowitz-Schaden', '12/6/2018', '6/15/2020', null, null, null);

Select * From Mission;

insert into MissionAgent (MissionId, AgentId) values (2, 10);
insert into MissionAgent (MissionId, AgentId) values (9, 4);
insert into MissionAgent (MissionId, AgentId) values (8, 1);
insert into MissionAgent (MissionId, AgentId) values (2, 6);
insert into MissionAgent (MissionId, AgentId) values (9, 9);
insert into MissionAgent (MissionId, AgentId) values (5, 1);
insert into MissionAgent (MissionId, AgentId) values (3, 5);
insert into MissionAgent (MissionId, AgentId) values (6, 3);
insert into MissionAgent (MissionId, AgentId) values (7, 5);
insert into MissionAgent (MissionId, AgentId) values (4, 2);

Select * From MissionAgent;