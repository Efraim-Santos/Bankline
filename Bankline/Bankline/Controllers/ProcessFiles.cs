﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bankline.Models;
using Bankline.Extensions;
using Microsoft.AspNetCore.Hosting;
using System.Globalization;

namespace Bankline.Controllers
{
    public class ProcessFiles : BaseController
    {
        public ProcessFiles(IHostingEnvironment _appEnvironment) : base(_appEnvironment)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult OrganizarAsContas()
        {
            var listExtract = ReadFiles();

            var selectDistinct = (from t in listExtract.Transacoes
                          select new { t.TRNTYPE, t.DTPOSTED, t.TRNAMT, t.MEMO }).Distinct();

            var listDefaultExtract = new BankStatementDefaultModel();

            foreach (var transaction in selectDistinct)
            {
                listDefaultExtract.Transacoes.Add(new TransactionDefaultModel
                {
                    Type = transaction.TRNTYPE.Equals("DEBIT") ? "Débito" : "Crédito",
                    Date = DateTime.ParseExact(transaction.DTPOSTED.Split("[")[0], "yyyyMMddHHmmss", CultureInfo.InvariantCulture),
                    Valor = Convert.ToDecimal(transaction.TRNAMT.Replace("-", "").Replace(".", ",")),
                    Descricao = transaction.MEMO.Trim()
                });
            }
            listDefaultExtract.Transacoes.OrderBy(t => t.Date);
            
            return View("Index", listDefaultExtract);
        }
        private BankStatement ReadFiles()
        {
            var extract = new BankStatement();

            string line;

            string pathAllFiles = $"{GetPathRootFiles()}\\ExtratosImportados\\";

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

                        extract.Transacoes.Add(transacaoCorrente);
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

    }
}