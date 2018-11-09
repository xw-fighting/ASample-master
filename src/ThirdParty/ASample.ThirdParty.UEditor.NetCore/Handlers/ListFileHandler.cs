using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using ASample.ThirdParty.UEditor.NetCore.Models.Result;
using ASample.ThirdParty.UEditor.NetCore.Models;
using Microsoft.AspNetCore.Http;

namespace ASample.ThirdParty.UEditor.NetCore.Handlers
{
    public class ListFileHandler : Handler
    {
        private int Start;
        private int Size;
        private int Total;
        private ResultState State;
        private String PathToList;
        private String[] FileList;
        private String[] SearchExtensions;
        public ListFileHandler(HttpContext context, string pathToList, string[] searchExtensions)
           : base(context)
        {
            this.SearchExtensions = searchExtensions.Select(x => x.ToLower()).ToArray();
            this.PathToList = pathToList;
        }

        public override UEditorResult Process()
        {
            try
            {
                Start = string.IsNullOrWhiteSpace(Request.Query["start"]) ? 0 : Convert.ToInt32(Request.Query["start"]);
                Size = string.IsNullOrWhiteSpace(Request.Query["size"]) ? Config.GetInt("imageManagerListSize") : Convert.ToInt32(Request.Query["size"]);
            }
            catch (FormatException)
            {
                State = ResultState.InvalidParam;
                return WriteResult();
            }
            UEditorResult result;
            var buildingList = new List<String>();
            try
            {
                var localPath = Path.Combine(Config.WebRootPath, PathToList);
                buildingList.AddRange(Directory.GetFiles(localPath, "*", SearchOption.AllDirectories)
                    .Where(x => SearchExtensions.Contains(Path.GetExtension(x).ToLower()))
                    .Select(x => PathToList + x.Substring(localPath.Length).Replace("\\", "/")));
                Total = buildingList.Count;
                FileList = buildingList.OrderBy(x => x).Skip(Start).Take(Size).ToArray();
            }
            catch (UnauthorizedAccessException)
            {
                State = ResultState.AuthorizError;
            }
            catch (DirectoryNotFoundException)
            {
                State = ResultState.PathNotFound;
            }
            catch (IOException)
            {
                State = ResultState.IOError;
            }
            finally
            {
                result = WriteResult();
            }

            return result;
        }

        private UEditorResult WriteResult()
        {
            return new UEditorResult
            {
                State = GetStateString(),
                List = FileList?.Select(x => new UEditorFileList { Url = x }),
                Start = Start,
                Size = Size,
                Total = Total
            };
        }

        private string GetStateString()
        {
            switch (State)
            {
                case ResultState.Success:
                    return "SUCCESS";
                case ResultState.InvalidParam:
                    return "参数不正确";
                case ResultState.PathNotFound:
                    return "路径不存在";
                case ResultState.AuthorizError:
                    return "文件系统权限不足";
                case ResultState.IOError:
                    return "文件系统读取错误";
            }
            return "未知错误";
        }
    }
}
