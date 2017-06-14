using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Entity;
using MVC.Manager;

namespace MVC.DaoImpl
{
    public class DocumentoDaoImpl
    {

        RepositorioManager repositorioManager = new RepositorioManager();

        public void grabarDocumento(TiposDocumentos tipoDocumento) {
            repositorioManager.ctx.TiposDocumentos.Add(tipoDocumento);
            repositorioManager.ctx.SaveChanges();
        }

        public TiposDocumentos getTipoDocumentoPorId(int IdTipoP)
        {
            TiposDocumentos tipoDocumento = repositorioManager.ctx.TiposDocumentos.OrderByDescending(o => o.IdTipoDocumento == IdTipoP).FirstOrDefault(); ;
            return tipoDocumento;
        }

        public List<TiposDocumentos> getListadoTipoDocumento()
        {
            var listado = repositorioManager.ctx.TiposDocumentos;
            List<TiposDocumentos> lista = listado.ToList();
            return lista;
        }
    }
}