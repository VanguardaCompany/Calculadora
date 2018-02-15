using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculoPrev.DAL.Models
{
    public class DataTable
    {
        public class DataTableRequest
        {
            public List<DataTableColumn> Columns { get; set; }
            public int draw { get; set; }
            public int start { get; set; }
            public int length { get; set; }
            public List<DataOrder> Order { get; set; }
            public Search Search { get; set; }

        }

        public class Search
        {
            public bool regex { get; set; }
            public string value { get; set; }
        }

        public class DataTableColumn
        {
            public int Data { get; set; }
            public string Name { get; set; }
            public bool Orderable { get; set; }
            public bool Searchable { get; set; }
            public Search Search { get; set; }
        }

        public class DataOrder
        {
            public int Column { get; set; }
            public string Dir { get; set; }
        }

        public class DataTableResponse
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public object data { get; set; }
            public string error { get; set; }
            public string aux { get; set; }
        }
    }
}