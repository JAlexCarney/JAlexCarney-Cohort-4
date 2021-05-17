-- Return agent information given a security clearance value and agency id
CREATE PROCEDURE [ClearanceAudit]
/* parameter list here in parenthesis */
(@AgencyId AS int, @SecurityClearanceId AS int)
AS
BEGIN
    Select
        AgencyAgent.BadgeId AS 'BadgeId',
        Agent.LastName + ', ' + Agent.FirstName AS 'NameLastFirst',
        Agent.DateOfBirth AS 'DateOfBirth',
        AgencyAgent.ActivationDate AS 'ActivationDate',
        AgencyAgent.DeactivationDate AS 'DeactivationDate'
    From Agent
    Inner Join AgencyAgent On AgencyAgent.AgentId = Agent.AgentId
    where AgencyAgent.AgencyId = @AgencyId
        And AgencyAgent.SecurityClearanceId = @SecurityClearanceId;
    -- Agency 1 and SecurityClearanceId = 4 Has data
END