using Domain;
using Model.ForRequest;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Importation
{
    public interface IImport
    {
        string GetName();

        List<LodgingModelForImport> Import();

        List<Parameter> GetParameter(); 

    }
}
