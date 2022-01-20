using Bankline.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Extensions
{
    public static class ReadFiles
    {
        public static BankStatement ReadAll(string pathRootFiles)
        {
            var extract = new BankStatement();

            string line;

            string pathAllFiles = $"{pathRootFiles}\\ExtratosImportados\\";

            var pathArchives = Directory.GetFiles(pathAllFiles);

            foreach (var path in pathArchives)
            {
                StreamReader sr = System.IO.File.OpenText(path);

                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (line.OpenTag(Tags.Transacao))
                    {
                        var transacaoCorrente = new Transaction();

                        while (!(line = sr.ReadLine()).ClosedTag(Tags.Transacao))
                        {
                            var tagNameCurrent = GetTag.Name(line);
                            var valueTagNameCurrent = GetValueProperty(tagNameCurrent, line);
                            transacaoCorrente = SetValue.Transaction(transacaoCorrente, tagNameCurrent, valueTagNameCurrent);
                        }

                        extract.AllTransaction.Add(transacaoCorrente);
                    }
                }
                sr.Close();
            }

            return extract;
        }

        private static string GetValueProperty(string propertyName, string line)
        {
            return line.Replace($"<{propertyName}>", string.Empty);
        }

        public static void RemoveFiles(string pathRootFiles)
        {
            string pathAllFiles = $"{pathRootFiles}\\ExtratosImportados\\";

            var pathArchives = new DirectoryInfo(pathAllFiles);

            foreach (FileInfo file in pathArchives.GetFiles())
            {
                file.Delete();
            }
        }
    }
}
