// See https://aka.ms/new-console-template for more information
using Application;
using Domain;
using ExtractProduct;
using Refit;

Console.WriteLine("Hello, World!");


var externalId = "106363319";

var kaspiClient = RestService.For<IKaspiClient>("https://kaspi.kz");
var productRequest = await kaspiClient.GetProduct(externalId);

if (!productRequest.IsSuccessStatusCode)
{
    Console.WriteLine($"productRequest status code: {productRequest.StatusCode}");
    return;
}

var productJson = await productRequest.Content.ReadAsStringAsync();
var hash = HashGenerator.Generate(productJson);

ProductOriginal productOriginal = new(externalId, productJson, hash);