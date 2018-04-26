using Xunit;
using FilterModule;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Client.Transport.Mqtt;

namespace FilterModuleTest
{
    public class FilterModuleTest
    {
        [Fact]
        public void filterLessThanThresholdTest()
        {
            var source = createMessage(25 - 1);
            var result = Program.filter(source);
            Assert.True(result == null);
        }

        [Fact]
        public void filterMoreThanThresholdAlertPropertyTest()
        {
            var source = createMessage(25 + 1);
            var result = Program.filter(source);
            Assert.True(result.Properties["MessageType"] == "Alert");
        }

        [Fact]
        public void filterMoreThanThresholdCopyPropertyTest()
        {
            var source = createMessage(25 + 1);
            source.Properties.Add("customTestKey", "customTestValue");
            var result = Program.filter(source);
            Assert.True(result.Properties["customTestKey"] == "customTestValue");
        }

        private Message createMessage(int temperature)
        {
            var messageBody = createMessageBody(temperature);
            var messageString = JsonConvert.SerializeObject(messageBody);
            var messageBytes = Encoding.UTF8.GetBytes(messageString);
            return new Message(messageBytes);
        }

        private MessageBody createMessageBody(int temperature)
        {
            var messageBody = new MessageBody
            {
                machine = new Machine
                {
                    temperature = temperature,
                    pressure = 0
                },
                ambient = new Ambient
                {
                    temperature = 0,
                    humidity = 0
                },
                timeCreated = string.Format("{0:O}", DateTime.Now)
            };

            return messageBody;
        }
    }
}