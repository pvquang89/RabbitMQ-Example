using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQNetCore.Consumer
{
    public class Consumer
    {
        public void Consume()
        {
            try
            {
                var factory = new ConnectionFactory() {
                    HostName = "localhost",
                };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                var myQueue = "MyQueue01";
                channel.QueueDeclare(queue: myQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

                Console.WriteLine("Waiting for messages.");

                //Tạo 1 consumer để nhận message
                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    //lấy nội dung message dưới dạng byte 
                    var body = ea.Body.ToArray();
                    //chuyển mảng byte thành chuỗi
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("Received {0}", message);
                };

                //Tiêu thụ tin nhắn từ queue
                //Có 2 cơ chế tiêu thụ là : push - BasicConsume() và pull - BasicGet()
                //autoAck : true - tự động coi message đã được xử lý và xoá khỏi queue
                channel.BasicConsume(queue: myQueue, autoAck: true, consumer: consumer);

                Console.WriteLine(" Press enter to exit.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
