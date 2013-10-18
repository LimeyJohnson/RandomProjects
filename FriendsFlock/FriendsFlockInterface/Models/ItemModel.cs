using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace FriendsFlockInterface
{
    public class ItemModel : ModelBase
    {
        public const string ItemCurrentIconPropertyName = "ItemCurrentIcon";
        private ImageSource _ItemCurrentIcon = null;
        public ImageSource ItemCurrentIcon
        {
            get
            {
                return _ItemCurrentIcon;
            }

            set
            {
                if (_ItemCurrentIcon == value)
                {
                    return;
                }

                var oldValue = _ItemCurrentIcon;
                _ItemCurrentIcon = value;

                RaisePropertyChanged(ItemCurrentIconPropertyName);
            }
        }

        public const string IconCurrentPropertyName = "IconCurrent";
        private string _IconCurrent;
        public string IconCurrent
        {
            get
            {
                return _IconCurrent;
            }

            set
            {
                if (_IconCurrent == value)
                {
                    return;
                }

                var oldValue = _IconCurrent;
                _IconCurrent = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(IconCurrentPropertyName);
            }
        }
      
        public string IconNormal { get; set; }
        public string IconAccent { get; set; }
        
        public bool ItemIsAccented { get; private set; }

        public const string ItemTitlePropertyName = "ItemTitle";
        private string _ItemTitle = "ItemTitle";
        public string ItemTitle
        {
            get
            {
                return _ItemTitle;
            }

            set
            {
                if (_ItemTitle == value)
                {
                    return;
                }

                var oldValue = _ItemTitle;
                _ItemTitle = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ItemTitlePropertyName);

            }
        }

        public const string ItemDescriptionPropertyName = "ItemDescription";
        private string _ItemDescription = "ItemDescription";
        public string ItemDescription
        {
            get
            {
                return _ItemDescription;
            }

            set
            {
                if (_ItemDescription == value)
                {
                    return;
                }

                var oldValue = _ItemDescription;
                _ItemDescription = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ItemDescriptionPropertyName);
            }
        }

        public const string IconAccentVisablePropertyName = "IconAccentVisable";
        private Visibility _IconAccentVisable = Visibility.Collapsed;
        public Visibility IconAccentVisable
        {
            get
            {
                return _IconAccentVisable;
            }

            set
            {
                if (_IconAccentVisable == value)
                {
                    return;
                }

                var oldValue = _IconAccentVisable;
                _IconAccentVisable = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(IconAccentVisablePropertyName);
            }
        }

        public void ToggleAccent(bool Accent)
        {
            ItemIsAccented = Accent;

            ToggleAccent();
        }

        public void ToggleAccent()
        {
            if (ItemIsAccented)
            {
                IconAccentVisable = Visibility.Visible;
            }
            else
            {
                IconAccentVisable = Visibility.Collapsed;
            }
        }
    }
}