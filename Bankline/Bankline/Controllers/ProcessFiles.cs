using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bankline.Models;
using Bankline.Extensions;
using Microsoft.AspNetCore.Hosting;
using System.Globalization;
using Bankline.Repository;

namespace Bankline.Controllers
{
    public class ProcessFiles : BaseController
    {
        private readonly IBankStatementRepository _bankRepository;
        public ProcessFiles(IHostingEnvironment _appEnvironment,
                            IBankStatementRepository repository) : base(_appEnvironment)
        {
            _bankRepository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult OrganizarAsContas()
        {
            var listExtract = ReadFiles.ReadAll(GetPathRootFiles());

            var selectDistinct = (from t in listExtract.Transacoes
                                  select new { t.TRNTYPE, t.DTPOSTED, t.TRNAMT, t.MEMO }).Distinct();

            var listDefaultExtract = new BankStatementDefaultViewModel();

            foreach (var transaction in selectDistinct)
            {
                listDefaultExtract.Transacoes.Add(new TransactionDefaultViewModel
                {
                    Type = transaction.TRNTYPE.Equals("DEBIT") ? "Débito" : "Crédito",
                    Date = DateTime.ParseExact(transaction.DTPOSTED.Split("[")[0], "yyyyMMddHHmmss", CultureInfo.InvariantCulture),
                    Value = Convert.ToDecimal(transaction.TRNAMT.Replace("-", "").Replace(".", ",")),
                    Description = transaction.MEMO.Trim()
                });
            }
            listDefaultExtract.Transacoes.OrderBy(t => t.Date);

            return View("Index", listDefaultExtract);
        }

        [HttpPost]
        public ActionResult CreatedDBNewRegisterOfExtractBank([FromBody] BankStatement viewModel)
        {
            var newBankStatementModel = new BankStatementModel()
            {
                Transacoes = new List<TransactionModel>()
            };

            foreach (var transaction in viewModel.Transacoes)
            {
                newBankStatementModel.Transacoes.Add(new TransactionModel
                {
                    Type = transaction.TRNTYPE,
                    Date = DateTime.Parse(transaction.DTPOSTED),
                    Value = Convert.ToDecimal(transaction.TRNAMT),
                    Description = transaction.MEMO
                });
            }

            newBankStatementModel.StatementPeriod = $"Account balance report for the period {viewModel.Transacoes.FirstOrDefault().DTPOSTED} to {viewModel.Transacoes.LastOrDefault().DTPOSTED}";

            //_bankRepository.AddNew(newBankStatementModel);

            ViewData["Sucess"] = "Arquivos foram incluidos com sucesso!";

            return Json(new { success = true, message = "Arquivos foram incluidos com sucesso!", status = 200 });
        }
    }
}
