using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

internal static class Tournament
{
    private enum MatchResult
    {
        Win,
        Draw,
        Loss
    }

    private class CompetitionResult
    {
        public CompetitionResult(string team) => Team = team;

        public string Team { get; }

        public int MatchesPlayed => MatchesWon + MatchesDrawn + MatchesLost;

        public int MatchesWon { get; private set; }

        public int MatchesDrawn { get; private set; }

        public int MatchesLost { get; private set; }

        public int Points => 3 * MatchesWon + MatchesDrawn;

        public void Add(MatchResult matchResult) => _ = matchResult switch
        {
            MatchResult.Win => MatchesWon++,
            MatchResult.Draw => MatchesDrawn++,
            MatchResult.Loss => MatchesLost++,
            _ => throw new ArgumentOutOfRangeException(nameof(matchResult))
        };
    }

    private class CompetitionResultCollection
    {
        private readonly IDictionary<string, CompetitionResult> _results = new Dictionary<string, CompetitionResult>();

        public IEnumerable<CompetitionResult> Values => _results.Values;
        
        public CompetitionResultCollection Add((string Team, MatchResult Result) item)
        {
            if (!_results.ContainsKey(item.Team))
            {
                _results[item.Team] = new CompetitionResult(item.Team);
            }
            
            _results[item.Team].Add(item.Result);

            return this;
        }
    }
    
    private const string Header = "Team                           | MP |  W |  D |  L |  P";
    private const string Pattern = "{0} |  {1} |  {2} |  {3} |  {4} |  {5}";
    
    public static void Tally(Stream inStream, Stream outStream)
    {
        var results = ParseInput(inStream.Read())
            .Aggregate(new CompetitionResultCollection(), (acc, item) => acc.Add(item)).Values
            .OrderByDescending(item => item.Points)
            .ThenBy(item => item.Team)
            .ToArray();

        outStream.Write(FormatResults(results));
    }

    private static IEnumerable<(string Team, MatchResult Result)> ParseInput(string input) => input
        .Split('\n', StringSplitOptions.RemoveEmptyEntries)
        .Select(ParseLine)
        .Where(arr => arr.Any())
        .SelectMany(arr => arr);

    private static IEnumerable<(string Team, MatchResult Result)> ParseLine(string line)
    {
        var items = line.Split(';');

        var team1 = items[0];
        var team2 = items[1];
        var result = items[2];

        return result switch
        {
            "win" => new [] { (team1, MatchResult.Win), (team2, MatchResult.Loss) },
            "draw" =>new [] { (team1, MatchResult.Draw), (team2, MatchResult.Draw) },
            "loss" => new [] { (team1, MatchResult.Loss), (team2, MatchResult.Win) },
            _ => Array.Empty<(string Team, MatchResult Result)>()
        };
    }
    
    private static string FormatResult(CompetitionResult result) => string.Format(Pattern,
        result.Team.PadRight(30),
        result.MatchesPlayed,
        result.MatchesWon,
        result.MatchesDrawn,
        result.MatchesLost,
        result.Points
    );

    private static string FormatResults(IEnumerable<CompetitionResult> results)
    {
        var lines = new List<string> { Header };
        lines.AddRange(results.Select(FormatResult));

        return string.Join('\n', lines);
    }
}

internal static class StreamExtension
{
    public static string Read(this Stream stream)
    {
        var buffer = new byte[stream.Length];
        var length = stream.Read(buffer, 0, buffer.Length);

        return Encoding.UTF8.GetString(buffer[..length]);
    }

    public static void Write(this Stream stream, string str)
    {
        var buffer = Encoding.UTF8.GetBytes(str);
        stream.Write(buffer, 0, buffer.Length);
    }
}
