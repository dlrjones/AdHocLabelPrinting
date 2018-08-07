using System;
using System.Configuration;
using System.IO;

namespace AdHocLabelPrinting
{
    class AES
    {
        private FileStream input;
        private FileStream output;
        protected static string AESPath = AppDomain.CurrentDomain.BaseDirectory;
        protected static string connStr = ConfigurationManager.ConnectionStrings["amc_userConnectionString"].ConnectionString;

        public bool  GetConnectionString(ref string mssg)
        {
            return GetResources(ref mssg);
        }

        private bool GetResources(ref string mssg)
        {
            string rsrcValu = "";
            string inPath = AESPath + "rsrc.aes";
            string outPath = AESPath + "rsrc.txt";
            string[] login = {"", ""};
            bool goodToGo = true;

            if (CheckInputFile(inPath, ref mssg))
            {
                FileStream input = File.Open(inPath, FileMode.Open);
                FileStream output = File.Create(outPath);

                try
                {
                    SharpAESCrypt.SharpAESCrypt.Decrypt("SKi#ObOIzgF*Gh2SR1YO", input, output);
                    input.Close();
                    output.Close();

                    output = File.Open(outPath, FileMode.Open);
                    StreamReader sr = new StreamReader(output);
                    rsrcValu = sr.ReadToEnd();
                    sr.Dispose();
                    output.Close();
                    File.Delete(outPath);
                    login = rsrcValu.Split("|".ToCharArray());
                }
                catch (Exception ex)
                {
                    mssg = "Unable to access the login credentials";
                    goodToGo = false;
                }
                mssg = connStr + ";User ID=" + login[0] + ";Password=" + login[1];
            }
            else
            {   //mssg comes from  CheckInputFile()               
                goodToGo = false;
            }
            return goodToGo;
        }

        private bool CheckInputFile(string fileName, ref string mssg)
        {
            bool goodToGo = true;
            try
            {
                if (File.Exists(fileName))
                {
                    input = File.Open(fileName.Trim(), FileMode.Open);
                    if (input != null)
                        input.Close();                    
                }
                else
                {
                    mssg = "The input file was not found";
                    goodToGo = false;
                }
            }
            catch (IOException ex)
            {
                mssg = "The input file is in use   " + Environment.NewLine + ex.Message;
                if (input != null)
                    input.Close();
               // logFile.LogEntry("CheckInputFile" + Environment.NewLine + ex.Message, compatibilityCode);
                goodToGo = false;
            }
            return goodToGo;
        }
    }
}
