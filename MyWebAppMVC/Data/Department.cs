using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebAppMVC.Data
{
    [Table("Department")]
    public partial class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
