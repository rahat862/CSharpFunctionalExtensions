﻿#if NET5_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions
{
    public static partial class MaybeExtensions
    {
        public static async ValueTask<TE> Match<TE, T>(
            this Maybe<T> maybe,
            Func<T, CancellationToken, ValueTask<TE>> Some,
            Func<CancellationToken, ValueTask<TE>> None,
            CancellationToken cancellationToken = default
        )
        {
            return maybe.HasValue
                ? await Some(maybe.GetValueOrThrow(), cancellationToken)
                : await None(cancellationToken);
        }

        public static async ValueTask Match<T>(
            this Maybe<T> maybe,
            Func<T, CancellationToken, ValueTask> Some,
            Func<CancellationToken, ValueTask> None,
            CancellationToken cancellationToken = default
        )
        {
            if (maybe.HasValue)
                await Some(maybe.GetValueOrThrow(), cancellationToken);
            else
                await None(cancellationToken);
        }

        public static async ValueTask<TE> Match<TE, TKey, TValue>(
            this Maybe<KeyValuePair<TKey, TValue>> maybe,
            Func<TKey, TValue, CancellationToken, ValueTask<TE>> Some,
            Func<CancellationToken, ValueTask<TE>> None,
            CancellationToken cancellationToken = default
        )
        {
            return maybe.HasValue
                ? await Some.Invoke(
                    maybe.GetValueOrThrow().Key,
                    maybe.GetValueOrThrow().Value,
                    cancellationToken
                )
                : await None.Invoke(cancellationToken);
        }

        public static async ValueTask Match<TKey, TValue>(
            this Maybe<KeyValuePair<TKey, TValue>> maybe,
            Func<TKey, TValue, CancellationToken, ValueTask> Some,
            Func<CancellationToken, ValueTask> None,
            CancellationToken cancellationToken = default
        )
        {
            if (maybe.HasValue)
            {
                await Some.Invoke(
                    maybe.GetValueOrThrow().Key,
                    maybe.GetValueOrThrow().Value,
                    cancellationToken
                );
            }
            else
            {
                await None.Invoke(cancellationToken);
            }
        }
    }
}
#endif
