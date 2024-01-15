using BusinessApp.Core.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessApp.Core.Entities
{
    public class Setting : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
