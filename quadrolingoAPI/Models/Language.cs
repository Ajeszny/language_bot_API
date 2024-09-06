using Microsoft.EntityFrameworkCore;

namespace quadrolingoAPI.Models
{
    [PrimaryKey("LANG_CODE")]

    public class Language
    {

        public string LANG_ENG_NAME { get; set; }
        public string LANG_CODE { get; set; }
    }
}
