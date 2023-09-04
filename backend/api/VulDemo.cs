using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security;
using System.Security.Permissions;

namespace YourNamespace
{
    public static class VulDemo
    {
        [FunctionName("VulDemo")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string fileName = req.Query["fileName"];
            string sanitizedFileName = Path.GetFileName(fileName);  // This will only get the file name, not the path

            // // Basic input validation
            // if (string.IsNullOrEmpty(fileName) || fileName.Contains("..") || fileName.Contains("\0"))
            // {
            //     return new BadRequestObjectResult("Invalid fileName.");
            // }

            // Simulating a "safe" path.
            string safePath = "./../../../../safe/safe_folder/";
            log.LogInformation($"Safe path: {safePath}");

            // The actual path the program will attempt to read.
            // string actualPath = $"{safePath}{fileName}";
            
            // if (fileName != sanitizedFileName)
            // {
            //     log.LogWarning("Directory traversal attempt detected.");
            //     throw new SecurityException("Directory traversal attempt detected.");
            // }

            string actualPath = Path.Combine(safePath, sanitizedFileName); // good practice to use Path.Combine to avoid path traversal

            // Log the path for debugging purposes.
            log.LogInformation($"Attempting to read file from: {actualPath}");

            try
            {

                // Simulating Buffer Overflow by reading a large file
                // Uncomment the next line and pass a large file name to test
                // byte[] largeFileContent = File.ReadAllBytes(actualPath);

                // Simulating DoS by introducing a sleep
                // Uncomment the next line to test
                // System.Threading.Thread.Sleep(5000);


                // Read the file's content.
                string fileContent = File.ReadAllText(actualPath);


                // Simulating IDOR by checking user permissions
                // Uncomment the next lines to simulate
                // if (!HasPermission(fileName))
                // {
                //    return new UnauthorizedResult();
                // }

            
                return new OkObjectResult($"File content: {fileContent}");
            }
            catch (SecurityException ex)
            {
                log.LogWarning(ex, "Directory traversal attempt detected.");
                return new BadRequestObjectResult("Directory traversal attempt detected. Access denied.");
            }
            catch (Exception ex)
            {
                log.LogError(ex, "An error occurred.");
                return new BadRequestObjectResult("An error occurred while attempting to read the file.");
            }
        }

        // Simulating IDOR by checking user permissions
        // Uncomment the next method to simulate
        // private static bool HasPermission(string fileName)
        // {
        //    return fileName != "restricted_file.txt";
        // }
    }
}
