using com.miaow.Core.Context;
using com.miaow.Core.Events;
using com.miaow.Core.Test.Fakes;
using FluentAssertions;
using Xunit;

namespace com.miaow.Core.Test.EventBusTests
{
    public class EventBusTests
    {
        [Fact]
        public void Regist_NormalReuqest_will_find_correct_handler()
        {
            ApplicationContext.Current.EventBus.Register<FakeRequest>();
        }

        [Fact]
        public void HasRegisted_exists_request_return_true()
        {
            ApplicationContext.Current.EventBus.Register<FakeRequest>();

            var actual = ApplicationContext.Current.EventBus.HasRegisted<FakeRequest>();

            actual.Should().BeTrue();
        }

        [Fact]
        public void Trigger_Normal_Success()
        {
            ApplicationContext.Current.EventBus.Register<FakeRequest>();

            var actual = ApplicationContext.Current.EventBus.Trigger<FakeRequest,FakeResponse>(new FakeRequest());

            actual.Status.Should().Be(ResponseStatus.Success);
        }
        
    }
}
