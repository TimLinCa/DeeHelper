using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
            General options = await General.GetLiveInstanceAsync();
            if(options.ProjectName == string.Empty || options.ProjectName == null)
            {
                await VS.MessageBox.ShowWarningAsync($"Please select a project name on optional page.");
                return;
            }
            CustomizedReferenceList customizedReferenceList = await CustomizedReferenceList.GetLiveInstanceAsync();
            List<ReferenceList> referenceLists = customizedReferenceList.ReferencesList;
            BusinessTierPathList businessTierPathList = await BusinessTierPathList.GetLiveInstanceAsync();
            List<BusinessTierPathObj> businessPathObjs = businessTierPathList.BusinessTierPaths;
            if (options.IsSortandRemoveUsing)
            {
                bool isfinished = await VS.Commands.ExecuteAsync("Edit.RemoveAndSort");
                await Task.Delay(500);
            }

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

            string[] DllFilesName = Directory.GetFiles(businessPathObjs.First(obj=>obj.ProjectName == options.ProjectName).BusinessTierFolderPath).Where(file => file.EndsWith(".dll")).ToArray();
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
                    if (referenceLists.Any(rl => rl.NameSpace == namespaceString))
                    {
                        List<ReferenceList> currentReferenceList = referenceLists.Where(rl => rl.NameSpace == namespaceString).ToList();
                        List<string> currentUsingAssembles = currentReferenceList.Where(crl => crl.Assembles != string.Empty).Select(crl => crl.Assembles.Split(';')).ToList().SelectMany(_ => _).Distinct().ToList();
                        currentUsingAssembles = currentUsingAssembles.Select(name => getdllreference(name)).ToList();
                        usingAssmbles.AddRange(currentUsingAssembles);
                        break;
                    }
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
                                    if (IsContainObject(textviewlines, objectName, options))
                                    {
                                        string usereferencedll = getdllreference(customdll);
                                        if (!usingAssmbles.Contains(usereferencedll)) usingAssmbles.Add(usereferencedll);
                                    }
                                }
                            }
                            // break if added custom dll.
                            else if (isaddcustmodll) break;
                            // add all customized library
                            else
                            {
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
                        //Trim namespace for next loop
                        namespaceString = namespaceString.Substring(0, namespaceString.LastIndexOf("."));
                        continue;
                    }
                    //Couldn't find the dll.
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
            if (IsContainObject(textviewlines, AsEnumerableStr, options))
            {
                usingAssmbles.Add(getdllreference(@"%MicrosoftNetPath%\\System.ComponentModel"));
                usingAssmbles.Add(getdllreference(@"%MicrosoftNetPath%\\System.ComponentModel.Primitives"));
                usingAssmbles.Add(getdllreference(@"%MicrosoftNetPath%\\System.Xml.ReaderWriter"));
                usingAssmbles.Add(getdllreference(@"%MicrosoftNetPath%\\System.private.Xml"));
            }
            usingAssmbles.Distinct();
            string DeeResult = newLine;
            DeeResult += "/**Using Assembles**/" + newLine;
            foreach (string assmblename in usingAssmbles) DeeResult += assmblename + newLine;
            DeeResult += "/**Using NameSpace**/" + newLine;
            foreach (string namespacename in usingNamespace)
            {
                if (namespacename != usingNamespace.Last()) DeeResult += namespacename + newLine;
                else DeeResult += namespacename;
            }

            //Delete UseReference line
            while (true)
            {
                var docView_temp = await VS.Documents.GetActiveDocumentViewAsync();
                var textviewlines_temp = await GetActiveDocumentLineContext();
                ITextSnapshotLine deleteLine = null;
                deleteLine = textviewlines_temp.FirstOrDefault(line => (line.Extent.GetText().Contains("UseReference(") ||
                line.Extent.GetText().Contains("Using Assembles") ||
                line.Extent.GetText().Contains("Using NameSpace")) &&
                !line.Extent.GetText().Contains(options.ActionCodeStartLine.Split(';').ToList()));

                if (deleteLine != null) docView_temp.TextBuffer.Delete(deleteLine.ExtentIncludingLineBreak);
                else break;
            }

            textviewlines = await GetActiveDocumentLineContext();

            //Find Actiob code started line
            ITextSnapshotLine startWpfText = textviewlines.FirstOrDefault(line => line.Extent.GetText().Contains(options.ActionCodeStartLine.Split(';').ToList()));
            if (startWpfText == null)
            {
                await VS.MessageBox.ShowWarningAsync($"Cannot find {options.ActionCodeStartLine} text line");
                return;
            }

            //Insert UseReference to file
            var docView = await VS.Documents.GetActiveDocumentViewAsync();
            docView.TextBuffer.Insert(startWpfText.End.Position, DeeResult);

            //Format Code
            await VS.Commands.ExecuteAsync("Edit.FormatDocument");

            //Show warning message which shows what dll couldn't find
            if (LosingDll.Any())
            {
                await VS.MessageBox.ShowWarningAsync($"{string.Join(",", LosingDll)} cannot find any related library to use. Please add the library manually.");
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
            if (!dllname.Contains("dll"))
            {
                return $"UseReference(\"{dllname}.dll\", \"\");";
            }
            return $"UseReference(\"{dllname}\", \"\");";
        }

        private bool IsContainObject(List<ITextSnapshotLine> textviewlines, string objectName, General options)
        {

            ITextSnapshotLine startWpfText = textviewlines.FirstOrDefault(line => line.Extent.GetText().Contains(options.ActionCodeStartLine.Split(';').ToList()));
            ITextSnapshotLine endWpfText = textviewlines.FirstOrDefault(line => line.Extent.GetText().Contains(options.ActionCodeEndLine.Split(';').ToList()));

            ITextSnapshotLine ValidationCodeStartWpfText = textviewlines.FirstOrDefault(line => line.Extent.GetText().Contains(options.ValidationCodeStartLine.Split(';').ToList()));
            ITextSnapshotLine ValidationCodeEndWpfText = textviewlines.FirstOrDefault(line => line.Extent.GetText().Contains(options.ValidationCodeEndLine.Split(';').ToList()));

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
