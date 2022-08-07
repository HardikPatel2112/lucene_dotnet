using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lu_search.Models
{
	public class IndexViewModel
	{
		public int Limit { get; set; }
		public bool SearchDefault { get; set; }
		public Student student { get; set; }
		public IEnumerable<Student> AllStudentData { get; set; }
		public IEnumerable<Student> SearchIndexData { get; set; }
		public IList<SelectedList> SearchFieldList { get; set; }
		public string SearchTerm { get; set; }
		public string SearchField { get; set; }
	}
}