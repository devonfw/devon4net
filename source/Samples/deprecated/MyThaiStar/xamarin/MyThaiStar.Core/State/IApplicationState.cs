using Excalibur.Shared.State;

namespace MyThaiStar.Core.State
{
    public interface IApplicationState : IBaseState
    {
        string Email { get; set; }
    }
}
