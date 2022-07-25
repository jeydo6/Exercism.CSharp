internal class SecurityPassMaker
{
    public string GetDisplayName(TeamSupport support) => support switch
    {
        SecurityJunior securityJunior => securityJunior.Title,
        SecurityIntern securityIntern => securityIntern.Title,
        PoliceLiaison policeLiaison => policeLiaison.Title,
        Security security => security.Title + " Priority Personnel",
        Staff staff => staff.Title,
        _ => "Too Important for a Security Pass"
    };
}

public interface TeamSupport { string Title { get; } }

internal abstract class Staff : TeamSupport { public abstract string Title { get; } }

internal class Manager : TeamSupport { public string Title => "The Manager"; }

internal class Chairman : TeamSupport { public string Title => "The Chairman"; }

internal class Physio : Staff { public override string Title => "The Physio"; }

internal class OffensiveCoach : Staff { public override string Title => "Offensive Coach"; }

internal class GoalKeepingCoach : Staff { public override string Title => "Goal Keeping Coach"; }

internal class Security : Staff { public override string Title => "Security Team Member"; }

internal class SecurityJunior : Security { public override string Title => "Security Junior"; }

internal class SecurityIntern : Security { public override string Title => "Security Intern"; }

internal class PoliceLiaison : Security { public override string Title => "Police Liaison Officer"; }
