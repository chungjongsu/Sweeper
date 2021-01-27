using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sweeper.Controls.Entities
{
    internal class CarouselPage
    {
        #region ::Fields & Properties::

        private readonly string _title = string.Empty;

        internal string Title
        {
            get
            {
                return _title;
            }
        }

        private readonly Page _content = null;

        internal Page Content
        {
            get
            {
                return _content;
            }
        }

        #endregion

        #region ::Constructors::

        internal CarouselPage()
        {

        }

        internal CarouselPage(string title, Page content)
        {
            _title = title;
            _content = content;
        }

        #endregion
    }
}
