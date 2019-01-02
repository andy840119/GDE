using GDEdit.Utilities.Objects.GeometryDash.LevelObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.Toolbars
{
    /// <summary>Represents a tab in the <seealso cref="ObjectSelectionToolbar"/>.</summary>
    public class ObjectSelectionToolbarTab
    {
        /// <summary>The object ID of the object whose image will be shown as the sample image in the tab.</summary>
        public int SampleImageObjectID { get; set; }
        /// <summary>The pages of the tab.</summary>
        public List<ObjectSelectionToolbarTabPage> Pages { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="ObjectSelectionToolbarTab"/> class.</summary>
        /// <param name="sampleImageObjectID">The object ID of the object whose image will be shown as the sample image in the tab.</param>
        public ObjectSelectionToolbarTab(int sampleImageObjectID)
        {
            SampleImageObjectID = sampleImageObjectID;
            Pages = new List<ObjectSelectionToolbarTabPage>();
        }
        /// <summary>Initializes a new instance of the <seealso cref="ObjectSelectionToolbarTab"/> class.</summary>
        /// <param name="sampleImageObjectID">The object ID of the object whose image will be shown as the sample image in the tab.</param>
        /// <param name="pages">The pages this <seealso cref="ObjectSelectionToolbarTab"/> will have.</param>
        public ObjectSelectionToolbarTab(int sampleImageObjectID, List<ObjectSelectionToolbarTabPage> pages)
        {
            SampleImageObjectID = sampleImageObjectID;
            Pages = pages;
        }

        /// <summary>Represents a page in the <seealso cref="ObjectSelectionToolbarTab"/>.</summary>
        public class ObjectSelectionToolbarTabPage
        {
            /// <summary>The items of the page.</summary>
            public List<ObjectSelectionToolbarTabItem> Items { get; set; }

            /// <summary>Initializes a new instance of the <seealso cref="ObjectSelectionToolbarTabPage"/> class.</summary>
            public ObjectSelectionToolbarTabPage()
            {
                Items = new List<ObjectSelectionToolbarTabItem>();
            }
            /// <summary>Initializes a new instance of the <seealso cref="ObjectSelectionToolbarTabPage"/> class.</summary>
            /// <param name="items">The items this <seealso cref="ObjectSelectionToolbarTabPage"/> will have.</param>
            public ObjectSelectionToolbarTabPage(List<ObjectSelectionToolbarTabItem> items)
            {
                Items = items;
            }
        }
        /// <summary>Represents an item in the <seealso cref="ObjectSelectionToolbarTabPage"/>.</summary>
        public class ObjectSelectionToolbarTabItem
        {
            /// <summary>The object that this item refers to, with all the properties it contains.</summary>
            public GeneralObject Object { get; set; }
            /// <summary>The background color of the tab item.</summary>
            public Color BackgroundColor { get; set; } = Color.Silver;
            
            /// <summary>Initializes a new instance of the <seealso cref="ObjectSelectionToolbarTabItem"/> class.</summary>
            /// <param name="obj">The object of this <seealso cref="ObjectSelectionToolbarTabItem"/>.</param>
            public ObjectSelectionToolbarTabItem(GeneralObject obj)
            {
                Object = obj;
            }
            /// <summary>Initializes a new instance of the <seealso cref="ObjectSelectionToolbarTabItem"/> class.</summary>
            /// <param name="obj">The object of this <seealso cref="ObjectSelectionToolbarTabItem"/>.</param>
            /// <param name="backgroundColor">The background color of this <seealso cref="ObjectSelectionToolbarTabItem"/>.</param>
            public ObjectSelectionToolbarTabItem(GeneralObject obj, Color backgroundColor)
            {
                Object = obj;
                BackgroundColor = backgroundColor;
            }
        }
    }
}
