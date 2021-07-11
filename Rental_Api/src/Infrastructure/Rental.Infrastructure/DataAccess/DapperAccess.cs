
using CSharpFunctionalExtensions;
using Dapper;
using Rental.Domain.Enums;
using Rental.Domain.Infrastructure;
using Rental.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.Infrastructure.Repositories
{
    //public class DapperAccess : IDataAccess
    //{
    //    private readonly ConnectionOptions _connections;
    //    private string _currentConectionString;

    //    public DapperAccess(IOptions<ConnectionOptions> connections)
    //    {
    //        _connections = connections.Value;
    //        _currentConectionString = _connections.LimitPre;
    //    }
    //    protected IDbConnection SqlConnection => new SqlConnection(_currentConectionString);


    //    public void ChangeDataBase(DbNamesEnum newDatabase)
    //    => _currentConectionString = newDatabase switch
    //    {
    //        DbNamesEnum.ASIDB => _connections.Asidb,
    //        DbNamesEnum.LIMITPRE => _connections.LimitPre,
    //        DbNamesEnum.LIMITLIVE => _connections.LimitLive,
    //        _ => _connections.LimitPre
    //    };


    //    /// <summary>
    //    /// Return an IEnumerable (multiple records)
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <param name="sql"></param>
    //    /// <param name="parameters"></param>
    //    /// <param name="commandType"></param>
    //    /// <returns></returns>
    //    public Result<Maybe<IEnumerable<T>>> GetRecords<T>(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure)
    //    {
    //        using var conn = SqlConnection;
    //        var result = conn.Query<T>(sql: sql, param: parameters, commandType: commandType);
    //        if (!result.Any()) return Result.Success(Maybe<IEnumerable<T>>.None);
    //        return Result.Success(Maybe<IEnumerable<T>>.From(result));
    //    }

    //    /// <summary>
    //    ///
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <param name="sql"></param>
    //    /// <param name="parameters"></param>
    //    /// <param name="commandType"></param>
    //    /// <returns></returns>
    //    public async Task<Result<Maybe<IEnumerable<T>>>> GetRecordsAsync<T>(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure)
    //    {
    //        using var conn = SqlConnection;
    //        var result = await conn.QueryAsync<T>(sql: sql, param: parameters, commandType: commandType);
    //        if (!result.Any()) return Result.Success(Maybe<IEnumerable<T>>.None);
    //        return Result.Success(Maybe<IEnumerable<T>>.From(result));
    //    }

    //    /// <summary>
    //    /// Return a Single record or null if not found
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <param name="sql"></param>
    //    /// <param name="parameters"></param>
    //    /// <param name="commandType"></param>
    //    /// <returns></returns>
    //    public async Task<Result<Maybe<T>>> GetRecordAsync<T>(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure)
    //    {
    //        using var conn = SqlConnection;
    //        return Result.Success(Maybe<T>.From(await conn.QueryFirstOrDefaultAsync<T>(sql: sql, param: parameters, commandType: commandType)));
    //    }

    //    /// <summary>
    //    ///  Return a Single record or null if not found
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <param name="sql"></param>
    //    /// <param name="parameters"></param>
    //    /// <param name="commandType"></param>
    //    /// <returns></returns>
    //    public Result<Maybe<T>> GetRecord<T>(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure)
    //    {
    //        using var conn = SqlConnection;
    //        return Result.Success(Maybe<T>.From(conn.QueryFirstOrDefault<T>(sql: sql, param: parameters, commandType: commandType)));
    //    }

    //    public Result Execute(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure)
    //    {
    //        using var conn = SqlConnection;
    //        conn.Execute(sql, parameters, commandType: commandType);
    //        return Result.Success();
    //    }
    //    public async Task<Result> ExecuteAsync(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure)
    //    {
    //        using var conn = SqlConnection;
    //        await conn.ExecuteAsync(sql, parameters, commandType: commandType);
    //        return Result.Success();
    //    }

    //    public Result ExecuteMany(params (string storeProcedureName, object parameters)[] operationsToExeute)
    //    {
    //        using var cn = SqlConnection;
    //        cn.Open();
    //        using var transaction = cn.BeginTransaction();
    //        try
    //        {
    //            foreach (var operations in operationsToExeute)
    //            {
    //                cn.Execute(operations.storeProcedureName, param: operations.parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
    //            }
    //            transaction.Commit();
    //            return Result.Success();
    //        }
    //        catch (Exception e)
    //        {
    //            transaction.Rollback();
    //            return Result.Failure(e.Message);
    //        }
    //        finally
    //        {
    //            cn.Close();
    //        }
    //    }

    //    public async Task<Result> ExecuteManyAsync(params (string storeProcedureName, object parameters)[] operationsToExeute)
    //    {
    //        using var cn = SqlConnection;
    //        cn.Open();
    //        using var transaction = cn.BeginTransaction();
    //        try
    //        {
    //            foreach (var operations in operationsToExeute)
    //            {
    //                await cn.ExecuteAsync(operations.storeProcedureName, param: operations.parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
    //            }
    //            transaction.Commit();
    //            return Result.Success();
    //        }
    //        catch (Exception e)
    //        {
    //            transaction.Rollback();
    //            return Result.Failure(e.Message);
    //        }
    //        finally
    //        {
    //            cn.Close();
    //        }
    //    }
    //}
}

