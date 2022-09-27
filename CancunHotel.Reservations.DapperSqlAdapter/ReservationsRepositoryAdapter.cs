using Dapper;
using CancunHotel.Reservations.Core.Domain.Entities;
using CancunHotel.Reservations.Core.Ports.Out;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CancunHotel.Reservations.DapperSqlAdapter
{
    public class ReservationsRepositoryAdapter : IReservationsRepository
    {
        private readonly string ConnectionString;

        public ReservationsRepositoryAdapter(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Guid> CreateAsync(DateTime startReservationPeriod,
                           DateTime endReservationPeriod,
                           Guid clientId,
                           DateTime requestDate)
        {
            const string sql = @"INSERT INTO ReservationsDb.dbo.Reservations (Id, CreatedAt, UpdatedAt, StartReservationPeriod, EndReservationPeriod, ClientId, IdcDelete) 
                                            VALUES (@Id, @CreatedAt, @UpdatedAt, @StartReservationPeriod, @EndReservationPeriod, @ClientId, @IdcDelete)";
            var parameters = new DynamicParameters();
            var key = Guid.NewGuid();
            parameters.Add("Id", key, DbType.Guid);
            parameters.Add("CreatedAt", requestDate, DbType.DateTime);
            parameters.Add("UpdatedAt", requestDate, DbType.DateTime);
            parameters.Add("StartReservationPeriod", startReservationPeriod, DbType.DateTime);
            parameters.Add("EndReservationPeriod", endReservationPeriod, DbType.DateTime);
            parameters.Add("ClientId", clientId, DbType.Guid);
            parameters.Add("IdcDelete", false, DbType.Boolean);

            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync(sql, parameters);
            return key;
        }

        public async Task<Reservation?> GetReservation(Guid reservationId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", reservationId, DbType.Guid);
            parameters.Add("IdcDelete", false, DbType.Boolean);

            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryFirstOrDefaultAsync<Reservation?>("select * from Reservations Where id = @id and IdcDelete = @IdcDelete", parameters);
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations(Guid clientId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("clientId", clientId, DbType.Guid);
            parameters.Add("IdcDelete", false, DbType.Boolean);

            const string sql = "select * from Reservations Where clientId = @clientId and IdcDelete = @IdcDelete";
            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryAsync<Reservation>(sql, parameters);
        }

        public async Task ModifyAsync(DateTime updatedAt, DateTime startReservationPeriod, DateTime endReservationPeriod, Guid id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("UpdatedAt", updatedAt, DbType.DateTime);
            parameters.Add("StartReservationPeriod", startReservationPeriod, DbType.DateTime);
            parameters.Add("EndReservationPeriod", endReservationPeriod, DbType.DateTime);
            parameters.Add("id", id, DbType.Guid);

            const string sql = @"UPDATE Reservations SET UpdatedAt=@UpdatedAt, StartReservationPeriod=@StartReservationPeriod, EndReservationPeriod=@EndReservationPeriod WHERE Id=@Id";

            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync(sql, parameters);
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations(DateTime startPeriod, DateTime endPeriod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PeriodStartAt", startPeriod, DbType.DateTime);
            parameters.Add("PeriodEndAt", endPeriod, DbType.DateTime);
            parameters.Add("IdcDelete", false, DbType.Boolean);

            const string sql = @"select * from Reservations where (StartReservationPeriod BETWEEN @PeriodStartAt AND @PeriodEndAt
                or EndReservationPeriod BETWEEN @PeriodStartAt AND @PeriodEndAt) and IdcDelete = @IdcDelete";

            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryAsync<Reservation>(sql, parameters);
        }

        public async Task DeleteAsync(Guid id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("IdcDelete", true, DbType.Boolean);
            parameters.Add("Id", id, DbType.Guid);

            const string sql = @"UPDATE Reservations SET IdcDelete = @IdcDelete WHERE Id=@Id";

            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync(sql, parameters);
        }
    }
}