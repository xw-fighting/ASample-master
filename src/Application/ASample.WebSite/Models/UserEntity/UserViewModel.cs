
namespace ASample.WebSite.Models.UserEntity
{
    public class UserViewModel
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        /// <summary>
        /// 性别的中文意思
        /// <para>输出：男、女、保命</para>
        /// </summary>
        public string GenderName
        {
            get
            {
                string rs = "保密";
                if (Gender == "Male")
                {
                    rs = "男";
                }
                else if (Gender == "Female")
                {
                    rs = "女";
                }
                return rs;
            }
            set
            {
                if (value == "男")
                {
                    Gender = "Male";
                }
                else if (value == "女")
                {
                    Gender = "Female";
                }
                else
                {
                    Gender = "unkonw";
                }
            }
        }

        public TranscriptsEntity TranscriptsEn { get; set; }

        public bool IsExcelVaildateOK { get; set; }
    }
}