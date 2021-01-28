using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sweeper.Controls.Entities
{
    public class CarouselPage
    {
        #region ::Fields & Properties::

        private readonly string _title = string.Empty;

        public string Title
        {
            get
            {
                return _title;
            }
        }

        private readonly Page _content = null;

        public Page Content
        {
            get
            {
                return _content;
            }
        }

        #endregion

        #region ::Constructors::

        public CarouselPage()
        {

        }

        public CarouselPage(string title, Page content)
        {
            _title = title;
            _content = content;
        }

        #endregion
    }
}
