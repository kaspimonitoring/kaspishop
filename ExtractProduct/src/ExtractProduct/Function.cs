using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Application;
using Domain;
using Refit;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ExtractProduct
{
    public class Function
    {
        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public Function()
        {

        }


        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
        /// to respond to SQS messages.
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
        {
            foreach (var message in evnt.Records)
            {
                await ProcessMessageAsync(message, context);
            }
        }

        private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
        {
            context.Logger.LogInformation($"Processed message {message.Body}");



            // TODO: Do interesting work based on the new message

            var externalId = message.Body;

            var kaspiClient = RestService.For<IKaspiClient>("https://kaspi.kz");
            var productRequest = await kaspiClient.GetProduct(externalId);

            if (!productRequest.IsSuccessStatusCode)
            {
                context.Logger.LogInformation($"productRequest status code: {productRequest.StatusCode}");
                return;
            }

            var productJson = await productRequest.Content.ReadAsStringAsync();
            var hash = HashGenerator.Generate(productJson);

            ProductOriginal productOriginal = new(externalId, productJson, hash);



            await Task.CompletedTask;
        }
    }
}