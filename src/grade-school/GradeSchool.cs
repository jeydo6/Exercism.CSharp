using System;
using System.Collections.Generic;

internal class GradeSchool
{
    private readonly IDictionary<int, SortedList<string, string>> _studentGrades = new SortedDictionary<int, SortedList<string, string>>();
    
    public void Add(string student, int grade)
    {
        if (!_studentGrades.ContainsKey(grade))
        {
            _studentGrades[grade] = new SortedList<string, string>();
        }
        
        _studentGrades[grade].Add(student, student);
    }

    public IEnumerable<string> Roster()
    {
        var result = new List<string>();
        foreach (var key in _studentGrades.Keys)
        {
            result.AddRange(_studentGrades[key].Values);
        }

        return result.ToArray();
    }

    public IEnumerable<string> Grade(int grade) =>
        _studentGrades.ContainsKey(grade) ?
        _studentGrades[grade].Values :
        Array.Empty<string>();
}
