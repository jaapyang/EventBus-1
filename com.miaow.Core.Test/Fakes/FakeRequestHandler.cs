using com.miaow.Core.Events;

namespace com.miaow.Core.Test.Fakes
{
    public class FakeRequestHandler : EventHandlerBase<FakeRequest, FakeResponse>
    {
        protected override FakeResponse HandleProcess(FakeRequest request)
        {
            return new FakeResponse();
        }
    }
}