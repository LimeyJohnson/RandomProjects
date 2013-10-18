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

namespace FriendsFlockInterface
{
    public class FriendsState : ModelBase
    {
        //IsSelected
        //IsShorestPathTarget
        //IsShorestPathMember
        //IsMutualFriend
        //IsHighlighted

        //public delegate void StateChangedEventHandler(object sender, EventArgs e);
        //public event StateChangedEventHandler IsSelected_Changed;
        //public event StateChangedEventHandler IsShorestPathTarget_Changed;


        #region Dynamic
        public string IsSelectedPropertyName = "IsSelected";
        private bool _IsSelected = false;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }

            set
            {
                if (_IsSelected == value)
                {
                    return;
                }

                var oldValue = _IsSelected;
                _IsSelected = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(IsSelectedPropertyName);

                //if (_IsSelected)
                //    if (IsSelected_Changed != null)
                //        IsSelected_Changed.Invoke(this, new EventArgs());
            }
        }

        public string IsShorestPathTargetPropertyName = "IsShorestPathTarget";
        private bool _IsShorestPathTarget = false;
        public bool IsShorestPathTarget
        {
            get
            {
                return _IsShorestPathTarget;
            }

            set
            {
                if (_IsShorestPathTarget == value)
                {
                    return;
                }

                var oldValue = _IsShorestPathTarget;
                _IsShorestPathTarget = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(IsShorestPathTargetPropertyName);

                //if (_IsShorestPathTarget)
                //    if (IsShorestPathTarget_Changed != null)
                //        IsShorestPathTarget_Changed.Invoke(this, new EventArgs());
            }
        }

        public string IsShorestPathMemberPropertyName = "IsShorestPathMember";
        private bool _IsShorestPathMember= false;
        public bool IsShorestPathMember
        {
            get
            {
                return _IsShorestPathMember;
            }

            set
            {
                if (_IsShorestPathMember == value)
                {
                    return;
                }

                var oldValue = _IsShorestPathMember;
                _IsShorestPathMember = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(IsShorestPathMemberPropertyName);
            }
        }

        public string IsMutualFriendPropertyName = "IsMutualFriend";
        private bool _IsMutualFriend = false;
        public bool IsMutualFriend
        {
            get
            {
                return _IsMutualFriend;
            }

            set
            {
                if (_IsMutualFriend == value)
                {
                    return;
                }

                var oldValue = _IsMutualFriend;
                _IsMutualFriend = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(IsMutualFriendPropertyName);
            }
        }

        public string IsHighlightedPropertyName = "IsHighlighted";
        private bool _IsHighlighted = false;
        public bool IsHighlighted
        {
            get
            {
                return _IsHighlighted;
            }

            set
            {
                if (_IsHighlighted == value)
                {
                    return;
                }

                var oldValue = _IsHighlighted;
                _IsHighlighted = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(IsHighlightedPropertyName);
            }
        }
        #endregion

        #region Static
        public bool ProfileUpdated = false;
        public bool StatusUpdated = false;
        public bool BirthdayUpdated = false;
        public bool Relationship = false;
        #endregion
    }
}