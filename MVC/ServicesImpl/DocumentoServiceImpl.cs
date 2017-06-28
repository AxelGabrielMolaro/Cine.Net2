using MVC.DaoImpl;
using MVC.Entity;
using System.Collections.Generic;

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