using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQNetCore.Models;
using System.Text;

namespace RabbitMQNetCore.Publisher
{
    public class Publisher
    {
        public bool QueueMessage(Message message)
        {
            var retVal = false;

            try
            {
                //Tạo obj cấu hình
                var factory = new ConnectionFactory() {
                    HostName = "localhost",
                };

                using (var connection = factory.CreateConnection())
                //Tạo 1 channel trên kết nối để thao tác với RabbitMQ
                using (var channel = connection.CreateModel())
                {
                    //[1]Tạo exchange
                    var myExchange = "MyExchange01";
                    channel.ExchangeDeclare(myExchange, type: ExchangeType.Direct);
                    //Tạo queue
                    var myQueue = "MyQueue01";
                    channel.QueueDeclare(queue: myQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    //[2]Liên kết exchange với queue
                    channel.QueueBind(queue: myQueue, exchange: myExchange, routingKey: "MyExchange01_MyQueue01");

                    //Chuyển message thành chuỗi json
                    var stringContent = JsonConvert.SerializeObject(message);
                    //Mã hoá chuỗi Json thành mảng byte
                    var body = Encoding.UTF8.GetBytes(stringContent);

                    //Gửi message exchange
                    //Exchange sẽ định tuyến message đến queue dựa trên routingKey
                    channel.BasicPublish(exchange: myExchange, routingKey: "MyExchange01_MyQueue01", basicProperties: null, body: body);

                    //Sử dụng default exchange mà ko cần [1]khởi tạo exchange và [2]bind queue, routingKey phải trùng tên queue 
                    //channel.BasicPublish(exchange: "", routingKey: "QueueTest1", basicProperties: null, body: body);
                }
                retVal = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return retVal;
        }
    }
}
