using Model.ForRequest;
using System.Collections.Generic;

namespace Importation
{
    public interface IImport
    {
        string GetName();

        List<LodgingModelForImport> Import();

        List<Parameter> GetParameter(); 

    }
}
