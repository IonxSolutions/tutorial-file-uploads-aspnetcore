using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Web.Models;

namespace Web.Controllers;

public class ScanController : Controller
{
    private readonly IHttpClientFactory clientFactory;
    private readonly JsonSerializerOptions jsonOptions;

    public ScanController(IHttpClientFactory clientFactory, IOptions<JsonOptions> jsonOptions)
    {
        this.clientFactory = clientFactory;
        this.jsonOptions = jsonOptions.Value.JsonSerializerOptions;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(UploadModel model)
    {
        using var client = this.clientFactory.CreateClient(nameof(HomeController));

        using var formData = new MultipartFormDataContent();
        formData.Add(new StreamContent(model.File.OpenReadStream()), "file", model.File.FileName);

        using var response = await client.PostAsync("malware/scan/file", formData);
        response.EnsureSuccessStatusCode();

        var apiModel = await response.Content.ReadFromJsonAsync<ApiResultModel>(this.jsonOptions);

        var result = new ResultModel(model.File.FileName, model.File.Length, model.File.ContentType, apiModel!.Status, apiModel.Signals);
        return View("Result", result);
    }
}
