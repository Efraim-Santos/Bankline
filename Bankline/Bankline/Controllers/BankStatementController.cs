using Bankline.Extensions;
using Bankline.Models;
using Bankline.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Controllers
{
    public class BankStatementController : BaseController
    {
        private readonly IBankStatementRepository _bankRepository;
        public BankStatementController(IHostingEnvironment _appEnvironment,
                            IBankStatementRepository repository) : base(_appEnvironment)
        {
            _bankRepository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult OrganizedExtracts()
        {
            var listExtract = ReadFiles.ReadAll(GetPathRootFiles());

            if (!listExtract.AllTransaction.Any())
            {
                ViewData["Error"] = "Nenhum arquivo foi adicionado!";
                return View("Index");
            }

            var selectDistinct = (from t in listExtract.AllTransaction
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
                Transaction = new List<TransactionModel>()
            };

            foreach (var transaction in viewModel.AllTransaction)
            {
                newBankStatementModel.Transaction.Add(new TransactionModel
                {
                    Type = transaction.TRNTYPE,
                    Date = DateTime.Parse(transaction.DTPOSTED),
                    Value = Convert.ToDecimal(transaction.TRNAMT),
                    Description = transaction.MEMO
                });
            }

            newBankStatementModel.StatementPeriod = $"Account balance report for the period {viewModel.AllTransaction.FirstOrDefault().DTPOSTED} to {viewModel.AllTransaction.LastOrDefault().DTPOSTED}";

            _bankRepository.AddNew(newBankStatementModel);

            return Json(new { success = true, message = "Arquivos foram incluidos com sucesso!", status = 200 });
        }

        [HttpGet]
        public async Task<ActionResult> FetchSavedExtracts()
        {
            var listStatmenteBanks = new AllStatementBankSavedViewModel();

            listStatmenteBanks.ListBankStatements = await _bankRepository.GetAll();

            return View(listStatmenteBanks);
        }

        [HttpPost]
        public async Task<ActionResult> SearchExtractById(BankStatementModel idExtract)
        {
            if (idExtract == null)
            {
                ViewData["Error"] = "Nenhum extrato foi passado";
                return View();
            }

            var bankStatement = await _bankRepository.GetBankStatementByID(idExtract.Id);

            if (bankStatement == null) return NotFound();

            var listTransaction = new BankStatementDefaultViewModel();

            foreach (var transaction in bankStatement.Transaction)
            {
                listTransaction.Transacoes.Add(new TransactionDefaultViewModel
                {
                    Type = transaction.Type,
                    Date = transaction.Date,
                    Value = transaction.Value,
                    Description = transaction.Description
                });
            }
            return View(listTransaction);
        }
    }
}
