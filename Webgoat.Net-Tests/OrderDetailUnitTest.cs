using WebGoatCore.Models;
using Xunit.Sdk;

namespace Webgoat.Net_Tests;


public class OrderDetailUnitTest
{
    [Fact]
    public void OrderDetail_Input_Negative()
    {
        var OrderDetail = new OrderDetail();        

        Assert.Throws<ArgumentOutOfRangeException>(() => OrderDetail.Quantity = -1);


    }
}