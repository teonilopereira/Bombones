using Bombones.Entidades.Entidades;

namespace Bombones.Servicios.Intefaces
{
    public interface IServiciosFormasDeVenta
    {
        void Borrar(int FormasDeVentaId);
        bool EstaRelacionado(int FormasDeVentaId);
        bool Existe(FormasDeVenta formasDeVenta);
        List<FormasDeVenta>? GetLista(int? currentPage, int? pageSize);
        void Guardar(FormasDeVenta formasDeVenta);
        FormasDeVenta? GetFormasDeVentaPorId(int formasDeVentaId);
        int GetCantidad();

        int GetPaginaPorRegistro(string FormasDeVenta, int pageSize);
        List<FormasDeVenta>? GetLista();
    }
}
