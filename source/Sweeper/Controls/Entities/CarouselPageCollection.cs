using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweeper.Controls.Entities
{
    internal class CarouselPageCollection
    {
        #region ::Fields & Properties::

        private readonly List<CarouselPage> _pages = new List<CarouselPage>();

        internal List<CarouselPage> Pages
        {
            get
            {
                return _pages;
            }
        }

        #endregion

        #region ::Constructors::

        internal CarouselPageCollection()
        {

        }

        internal CarouselPageCollection(List<CarouselPage> pages)
        {
            _pages = pages;
        }

        #endregion
    }
}
