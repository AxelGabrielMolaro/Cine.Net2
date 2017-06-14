using MVC.DaoImpl;
using MVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ServicesImpl
{
    public class DocumentoServiceImpl
    {
        DocumentoDaoImpl documentoDao = new DocumentoDaoImpl();

        public void grabarDocumento(TiposDocumentos tipoDocumento)
        {
            documentoDao.grabarDocumento(tipoDocumento);
        }

        public TiposDocumentos getTipoDocumentoPorId(int IdTipoP)
        {

            return documentoDao.getTipoDocumentoPorId(IdTipoP);
        }

        public List<TiposDocumentos> getListadoTipoDocumento()
        {
            return documentoDao.getListadoTipoDocumento();
        }
    }
}