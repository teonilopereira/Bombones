using Bombones.Datos.Interfaces;
using Bombones.Entidades.Entidades;
using Bombones.Servicios.Intefaces;
using System.Data.SqlClient;

namespace Bombones.Servicios.Servicios
{
    public class ServiciosFormasDeVenta : IServiciosFormasDeVenta
    {
        private readonly IRepositorioFormasDeVenta? _repositorio;
        private readonly string _cadena;
        public ServiciosFormasDeVenta(IRepositorioFormasDeVenta? repositorio, string cadena)
        {
            _repositorio = repositorio ?? throw new ApplicationException("Dependencias no cargadas!!!"); ;
            _cadena = cadena;
        }

        public void Borrar(int FormasDeVentaId)
        {
            using (var conn = new SqlConnection(_cadena))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        _repositorio!.Borrar(FormasDeVentaId, conn, tran);
                        tran.Commit();
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }
        public bool EstaRelacionado(int FormasDeVentaId)
        {

            using (var conn = new SqlConnection(_cadena))
            {
                conn.Open();
                return _repositorio!.EstaRelacionado(FormasDeVentaId, conn);
            }
        }

        public bool Existe(FormasDeVenta formasDeVenta)
        {
            using (var conn = new SqlConnection(_cadena))
            {
                conn.Open();
                return _repositorio!.Existe(formasDeVenta, conn);
            }
        }
        public FormasDeVenta? GetFormasDeVentaPorId(int formasDeVentaId)
        {

            using (var conn = new SqlConnection(_cadena))
            {
                return _repositorio!.GetFormaDePagoPorId(formasDeVentaId, conn);
            }
        }

        public List<FormasDeVenta>? GetLista(int? currentPage, int? pageSize)
        {
            using (var conn = new SqlConnection(_cadena))
            {
                conn.Open();
                return _repositorio!.GetLista(conn, currentPage, pageSize);
            }
        }


        public void Guardar(FormasDeVenta formasDeVenta)
        {

            using (var conn = new SqlConnection(_cadena))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        if (formasDeVenta.FormasDeVentaid == 0)
                        {
                            _repositorio!.Agregar(formasDeVenta, conn, tran);
                        }
                        else
                        {
                            _repositorio!.Editar(formasDeVenta, conn, tran);
                        }

                        tran.Commit();
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }


        public int GetCantidad()
        {
            using (var conn = new SqlConnection(_cadena))
            {
                conn.Open();
                return _repositorio!.GetCantidad(conn);
            }

        }
        public int GetPaginaPorRegistro(string formasDeVenta, int pageSize)
        {

            using (var conn = new SqlConnection(_cadena))
            {
                conn.Open();
                return _repositorio!.GetPaginaPorRegistro(conn, formasDeVenta, pageSize);
            }
        }




    }
}
