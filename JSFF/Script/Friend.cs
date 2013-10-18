// Class1.cs
//

using System;
using System.Html;
using System.Runtime.CompilerServices;
using System.Collections;
using FreindsLibrary;
using System.Html.Media.Graphics;
using jQueryApi;
namespace JSFFScript
{
    public sealed class Friend
    {
        public Friend(FriendInfo _response, int _index)
        {
            this.name = _response.name;
            this.id = _response.id;
            this.index = _index;
        }
       
        public string name;
        public string id;
        public int index;
    }
   

}


