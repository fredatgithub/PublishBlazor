using System;
using System.IO;

namespace PublishBlazor
{
  class Program
  {
    static void Main(string[] arguments)
    {
      Action<string> display = Console.WriteLine;
      display("Publication d'une application blazor");
      string apiServerPublishSourcePath = Properties.Settings.Default.apiServerPublishSourcePath;
      string webServerPublishSourcePath = Properties.Settings.Default.webServerPublishSourcePath;
      string apiServerPublishTargetPath = Properties.Settings.Default.apiServerPublishTargetPath;
      string webServerPublishTargetPath = Properties.Settings.Default.webServerPublishTargetPath;
      string targetServerName = Properties.Settings.Default.TargetServerName;
      string userName = Properties.Settings.Default.UserName;
      string applicationName = Properties.Settings.Default.ApplicationName;
      if (applicationName == "for confidentiality, don't publish it to GitHub")
      {
        display("Type the name of the application: ");
        applicationName = Console.ReadLine();
        apiServerPublishSourcePath = apiServerPublishSourcePath.Replace("UserName", applicationName);
        webServerPublishSourcePath = webServerPublishSourcePath.Replace("UserName", applicationName);
      }
      else
      {
        apiServerPublishSourcePath = apiServerPublishSourcePath.Replace("UserName", userName);
        webServerPublishSourcePath = webServerPublishSourcePath.Replace("UserName", userName);
      }

      if (targetServerName == "for confidentiality, don't publish it to GitHub")
      {
        display("Type the name of the target server name: ");
        targetServerName = Console.ReadLine();
      }
      else
      {

      }

      if (userName == "for confidentiality, don't publish it to GitHub")
      {
        display("Type the name of the user: ");
        userName = Console.ReadLine();
      }
      else
      {

      }


      // delete *.pdb
      // copy file to target directories
      string pattern = "*.pdb";
      string[] files;
      if (Directory.Exists(apiServerPublishSourcePath))
      {
        files = Directory.GetFiles(apiServerPublishSourcePath, pattern, SearchOption.AllDirectories);
        foreach (var fileName in files)
        {
          if (File.Exists(fileName))
          {
            try
            {
              File.Delete(fileName);
            }
            catch (Exception exception)
            {
              display($"Exception: {exception.Message}");
            }
          }
        }
      }

      display("Press any key to exit:");
      Console.ReadKey();
    }
  }
}
