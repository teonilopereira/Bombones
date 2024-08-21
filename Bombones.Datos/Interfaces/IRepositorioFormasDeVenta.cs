using Bombones.Entidades.Entidades;
using System.Data.SqlClient;

namespace Bombones.Datos.Interfaces
{
    public interface IRepositorioFormasDeVenta
    {
        int GetCantidad(SqlConnection conn, SqlTransaction? tran = null);
        void Agregar(FormasDeVenta formasDeVenta, SqlConnection conn, SqlTransaction? tran = null);
        void Borrar(int FormasDeVentaId, SqlConnection conn, SqlTransaction? tran = null);
        void Editar(FormasDeVenta formasDeVenta, SqlConnection conn, SqlTransaction? tran = null);
        bool EstaRelacionado(int FormasDePagoId, SqlConnection conn, SqlTransaction? tran = null);
        bool Existe(FormasDeVenta formasDeVenta, SqlConnection conn, SqlTransaction? tran = null);
        List<FormasDeVenta>? GetLista(SqlConnection conn, int? currentPage, int? pageSize, SqlTransaction? tran = null);
        FormasDeVenta? GetFormaDePagoPorId(int FormasDeVentaId, SqlConnection conn, SqlTransaction? tran = null);
        int GetPaginaPorRegistro(SqlConnection conn, string FormasDeVenta, int totalPages);
    }
}
