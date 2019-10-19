using Qiniu.Http;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Internal.Service
{
    public class QiniuService
    {
        private static string AccessKey = "Ni9l6QUYXZRy655Grm5j3UwvMk8yihJAoePtjPf3";
        private static string SecretKey = "N2nyhGJRGYyvfkysyNUoAmu1vn9hkkkawBwdp7uH";
        private static string HuananBucket = "sfyimages";

        static QiniuService()
        {
            Qiniu.Common.Config.AutoZone(AccessKey, HuananBucket, true);
        }
        /// <summary>
        /// 简单上传-上传字节数据
        /// </summary>
        public async Task<bool> UploadData(string saveKey, byte[] data)
        {
            // 生成(上传)凭证时需要使用此Mac
            // 这个示例单独使用了一个Settings类，其中包含AccessKey和SecretKey
            // 实际应用中，请自行设置您的AccessKey和SecretKey
            Mac mac = new Mac(AccessKey, SecretKey);
            string bucket = HuananBucket;
            //string saveKey = "微信图片_20180817133340";
           // byte[] data = System.IO.File.ReadAllBytes(@"D:\images\微信图片_20180817133340.jpg");
            //byte[] data = System.Text.Encoding.UTF8.GetBytes("Hello World!");
            // 上传策略，参见 
            // https://developer.qiniu.com/kodo/manual/put-policy
            PutPolicy putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            // putPolicy.Scope = bucket + ":" + saveKey;
            putPolicy.Scope = bucket;
            // 上传策略有效期(对应于生成的凭证的有效期)          
            putPolicy.SetExpires(3600);
            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            putPolicy.DeleteAfterDays = 1;
            // 生成上传凭证，参见
            // https://developer.qiniu.com/kodo/manual/upload-token            
            string jstr = putPolicy.ToJsonString();
            string token = Auth.CreateUploadToken(mac, jstr);
            FormUploader fu = new FormUploader();
            HttpResult result = fu.UploadData(data, saveKey, token);
            if (result.Code==200)
            {
                return true;
            } 
            return false;
        }
    }
}
