using System.Collections.Generic;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Packaging;

namespace Cake.APT.Module
{
    public interface IAPTContentResolver
    {
         IReadOnlyCollection<IFile> GetFiles(PackageReference package, PackageType type);
    }
}