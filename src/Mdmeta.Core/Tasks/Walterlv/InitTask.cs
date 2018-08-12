﻿using System;
using System.IO;
using Mdmeta.Core;
using static Mdmeta.Tasks.Walterlv.MdmetaUtils;
using static Mdmeta.Tasks.Walterlv.PostMeta;

namespace Mdmeta.Tasks.Walterlv
{
    [CommandMetadata("winit", Description = "将所有的 md 文件按照 YAML 元数据设置文件属性。")]
    public sealed class InitTask : CommandTask
    {
        [CommandArgument("[folder]", Description = "要初始化 md 文件属性的文件夹。（如果不指定，则会自动查找。）")]
        public string FolderName { get; set; }

        public override int Run()
        {
            try
            {
                var folderName = FindPostFolder(FolderName);
                var folder = new DirectoryInfo(Path.GetFullPath(folderName));
                foreach (var file in folder.EnumerateFiles("*.md", SearchOption.AllDirectories))
                {
                    InitFile(file);
                }
                return 0;
            }
            catch (Exception ex)
            {
                OutputError(ex.Message);
                return 1;
            }
        }

        private void InitFile(FileInfo file)
        {
            var frontMatter = Read(file);
            if (frontMatter == null) return;

            var dateString = frontMatter.Date;
            var publishDateString = frontMatter.PublishDate ?? dateString;

            if (!string.IsNullOrWhiteSpace(dateString))
            {
                var date = DateTimeOffset.Parse(dateString);
                var publishDate = DateTimeOffset.Parse(publishDateString);

                FixFileDate(file, publishDate, date);
            }
        }

        private void FixFileDate(FileInfo file, DateTimeOffset createdTime, DateTimeOffset modifiedTime)
        {
            file.CreationTimeUtc = createdTime.UtcDateTime;
            file.LastWriteTimeUtc = modifiedTime.UtcDateTime;
            file.LastAccessTimeUtc = DateTimeOffset.Now.UtcDateTime;
        }
    }
}
