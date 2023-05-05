
void Main()
{
    var pipe = new PipeBuilder(First)
        .AddPipe(typeof(Try))
        .AddPipe(typeof(Wrap))
        .Build();

    pipe("Start");
}

public abstract class Pipe
{
    protected Action<string> _action;
    public Pipe(Action<string> action)
    {
        _action = action;
    }
    public abstract void Handle(string msg);

}
public class PipeBuilder
{
    Action<string> _mainAction;
    List<Type> pipeTypes;
    public PipeBuilder(Action<string> mainAction)
    {
        _mainAction = mainAction;
        pipeTypes = new List<System.Type>();
    }
    public Action<string> CreatePipe(int index)
    {
        if (index < pipeTypes.Count - 1)
        {
            var childPipe = CreatePipe(index + 1);
            var pipe = (Pipe)Activator.CreateInstance(pipeTypes[index], childPipe);
            return pipe.Handle;
        }
        else
        {
            var finalPipe = (Pipe)Activator.CreateInstance(pipeTypes[index], _mainAction);
            return finalPipe.Handle;

        }
    }
    public PipeBuilder AddPipe(Type pipeType)
    {
        pipeTypes.Add(pipeType);
        return this;
    }
    public Action<string> Build()
    {
        var pipe = CreatePipe(0);
        return pipe;
    }

}
public class Wrap : Pipe
{
    public Wrap(Action<string> action) : base(action)
    {

    }
    public override void Handle(string msg)
    {
        $"Wrap : {msg} ".Dump();
        _action(msg);
        "end wrap".Dump();
    }
}
public class Try : Pipe
{
    public Try(Action<string> action) : base(action)
    {

    }
    public override void Handle(string msg)
    {
        $"Try : {msg} ".Dump();
        _action(msg);
        "end Try".Dump();
    }
}

public void First(string msg)
{
    $"First: {msg}".Dump();
}
public void Second(string msg)
{
    $"Second: {msg}".Dump();
}