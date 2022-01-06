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
    public class UploadFilesController : BaseController
    {
        public UploadFilesController(IHostingEnvironment _appEnvironment) : base (_appEnvironment)
        {

        }
        public IActionResult Index()
            {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendFiles(List<IFormFile> files)
        {
            if (!files.Any())
            {
                ViewData["Error"] = "Nenhum Arquivo foi selecionado";
                return View("Index");
            }
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
            ViewData["Sucess"] = "Arquivos foram incluidos com sucesso!";
            return View("Index");
        }

    }
}
