using System;
using Avalonia.Reactive;

namespace Avalonia.Controls.Helpers;

/// <summary>
/// Provides common observable methods as a replacement for the Rx framework.
/// </summary>
internal static class ObservableHelpers
{
    internal static IObservable<TSource> Create<TSource>(Func<IObserver<TSource>, IDisposable> subscribe)
    {
        return new CreateWithDisposableObservable<TSource>(subscribe);
    }

    internal static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> action)
    {
        return source.Subscribe(new AnonymousObserver<T>(action));
    }

    internal static IObservable<T> Skip<T>(this IObservable<T> source, int skipCount)
    {
        if (skipCount <= 0)
        {
            throw new ArgumentException("Skip count must be bigger than zero", nameof(skipCount));
        }

        return Create<T>(obs =>
        {
            var remaining = skipCount;
            return source.Subscribe(new AnonymousObserver<T>(
                input =>
                {
                    if (remaining <= 0)
                    {
                        obs.OnNext(input);
                    }
                    else
                    {
                        remaining--;
                    }
                }, obs.OnError, obs.OnCompleted));
        });
    }

    private sealed class CreateWithDisposableObservable<TSource> : IObservable<TSource>
    {
        private readonly Func<IObserver<TSource>, IDisposable> _subscribe;

        public CreateWithDisposableObservable(Func<IObserver<TSource>, IDisposable> subscribe)
        {
            _subscribe = subscribe;
        }

        public IDisposable Subscribe(IObserver<TSource> observer)
        {
            return _subscribe(observer);
        }
    }
}