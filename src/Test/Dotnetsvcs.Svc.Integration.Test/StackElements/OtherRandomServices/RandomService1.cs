namespace Dotnetsvcs.Svc.Integration.Test.StackElements.OtherRandomServices;
public class RandomService1 : IRandomService1 {
    public void Dispose() {
    }

    public int Sum2(int data) => data + 2;
}
