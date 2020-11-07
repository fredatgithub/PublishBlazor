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
      string secretPhrase = Properties.Settings.Default.SecretMessage;
      if (userName == secretPhrase)
      {
        display("Type the name of the user: ");
        userName = Console.ReadLine();
        apiServerPublishSourcePath = apiServerPublishSourcePath.Replace("UserName", userName);
        webServerPublishSourcePath = webServerPublishSourcePath.Replace("UserName", userName);
      }
      else
      {
        apiServerPublishSourcePath = apiServerPublishSourcePath.Replace("UserName", userName);
        webServerPublishSourcePath = webServerPublishSourcePath.Replace("UserName", userName);
      }

      if (applicationName == secretPhrase)
      {
        display("Type the name of the application: ");
        applicationName = Console.ReadLine();
        apiServerPublishSourcePath = apiServerPublishSourcePath.Replace("ApplicationName", applicationName);
        webServerPublishSourcePath = webServerPublishSourcePath.Replace("ApplicationName", applicationName);
      }
      else
      {
        apiServerPublishSourcePath = apiServerPublishSourcePath.Replace("ApplicationName", applicationName);
        webServerPublishSourcePath = webServerPublishSourcePath.Replace("ApplicationName", applicationName);
      }

      if (targetServerName == secretPhrase)
      {
        display("Type the name of the target server name, you want to deploy to: ");
        targetServerName = Console.ReadLine();
        apiServerPublishTargetPath = apiServerPublishTargetPath.Replace("TargetServerName", targetServerName);
        webServerPublishTargetPath = webServerPublishTargetPath.Replace("TargetServerName", targetServerName);
      }
      else
      {
        apiServerPublishTargetPath = apiServerPublishTargetPath.Replace("TargetServerName", targetServerName);
        webServerPublishTargetPath = webServerPublishTargetPath.Replace("TargetServerName", targetServerName);
      }


      // delete *.pdb
      string pattern = "*.pdb";
      DeleteFiles(pattern, apiServerPublishSourcePath);
      DeleteFiles(pattern, webServerPublishSourcePath);

      // copy files to target directories
      Copyfiles(apiServerPublishSourcePath, apiServerPublishTargetPath);
      Copyfiles(webServerPublishSourcePath, webServerPublishTargetPath);


      display("Press any key to exit:");
      Console.ReadKey();
    }

    private static void Copyfiles(string sourcePath, string targetPath, bool overwrite = true)
    {
      if (!Directory.Exists(sourcePath))
      {
        return;
      }

      //if (!Directory.Exists(targetPath))
      //{
      //  return;
      //}

      string[] sourceFiles;
      sourceFiles = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);
      
      foreach (var fileName in sourceFiles)
      {
        string targetFileName = Path.Combine(targetPath, Path.GetFileName(fileName));
        try
        {
          //File.Copy(fileName, targetFileName, overwrite);
        }
        catch (Exception exception)
        {
          Console.WriteLine($"There was an exception while trying to copy the file {fileName} to the target {targetFileName}. The exception is {exception.Message}");
        }
      }
    }

    private static void DeleteFiles(string pattern, string filePath)
    {
      string[] files;
      if (Directory.Exists(filePath))
      {
        files = Directory.GetFiles(filePath, pattern, SearchOption.AllDirectories);
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
              Console.WriteLine($"There was an exception while trying to delete PDB files: {exception.Message}");
            }
          }
        }
      }
    }
  }
}
