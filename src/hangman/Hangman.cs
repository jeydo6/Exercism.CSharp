using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;

internal class HangmanState
{
    public string MaskedWord { get; }
    
    public ImmutableHashSet<char> GuessedChars { get; }
    
    public int RemainingGuesses { get; }

    public HangmanState(string maskedWord, ImmutableHashSet<char> guessedChars, int remainingGuesses)
    {
        MaskedWord = maskedWord;
        GuessedChars = guessedChars;
        RemainingGuesses = remainingGuesses;
    }
}

internal class TooManyGuessesException : Exception
{
}

internal class Hangman
{
    private const int RemainingGuesses = 9;
    
    public IObservable<HangmanState> StateObservable { get; }
    public IObserver<char> GuessObserver { get; }
  
    public Hangman(string word)
    {
        var stateObservable = new BehaviorSubject<HangmanState>(new HangmanState(
            GetMaskedWord(word, new HashSet<char>()),
            ImmutableHashSet<char>.Empty,
            RemainingGuesses)
        );

        StateObservable = stateObservable;
        GuessObserver = Observer.Create<char>(ch =>
        {
            var guessedChars = new HashSet<char>(stateObservable.Value.GuessedChars);
            var isGuess = !guessedChars.Contains(ch) && word.Contains(ch);
            guessedChars.Add(ch);
            
            var maskedWord = GetMaskedWord(word, guessedChars);
            if (maskedWord == word)
            {
                stateObservable.OnCompleted();
            }
            else if (stateObservable.Value.RemainingGuesses < 1)
            {
                stateObservable.OnError(new TooManyGuessesException());
            }
            else
            {
                stateObservable.OnNext(new HangmanState(
                    maskedWord,
                    guessedChars.ToImmutableHashSet(),
                    isGuess ? stateObservable.Value.RemainingGuesses : stateObservable.Value.RemainingGuesses - 1)
                );
            }
        });
    }

    private static string GetMaskedWord(string word, ICollection<char> guessedChars)
    {
        var result = new char[word.Length];
        for (var i = 0; i < word.Length; i++)
        {
            result[i] = guessedChars.Contains(word[i]) ? word[i] : '_';
        }

        return new string(result);
    }
}
