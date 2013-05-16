using System;
using System.IO;

namespace DownMarker.Core
{
    public class LinkEditorViewModel : ViewModelBase<LinkEditorViewModel>
    {
        private readonly IPrompt prompt;
        private readonly IFileSystem fileSystem;
        private readonly bool image;
        private string description;
        private string target;
        private bool createTarget;        

        public LinkEditorViewModel(IFileSystem fileSystem, IPrompt prompt, bool image=false)
        {
            this.fileSystem = fileSystem;
            this.prompt = prompt;
            this.image = image;
            this.CreateTarget = !image;
        }

        public string CurrentFile { get; set; }

        public bool CreateTargetAvailable
        {
           get
           {
              return !image;
           }
        }

        public bool CreateTarget
        {
            get
            {
                return createTarget;
            }
            set
            {
                createTarget = value;
                OnPropertyChanged(x => x.IsValid);
                OnPropertyChanged(x => x.CreateTarget);
                OnPropertyChanged(x => x.CreateTargetError);
            }
        }

        private string CheckCreateTargetFilePath(string file)
        {
            if (Path.GetExtension(file) != ".md")
            {
                return "Only .md files can be created";
            }
            if (fileSystem.Exists(file))
            {
                return "File already exists!";
            }
            return null;
        }

        public string CreateTargetError
        {
            get
            {
                if ((CreateTarget) && (LinkTargetError == null))
                {
                    var uri = new Uri(LinkTarget, UriKind.RelativeOrAbsolute);
                    if (uri.IsAbsoluteUri)
                    {
                        if (uri.Scheme == Uri.UriSchemeFile)
                        {
                            return CheckCreateTargetFilePath(uri.LocalPath);
                        }
                        else
                        {
                            return "Only file:/// targets can be created";
                        }
                    }
                    else // relative URI
                    {
                        if (CurrentFile == null)
                        {
                            return "Can't determine absolute file path from relative URI because the current file is not yet saved.";
                        }
                        else
                        {
                            uri = new Uri(new Uri(CurrentFile), uri);
                            return CheckCreateTargetFilePath(uri.LocalPath);
                        }
                    }
                }
                return null;
            }
        }

        public string LinkDescription
        {
            get
            {
                return this.description;
            }
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    OnPropertyChanged(x => x.IsValid);
                    OnPropertyChanged(x => x.LinkDescription);
                    OnPropertyChanged(x => x.LinkDescriptionError);
                }
            }
        }

        public string LinkTarget
        {
            get
            {
                return this.target;
            }
            set
            {
                if (this.target != value)
                {
                    this.target = value;
                    OnPropertyChanged(x => x.IsValid);
                    OnPropertyChanged(x => x.LinkTarget);
                    OnPropertyChanged(x => x.LinkTargetError);
                    OnPropertyChanged(x => x.CreateTargetError);
                }
            }
        }

        public bool IsValid
        {
            get
            {
                return (LinkTargetError == null)
                    && (LinkDescriptionError == null)
                    && (CreateTargetError == null);
            }
        }


        public string LinkTargetError
        {
            get
            {
                if (String.IsNullOrEmpty(LinkTarget))
                {
                    return image ? "Please enter an image URL" : "Please enter a link target URL";
                }
                if (!Uri.IsWellFormedUriString(LinkTarget, UriKind.RelativeOrAbsolute))
                {
                    return "Please enter a well-formed URI.";
                }
                return null;
            }
        }

        public string LinkDescriptionError
        {
            get
            {
                if (String.IsNullOrEmpty(LinkDescription))
                    return image ? "Please enter an image description" : "Please enter a link description";
                else
                    return null;
            }
        }

        private string GetCurrentLocation()
        {
            if (CurrentFile == null)
            {
                return null;
            }
            else
            {
                return Path.GetDirectoryName(CurrentFile);
            }
        }

        public void Browse()
        {
            string currentLocation = GetCurrentLocation();
            string file = image ? 
                prompt.QuestionOpenImageFile(currentLocation) 
                : prompt.QuestionOpenFile(currentLocation);
            if (file != null)
            {
                var browseUri = new Uri(file);
                if (CurrentFile != null)
                {
                    browseUri = new Uri(CurrentFile).MakeRelativeUri(browseUri);
                }
                this.LinkTarget = browseUri.ToString();
                this.CreateTarget = false;
            }
        }

    }
}
