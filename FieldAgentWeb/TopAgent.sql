CREATE PROCEDURE [TopAgents]
/* parameter list here in parenthesis */
AS
BEGIN
-- Returns top three agents by number of missions completed
Select
    A.LastName + ', ' + A.FirstName AS 'NameLastFirst',
    A.DateOfBirth AS 'DateOfBirth',
    (SELECT Count(*) From Mission
        INNER JOIN MissionAgent ON MissionAgent.MissionId = Mission.MissionId
        Where AgentId = A.AgentId
        AND Mission.ActualEndDate IS NOT NULL
        Group By MissionAgent.AgentId) AS 'CompletedMissionCount'
From Agent A
Order By (SELECT Count(*) From Mission
         INNER JOIN MissionAgent ON MissionAgent.MissionId = Mission.MissionId
         Where AgentId = A.AgentId
         AND Mission.ActualEndDate IS NOT NULL
         Group By MissionAgent.AgentId) Desc;
END
        