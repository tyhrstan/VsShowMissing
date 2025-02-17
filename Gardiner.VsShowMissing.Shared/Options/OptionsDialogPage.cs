﻿using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing.Design;
using System.Runtime.InteropServices;

using Microsoft.VisualStudio.Shell;

namespace Gardiner.VsShowMissing.Options
{
#pragma warning disable CA1812

    [Description("Extension that checks for any files referenced in projects that do not exist")]
    [DisplayName("Extension that checks for any files referenced in projects that do not exist")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    [Guid("1D9ECCF3-5D2F-4112-9B25-264596873DC9")]
    internal class GeneralOptions : BaseOptionModel<GeneralOptions>
    {
        private const string IgnorePhysicalFilesDefault = "*.*proj;*.user;.gitignore;*.ruleset;*.suo;*.licx;*.dotSettings;*.dbmdl;*.jfm";
        private const bool UseGitIgnoreDefault = true;
        private const bool UseGlobalGitIgnoreDefault = true;
        private const bool NotIncludedFilesDefault = true;
        private const bool FailBuildOnErrorDefault = false;
        private string _ignorePhysicalFiles;

        public GeneralOptions()
        {
            _ignorePhysicalFiles = IgnorePhysicalFilesDefault;
            UseGitIgnore = UseGitIgnoreDefault;
            UseGlobalGitIgnore = UseGlobalGitIgnoreDefault;
            NotIncludedFiles = NotIncludedFilesDefault;
            FailBuildOnError = FailBuildOnErrorDefault;
        }

        [LocDisplayName("Use .gitignore")]
        [Description("If checked then .gitignore file is also used for ignoring files")]
        [Category("Show Missing")]
        [DefaultValue(UseGitIgnoreDefault)]
        public bool UseGitIgnore { get; set; }

        [LocDisplayName("Use global .gitignore also")]
        [Description("If checked then global .gitignore file is also used for ignoring files")]
        [Category("Show Missing")]
        [DefaultValue(UseGlobalGitIgnoreDefault)]
        public bool UseGlobalGitIgnore { get; set; }

        [LocDisplayName("Message importance")]
        [Description("What kind of message to create in the Error List window for each missing file")]
        [Category("Show Missing")]
        [DefaultValue(TaskErrorCategory.Error)]
        public TaskErrorCategory MessageLevel { get; set; }

        [LocDisplayName("Cancel build on error")]
        [Description(
            "If true, cancel the build if any missing files are found. This only has effect if When is set to BeforeBuild"
            )]
        [Category("Show Missing")]
        [DefaultValue(FailBuildOnErrorDefault)]
        public bool FailBuildOnError { get; set; }

        [LocDisplayName("When")]
        [Description("When to check for missing files")]
        [Category("Show Missing")]
        [DefaultValue(RunWhen.BeforeBuild)]
        public RunWhen Timing { get; set; }

        [LocDisplayName("Non-included files")]
        [Description("Generate warnings/errors for files on disk that are not included in the project")]
        [Category("Show Missing")]
        [DefaultValue(NotIncludedFilesDefault)]
        public bool NotIncludedFiles { get; set; }

        [LocDisplayName("Ignore Pattern")]
        [Description("Semicolon-separated list of filename patterns to ignore when checking physical files")]
        [Category("Show Missing")]
        [DefaultValue(IgnorePhysicalFilesDefault)]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string IgnorePhysicalFiles
        {
            get => _ignorePhysicalFiles;
            set
            {
                if (string.Equals(value, _ignorePhysicalFiles, StringComparison.Ordinal))
                {
                    return;
                }

                Debug.WriteLine($"IgnoreFiles\nOld: {_ignorePhysicalFiles}\nNew: {value}");
                _ignorePhysicalFiles = value;
            }
        }
    }
}
