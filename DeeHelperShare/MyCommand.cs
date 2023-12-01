using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace DeeHelper
{
    [Command(PackageIds.MyCommand)]
    internal sealed class MyCommand : BaseCommand<MyCommand>
    {
        private string newLine = Environment.NewLine;
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            List<string> LosingDll = new List<string>();
            var options = await General.GetLiveInstanceAsync();
            if(options.IsRortandRemoveUsing) await VS.Commands.ExecuteAsync("Edit.RemoveAndSort");

            await Task.Delay(500);

            var textviewlines = await GetActiveDocumentLineContext();
            List<string> usingString = new List<string>();
            foreach (var textview in textviewlines)
            {
                string lineString = textview.Extent.GetText();
                if (lineString.Contains("namespace")) break;
                else usingString.Add(lineString);
            }

            usingString = usingString.Where(st => st.Contains("using")).ToList();
            usingString = usingString.Select(st => st.Replace(";", string.Empty)).ToList();
          


            string[] DllFilesName = Directory.GetFiles(options.BusinessTierPath).Where(file => file.EndsWith(".dll")).ToArray();
            List<string> usingAssmbles = new List<string>();
            List<string> usingNamespace = new List<string>();
            List<string> dllfilenames = new List<string>(); ;

            foreach (string dllfileName in DllFilesName) dllfilenames.Add(Regex.Replace(Path.GetFileName(dllfileName), ".dll", "").Trim());
            bool isaddcustmodll = false;

            foreach (string usingtext in usingString)
            {
                string namespaceString = Regex.Replace(usingtext, "using", "").Trim();
                if (namespaceString.Contains("="))
                {
                    int indexOfEqula = namespaceString.IndexOf("=");
                    namespaceString = namespaceString.Substring(indexOfEqula + 1);
                    namespaceString = namespaceString.Trim();
                }
                usingNamespace.Add($"UseReference(\"\", \"{namespaceString}\");");
                if (namespaceString.Substring(0, 6) == "System") continue;
                while (true)
                {
                    //Normal
                    if (dllfilenames.Any(dllfilename => dllfilename == namespaceString))
                    {
                        string name = dllfilenames.Where(dllfilename => dllfilename == namespaceString).FirstOrDefault();
                        string usereferencedll = getdllreference(name);
                        if (!usingAssmbles.Contains(usereferencedll)) usingAssmbles.Add(usereferencedll);
                        break;
                    }
                    if (namespaceString.Contains("."))
                    {
                        //Erp
                        if (namespaceString.Substring(namespaceString.LastIndexOf(".") + 1) == "Erp")
                        {
                            string usereferencedll = getdllreference("Cmf.Custom.BusinessObjects.ErpCustomManagement");
                            if (!usingAssmbles.Contains(usereferencedll)) usingAssmbles.Add(usereferencedll);
                            break;
                        }
                        //Customer
                        if (namespaceString.Contains("Cmf.Custom.") && namespaceString.Contains("BusinessObjects"))
                        {
                            //Scan
                            if (options.ScanBusinessObjects)
                            {
                                List<string> customdllname = dllfilenames.Where(dllfilename => dllfilename.Contains("Cmf.Custom.") && dllfilename.Contains("BusinessObjects")).ToList();
 
                                foreach (string customdll in customdllname)
                                {
                                    string objectName = customdll.Substring(customdll.LastIndexOf(".") + 1);
                                    if(IsContainerObject(textviewlines, objectName, options))
                                    {
                                        string usereferencedll = getdllreference(customdll);
                                        if (!usingAssmbles.Contains(usereferencedll)) usingAssmbles.Add(usereferencedll);
                                    }
                                }
                            }
                            else
                            {
                                if (isaddcustmodll) break;
                                List<string> customdllname = dllfilenames.Where(dllfilename => dllfilename.Contains("Cmf.Custom.") && dllfilename.Contains("BusinessObjects")).ToList();
                                foreach (string customdll in customdllname)
                                {
                                    string usereferencedll = getdllreference(customdll);
                                    if (!usingAssmbles.Contains(usereferencedll)) usingAssmbles.Add(usereferencedll);
                                }
                                isaddcustmodll = true;
                            }
                            break;
                        }
                        namespaceString = namespaceString.Substring(0, namespaceString.LastIndexOf("."));
                        continue;
                    }
                    else
                    {
                        LosingDll.Add(Regex.Replace(usingtext, "using", ""));
                        break;
                        //await VS.MessageBox.ShowWarningAsync($"Cannot find any dll realted to {Regex.Replace(usingtext, "using", "")}");
                        //return;
                    }
                }
            }

            //Detect .AsEnumerable()
            string AsEnumerableStr = ".AsEnumerable()";
            if (IsContainerObject(textviewlines, AsEnumerableStr, options))
            {
                usingAssmbles.Add(getdllreference(@"%MicrosoftNetPath%\\System.ComponentModel"));
                usingAssmbles.Add(getdllreference(@"%MicrosoftNetPath%\\System.ComponentModel.Primitives"));
                usingAssmbles.Add(getdllreference(@"%MicrosoftNetPath%\\System.Xml.ReaderWriter"));
                usingAssmbles.Add(getdllreference(@"%MicrosoftNetPath%\\System.private.Xml"));
            }
            string DeeResult = newLine;
            DeeResult += "/**Using Assembles**/" + newLine;
            foreach (string assmblename in usingAssmbles) DeeResult += assmblename + newLine;
            DeeResult += "/**Using NameSpace**/" + newLine;
            foreach (string namespacename in usingNamespace)
            {
                if (namespacename != usingNamespace.Last()) DeeResult += namespacename + newLine;
                else DeeResult += namespacename;
            }

            while (true)
            {
                var docView_temp = await VS.Documents.GetActiveDocumentViewAsync();
                var textviewlines_temp = await GetActiveDocumentLineContext();
                ITextSnapshotLine deleteLine = null;
                deleteLine = textviewlines_temp.FirstOrDefault(line => (line.Extent.GetText().Contains("UseReference(") ||
                line.Extent.GetText().Contains("Using Assembles") ||
                line.Extent.GetText().Contains("Using NameSpace")) &&
                !line.Extent.GetText().Contains(options.ActionCodeStartLine));

                if (deleteLine != null) docView_temp.TextBuffer.Delete(deleteLine.ExtentIncludingLineBreak);
                else break;
            }

            textviewlines = await GetActiveDocumentLineContext();

            ITextSnapshotLine startWpfText = textviewlines.FirstOrDefault(line => line.Extent.GetText().Contains(options.ActionCodeStartLine));

            if (startWpfText == null)
            {
                await VS.MessageBox.ShowWarningAsync($"Cannot find {options.ActionCodeStartLine} text line");
                return;
            }
            var docView = await VS.Documents.GetActiveDocumentViewAsync();
            docView.TextBuffer.Insert(startWpfText.End.Position, DeeResult);
            await VS.Commands.ExecuteAsync("Edit.FormatDocument");

            if (LosingDll.Any())
            {
                await VS.MessageBox.ShowWarningAsync($"{string.Join(",", LosingDll)} cannot find any dll which realted to");
                return;
            }
        }

        private async Task<List<ITextSnapshotLine>> GetActiveDocumentLineContext()
        {
            var docView = await VS.Documents.GetActiveDocumentViewAsync();
            return docView.TextView.FormattedLineSource.SourceTextSnapshot.Lines.ToList();
        }

        private List<string> GetQuoteSet(string text, string separator)
        {
            List<string> stringSet = new List<string>();
            int? indextemp = null;
            int scanIndex = 0;
            while (true)
            {
                int indexOfQuote;
                indexOfQuote = text.IndexOf(separator, scanIndex);
                if (indexOfQuote == -1) break;
                scanIndex = indexOfQuote + 1;
                if (indextemp == null) indextemp = indexOfQuote;
                else
                {
                    stringSet.Add(text.Substring(indextemp.Value + 1, indexOfQuote - indextemp.Value - 1));
                    indextemp = null;
                }
            }

            return stringSet;


        }

        private string getdllreference(string dllname)
        {
            return $"UseReference(\"{dllname}.dll\", \"\");";
        }

        private bool IsContainerObject(List<ITextSnapshotLine> textviewlines, string objectName, General options)
        {
            
            ITextSnapshotLine startWpfText = textviewlines.FirstOrDefault(line => line.Extent.GetText().Contains(options.ActionCodeStartLine));
            ITextSnapshotLine endWpfText = textviewlines.FirstOrDefault(line => line.Extent.GetText().Contains(options.ActionCodeEndLine));

            ITextSnapshotLine ValidationCodeStartWpfText = textviewlines.FirstOrDefault(line => line.Extent.GetText().Contains(options.ValidationCodeStartLine));
            ITextSnapshotLine ValidationCodeEndWpfText = textviewlines.FirstOrDefault(line => line.Extent.GetText().Contains(options.ValidationCodeEndLine));

            int StartDetectLine = textviewlines.IndexOf(startWpfText) + 1;
            int EndDetectLine = textviewlines.IndexOf(endWpfText) - 1;

            int ValidationCodeStartDetectLine = textviewlines.IndexOf(ValidationCodeStartWpfText) + 1;
            int ValidationCodeEndDetectLine = textviewlines.IndexOf(ValidationCodeEndWpfText) - 1;

            for (int i = StartDetectLine; i <= EndDetectLine; i++)
            {
                if (!textviewlines[i].Extent.GetText().Contains("UseReference(") && textviewlines[i].Extent.GetText().Contains(objectName))
                {
                    return true;
                }
            }

            for (int i = ValidationCodeStartDetectLine; i <= ValidationCodeEndDetectLine; i++)
            {
                if (!textviewlines[i].Extent.GetText().Contains("UseReference(") && textviewlines[i].Extent.GetText().Contains(objectName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
