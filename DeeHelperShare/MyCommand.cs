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
                    //Customized reference list
                    if (referenceLists.Any(rl => namespaceString.SqlContains(rl.NameSpace)))
                    {
                        List<ReferenceList> currentReferenceList = referenceLists.Where(rl => namespaceString.SqlContains(rl.NameSpace)).ToList();
                        List<string> currentUsingAssembles = DivideStringByColon(currentReferenceList.Select(crl => crl.Assembles).Where(ass => !String.IsNullOrEmpty(ass)));
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

            //keyword
            foreach(ReferenceList referenceList in referenceLists)
            {
                if (!String.IsNullOrEmpty(referenceList.KeyWord) && IsContainObject(textviewlines, referenceList.KeyWord, options))
                {
                    if(!String.IsNullOrEmpty(referenceList.NameSpace))
                    {
                        List<string> namespaces = referenceList.NameSpace.Split(';').ToList();
                        usingString.AddRange(namespaces);
                    }
                    if(!String.IsNullOrEmpty(referenceList.Assembles))
                    {
                        List<string> assembles = referenceList.Assembles.Split(';').ToList();
                        assembles.ForEach(assemble => usingAssmbles.Add(getdllreference(assemble)));
                    }
                    //usingAssmbles.Add(getdllreference(@"%MicrosoftNetPath%\\System.ComponentModel"));
                    //usingAssmbles.Add(getdllreference(@"%MicrosoftNetPath%\\System.ComponentModel.Primitives"));
                    //usingAssmbles.Add(getdllreference(@"%MicrosoftNetPath%\\System.Xml.ReaderWriter"));
                    //usingAssmbles.Add(getdllreference(@"%MicrosoftNetPath%\\System.private.Xml"));
                }
            }
            usingString.Distinct();
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

        private string getdllreference(string dllname)
        {
            if (!dllname.Contains(".dll"))
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
                if (!textviewlines[i].Extent.GetText().Contains("UseReference(") && textviewlines[i].Extent.GetText().SqlContains(objectName))
                {
                    return true;
                }
            }

            for (int i = ValidationCodeStartDetectLine; i <= ValidationCodeEndDetectLine; i++)
            {
                if (!textviewlines[i].Extent.GetText().Contains("UseReference(") && textviewlines[i].Extent.GetText().SqlContains(objectName))
                {
                    return true;
                }
            }

            return false;
        }
         
        private List<string> DivideStringByColon(IEnumerable<string> strings)
        {
            return strings.Select(str => str.Split(';')).ToList().SelectMany(_ => _).Distinct().ToList();
        }
    }
}
