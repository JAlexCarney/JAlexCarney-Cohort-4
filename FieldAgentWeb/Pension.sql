CREATE PROCEDURE [Pension] 
/* parameter list here in parenthesis */
(@AgencyId AS int)
AS
BEGIN
    -- Return agent information for only retired agents (security clearance retired)
    Select 
        Agency.LongName AS 'AgencyName',
        AgencyAgent.BadgeId AS 'BadgeId',
        Agent.LastName + ', ' + Agent.FirstName AS 'NameLastFirst',
        Agent.DateOfBirth AS 'DateOfBirth',
        AgencyAgent.DeactivationDate AS 'DeactivationDate'
    From Agency
    Inner Join AgencyAgent On AgencyAgent.AgencyId = Agency.AgencyId
    Inner Join SecurityClearance On SecurityClearance.SecurityClearanceId = AgencyAgent.SecurityClearanceId
    Inner Join Agent On Agent.AgentId = AgencyAgent.AgentId
    Where Agency.AgencyId = @AgencyId AND SecurityClearance.SecurityClearanceName = 'Retired';
    -- Agency 5 has seeded data
END