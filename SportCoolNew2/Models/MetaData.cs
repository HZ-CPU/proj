using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace SportCoolNew2.Models
{
    public class MetaData
    {
        public class MetadataMember
        {
            [Key]
            [DisplayName("會員編號")]
            public string mId { get; set; }

            [DisplayName("信箱")]
            [Required(ErrorMessage = "您必須輸入Email，此Email為後續您登入的帳號")]
            [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "信箱格式錯誤")]

            public string email { get; set; }

            [DisplayName("密碼")]
            [Required(ErrorMessage = "密碼必填")]
            [DataType(DataType.Password)]
            [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$", ErrorMessage = "密碼必須包含英數字混和6-20碼,至少一個大寫和小寫字母")]
            public string password { get; set; }


            [DisplayName("再次輸入密碼")]
            [Compare("password", ErrorMessage = "密碼不合")]
            [DataType(DataType.Password)]
            [Required(ErrorMessage = "必填")]
            public string Checkpassword { get; set; }




            [DisplayName("生日")]
            [DataType(DataType.DateTime, ErrorMessage = "輸入日期有誤")]
            [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
            [Required(ErrorMessage = "生日必填")]
            public System.DateTime birthday { get; set; }


            [DisplayName("性別")]
            [Required(ErrorMessage = "性別必填")]
            public bool gender { get; set; }

            [DisplayName("姓名")]
            [StringLength(20, ErrorMessage = "最多20個字")]
            [Required(ErrorMessage = "姓名必填")]
            public string name { get; set; }

            [DisplayName("電話")]
            [RegularExpression(@"^09[0-9]{8}$", ErrorMessage = "需要10碼")]
            //[StringLength(10, ErrorMessage = "需要10碼")]
            [Required(ErrorMessage = "請輸入電話")]
            public string phone { get; set; }

            [DisplayName("縣市")]
            [Required(ErrorMessage = "縣市必填")]
            public string city { get; set; }

            [DisplayName("地區")]
            [Required(ErrorMessage = "地區必填")]
            public string region { get; set; }

            [DisplayName("地址行")]

            
            public string other { get; set; }

            [DisplayName("加入日期")]
            public System.DateTime startDate { get; set; }

            [DisplayName("所剩酷幣")]
            public Nullable<decimal> credits { get; set; }

            [DisplayName("會員類別編號")]
            public string mTypeId { get; set; }

            [DisplayName("飲食方式")]
            public string wayofdietId { get; set; }

            [DisplayName("身分證字號")]

            public string identityId { get; set; }
            [DisplayName("照片")]

            public string photoSticker { get; set; }

        }

        public class MetaTrendingArticles
        {
            [DisplayName("文章編號")]

            public string articlesId { get; set; }
            [DisplayName("內容")]
            [Required(ErrorMessage = "請填寫內容")]

            public string aContent { get; set; }
            [DisplayName("標題")]
            [Required(ErrorMessage = "請加入標題")]

            public string article { get; set; }
            [DisplayName("照片")]
            [Required(ErrorMessage = "請上傳照片")]

            public string image { get; set; }
        }

        public class MataClass
        {
            [Key]
            [DisplayName("課程編號")]
            public int cId { get; set; }

            [DisplayName("課程名稱")]
            [StringLength(20, ErrorMessage = "最多20個字")]
            [Required(ErrorMessage = "課程名稱必填")]
            public string className { get; set; }


            [DisplayName("課程級別")]
           
            public int cGradeId { get; set; }

            [DisplayName("課程類別")]
          
            public int cTypeId { get; set; }

            [DisplayName("教練課程")]
            public int coachId { get; set; }

            [DisplayName("影片")]
            [Required(ErrorMessage = "請上傳影片")]
            public string audio { get; set; }
        }


        public class MataCommodity
        {
            [DisplayName("商品編號")]
            public int commodityId { get; set; }
            [Required(ErrorMessage = "請輸入商品名稱")]
            [DisplayName("商品名稱")]
            public string commodityName { get; set; }
            //[Required(ErrorMessage = "請輸入商品價格")]
            [DisplayName("商品價格")]
            public int commodityPrice { get; set; }
            [DisplayName("商品折扣")]
            public double commodityDiscount { get; set; }
            [Required(ErrorMessage = "請選擇商品圖樣")]
            [DisplayName("商品圖樣")]
            public string commodityImage { get; set; }
            //[Required(ErrorMessage = "請輸入商品描述")]
            [DisplayName("商品描述")]
            public string commodityDescription { get; set; }
            //[Required(ErrorMessage = "請輸入商品庫存")]
            [DisplayName("商品庫存")]
            public int commodityStock { get; set; }
            public Nullable<int> shopId { get; set; }

        }



        public class MataFriend
        {
            [Key]
            [DisplayName("好友編號")]
            public int fId { get; set; }
            [DisplayName("會員編號")]
            public int mId { get; set; }
            [DisplayName("會員編號")]
            public int fmId { get; set; }

            [DisplayName("認證狀態")]
            public bool status { get; set; }

        }



        public class MataActivity
        {
            [Key]
            [DisplayName("活動編號")]
            public int activityId { get; set; }
            [DisplayName("活動名稱")]
            [Required(ErrorMessage = "請填寫活動名稱")]
            public string name { get; set; }
            [DisplayName("活動地點")]
            [Required(ErrorMessage = "請填寫活動地點")]
            public string place { get; set; }
            [DisplayName("活動時間")]
            [Required(ErrorMessage = "請選擇活動時間")]
            public System.DateTime dateTime { get; set; }
            [DisplayName("活動項目")]
            [Required(ErrorMessage = "請填寫活動項目")]
            public string item { get; set; }
            [DisplayName("舉辦人")]
            public string holder { get; set; }
            [DisplayName("活動內容")]
            public string activitycontent { get; set; }
        }

        public class MataHoldList
        {
            [DisplayName("舉辦人")]
            public string organizerName { get; set; }
            [DisplayName("參加者")]
            public string participantName { get; set; }
            [DisplayName("參加人數")]
            public int participantNumber { get; set; }
            public int mId { get; set; }
            public int activityId { get; set; }

            
        }

        public class MataOrderDetails
        {
            
            [Required(ErrorMessage = "請填寫電話號碼")]
            [RegularExpression(@"^09[0-9]{8}$", ErrorMessage = "需要10碼")]
            public Nullable<int> tel { get; set; }
            
            


        }
       
    }
}