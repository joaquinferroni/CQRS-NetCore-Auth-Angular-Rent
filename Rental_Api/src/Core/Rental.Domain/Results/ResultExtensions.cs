using CSharpFunctionalExtensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.Domain.Results
{
    public static class ResultExtensions
    {
        public static Result<K> Select<T, K>(this Result<T> result, Func<T, K> selector)
        {
            return result.Map(selector);
        }

        public static Result<K, E> Select<T, E, K>(this Result<T, E> result, Func<T, K> selector)
        {
            return result.Map(selector);
        }
        
        public static Result<T> Where<T>(this Result<T> result, Func<T, bool> predicate)
        {
            if (result.IsFailure)
                return result;

            if (predicate(result.Value))
                return result;

            return Result.Failure<T>("Clausule where is wrong");
        }

        public static Result<T, K> Where<T, K>(this Result<T, K> result, Func<T, bool> predicate)
        {
            if (result.IsFailure)
                return result;

            if (predicate(result.Value))
                return result;

            return Result.Failure<T, K>(default);
        }
        public static async Task<Result<T>> Where<T>(this Task<Result<T>> resultTask, Func<T, bool> predicate)
        {
            Result<T> result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return result.Where(predicate);
        }

        public static async Task<Result<T>> Where<T>(this Result<T> result, Func<T, Task<bool>> predicate)
        {
            if (result.IsFailure)
                return result;

            if (await predicate(result.Value).ConfigureAwait(Result.DefaultConfigureAwait))
                return result;

            return Result.Failure<T>("Clausule where is wrong");
        }

        public static async Task<Result<T>> Where<T>(this Task<Result<T>> resultTask, Func<T, Task<bool>> predicate)
        {
            Result<T> result = await resultTask.ConfigureAwait(Result.DefaultConfigureAwait);
            return await result.Where(predicate).ConfigureAwait(Result.DefaultConfigureAwait);

        }

        //public static Result<IEnumerable<T1>> Transverse<T, T1>(this IEnumerable<Result<T>> resultTask, Func<T, T1> predicate)
        //{
        //    var result = new List<T1>();
        //    foreach (var r in resultTask)
        //    {
        //        var mapped = r.Map(predicate);
        //        if (mapped.IsFailure) return Result.Failure<IEnumerable<T1>>(mapped.Error);
        //        result.Add(mapped.Value);
        //    }
        //    return result;
        //}

       // public static Result<IEnumerable<R>> Traverse<T, R>(this IEnumerable<Result<T>> ts,Func<Result<T>, Result<R>> func)
       //=> ts.Aggregate(Result.Success(Enumerable.Empty<R>()),
       //                (optRs,
       //                 t) => from rs in optRs
       //                       from r in func(t)
       //                       select rs.Append(r));

        public static Result<IEnumerable<R>> Traverse<T, R>(this IEnumerable<T> ts, Func<T, Result<R>> func)
          => ts.Aggregate(Result.Success(Enumerable.Empty<R>()),
                          (optRs,
                           t) => from rs in optRs
                                 from r in func(t)
                                 select rs.Append(r));

    }
}
