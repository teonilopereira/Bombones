using Bombones.Datos.Interfaces;
using Bombones.Entidades.Entidades;
using Dapper;
using System.Data.SqlClient;

namespace Bombones.Datos.Repositorios
{
    public class RepositorioFormasDeVenta:IRepositorioFormasDeVenta
    {
        public RepositorioFormasDeVenta() 
        {
        }
        public bool EstaRelacionado(int FormasDeVentaId, SqlConnection conn, SqlTransaction? tran = null)
        {
            try
            {
                string selectQuery = @"SELECT COUNT(*) 
                        FROM FormasDeVenta s
                            WHERE FormasDeVentaId=@FormasDeVentaId";
                return conn.
                    QuerySingle<int>(selectQuery, new { FormasDeVentaId }) > 0;

            }
            catch (Exception)
            {

                throw new Exception("No se pudo comprobar si existe la forma de Venta");
            }

        }
        public bool Existe(FormasDeVenta formasDeVenta, SqlConnection conn, SqlTransaction? tran = null)
        {
            try
            {
                string selectQuery = @"SELECT COUNT(*) FROM FormasDeVentas ";
                string finalQuery = string.Empty;
                string conditional = string.Empty;
                if (formasDeVenta.FormasDeVentaid == 0)
                {
                    conditional = "WHERE FormasDeVenta = @FormasDeVenta";
                }
                else
                {
                    conditional = @"WHERE FormasDeVenta = @FormasDeVenta
                            AND FormasDeVentaId<>@FormasDeVentaId";
                }
                finalQuery = string.Concat(selectQuery, conditional);
                return conn.QuerySingle<int>(finalQuery, formasDeVenta) > 0;

            }
            catch (Exception)
            {

                throw new Exception("No se pudo comprobar si existe la forma de venta");
            }
        }
        public void Agregar(FormasDeVenta formasDeVenta, SqlConnection conn, SqlTransaction? tran)
        {
            int primaryKey = -1;
            string insertQuery = @"INSERT INTO FormasDeVenta (FormasDeVenta) 
                VALUES(@FormasDeVenta); SELECT CAST(SCOPE_IDENTITY() as int)";

            primaryKey = conn.QuerySingle<int>(insertQuery, formasDeVenta, tran);
            if (primaryKey > 0)
            {
                formasDeVenta.FormasDeVentaid = primaryKey;
                return;
            }

            throw new Exception("No se pudo agregar la forma de pago");
        }
        public void Borrar(int FormasDePagoId, SqlConnection conn, SqlTransaction? tran)
        {
            string deleteQuery = @"DELETE FROM FormasDeVenta 
                WHERE FormasDeVentaId=@FormasDeVentaId";
            int registrosAfectados = conn
                .Execute(deleteQuery, new { FormasDePagoId }, tran);
            if (registrosAfectados == 0)
            {
                throw new Exception("No se pudo borrar la forma de Pago");
            }

        }
        public List<FormasDeVenta>? GetLista(SqlConnection conn, int? currentPage, int? pageSize, SqlTransaction? tran)
        {
            var selectQuery = @"SELECT FormasDeVentaId, FormasDeVenta FROM 
                FormasDeVenta ORDER BY FormasDeVenta";
            if (currentPage.HasValue && pageSize.HasValue)
            {
                var offSet = (currentPage.Value - 1) * pageSize;
                selectQuery += $" OFFSET {offSet} ROWS FETCH NEXT {pageSize.Value} ROWS ONLY";
            }
            return conn.Query<FormasDeVenta>(selectQuery).ToList();

        }
        public void Editar(FormasDeVenta formasDePago, SqlConnection conn, SqlTransaction? tran)
        {
            string updateQuery = @"UPDATE Paises SET FormasDeVenta=@FormasDeVenta 
                WHERE FormasDeVentaId=@FormasDeVentaId";

            int registrosAfectados = conn.Execute(updateQuery, formasDePago, tran);
            if (registrosAfectados == 0)
            {
                throw new Exception("No se pudo editar la forma de pago");
            }
        }

        public FormasDeVenta? GetFormaDePagoPorId(int formasDePagoId, SqlConnection conn, SqlTransaction? tran = null)
        {
            string selectQuery = @"SELECT FormasDeVentaId, FormasDeVenta, 
                 FROM FormasDeVenta 
                WHERE FormasDeVentaId=@FormasDeVentaId";
            return conn.QuerySingleOrDefault<FormasDeVenta>(
                selectQuery, new { FormasDePagoId = formasDePagoId });
        }

        public int GetCantidad(SqlConnection conn, SqlTransaction? tran)
        {
            string selectQuery = @"SELECT COUNT(*) FROM FormasDeVenta";
            return conn.ExecuteScalar<int>(selectQuery);
        }
        public int GetPaginaPorRegistro(SqlConnection conn, string formasDePago, int pageSize)
        {
            var positionQuery = @"
                    WITH FormasDePagoOrdenado AS (
                    SELECT 
                        ROW_NUMBER() OVER (ORDER BY FormasDeVenta) AS RowNum,
                        FormasDeVenta
                    FROM 
                        FormasDeVenta
                )
                SELECT 
                    RowNum 
                FROM 
                    FormasDePagoOrdenado 
                WHERE 
                    FormasDeVenta = @FormasDeVenta";

            int position = conn.ExecuteScalar<int>(positionQuery, new { FormasDePago = formasDePago });
            return (int)Math.Ceiling((decimal)position / pageSize);

        }

    }
}
