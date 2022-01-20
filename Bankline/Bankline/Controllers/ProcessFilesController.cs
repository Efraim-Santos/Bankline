using Bankline.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Controllers
{
    public class ProcessFilesController : BaseController
    {
        public ProcessFilesController(IHostingEnvironment _appEnvironment) : base (_appEnvironment)
        {

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (!files.Any())
            {
                ViewData["Error"] = "Nenhum Arquivo foi selecionado";
                return View("Index");
            }

            ReadFiles.RemoveFiles(GetPathRootFiles());

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    if (!formFile.FileName.Contains(".ofx"))
                    {
                        ViewData["Error"] = $"A Extensão {formFile.FileName.Split('.')[1]} é invalida, favor carregar arquivo do tipo ofx!";
                        return View("Index");
                    }
                    var filePath = Path.GetTempFileName();

                    string newNameFile = $"extrato_{DateTime.Now.ToString("yyyyMMddTHHmmssZ")}.ofx";

                    string caminhoDestinoArquivo = $"{GetPathRootFiles()}\\ExtratosImportados\\{newNameFile}";

                    using (var stream = System.IO.File.Create(caminhoDestinoArquivo))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            ViewData["Success"] = "Arquivos foram incluidos com sucesso!";
            return View("Index");
        }
        [HttpGet]
        public ActionResult RemoveAllFiles()
        {
            ReadFiles.RemoveFiles(GetPathRootFiles());
            return Ok();
        }

    }
}
