using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elgraiv.Aphroreader.Model
{
    [JsonObject("content", MemberSerialization = MemberSerialization.OptIn)]
    public class AphContent : BindableBase
    {
        [JsonProperty("title")]
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        [JsonProperty("directory")]
        private string _directoryPath;
        public string DirectoryPath
        {
            get => _directoryPath;
            set => SetProperty(ref _directoryPath, value);
        }
        [JsonProperty("thumbnail")]
        private string _thumbnailPath;
        public string ThumbnailPath
        {
            get => _thumbnailPath;
            set => SetProperty(ref _thumbnailPath, value);
        }

        private Collection<string> _imageList;
        public ReadOnlyCollection<string> ImageList { get; }

        public AphContent()
        {
            _imageList = new Collection<string>();
            ImageList = new ReadOnlyCollection<string>(_imageList);
        }
        public void CollectImages()
        {
            var files = Directory.EnumerateFiles(DirectoryPath);
            foreach (var file in files)
            {
                _imageList.Add(file);
            }
            NotifyImageListUpdated();
        }
        public void NotifyImageListUpdated()
        {
            RaisePropertyChanged(nameof(ImageList));
        }
    }
}
