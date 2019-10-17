using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesparcheBackend.BO.BlobStorage
{
    interface IBlobStorage
    {
        string SubirArchivoSoporte(string strRuta, string strNombreArchivoSistema, string strNombreArchivo, bool boolSoporte);
    }
}
