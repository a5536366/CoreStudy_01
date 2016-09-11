using System;
using System.Collections.Generic;

namespace CoreStudy_05
{
    public partial class News
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int OrderNo { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
