using System;
using System.Collections.Generic;

namespace CoreStudy_05
{
    public partial class Blogs
    {
        public Blogs()
        {
            Posts = new HashSet<Posts>();
        }

        public int BlogId { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }

        public virtual ICollection<Posts> Posts { get; set; }
    }
}
