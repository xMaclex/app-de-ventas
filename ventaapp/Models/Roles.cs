/* using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ventaapp.Models
{

    [Table("roles_tb")]
    public class Roles
    {
        [Key]
        [Column("id_roles")]
        public int IdRoles { get; set; }

        [Display(Name = "Roles")]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        public ICollection<Usuarios> Usuarios{ get; set; } = new List<Usuarios>();
    }

}

*/