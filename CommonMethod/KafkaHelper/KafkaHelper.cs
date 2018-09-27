using CommonMethodLibrary;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using LogHelperLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KafkaHelperLibrary
{
    /// <summary>
    /// 
    /// </summary>

    public class KafkaHelper
    {
        /// <summary>
        /// Kafka服务器配置
        /// </summary>
        private Dictionary<string, object> config = new Dictionary<string, object>();
        /// <summary>
        /// Topic信息
        /// </summary>
        private string topic = "";
        private KafkaHelper()
        {
            //读取配置
            config.Add("bootstrap.servers", ConfigHelper.GetAppSettingValue("Brokers"));
            topic = ConfigHelper.GetAppSettingValue("Topic");
        }

        /// <summary>
        /// 单例
        /// </summary>
        public static KafkaHelper Instance { get { return _Instance; } }
        private static KafkaHelper _Instance = new KafkaHelper();


        /// <summary>
        /// 发送信息到Kafka
        /// </summary>
        /// <param name="msg"></param>
        public async void SendMsg(string msg)
        {
            try
            {
                using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
                {
                    var result = await producer.ProduceAsync(topic, new Message<Null, string>() { Key = null, Value = msg });
                    LogHelper.Instance.LogDebug("Kafka发送完成");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Instance.LogFatal("Kafka发送失败", ex);
            }
        }
    }
}
