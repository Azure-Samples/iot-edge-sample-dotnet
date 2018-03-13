using Xunit;
using filtermodule;

namespace filtermodule.Test
{
  using Microsoft.Azure.Devices.Client;
  
  public class ProgramUnitTest
  {
    [Fact]
    public void messagePropertyTest()
    {
      var source = new Message();
      var target = new Message();
      source.Properties.Add("test", "value");
      Program.CopyProperties(source, target);
      Assert.True(target.Properties.ContainsKey("test"), "Should copy properties");
    }
  }
}