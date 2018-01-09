using com.miaow.Core.Events;
using com.miaow.Core.Test.Fakes;
using Xunit;

namespace com.miaow.Core.Test.EventBusTests
{
    public class EventBusTests
    {
        [Fact]
        public void Regist_NormalReuqest_will_find_correct_handler()
        {
            EventBus.Current.Register<FakeRequest>();
        }
    }
}
